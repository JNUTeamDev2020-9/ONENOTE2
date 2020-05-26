using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONENOTE2
{
    public partial class UrlForm : Form
    {
        public UrlForm()
        {
            InitializeComponent();
        }

        private void urlok_button_Click(object sender, EventArgs e)//确认事件
        {
            Close();
        }

        private void urlcancel_button_Click(object sender, EventArgs e)//取消事件
        {
            this.Close();
        }
    }
}
