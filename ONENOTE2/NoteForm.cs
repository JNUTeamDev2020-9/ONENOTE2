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
    public partial class NodeForm : Form
    {
        public static NodeForm nodeForm;
        public NodeForm()
        {
            InitializeComponent();
            nodeForm = this;
        }
       
    }
}
