using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIA;
using System.Runtime.InteropServices;

namespace PointInvt
{
    public partial class ListofScanImage : Form
    {
        public ListofScanImage()
        {
            InitializeComponent();
            conString = Login.LoginInfo.conString;
            //SqlConnection con = new SqlConnection(conString);
            //con.Open();
        }

        public static string conString;
        private void ScanImage_Load(object sender, EventArgs e)
        {
            try
            {
                var devicemanager= new DeviceManager();
                for (int i = 1; i <= devicemanager.DeviceInfos.Count; i++)
                {
                    if (devicemanager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                    {
                        continue;
                    }
                    listBox1.Items.Add(devicemanager.DeviceInfos[i].Properties["Name"].get_Value());
                }
            }
            catch (COMException ex)
            {
                MessageBox.Show(ex.Message);
            }

            

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select CID,Contact,Model,IMEI from civilID";

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.Columns[0].DataPropertyName = "CID";
            dataGridView1.Columns[1].DataPropertyName = "Contact";
            dataGridView1.Columns[2].DataPropertyName = "Model";
            dataGridView1.Columns[3].DataPropertyName = "IMEI";
            

            dataGridView1.DataSource = dt;
            

        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }


        private string Path;
        private string Branch = Login.LoginInfo.branchID;
        private void FrontImageTxt_Click(object sender, EventArgs e)
        {
            try
            {
                var devicemanager = new DeviceManager();
                DeviceInfo avaDeviceInfo = null;

                for (int i = 1; i <= devicemanager.DeviceInfos.Count; i++)
                {
                    if (devicemanager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                    {
                        continue;
                    }
                    avaDeviceInfo = devicemanager.DeviceInfos[i];
                    break;
                    

                }
                var device = avaDeviceInfo.Connect();
                var scanItem = device.Items[1];
                
               
                var imgFile = (ImageFile)scanItem.Transfer(FormatID.wiaFormatJPEG);
                Path = @"\\195.39.153.188\Image\" + CivilIDTxt.Text + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss-fffffff") + ".jpg";
                imgFile.SaveFile(Path);
                pictureBox1.ImageLocation = Path;
                SqlConnection con = new SqlConnection(conString);
                SqlCommand command = new SqlCommand("INSERT INTO CivilID (Date,CID,Contact,Price,Model,IMEI,Gaurantee,Branch,Location) VALUES('" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "','" + CivilIDTxt.Text + "','" + contactTxt.Text + "','" + priceTxt.Text + "','" + modelTxt.Text + "','" + IMEITxt.Text + "','" + graunteeTxt.Text + "','" + BranchTxt.Text + "','" + Path + "')", con);
                // command.Parameters.AddWithValue("@image", arr);
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
                avaDeviceInfo.DeviceID.Remove(1);
            }
            catch (COMException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Path = @"\\195.39.153.188\Image\" + CivilIDTxt.Text + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss-fffffff") + ".jpg";
            //pictureBox1.Image = Image.FromFile(Path);
        
            Bitmap img = new Bitmap(pictureBox1.Image);
            img.Save(Path);
            SqlConnection con = new SqlConnection(conString);
            SqlCommand command = new SqlCommand("INSERT INTO CivilID (Date,CID,Contact,Price,Model,IMEI,Gaurantee,Branch,Location) VALUES('" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "','" + CivilIDTxt.Text + "','" + contactTxt.Text + "','" + priceTxt.Text + "','" + modelTxt.Text + "','" + IMEITxt.Text + "','" + graunteeTxt.Text + "','" + BranchTxt.Text + "','" + Path + "')", con);
            con.Open();
            command.ExecuteNonQuery();
            con.Close();
            this.Close();
            ListofScanImage listofScan = new ListofScanImage();
            listofScan.Show();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    Image img = resizeImage(pictureBox1.Image, new Size(300, 300));

        //    // Image img = pictureBox1.Image;
        //    img = new Bitmap(img);
        //    byte[] arr;
        //    ImageConverter converter = new ImageConverter();
        //    arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
        //    SqlConnection con = new SqlConnection(conString);
        //    SqlCommand command = new SqlCommand("INSERT INTO CivilID (Date,CID,Contact,Price,Model,IMEI,Gaurantee,Photo) VALUES('" + dateTimePicker1.Value.Date + "','" + CivilIDTxt.Text + "','" + contactTxt.Text + "','" + priceTxt.Text + "','" + modelTxt.Text + "','" + IMEITxt.Text + "','" + graunteeTxt.Text + "',@image)", con);
        //    command.Parameters.AddWithValue("@image", arr);
        //    con.Open();
        //    command.ExecuteNonQuery();
        //    con.Close();
        //    this.Close();
        //    ListofScanImage listofScan = new ListofScanImage();
        //    listofScan.Show();
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select CID,Contact,Model,IMEI from civilID WHERE date between '" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker3.Value.Date.ToString("yyyy-MM-dd") + "'";

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.Columns[0].DataPropertyName = "CID";
            dataGridView1.Columns[1].DataPropertyName = "Contact";
            dataGridView1.Columns[2].DataPropertyName = "Model";
            dataGridView1.Columns[3].DataPropertyName = "IMEI";


            dataGridView1.DataSource = dt;
        
        }
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }

              
       

        private void CivilIDTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SqlConnection con = new SqlConnection(conString);

                using (SqlCommand cmd = new SqlCommand("select * from civilID where Cid = '" + CivilIDTxt.Text + "'", con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            contactTxt.Text = (reader["Contact"].ToString());
                            priceTxt.Text = (reader["Price"].ToString());
                            modelTxt.Text = (reader["Model"].ToString());
                            IMEITxt.Text = (reader["IMEI"].ToString());
                            graunteeTxt.Text = (reader["Gaurantee"].ToString());
                            BranchTxt.Text = (reader["Branch"].ToString());
                            dateTimePicker1.Text = (reader["date"].ToString());
                            Path = (reader["Location"].ToString());
                            
                        }
                    }
                    con.Close();
                    pictureBox1.Image = Image.FromFile(Path);
                }
            }
        }

        private void IMEITxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SqlConnection con = new SqlConnection(conString);

                using (SqlCommand cmd = new SqlCommand("select * from civilID where IMEI = '" + IMEITxt.Text + "'", con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            CivilIDTxt.Text = (reader["CID"].ToString());
                            contactTxt.Text = (reader["Contact"].ToString());
                            priceTxt.Text = (reader["Price"].ToString());
                            modelTxt.Text = (reader["Model"].ToString());
                            IMEITxt.Text = (reader["IMEI"].ToString());
                            graunteeTxt.Text = (reader["Gaurantee"].ToString());
                            BranchTxt.Text = (reader["Branch"].ToString());
                            dateTimePicker1.Text = (reader["date"].ToString());
                            Path = (reader["Location"].ToString());
                            //pictureBox1.Image = ByteArrayToImage((byte[])(reader.GetValue(8)));
                        }
                    }
                    con.Close();
                    pictureBox1.Image = Image.FromFile(Path);
                }
            }
        }
        
        //private void BackIamgeTxt_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var devicemanager = new DeviceManager();
        //        DeviceInfo avaDeviceInfo = null;

        //        for (int i = 1; i <= devicemanager.DeviceInfos.Count; i++)
        //        {
        //            if (devicemanager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
        //            {
        //                continue;
        //            }
        //            avaDeviceInfo = devicemanager.DeviceInfos[i];
        //            break;


        //        }
        //        var device = avaDeviceInfo.Connect();
        //        var scanItem = device.Items[1];
        //       var imgFile = (ImageFile)scanItem.Transfer(FormatID.wiaFormatJPEG);
        //        var Path = @"D:\" + CivilIDTxt.Text + "" + "B.jpg";
        //        if (File.Exists(Path))
        //        {
        //            File.Delete(Path);
        //        }
        //        imgFile.SaveFile(Path);
        //        pictureBox2.ImageLocation = Path;
        //        avaDeviceInfo.DeviceID.Remove(1);
        //    }
        //    catch (COMException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        
    }
}
