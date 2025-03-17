using LockByte.Encryption_Decryption;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockByte
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void _ConfigureFileDialog()
        {
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "All Files (*.*)|*.*|Text Files (*.txt)|*.txt|PDF Files (*.pdf)|*.pdf|PNG Files (*.png)|*.png";
            openFileDialog.Title = "Select a File to Encrypt/Decrypt";
            openFileDialog.Multiselect = false; 


        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _ConfigureFileDialog();
        }

        private string _filepath = null;
        private void btnBrowse_Click(object sender, EventArgs e)
        {

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _filepath = openFileDialog.FileName;

                lblChosenFile.Text = _filepath;

                btnDecrypt.Visible = true;
                btnEncrypt.Visible = true;
                btnClear.Visible = true;
                lblMessage.Text = string.Empty;

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _filepath = null;
            
            btnDecrypt.Visible = false;
            btnEncrypt.Visible = false;
            btnClear.Visible = false;
            
            lblMessage.Text = string.Empty;
            lblChosenFile.Text = string.Empty;
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            frmEncryption frmE = new frmEncryption(_filepath);
            frmE.OnEncryptionCompleted += FrmEncryption_OnEncryptionCompleted;
            frmE.ShowDialog();
        }
        private void FrmEncryption_OnEncryptionCompleted(object sender, clsGlobalSettings.OnEncryptionCompletedEventArgs e)
        {
            lblMessage.Visible = true;
            lblMessage.Text = $"Selected File has been Encrypted Successfully.\nEncrypted File Path: {e.encryptedfilepath}";
            btnDecrypt.Visible = false;
            btnEncrypt.Visible = false;
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (!clsGlobalSettings.IsValidExtension(_filepath))
            {
                MessageBox.Show("File Extension Invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmDecryption frmD = new frmDecryption(_filepath);
            frmD.OnDecryptionCompleted += frmDecryption_OnDecryptionCompleted;
            frmD.ShowDialog();
        }
        private void frmDecryption_OnDecryptionCompleted(object sender, clsGlobalSettings.OnDecryptionCompletedEventArgs e)
        {
            lblMessage.Visible = true;
            lblMessage.Text = $"Selected File has been Decrypted Successfully.\nDecrypted File Path: {e.decryptedfilepath}";
            btnDecrypt.Visible = false;
            btnEncrypt.Visible = false;
        }
    }
}
