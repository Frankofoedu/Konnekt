using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KONNEKT_V2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {            
              
                foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
                {
                   if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                    {
                        MessageBox.Show("wire");
                        if (netInterface.OperationalStatus == OperationalStatus.Dormant)
                        {
                            var proc = new Process();
                            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            proc.StartInfo.CreateNoWindow = true;
                            proc.StartInfo.FileName = "netsh";
                            proc.StartInfo.Arguments = "wlan start hostednetwork";
                            proc.StartInfo.RedirectStandardOutput = true;
                            proc.StartInfo.UseShellExecute = false;
                            proc.Start();
                            lblStatus.Text = Properties.Settings.Default.NetworkName + " Switched ON";
                            return;
                            
                        }
                        else if (netInterface.OperationalStatus == OperationalStatus.Down)
                        {
                            MessageBox.Show("please turn on wifi");
                            return;
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            //  TextBox1.Text = (proc.StandardOutput.ReadToEnd);
            // Button3.Enabled = True;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var proc = new Process();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.FileName = "netsh";
                proc.StartInfo.Arguments = "wlan stop hostednetwork";
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
                lblStatus.Text = Properties.Settings.Default.NetworkName + " Switched OFF";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPass.Text)
                || string.IsNullOrEmpty(txtConfPass.Text))
            {
                MessageBox.Show(panel2, "Please fill all fields", "Incomplete Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!string.Equals(txtPass.Text.Trim(), txtConfPass.Text.Trim()))
                {
                    MessageBox.Show(panel2, "Password does not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (txtPass.Text.Trim().Length < 8)
                    {
                        MessageBox.Show(panel2, "Password must be more than 8 characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        Properties.Settings.Default.NetworkName = txtName.Text;
                        Properties.Settings.Default.Pasword = txtPass.Text;

                        var proc = new Process();
                        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        proc.StartInfo.CreateNoWindow = true;
                        proc.StartInfo.FileName = "netsh";
                        proc.StartInfo.Arguments = "wlan set hostednetwork mode=allow ssid=" + Properties.Settings.Default.NetworkName + " key=" + Properties.Settings.Default.Pasword;
                        proc.StartInfo.RedirectStandardOutput = true;
                        proc.StartInfo.UseShellExecute = false;
                        proc.Start();
                         MessageBox.Show(panel2, "Network name saved", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        panel2.Visible = false;
                        
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtPass.Text = "";
            txtConfPass.Text = "";
            panel2.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
