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
    public partial class Tip : Form
    {
        public Tip(String typeTip)//同构函数
        {
            InitializeComponent();
            this.tip_label.Text = typeTip;
            
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sure_button_Click(object sender, EventArgs e)
        {
            
            Form1.nodeName = name_textBox.Text;                    //将文本框的内容赋值给变量
            Close();
        }
    }
}
