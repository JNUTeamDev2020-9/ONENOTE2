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

namespace ONENOTE2
{

    public partial class Form1 : Form
    {
        public static Form1 form1;
        public static String nodeName;
        public float X;//当前窗体的宽度
        public float Y;//当前窗体的高度
        public int len = 0;
        public int curRtbStart = 0;
        Boolean boldbool = false,underlinebool=false, inclinesbool=false;
        public Form1()
        {
            InitializeComponent();
            form1 = this;
            
        }
        
        /// <summary>
        /// 将控件的宽，高，左边距，顶边距和字体大小暂存到tag属性中
        /// </summary>
        /// <param name="cons">递归控件中的控件</param>
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
            
        }
        /*
        //根据窗体大小调整控件大小
        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            //cons.Controls.Add(NodeForm.nodeForm.edit_richTextBox);//将富文本框控件加入
            foreach (Control con in cons.Controls)
            {

                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });  //获取控件的Tag属性值，并分割后存储字符串数组
                float a = System.Convert.ToSingle(mytag[0]) * newx;               //根据窗体缩放比例确定控件的值，宽度
                con.Width = (int)a;//宽度
                a = System.Convert.ToSingle(mytag[1]) * newy;//高度
                con.Height = (int)(a);
                a = System.Convert.ToSingle(mytag[2]) * newx;//左边距离
                con.Left = (int)(a);
                a = System.Convert.ToSingle(mytag[3]) * newy;//上边缘距离
                con.Top = (int)(a);
               // Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                //con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }

            }
        }
        */
        private void Form1_Load(object sender, EventArgs e)
        {
            list_treeView.LabelEdit = true;        //树目录为可编辑状态
            X = this.Width;//获取窗体的宽度
            Y = this.Height;//获取窗体的高度
            //setTag(this);//调用方法
            loadTreeView();
            ColumnHeader ch = new ColumnHeader();
            ch.Text = "查询结果";
            this.listView_seek.Columns.Add(ch);
        }
        private void loadTreeView() {
            KBM = FileManagement.loadKnowledgeBaseManagement();
            List<KnowledgeBase> kbs = KBM.getKBS();
            foreach (KnowledgeBase kb in kbs)
            {
                TreeNode treeNode = addRootNode(kb.getName());
                List<Note> notes = kb.GetNotes();
                foreach (Note note in notes)
                {
                    addSonNode(treeNode, note.getName());

                }
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / X;                  //窗体宽度缩放比例
            float newy = (this.Height) / Y;                 //窗体高度缩放比例
            //setControls(newx, newy, this);                  //随窗体改变控件大小
        }
        private void ChangFontSize(float fontSize)           //改变字体大小，第一个参数为字号大小
        {
            
            if (fontSize <= 0.0)
                throw new InvalidProgramException("字号参数错误");
            curRtbStart = NodeForm.nodeForm.edit_richTextBox.SelectionStart;
            len = NodeForm.nodeForm.edit_richTextBox.SelectionLength;
            RichTextBox tempRichTextBox = new RichTextBox();
            int tempRtbStart = 0;
            Font font = NodeForm.nodeForm.edit_richTextBox.SelectionFont;
            if (len <= 1 && font != null)
            {
                NodeForm.nodeForm.edit_richTextBox.SelectionFont = new Font(font.Name, fontSize, font.Style);
                return;
            }
            tempRichTextBox.Rtf = NodeForm.nodeForm.edit_richTextBox.SelectedRtf;
            for (int i = 0; i < len; i++)
            {
                tempRichTextBox.Select(tempRtbStart + i, 1);
                tempRichTextBox.SelectionFont = new Font(tempRichTextBox.SelectionFont.Name, fontSize, tempRichTextBox.SelectionFont.Style);
            }
            tempRichTextBox.Select(tempRtbStart, len);
            NodeForm.nodeForm.edit_richTextBox.SelectedRtf = tempRichTextBox.SelectedRtf;
            NodeForm.nodeForm.edit_richTextBox.Select(curRtbStart, len);
            NodeForm.nodeForm.edit_richTextBox.Focus();
        }
        private void SetFontStyle(FontStyle fontstyle)//设置字体粗体斜体下划线
        {
            if (fontstyle != FontStyle.Bold && fontstyle != FontStyle.Italic && fontstyle != FontStyle.Underline && fontstyle != FontStyle.Strikeout && fontstyle != FontStyle.Regular)
                throw new System.InvalidProgramException("字体格式错误");
            RichTextBox tempRichTextBox = new RichTextBox();
            int tempRtbStart = 0;
            Font font = NodeForm.nodeForm.edit_richTextBox.SelectionFont;

            if (len <= 1 && font != null)
            {
                NodeForm.nodeForm.edit_richTextBox.SelectionFont = new Font(font, font.Style | fontstyle);
                return;
            }
            tempRichTextBox.Rtf = NodeForm.nodeForm.edit_richTextBox.SelectedRtf;
            for (int i = 0; i < len; i++)
            {
                tempRichTextBox.Select(tempRtbStart + i, 1);
                tempRichTextBox.SelectionFont =
                        new Font(tempRichTextBox.SelectionFont,
                            tempRichTextBox.SelectionFont.Style | fontstyle);
            }
            tempRichTextBox.Select(tempRtbStart, len);
            NodeForm.nodeForm.edit_richTextBox.SelectedRtf = tempRichTextBox.SelectedRtf;
            NodeForm.nodeForm.edit_richTextBox.Select(curRtbStart, len);
            NodeForm.nodeForm.edit_richTextBox.Focus();
        }
        private void RemoveFontStyle(FontStyle fontstyle)//取消字体粗体斜体下划线
        {
            if (fontstyle != FontStyle.Bold && fontstyle != FontStyle.Italic && fontstyle != FontStyle.Underline && fontstyle != FontStyle.Strikeout && fontstyle != FontStyle.Regular)
                throw new System.InvalidProgramException("字体格式错误");
            RichTextBox tempRichTextBox = new RichTextBox();
            int tempRtbStart = 0;
            Font font = NodeForm.nodeForm.edit_richTextBox.SelectionFont;

            if (len <= 1 && font != null)
            {
                NodeForm.nodeForm.edit_richTextBox.SelectionFont = new Font(font, font.Style ^ fontstyle);
                return;
            }
            tempRichTextBox.Rtf = NodeForm.nodeForm.edit_richTextBox.SelectedRtf;
            for (int i = 0; i < len; i++)
            {
                tempRichTextBox.Select(tempRtbStart + i, 1);
                tempRichTextBox.SelectionFont = new Font(tempRichTextBox.SelectionFont,
                     tempRichTextBox.SelectionFont.Style ^ fontstyle);
            }
            tempRichTextBox.Select(tempRtbStart, len);
            NodeForm.nodeForm.edit_richTextBox.SelectedRtf = tempRichTextBox.SelectedRtf;
            NodeForm.nodeForm.edit_richTextBox.Select(curRtbStart, len);
            NodeForm.nodeForm.edit_richTextBox.Focus();
        }
        private void ChangeFont(String fontName)// 改变字体
        {
            if (fontName == "")
                throw new System.InvalidProgramException("字体参数错误");
            curRtbStart = NodeForm.nodeForm.edit_richTextBox.SelectionStart;
            len = NodeForm.nodeForm.edit_richTextBox.SelectionLength;

            RichTextBox tempRichTextBox = new RichTextBox();
            int tempRtbStart = 0;
            Font font = NodeForm.nodeForm.edit_richTextBox.SelectionFont;
            if (len <= 1 && font != null)
            {
                NodeForm.nodeForm.edit_richTextBox.SelectionFont = new Font(fontName, font.Size, font.Style);
                return;
            }
            tempRichTextBox.Rtf = NodeForm.nodeForm.edit_richTextBox.SelectedRtf;
            for (int i = 0; i < len; i++)
            {
                tempRichTextBox.Select(tempRtbStart + i, 1);
                tempRichTextBox.SelectionFont = new Font(fontName, tempRichTextBox.SelectionFont.Size, tempRichTextBox.SelectionFont.Style);
            }
            tempRichTextBox.Select(tempRtbStart, len);
            NodeForm.nodeForm.edit_richTextBox.SelectedRtf = tempRichTextBox.SelectedRtf;
            NodeForm.nodeForm.edit_richTextBox.Select(curRtbStart, len);
            NodeForm.nodeForm.edit_richTextBox.Focus();
        }


        private void ClosePreForm()
        {
            foreach (Control item in this.note_tabControl.Controls)
            {
                if (item is Form)
                {
                    Form objControl = (Form)item;
                    objControl.Close();
                }

            }
        }
        private void OpenForm(Form objFrm, String name)
        {
                                                                     //嵌入子窗体到父窗体中，把添加学员信息嵌入到主窗体右侧
            objFrm.TopLevel = false;                                //将子窗体设置成非最高层，非顶级控件
            objFrm.WindowState = FormWindowState.Maximized;        //将当前窗口设置成最大化
            objFrm.FormBorderStyle = FormBorderStyle.None;        //去掉窗体边框
            this.note_tabControl.TabPages.Add(name);             //添加一个tabpage
            this.note_tabControl.SelectTab((int)(this.note_tabControl.TabPages.Count - 1));   //将当前选项选中为新添的选项
            objFrm.Parent = this.note_tabControl.SelectedTab;     //指定子窗体显示的容器
            objFrm.Show();
        }

        private void label1_Click(object sender, EventArgs e)//添加知识库
        {
            Tip knowbank = new Tip("请输入知识库名称");
            knowbank.ShowDialog();
            if (nodeName == null)
                nodeName = "";
            if (nodeName.Equals(""))
                return;
            addRootNode(nodeName);
        }

        private void addnotepage_label_Click(object sender, EventArgs e)//添加笔记页
        {
           /* Tip notepage = new Tip("请输入笔记页名称");
            notepage.ShowDialog();
            if (nodeName.Equals(""))
                return;
            //TreeView tv = sender as TreeView;
           // Point point = list_treeView.PointToClient(Control.MousePosition);
            //TreeNode node = list_treeView.GetNodeAt(point);
            TreeNode node = list_treeView.SelectedNode;
            addSonNode(node, nodeName);
            ClosePreForm();//嵌入窗体前判断当前容器中是否有窗口没关掉
            NodeForm nodeForm = new NodeForm();
            
            OpenForm(nodeForm, nodeName);*/
        }

        private void fileinsert_toolStripButton_Click(object sender, EventArgs e)//插入文件的点击事件
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;                             //不允许多选
            openFileDialog.Filter ="ALL Word Files|*.docx;*.pptx;*.dox";    //文件类型筛选
            openFileDialog.Title = "请选择要插入的文档";                    
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
               

            }
        }

        private void pictrueinsert_toolStripButton_Click(object sender, EventArgs e)       //插入图片的点击事件
        {
            int index = note_tabControl.SelectedIndex;
            RichTextBox rtx = richTextBoxes.ElementAt(index);
            try
            {
                OpenFileDialog P_OpenFileDialog = new OpenFileDialog();//创建打开文件对话框对象
                P_OpenFileDialog.Filter = "*.jpg|*.jpg|*.bmp|*.bmp|*.png|*.png|*.ico|*.ico";//设置搜索的文件格式
                DialogResult P_DialogResult = P_OpenFileDialog.ShowDialog();//弹出打开文件对话框
                if (P_DialogResult == DialogResult.OK)//判断是否选中文件
                {
                    Clipboard.SetDataObject(Image.FromFile(P_OpenFileDialog.FileName), false);//将图像放入剪切板
                    if (rtx.CanPaste(DataFormats.GetFormat(DataFormats.Bitmap)))//判断剪切板内是否是图像
                    {
                        rtx.Paste();//粘贴剪切板的内容到控件中
                        Clipboard.SetDataObject(String.Empty, false);//清空剪切板
                    }
                }
            }
            catch { }
        }

        private void musicinsert_toolStripButton_Click(object sender, EventArgs e)//插入图片的
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;                             //不允许多选
            openFileDialog.Filter = "ALL Word Files|*.mp3;*.wma;*.avi;*.rm*.rmvb;*.flv;*.mpg*.mov;*.mkv";    //图片类型筛选
            openFileDialog.Title = "请选择要插入的音频";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;


            }
        }
        public TreeNode addRootNode(String treename)               //增加一个知识库节点，参数为知识库名称  
        {
            
            TreeNode node = new TreeNode();
            node.Text = treename;
            node.BackColor = ColorTranslator.FromHtml("#FF00FF");    //设置背景色
            list_treeView.Nodes.Add(node);                    //添加一个根节点
            return node;
        }
        public void addSonNode(TreeNode roodnode,String treename)//增加一个笔记页节点，第一个参数为知识库节点，第二个参数为名字
        {
            TreeNode node = new TreeNode();
            node.Text = treename;
            node.BackColor = ColorTranslator.FromHtml("#00FF00");    //设置背景色
            roodnode.Nodes.Add(node);                               //添加一个子节点
        }

        private void save_ToolStripMenuItem_Click(object sender, EventArgs e)//保存的点击事件
        {
            int index = note_tabControl.SelectedIndex;
            RichTextBox rtx = richTextBoxes.ElementAt(index);
            Note note = noteList.ElementAt(index);
            rtx.SaveFile(note.getRecordLocation(), RichTextBoxStreamType.RichText);
        }
    

        private void inclines_toolStripButton_Click(object sender, EventArgs e)//斜体的点击事件
        {
            inclinesbool = !inclinesbool;
            if (inclinesbool)
                SetFontStyle(FontStyle.Italic);
            else
                RemoveFontStyle(FontStyle.Italic);
        }

        private void underline_toolStripButton_Click(object sender, EventArgs e)//下划线的点击事件
        {
            underlinebool = !underlinebool;
            if (underlinebool)
                SetFontStyle(FontStyle.Underline);
            else
                RemoveFontStyle(FontStyle.Underline);
        }

        private void list_treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)//树的点击事件
        {
           
        }

        private void urlinsert_toolStripButton_Click(object sender, EventArgs e)//插入链接的点击事件
        {
            UrlForm urlForm = new UrlForm();
            urlForm.ShowDialog();

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void fontcolor_toolStripButton_Click(object sender, EventArgs e)//改变字体颜色
        {
            ColorDialog colorDialor = new ColorDialog();
        
            if (colorDialor.ShowDialog() == DialogResult.OK)
            {
                NodeForm.nodeForm.edit_richTextBox.SelectionColor = colorDialor.Color;                //设置 richTextbox1 中的文字颜色
                fontcolor_toolStripButton.ForeColor = colorDialor.Color;                              //设置按钮的颜色
            }

        }

        private void font_toolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)//改变字体的点击事件
        {
            String fonttype = font_toolStripComboBox.SelectedItem.ToString();
            ChangeFont(fonttype);

        }

        private void note_tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void fontsize_toolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)//改变字体大小的点击事件
        {
            float q = float.Parse(fontsize_toolStripComboBox.SelectedItem.ToString());
            ChangFontSize(q);
        }

        private Point pi;
        private void list_treeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode node = list_treeView.SelectedNode;
            if(1 == node.Level)
            {
                ClosePreForm();//嵌入窗体前判断当前容器中是否有窗口没关掉
               
                NodeForm nodeForm = new NodeForm();
                int index = node.Index;
                TreeNode prant = node.Parent;
                int ip = prant.Index;
                //MessageBox.Show("" + index+"-"+ip);
                Note note = KBM.getKB(ip).getNote(index);
                bindingNoteForm(nodeForm.edit_richTextBox, note);
                OpenForm(nodeForm, note.getName());
                showNote(nodeForm, note);
               //MessageBox.Show("" + note.getRecordLocation());
            }
        }

        private void listView_seek_DoubleClick(object sender, MouseEventArgs e)//双击查询列表子项事件
        {
            string name="";
            if (this.listView_seek.SelectedItems.Count == 0)
                return;
            name = this.listView_seek.SelectedItems[0].Text.ToString();
            ClosePreForm();
            NodeForm nodeForm = new NodeForm();
            KBM = FileManagement.loadKnowledgeBaseManagement();
            List<KnowledgeBase> kbs = KBM.getKBS();
            foreach (KnowledgeBase kb in kbs)
            {
                List<Note> notes = kb.GetNotes();
                foreach (Note note in notes)
                {
                    if (note.getName().Equals(name))
                    {
                        bindingNoteForm(nodeForm.edit_richTextBox, note);
                        OpenForm(nodeForm, note.getName());
                        showNote(nodeForm, note);
                        SearchKey(textBox_seek.Text, nodeForm.edit_richTextBox);
                    }
                }
            }
        }
        public void SearchKey(string text,RichTextBox rtb)//在richtextbox中高亮显示关键字
        {
            int index = rtb.Find(text, RichTextBoxFinds.MatchCase);
            int startPos = index;
            int nextIndex = 0;
            while (nextIndex != startPos)
            {
                rtb.SelectionStart = index;
                rtb.SelectionLength = text.Length;
                rtb.SelectionColor = Color.Blue;
                rtb.SelectionFont = new Font("Times New Roman", (float)12, FontStyle.Bold);
                rtb.Focus();
                nextIndex = rtb.Find(text, index + text.Length, RichTextBoxFinds.MatchCase);
                if (nextIndex == -1)
                    nextIndex = startPos;
                index = nextIndex;
            }
        }

        private void button_seek_Click(object sender, EventArgs e)//查找所有笔记页中是否存在匹配关键字的笔记页
        {
            listView_seek.Items.Clear();
            string path;
           
            KBM = FileManagement.loadKnowledgeBaseManagement();
            List<KnowledgeBase> kbs = KBM.getKBS();
            foreach (KnowledgeBase kb in kbs)
            {
                List<Note> notes = kb.GetNotes();
                foreach (Note note in notes)
                {
                    path = note.getRecordLocation();
                    RichTextBox rtb = new RichTextBox();
                    string s = File.ReadAllText(path);
                    rtb.Rtf = s;
                    if(-1!= rtb.Find(textBox_seek.Text, RichTextBoxFinds.MatchCase))
                    {
                        listView_seek.Items.Add(note.getName());
                    }
                }
            }
        }
        private void showNote(NodeForm nodeForm,Note note)
        {
            if (File.Exists(note.getRecordLocation())) {
                nodeForm.edit_richTextBox.LoadFile(note.getRecordLocation(), RichTextBoxStreamType.RichText);
            }
            else
            {
                MessageBox.Show("文件不存在");
            }
            
        }
        private void list_treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void list_treeView_MouseDown(object sender, MouseEventArgs e)
        {
            pi = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Right)//右击
            {
                list_treeView.ContextMenuStrip = null;
                TreeNode selectNode = list_treeView.GetNodeAt(e.X, e.Y);
                if (null != selectNode) {
                    if (selectNode.Level == 0)
                    {
                        list_treeView.ContextMenuStrip = knowbank_contextMenuStrip;
                    }
                    else if (selectNode.Level == 1)
                    {
                        list_treeView.ContextMenuStrip = note_contextMenuStrip;
                    }
                }
                else
                {
                    list_treeView.ContextMenuStrip = blackContextMunuStrip;
                }
                

            }
            
        }

        private void bold_toolStripButton_Click(object sender, EventArgs e)//设置或取消粗体的点击事件
        {
            boldbool = !boldbool;
            if (boldbool)
            {
                bold_toolStripButton.BackColor = Color.AliceBlue;
                SetFontStyle(FontStyle.Bold);
            }
            else
                RemoveFontStyle(FontStyle.Bold);
        }
        //5/18
        /*知识库加载
         * 点击——编辑框
         * 点击保存
         * 右键导入导出
         */
        KonwledgeBaseManagement KBM;
        List<RichTextBox> richTextBoxes = new List<RichTextBox>();

        private void addNotelick(object sender, EventArgs e)
        {
            // MessageBox.Show("导入笔记");
            addNote();
        }
        private void updateTree()
        {
            list_treeView.Nodes.Clear();
            List<KnowledgeBase> kbs = KBM.getKBS();
            foreach (KnowledgeBase kb in kbs)
            {
                TreeNode treeNode = addRootNode(kb.getName());
                List<Note> notes = kb.GetNotes();
                foreach (Note note in notes)
                {
                    addSonNode(treeNode, note.getName());
                }
            }
        }
        private void exportKBClick(object sender, EventArgs e)
        {
            // MessageBox.Show("导出知识库");
            exportKB();
        }

      

        private void exportNoteClick(object sender, EventArgs e)
        {
            //MessageBox.Show("导出笔记");
            exportNote();
          
        }
        private void addNote()
        {
            TreeNode node = list_treeView.SelectedNode;
            int index = node.Index;
            KnowledgeBase kb = KBM.getKB(index);
            FileManagement.addNoteDialog(kb);
            updateTree();
        }
        private void exportNote()
        {
            TreeNode node = list_treeView.SelectedNode;
            int index = node.Index;
            TreeNode prant = node.Parent;
            int ip = prant.Index;
            Note note = KBM.getKB(ip).getNote(index);
            FileManagement.exportNote(note);
        }
        private void addKB()
        {
            FileManagement.addKBDialog(KBM);
            updateTree();
        }
        private void exportKB()
        {
            TreeNode node = list_treeView.SelectedNode;
            int index = node.Index;
            FileManagement.exportKB(KBM.getKB(index));
        }

        private void addKBClick(object sender, EventArgs e)
        {
            //MessageBox.Show("导入");
            addKB();
        }

        private void listView_seek_double(object sender, EventArgs e)
        {

        }

        List<Note> noteList = new List<Note>();
        private void bindingNoteForm (RichTextBox richTextBox,Note note)
        {
            richTextBoxes.Add(richTextBox);
            noteList.Add(note);
        }


    }
}
