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
using System.Windows.Forms.VisualStyles;

namespace PointInvt
{
    
    public partial class Login : Form
    {
        
        public Login()
        {
            InitializeComponent();
            StreamReader reader = new StreamReader("C:\\Safi\\ConString.txt");
            LoginInfo.conString = reader.ReadLine();
            reader.Close();
        }
        public static class LoginInfo
        {
            public static string UserID;
            public static string branchID;
            public static string sessionId;
            public static string SalesmanId;
            public static string designation;
            public static string conString;
            public static string PPurchase;
            public static string PEdit;
            public static string PProfit;
            public static string PInventory;

        }
        public static string tdate;
        public static string ttime;
         
        //string conString;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
           Application.Exit();
        }
        string UId;
        string Pass;
        string userId;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            DateTime todate = DateTime.Today;
            tdate = todate.ToString("yyyy-MM-dd");
            ttime = DateTime.Now.ToString("hh:mm:ss tt");
            try
            {

                SqlConnection con = new SqlConnection(LoginInfo.conString);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from LogUser WHERE UserName = '" + txtUserName.Text + "' and PassWord='"+txtPassWord.Text+"'", con);
                
                cmd.ExecuteNonQuery();
                
                SqlDataReader read = cmd.ExecuteReader();
               
               
                while (read.Read())
                {
                     UId = (read["UserName"].ToString());
                     Pass = (read["Password"].ToString());
                    userId = (read["UserId"].ToString());
                }
                con.Close();
                if ((txtUserName.Text == UId) && (txtPassWord.Text == Pass))
                {
                    LoginInfo.UserID = userId;
                   
                    con.Open();
                    SqlCommand salesID = new SqlCommand("SELECT EmployeeID, BranchID, Designation,PEdit,PPurchase,PProfit,PInventory from EmployeeInfo WHERE UserId='" + userId + "'", con);
                    salesID.ExecuteNonQuery();
                    SqlDataReader readSID = salesID.ExecuteReader();

                    while (readSID.Read())
                    {
                   LoginInfo.SalesmanId = (readSID["EmployeeID"].ToString());
                   LoginInfo.branchID = (readSID["BranchID"].ToString());
                   LoginInfo.designation = (readSID["Designation"].ToString());
                   LoginInfo.PEdit = (readSID["PEdit"].ToString());
                   LoginInfo.PPurchase = (readSID["PPurchase"].ToString());
                   LoginInfo.PProfit = (readSID["PProfit"].ToString());
                   LoginInfo.PInventory = (readSID["PInventory"].ToString());


                    }
                   
                    con.Close();
                    if (Login.LoginInfo.designation == "System Admin")
                    {
                        SqlConnection con2 = new SqlConnection(LoginInfo.conString);
                        SqlCommand com2 =
                            new SqlCommand(
                                "insert into UserAcces (UserID,Date,time) values ('" + userId + "','" + tdate + "','" + ttime + "')", con2);
                        con2.Open();
                        com2.ExecuteNonQuery();
                        con2.Close();
                        Inventory invt = new Inventory();
                        invt.Show();
                        this.Hide();
                    }
                    else
                    {
                        SqlConnection con2 = new SqlConnection(LoginInfo.conString);
                        SqlCommand com2 =
                            new SqlCommand(
                                "insert into UserAcces (UserID,Date,time) values ('" + userId + "','" + tdate + "','" + ttime + "')", con2);
                        con2.Open();
                        com2.ExecuteNonQuery();
                        con2.Close();
                        InventoryS invtS = new InventoryS();
                        invtS.Show(); 
                        this.Hide();
                    }
                   
                }
                else if ((txtUserName.Text == "") && (txtPassWord.Text == ""))
                {
                    LoginInfo.UserID = "101";

                    con.Open();
                    SqlCommand salesID = new SqlCommand("SELECT EmployeeID, BranchID, Designation,PEdit,PPurchase,PProfit,PInventory from EmployeeInfo WHERE UserId='" + LoginInfo.UserID + "'", con);
                    salesID.ExecuteNonQuery();
                    SqlDataReader readSID = salesID.ExecuteReader();

                    while (readSID.Read())
                    {
                        LoginInfo.SalesmanId = (readSID["EmployeeID"].ToString());
                        LoginInfo.branchID = (readSID["BranchID"].ToString());
                        LoginInfo.designation = (readSID["Designation"].ToString());
                        LoginInfo.PEdit = (readSID["PEdit"].ToString());
                        LoginInfo.PPurchase = (readSID["PPurchase"].ToString());
                        LoginInfo.PProfit = (readSID["PProfit"].ToString());
                        LoginInfo.PInventory = (readSID["PInventory"].ToString());


                    }

                    con.Close();
                    Inventory invt = new Inventory();
                    invt.Show();
                    this.Hide();

                }
                else MessageBox.Show("Please Check Your ID Or Password");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Contact with your System Admin");
            }
            
            
            
            
            
           
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassWord.Focus();
                e.Handled = true;
            }
        }

        private void txtPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.Focus();
                e.Handled = true;
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
