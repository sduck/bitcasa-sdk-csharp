using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ExampleApp
{
    public partial class ClientInfo : Form
    {
        public ClientInfo()
        {
            InitializeComponent();
        }

        private void textBox_Validating(object sender, CancelEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox == null)
            {
                return;
            }

            errorProvider.SetError(textBox,
                String.IsNullOrEmpty(textBox.Text)
                    ? String.Format("{0} must be filled in", textBox.Text.Replace(":", ""))
                    : "");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtClientId.Text) || String.IsNullOrEmpty(txtClientSecret.Text))
            {
                return;
            }

            Properties.Settings.Default.clientId = txtClientId.Text;
            Properties.Settings.Default.clientSecret = txtClientSecret.Text;
            Properties.Settings.Default.Save();
        }
    }
}
