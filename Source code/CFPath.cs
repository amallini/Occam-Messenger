using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OccamMsgr
{
    public partial class CFPath : Form
    {
        public CFPath()
        {
            // управление языком интерфейса
            if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
            }
            InitializeComponent();
        }

        private void BSelectFile2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                TCFPath.Text = FBD.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
