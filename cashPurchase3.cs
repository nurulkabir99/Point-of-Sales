using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace PointInvt
{
    public partial class cashPurchase3 : Form
    {
        public cashPurchase3()
        {
            InitializeComponent();
            //StreamReader reader = new StreamReader("C:\\Safi\\ConString.txt");
            //conString = reader.ReadLine();
            //reader.Close();
            //string sessionId=Login.LoginInfo.UserID;

            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand cmd =
                new SqlCommand("select MAX(InvoiceId)+1 as ID from voucherNo where Branch='1' and Type='PI'", con);
            cmd.ExecuteNonQuery();
            SqlDataReader read = cmd.ExecuteReader();

            while (read.Read())
            {
                invoiceNoText.Text = (read["ID"].ToString());
                BranchInfo.MaxinvoiceNo = (read["ID"].ToString());


            }
            con.Close();
            BranchInfo.SalesmanId = Login.LoginInfo.SalesmanId;
            comboBox2.SelectedIndex = Convert.ToInt16(Login.LoginInfo.branchID) - 1;
            comboBox2.Text = comboBox2.SelectedIndex.ToString();
            salesmanIDtxt.Text = BranchInfo.SalesmanId;
            
            SaveBtn.Enabled = false;
        }

        public static class BranchInfo
        {
            public static string BranchID;
            public static string MaxinvoiceNo;
            public static string SalesmanId;
            public static int TrueFalse = 0;
            public static Int32 MaxInvcID;
            public static Int32 MinInvcID;
            public static string InvoiceNo;
           

        }
        public DataTable dt = new DataTable();

        private string conString = Login.LoginInfo.conString;

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
        public static string matchvalue;
        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.ProductList.Rows[e.RowIndex].Cells["SlNo"].Value = (e.RowIndex + 1).ToString();

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
            try
            {
                int cellID = ProductList.CurrentCell.RowIndex;

                var searchValue1 = ProductList.Rows[cellID].Cells[7].Value;
                string SearchValue = (ProductList["BarCode", cellID].Value).ToString();
               
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from ItemList WHERE ItemCode = '" + SearchValue + "'", con);
                cmd.ExecuteNonQuery();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    this.ProductList.Rows[e.RowIndex].Cells["ItemCode"].Value = read["Itemcode"].ToString();
                    this.ProductList.Rows[e.RowIndex].Cells["ItemName"].Value = read["itemName"].ToString();
                   

                }
                con.Close();


                //int cellID = ProductList.CurrentCell.RowIndex;

                //string SearchValue = (ProductList["BarCode",cellID].Value).ToString();


                //SqlConnection con1 = new SqlConnection(conString);
                con.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT SerialNo from Inventory WHERE SerialNo = '" + searchValue1 + "'and InOut='0'", con);
                cmd1.ExecuteNonQuery();
                SqlDataReader read1 = cmd1.ExecuteReader();

                while (read1.Read())
                {
                    matchvalue = read1["SerialNo"].ToString();

                }
               
                if (read1.HasRows)
                {
                    MessageBox.Show("You Have Same Barcode");
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Select Product/ يرجى اختيار المنتج");
            }


            foreach (DataGridViewRow row in ProductList.Rows)
            {
                row.Cells[ProductList.Columns["Total"].Index].Value =
                    ((Convert.ToDouble(row.Cells[ProductList.Columns["Quantity"].Index].Value))*
                     (Convert.ToDouble(row.Cells[ProductList.Columns["Price"].Index].Value)));
            }

            double sum = 0;

            for (int i = 0; i < ProductList.Rows.Count; ++i)
            {
                sum += Convert.ToDouble(ProductList.Rows[i].Cells[6].Value);
            }
            grossTotal.Text = sum.ToString();
           // dt=ProductList.DataSource as DataTable;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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

        //private void cashPay_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == '.' && cashPay.Text.Contains("."))
        //    {
        //        e.Handled = true;
        //    }
        //    else if (e.KeyChar == '.' && cashPay.Text.Length==0)
        //    {
        //        e.Handled = true;
        //    }
            
        //    else if (!char.IsControl(e.KeyChar)&& !char.IsDigit(e.KeyChar)&& e.KeyChar!='.')
        //    {
        //        e.Handled = true;
        //    }
        //}
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
            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                //SqlCommand cmd = new SqlCommand("SELECT * from Inventory WHERE SerialNo = '" + itemCodeText.Text + "'and InOut='0'", con);
                SqlCommand cmdIstock =
                    new SqlCommand(
                        "select Itemcode,ItemName, SUM(quantity)as Quantity, SUM(PurchaseCost) as Cost ,SerialNo from Inventory where SerialNo='" +
                        itemCodeText.Text + "' and Branch='" + (comboBox2.SelectedIndex + 1) +
                        "' group by ItemCode , SerialNo, ItemName ", con);
                cmdIstock.ExecuteNonQuery();
                SqlDataReader read = cmdIstock.ExecuteReader();

                while (read.Read())
                {
                    itemNameText.Text = (read["itemName"].ToString());
                    //costText.Text = (read["CostUnit"].ToString());
                    inStockText.Text = (read["Quantity"].ToString());
                    costPrice = float.Parse(read["cost"].ToString())/float.Parse(read["quantity"].ToString());
                    costText.Text = costPrice.ToString();
                }
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = ProductList.CurrentCell.RowIndex;
                ProductList.Rows.RemoveAt(rowIndex);

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

            this.Controls.Clear();
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

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            string searchInvc = Interaction.InputBox("Please Enter your Invoice No/ الرجاء إدخال رقم الفاتورة", "Search",
                "", -1, -1);

            SqlConnection con = new SqlConnection(conString);
            try
            {

                SqlConnection conn = new SqlConnection(conString);
                conn.Open();

                SqlCommand invIdSqlCommand =
                    new SqlCommand(
                        "select * from HeadInventory where VoucharNo ='" + searchInvc + "'and Vouchartype='PI'", conn);
                invIdSqlCommand.ExecuteNonQuery();
                SqlDataReader read = invIdSqlCommand.ExecuteReader();

                while (read.Read())
                {
                    datePucharse.Text = (read["VoucharDate"].ToString());
                    SuplyerIdTxt.Text = (read["CustSuplyId"].ToString());
                    salesmanIDtxt.Text = (read["SalesmanId"].ToString());
                    grossTotal.Text = (read["GrossTotal"].ToString());
                    discountText.Text = (read["Discount"].ToString());
                    netGrossTotal.Text = (read["TotalBuy"].ToString());
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
                    "select ItemCode as BarCode, ItemCode, ItemName, Quantity as QTY,UnitPrice as Price,TotalPrice as Total,SerialNo, CostUnit,Id from Inventory where VoucharNo='" +
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

        }
       public  static string max;
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            
            var confirmResult = MessageBox.Show("Are you sure to Submit ??", "Confirm OK!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
               // dt.Clear();
                SqlConnection cons = new SqlConnection(conString);
                cons.Open();
                SqlCommand cmd =
                    new SqlCommand("select MAX(InvoiceId)+1 as ID from voucherNo where Branch='" + (comboBox2.SelectedIndex + 1) + "' and Type='PI'", cons);
                cmd.ExecuteNonQuery();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    max = (read["ID"].ToString());
                    BranchInfo.MaxinvoiceNo = (read["ID"].ToString());


                }
                cons.Close();
                try
                {
                    SqlConnection con1 = new SqlConnection(conString);
                    SqlCommand com1 =
                        new SqlCommand(
                            "insert into HeadInventory (VoucharNo,VoucharType,VoucharDate,CustSuplyId,GrossTotal,Discount,TotalBuy, VoucharTime, SalesmanId,Branch,Cash,Knet,KnetCode,Due) values ('" +
                            max + "','PI','" + datePucharse.Value.Date.ToString("yyyy-MM-dd") + "','" + SuplyerIdTxt.Text +
                            "','" + grossTotal.Text + "','" + discountText.Text + "','" + netGrossTotal.Text + "','" +
                            datePucharse.Value.TimeOfDay + "','" +
                            salesmanIDtxt.Text + "','" + (comboBox2.SelectedIndex + 1) + "','" + cashPay.Text + "','" +
                            knetPay.Text + "','" + knetCode.Text + "','" + dueText.Text + "')", con1);
                    con1.Open();
                    com1.ExecuteNonQuery();
                    con1.Close();
                    for (int r = 0; r < ProductList.RowCount - 1; r++)
                    {
                        DataGridViewRow g1 = ProductList.Rows[r];
                        //DataRow dr = g1.Cells;
                        //dt.Rows.Add(dr);
                        SqlConnection con = new SqlConnection(conString);
                        SqlCommand com =
                            new SqlCommand(
                                "insert into Inventory (VoucharNo,ItemCode,ItemName,Quantity,CostUnit, UnitPrice, TotalPrice, SerialNo,Branch,Date) values ('" +
                                max + "','" + g1.Cells[2].Value + "','" + g1.Cells[3].Value + "','" +
                                g1.Cells[4].Value + "','" + g1.Cells[5].Value + "','" + g1.Cells[5].Value + "','" +
                                g1.Cells[6].Value + "','" + g1.Cells[7].Value + "','" + (comboBox2.SelectedIndex + 1) +
                                "','" + datePucharse.Value.Date.ToString("yyyy-MM-dd") + "')", con);
                        con.Open();
                        com.ExecuteNonQuery();
                        con.Close();
                    }
                
                    SqlConnection con2 = new SqlConnection(conString);
                    SqlCommand com2 =
                        new SqlCommand(
                            "insert into voucherNo (InvoiceId, Branch, Type) values ('" + max + "','" +
                            (comboBox2.SelectedIndex + 1) + "','PI')", con2);
                    con2.Open();
                    com2.ExecuteNonQuery();
                    con2.Close();
                    //this.Refresh();
                    //this.Close();
                    //cashPurchase ss = new cashPurchase();
                    //ss.Show();
                    if (checkBox1.Checked)
                    {
                        //foreach (DataGridViewRow g  in ProductList.Rows)
                        //{
                        //    dt.Rows.Add(g.Cells[0].Value, g.Cells[1].Value, g.Cells[2].Value, g.Cells[3].Value, g.Cells[4].Value, g.Cells[5].Value, g.Cells[6].Value, g.Cells[7].Value);

                        //}
                        //BranchInfo.BranchID = (comboBox2.SelectedIndex + 1).ToString();
                        string InvoiceNo = max;
                        PurchasesViewer sendData1 = new PurchasesViewer(InvoiceNo);
                        sendData1.ShowDialog();
                    }

                    this.Controls.Clear();
                    this.InitializeComponent();
                    comboBox2.Text = BranchInfo.BranchID;
                    invoiceNoText.Text = BranchInfo.MaxinvoiceNo;
                    salesmanIDtxt.Text = BranchInfo.SalesmanId;


                }

                catch (Exception)
                {
                    MessageBox.Show("Invalid Value");
                }

            }



            //foreach (DataGridViewRow g1 in (ProductList.Rows))
            //{
            //    SqlConnection con = new SqlConnection(conString);
            //    SqlCommand com = new SqlCommand("insert into Inventory (VoucharNo,ItemCode,ItemName,Quantity,UnitPrice, TotalPrice, SerialNo) values ('" + invoiceNoText.Text + "','" + g1.Cells[2].Value + "','" + g1.Cells[3].Value + "','" + g1.Cells[4].Value + "','" + g1.Cells[5].Value + "','" + g1.Cells[7].Value + "','" + g1.Cells[8].Value + "')", con);  
            //    con.Open();  
            //    com.ExecuteNonQuery();  
            //    con.Close();  
            //}  
        }

        private void suplyerBtn_Click(object sender, EventArgs e)
        {
            float supDue;

            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();

                SqlCommand cmdsuplyer =
                    new SqlCommand(
                        "select SuplyerId,SuplyerName, SUM(buyAmount)as Buy, SUM(PayAmount) as Pay from SuplyerInfo where SuplyerId='" +
                        SuplyerIdTxt.Text + "' and branch='"+(comboBox2.SelectedIndex+1)+"' group by SuplyerId, SuplyerName", con);
                cmdsuplyer.ExecuteNonQuery();
                SqlDataReader read = cmdsuplyer.ExecuteReader();

                while (read.Read())
                {
                    suplyerNameTxt.Text = (read["SuplyerName"].ToString());
                    PrePurchaseTxt.Text = (read["Buy"].ToString());
                    supDue = float.Parse(read["Buy"].ToString()) - float.Parse(read["Pay"].ToString());
                    SuplyerDueTxt.Text = supDue.ToString();

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
                        "'and type='PI'", con);
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
            BranchInfo.MaxInvcID = Convert.ToInt32(invoiceNoText.Text);
            BranchInfo.MinInvcID = Convert.ToInt32(invoiceNoText.Text);
            if ((BranchInfo.MaxInvcID > 1999999 || BranchInfo.MinInvcID <= 1000001) && comboBox2.Text == "Safi 1")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 2999999 || BranchInfo.MinInvcID <= 2000001) && comboBox2.Text == "Safi 2")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 3999999 || BranchInfo.MinInvcID <= 3000001) && comboBox2.Text == "Safi 3")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 4999999 || BranchInfo.MinInvcID <= 4000001) && comboBox2.Text == "Safi 4")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 5999999 || BranchInfo.MinInvcID <= 5000001) && comboBox2.Text == "Safi 5")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 6999999 || BranchInfo.MinInvcID <= 6000001) && comboBox2.Text == "Safi 6")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 7999999 || BranchInfo.MinInvcID <= 7000001) && comboBox2.Text == "Safi 7")
            {
                refreash();
            }
            else
            {

                SqlConnection con = new SqlConnection(conString);
                try
                {

                    invoiceNoText.Text = (Convert.ToInt32(invoiceNoText.Text) - 1).ToString();
                    //invoiceNoText.Text = invcID.ToString();
                    SqlConnection conn = new SqlConnection(conString);
                    conn.Open();

                    SqlCommand invIdSqlCommand =
                        new SqlCommand(
                            "select * from HeadInventory where VoucharNo ='" + invoiceNoText.Text +
                            "'and Vouchartype='PI'", conn);
                    invIdSqlCommand.ExecuteNonQuery();
                    SqlDataReader read = invIdSqlCommand.ExecuteReader();

                    while (read.Read())
                    {
                       
                        datePucharse.Text = (read["VoucharDate"].ToString());
                        SuplyerIdTxt.Text = (read["CustSuplyId"].ToString());
                        salesmanIDtxt.Text = (read["SalesmanId"].ToString());
                        grossTotal.Text = (read["GrossTotal"].ToString());
                        discountText.Text = (read["Discount"].ToString());
                        netGrossTotal.Text = (read["TotalBuy"].ToString());
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
                        "select ItemCode as BarCode, ItemCode, ItemName, Quantity as QTY,UnitPrice as Price,TotalPrice as Total,SerialNo, CostUnit,Id from Inventory where VoucharNo='" +
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
                            invoiceNoText.Text + "'and Vouchartype='PI'", conn);
                    invIdSqlCommand.ExecuteNonQuery();
                    SqlDataReader read = invIdSqlCommand.ExecuteReader();

                    while (read.Read())
                    {
                        datePucharse.Text = (read["VoucharDate"].ToString());
                        SuplyerIdTxt.Text = (read["CustSuplyId"].ToString());
                        salesmanIDtxt.Text = (read["SalesmanId"].ToString());
                        grossTotal.Text = (read["GrossTotal"].ToString());
                        discountText.Text = (read["Discount"].ToString());
                        netGrossTotal.Text = (read["TotalBuy"].ToString());
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
                    //initialize a new instance of sqlcommand
                    SqlCommand cmd = new SqlCommand();
                    //set a connection used by this instance of sqlcommand
                    cmd.Connection = con;
                    //set the sql statement to execute at the data source
                    cmd.CommandText =
                        "select ItemCode as BarCode, ItemCode, ItemName, Quantity as QTY,UnitPrice as Price,TotalPrice as Total,SerialNo, CostUnit,Id from Inventory where VoucharNo='" +
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

        private void editText_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to Update ??", "Confirm OK!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    SqlConnection con1 = new SqlConnection(conString);
                    SqlCommand com1 =
                        new SqlCommand(
                            "Update HeadInventory set VoucharDate='" + datePucharse.Value.Date.ToString("yyyy-MM-dd") + "',GrossTotal='" + grossTotal.Text + "',Discount='" + discountText.Text + "'," +
                            "TotalBuy='" + netGrossTotal.Text + "',Cash='" + cashPay.Text +"',Knet='" + knetPay.Text + "'," +
                            "KnetCode='" + knetCode.Text + "',Due='" + dueText.Text + "' where VoucharNo='"+invoiceNoText.Text+"'",
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
                                "Quantity='" + g1.Cells[4].Value + "',CostUnit='" + g1.Cells[5].Value + "', UnitPrice='" +
                                g1.Cells[5].Value + "', TotalPrice='" +
                                g1.Cells[6].Value + "', SerialNo='" + g1.Cells[7].Value + "',date='" + datePucharse.Value.Date.ToString("yyyy-MM-dd") + "' where VoucharNo='" + invoiceNoText.Text + "' and Id='" + g1.Cells[9].Value + "'", con);
                        //SqlCommand com2 =
                        //    new SqlCommand(
                        //        "update Inventory set CostUnit='" + g1.Cells[5].Value + "' where serialNo='" + g1.Cells[7].Value + "'", con);
                      
                        con.Open();
                        com.ExecuteNonQuery();
                       // com2.ExecuteNonQuery();
                        con.Close();
                    }
                    this.Controls.Clear();
                    this.InitializeComponent();
                    comboBox2.Text = BranchInfo.BranchID;
                    invoiceNoText.Text = BranchInfo.MaxinvoiceNo;
                    salesmanIDtxt.Text = BranchInfo.SalesmanId;

                }
                catch (Exception)
                {
                    MessageBox.Show("Please Select Branch");
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            BranchInfo.BranchID = (comboBox2.SelectedIndex + 1).ToString();
            BranchInfo.InvoiceNo = invoiceNoText.Text;
            BarCodeViewer brv = new BarCodeViewer();
            brv.Show();
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
    }
}

    