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
    public partial class AccountsHead : Form
    {
        public string conString;
        public AccountsHead()
        {
            InitializeComponent();
            StreamReader reader = new StreamReader("C:\\Safi\\ConString.txt");
            conString = reader.ReadLine();
            reader.Close();
            cashPurchase.BranchInfo.SalesmanId = Login.LoginInfo.SalesmanId;
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conString);
            cmd.Connection = con;

            cmd.CommandText = "select HeadOfAccounts,Name,ParentId  from HeadOfAccount order by Parentid asc";

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.Columns[0].DataPropertyName = "HeadOfAccounts";
            dataGridView1.Columns[1].DataPropertyName = "Name";
            dataGridView1.Columns[2].DataPropertyName = "ParentId";
            dataGridView1.DataSource = dt;
                
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public static string headId;
        private void ParentId_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            //string sessionId=Login.LoginInfo.UserID;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand cmd =
                new SqlCommand("select MAX(HeadId)+1 as ID from HeadofAccount where ParentId='" + (ParentId.SelectedIndex + 1) + "'", con);
            cmd.ExecuteNonQuery();
            SqlDataReader read = cmd.ExecuteReader();

            while (read.Read())
            {
                IdText.Text = ((ParentId.SelectedIndex+1) +read["ID"].ToString());
                headId = (read["ID"].ToString());
                //cashPurchase.BranchInfo.MaxinvoiceNo = (read["ID"].ToString());


            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con2 = new SqlConnection(conString);
            SqlCommand com2 =
                new SqlCommand(
                    "insert into HeadOfAccount (HeadId, Name, ParentId) values ('" + headId + "','" +
                    nameheadtxt.Text + "','" + (ParentId.SelectedIndex + 1) + "')", con2);
            con2.Open();
            com2.ExecuteNonQuery();
            con2.Close();
            
            SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
            
            cmd.CommandText = "select HeadOfAccounts,Name,ParentId  from HeadOfAccount order by Parentid asc ";

            SqlDataAdapter da = new SqlDataAdapter();
          da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.Columns[0].DataPropertyName = "HeadOfAccounts";
            dataGridView1.Columns[1].DataPropertyName = "Name";
            dataGridView1.Columns[2].DataPropertyName = "ParentId";
            dataGridView1.DataSource = dt;
            IdText.Text = "";
            ParentId.Text = "";
            nameheadtxt.Text = "";




        }

        private void button2_Click(object sender, EventArgs e)
        {
            //int rowIndex = dataGridView1.CurrentCell.RowIndex;
          string searchVal=  dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
            var confirmResult = MessageBox.Show("Are you sure to Delete ??", "Confirm OK!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    SqlConnection con = new SqlConnection(conString);
                    con.Open();
                    SqlCommand cmd1 =
                        new SqlCommand("Delete from HeadOfAccount where HeadOfAccounts='" + searchVal + "'", con);
                  
                    cmd1.ExecuteNonQuery();
                   
                    con.Close();
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    cmd.CommandText = "select HeadOfAccounts,Name,ParentId  from HeadOfAccount order by Parentid asc ";

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.Columns[0].DataPropertyName = "HeadOfAccounts";
                    dataGridView1.Columns[1].DataPropertyName = "Name";
                    dataGridView1.Columns[2].DataPropertyName = "ParentId";
                    dataGridView1.DataSource = dt;
                    IdText.Text = "";
                    ParentId.Text = "";
                    nameheadtxt.Text = "";

                    con.Close();
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
