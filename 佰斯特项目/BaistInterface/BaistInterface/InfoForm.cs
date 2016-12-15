using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaistInterface
{
    public partial class InfoForm : Form
    {
        public List<string> Infos { get; set; }

        public InfoForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InfoForm_Load(object sender, EventArgs e)
        {
            foreach (var value in Infos)
            {
                richTextBoxInfo.AppendText(value + "\r\n");
            }
        }
    }
}
