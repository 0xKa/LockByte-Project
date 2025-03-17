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

namespace LockByte.Encryption_Decryption
{
    public partial class frmEncryption : Form
    {
        public class OnEncryptionCompletedEventArgs : EventArgs
        { 
            public string originalfilepath { get; }
            public string encryptedfilepath { get; }
            public string key {  get; }

            public OnEncryptionCompletedEventArgs(string originalfilepath, string encryptedfilepath, string key)
            {
                this.originalfilepath = originalfilepath;
                this.encryptedfilepath = encryptedfilepath;
                this.key = key;
            }
        }
        public event EventHandler<OnEncryptionCompletedEventArgs> OnEncryptionCompleted;
        protected virtual void RaiseOnEncryptionCompletedEvent(OnEncryptionCompletedEventArgs e)
        {
            OnEncryptionCompleted.Invoke(this, new OnEncryptionCompletedEventArgs(e.originalfilepath, e.encryptedfilepath, e.key));
        }

        private string _filepath = null;
        public frmEncryption(string filepath)
        {
            InitializeComponent();


            _filepath = filepath;
            lblFileName.Text = Path.GetFileName(_filepath);
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbKey.Text))
            {
                MessageBox.Show("Enter a Password for Encryption.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txbKey.Focus();
                return; 
            }

            using (EncryptionSaveFileDialog)
            {
                
                EncryptionSaveFileDialog.DefaultExt = $".{Path.GetExtension(_filepath)}.lockbyte";
                EncryptionSaveFileDialog.Filter = "Encrypted Files (*.lockbyte)|*.lockbyte|All Files (*.*)|*.*";

                EncryptionSaveFileDialog.FileName = Path.GetFileNameWithoutExtension(_filepath) + EncryptionSaveFileDialog.DefaultExt;

                if (EncryptionSaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string encryptedFilePath = EncryptionSaveFileDialog.FileName;  
                    string inputFile = _filepath;  
                    string key = txbKey.Text.Trim();  

                    clsSymmetricEncryption.EncryptFile(inputFile, encryptedFilePath, key);

                    RaiseOnEncryptionCompletedEvent(new OnEncryptionCompletedEventArgs(inputFile, encryptedFilePath, key));
                    MessageBox.Show("File Encrypted Successfully.", "Encryption Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
