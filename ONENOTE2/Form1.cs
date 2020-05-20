using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ONENOTE2
{

    public partial class Form1 : Form
    {
        public static Form1 form1;
        public static String nodeName;
        public int len = 0;     //用于改变字体
        public int curRtbStart = 0;//用于改变字体
        Boolean boldbool = false,underlinebool=false, inclinesbool=false;//用于判断是否字体加粗还是取消加粗
        public Form1()//窗口初始化
        {
            InitializeComponent();
            form1 = this;
            
        }
        private void Form1_Load(object sender, EventArgs e)//加载里面的项
        {
            list_treeView.LabelEdit = true;        //树目录为可编辑状态
            loadTreeView();
        }
        private void loadTreeView() //加载树目录
        {
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
        private void ChangeFont(String fontName)// 改变字体类型
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


        private void ClosePreForm()    //关闭note_tabControl当前窗口
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
        private void OpenForm(Form objFrm, String name)    //新建一个tabpage,并显示
        {
            //嵌入子窗体到父窗体中，把添加学员信息嵌入到主窗体右侧
            objFrm.TopLevel = false;                                //将子窗体设置成非最高层，非顶级控件
            objFrm.WindowState = FormWindowState.Maximized;        //将当前窗口设置成最大化
            objFrm.FormBorderStyle = FormBorderStyle.None;        //去掉窗体边框
            objFrm.TopLevel = false;
            objFrm.BackColor = Color.White;
            objFrm.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            objFrm.FormBorderStyle = FormBorderStyle.None;
            objFrm.Dock = DockStyle.Fill;
            objFrm.Show();
            TabPage tabPage = new TabPage();
            tabPage.Text = name;
            this.note_tabControl.TabPages.Add(tabPage);             //添加一个tabpage
            this.note_tabControl.SelectTab((int)(this.note_tabControl.TabPages.Count - 1));   //将当前选项选中为新添的选项
            objFrm.Parent = this.note_tabControl.SelectedTab;     //指定子窗体显示的容器
            objFrm.Show();
        }

        private void label1_Click(object sender, EventArgs e)//添加知识库的点击事件
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
            Tip notepage = new Tip("请输入笔记页名称");
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
            nodeForm.edit_richTextBox.Dock = DockStyle.Fill;    //设置富文本框的填充
            OpenForm(nodeForm, nodeName);
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
        
        private void fontsize_toolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)//改变字体大小的点击事件
        {
            float q = float.Parse(fontsize_toolStripComboBox.SelectedItem.ToString());
            ChangFontSize(q);
        }
        
        
        
        //5/18
        /*知识库加载
         * 点击——编辑框
         * 点击保存
         * 右键导入导出
         */
        private Point pi;
        private void list_treeView_MouseDoubleClick(object sender, MouseEventArgs e)//知识树的双击事件
        {
            TreeNode node = list_treeView.SelectedNode;
            if(null != node)
            {
                if (1 == node.Level)
                {
                    ClosePreForm();//嵌入窗体前判断当前容器中是否有窗口没关掉

                    NodeForm nodeForm = new NodeForm();
                    nodeForm.edit_richTextBox.Dock = DockStyle.Fill;    //将富文本框设置为自动适应
                    int index = node.Index;
                    TreeNode prant = node.Parent;
                    int ip = prant.Index;
                    //MessageBox.Show("" + index+"-"+ip);
                    Note note = KBM.getKB(ip).getNote(index);
                    bindingNoteForm(nodeForm, note);
                    OpenForm(nodeForm, note.getName());
                    showNote(nodeForm, note);
                    //MessageBox.Show("" + note.getRecordLocation());
                }
            }
            
        }
        private void showNote(NodeForm nodeForm,Note note)//双击笔记名字时加载内容
        {
            if (File.Exists(note.getRecordLocation())) {
                nodeForm.edit_richTextBox.LoadFile(note.getRecordLocation(), RichTextBoxStreamType.RichText);
            }
            else
            {
                MessageBox.Show("文件不存在");
            }
            
        }
        private void list_treeView_MouseDown(object sender, MouseEventArgs e)//树的范围内鼠标按下时获取坐标
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
       
        KonwledgeBaseManagement KBM;
        List<RichTextBox> richTextBoxes = new List<RichTextBox>();

        private void list_treeView_importNotelick(object sender, EventArgs e)//导入笔记
        {
            // MessageBox.Show("导入笔记");
            importNote();
        }
        private void list_treeView_exportKBClick(object sender, EventArgs e)
        {
            // MessageBox.Show("导出知识库");
            exportKB();
        }
        private void list_treeView_exportNoteClick(object sender, EventArgs e)
        {
            //MessageBox.Show("导出笔记");
            exportNote();
          
        }
        private void list_treeView_importKBClick(object sender, EventArgs e)
        {
            //MessageBox.Show("导入");
            importKB();
        }
        
        private void importNote()
        {
            TreeNode node;
            if (( node = list_treeView.SelectedNode)!= null){
                int index = node.Index;
                KnowledgeBase kb = KBM.getKB(index);
                FileManagement.importNoteDialog(KBM, kb);
                updateTree();
            }

        }
        private void exportNote()
        {
            TreeNode node = list_treeView.SelectedNode;
            if(null != node)
            {
                if(node.Parent != null)
                {
                    int index = node.Index;
                    TreeNode prant = node.Parent;
                    int ip = prant.Index;
                    Note note = KBM.getKB(ip).getNote(index);
                    FileManagement.exportNote(note);
                }
            }
        }
        private void importKB()
        {
            FileManagement.importKBDialog(KBM);
            updateTree();
        }
        private void exportKB()
        {
            TreeNode node;
            if((node= list_treeView.SelectedNode) != null)
            {
                int index = node.Index;
                FileManagement.exportKB(KBM.getKB(index));
            }
        }

        
        private void list_treeView_newNoteClick(object sender, EventArgs e)
        {
            newNote();
        }
        private void list_treeView_newKBCilck(object sender, EventArgs e)
        {
            newKB();
        }

        private void newNote()
        {
            TreeNode node;
            if ((node = list_treeView.SelectedNode) != null)
            {
                Tip notepage = new Tip("请输入笔记页名称");
                notepage.ShowDialog();
                if (nodeName.Equals("")) return;

                ClosePreForm();//嵌入窗体前判断当前容器中是否有窗口没关掉
                NodeForm nodeForm = new NodeForm();
                nodeForm.edit_richTextBox.Dock = DockStyle.Fill;    //设置富文本框的填充
                int index = node.Index;
                KnowledgeBase kb = KBM.getKB(index);
                Note note;
                if ((note =FileManagement.newNote(kb, nodeName))!=null)
                {
                    addSonNode(node, nodeName);
                    OpenForm(nodeForm, nodeName);
                    bindingNoteForm(nodeForm, note);
                }
            }
        }
        private void newKB()
        {
            Tip notepage = new Tip("请输入知识库名称");
            notepage.ShowDialog();
            if (!nodeName.Equals(""))
            {
                if (FileManagement.newKB(KBM, nodeName))
                {
                    updateTree();
                }
            }

        }

        List<Note> noteList = new List<Note>();
        private void bindingNoteForm (NodeForm nodeForm,Note note)//将富文本和当前笔记绑定
        {
            richTextBoxes.Add(nodeForm.edit_richTextBox);
            noteList.Add(note);
        }
        private void updateTree()//更新树目录
        {
            list_treeView.Nodes.Clear();
            List<KnowledgeBase> kbs = KBM.getKBS();
            foreach (KnowledgeBase kb in kbs)
            {
                TreeNode treeNode = addRootNode(kb.getName());
                List<Note> notes;
                if((notes = kb.GetNotes()) != null)
                {
                    foreach (Note note in notes)
                    {
                        addSonNode(treeNode, note.getName());
                    }
                }
            }
        }


    }
}
