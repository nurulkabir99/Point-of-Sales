using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace PointInvt
{
    public partial class EditcashSales : Form
    {
        public EditcashSales(string search)
        {
            InitializeComponent();
            conString = Login.LoginInfo.conString;
            salesmanIDtxt.Text = Login.LoginInfo.SalesmanId;
            comboBox2.SelectedIndex = Convert.ToInt16(Login.LoginInfo.branchID)-1;
            SqlConnection con = new SqlConnection(conString);
            //con.Open();
            //SqlCommand cmd =
            //    new SqlCommand("select MAX(InvoiceId)+1 as ID from voucherNo where Branch='1' and Type='SI'", con);
            //cmd.ExecuteNonQuery();
            //SqlDataReader read = cmd.ExecuteReader();

            //while (read.Read())
            //{
            //    invoiceNoText.Text = (read["ID"].ToString());
            //    BranchInfo.MaxinvoiceNo = (read["ID"].ToString());


            //}
            //con.Close();
            //BranchInfo.SalesmanId = Login.LoginInfo.SalesmanId;
            //comboBox2.SelectedIndex = Convert.ToInt16(Login.LoginInfo.branchID) - 1;
            //comboBox2.Text = comboBox2.SelectedIndex.ToString();
            //salesmanIDtxt.Text = BranchInfo.SalesmanId;
            //SaveBtn.Enabled = false;
           
            string searchInvc = search;

            //SqlConnection con = new SqlConnection(conString);
            try
            {

                SqlConnection conn = new SqlConnection(conString);
                conn.Open();

                SqlCommand invIdSqlCommand =
                    new SqlCommand(
                        "select * from HeadInventory where VoucharNo ='" + searchInvc + "'and Vouchartype='SI'", conn);
                invIdSqlCommand.ExecuteNonQuery();
                SqlDataReader read = invIdSqlCommand.ExecuteReader();

                while (read.Read())
                {
                    BranchInfo.dateTime = Convert.ToDateTime(read["VoucharDate"].ToString());
                    SuplyerIdTxt.Text = (read["CustSuplyId"].ToString());
                    salesmanIDtxt.Text = (read["SalesmanId"].ToString());
                    grossTotal.Text = (read["GrossTotal"].ToString());
                    discountText.Text = (read["Discount"].ToString());
                    netGrossTotal.Text = (read["TotalSale"].ToString());
                    int ind = Convert.ToInt16(read["Branch"].ToString());
                    comboBox2.SelectedIndex = ind - 1;
                    cashPay.Text = (read["Cash"].ToString());
                    knetPay.Text = (read["Knet"].ToString());
                    knetCode.Text = (read["KnetCode"].ToString());
                    dueText.Text = (read["Due"].ToString());

                }
                invoiceNoText.Text = searchInvc;
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Please Select Branch");
            }

            con.Open();
            try
            {
                //initialize a new instance of sqlcommand
                SqlCommand cmd = new SqlCommand();
                //set a connection used by this instance of sqlcommand
                cmd.Connection = con;
                //set the sql statement to execute at the data source
                cmd.CommandText =
                    "select SerialNo as BarCode, ItemCode, ItemName, Quantity as QTY,UnitPrice as Price,TotalPrice as Total,SerialNo, CostUnit,Id from Inventory where VoucharNo='" +
                    searchInvc + "'";
                //initialize a new instance of sqlDataAdapter
                SqlDataAdapter da = new SqlDataAdapter();
                //set the sql statement or stored procedure to execute at the data source
                da.SelectCommand = cmd;
                //initialize a new instance of DataTable
                DataTable dt = new DataTable();
                //add or resfresh rows in the certain range in the datatable to match those in the data source.
                da.Fill(dt);
                //set the data source to display the data in the datagridview

                ProductList.Columns[1].DataPropertyName = "BarCode";
                ProductList.Columns[2].DataPropertyName = "ItemCode";
                ProductList.Columns[3].DataPropertyName = "ItemName";
                ProductList.Columns[4].DataPropertyName = "QTY";
                ProductList.Columns[5].DataPropertyName = "Price";
                ProductList.Columns[6].DataPropertyName = "Total";
                ProductList.Columns[7].DataPropertyName = "SerialNo";
                ProductList.Columns[8].DataPropertyName = "CostUnit";
                ProductList.Columns[9].DataPropertyName = "Id";
                ProductList.DataSource = dt;
                //ProductList.Refresh();
                //ProductList.Parent.Refresh();



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

            //datePucharse.Value = BranchInfo.dateTime;

        }

        public static class BranchInfo
        {
            public static string BranchID;
            public static string MaxinvoiceNo;
            public static string SalesmanId;
            public static int TrueFalse = 0;
            public static Int32 MaxInvcID;
            public static Int32 MinInvcID;
           public static DateTime dateTime;

        }

        private string conString;

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.ProductList.Rows[e.RowIndex].Cells["SlNo"].Value = (e.RowIndex + 1).ToString();

        }
        public Int16 qtys=0;
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               // ProductList.Columns["BarCode"].DefaultCellStyle.BackColor = Color.LightGreen;
                int cellID = ProductList.CurrentCell.RowIndex;

                string SearchValue = (ProductList["BarCode", cellID].Value).ToString();

                float costPrice;

              
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ItemCode,itemName,SerialNo,CostUnit from Inventory WHERE SerialNo = '" + SearchValue + "' and Branch='" + (comboBox2.SelectedIndex + 1) + "' and inout=0", con);
               
                cmd.ExecuteNonQuery();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    this.ProductList.Rows[e.RowIndex].Cells["ItemCode"].Value = read["Itemcode"].ToString();
                    this.ProductList.Rows[e.RowIndex].Cells["ItemName"].Value = read["itemName"].ToString();
                    this.ProductList.Rows[e.RowIndex].Cells["SerialNo"].Value = read["SerialNo"].ToString();
                    this.ProductList.Rows[e.RowIndex].Cells[8].Value = read["CostUnit"].ToString();
                   
                    //this.costText.Text = read["CostUnit"].ToString();

                }
                con.Close();
                //con.Open();
                //SqlCommand cmdIstock =
                //    new SqlCommand(
                //        "select Itemcode,ItemName, SUM(qty)as Quantity, SUM(PurchaseCost) as Cost ,SerialNo from Inventory where SerialNo='" +
                //        SearchValue + "' and Branch='" + (comboBox2.SelectedIndex + 1) +
                //        "' group by ItemCode , SerialNo, ItemName ", con);
                //cmdIstock.ExecuteNonQuery();
                //read = cmdIstock.ExecuteReader();
                
                //while (read.Read())
                //{
                   
                //    qtys = Convert.ToInt16(read["Quantity"].ToString());
                  
                //}
               
                //con.Close();
                //if (this.ProductList.Rows[e.RowIndex].Cells["Quantity"].Value == System.DBNull.Value)
                //{
                //    ProductList.Rows[e.RowIndex].Cells["Quantity"].Value = 0;
                //    ProductList.Rows[e.RowIndex].Cells["Price"].Value = 0;
                //}
                //if ((Convert.ToInt16(this.ProductList.Rows[e.RowIndex].Cells["Quantity"].Value) > qtys) || (Convert.ToInt16(this.ProductList.Rows[e.RowIndex].Cells["Quantity"].Value) <= 0) )
                //{
                //    ProductList.Rows[e.RowIndex].Cells["Quantity"].Value = 0;
                //    ProductList.Rows[e.RowIndex].Cells["Price"].Value = 0;
                  

                //}


                foreach (DataGridViewRow row in ProductList.Rows)
                {
                    row.Cells[ProductList.Columns["Total"].Index].Value =((Convert.ToDouble(row.Cells[ProductList.Columns["Quantity"].Index].Value)) *(Convert.ToDouble(row.Cells[ProductList.Columns["Price"].Index].Value)));
                }

                double sum = 0;

                for (int i = 0; i < ProductList.Rows.Count; ++i)
                {
                    sum += Convert.ToDouble(ProductList.Rows[i].Cells[6].Value);
                }
                grossTotal.Text = sum.ToString();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Select Product");
            }


           

        }

        //private void button5_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //    cashSales csSales = new cashSales();
        //    csSales.Show();
        //}

        private void grossTotal_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(grossTotal.Text) && !string.IsNullOrEmpty(discountText.Text))
                netGrossTotal.Text =
                    (Convert.ToDouble(grossTotal.Text) - Convert.ToDouble(discountText.Text)).ToString();
            else
            {
                netGrossTotal.Text = grossTotal.Text;
            }
        }

        private void discountText_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(grossTotal.Text) && !string.IsNullOrEmpty(discountText.Text))
                netGrossTotal.Text =
                    (Convert.ToDouble(grossTotal.Text) - Convert.ToDouble(discountText.Text)).ToString();
            else
            {
                netGrossTotal.Text = grossTotal.Text;
            }
        }
        public static double cash;
        public static double knet;
        public static double Due;

        private void cashPay_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (cashPay.Text == "")
                {
                    cash = 0;
                }
                else
                {
                    cash = Convert.ToDouble(cashPay.Text);
                }
                if (knetPay.Text == "")
                {
                    knet = 0;
                }
                else
                {
                    knet = Convert.ToDouble(knetPay.Text);
                }
                if (dueText.Text == "")
                {
                    Due = 0;
                }
                else
                {
                    Due = Convert.ToDouble(dueText.Text);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please Enter Amount");
            }
            try
            {
                if (!string.IsNullOrEmpty(cashPay.Text) && !string.IsNullOrEmpty(netGrossTotal.Text))
                {

                    knetPay.Text = (Convert.ToDouble(netGrossTotal.Text) - cash).ToString();

                }


                else
                {
                    knetPay.Text = netGrossTotal.Text;
                }
                if (Convert.ToDouble(netGrossTotal.Text) == cash + knet + Due)
                {
                    SaveBtn.Enabled = true;
                }
                else
                {
                    SaveBtn.Enabled = false;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Please Input Valid Data");
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            float costPrice;
            float Limit=3;
            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from Inventory WHERE SerialNo = '" + itemCodeText.Text + "'and InOut='0'", con);
                SqlCommand cmdIstock =
                    new SqlCommand(
                        "select Itemcode,ItemName, SUM(qty)as Quantity, SUM(PurchaseCost) as Cost ,SerialNo from Inventory where SerialNo='" +
                        itemCodeText.Text + "' and Branch='" + (comboBox2.SelectedIndex + 1) +
                        "' group by ItemCode , SerialNo, ItemName ", con);
                //SqlCommand cmdIstock =
                //    new SqlCommand(
                //        "select Itemcode,ItemName, SUM(qty)as Quantity, sum(RetailPrice) as cost,SerialNo from Inventory where SerialNo='" +
                //        itemCodeText.Text + "' and Branch='" + (comboBox2.SelectedIndex + 1) +
                //        "' group by ItemCode , SerialNo, ItemName", con);
                cmdIstock.ExecuteNonQuery();
                SqlDataReader read = cmdIstock.ExecuteReader();

                while (read.Read())
                {
                    itemNameText.Text = (read["itemName"].ToString());
                    //costText.Text = (read["cost"].ToString());
                    inStockText.Text = (read["Quantity"].ToString());
                    costPrice = float.Parse(read["cost"].ToString()) / float.Parse(read["quantity"].ToString());
                    costText.Text = costPrice.ToString();
                }
                con.Close();
                if (float.Parse(inStockText.Text) < Limit)
                {
                    limitText.BackColor = Color.Red;
                    limitText.Text = Limit.ToString();
                }
                else
                {
                    limitText.Text = Limit.ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rowIndex = ProductList.CurrentCell.RowIndex;
            string cellValue = ProductList.Rows[rowIndex].Cells[9].Value.ToString();
            var confirmResult = MessageBox.Show("Are you sure to Delete ??", "Confirm OK!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    SqlConnection con = new SqlConnection(conString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "Delete from Inventory where VoucharNo='" + invoiceNoText.Text + "' and Id='"+cellValue+"'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                catch (Exception)
                {

                }
                ProductList.Rows.RemoveAt(rowIndex);
            }
            try
            {
                
              

                double sum = 0;

                for (int i = 0; i < ProductList.Rows.Count; ++i)
                {
                    sum += Convert.ToDouble(ProductList.Rows[i].Cells[6].Value);
                }
                grossTotal.Text = sum.ToString();

                 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nothing to Delete / لا شيء للحذف");
            }

        }

        public void refreash()
        {
            this.Controls.Clear();
            this.InitializeComponent();
            comboBox2.Text = BranchInfo.BranchID;
            invoiceNoText.Text = BranchInfo.MaxinvoiceNo;
            salesmanIDtxt.Text = BranchInfo.SalesmanId;
            SaveBtn.Enabled = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
           
            // ReSharper disable once ArrangeThisQualifier
            this.Controls.Clear();
            // ReSharper disable once ArrangeThisQualifier
            this.InitializeComponent();
            comboBox2.Text = BranchInfo.BranchID;
            invoiceNoText.Text = BranchInfo.MaxinvoiceNo;
            salesmanIDtxt.Text = BranchInfo.SalesmanId;
            //this.Refresh();
            //this.Close();
            //cashPurchase ss = new cashPurchase();
            //ss.Show();
            SaveBtn.Enabled = false;
        }

        
        public static string max;
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            
            //var confirmResult = MessageBox.Show("Are you sure to Submit ??", "Confirm OK!!", MessageBoxButtons.YesNo);
            //if (confirmResult == DialogResult.Yes)
            //{
            //    SqlConnection cons = new SqlConnection(conString);
            //    cons.Open();
            //    SqlCommand cmd =
            //        new SqlCommand("select MAX(InvoiceId)+1 as ID from voucherNo where Branch='" + (comboBox2.SelectedIndex + 1) + "' and Type='SI'", cons);
            //    cmd.ExecuteNonQuery();
            //    SqlDataReader read = cmd.ExecuteReader();

            //    while (read.Read())
            //    {
            //        max = (read["ID"].ToString());
            //        BranchInfo.MaxinvoiceNo = (read["ID"].ToString());


            //    }
            //    cons.Close(); try
            //    {
            //        SqlConnection con1 = new SqlConnection(conString);
            //        SqlCommand com1 =
            //            new SqlCommand(
            //                "insert into HeadInventory (VoucharNo,VoucharType,VoucharDate,CustSuplyId,GrossTotal,Discount,TotalSale, VoucharTime, SalesmanId,Branch,Cash,Knet,KnetCode,Due) values ('" +
            //                max + "','SI','" + datePucharse.Value.Date + "','" + SuplyerIdTxt.Text +
            //                "','" + grossTotal.Text + "','" + discountText.Text + "','" + netGrossTotal.Text + "','" +
            //                datePucharse.Value.TimeOfDay + "','" +
            //                salesmanIDtxt.Text + "','" + (comboBox2.SelectedIndex + 1) + "','" + cashPay.Text + "','" +
            //                knetPay.Text + "','" + knetCode.Text + "','" + dueText.Text + "')", con1);
            //        con1.Open();
            //        com1.ExecuteNonQuery();
            //        con1.Close();
            //        for (int r = 0; r < ProductList.RowCount - 1; r++)
            //        {
            //            DataGridViewRow g1 = ProductList.Rows[r];

            //            SqlConnection con = new SqlConnection(conString);
            //            SqlCommand com =
            //                new SqlCommand(
            //                    "insert into Inventory (VoucharNo,ItemCode,ItemName,Quantity,CostUnit, UnitPrice, TotalPrice, SerialNo,Branch,InOut,date) values ('" +
            //                    max + "','" + g1.Cells[2].Value + "','" + g1.Cells[3].Value + "','"+
            //                    g1.Cells[4].Value + "','" + g1.Cells[8].Value + "','" + g1.Cells[5].Value + "','" +
            //                    g1.Cells[6].Value + "','" + g1.Cells[7].Value + "','" + (comboBox2.SelectedIndex + 1) +
            //                    "','1','" + datePucharse.Value.Date + "')", con);
            //            con.Open();
            //            com.ExecuteNonQuery();
            //            con.Close();
            //        }
            //        SqlConnection con2 = new SqlConnection(conString);
            //        SqlCommand com2 =
            //            new SqlCommand(
            //                "insert into voucherNo (InvoiceId, Branch, Type) values ('" + max + "','" +
            //                (comboBox2.SelectedIndex + 1) + "','SI')", con2);
            //        con2.Open();
            //        com2.ExecuteNonQuery();
            //        con2.Close();
            //        //this.Refresh();
            //        //this.Close();
            //        //cashPurchase ss = new cashPurchase();
            //        //ss.Show();
            //        if (checkBox1.Checked)
            //        {
            //            //foreach (DataGridViewRow g  in ProductList.Rows)
            //            //{
            //            //    dt.Rows.Add(g.Cells[0].Value, g.Cells[1].Value, g.Cells[2].Value, g.Cells[3].Value, g.Cells[4].Value, g.Cells[5].Value, g.Cells[6].Value, g.Cells[7].Value);

            //            //}
            //            //BranchInfo.BranchID = (comboBox2.SelectedIndex + 1).ToString();
            //            string InvoiceNo = max;
            //            PurchasesViewer sendData1 = new PurchasesViewer(InvoiceNo);
            //            sendData1.ShowDialog();
            //        }

            //        this.Controls.Clear();
            //        this.InitializeComponent();
            //        comboBox2.Text = BranchInfo.BranchID;
            //        invoiceNoText.Text = BranchInfo.MaxinvoiceNo;
            //        salesmanIDtxt.Text = BranchInfo.SalesmanId;
            //    }

            //    catch (Exception)
            //    {
            //        MessageBox.Show("Please Select Branch");
            //    }

            //}



            //foreach (DataGridViewRow g1 in (ProductList.Rows))
            //{
            //    SqlConnection con = new SqlConnection(conString);
            //    SqlCommand com = new SqlCommand("insert into Inventory (VoucharNo,ItemCode,ItemName,Quantity,UnitPrice, TotalPrice, SerialNo) values ('" + invoiceNoText.Text + "','" + g1.Cells[2].Value + "','" + g1.Cells[3].Value + "','" + g1.Cells[4].Value + "','" + g1.Cells[5].Value + "','" + g1.Cells[7].Value + "','" + g1.Cells[8].Value + "')", con);  
            //    con.Open();  
            //    com.ExecuteNonQuery();  
            //    con.Close();  
            //}
            var confirmResult = MessageBox.Show("Are you sure to Update ??", "Confirm OK!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    SqlConnection con1 = new SqlConnection(conString);
                    SqlCommand com1 =
                        new SqlCommand(
                            "Update HeadInventory set VoucharDate='" + datePucharse.Value.Date.ToString("yyyy-MM-dd") + "', GrossTotal='" + grossTotal.Text + "',Discount='" + discountText.Text + "',SalesmanId='" + salesmanIDtxt.Text + "'," +
                            "TotalSale='" + netGrossTotal.Text + "',Cash='" + cashPay.Text + "',Knet='" + knetPay.Text + "'," +
                            "KnetCode='" + knetCode.Text + "',Due='" + dueText.Text + "' where VoucharNo='" + invoiceNoText.Text + "'",
                            con1);
                    con1.Open();
                    com1.ExecuteNonQuery();
                    con1.Close();
                    for (int r = 0; r < ProductList.RowCount - 1; r++)
                    {
                        DataGridViewRow g1 = ProductList.Rows[r];

                        SqlConnection con = new SqlConnection(conString);
                        SqlCommand com =
                            new SqlCommand(
                                "update Inventory set ItemCode='" + g1.Cells[2].Value + "',ItemName='" +
                                g1.Cells[3].Value + "'," +
                                "Quantity='" + g1.Cells[4].Value + "',CostUnit='" + g1.Cells[8].Value + "', UnitPrice='" +
                                g1.Cells[5].Value + "', TotalPrice='" +
                                g1.Cells[6].Value + "', SerialNo='" + g1.Cells[7].Value + "', Date='" + datePucharse.Value.Date.ToString("yyyy-MM-dd") + "' where VoucharNo='" + invoiceNoText.Text + "'and Id='" + g1.Cells[9].Value + "'", con);
                        con.Open();
                        com.ExecuteNonQuery();
                        con.Close();
                    }
                    //  g1.Cells[6].Value + "', SerialNo='" + g1.Cells[7].Value + "', Date='" + datePucharse.Value.Date.ToString("yyyy-MM-dd") + "' where VoucharNo='" + invoiceNoText.Text + "'and serialNo='" + g1.Cells[7].Value + "'and Id='" + g1.Cells[9].Value + "'", con);

                    //this.Controls.Clear();
                    //this.InitializeComponent();
                    //comboBox2.Text = BranchInfo.BranchID;
                    //invoiceNoText.Text = BranchInfo.MaxinvoiceNo;
                    //salesmanIDtxt.Text = BranchInfo.SalesmanId;
                    // ReSharper disable once ArrangeThisQualifier
                    //this.Close();
                    //cashSales csSales = new cashSales();
                    //csSales.Show();
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("Please Select Branch");
                }

                    
                    
            }
        }

        private void suplyerBtn_Click(object sender, EventArgs e)
        {
            float supDue;
            float pay;
            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();

                SqlCommand cmdsuplyer =
                    new SqlCommand(
                        "select customerId,CustomerName, SUM(buyAmount)as Buy, SUM(PayAmount) as Pay from customerinfo where customerId='" +
                        SuplyerIdTxt.Text + "' group by customerId, CustomerName", con);
                cmdsuplyer.ExecuteNonQuery();
                SqlDataReader read = cmdsuplyer.ExecuteReader();

                while (read.Read())
                {
                    //if (read["Pay"].ToString() == "")
                    //{
                    //    pay = 0;}
                    //else
                    //{
                    //    pay = float.Parse(read["Pay"].ToString());
                    //}

                    textBox1.Text = (read["CustomerName"].ToString());   //suplyerNameTxt
                    PrePurchaseTxt.Text = (read["Buy"].ToString());
                    //supDue = float.Parse(read["Buy"].ToString()) - pay;
                    //SuplyerDueTxt.Text = supDue.ToString();

                }
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void knetPay_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (cashPay.Text == "")
                {
                    cash = 0;
                }
                else
                {
                    cash = Convert.ToDouble(cashPay.Text);
                }
                if (knetPay.Text == "")
                {
                    knet = 0;
                }
                else
                {
                    knet = Convert.ToDouble(knetPay.Text);
                }
                if (dueText.Text == "")
                {
                    Due = 0;
                }
                else
                {
                    Due = Convert.ToDouble(dueText.Text);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please Enter Amount");
            }
            try
            {

                if (!string.IsNullOrEmpty(cashPay.Text) && !string.IsNullOrEmpty(knetPay.Text))
                {


                    dueText.Text =
                        (Convert.ToDouble(netGrossTotal.Text) -
                         (cash + knet)).ToString();



                }
                else
                {
                    if (string.IsNullOrEmpty(cashPay.Text))
                    {
                        string cashpay = "0";
                        dueText.Text =
                            (Convert.ToDouble(netGrossTotal.Text) -
                             (cash + knet)).ToString();
                    }
                    else
                    {
                        dueText.Text =
                            (Convert.ToDouble(netGrossTotal.Text) - cash).ToString();
                    }

                }
                if (Convert.ToDouble(netGrossTotal.Text) == cash + knet + Due)
                {
                    SaveBtn.Enabled = true;
                }
                else
                {
                    SaveBtn.Enabled = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please Enter Amount");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //refreash();
            try
            {
                BranchInfo.BranchID = comboBox2.Text;

                SqlConnection con = new SqlConnection(conString);
                con.Open();

                SqlCommand invIdSqlCommand =
                    new SqlCommand(
                        "select MAX(InvoiceId)+1 as ID from voucherNo where Branch='" + (comboBox2.SelectedIndex + 1) +
                        "'and type='SI'", con);
                invIdSqlCommand.ExecuteNonQuery();
                SqlDataReader read = invIdSqlCommand.ExecuteReader();

                while (read.Read())
                {
                    invoiceNoText.Text = (read["ID"].ToString());
                    BranchInfo.MaxinvoiceNo = invoiceNoText.Text;
                }
                con.Close();


            }
            catch (Exception)
            {
                MessageBox.Show("Please Select Branch");
            }
        }

        private void FwrdText_Click(object sender, EventArgs e)
        {
            //int invcID;
            invoiceNoText.Text = (Convert.ToInt32(invoiceNoText.Text) - 1).ToString();
            BranchInfo.MaxInvcID = Convert.ToInt32(invoiceNoText.Text);
            BranchInfo.MinInvcID = Convert.ToInt32(invoiceNoText.Text);
            if ((BranchInfo.MaxInvcID > 19999999 || BranchInfo.MinInvcID < 10000000) && comboBox2.Text == "Safi 1")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 29999999 || BranchInfo.MinInvcID < 20000000) && comboBox2.Text == "Safi 2")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 39999999 || BranchInfo.MinInvcID < 30000000) && comboBox2.Text == "Safi 3")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 49999999 || BranchInfo.MinInvcID < 40000000) && comboBox2.Text == "Safi 4")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 59999999 || BranchInfo.MinInvcID < 50000000) && comboBox2.Text == "Safi 5")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 69999999 || BranchInfo.MinInvcID < 60000000) && comboBox2.Text == "Safi 6")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 79999999 || BranchInfo.MinInvcID < 70000000) && comboBox2.Text == "Safi 7")
            {
                refreash();
            }
            else
            {

                SqlConnection con = new SqlConnection(conString);
                try
                {

                   
                    //invoiceNoText.Text = invcID.ToString();
                    SqlConnection conn = new SqlConnection(conString);
                    conn.Open();

                    SqlCommand invIdSqlCommand =
                        new SqlCommand(
                            "select * from HeadInventory where VoucharNo ='" + invoiceNoText.Text +
                            "'and Vouchartype='SI'", conn);
                    invIdSqlCommand.ExecuteNonQuery();
                    SqlDataReader read = invIdSqlCommand.ExecuteReader();

                    while (read.Read())
                    {
                        datePucharse.Text = (read["VoucharDate"].ToString());
                        SuplyerIdTxt.Text = (read["CustSuplyId"].ToString());
                        salesmanIDtxt.Text = (read["SalesmanId"].ToString());
                        grossTotal.Text = (read["GrossTotal"].ToString());
                        discountText.Text = (read["Discount"].ToString());
                        netGrossTotal.Text = (read["TotalSale"].ToString());
                        int ind = Convert.ToInt16(read["Branch"].ToString());
                        comboBox2.SelectedIndex = ind - 1;
                        cashPay.Text = (read["Cash"].ToString());
                        knetPay.Text = (read["Knet"].ToString());
                        knetCode.Text = (read["KnetCode"].ToString());
                        dueText.Text = (read["Due"].ToString());

                    }
                    conn.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Please Select Branch");
                }

                con.Open();
                try
                {
                    //initialize a new instance of sqlcommand
                    SqlCommand cmd = new SqlCommand();
                    //set a connection used by this instance of sqlcommand
                    cmd.Connection = con;
                    //set the sql statement to execute at the data source
                    cmd.CommandText =
                        "select SerialNo as BarCode,ItemCode, ItemName, Quantity as QTY,UnitPrice as Price,TotalPrice as Total,SerialNo, CostUnit,Id from Inventory where VoucharNo='" +
                        invoiceNoText.Text + "'";
                    //initialize a new instance of sqlDataAdapter
                    SqlDataAdapter da = new SqlDataAdapter();
                    //set the sql statement or stored procedure to execute at the data source
                    da.SelectCommand = cmd;
                    //initialize a new instance of DataTable
                    DataTable dt = new DataTable();
                    //add or resfresh rows in the certain range in the datatable to match those in the data source.
                    da.Fill(dt);
                    //set the data source to display the data in the datagridview

                    ProductList.Columns[1].DataPropertyName = "BarCode";
                    ProductList.Columns[2].DataPropertyName = "ItemCode";
                    ProductList.Columns[3].DataPropertyName = "ItemName";
                    ProductList.Columns[4].DataPropertyName = "QTY";
                    ProductList.Columns[5].DataPropertyName = "Price";
                    ProductList.Columns[6].DataPropertyName = "Total";
                    ProductList.Columns[7].DataPropertyName = "SerialNo";
                    ProductList.Columns[8].DataPropertyName = "CostUnit";
                    ProductList.Columns[9].DataPropertyName = "Id";
                    ProductList.DataSource = dt;
                    //ProductList.Refresh();
                    //ProductList.Parent.Refresh();



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
                SaveBtn.Enabled = false;
            }
        }

        private void BwrdText_Click(object sender, EventArgs e)
        {
            // Int32 invcID;
            invoiceNoText.Text = (Convert.ToInt32(invoiceNoText.Text) + 1).ToString();
            BranchInfo.MaxInvcID = Convert.ToInt32(invoiceNoText.Text);
            BranchInfo.MinInvcID = Convert.ToInt32(invoiceNoText.Text);
            if ((BranchInfo.MaxInvcID > 1999999 || BranchInfo.MinInvcID <= 1000001 ||
                 Convert.ToInt32(BranchInfo.MaxinvoiceNo) <= BranchInfo.MinInvcID) && comboBox2.Text == "Safi 1")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 2999999 || BranchInfo.MinInvcID <= 2000001 ||
                      Convert.ToInt32(BranchInfo.MaxinvoiceNo) <= BranchInfo.MinInvcID) && comboBox2.Text == "Safi 2")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 3999999 || BranchInfo.MinInvcID <= 3000001 ||
                      Convert.ToInt32(BranchInfo.MaxinvoiceNo) <= BranchInfo.MinInvcID) && comboBox2.Text == "Safi 3")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 4999999 || BranchInfo.MinInvcID <= 4000001 ||
                      Convert.ToInt32(BranchInfo.MaxinvoiceNo) <= BranchInfo.MinInvcID) && comboBox2.Text == "Safi 4")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 5999999 || BranchInfo.MinInvcID <= 5000001 ||
                     Convert.ToInt32(BranchInfo.MaxinvoiceNo) <= BranchInfo.MinInvcID) && comboBox2.Text == "Safi 5")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 6999999 || BranchInfo.MinInvcID <= 6000001 ||
                      Convert.ToInt32(BranchInfo.MaxinvoiceNo) <= BranchInfo.MinInvcID) && comboBox2.Text == "Safi 6")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 7999999 || BranchInfo.MinInvcID <= 7000001 ||
                      Convert.ToInt32(BranchInfo.MaxinvoiceNo) <= BranchInfo.MinInvcID) && comboBox2.Text == "Safi 7")
            {
                refreash();
            }
            else
            {
                try
                {
                    // invcID = Convert.ToInt32(invoiceNoText.Text) + 1;
                    //invoiceNoText.Text = invcID.ToString();

                    SqlConnection conn = new SqlConnection(conString);
                    conn.Open();

                    SqlCommand invIdSqlCommand =
                        new SqlCommand(
                            "select * from HeadInventory where VoucharNo ='" +
                            invoiceNoText.Text + "'and Vouchartype='SI'", conn);
                    invIdSqlCommand.ExecuteNonQuery();
                    SqlDataReader read = invIdSqlCommand.ExecuteReader();

                    while (read.Read())
                    {
                        datePucharse.Text = (read["VoucharDate"].ToString());
                        SuplyerIdTxt.Text = (read["CustSuplyId"].ToString());
                        salesmanIDtxt.Text = (read["SalesmanId"].ToString());
                        grossTotal.Text = (read["GrossTotal"].ToString());
                        discountText.Text = (read["Discount"].ToString());
                        netGrossTotal.Text = (read["TotalSale"].ToString());
                        int ind = Convert.ToInt16(read["Branch"].ToString());
                        comboBox2.SelectedIndex = ind - 1;
                        cashPay.Text = (read["Cash"].ToString());
                        knetPay.Text = (read["Knet"].ToString());
                        knetCode.Text = (read["KnetCode"].ToString());
                        dueText.Text = (read["Due"].ToString());

                    }
                    conn.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Please Select Branch");
                }
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                try
                {
                    //initialize a new instance of sqlcommand  ItemCode as BarCode, 
                    SqlCommand cmd = new SqlCommand();
                    //set a connection used by this instance of sqlcommand
                    cmd.Connection = con;
                    //set the sql statement to execute at the data source
                    cmd.CommandText =
                        "select SerialNo as BarCode,ItemCode, ItemName, Quantity as QTY,UnitPrice as Price,TotalPrice as Total,SerialNo, CostUnit from Inventory where VoucharNo='" +
                        invoiceNoText.Text + "'";
                    //initialize a new instance of sqlDataAdapter
                    SqlDataAdapter da = new SqlDataAdapter();
                    //set the sql statement or stored procedure to execute at the data source
                    da.SelectCommand = cmd;
                    //initialize a new instance of DataTable
                    DataTable dt = new DataTable();
                    //add or resfresh rows in the certain range in the datatable to match those in the data source.
                    da.Fill(dt);
                    //set the data source to display the data in the datagridview

                    ProductList.Columns[1].DataPropertyName = "BarCode";
                    ProductList.Columns[2].DataPropertyName = "ItemCode";
                    ProductList.Columns[3].DataPropertyName = "ItemName";
                    ProductList.Columns[4].DataPropertyName = "QTY";
                    ProductList.Columns[5].DataPropertyName = "Price";
                    ProductList.Columns[6].DataPropertyName = "Total";
                    ProductList.Columns[7].DataPropertyName = "SerialNo";
                    ProductList.Columns[8].DataPropertyName = "CostUnit";
                    ProductList.Columns[9].DataPropertyName = "ID";
                    ProductList.DataSource = dt;
                    //ProductList.Refresh();
                    //ProductList.Parent.Refresh();



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
                SaveBtn.Enabled = false;
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to Delete ??", "Confirm OK!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    SqlConnection con = new SqlConnection(conString);
                    con.Open();
                    SqlCommand cmd1 =
                        new SqlCommand("Delete from HeadInventory where VoucharNo='" + invoiceNoText.Text + "'", con);
                    SqlCommand cmd = new SqlCommand(
                        "Delete from Inventory where VoucharNo='" + invoiceNoText.Text + "'", con);

                    //set a connection used by this instance of sqlcommand
                    //cmd.Connection = con;
                    //set the sql statement to execute at the data source
                    //cmd.CommandText = "Delete from Inventory where VoucharNo='" + invoiceNoText.Text + "'";
                    cmd1.ExecuteNonQuery();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                catch (Exception)
                {

                }
            }
        }

       
        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void dueText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (cashPay.Text == "")
                {
                    cash = 0;
                }
                else
                {
                    cash = Convert.ToDouble(cashPay.Text);
                }
                if (knetPay.Text == "")
                {
                    knet = 0;
                }
                else
                {
                    knet = Convert.ToDouble(knetPay.Text);
                }
                if (dueText.Text == "")
                {
                    Due = 0;
                }
                else
                {
                    Due = Convert.ToDouble(dueText.Text);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please Enter Amount");
            }
            if (Convert.ToDouble(netGrossTotal.Text) == cash + knet + Due)
            {
                SaveBtn.Enabled = true;
            }
            else
            {
                SaveBtn.Enabled = false;
            }
        }



        

        private void EditcashSales_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (grossTotal.Text == null)
            //{
            //    grossTotal.Text = "0";

            //}
            //if ((cash + knet + Due) != Convert.ToDouble(grossTotal.Text))
            //{
            //    e.Cancel = true;
            //}
            //else
            //{
            //    cashSales csSales = new cashSales();
            //    csSales.Show();
            //}
            //e.Cancel = true;
            // ReSharper disable once ArrangeThisQualifier
            cashSales csSales = new cashSales();
            csSales.Show();
           
        }

        private void EditcashSales_Load(object sender, EventArgs e)
        {
            datePucharse.Value = BranchInfo.dateTime;
        }

        

        
    }
}

    