﻿using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProDocEstimate.Editors
{
    public partial class TestAddingRow : Window, INotifyPropertyChanged
    {
        #region Properties
        public string ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        public SqlConnection conn;
        public SqlDataAdapter da;
        public SqlCommand scmd;
        public DataTable dt;
        public DataView dv;
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); }

        public TestAddingRow()
        {
            InitializeComponent();

            string cmd = "SELECT * FROM [ESTIMATING].[dbo].[Test1] ORDER BY ID";

            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            dt = new DataTable("Tester");
            da.Fill(dt);
            dv = dt.DefaultView;
            grdData.ItemsSource = dv;
            DataContext = this;

            PreviewKeyDown += (s, e) => { if (e.Key == Key.Escape) Close(); };

        }

        private void mnuFileExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void grdData_CellEditEnding(object sender, System.Windows.Controls.DataGridCellEditEndingEventArgs e)
        {

            SqlConnection conn = new SqlConnection(ConnectionString);
            var selectedRow = grdData.SelectedItem as DataRowView;
            string ID = selectedRow[0].ToString();
            TextBox t = e.EditingElement as TextBox;
            string editedCellValue = t.Text.ToString();
            string colName = e.Column.SortMemberPath.ToString();

            // Put the new value in quotes if it contains non-numeric characters:
            string cmd = ""; string regExPattern = @"^[0-9]+$"; Regex pattern = new Regex(regExPattern);
            if (pattern.IsMatch(editedCellValue))
            { cmd = $"UPDATE [ESTIMATING].[dbo].[Test1] set {colName} =  {editedCellValue}  WHERE ID = '{ID}'"; }
            else
            { cmd = $"UPDATE [ESTIMATING].[dbo].[Test1] set {colName} = '{editedCellValue}' WHERE ID = '{ID}'"; };

            if (conn.State != ConnectionState.Open) { conn.Open(); }
            SqlCommand cmd2 = new SqlCommand(cmd, conn);

            // If it's not a string and quotes were used, report an error
            try { cmd2.ExecuteNonQuery(); }
            catch (Exception ex) { MessageBox.Show(ex.Message + "\n" + cmd2); }
            finally { conn.Close(); }
            Title = "Table updated";
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            Close();
        }

        private void mnuAddRow_Click(object sender, RoutedEventArgs e)
        {
            string cmd = "INSERT INTO [ESTIMATING].[dbo].[Test1] ( Name ) VALUES ( '' )";  // a blank row; only works if all columns in the table are NOT NULL with a default value
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand(cmd, conn);
            sqlCommand.ExecuteNonQuery();

            cmd = "SELECT MAX(ID) AS ID FROM [ESTIMATING].[dbo].[Test1]";
            da = new SqlDataAdapter(cmd, conn); DataTable dt2 = new DataTable(); da.Fill(dt2);
            string NewID = dt2.Rows[0]["ID"].ToString();
            dv.AddNew();
            dv[dv.Count - 1][0] = NewID;
        }

    }
}