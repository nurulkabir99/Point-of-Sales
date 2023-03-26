using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointInvt
{
    public partial class TrialBalanceViewer : Form
    {
        public TrialBalanceViewer(DataTable dtTable,string branchNo)
        {
            InitializeComponent();
            TrialBalance1 cr = new TrialBalance1();



            cr.Load(@"C:\Report\TrialBalance1.rpt");
            cr.SetDataSource(dtTable);
            string txtval = branchNo; 
            cr.SetParameterValue("Branches", txtval);
            //crystalReportViewer1.ParameterFieldInfo["Branches"].CurrentValues.AddValue(txtval);
            crystalReportViewer1.ReportSource = cr;
            crystalReportViewer1.Refresh();
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
