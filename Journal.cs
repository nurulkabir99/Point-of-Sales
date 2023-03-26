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
    public partial class Journal : Form
    {
        public Journal()
        {
            InitializeComponent();
            StreamReader reader = new StreamReader("C:\\Safi\\ConString.txt");
            conString = reader.ReadLine();
            reader.Close();
            comboBox1.SelectedIndex = Convert.ToInt16(Login.LoginInfo.branchID) - 1;
           BranchInfo.SalesmanId = Login.LoginInfo.SalesmanId;
            AccountIDTxt.Text = BranchInfo.SalesmanId;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand cmd =
                new SqlCommand("select MAX(InvoiceId)+1 as ID from AccVNo where Branch='1' and Type='JI'", con);
            cmd.ExecuteNonQuery();
            SqlDataReader read = cmd.ExecuteReader();

            while (read.Read())
            {
                VoucharNoTxt.Text = (read["ID"].ToString());
                BranchInfo.MaxinvoiceNo = (read["ID"].ToString());


            }
            con.Close();

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
            public static string HeadIdDR;
            public static string HeadIdCR;
            public static string ParentIdDR;
            public static string ParentIdCR;
            public static double amount;


        }
        public string conString;
        private void Journal_Load(object sender, EventArgs e)
        {
            DataTable dts = this.GetData("SELECT HeadOfAccounts as Id, Name FROM HeadOfAccount");
            this.PopulateTreeView(dts, 0, null);
        }
        private void PopulateTreeView(DataTable dtParent, int parentId, TreeNode treeNode)
        {
            foreach (DataRow row in dtParent.Rows)
            {
                TreeNode child = new TreeNode
                {
                    Text = row["ID"].ToString() + "-" + row["Name"].ToString(),
                    Tag = row["Id"]
                };
                if (parentId == 0)
                {
                    treeView1.Nodes.Add(child);
                    DataTable dtChild = this.GetData("SELECT ChartOfAccounts as ID,Name FROM NameOfAccount WHERE HeadId = " + child.Tag);
                    PopulateTreeView(dtChild, Convert.ToInt32(child.Tag), child);
                }
                else
                {
                    treeNode.Nodes.Add(child);
                }
            }
        }
        private DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                return dt;
            }
        }
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells["Sl"].Value = (e.RowIndex + 1).ToString();

        }

        //private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
            
        //}

        private void dataGridView1_RowPostPaint_1(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells["Sl"].Value = (e.RowIndex + 1).ToString();

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int cellID = dataGridView1.CurrentCell.RowIndex;

                string SearchValue = (dataGridView1["AccId", cellID].Value).ToString();


                SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from NameOfAccount WHERE ChartOfAccounts = '" + SearchValue + "'", con);
                cmd.ExecuteNonQuery();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells["NameAccount"].Value = read["Name"].ToString();
                    this.dataGridView1.Rows[e.RowIndex].Cells["HeadId"].Value = read["HeadId"].ToString();
                    this.dataGridView1.Rows[e.RowIndex].Cells["ParentId"].Value = read["ParentId"].ToString();

                    if (((read["ParentId"].ToString() == "1") || (read["ParentId"].ToString() == "2")) && (this.dataGridView1.Rows[e.RowIndex].Cells["Credit"].Value != null))
                    {
                        if (Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["Credit"].Value) == 0)
                        {
                            this.dataGridView1.Rows[e.RowIndex].Cells["Amount"].Value =this.dataGridView1.Rows[e.RowIndex].Cells["Debit"].Value;
                        }
                        else
                        {
                             this.dataGridView1.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["Credit"].Value) * (-1);
                        }
                       
                    }
                    //else
                    //{
                    //    this.dataGridView1.Rows[e.RowIndex].Cells["Amount"].Value =this.dataGridView1.Rows[e.RowIndex].Cells["Debit"].Value;
                    //}

                    else if (((read["ParentId"].ToString() == "3") || (read["ParentId"].ToString() == "4")) && (this.dataGridView1.Rows[e.RowIndex].Cells["Debit"].Value!=null))
                    {
                        if (Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["Debit"].Value) == 0)
                        {
                            this.dataGridView1.Rows[e.RowIndex].Cells["Amount"].Value = this.dataGridView1.Rows[e.RowIndex].Cells["Credit"].Value;
                        }
                        else
                        {
                            this.dataGridView1.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["Debit"].Value) * (-1);
                        }
                        
                    }
                    else
                    {
                        if ((read["ParentId"].ToString() == "1") || (read["ParentId"].ToString() == "2"))
                        {
                            this.dataGridView1.Rows[e.RowIndex].Cells["Amount"].Value = this.dataGridView1.Rows[e.RowIndex].Cells["debit"].Value;
                        }
                        else
                        {
                            this.dataGridView1.Rows[e.RowIndex].Cells["Amount"].Value = this.dataGridView1.Rows[e.RowIndex].Cells["Credit"].Value;
                        }
                        
                    }


                }
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Select Product/ يرجى اختيار المنتج");
            }


            double sum = 0;
            double sum1 = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                
                sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                sum1 += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value);
            }
            DBTotalTxt.Text = sum.ToString();
            CRTotalTXT.Text = sum1.ToString();
            // dt=ProductList.DataSource as DataTable;

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to Submit ??", "Confirm OK!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes && (DBTotalTxt.Text==CRTotalTXT.Text))
            {
                // dt.Clear();
                try
                {
                    SqlConnection con2 = new SqlConnection(conString);
                    SqlCommand com2 =
                        new SqlCommand(
                            "insert into HeadingAccounts (VoucharNo,VoucharType,TotalDebit,TotalCredit,UserId,Branch,date) values ('" + VoucharNoTxt.Text + "','JI','" + DBTotalTxt.Text + "','" + CRTotalTXT.Text + "','" + AccountIDTxt.Text + "','" +
                            (comboBox1.SelectedIndex + 1) + "','"+dateTimePicker1.Value.Date.ToString("yyyy-MM-dd")+"')", con2);
                    con2.Open();
                    com2.ExecuteNonQuery();
                    con2.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please Check Every Filed");
                }
                try
                {
                   
                    for (int r = 0; r < dataGridView1.RowCount - 1; r++)
                    {
                        DataGridViewRow g1 = dataGridView1.Rows[r];
                        //DataRow dr = g1.Cells;
                        //dt.Rows.Add(dr);
                        SqlConnection con = new SqlConnection(conString);
                        SqlCommand com =
                            new SqlCommand(
                                "insert into Accounts (VoucharNo,VoucharType,AccountId,AccountName,Description, Debit,Credit,HeadId,ParentId,Amount,UserId,Branch,Date) values ('" +
                                VoucharNoTxt.Text + "','JI','" + g1.Cells[1].Value + "','" + g1.Cells[2].Value + "','" +
                                g1.Cells[3].Value + "','" + g1.Cells[4].Value + "','" + g1.Cells[5].Value + "','" +
                                g1.Cells[6].Value + "','" + g1.Cells[7].Value + "','" + g1.Cells[8].Value + "','" + AccountIDTxt.Text+ "','" + (comboBox1.SelectedIndex + 1) +
                                "','" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "')", con);
                        con.Open();
                        com.ExecuteNonQuery();
                        con.Close();
                    }

                    SqlConnection con2 = new SqlConnection(conString);
                    SqlCommand com2 =
                        new SqlCommand(
                            "insert into AccVNo (InvoiceId, Branch, Type) values ('" + VoucharNoTxt.Text + "','" +
                            (comboBox1.SelectedIndex + 1) + "','JI')", con2);
                    con2.Open();
                    com2.ExecuteNonQuery();
                    con2.Close();
                   
                    //if (checkBox1.Checked)
                    //{
                        
                    //    string InvoiceNo = invoiceNoText.Text;
                    //    PurchasesViewer sendData1 = new PurchasesViewer(InvoiceNo);
                    //    sendData1.ShowDialog();
                    //}

                    this.Controls.Clear();
                    this.InitializeComponent();
                    comboBox1.Text = BranchInfo.BranchID;
                    VoucharNoTxt.Text = BranchInfo.MaxinvoiceNo;
                    AccountIDTxt.Text = BranchInfo.SalesmanId;


                }

                catch (Exception)
                {
                    MessageBox.Show("Invalid Value");
                }

            }
            else
            {
                MessageBox.Show("Check Your Balance");
            }
            treeView1.Nodes.Clear();
            DataTable dts = this.GetData("SELECT HeadOfAccounts as Id, Name FROM HeadOfAccount");
            this.PopulateTreeView(dts, 0, null);
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewBtn_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.InitializeComponent();
            comboBox1.Text = BranchInfo.BranchID;
            VoucharNoTxt.Text = BranchInfo.MaxinvoiceNo;
            AccountIDTxt.Text = BranchInfo.SalesmanId;
            DataTable dts = this.GetData("SELECT HeadOfAccounts as Id, Name FROM HeadOfAccount");
            this.PopulateTreeView(dts, 0, null);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BranchInfo.BranchID = comboBox1.Text;

                SqlConnection con = new SqlConnection(conString);
                con.Open();

                SqlCommand invIdSqlCommand =
                    new SqlCommand(
                        "select MAX(InvoiceId)+1 as ID from AccVNo where Branch='" + (comboBox1.SelectedIndex + 1) +
                        "'and type='JI'", con);
                invIdSqlCommand.ExecuteNonQuery();
                SqlDataReader read = invIdSqlCommand.ExecuteReader();

                while (read.Read())
                {
                    VoucharNoTxt.Text = (read["ID"].ToString());
                    BranchInfo.MaxinvoiceNo = VoucharNoTxt.Text;
                }
                con.Close();


            }
            catch (Exception)
            {
                MessageBox.Show("Please Select Branch");
            }
        }

        public void refreash()
        {
            this.Controls.Clear();
            this.InitializeComponent();
            comboBox1.Text = BranchInfo.BranchID;
            VoucharNoTxt.Text = BranchInfo.MaxinvoiceNo;
            AccountIDTxt.Text = BranchInfo.SalesmanId;
        }

        private void BckBtn_Click(object sender, EventArgs e)
        {
            //int invcID;
            BranchInfo.MaxInvcID = Convert.ToInt32(VoucharNoTxt.Text);
            BranchInfo.MinInvcID = Convert.ToInt32(VoucharNoTxt.Text);
            if ((BranchInfo.MaxInvcID > 19999999 || BranchInfo.MinInvcID <= 10000001) && comboBox1.Text == "Safi 1")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 29999999 || BranchInfo.MinInvcID <= 20000001) && comboBox1.Text == "Safi 2")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 39999999 || BranchInfo.MinInvcID <= 30000001) && comboBox1.Text == "Safi 3")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 49999999 || BranchInfo.MinInvcID <= 40000001) && comboBox1.Text == "Safi 4")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 59999999 || BranchInfo.MinInvcID <= 50000001) && comboBox1.Text == "Safi 5")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 69999999 || BranchInfo.MinInvcID <= 60000001) && comboBox1.Text == "Safi 6")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 79999999 || BranchInfo.MinInvcID <= 70000001) && comboBox1.Text == "Safi 7")
            {
                refreash();
            }
            else
            {

                SqlConnection con = new SqlConnection(conString);
                

                    VoucharNoTxt.Text = (Convert.ToInt32(VoucharNoTxt.Text) - 1).ToString();
                    //invoiceNoText.Text = invcID.ToString();
                    try
                    {

                        
                       
                        con.Open();

                        SqlCommand invIdSqlCommand =new SqlCommand(
                                "select * from HeadingAccounts where VoucharNo ='" + VoucharNoTxt.Text +
                                "'and Vouchartype='JI'", con);
                        invIdSqlCommand.ExecuteNonQuery();
                        SqlDataReader read = invIdSqlCommand.ExecuteReader();

                        while (read.Read())
                        {
                            DBTotalTxt.Text = (read["TotalDebit"].ToString());
                            CRTotalTXT.Text = (read["TotalCredit"].ToString());
                            AccountIDTxt.Text = (read["UserId"].ToString());
                            int ind = Convert.ToInt16(read["Branch"].ToString());
                            comboBox1.SelectedIndex = ind - 1;
                            //comboBox1.SelectedIndex =0;
                            dateTimePicker1.Text = (read["date"].ToString());
                            
                        }
                        con.Close();
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
                        "select AccountId,AccountName,Description,Debit,Credit,HeadId,ParentId,Amount from Accounts where VoucharNo='" +
                        VoucharNoTxt.Text + "'";
                    //initialize a new instance of sqlDataAdapter
                    SqlDataAdapter da = new SqlDataAdapter();
                    //set the sql statement or stored procedure to execute at the data source
                    da.SelectCommand = cmd;
                    //initialize a new instance of DataTable
                    DataTable dt = new DataTable();
                    //add or resfresh rows in the certain range in the datatable to match those in the data source.
                    da.Fill(dt);
                    //set the data source to display the data in the datagridview
                    
                 
                    dataGridView1.Columns[1].DataPropertyName = "AccountId";
                    dataGridView1.Columns[2].DataPropertyName = "AccountName";
                    dataGridView1.Columns[3].DataPropertyName = "Description";
                    dataGridView1.Columns[4].DataPropertyName = "Debit";
                    dataGridView1.Columns[5].DataPropertyName = "Credit";
                    dataGridView1.Columns[6].DataPropertyName = "HeadId";
                    dataGridView1.Columns[7].DataPropertyName = "ParentId";
                    dataGridView1.Columns[8].DataPropertyName = "Amount";
                    dataGridView1.DataSource = dt;
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
        treeView1.Nodes.Clear();
            DataTable dts = this.GetData("SELECT HeadOfAccounts as Id, Name FROM HeadOfAccount");
            this.PopulateTreeView(dts, 0, null);

        }

        private void FrdBtn_Click(object sender, EventArgs e)
        {
            VoucharNoTxt.Text = (Convert.ToInt32(VoucharNoTxt.Text) + 1).ToString();
            BranchInfo.MaxInvcID = Convert.ToInt32(VoucharNoTxt.Text);
            BranchInfo.MinInvcID = Convert.ToInt32(VoucharNoTxt.Text);
            if ((BranchInfo.MaxInvcID > 19999999 || BranchInfo.MinInvcID <= 10000001 ||
                 Convert.ToInt32(BranchInfo.MaxinvoiceNo) <= BranchInfo.MinInvcID) && comboBox1.Text == "Safi 1")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 29999999 || BranchInfo.MinInvcID <= 20000001 ||
                  Convert.ToInt32(BranchInfo.MaxinvoiceNo) <= BranchInfo.MinInvcID) && comboBox1.Text == "Safi 2")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 39999999 || BranchInfo.MinInvcID <= 30000001 ||
                  Convert.ToInt32(BranchInfo.MaxinvoiceNo) <= BranchInfo.MinInvcID) && comboBox1.Text == "Safi 3")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 49999999 || BranchInfo.MinInvcID <= 40000001 ||
                  Convert.ToInt32(BranchInfo.MaxinvoiceNo) <= BranchInfo.MinInvcID) && comboBox1.Text == "Safi 4")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 59999999 || BranchInfo.MinInvcID <= 50000001 ||
                  Convert.ToInt32(BranchInfo.MaxinvoiceNo) <= BranchInfo.MinInvcID) && comboBox1.Text == "Safi 5")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 69999999 || BranchInfo.MinInvcID <= 60000001 ||
                 Convert.ToInt32(BranchInfo.MaxinvoiceNo) <= BranchInfo.MinInvcID) && comboBox1.Text == "Safi 6")
            {
                refreash();
            }
            else if ((BranchInfo.MaxInvcID > 79999999 || BranchInfo.MinInvcID <= 70000001 ||
                  Convert.ToInt32(BranchInfo.MaxinvoiceNo) <= BranchInfo.MinInvcID) && comboBox1.Text == "Safi 7")
            {
                refreash();
            }
            else
            {

                SqlConnection con = new SqlConnection(conString);


               // VoucharNoTxt.Text = (Convert.ToInt32(VoucharNoTxt.Text) + 1).ToString();
                //invoiceNoText.Text = invcID.ToString();
                try
                {



                    con.Open();

                    SqlCommand invIdSqlCommand = new SqlCommand(
                            "select * from HeadingAccounts where VoucharNo ='" + VoucharNoTxt.Text +
                            "'and Vouchartype='JI'", con);
                    invIdSqlCommand.ExecuteNonQuery();
                    SqlDataReader read = invIdSqlCommand.ExecuteReader();

                    while (read.Read())
                    {
                        DBTotalTxt.Text = (read["TotalDebit"].ToString());
                        CRTotalTXT.Text = (read["TotalCredit"].ToString());
                        AccountIDTxt.Text = (read["UserId"].ToString());
                        int ind = Convert.ToInt16(read["Branch"].ToString());
                        comboBox1.SelectedIndex = ind - 1;
                        dateTimePicker1.Text = (read["date"].ToString());

                    }
                    con.Close();
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
                        "select AccountId,AccountName,Description,Debit,Credit,HeadId,ParentId,Amount from Accounts where VoucharNo='" +
                        VoucharNoTxt.Text + "'";
                    //initialize a new instance of sqlDataAdapter
                    SqlDataAdapter da = new SqlDataAdapter();
                    //set the sql statement or stored procedure to execute at the data source
                    da.SelectCommand = cmd;
                    //initialize a new instance of DataTable
                    DataTable dt = new DataTable();
                    //add or resfresh rows in the certain range in the datatable to match those in the data source.
                    da.Fill(dt);
                    //set the data source to display the data in the datagridview


                    dataGridView1.Columns[1].DataPropertyName = "AccountId";
                    dataGridView1.Columns[2].DataPropertyName = "AccountName";
                    dataGridView1.Columns[3].DataPropertyName = "Description";
                    dataGridView1.Columns[4].DataPropertyName = "Debit";
                    dataGridView1.Columns[5].DataPropertyName = "Credit";
                    dataGridView1.Columns[6].DataPropertyName = "HeadId";
                    dataGridView1.Columns[7].DataPropertyName = "ParentId";
                    dataGridView1.Columns[8].DataPropertyName = "Amount";
                    dataGridView1.DataSource = dt;
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
            treeView1.Nodes.Clear();
            DataTable dts = this.GetData("SELECT HeadOfAccounts as Id, Name FROM HeadOfAccount");
            this.PopulateTreeView(dts, 0, null);
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to Update ??", "Confirm OK!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    SqlConnection con1 = new SqlConnection(conString);
                    SqlCommand com1 =
                        new SqlCommand(
                            "Update HeadingAccounts set TotalDebit='" + DBTotalTxt.Text + "',TotalCredit='" + CRTotalTXT.Text + "',UserId='" + AccountIDTxt.Text + "'," +
                            "Branch='" + (comboBox1.SelectedIndex + 1) + "',date='" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "' where VoucharNo='" + VoucharNoTxt.Text + "'",
                            con1);
                    con1.Open();
                    com1.ExecuteNonQuery();
                    con1.Close();
                    for (int r = 0; r < dataGridView1.RowCount - 1; r++)
                    {
                        DataGridViewRow g1 = dataGridView1.Rows[r];

                        SqlConnection con = new SqlConnection(conString);
                        SqlCommand com =
                            new SqlCommand(
                                "update Accounts set AccountId='" + g1.Cells[1].Value + "',AccountName='" +
                                g1.Cells[2].Value + "'," + "Description='" + g1.Cells[3].Value + "',Debit='" + g1.Cells[4].Value + "', Credit='" +
                                g1.Cells[5].Value + "', HeadId='" +
                                g1.Cells[6].Value + "', ParentId='" + g1.Cells[7].Value + "', Amount='" + g1.Cells[8].Value + "' where VoucharNo='" + VoucharNoTxt.Text + "' and AccountId='" + g1.Cells[1].Value + "'", con);
                             con.Open();
                        com.ExecuteNonQuery();
                        
                        con.Close();
                    }
                    this.Controls.Clear();
                    this.InitializeComponent();
                    comboBox1.Text = BranchInfo.BranchID;
                    VoucharNoTxt.Text = BranchInfo.MaxinvoiceNo;
                    AccountIDTxt.Text = BranchInfo.SalesmanId;

                }
                catch (Exception)
                {
                    MessageBox.Show("Please Select Branch");
                }
            }
            treeView1.Nodes.Clear();
            DataTable dts = this.GetData("SELECT HeadOfAccounts as Id, Name FROM HeadOfAccount");
            this.PopulateTreeView(dts, 0, null);
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string gridval = treeView1.SelectedNode.Tag.ToString();
                dataGridView1.CurrentRow.Cells["AccId"].Value = gridval;
            }
            catch (Exception)
            {
                MessageBox.Show("Please Select Row First");
            }
           
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to Delete ??", "Confirm OK!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    SqlConnection con = new SqlConnection(conString);
                    con.Open();
                    SqlCommand cmd1 =
                        new SqlCommand("Delete from HeadingAccounts where VoucharNo='" + VoucharNoTxt.Text + "'", con);
                    SqlCommand cmd = new SqlCommand(
                        "Delete from Accounts where VoucharNo='" + VoucharNoTxt.Text + "'", con);

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
            treeView1.Nodes.Clear();
            DataTable dts = this.GetData("SELECT HeadOfAccounts as Id, Name FROM HeadOfAccount");
            this.PopulateTreeView(dts, 0, null);
        }

        private void CencelBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                dataGridView1.Rows.RemoveAt(rowIndex);
            }
            catch (Exception)
            {
                MessageBox.Show("Nothing to Delete");
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        

    }
}
