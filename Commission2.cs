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
    public partial class Commission2 : Form
    {
        public string conString;
        public Commission2()
        {
            InitializeComponent();
            StreamReader reader = new StreamReader("C:\\Safi\\ConString.txt");
            conString = reader.ReadLine();
            reader.Close();
            salestxtc.Text = Login.LoginInfo.SalesmanId;
            branchTxt.Text = Login.LoginInfo.branchID;
            DateTime todate = DateTime.Today;
            tdate = todate.ToString("yyyy-MM-dd");
            ttime = DateTime.Now.ToString("hh:mm:ss tt");
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conString);
            cmd.Connection = con;

            cmd.CommandText = "select UserID, Name,Amount,Date,time,Branch,Id from Commission where branch='" + branchTxt.Text + "' and date ='" + tdate + "' order by date asc";

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.Columns[0].DataPropertyName = "UserId";
            dataGridView1.Columns[1].DataPropertyName = "Name";
            dataGridView1.Columns[2].DataPropertyName = "Amount";
            dataGridView1.Columns[3].DataPropertyName = "Date";
            dataGridView1.Columns[4].DataPropertyName = "time";
            dataGridView1.Columns[5].DataPropertyName = "Branch";
            dataGridView1.Columns[6].DataPropertyName = "Id";
            dataGridView1.DataSource = dt;
        }

        public static string tdate;
        public static string ttime;
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime todate= DateTime.Today;
            tdate = todate.ToString("yyyy-MM-dd");
            ttime = DateTime.Now.ToString("hh:mm:ss tt");

            SqlConnection con2 = new SqlConnection(conString);
            SqlCommand com2 =
                new SqlCommand(
                    "insert into commission (UserID, Name,Amount,Date,time,Branch) values ('" + salestxtc.Text + "','" +
                    comtype.Text + "','" + amnttextc.Text + "','" + tdate + "','" + ttime + "','" + branchTxt.Text + "')", con2);
            con2.Open();
            com2.ExecuteNonQuery();
            con2.Close();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;

            cmd.CommandText = "select UserID, Name,Amount,Date,time,Branch,Id from Commission where branch='" + branchTxt.Text + "' and date ='" + tdate + "' order by date asc";

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.Columns[0].DataPropertyName = "UserId";
            dataGridView1.Columns[1].DataPropertyName = "Name";
            dataGridView1.Columns[2].DataPropertyName = "Amount";
            dataGridView1.Columns[3].DataPropertyName = "Date";
            dataGridView1.Columns[4].DataPropertyName = "time";
            dataGridView1.Columns[5].DataPropertyName = "Branch";
            dataGridView1.Columns[6].DataPropertyName = "Id";
            dataGridView1.DataSource = dt;
            comtype.Text = "";
            amnttextc.Text = "";

        }

       
    }
}
