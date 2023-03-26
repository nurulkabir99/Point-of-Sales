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
using Microsoft.VisualBasic;

namespace PointInvt
{
    public partial class TrialBalance : Form
    {
        public string conString;
        public TrialBalance()
        {
            InitializeComponent();

            StreamReader reader = new StreamReader("C:\\Safi\\ConString.txt");
          conString = reader.ReadLine();
            reader.Close();
            //string sessionId=Login.LoginInfo.UserID;
            DateTime now = DateTime.Now;
            fromDate.Value = new DateTime(now.Year, now.Month, 1).Date;
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
           
            con.Close();
            
        }

        public static string frDate;
        public static string tDate;
        public static string ItemCo;
       public  DataTable dt =new DataTable();
        public static string branchNo;
        public DataTable dts = new DataTable();
       

        private void button1_Click(object sender, EventArgs e)
        {
           
            //frDate = fromDate.Value.Date.ToString();
            //tDate = toDate.Value.Date.ToString();
            ////ItemCo = InvoiceCombo.Text.Split('-').Skip(0).FirstOrDefault();
            ////ItemCo =ItemListCombo.Text.TrimEnd(new[] { '-' });
            //branchNo = (branchCombo.SelectedIndex + 1).ToString();
            
           
            ////InvoiceCombo.SelectedIndex = 0;
            ////branchCombo.SelectedIndex = 0;
            ////InvoiceCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ////InvoiceCombo.AutoCompleteSource = AutoCompleteSource.ListItems;
            //SqlConnection con = new SqlConnection(conString);
            //con.Open();
            //try
            //{
            //    //initialize a new instance of sqlcommand
            //    SqlCommand cmd = new SqlCommand();
            //    //set a connection used by this instance of sqlcommand
            //    cmd.Connection = con;
            //    //set the sql statement to execute at the data source
            //    if (InvoiceCombo.Text=="ALL")
            //    {
            //        cmd.CommandText =
            //        "select VoucharNo,ItemName, sum(-1*purchaseCost) as CostPrice,TotalPrice as SalePrice,sum(TotalPrice + purchaseCost) as Profit from Inventory where branch='"+branchNo+"'and inout=1 and date between '"+frDate+"' and '"+tDate+"' group by VoucharNo, ItemName,purchaseCost,TotalPrice";
               
            //    }
            //    else
            //    {
            //        cmd.CommandText =
            //        "select VoucharNo,ItemName, sum(-1*purchaseCost) as CostPrice,TotalPrice as SalePrice,sum(TotalPrice + purchaseCost) as Profit from Inventory where voucharNo='"+(InvoiceCombo.SelectedItem)+"' and branch='" + branchNo + "'and inout=1 and date between '" + frDate + "' and '" + tDate + "' group by VoucharNo, ItemName,purchaseCost,TotalPrice";
                
            //    }
            //    //initialize a new instance of sqlDataAdapter
            //    SqlDataAdapter da = new SqlDataAdapter();
            //    //set the sql statement or stored procedure to execute at the data source
            //    da.SelectCommand = cmd;
            //    //initialize a new instance of DataTable
            //    //DataTable dt = new DataTable();
            //    //add or resfresh rows in the certain range in the datatable to match those in the data source.
            //    dt.Clear();
            //    da.Fill(dt);

            //    //inventoryListGrid.Rows.Clear();
            //    //set the data source to display the data in the datagridview

              
            //    ProfitListGrid.Columns[0].DataPropertyName = "VoucharNo";
            //    ProfitListGrid.Columns[1].DataPropertyName = "ItemName";
            //    ProfitListGrid.Columns[2].DataPropertyName = "CostPrice";
            //    ProfitListGrid.Columns[3].DataPropertyName = "SalePrice";
            //    ProfitListGrid.Columns[4].DataPropertyName = "Profit";
            //   //inventoryListGrid.Columns[5].DataPropertyName = "SerialNo";
            
            //    ProfitListGrid.DataSource = dt;
            //    //inventoryListGrid.Refresh();
            //    //inventoryListGrid.Parent.Refresh();
               

            //    double sum = 0;
            //    double sum1 = 0;

            //    for (int i = 0; i < ProfitListGrid.Rows.Count; ++i)
            //    {
            //        //sum += Convert.ToDouble(ProfitListGrid.Rows[i].Cells[2].Value);
            //        sum1 += Convert.ToDouble(ProfitListGrid.Rows[i].Cells[4].Value);
            //    }
            //    //TotalItemTxt.Text = sum.ToString();
            //    totalAmountTxt.Text = sum1.ToString();


            //    //chart1.DataSource = dt;
            //    //set the member of the chart data source used to data bind to the X-values of the series  
            //    //chart1.Series["Product List"].XValueMember = "ItemN";
            //    //set the member columns of the chart data source used to data bind to the X-values of the series  
            //    //chart1.Series["Product List"].YValueMembers = "Quantity";
            //    //chart1.Titles.Clear();
            //    //chart1.Titles.Add("Product List"); 
            //}
            //catch (Exception ex)
            //{
            //    //catching error 
            //    MessageBox.Show(ex.Message);
            //}
            ////release all resources used by the component
            ////da.Dispose();
            ////clossing connection
            //con.Close();


        }
       
       
        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            branchNo = (branchCombo.SelectedIndex + 1).ToString();
            TrialBalanceViewer sendData1 = new TrialBalanceViewer(dts,branchNo);
            sendData1.ShowDialog();

        }

        private void branchCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
           // InvoiceCombo.Text = "";
           // DataTable linkcat = new DataTable();
           //InvoiceCombo.Items.Clear();
           // SqlConnection con1 = new SqlConnection(conString);
           // con1.Open();
           // using (SqlDataAdapter da = new SqlDataAdapter("SELECT distinct( VoucharNo) FROM Inventory where branch='" + (branchCombo.SelectedIndex + 1) + "' and inout='1'", con1))
           // {
           //     da.Fill(linkcat);
           // }
           // InvoiceCombo.Items.Add("ALL");
           // foreach (DataRow da in linkcat.Rows)
           // {
           //     InvoiceCombo.Items.Add(da[0].ToString());
           //     //ItemListCombo.Items.Add(da[2].ToString());

           // }
           // con1.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


       

        private void ProfitListGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataTable dtsTable = new DataTable();
            ////string cellValue = ProfitListGrid.Rows[e.RowIndex].Cells[0].Value.ToString();

            //frDate = fromDate.Value.Date.ToString();
            //tDate = toDate.Value.Date.ToString();
            ////ItemCo = InvoiceCombo.Text.Split('-').Skip(0).FirstOrDefault();
            ////ItemCo =ItemListCombo.Text.TrimEnd(new[] { '-' });
            //branchNo = (branchCombo.SelectedIndex + 1).ToString();

            //SqlConnection con = new SqlConnection(conString);
            //con.Open();
            //try
            //{
            //    //initialize a new instance of sqlcommand
            //    SqlCommand cmd = new SqlCommand();
            //    //set a connection used by this instance of sqlcommand
            //    cmd.Connection = con;
            //    //set the sql statement to execute at the data source


            //    cmd.CommandText =
            //        "select VoucharNo,ItemName,Quantity, CostUnit as CostPrice,(quantity*CostUnit) as TotalCost,UnitPrice as SalePrice,TotalPrice,(UnitPrice - Costunit) as Profit ,(TotalPrice + purchaseCost) as TotalProfit from Inventory where voucharNo='" + cellValue + "' and branch='" + branchNo + "'and inout=1 and date between '" + frDate + "' and '" + tDate + "'";
            //       SqlDataAdapter da = new SqlDataAdapter();
                
            //    da.SelectCommand = cmd;

            //    dtsTable.Clear();
            //    da.Fill(dtsTable);

                
            //    //dataGridView1.Columns[0].DataPropertyName = "VoucharNo";
            //    //dataGridView1.Columns[1].DataPropertyName = "ItemName";
            //    //dataGridView1.Columns[2].DataPropertyName = "Quantity";
            //    //dataGridView1.Columns[3].DataPropertyName = "CostPrice";
            //    //dataGridView1.Columns[4].DataPropertyName = "TotalCost";
            //    //dataGridView1.Columns[5].DataPropertyName = "SalePrice";
            //    //dataGridView1.Columns[6].DataPropertyName = "TotalPrice";
            //    //dataGridView1.Columns[7].DataPropertyName = "Profit";
            //    //dataGridView1.Columns[8].DataPropertyName = "TotalProfit";



            //    //dataGridView1.DataSource = dtsTable;
               
            //    //chart1.DataSource = dt;
            //    //set the member of the chart data source used to data bind to the X-values of the series  
            //    //chart1.Series["Product List"].XValueMember = "ItemN";
            //    //set the member columns of the chart data source used to data bind to the X-values of the series  
            //    //chart1.Series["Product List"].YValueMembers = "Quantity";
            //    //chart1.Titles.Clear();
            //    //chart1.Titles.Add("Product List"); 
            //}
            //catch (Exception ex)
            //{
            //    //catching error 
            //    MessageBox.Show(ex.Message);
            //}
            ////release all resources used by the component
            ////da.Dispose();
            ////clossing connection
            //con.Close();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //inventoryListGrid.DataSource = null;
            //inventoryListGrid.Refresh();
            frDate = fromDate.Value.Date.ToString("yyyy-MM-dd");
            tDate = toDate.Value.Date.ToString("yyyy-MM-dd");
            //ItemCo = InvoiceCombo.Text.Split('-').Skip(0).FirstOrDefault();
            //ItemCo =ItemListCombo.Text.TrimEnd(new[] { '-' });
            branchNo = (branchCombo.SelectedIndex + 1).ToString();


            //InvoiceCombo.SelectedIndex = 0;
            //branchCombo.SelectedIndex = 0;
            //InvoiceCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //InvoiceCombo.AutoCompleteSource = AutoCompleteSource.ListItems;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            try
            {
                //initialize a new instance of sqlcommand
                SqlCommand cmd = new SqlCommand();
                //set a connection used by this instance of sqlcommand
                cmd.Connection = con;
                //set the sql statement to execute at the data source
                
                    //cmd.CommandText =
                    //"SELECT AccountId,AccountName,SUM(amount) Debit,SUM(amount) Credit,ParentId,HeadId FROM Accounts where branch='" + branchNo + "' and date between '" + frDate + "' and '" + tDate + "' GROUP BY AccountId,AccountName,ParentId,HeadId order by AccountId";
                cmd.CommandText = "select AccountId,AccountName, CAST(sum(amount) AS NUMERIC(38, 3)) AS Debit,CAST(SUM(amount) AS NUMERIC(38, 3)) AS Credit,ParentId,HeadId from ((SELECT AccountId,AccountName,SUM(amount) amount,ParentId,HeadId FROM Accounts where branch='" + branchNo + "' and date <'" + frDate + "' GROUP BY AccountId,AccountName,ParentId,HeadId) union all (sELECT AccountId,AccountName,SUM(amount) amount,ParentId,HeadId FROM Accounts where branch='" + branchNo + "' and date between '" + frDate + "' and '" + tDate + "' GROUP BY AccountId,AccountName,ParentId,HeadId)) t group by AccountId,AccountName,ParentId,HeadId order by HeadId";
               
                //initialize a new instance of sqlDataAdapter
                SqlDataAdapter da = new SqlDataAdapter();
                //set the sql statement or stored procedure to execute at the data source
                da.SelectCommand = cmd;
                //initialize a new instance of DataTable
                //DataTable dt = new DataTable();
                //add or resfresh rows in the certain range in the datatable to match those in the data source.
                dts.Clear();
                da.Fill(dts);

                foreach (DataRow row in dts.Rows)
                {
                    if ((row["ParentId"].ToString() == "1") || (row["ParentId"].ToString() == "2"))
                    {
                        row.SetField("Credit", "0");
                    }
                    else
                    {
                        row.SetField("Debit", "0");
                    }
                }

                //inventoryListGrid.Rows.Clear();
                //set the data source to display the data in the datagridview


                dataGridView2.Columns[0].DataPropertyName = "AccountId";
                dataGridView2.Columns[1].DataPropertyName = "AccountName";
               dataGridView2.Columns[2].DataPropertyName = "Debit";
               dataGridView2.Columns[3].DataPropertyName = "Credit";
               dataGridView2.Columns[2].DefaultCellStyle.Format = "0.000#";
               dataGridView2.Columns[3].DefaultCellStyle.Format = "0.000#";
                dataGridView2.Columns[4].DataPropertyName = "ParentId";
                dataGridView2.Columns[5].DataPropertyName = "HeadId";

                DataView dvView = new DataView(dts);
                if (checkBox1.Checked) { dvView.RowFilter = ""; }
                else
                {
                    dvView.RowFilter = "Debit + Credit <>0";
                }
                
                DataTable ndt = dvView.ToTable();
                //DataView dvViews = new DataView(ndt);
                //dvView.RowFilter = "Price IN (1.0, 9.9, 11.5)";

                dataGridView2.DataSource = ndt;

                dts = ndt;

                double sum = 0;
                double sum1 = 0;

                for (int i = 0; i < dataGridView2.Rows.Count; ++i)
                {
                    sum += Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value);
                    sum1 += Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);
                }
                byDrTxt.Text = sum.ToString();
                byCRTxt.Text = sum1.ToString();


                //chart1.DataSource = dts;
                ////set the member of the chart data source used to data bind to the X-values of the series  
                //chart1.Series["Profit Per Day"].XValueMember = "Date";
                
                ////set the member columns of the chart data source used to data bind to the X-values of the series  
                //chart1.Series["Profit Per Day"].YValueMembers = "Profit";
                //chart1.Titles.Clear();
                //chart1.Titles.Add("Profit Per Day"); 
            }
            catch (Exception ex)
            {
                //catching error 
                MessageBox.Show(ex.Message);
            }
            //release all resources used by the component
            //da.Dispose();
            //clossing connection
            con.Close();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        //    DataTable dtsTable = new DataTable();
        //    string cellValue = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();

        //    //frDate = fromDate.Value.Date.ToString();
        //    //tDate = toDate.Value.Date.ToString();
        //    //ItemCo = InvoiceCombo.Text.Split('-').Skip(0).FirstOrDefault();
        //    //ItemCo =ItemListCombo.Text.TrimEnd(new[] { '-' });
        //    branchNo = (branchCombo.SelectedIndex + 1).ToString();

        //    SqlConnection con = new SqlConnection(conString);
        //    con.Open();
        //    try
        //    {
        //        //initialize a new instance of sqlcommand
        //        SqlCommand cmd = new SqlCommand();
        //        //set a connection used by this instance of sqlcommand
        //        cmd.Connection = con;
        //        //set the sql statement to execute at the data source


        //        cmd.CommandText =
        //            "select VoucharNo,ItemName,Quantity, CostUnit as CostPrice,(quantity*CostUnit) as TotalCost,UnitPrice as SalePrice,TotalPrice,(UnitPrice - Costunit) as Profit ,(TotalPrice + purchaseCost) as TotalProfit from Inventory where date='" + cellValue + "' and branch='" + branchNo + "'and inout=1 ";
        //        SqlDataAdapter da = new SqlDataAdapter();

        //        da.SelectCommand = cmd;

        //        dtsTable.Clear();
        //        da.Fill(dtsTable);


            //}
            //catch (Exception ex)
            //{
            //    //catching error 
            //    MessageBox.Show(ex.Message);
            //}
            ////release all resources used by the component
            ////da.Dispose();
            ////clossing connection
            //con.Close();

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           

            try
               {
                   string cellValue = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                   acoutnNametxt.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                   frDate = fromDate.Value.Date.ToString("yyyy-MM-dd");
                   tDate = toDate.Value.Date.ToString("yyyy-MM-dd");
                   //ItemCo = ItemListCombo.Text.Split('-').Skip(0).FirstOrDefault();
                   //ItemCo =ItemListCombo.Text.TrimEnd(new[] { '-' });
                   branchNo = (branchCombo.SelectedIndex + 1).ToString();
                   string balnow = "0";
                   string balPre = "0";
                   string balnet = "0";
                   SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlCommand invIdSqlCommand = new SqlCommand("select sum(Amount) as Amount from Accounts where branch='" + (branchCombo.SelectedIndex + 1) + "' and date < '" + frDate + "' and AccountId='" + cellValue + "'", con);
                invIdSqlCommand.ExecuteNonQuery();
                SqlDataReader read = invIdSqlCommand.ExecuteReader();

                while (read.Read())
                {
                    balPre = (read["Amount"].ToString());
                    totalAmountTxt.Text = balPre;

                }
                if (balPre == "")
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
                "select date,VoucharNo, Description, Debit ,Credit,Amount from Accounts where branch='" + (branchCombo.SelectedIndex + 1) + "' and date between '" + frDate + "'and '" + tDate + "' and AccountId='" + cellValue + "'";

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
                inventoryListGrid.Columns[3].DefaultCellStyle.Format = "0.000#";
                inventoryListGrid.Columns[4].DefaultCellStyle.Format = "0.000#";
                inventoryListGrid.Columns[5].DefaultCellStyle.Format = "0.000#";

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
                dbtxt.Text = sum.ToString("#,0.000");
                crtxt.Text = sum1.ToString("#,0.000");
                balnow = sum2.ToString("#,0.000");
                TotalITxt.Text = balnow;
                netBalanceTxt.Text = (Convert.ToDouble(balPre) + Convert.ToDouble(balnow)).ToString("#,0.000");
                con.Close();

               
            }
            catch (Exception ex)
            {
                //catching error 
                MessageBox.Show(ex.Message);
            }
           
           
        }



    }
}
