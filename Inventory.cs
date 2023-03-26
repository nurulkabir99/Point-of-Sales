using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointInvt
{
    public partial class Inventory : Form
    {
        private int childFormNumber = 0;

        public Inventory()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {

        }

        private void OpenFile(object sender, EventArgs e)
        {
            ProfitCheck profit = new ProfitCheck();
            profit.Show();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //LayoutMdi(MdiLayout.Cascade);
            ProfitCheck profit = new ProfitCheck();
            profit.Show();
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cashPurchaseشراءنقداToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cashPurchase csPurchase = new cashPurchase();
            csPurchase.Show();
        }

        private void cashSales_Click(object sender, EventArgs e)
        {
            cashSales csSales = new cashSales();
            csSales.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

            StockCheck stock = new StockCheck();
            stock.Show();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            cashPurchase csPurchase = new cashPurchase();
            csPurchase.Show();
        }


        public static string InvcId;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //BranchInfo.BranchID = (comboBox2.SelectedIndex + 1).ToString();
            InvcId = SearchMenuText.Text;
            PurchasesViewer sendData1 = new PurchasesViewer(InvcId);
            sendData1.ShowDialog();
           
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            StockCheck stock = new StockCheck();
            stock.Show();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            DailySheet dailySheet = new DailySheet();
            dailySheet.Show();
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            PurchaseOrder PO = new PurchaseOrder();
            PO.Show();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            
            PODetails poDetails =new PODetails();
            poDetails.Show();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AccountsHead headOfAccount = new AccountsHead();
            headOfAccount.Show();
        }

        private void newToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AccountingChart accountingChart = new AccountingChart();
            accountingChart.Show();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Journal journal= new Journal();
            journal.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            BalanceQuery bal = new BalanceQuery();
            bal.Show();
        }

        private void balanceSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrialBalance trial = new TrialBalance();
            trial.Show();
        }

        private void toolStripButton10_Click_1(object sender, EventArgs e)
        {
            cashSales csSales = new cashSales();
            csSales.Show();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newEployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            employee.Show();
        }

        private void Inventory_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            Login lg = new Login();
            lg.Show();
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StockCheck stock = new StockCheck();
            stock.Show();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            TrialBalance trBal = new TrialBalance();
            trBal.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            ScanImage scan = new ScanImage();
            scan.Show();

        }

        private void addNewItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewItem aditem = new AddNewItem();
            aditem.Show();
        }

        private void civilIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListofScanImage listofScan= new ListofScanImage();
            listofScan.Show();
        }

        private void profitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfitBar pb = new ProfitBar();
           
           
            
        }

        private void branchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfitBar pb = new ProfitBar();
            pb.Show();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            Repair rp = new Repair();
            rp.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            
            Commission cmons= new Commission();
            cmons.Show();
            //cashPurchase.BranchInfo.BranchID = (comboBox2.SelectedIndex + 1).ToString();
            //cashPurchase.BranchInfo.InvoiceNo = invoiceNoText.Text;
            //BarCodeViewer brv = new BarCodeViewer();
            //brv.Show();
        }

        private void payableAccountToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreditList clList = new CreditList();
            clList.Show();
        }

        private void creditPurchaseشراءالائتمانToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreditPurchase creditPurchase = new CreditPurchase();
            creditPurchase.Show();
        }

        private void purchaseAnalysisتحليلالشراءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PurchaseAnalysis purchaseAnalysis= new PurchaseAnalysis();
            purchaseAnalysis.Show();
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
           string conString = Login.LoginInfo.conString;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand cmd1 =
                new SqlCommand("select ItemCode,ItemName from ItemList ", con);
            cmd1.ExecuteNonQuery();
            SqlDataReader read1 = cmd1.ExecuteReader();

            while (read1.Read())
            {
                toolStripComboBox1.Items.Add(read1["ItemCode"].ToString() + '-' + read1["ItemName"].ToString());
               
            }
            con.Close();
            //toolStripComboBox1.SelectedIndex = 0;
            //toolStripComboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //toolStripComboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;

        }
    }
}
