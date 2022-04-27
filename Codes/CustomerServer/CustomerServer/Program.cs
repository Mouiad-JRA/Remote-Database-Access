using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using Interface_CustomerInfo;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
namespace CustomerServer
{
    public class CUSTOMER_SERVER1 : MarshalByRefObject, ICustomerInfo
    {
        private SqlConnection myConnection = null;
        private SqlDataReader myReader;
        public CUSTOMER_SERVER1() { }
        public void Init(string userid, string password)
        {
            try
            {
                MessageBox.Show("COMES HERE");
                string myConnectString = "user id=" + "sa" + ";password=" + "1234" + ";Database=NORTHWND;Server=DESKTOP-IIRMO93\\ABDULLAHSQL;Connect Timeout=30";
                myConnection = new SqlConnection(myConnectString);
                myConnection.Open();
                if (myConnection == null)
                {
                    Console.WriteLine("OPEN NULL VALUE =====================");
                    return;
                }
            }
            catch (Exception es)
            {
                Console.WriteLine("[Error WITH DB CONNECT...] " + es.Message);
            }
        }
        public bool ExecuteSelectCommand(string selCommand)
        {
            try
            {
                Console.WriteLine("EXECUTING .. " + selCommand);
                SqlCommand myCommand = new SqlCommand(selCommand);
                if (myConnection == null)
                {
                    Console.WriteLine("NULL VALUE =====================");
                    return false;
                }
                myCommand.Connection = myConnection;
                myCommand.ExecuteNonQuery();
                myReader = myCommand.ExecuteReader();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public string GetRow()
        {
            if (!myReader.Read())
            {
                myReader.Close();
                return "";
            }
            int nCol = myReader.FieldCount;
            string outstr = "";
            object[] values = new Object[nCol];
            myReader.GetValues(values);
            for (int i = 0; i < values.Length; i++)
            {
                string coldata = values[i].ToString();
                coldata = coldata.TrimEnd();
                outstr += coldata + ",";
            }
            return outstr;
        }
    }
    ///  
    /// Summary description for Form1.  
    ///  
    public class Form1 : System.Windows.Forms.Form
    {
        public System.Windows.Forms.TextBox textBox1;
        ///  
        /// Required designer variable.  
        ///  
        private System.ComponentModel.Container components = null;
        public Form1()
        {
            //  
            // Required for Windows Form Designer support  
            //  
            InitializeComponent();
            //  
            // TODO: Add any constructor code after InitializeComponent call  
            //  
        }
        ///  
        /// Clean up any resources being used.  
        ///  
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code  
        ///  
        /// Required method for Designer support - do not modify  
        /// the contents of this method with the code editor.  
        ///  
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            //  
            // textBox1  
            //  
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
           // this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25 F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(288, 85);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "textBox1";
            //  
            // Form1  
            //  
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(288, 85);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.textBox1 });
            this.Name = "Form1";
            this.Text = "Interface_Server";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
        }
        #endregion
        ///  
        /// The main entry point for the application.  
        ///  
        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }
        private void Form1_Load(object sender, System.EventArgs e)
        {
            TcpServerChannel tsc = new TcpServerChannel(8228);
            ChannelServices.RegisterChannel(tsc);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(CUSTOMER_SERVER1), "CUSTOMER_SERVER2", WellKnownObjectMode.Singleton);
            textBox1.Text = "SERVER RUNNING ..";
        }
    }
}