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
using static LockByte.clsUtil;

namespace LockByte.Encryption_Decryption
{
    public partial class frmDecryption : Form
    {
        public event EventHandler<OnDecryptionCompletedEventArgs> OnDecryptionCompleted;
        protected virtual void RaiseOnDecryptionCompletedEvent(OnDecryptionCompletedEventArgs e)
        {
            OnDecryptionCompleted.Invoke(this, new OnDecryptionCompletedEventArgs(e.encryptedfilepath, e.decryptedfilepath, e.key));
        }

        private string _encryptedfilepath = null;
        public frmDecryption(string EncryptedFilePath)
        {
            InitializeComponent();

            _encryptedfilepath = EncryptedFilePath;
            lblFileName.Text = Path.GetFileName(_encryptedfilepath);
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbKey.Text))
            {
                MessageBox.Show("Enter a Password for Decryption.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txbKey.Focus();
                return;
            }
            
               
            
            using (DecryptionSaveFileDialog)
            {
                DecryptionSaveFileDialog.DefaultExt = $".{GlobalEncryptionExtension}.decrypted";
                DecryptionSaveFileDialog.Filter = $"Encrypted Files (*{GlobalEncryptionExtension})|*{GlobalEncryptionExtension}|All Files (*.*)|*.*";

                DecryptionSaveFileDialog.FileName = Path.GetFileNameWithoutExtension(_encryptedfilepath);

                if (DecryptionSaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string encryptedFilePath = _encryptedfilepath;
                    string decryptedFilePath = DecryptionSaveFileDialog.FileName;
                    string key = txbKey.Text.Trim();

                    if (clsSymmetricEncryption.DecryptFile(encryptedFilePath, decryptedFilePath, key))
                    {
                        RaiseOnDecryptionCompletedEvent(new OnDecryptionCompletedEventArgs(encryptedFilePath, decryptedFilePath, key));
                        MessageBox.Show("File Decrypted Successfully.", "Decryption Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                        MessageBox.Show("Decryption Failed! Please enter the correct key and try again.", "Decryption Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

        }
    }
}
