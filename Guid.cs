using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.Windows.Forms;

namespace PyCode
{
    public partial class wdwGuid : MetroForm
    {
        public wdwGuid()
        {
            InitializeComponent();
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            key.Text = System.Guid.NewGuid().ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(key.Text);
        }

        private void wdwGuid_Load(object sender, EventArgs e)
        {
            btnBuild.PerformClick();
            key.Select(key.Text.Length, key.Text.Length);
        }
    }
}
