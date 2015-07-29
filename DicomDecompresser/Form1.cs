/*
 *  Ricky Region Tran 
 *  I don't care, you do whatever you want with this program.
 *  But remember to give thanks to the people at OFFIS
 *  
 *     http://dicom.offis.de/dcmtk.php.en
 *     
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace DicomDecompresser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectInputDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.ShowDialog();
            txtInputDirectory.Text = fb.SelectedPath;
        }

        private void txtInputDirectory_TextChanged(object sender, EventArgs e)
        {
            if (txtInputDirectory.TextLength > 0)
            {
                txtOutputLog.Text += "Set input directory to: " + txtInputDirectory.Text + Environment.NewLine;
                if (txtOutputDirectory.TextLength > 0)
                    btnStart.Enabled = true;
                else
                    btnStart.Enabled = false;
            }
            else
                btnStart.Enabled = false;
        }

        private void btnSelectOutputDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.ShowDialog();
            txtOutputDirectory.Text = fb.SelectedPath;
        }

        private void txtOutputDirectory_TextChanged(object sender, EventArgs e)
        {
            if (txtOutputDirectory.TextLength > 0)
            {
                txtOutputLog.Text += "Set output directory to: " + txtOutputDirectory.Text + Environment.NewLine;
                if (txtInputDirectory.TextLength > 0)
                    btnStart.Enabled = true;
                else
                    btnStart.Enabled = false;
            }
            else
                btnStart.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            IEnumerable<string> pathList = Directory.EnumerateFiles(txtInputDirectory.Text);
            Process p = new Process();
            p.StartInfo.FileName = ".\\Resources\\dcmdjpeg.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            foreach(string path in pathList)
            {
                string outputFullPathFilename = txtOutputDirectory.Text + '\\' + path.Substring(path.LastIndexOf('\\'));
                
                p.StartInfo.Arguments = String.Format("-v \"{0}\" \"{1}\"", path, outputFullPathFilename);
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                txtOutputLog.Text += output + Environment.NewLine;
                p.WaitForExit();
            }
                         
        }

        private void txtOutputLog_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}