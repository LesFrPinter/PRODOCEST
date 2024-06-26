﻿using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ProDocEstimate.Views
{
    public partial class Finishing : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); }

        #region Properties

        public string ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        public SqlConnection? conn;
        public SqlDataAdapter? da;
        public DataTable? dt;
        public SqlCommand? scmd;
        public DataView? dv;

        private float linearInchCostCello; public float LinearInchCostCello { get { return linearInchCostCello; } set { linearInchCostCello = value; OnPropertyChanged(); } }
        private float linearInchCostBook;  public float LinearInchCostBook  { get { return linearInchCostBook;  } set { linearInchCostBook  = value; OnPropertyChanged(); } }
        private float linearFeet;          public float LinearFeet          { get { return linearFeet;          } set { linearFeet          = value; OnPropertyChanged(); } }

        private int    max;         public int    Max         { get { return max;           } set { max = value;         OnPropertyChanged(); } }
        private string pressSize;   public string PressSize   { get { return pressSize;     } set { pressSize = value;   OnPropertyChanged(); } }
        private string collatorCut; public string CollatorCut { get { return collatorCut;   } set { collatorCut = value; OnPropertyChanged(); } }
        private string rollWidth;   public string RollWidth   { get { return rollWidth;     } set { rollWidth = value;   OnPropertyChanged(); } }

        private string quoteNum; public string QuoteNum { get { return quoteNum; } set { quoteNum = value; OnPropertyChanged(); } }
        private string quoteNo; public string QuoteNo { get { return quoteNo; } set { quoteNo = value; } }

        private string book = "";        public string Book       { get { return book;       } set { book        = value; OnPropertyChanged(); } }
        private string cello = "";       public string Cello      { get { return cello;      } set { cello       = value; OnPropertyChanged(); } }
        private string drillHoles = "";  public string DrillHoles { get { return drillHoles; } set { drillHoles  = value; OnPropertyChanged(); } }
        private string pad = "";         public string Pad        { get { return pad;        } set { pad         = value; OnPropertyChanged(); } }
        private string trim = "";        public string Trim       { get { return trim;       } set { trim        = value; OnPropertyChanged(); } }

        private float flatCharge; public float FlatCharge { get { return flatCharge; } set { flatCharge = value; OnPropertyChanged(); } }
        private float baseflatCharge; public float BaseFlatCharge { get { return baseflatCharge; } set { baseflatCharge = value; OnPropertyChanged(); } }
        private float flatChargePct; public float FlatChargePct { get { return flatChargePct; } set { flatChargePct = value; OnPropertyChanged(); } }
        private float calculatedflatCharge; public float CalculatedFlatCharge { get { return calculatedflatCharge; } set { calculatedflatCharge = value; OnPropertyChanged(); } }

        private float runCharge; public float RunCharge { get { return runCharge; } set { runCharge = value; OnPropertyChanged(); } }
        private float baserunCharge; public float BaseRunCharge { get { return baserunCharge; } set { baserunCharge = value; OnPropertyChanged(); } }
        private float runChargePct; public float RunChargePct { get { return runChargePct; } set { runChargePct = value; OnPropertyChanged(); } }
        private float calculatedrunCharge; public float CalculatedRunCharge { get { return calculatedrunCharge; } set { calculatedrunCharge = value; OnPropertyChanged(); } }

        private float plateCharge; public float PlateCharge { get { return plateCharge; } set { plateCharge = value; OnPropertyChanged(); } }
        private float baseplateCharge; public float BasePlateCharge { get { return baseplateCharge; } set { baseplateCharge = value; OnPropertyChanged(); } }
        private float plateChargePct; public float PlateChargePct { get { return plateChargePct; } set { plateChargePct = value; OnPropertyChanged(); } }
        private float calculatedplateCharge; public float CalculatedPlateCharge { get { return calculatedplateCharge; } set { calculatedplateCharge = value; OnPropertyChanged(); } }

        private float finishCharge; public float FinishCharge { get { return finishCharge; } set { finishCharge = value; OnPropertyChanged(); } }
        private float basefinishCharge; public float BaseFinishCharge { get { return basefinishCharge; } set { basefinishCharge = value; OnPropertyChanged(); } }
        private float finishChargePct; public float FinishChargePct { get { return finishChargePct; } set { finishChargePct = value; OnPropertyChanged(); } }
        private float calculatedfinishCharge; public float CalculatedFinishCharge { get { return calculatedfinishCharge; } set { calculatedfinishCharge = value; OnPropertyChanged(); } }

        private float pressCharge; public float PressCharge { get { return pressCharge; } set { pressCharge = value; OnPropertyChanged(); } }
        private float basepressCharge; public float BasePressCharge { get { return basepressCharge; } set { basepressCharge = value; OnPropertyChanged(); } }
        private float pressChargePct; public float PressChargePct { get { return pressChargePct; } set { pressChargePct = value; OnPropertyChanged(); } }
        private float calculatedpressCharge; public float CalculatedPressCharge { get { return calculatedpressCharge; } set { calculatedpressCharge = value; OnPropertyChanged(); } }

        private float convCharge; public float ConvCharge { get { return convCharge; } set { convCharge = value; OnPropertyChanged(); } }
        private float baseconvCharge; public float BaseConvCharge { get { return baseconvCharge; } set { baseconvCharge = value; OnPropertyChanged(); } }
        private float convChargePct; public float ConvChargePct { get { return convChargePct; } set { convChargePct = value; OnPropertyChanged(); } }
        private float calculatedconvCharge; public float CalculatedConvCharge { get { return calculatedconvCharge; } set { calculatedconvCharge = value; OnPropertyChanged(); } }

        private float  flatTotal; public float FlatTotal  { get { return flatTotal; } set { flatTotal = value; OnPropertyChanged(); } }
        private string fieldList; public string FieldList { get { return fieldList; } set { fieldList = value; OnPropertyChanged(); } }

        private int labPS;  public int LabPS  { get { return labPS;  } set { labPS  = value; OnPropertyChanged(); } }
        private int labCS;  public int LabCS  { get { return labCS;  } set { labCS  = value; OnPropertyChanged(); } }
        private int labBS;  public int LabBS  { get { return labBS;  } set { labBS  = value; OnPropertyChanged(); } }
        private int labPSL; public int LabPSL { get { return labPSL; } set { labPSL = value; OnPropertyChanged(); } }
        private int labCSL; public int LabCSL { get { return labCSL; } set { labCSL = value; OnPropertyChanged(); } }
        private int labBSL; public int LabBSL { get { return labBSL; } set { labBSL = value; OnPropertyChanged(); } }

        private int pressSetup; public int PressSetup { get { return pressSetup; } set { pressSetup = value; OnPropertyChanged(); } }
        private int collatorSetup; public int CollatorSetup { get { return collatorSetup; } set { collatorSetup = value; OnPropertyChanged(); } }
        private int binderySetup; public int BinderySetup { get { return binderySetup; } set { binderySetup = value; OnPropertyChanged(); } }

        private int basePressSetup; public int BasePressSetup { get { return basePressSetup; } set { basePressSetup = value; OnPropertyChanged(); } }
        private int baseCollatorSetup; public int BaseCollatorSetup { get { return baseCollatorSetup; } set { baseCollatorSetup = value; OnPropertyChanged(); } }
        private int baseBinderySetup; public int BaseBinderySetup { get { return baseBinderySetup; } set { baseBinderySetup = value; OnPropertyChanged(); } }

        private int pressSlowdown; public int PressSlowdown { get { return pressSlowdown; } set { pressSlowdown = value; OnPropertyChanged(); } }
        private int collatorSlowdown; public int CollatorSlowdown { get { return collatorSlowdown; } set { collatorSlowdown = value; OnPropertyChanged(); } }
        private int binderySlowdown; public int BinderySlowdown { get { return binderySlowdown; } set { binderySlowdown = value; OnPropertyChanged(); } }

        private int basepressSlowdown; public int BasePressSlowdown { get { return basepressSlowdown; } set { basepressSlowdown = value; OnPropertyChanged(); } }
        private int basecollatorSlowdown; public int BaseCollatorSlowdown { get { return basecollatorSlowdown; } set { basecollatorSlowdown = value; OnPropertyChanged(); } }
        private int basebinderySlowdown; public int BaseBinderySlowdown { get { return basebinderySlowdown; } set { basebinderySlowdown = value; OnPropertyChanged(); } }

        private int calculatedPS; public int CalculatedPS { get { return calculatedPS; } set { calculatedPS = value; OnPropertyChanged(); } }
        private int calculatedCS; public int CalculatedCS { get { return calculatedCS; } set { calculatedCS = value; OnPropertyChanged(); } }
        private int calculatedBS; public int CalculatedBS { get { return calculatedBS; } set { calculatedBS = value; OnPropertyChanged(); } }

        private int slowdownTotal; public int SlowdownTotal { get { return slowdownTotal; } set { slowdownTotal = value; OnPropertyChanged(); } }
        private float setupTotal; public float SetupTotal { get { return setupTotal; } set { setupTotal = value; OnPropertyChanged(); } }

        #endregion

        public Finishing(string QUOTENUM, string PRESSSIZE, string COLLATORCUT, string ROLLWIDTH)
        {
            InitializeComponent();
            this.DataContext = this;

//            Title = "Quote #: " + QUOTENUM;

            // Local properties (variables)
            QuoteNum = QUOTENUM;
            PressSize = PRESSSIZE;
            CollatorCut = COLLATORCUT;
            RollWidth = ROLLWIDTH;

            StringToNumber sTOn = new StringToNumber();
            LinearFeet = sTOn.Convert(COLLATORCUT) / 12.0F;

            SqlConnection conn = new(ConnectionString);

            LoadDropDowns();
            LoadData();
            LoadBaseValues();
            CalculateBaseFinishCharge();

            PreviewKeyDown += (s, e) => { if (e.Key == Key.Escape) Close(); };
        }

        private void CalculateBaseFinishCharge()
        {
            LinearInchCostCello = 0; LinearInchCostBook = 0;
            conn = conn ?? new(ConnectionString);

            StringToNumber sTOn = new StringToNumber();
            float rw = sTOn.Convert(RollWidth);
            string srw = rw.ToString();

            string cmd = "SELECT * FROM [ESTIMATING].[dbo].[Bindery_Materials] WHERE Category = 'Finishing' AND F_TYPE = 'Cello' "
                       + $" AND {srw} BETWEEN MinRollWidth AND MaxRollWidth";
            SqlDataAdapter da = new(cmd, conn); DataTable dt = new(); dt.Rows.Clear(); da.Fill(dt); DataView dv2 = dt.DefaultView;
            if (Cello.ToString().TrimEnd().Length > 0) { LinearInchCostCello = float.Parse(dv2[0]["LinearInchCost"].ToString()); }

            da.SelectCommand.CommandText = cmd.Replace("Cello", "Book"); dt.Clear(); da.Fill(dt); DataView dv3 = dt.DefaultView;
            if (Book.ToString().TrimEnd().Length > 0) { LinearInchCostBook = float.Parse(dv2[0]["LinearInchCost"].ToString()); }

//            LinearInchCostBook = float.Parse(dv2[0]["LinearInchCost"].ToString()); // Store in Value7

            LinearInchCostBook *= LinearFeet;
            LinearInchCostCello *= LinearFeet;

            BaseFinishCharge = LinearInchCostBook + LinearInchCostCello;
            CalculatedFinishCharge = BaseFinishCharge;
        }

        public void OnLoad(object sender, RoutedEventArgs e)
        {
            this.Height = this.Height *= (1.0F + MainWindow.FeatureZoom);
            this.Width = this.Width *= (1.0F + MainWindow.FeatureZoom);
            Top = 50;
        }

        private void LoadDropDowns()
        {

            // Make the '4-5' entry the last one in the list. Sorting in alphabetical order doesn't work here.
            string str = $"SELECT F_TYPE, CONVERT(VARCHAR(10),CONVERT(INT,NUMBER)) AS NUMBER FROM [ESTIMATING].[dbo].[FEATURES] WHERE CATEGORY = 'FINISHING' AND PRESS_SIZE = '{PressSize}' AND NUMBER NOT LIKE '%-%'"
                       + $" UNION ALL SELECT F_TYPE,                                  NUMBER FROM [ESTIMATING].[dbo].[FEATURES] WHERE CATEGORY = 'FINISHING' AND PRESS_SIZE = '{PressSize}' AND (NUMBER LIKE '%-%')  "
                       +  " ORDER BY F_TYPE";

            SqlConnection conn = new(ConnectionString);
            SqlDataAdapter da = new(str, conn); dt = new(); da.Fill(dt);
            dv = dt.DefaultView;

            M1.Items.Clear(); M2.Items.Clear(); M3.Items.Clear(); M4.Items.Clear(); M5.Items.Clear();
            //NOTE: In order to let the user blank a value that was previously entered, " " is the first item in each ItemsList in each of the five ComboBoxes.
            M1.Items.Add(""); M2.Items.Add(""); M3.Items.Add(""); M4.Items.Add(""); M5.Items.Add("");   // allows them to leave any selection blank

            dv.RowFilter = "F_TYPE='BOOK'";         for (int i = 0; i < dv.Count; i++) { M1.Items.Add(dv[i]["number"].ToString()); }
            dv.RowFilter = "F_TYPE='CELLO'";        for (int i = 0; i < dv.Count; i++) { M2.Items.Add(dv[i]["number"].ToString()); }
            dv.RowFilter = "F_TYPE='DRILL HOLES'";  for (int i = 0; i < dv.Count; i++) { M3.Items.Add(dv[i]["number"].ToString()); }
            dv.RowFilter = "F_TYPE='PAD'";          for (int i = 0; i < dv.Count; i++) { M4.Items.Add(dv[i]["number"].ToString()); }
            dv.RowFilter = "F_TYPE='TRIM'";         for (int i = 0; i < dv.Count; i++) { M5.Items.Add(dv[i]["number"].ToString()); }
        }

        private void LoadBaseValues()
        {
            // This runs any time one of the five parameters in the dropdowns changes

            BaseFlatCharge = 0;
            BaseRunCharge = 0;
//            BaseFinishCharge = 0;
            BaseConvCharge = 0;
            BasePlateCharge = 0;
            BasePressCharge = 0;

            BasePressSetup = 0;
            BaseCollatorSetup = 0;
            BaseBinderySetup = 0;
            BasePressSlowdown = 0;
            BaseCollatorSlowdown = 0;
            BaseBinderySlowdown = 0;

            SqlConnection conn = new(ConnectionString);
            string str = "SELECT * FROM [ESTIMATING].[dbo].[FEATURES]"
                        + " WHERE CATEGORY   = 'FINISHING'"
                        + $"   AND PRESS_SIZE = '{PressSize}'"
                        + $"   AND ((F_TYPE = 'BOOK'        AND Number = '{Book}')"
                        + $"    OR  (F_TYPE = 'CELLO'       AND Number = '{Cello}')"
                        + $"    OR  (F_TYPE = 'DRILL HOLES' AND Number = '{DrillHoles}')"
                        + $"    OR  (F_TYPE = 'PAD'         AND Number = '{Pad}')"
                        + $"    OR  (F_TYPE = 'TRIM'        AND Number = '{Trim}'))"
                        + " ORDER BY F_TYPE";

            SqlDataAdapter da = new(str, conn);
            dt = new DataTable("Features");
            dt.Clear();
            da.Fill(dt);
            dv = dt.DefaultView;

            for (int i = 0; i < dv.Count; i++)
            {   float t1 = 0.00F; float.TryParse(dv[i]["FLAT_CHARGE"].ToString(),     out t1); BaseFlatCharge       += t1;
                float t2 = 0.00F; float.TryParse(dv[i]["RUN_CHARGE"].ToString(),      out t2); BaseRunCharge        += t2;
//                float t3 = 0.00F; float.TryParse(dv[i]["FINISH_MATL"].ToString(),     out t3); BaseFinishCharge     += t3;
                float t4 = 0.00F; float.TryParse(dv[i]["CONV_MATL"].ToString(),       out t4); BaseConvCharge       += t4;
                float t5 = 0.00F; float.TryParse(dv[i]["PLATE_MATL"].ToString(),      out t5); BasePlateCharge      += t5;
                float t6 = 0.00F; float.TryParse(dv[i]["PRESS_MATL"].ToString(),      out t6); BasePressCharge      += t6;

                int l1 = 0;       int.TryParse(dv[i]["PRESS_SETUP_TIME"].ToString(),  out l1); BasePressSetup       += l1;
                int l2 = 0;       int.TryParse(dv[i]["COLLATOR_SETUP"].ToString(),    out l2); BaseCollatorSetup    += l2;
                int l3 = 0;       int.TryParse(dv[i]["BINDERY_SETUP"].ToString(),     out l3); BaseBinderySetup     += l3;
                int l4 = 0;       int.TryParse(dv[i]["PRESS_SLOWDOWN"].ToString(),    out l4); BasePressSlowdown    += l4;
                int l5 = 0;       int.TryParse(dv[i]["COLLATOR_SLOWDOWN"].ToString(), out l5); BaseCollatorSlowdown += l5;
                int l6 = 0;       int.TryParse(dv[i]["BINDERY_SLOWDOWN"].ToString(),  out l6); BaseBinderySlowdown  += l6;
            }

        }

        private void LoadData()
        {
            string str = $"SELECT * FROM [ESTIMATING].[dbo].[Quote_Details] WHERE QUOTE_NUM = '{QuoteNum}' AND Category = 'Finishing'";
            SqlConnection conn = new(ConnectionString);
            SqlDataAdapter da = new(str, conn); DataTable dt = new(); dt.Rows.Clear(); da.Fill(dt);

            if (dt.Rows.Count == 0) return;

            dv = dt.DefaultView;
            Book        = dv[0]["Value1"].ToString();
            Cello       = dv[0]["Value2"].ToString();
            DrillHoles  = dv[0]["Value3"].ToString();
            Pad         = dv[0]["Value4"].ToString();
            Trim        = dv[0]["Value5"].ToString();

            float BookLinearInchCost  = float.Parse(dv[0]["Value6"].ToString());
            float CelloLinearInchCost = float.Parse(dv[0]["Value7"].ToString());

            float t = 0.00F;
            t = 0.00F; float.TryParse(dv[0]["FlatChargePct"]   .ToString(), out t); FlatChargePct   = t;
            t = 0.00F; float.TryParse(dv[0]["RunChargePct"]    .ToString(), out t); RunChargePct    = t;
            t = 0.00F; float.TryParse(dv[0]["ConvertChargePct"].ToString(), out t); ConvChargePct   = t;
            t = 0.00F; float.TryParse(dv[0]["PlateChargePct"]  .ToString(), out t); PlateChargePct  = t;
            t = 0.00F; float.TryParse(dv[0]["PressChargePct"]  .ToString(), out t); PressChargePct  = t;
            t = 0.00F; float.TryParse(dv[0]["FinishChargePct"] .ToString(), out t); FinishChargePct = t;

            //Labor adjustments
            int k;
            k = 0; int.TryParse(dt.Rows[0]["PRESS_ADDL_MIN"]   .ToString(), out k); LabPS  = k;
            k = 0; int.TryParse(dt.Rows[0]["PRESS_SLOW_PCT"]   .ToString(), out k); LabPSL = k;
            k = 0; int.TryParse(dt.Rows[0]["COLL_ADDL_MIN"]    .ToString(), out k); LabCS  = k;
            k = 0; int.TryParse(dt.Rows[0]["COLL_SLOW_PCT"]    .ToString(), out k); LabCSL = k;
            k = 0; int.TryParse(dt.Rows[0]["BIND_ADDL_MIN"]    .ToString(), out k); LabBS  = k;
            k = 0; int.TryParse(dt.Rows[0]["BIND_SLOW_PCT"]    .ToString(), out k); LabBSL = k;

        }

        private void GrandTotal()
        { CalcAll(); }

        private void SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            LoadBaseValues();
            CalculateBaseFinishCharge();
            GrandTotal();
        }

        private void PctChanged(object sender, Telerik.Windows.Controls.RadRangeBaseValueChangedEventArgs e)
        {
            CalcAll();
        }

        private void CalcAll()
        {
            CalculatedConvCharge   = BaseConvCharge   * (1 + (ConvChargePct   / 100));
            CalculatedFinishCharge = BaseFinishCharge * (1 + (FinishChargePct / 100));
            CalculatedFlatCharge   = BaseFlatCharge   * (1 + (FlatChargePct   / 100));
            CalculatedRunCharge    = BaseRunCharge    * (1 + (RunChargePct    / 100));
            CalculatedPlateCharge  = BasePlateCharge  * (1 + (PlateChargePct  / 100));
            CalculatedPressCharge  = BasePressCharge  * (1 + (PressChargePct  / 100));

            FlatTotal =
                  CalculatedConvCharge
                + CalculatedFinishCharge
                + CalculatedFlatCharge
                + CalculatedPlateCharge
                + CalculatedPressCharge;

            //CalculateBaseFinishCharge();        // I just added this...
            CalculateLabor();
        }

        private void CalcLabor(object sender, Telerik.Windows.Controls.RadRangeBaseValueChangedEventArgs e)
        {
            CalculateLabor();
        }

        private void CalculateLabor()
        {
            PressSetup       = BasePressSetup       + LabPS;
            CollatorSetup    = BaseCollatorSetup    + LabCS;
            BinderySetup     = BaseBinderySetup     + LabBS;
            SetupTotal       = BinderySetup;

            PressSlowdown    = BasePressSlowdown    + LabPSL;
            CollatorSlowdown = BaseCollatorSlowdown + LabCSL;
            BinderySlowdown  = BaseBinderySlowdown  + LabBSL;
            SlowdownTotal    = PressSlowdown + CollatorSlowdown + BinderySlowdown;
        }

        private void btnSave_Click  (object sender, RoutedEventArgs e)
        {
            // Delete 'Finishing' quote_detail line, then re-insert one.
            string cmd = $"DELETE [ESTIMATING].[dbo].[Quote_Details] WHERE Quote_Num = '{QuoteNum}' AND Category = 'Finishing'";
            conn = new SqlConnection(ConnectionString); SqlCommand scmd = new SqlCommand(cmd, conn); conn.Open();

            try { scmd.ExecuteNonQuery(); }
            catch (System.Exception ex) { MessageBox.Show(ex.Message); }
            finally { conn.Close(); }

            string FlatChg = FlatTotal.ToString("F6");
            float FC = float.Parse(FlatChg.ToString());

            // Store in Quote_Detail table:
            cmd = "INSERT INTO [ESTIMATING].[dbo].[Quote_Details] ("
                + "   Quote_Num,         Category,         Sequence, "
                + "   Param1,            Param2,           Param3,              Param4,             Param5,             Param6,                 Param7, "
                + "   Value1,            Value2,           Value3,              Value4,             Value5,             Value6,                 Value7, "
                + "   FlatChargePct,     RunChargePct,     PlateChargePct,      FinishChargePct,    PressChargePct,     ConvertChargePct,       TotalFlatChg,       PerThousandChg,"
                + "   PRESS_ADDL_MIN,    COLL_ADDL_MIN,    BIND_ADDL_MIN,       PRESS_SLOW_PCT,     COLL_SLOW_PCT,      BIND_SLOW_PCT,"
                + "   PressSetupMin,     PressSlowPct,     CollSetupMin,        CollSlowPct,        BindSetupMin,       BindSlowPct,            SETUP_MINUTES,      SLOWDOWN_PERCENT ) "
                + "   VALUES ( "
                + $" '{QuoteNum}',       'Finishing',      8,"
                + "   'Book',            'Cello',          'Drill Holes',      'Pad',               'Trim',             'LinearInchCostCello', 'LinearInchCostBook', "
                + $" '{Book}',          '{Cello}',        '{DrillHoles}',     '{Pad}',             '{Trim}',            {LinearInchCostCello}, {LinearInchCostBook}, "
                + $" '{FlatChargePct}', '{RunChargePct}', '{PlateChargePct}', '{FinishChargePct}', '{PressChargePct}', '{ConvChargePct}',     '{FC}',               '{CalculatedRunCharge}',"
                + $"  {LabPS},           {LabCS},          {LabBS},            {LabPSL},            {LabCSL},           {LabBSL}, "
                + $"  {PressSetup},      {PressSlowdown},  {CollatorSetup},    {CollatorSlowdown},  {BinderySetup},     {BinderySlowdown},     {SetupTotal},       {SlowdownTotal} )";

            scmd.CommandText = cmd; 
            conn.Open(); 
            try { scmd.ExecuteNonQuery(); } 
            catch (System.Exception ex) { MessageBox.Show(ex.Message); } 
            finally { conn.Close(); }

            cmd =  "UPDATE [ESTIMATING].[dbo].[Quote_Details]     "
                + $" SET PressSlowdown      = {PressSlowdown},    "
                + $"     ConvertingSlowdown = {CollatorSlowdown}, "
                + $"     FinishingSlowdown  = {BinderySlowdown},  "
                + $"     PrePress           = {CalculatedPlateCharge},"
                + $"     Press              = {PressSetup}, "
                + $"     Converting         = {CollatorSetup},  "
                + $"     Finishing          = {BinderySetup} "
                + $" WHERE Quote_Num = '{QuoteNum}' "
                + "   AND CATEGORY   = 'Finishing'";
            scmd.CommandText = cmd;
            conn.Open();
            try { scmd.ExecuteNonQuery(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { conn.Close(); }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        { Close(); }

    }
}