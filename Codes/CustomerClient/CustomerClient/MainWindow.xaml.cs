using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using Interface_CustomerInfo;
namespace CustomerClient
{
    ///  
    /// Summary description for Form1.  
    ///  
    public class Form1 : System.Windows.Forms.Form
    {
        ICustomerInfo custl;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //  
            // textBox1  
            //  
            this.textBox1.Location = new System.Drawing.Point(0, 32);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(736, 100);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "";
            //  
            // button1  
            //  
            this.button1.Location = new System.Drawing.Point(360, 150);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "Run SQL";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            //  
            // textBox2  
            //  
            this.textBox2.Location = new System.Drawing.Point(100, 248);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(500, 50);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "";
            //  
            // button2  
            //  
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(192, 150);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(130, 30);
            this.button2.TabIndex = 1;
            this.button2.Text = "Press to get the Next data Row";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            //  
            // label1  
            //  
            this.label1.Location = new System.Drawing.Point(270, 232);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Here you can Type SQL Command";
            //  
            // label2  
            //  
            this.label2.Location = new System.Drawing.Point(220, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(300, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "The Returned Data From the Remote Object separated by commas ";
            //  
            // Form1  
            //  
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(736, 269);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.label2, this.label1, this.textBox2, this.button1, this.textBox1, this.button2 });
            this.Name = "Form1";
            this.Text = "Abullah, Mouiad and Mazen Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
        }
        #endregion
        ///  
        /// The main entry point for the application.  
        ///  
        [STAThread]
        private static void Main()
        {
            Application.Run(new Form1());
        }
        private void Form1_Load(object sender, System.EventArgs e)
        {
            ChannelServices.RegisterChannel(new TcpClientChannel());
            custl = (ICustomerInfo)Activator.GetObject(typeof(ICustomerInfo), "tcp://localhost:8228/CUSTOMER_SERVER2");
            if (custl == null)
            {
                Console.WriteLine("SERVER IS OFFLINE NOW, ...PLEASE TRY AGAIN LATER");
                return;
            }
            custl.Init("=DESKTOP-IIRMO93\\ABDULLAHSQL", "");
        }
        private void button2_Click(object sender, System.EventArgs e)
        {
            textBox1.Text = custl.GetRow();
            if (textBox1.Text == "")
                button2.Enabled = false;
        }
        private void button1_Click(object sender, System.EventArgs e)
        {
            button2.Enabled = false;
            textBox1.Text = "";
            if (textBox2.Text == "")
            {
                MessageBox.Show("Enter a SQL Command", "Error");
                return;
            }
            bool ret = custl.ExecuteSelectCommand(textBox2.Text);
            if (!ret)
            {
                textBox1.Text = " Some Error Happend while Executing Your SQL command / There is no rows returned ";
                return;
            }
            button2.Enabled = true;
            textBox1.Text = custl.GetRow();
        }
    }
}