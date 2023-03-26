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
    public partial class CreditList : Form
    {
        public CreditList()
        {
            InitializeComponent();
            conString = Login.LoginInfo.conString;
            SqlConnection con = new SqlConnection(conString);
            //con.Open();
            //SqlCommand cmd =
            //    new SqlCommand("select MAX(InvoiceId)+1 as ID from voucherNo where Branch='1' and Type='SI'", con);
            //cmd.ExecuteNonQuery();
            //SqlDataReader read = cmd.ExecuteReader();

            //while (read.Read())
            //{
            //    invoiceNoText.Text = (read["ID"].ToString());
                


            //}
            //con.Close();

            con.Open();
            SqlCommand cmd1 =
                new SqlCommand("select ItemCode,ItemName from ItemList ", con);
            cmd1.ExecuteNonQuery();
            SqlDataReader read1 = cmd1.ExecuteReader();

            while (read1.Read())
            {
                ItemListCombo.Items.Add(read1["ItemCode"].ToString() + '-' + read1["ItemName"].ToString());
               
            }
            con.Close();
        }
        private string conString;
        private void CreditList_Load(object sender, EventArgs e)
        {
            SqlConnection con2 = new SqlConnection(conString);
            con2.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;

            cmd.CommandText = "Select SuplyerId,SuplyerName,ProductName,SerialNo,BuyDate,BuyAmount,PayDate,PayAmount,CreditInvoice,VoucharNo,Comments,ID from SuplyerInfo order by BuyDate asc ";

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            CreditListGrid.Columns[0].DataPropertyName = "SuplyerId";
            CreditListGrid.Columns[1].DataPropertyName = "SuplyerName";
            CreditListGrid.Columns[2].DataPropertyName = "ProductName";
            CreditListGrid.Columns[3].DataPropertyName = "SerialNo";
            CreditListGrid.Columns[4].DataPropertyName = "BuyDate";
            CreditListGrid.Columns[5].DataPropertyName = "BuyAmount";
            
            CreditListGrid.Columns[6].DataPropertyName = "PayDate";
            CreditListGrid.Columns[7].DataPropertyName = "PayAmount";
            CreditListGrid.Columns[8].DataPropertyName = "CreditInvoice";
            CreditListGrid.Columns[9].DataPropertyName = "VoucharNo";
            CreditListGrid.Columns[10].DataPropertyName = "Comments";
            CreditListGrid.Columns[11].DataPropertyName = "ID";
            CreditListGrid.DataSource = dt;
            con2.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection con2 = new SqlConnection(conString);
            SqlCommand com2 =
                new SqlCommand(
                    "insert into SuplyerInfo ([SuplyerId],[SuplyerName],[ProductName],[SerialNo],[BuyDate],[BuyAmount],[CreditInvoice],[VoucharNo],[Comments],[Branch]) values ('" + SuppIdTxt.Text + "','" +
                    SuppNameTxt.Text + "','" + ItemListCombo.SelectedItem + "','" + ProductSLTxt.Text + "','" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "','" + PriceTxt.Text + "','" + CreditIvcTxt.Text + "','" + InvcNoTxt.Text + "','" + RemarksTxt.Text + "','" + (branchCombo.SelectedIndex + 1) + "')", con2);
            con2.Open();
            com2.ExecuteNonQuery();
            con2.Close();
            con2.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;

            cmd.CommandText = "Select SuplyerId,SuplyerName,ProductName,SerialNo,BuyDate,BuyAmount,PayDate,PayAmount,CreditInvoice,VoucharNo,Comments,ID from SuplyerInfo order by BuyDate asc ";

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            CreditListGrid.Columns[0].DataPropertyName = "SuplyerId";
            CreditListGrid.Columns[1].DataPropertyName = "SuplyerName";
            CreditListGrid.Columns[2].DataPropertyName = "ProductName";
            CreditListGrid.Columns[3].DataPropertyName = "SerialNo";
            CreditListGrid.Columns[4].DataPropertyName = "BuyDate";
            CreditListGrid.Columns[5].DataPropertyName = "BuyAmount";
            CreditListGrid.Columns[6].DataPropertyName = "PayDate";
            CreditListGrid.Columns[7].DataPropertyName = "PayAmount";
            CreditListGrid.Columns[8].DataPropertyName = "CreditInvoice";
            CreditListGrid.Columns[9].DataPropertyName = "VoucharNo";
            CreditListGrid.Columns[10].DataPropertyName = "Comments";
            CreditListGrid.Columns[11].DataPropertyName = "ID";
            CreditListGrid.DataSource = dt;
            con2.Close();
        }

        private void BranchComb2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con2 = new SqlConnection(conString);
            con2.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;

            cmd.CommandText = "Select SuplyerId,SuplyerName,ProductName,SerialNo,BuyDate,BuyAmount,PayDate,PayAmount,CreditInvoice,VoucharNo,Comments,ID from SuplyerInfo where branch='"+(BranchComb2.SelectedIndex+1)+"' and buydate between '"+dateTimePicker2.Value.Date.ToString("yyyy-MM-dd")+"' and '"+dateTimePicker3.Value.Date.ToString("yyyy-MM-dd")+"' order by BuyDate asc ";

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            con2.Close();
            CreditListGrid.Columns[0].DataPropertyName = "SuplyerId";
            CreditListGrid.Columns[1].DataPropertyName = "SuplyerName";
            CreditListGrid.Columns[2].DataPropertyName = "ProductName";
            CreditListGrid.Columns[3].DataPropertyName = "SerialNo";
            CreditListGrid.Columns[4].DataPropertyName = "BuyDate";
            CreditListGrid.Columns[5].DataPropertyName = "BuyAmount";

            CreditListGrid.Columns[6].DataPropertyName = "PayDate";
            CreditListGrid.Columns[7].DataPropertyName = "PayAmount";
            CreditListGrid.Columns[8].DataPropertyName = "CreditInvoice";
            CreditListGrid.Columns[9].DataPropertyName = "VoucharNo";
            CreditListGrid.Columns[10].DataPropertyName = "Comments";
            CreditListGrid.Columns[11].DataPropertyName = "ID";
            CreditListGrid.DataSource = dt;
            
        }

        private void SupIDTxt2_TextChanged(object sender, EventArgs e)
        {
            (CreditListGrid.DataSource as DataTable).DefaultView.RowFilter = string.Format("SuplyerId LIKE '%{0}%'", SupIDTxt2.Text);
        }

        private void SerialSearchTxt_TextChanged(object sender, EventArgs e)
        {
            (CreditListGrid.DataSource as DataTable).DefaultView.RowFilter = string.Format("SerialNo LIKE '%{0}%'", SerialSearchTxt.Text);
      
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to Update ??", "Confirm OK!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    SqlConnection con = new SqlConnection(conString);

                    for (int r = 0; r < CreditListGrid.RowCount - 1; r++)
                    {
                        DataGridViewRow g1 = CreditListGrid.Rows[r];


                        SqlCommand com =
                            new SqlCommand(
                                "update SuplyerInfo set PayDate='" + g1.Cells[6].Value + "',PayAmount= '" +
                                g1.Cells[7].Value + "',CreditInvoice= '" + g1.Cells[8].Value + "',Comments= '" + g1.Cells[10].Value + "' where Id='" + g1.Cells[11].Value + "'", con);
                        con.Open();
                        com.ExecuteNonQuery();
                        con.Close();
                    }



                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    cmd.CommandText =
                        "Select SuplyerId,SuplyerName,ProductName,SerialNo,BuyDate,BuyAmount,PayDate,PayAmount,CreditInvoice,VoucharNo,Comments,ID from SuplyerInfo where branch='" +
                        (BranchComb2.SelectedIndex + 1) + "' and buydate between '" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd") + "' and '" +
                        dateTimePicker3.Value.Date.ToString("yyyy-MM-dd") + "' order by BuyDate asc ";

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    da.Fill(dt);


                    CreditListGrid.Columns[0].DataPropertyName = "SuplyerId";
                    CreditListGrid.Columns[1].DataPropertyName = "SuplyerName";
                    CreditListGrid.Columns[2].DataPropertyName = "ProductName";
                    CreditListGrid.Columns[3].DataPropertyName = "SerialNo";
                    CreditListGrid.Columns[4].DataPropertyName = "BuyDate";
                    CreditListGrid.Columns[5].DataPropertyName = "BuyAmount";

                    CreditListGrid.Columns[6].DataPropertyName = "PayDate";
                    CreditListGrid.Columns[7].DataPropertyName = "PayAmount";
                    CreditListGrid.Columns[8].DataPropertyName = "CreditInvoice";
                    CreditListGrid.Columns[9].DataPropertyName = "VoucharNo";
                    CreditListGrid.Columns[10].DataPropertyName = "Comments";
                    CreditListGrid.Columns[11].DataPropertyName = "ID";
                    CreditListGrid.DataSource = dt;

                    con.Close();
                    this.Close();
                    CreditList crlList = new CreditList();
                    crlList.Show();

                }
                catch (Exception)
                {

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
           CreditList crlList = new CreditList();
            crlList.Show();
        }
    }
}
