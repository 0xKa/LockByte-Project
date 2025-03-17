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
                _filepath = openFileDialog.FileName;

            lblChosenFile.Text = _filepath;

            btnDecrypt.Enabled = true;
            btnEncrypt.Enabled = true;
            btnClear.Visible = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            btnDecrypt.Enabled = false;
            btnEncrypt.Enabled = false;
            btnClear.Visible = false;
            _filepath = null;
            lblChosenFile.Text = string.Empty;
        }
    }
}
