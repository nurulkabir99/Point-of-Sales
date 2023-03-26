using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace PointInvt
{
    public partial class BalanceQuery : Form
    {
        public string conString;
        public BalanceQuery()
        {
            InitializeComponent();

            StreamReader reader = new StreamReader("C:\\Safi\\ConString.txt");
          conString = reader.ReadLine();
            reader.Close();
            //string sessionId=Login.LoginInfo.UserID;
            //fromDate.Value = DateTime.Today.AddYears(-1);
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            DataTable BrachList = new DataTable();

            using (SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM BranchList", con))
            {
                da1.Fill(BrachList);
            }

            foreach (DataRow da1 in BrachList.Rows)
            {
                branchCombo.Items.Add(da1[2].ToString());
               

            }
           // DataTable linkcat = new DataTable();

           //using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Itemlist", con))
           //     {
           //         da.Fill(linkcat);
           //     }
           // ItemListCombo.Items.Add("ALL");
           // foreach (DataRow da in linkcat.Rows)
           // {
           //    ItemListCombo.Items.Add(da[1].ToString()+"-"+da[2].ToString());
           //     //ItemListCombo.Items.Add(da[2].ToString());
               
           // }
           // con.Close();
           // ItemListCombo.SelectedIndex=0;
           // branchCombo.SelectedIndex = 0;
           // ItemListCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
           // ItemListCombo.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        public static string frDate;
        public static string tDate;
        public static string AccountNo;
        public  DataTable dt =new DataTable();
        public static string branchNo;
  
       

        private void button1_Click(object sender, EventArgs e)
        {
            frDate = fromDate.Value.Date.ToString("yyyy-MM-dd");
            tDate = toDate.Value.Date.ToString("yyyy-MM-dd");
            //ItemCo = ItemListCombo.Text.Split('-').Skip(0).FirstOrDefault();
            //ItemCo =ItemListCombo.Text.TrimEnd(new[] { '-' });
            branchNo = (branchCombo.SelectedIndex + 1).ToString();
            string balnow="0";
            string balPre="0";
            string balnet="0";
            SqlConnection con = new SqlConnection(conString);
           
            try
            {
                con.Open();
                SqlCommand invIdSqlCommand = new SqlCommand("select sum(Amount) as Amount from Accounts where branch='" + (branchCombo.SelectedIndex + 1) + "' and date < '" + frDate + "' and AccountId='" + AccountNo + "'", con);
                invIdSqlCommand.ExecuteNonQuery();
                SqlDataReader read = invIdSqlCommand.ExecuteReader();

                while (read.Read())
                {
                    balPre= (read["Amount"].ToString());
                    totalAmountTxt.Text = balPre;

                }
                if (balPre=="")
                {
                    balPre = "0";
                }

                con.Close();
                con.Open();
                //initialize a new instance of sqlcommand
                SqlCommand cmd = new SqlCommand();
                //set a connection used by this instance of sqlcommand
                cmd.Connection = con;
                //set the sql statement to execute at the data source
                //if (ItemListCombo.Text=="ALL")
                //{
                //    cmd.CommandText =
                //    "select Itemcode,ItemName, SUM(qty)as Quantity, CostUnit ,SUM(PurchaseCost) as Cost,SerialNo from Inventory where branch='" + (branchCombo.SelectedIndex + 1) + "' and date between '" + frDate + "'and '" + tDate + "' group by ItemCode , SerialNo, ItemName,CostUnit";
               
                //}
                //else
                //{
                cmd.CommandText =
                "select date,VoucharNo, Description, Debit ,Credit,Amount from Accounts where branch='" + (branchCombo.SelectedIndex + 1) + "' and date between '" + frDate + "'and '" + tDate + "' and AccountId='" + AccountNo + "'";
                
                //}
                ////initialize a new instance of sqlDataAdapter
                SqlDataAdapter da = new SqlDataAdapter();
                //set the sql statement or stored procedure to execute at the data source
                da.SelectCommand = cmd;
                //initialize a new instance of DataTable
                //DataTable dt = new DataTable();
                //add or resfresh rows in the certain range in the datatable to match those in the data source.
                dt.Clear();
                da.Fill(dt);

                //inventoryListGrid.Rows.Clear();
                //set the data source to display the data in the datagridview

              
                inventoryListGrid.Columns[0].DataPropertyName = "date";
                inventoryListGrid.Columns[1].DataPropertyName = "VoucharNo";
                inventoryListGrid.Columns[2].DataPropertyName = "Description";
                inventoryListGrid.Columns[3].DataPropertyName = "Debit";
                inventoryListGrid.Columns[4].DataPropertyName = "Credit";
                inventoryListGrid.Columns[5].DataPropertyName = "Amount";
             
            
                inventoryListGrid.DataSource = dt;
                //inventoryListGrid.Refresh();
                //inventoryListGrid.Parent.Refresh();
               

                double sum = 0;
                double sum1 = 0;
                double sum2 = 0;
                for (int i = 0; i < inventoryListGrid.Rows.Count; ++i)
                {
                    sum += Convert.ToDouble(inventoryListGrid.Rows[i].Cells[3].Value);
                    sum1 += Convert.ToDouble(inventoryListGrid.Rows[i].Cells[4].Value);
                    sum2 += Convert.ToDouble(inventoryListGrid.Rows[i].Cells[5].Value);
                }
                dbtxt.Text = sum.ToString();
                crtxt.Text = sum1.ToString();
                balnow = sum2.ToString();
                TotalITxt.Text = balnow;
                netBalanceTxt.Text =(Convert.ToDouble(balPre) + Convert.ToDouble(balnow)).ToString();
                con.Close();

                //chart1.DataSource = dt;
                ////set the member of the chart data source used to data bind to the X-values of the series  
                //chart1.Series["Product List"].XValueMember = "ItemCode";
                ////set the member columns of the chart data source used to data bind to the X-values of the series  
                //chart1.Series["Product List"].YValueMembers = "Quantity";
                //chart1.Titles.Clear();
                //chart1.Titles.Add("Product List"); 
            }
            catch (Exception ex)
            {
                //catching error 
                MessageBox.Show(ex.Message);
            }
            //release all resources used by the component
            //da.Dispose();
            //clossing connection
           


        }
       
        private void button1_Click_1(object sender, EventArgs e)
        {
            
            InventoryReportViewer sendData1 = new InventoryReportViewer(dt);
            sendData1.ShowDialog();

           
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {

            InventoryReportViewer sendData1 = new InventoryReportViewer(dt);
            sendData1.ShowDialog();

        }

        private void branchCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BalanceQuery_Load(object sender, EventArgs e)
        {
            DataTable dts = this.GetData("SELECT HeadOfAccounts as Id, Name FROM HeadOfAccount");
            this.PopulateTreeView(dts, 0, null);
        }

        private void PopulateTreeView(DataTable dtParent, int parentId, TreeNode treeNode)
        {
            foreach (DataRow row in dtParent.Rows)
            {
                TreeNode child = new TreeNode
                {
                    Text = row["ID"].ToString() + "-" + row["Name"].ToString(),
                    Tag = row["Id"]
                };
                if (parentId == 0)
                {
                    treeView1.Nodes.Add(child);
                    DataTable dtChild = this.GetData("SELECT ChartOfAccounts as ID,Name FROM NameOfAccount WHERE HeadId = " + child.Tag);
                    PopulateTreeView(dtChild, Convert.ToInt32(child.Tag), child);
                }
                else
                {
                    treeNode.Nodes.Add(child);
                }
            }
        }
        private DataTable GetData(string query)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                return dt;
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                AccountNo = treeView1.SelectedNode.Tag.ToString();
                string gridval = treeView1.SelectedNode.Text.ToString();
               string val= gridval.Split('-').Skip(1).FirstOrDefault();
                AccnameTxt.Text = val;
            }
            catch (Exception)
            {
                MessageBox.Show("Please Select Branch");
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void dbtxt_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
