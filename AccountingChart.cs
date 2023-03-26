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
    public partial class AccountingChart : Form
    {
        public string conString;
        public AccountingChart()
        {
            InitializeComponent();
            StreamReader reader = new StreamReader("C:\\Safi\\ConString.txt");
            conString = reader.ReadLine();
            reader.Close();
            cashPurchase.BranchInfo.SalesmanId = Login.LoginInfo.SalesmanId;
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conString);
            cmd.Connection = con;

            cmd.CommandText = "select AccId,ChartOfAccounts,Name,HeadId,ParentId from NameOfAccount order by HeadId asc";

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.Columns[0].DataPropertyName = "AccId";
            dataGridView1.Columns[1].DataPropertyName = "ChartOfAccounts";
            dataGridView1.Columns[2].DataPropertyName = "Name";
            dataGridView1.Columns[3].DataPropertyName = "HeadId";
            dataGridView1.Columns[4].DataPropertyName = "ParentId";
            
            dataGridView1.DataSource = dt;
                
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public static string Accid;
        public static string HeadId;
        public static string Parentid;
       
        private void ParentId_SelectedIndexChanged(object sender, EventArgs e)
        {

            HeadACCCombo.Text = "";
            DataTable linkcat = new DataTable();
            HeadACCCombo.Items.Clear();
            SqlConnection con1 = new SqlConnection(conString);
            con1.Open();
            using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM HeadOfAccount where ParentId='" + (ParentIdCombo.SelectedIndex + 1) + "'", con1))
            {
                da.Fill(linkcat);
            }
            //InvoiceCombo.Items.Add("ALL");
            foreach (DataRow da in linkcat.Rows)
            {
                HeadACCCombo.Items.Add(da[2].ToString()+'-'+da[3].ToString());
                //ItemListCombo.Items.Add(da[2].ToString());

            }
            con1.Close();
            //SqlConnection con = new SqlConnection(conString);
            //con.Open();
            //SqlCommand cmd =
            //    new SqlCommand("select MAX(HeadId)+1 as ID from HeadofAccount where ParentId='" + (ParentId.SelectedIndex + 1) + "'", con);
            //cmd.ExecuteNonQuery();
            //SqlDataReader read = cmd.ExecuteReader();

            //while (read.Read())
            //{
            //    IdText.Text = ((ParentId.SelectedIndex+1) +read["ID"].ToString());
            //    headId = (read["ID"].ToString());
            //    //cashPurchase.BranchInfo.MaxinvoiceNo = (read["ID"].ToString());


            //}
           // con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con2 = new SqlConnection(conString);
            SqlCommand com2 =
                new SqlCommand(
                    "insert into NameOfAccount (AccId,Name,HeadId,ParentId) values ('" + Accid + "','" +
                    ChartACCNametxt.Text + "','" + HeadId + "','" + Parentid + "')", con2);
            con2.Open();
            com2.ExecuteNonQuery();
            con2.Close();
            
            SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;

           cmd.CommandText = "select AccId,ChartOfAccounts,Name,HeadId,ParentId from NameOfAccount order by HeadId asc ";

            SqlDataAdapter da = new SqlDataAdapter();
          da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.Columns[0].DataPropertyName = "AccId";
            dataGridView1.Columns[1].DataPropertyName = "ChartOfAccounts";
            dataGridView1.Columns[2].DataPropertyName = "Name";
            dataGridView1.Columns[3].DataPropertyName = "HeadId";
            dataGridView1.Columns[4].DataPropertyName = "ParentId";
            dataGridView1.DataSource = dt;
            HeadACCCombo.Text = "";
            ChartACCIDText.Text = "";
            ChartACCNametxt.Text = "";




        }

        private void button2_Click(object sender, EventArgs e)
        {
            //int rowIndex = dataGridView1.CurrentCell.RowIndex;
            string searchVal = dataGridView1.CurrentRow.Cells["AccId"].Value.ToString();
            var confirmResult = MessageBox.Show("Are you sure to Delete ??", "Confirm OK!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    SqlConnection con = new SqlConnection(conString);
                    con.Open();
                    SqlCommand cmd1 =
                        new SqlCommand("Delete from NameOfAccount where AccId='" + searchVal + "'", con);
                  
                    cmd1.ExecuteNonQuery();
                   
                    con.Close();
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    cmd.CommandText = "select AccId,ChartOfAccounts,Name,HeadId,ParentId from NameOfAccount order by HeadId asc";

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.Columns[0].DataPropertyName = "AccId";
                    dataGridView1.Columns[1].DataPropertyName = "ChartOfAccounts";
                    dataGridView1.Columns[2].DataPropertyName = "Name";
                    dataGridView1.Columns[3].DataPropertyName = "HeadId";
                    dataGridView1.Columns[4].DataPropertyName = "ParentId";
                    dataGridView1.DataSource = dt;
                    HeadACCCombo.Text = "";
                    ChartACCIDText.Text = "";
                    ChartACCNametxt.Text = "";



                    con.Close();
                }
                catch (Exception)
                {

                }
            }
        }

        private void HeadACCCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand cmd =
                new SqlCommand("select MAX(AccId)+1 as ID from NameOfAccount ", con);
            cmd.ExecuteNonQuery();
            SqlDataReader read = cmd.ExecuteReader();

            while (read.Read())
            {
                //ChartACCIDText.Text = (read["HeadId"].ToString() + read["ID"].ToString());
                Accid = (read["ID"].ToString());
                //HeadId = (read["HeadId"].ToString());
                //Parentid = (read["ParentId"].ToString());
                //cashPurchase.BranchInfo.MaxinvoiceNo = (read["ID"].ToString());


            }
            con.Close();
            HeadId = HeadACCCombo.Text.Split('-').Skip(0).FirstOrDefault();
            con.Open();
            SqlCommand cmd1 =
                new SqlCommand("select ParentId from HeadOfAccount where HeadOfAccounts='"+HeadId+"' ", con);
            cmd1.ExecuteNonQuery();
            SqlDataReader read1 = cmd1.ExecuteReader();

            while (read1.Read())
            {
                ChartACCIDText.Text = HeadId + Accid;
                //Accid = (read["ID"].ToString());
                //HeadId = (read1["HeadOfAccounts"].ToString());
                Parentid = (read1["ParentId"].ToString());
                //cashPurchase.BranchInfo.MaxinvoiceNo = (read["ID"].ToString());


            }
            con.Close();
        }
    }
}
