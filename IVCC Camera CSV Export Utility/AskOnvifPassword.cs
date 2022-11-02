using System;
using System.Windows.Forms;

namespace IVCC_Camera_CSV_Export_Utility
{
    public partial class AskOnvifPassword : Form
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public AskOnvifPassword(string _targetIP)
        {
            InitializeComponent();
            lblTargetIP.Text = _targetIP;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != null | txtPassword.Text != null)
            {
                UserName = txtUsername.Text;
                Password = txtPassword.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Username or password cannot be empty!","Invalid Input",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }
    }
}
