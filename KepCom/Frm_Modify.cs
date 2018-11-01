using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KepCom
{
    public partial class Frm_Modify : Form
    {
        public Frm_Modify(string value)
        {
            InitializeComponent();
            this.txt_initial.Text = value;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            string res = string.Empty;
            if (chk_Async.Checked)
            {
                res = this.txt_modify.Text.Trim() + "|" + "1";
            }
            else
            {
                res = this.txt_modify.Text.Trim() + "|" + "0";
            }

            this.Tag = res;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
