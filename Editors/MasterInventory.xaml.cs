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
    public partial class MasterInventory : Window, INotifyPropertyChanged
    {
        #region Properties
        public string ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        public SqlConnection conn;
        public SqlDataAdapter da;
        public SqlCommand scmd;
        public DataTable dt;
        public DataView dv;

        private string matchThis; public string MatchThis { get { return matchThis; } set { matchThis = value; OnPropertyChanged(); } }
        private string matchIT;   public string MatchIT   { get { return matchIT;   } set { matchIT   = value; OnPropertyChanged(); } }
        private string rowCount;  public string RowCount  { get { return rowCount;  } set { rowCount  = value; OnPropertyChanged(); } }

        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); }

        public MasterInventory()
        {
            InitializeComponent();
            string cmd = "SELECT * FROM [ProVisionDev].[dbo].[MasterInventory] ORDER BY Description";

            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            dt = new DataTable("Details");
            da.Fill(dt);
            dv = dt.DefaultView;
            grdData.ItemsSource = dv;
            DataContext = this;

            PreviewKeyDown += (s, e) => { if (e.Key == Key.Escape) Close(); };
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
            { cmd = $"UPDATE [ESTIMATING].[dbo].[MasterInventory] set {colName} =  {editedCellValue}  WHERE Description = '{ID}'"; }
            else
            { cmd = $"UPDATE [ESTIMATING].[dbo].[MasterInventory] set {colName} = '{editedCellValue}' WHERE Description = '{ID}'"; };

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

        private void MatchDesc_LostFocus(object sender, RoutedEventArgs e)
        {
            dv.RowFilter = $"Description LIKE '*{MatchThis}*'";
            RowCount = dv.Count.ToString() + " matches";
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            dv.RowFilter = "";
            MatchThis = "";
            RowCount = dv.Count.ToString() + " matches";
        }

        private void mnuFileExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}