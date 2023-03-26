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

namespace PointInvt
{
    public partial class AddNewItem : Form
    {
        public string conString;
        public AddNewItem()
        {
            InitializeComponent();
            conString = Login.LoginInfo.conString;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand cmd =
                new SqlCommand("select MAX(Id)+1 as ID from ItemList ", con);
            cmd.ExecuteNonQuery();
            SqlDataReader read = cmd.ExecuteReader();

            while (read.Read())
            {
                //comboBox1.Text = (read["ItemCode"].ToString() + read["ItemName"].ToString());
                IdText.Text = (read["ID"].ToString());
               
            }
            con.Close();
            con.Open();
            SqlCommand cmd1 =
                new SqlCommand("select ItemCode,ItemName from ItemList ", con);
            cmd1.ExecuteNonQuery();
            SqlDataReader read1 = cmd1.ExecuteReader();

            while (read1.Read())
            {
                comboBox1.Items.Add(read1["ItemCode"].ToString() +'-'+ read1["ItemName"].ToString());
               // IdText.Text = (read["ID"].ToString());

            }
            con.Close();
           
        }

        private void AddNewItem_Load(object sender, EventArgs e)
        {
            
            cashPurchase.BranchInfo.SalesmanId = Login.LoginInfo.SalesmanId;
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conString);
            cmd.Connection = con;

            cmd.CommandText = "Select ID,ItemCode,ItemName,ItemNameArabic,ParentId from ItemList order by ParentId asc";

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.Columns[0].DataPropertyName = "ID";
            dataGridView1.Columns[1].DataPropertyName = "ItemCode";
            dataGridView1.Columns[2].DataPropertyName = "ItemName";
            dataGridView1.Columns[3].DataPropertyName = "ItemNameArabic";
            dataGridView1.Columns[4].DataPropertyName = "ParentId";

            dataGridView1.DataSource = dt;
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
           string HeadId = comboBox1.Text.Split('-').Skip(0).FirstOrDefault();
            con.Open();
            SqlCommand cmd1 =
                new SqlCommand("select (Itemcode)+1 as Itemcode,ParentId from ItemList where ItemCode='" + HeadId + "' ", con);
            cmd1.ExecuteNonQuery();
            SqlDataReader read1 = cmd1.ExecuteReader();

            while (read1.Read())
            {
                //ChartACCIDText.Text = HeadId + Accid;
                //Accid = (read["ID"].ToString());
                ITemcodeTxt.Text = (read1["Itemcode"].ToString());
                ParentTxt.Text = (read1["ParentId"].ToString());
                //cashPurchase.BranchInfo.MaxinvoiceNo = (read["ID"].ToString());


            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            SqlConnection con2 = new SqlConnection(conString);
            SqlCommand com2 =
                new SqlCommand(
                    "insert into ItemList (Id,ItemCode,ItemName,ItemNameArabic,ParentId) values ('" + IdText.Text + "','" +
                    ITemcodeTxt.Text + "','" + EngTxt.Text + "','" + ArabicTxt.Text + "','" + ParentTxt.Text + "')", con2);
            con2.Open();
            com2.ExecuteNonQuery();
            con2.Close();
            con2.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;

            cmd.CommandText = "Select ID,ItemCode,ItemName,ItemNameArabic,ParentId from ItemList order by ParentId asc ";

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.Columns[0].DataPropertyName = "ID";
            dataGridView1.Columns[1].DataPropertyName = "ItemCode";
            dataGridView1.Columns[2].DataPropertyName = "ItemName";
            dataGridView1.Columns[3].DataPropertyName = "ItemNameArabic";
            dataGridView1.Columns[4].DataPropertyName = "ParentId";
            dataGridView1.DataSource = dt;
            con2.Close();
            ITemcodeTxt.Text = "";
            EngTxt.Text = "";
            ArabicTxt.Text = "";
            ParentTxt.Text = "";
            this.Close();
            AddNewItem ad = new AddNewItem();
            ad.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
             string searchVal = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
            var confirmResult = MessageBox.Show("Are you sure to Delete ??", "Confirm OK!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    SqlConnection con = new SqlConnection(conString);
                    con.Open();
                    SqlCommand cmd1 =
                        new SqlCommand("Delete from ItemList where Id='" + searchVal + "'", con);

                    cmd1.ExecuteNonQuery();

                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("You can not Delete this Item");
                }
            }
            this.Close();
            AddNewItem ad = new AddNewItem();
            ad.Show();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("ItemName LIKE '%{0}%'", textBox1.Text);
        }
    }
}
