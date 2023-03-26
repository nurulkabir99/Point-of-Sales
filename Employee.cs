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
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
            SqlConnection cons = new SqlConnection(conString);
            cons.Open();
            SqlCommand cmd =
                new SqlCommand("select MAX(EmployeeId)+1 as ID from EmployeeInfo", cons);
            cmd.ExecuteNonQuery();
            SqlDataReader read = cmd.ExecuteReader();

            while (read.Read())
            {
                max = (read["ID"].ToString());
                empIDTxt.Text = (read["ID"].ToString());


            }
            cons.Close();
            cons.Open();
            SqlCommand cmd3 =
              new SqlCommand("select MAX(UserId)+1 as ID from LogUser", cons);
            cmd3.ExecuteNonQuery();
            SqlDataReader read3 = cmd3.ExecuteReader();

            while (read3.Read())
            {

                UserIdTxt.Text = (read3["ID"].ToString());


            }
            cons.Close();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
        private string conString = Login.LoginInfo.conString;
        public static string max;
        private void button1_Click(object sender, EventArgs e)
        {

            var confirmResult = MessageBox.Show("Are you sure to Submit ??", "Confirm OK!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                // dt.Clear();
                SqlConnection cons = new SqlConnection(conString);
                cons.Open();
                SqlCommand cmd =
                    new SqlCommand("select MAX(EmployeeId)+1 as ID from EmployeeInfo", cons);
                cmd.ExecuteNonQuery();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    max = (read["ID"].ToString());
                    empIDTxt.Text = (read["ID"].ToString());


                }
                cons.Close();
                cons.Open();
                SqlCommand cmd3 =
                  new SqlCommand("select MAX(UserId)+1 as ID from LogUser", cons);
                cmd3.ExecuteNonQuery();
                SqlDataReader read3 = cmd3.ExecuteReader();

                while (read3.Read())
                {
                    
                    UserIdTxt.Text = (read3["ID"].ToString());


                }
                cons.Close();
                try
                {
                    SqlConnection con1 = new SqlConnection(conString);
                    SqlCommand com1 =
                        new SqlCommand(
                            "insert into EmployeeInfo ([EmployeeId],[EmployeeName],[Designation],[Contact],[JoinDate],[BranchID],[Age],[Nationality],[Passport],[PassExpire],[CivilID],[CivilExpire],[UserID],[UserName],[Password],[Salary]) values ('" +
                            max + "','"+EmpNameTxt.Text+"','" + DesignaTxt.Text +
                            "','" + ContactTxt.Text + "','" + JoinDate.Value.Date.ToString("yyyy-MM-dd") + "','" + (comboBox1.SelectedIndex + 1) + "','" + ageTxt.Text + "','" + NationTxt.Text + "','" + PassTxt.Text + "','" + PassExpDate.Value.Date.ToString("yyyy-MM-dd") + "','" + CivilTxt.Text + "','" + CivilExpDate.Value.Date.ToString("yyyy-MM-dd") + "','" + UserIdTxt.Text + "','" + UserNameTxt.Text + "','" + PasswordTxt.Text + "','" + SalaryTxt.Text + "')", con1);
                    con1.Open();
                    com1.ExecuteNonQuery();
                    con1.Close();
                   
                    SqlConnection con2 = new SqlConnection(conString);
                    SqlCommand com2 =
                        new SqlCommand(
                            "insert into LogUser ([UserId],[UserName],[Password],[RegistrationDate]) values ('" + UserIdTxt.Text + "','" + UserNameTxt.Text + "','" + PasswordTxt.Text + "','" + PassExpDate.Value.Date.ToString("yyyy-MM-dd") + "')", con2);
                    con2.Open();
                    com2.ExecuteNonQuery();
                    con2.Close();
                
                    
                    this.Close();
                    Employee emp = new Employee();
                    emp.Show();
                    //comboBox2.Text = cashPurchase.BranchInfo.BranchID;
                    //invoiceNoText.Text = cashPurchase.BranchInfo.MaxinvoiceNo;
                    //salesmanIDtxt.Text = cashPurchase.BranchInfo.SalesmanId;


                }

                catch (Exception)
                {
                    MessageBox.Show("Invalid Value");
                }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static string vEdit;
        public static string vPurchase;
        public static string vProfit;
        public static string vInventory;
        private void button3_Click(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
                vEdit = "1";}
            else
            {
                vEdit = "0";
            }
            if (checkBox2.Checked)
            {
                vPurchase = "1";
            }
            else
            {
                vPurchase = "0";
            }

            if (checkBox3.Checked)
            {
                vProfit = "1";
            }
            else
            {
                vProfit = "0";
            }
            if (checkBox4.Checked)
            {
                vInventory = "1";
            }
            else
            {
                vInventory = "0";
            }

            var confirmResult = MessageBox.Show("Are you sure to Update ??", "Confirm OK!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    SqlConnection con1 = new SqlConnection(conString);
                    SqlCommand com1 =
                        new SqlCommand(
                            "Update EmployeeInfo set EmployeeName='" + EmpNameTxt.Text + "', Designation='" +
                            DesignaTxt.Text + "',Contact='" + ContactTxt.Text + "',JoinDate='" + JoinDate.Value.Date.ToString("yyyy-MM-dd") +
                            "'," + "BranchID='" + (comboBox1.SelectedIndex+1) + "',Age='" + ageTxt.Text + "',Nationality='" + NationTxt.Text +
                            "'," + "Passport='" + PassTxt.Text + "',PassExpire='" + PassExpDate.Value.Date.ToString("yyyy-MM-dd") + "',CivilID='" + CivilTxt.Text +
                            "'," + "CivilExpire='" + CivilExpDate.Value.Date.ToString("yyyy-MM-dd") + "',UserName='" + UserNameTxt.Text +
                            "'," + "Password='" + PasswordTxt.Text + "',Salary='" + SalaryTxt.Text + "'," + "PEdit='" + vEdit + "',PPurchase='" + vPurchase + "',PProfit='" + vProfit + "',PInventory='" + vInventory + "'  where EmployeeId='" +
                            empIDTxt.Text + "'",
                            con1);
                    con1.Open();
                    com1.ExecuteNonQuery();
                    con1.Close();
                    SqlCommand com =
                       new SqlCommand(
                           "Update LogUser set UserName='" + UserNameTxt.Text + "', Password='" +
                           PasswordTxt.Text + "',RegistrationDate='" + DateTime.Today.Date.ToString("yyyy-MM-dd") + "' where UserId='" + UserIdTxt.Text + "'",
                           con1);
                    con1.Open();
                    com.ExecuteNonQuery();
                    con1.Close();
                }
                catch (Exception ex)
                {
                    
                }
            }
        }

        private void empIDTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SqlConnection con = new SqlConnection(conString);

                using (SqlCommand cmd = new SqlCommand("SELECT a.*, b.UserName uName, b.Password uPassword, b.RegistrationDate regidate  FROM EmployeeInfo a left join LogUser b on a.userId=b.UserId where EmployeeId = '" + empIDTxt.Text + "'", con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            EmpNameTxt.Text = (reader["EmployeeName"].ToString());
                            DesignaTxt.Text = (reader["Designation"].ToString());
                            SalaryTxt.Text = (reader["Salary"].ToString());
                            JoinDate.Text = (reader["JoinDate"].ToString());
                            comboBox1.SelectedIndex = Convert.ToInt16(reader["BranchID"].ToString())-1;
                            ageTxt.Text = (reader["Age"].ToString());
                            NationTxt.Text = (reader["Nationality"].ToString());
                            ContactTxt.Text = (reader["Contact"].ToString());
                            PassTxt.Text = (reader["Passport"].ToString());
                            PassExpDate.Text = (reader["PassExpire"].ToString());
                            CivilTxt.Text = (reader["CivilID"].ToString());
                            CivilExpDate.Text = (reader["CivilExpire"].ToString());
                            UserIdTxt.Text = (reader["UserID"].ToString());
                            Login.LoginInfo.PEdit = (reader["PEdit"].ToString());
                            Login.LoginInfo.PPurchase = (reader["PPurchase"].ToString());
                            Login.LoginInfo.PProfit = (reader["PProfit"].ToString());
                            Login.LoginInfo.PInventory = (reader["PInventory"].ToString());
                            UserNameTxt.Text = (reader["uName"].ToString());
                            PasswordTxt.Text = (reader["uPassword"].ToString());
                            if (Login.LoginInfo.PEdit=="1")
                            {
                                checkBox1.Checked = true;
                            }
                            else
                            {
                                checkBox1.Checked = false;
                            }

                            if (Login.LoginInfo.PPurchase == "1")
                            {
                                checkBox2.Checked = true;
                            }
                            else
                            {
                                checkBox2.Checked = false;
                            }

                            if (Login.LoginInfo.PProfit == "1")
                            {
                                checkBox3.Checked = true;
                            }
                            else
                            {
                                checkBox3.Checked = false;
                            }

                            if (Login.LoginInfo.PInventory == "1")
                            {
                                checkBox4.Checked = true;
                            }
                            else
                            {
                                checkBox4.Checked = false;
                            }
                        }
                    }
                    con.Close();
                }
                button1.Enabled = false;
            }
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            //cashPurchase.BranchInfo.SalesmanId = Login.LoginInfo.SalesmanId;
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conString);
            cmd.Connection = con;

            cmd.CommandText = "Select EmployeeId,EmployeeName from EmployeeInfo order by EmployeeId asc";

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.Columns[0].DataPropertyName = "EmployeeId";
            dataGridView1.Columns[1].DataPropertyName = "EmployeeName";
            dataGridView1.DataSource = dt;
        }
    }
}
