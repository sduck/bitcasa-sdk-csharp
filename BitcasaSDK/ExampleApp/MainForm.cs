using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using BitcasaSdk;
using BitcasaSdk.Dao;
using BitcasaSDK.Exception;
using ExampleApp.Properties;

namespace ExampleApp
{
    public partial class MainForm : Form
    {
        private BitcasaClient _client;

        #region Properties

        private string ClientId
        {
            get
            {
                return Settings.Default.clientId;
            }
        }

        private string ClientSecret
        {
            get
            {
                return Settings.Default.clientSecret;
            }
        }

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            InitClient(false);
        }

        private void btnClientInfo_Click(object sender, EventArgs e)
        {
            InitClient(true);
        }

        private void InitClient(bool forceDialog)
        {
            if (forceDialog || String.IsNullOrEmpty(ClientId) || String.IsNullOrEmpty(ClientSecret))
            {
                var clientInfoForm = new ClientInfo();
                clientInfoForm.ShowDialog();
            }

            _client = new BitcasaClient(ClientId, ClientSecret);

            txtAuthUrl.Text = _client.GetAuthenticateUrl();
        }

        private async Task<bool> Authenticate()
        {
            if (null == _client.AccessToken)
            {
                lblStatus.Text = @"Authenticating...";
                try
                {
                    await _client.RequestAccessToken(txtAuthCode.Text);
                }
                catch (BitcasaSdkAuthenticationException ex)
                {
                    lblStatus.Text = String.Format(@"Authenticating...failed: {0}", ex.Message);

                    return false;
                }
            }

            lblStatus.Text = @"Authenticating... done";

            return true;
        }

        private async void LoadFolders(Folder folder)
        {
            btnLoad.Enabled = false;

            
            if (!await Authenticate())
            {
                btnLoad.Enabled = true;
                return;
            }

            lblStatus.Text = @"Loading folder list...";
            var folders = await _client.GetItemsInFolder(folder);

            lstResult.Items.Add(String.Format("----- START ({0} folders) -----", folders.Count));
            foreach (var fldr in folders)
            {
                lstResult.Items.Add(fldr);
            }
            lstResult.Items.Add(String.Format("----- END ({0} folders) -----", folders.Count));

            lblStatus.Text = @"Done";
            btnLoad.Enabled = true;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtAuthCode.Text))
            {
                return;
            }

            LoadFolders(null);
        }

        private void lstResult_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var index = lstResult.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                var item = lstResult.Items[index] as Folder;
                if (null != item)
                {
                    LoadFolders(item);
                }
            }
        }
    }
}
