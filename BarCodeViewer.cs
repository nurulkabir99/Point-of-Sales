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
    public partial class BarCodeViewer : Form
    {
        public BarCodeViewer()
        {
           

            InitializeComponent();
            StreamReader reader = new StreamReader("C:\\Safi\\ConString.txt");
           string conString = reader.ReadLine();
            reader.Close();
            BarcodeDT barcodeDetails = new BarcodeDT();
            DataTable dataTable = barcodeDetails.Barcode;
            Barcode Report = new Barcode();

            string branchID=cashPurchase.BranchInfo.BranchID;
            string InvcId=cashPurchase.BranchInfo.InvoiceNo;
            //if (CreditPurchase.BranchInfo.InvType == "CP")
            //{
            //    branchID = CreditPurchase.BranchInfo.BranchID;
            //    InvcId = CreditPurchase.BranchInfo.InvoiceNo;
            //}
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand cmd =
                new SqlCommand("select * from Inventory where Branch='"+branchID+"' and voucharNo='"+InvcId+"' ", con);
            cmd.ExecuteNonQuery();
            SqlDataReader read = cmd.ExecuteReader();
           
            while (read.Read())
            {
               
                int noOfLevel = Convert.ToInt16(read["Quantity"].ToString());
                for (int i = 0; i < noOfLevel; i++)
                {
                    DataRow drow = dataTable.NewRow();
                    drow["ItemName"] = read["ItemName"].ToString();
                    drow["Branch"] = read["Branch"].ToString();
                   drow["SerialNo"] = "*" + read["SerialNo"].ToString() + "*";
                    //drow["SerialNo"] = read["SerialNo"].ToString();
                    dataTable.Rows.Add(drow);
                }

             
               

            }
            con.Close();
            Report.Load(@"C:\Report\Barcode.rpt");
            Report.Database.Tables["Barcode"].SetDataSource((DataTable)dataTable);


            crystalReportViewer1.ReportSource = Report;
            crystalReportViewer1.Refresh();
        }
    }
}
