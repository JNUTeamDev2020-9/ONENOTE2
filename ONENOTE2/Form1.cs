using SQLiteDemo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms;

namespace ONENOTE2
{

    public partial class Form1 : Form
    {
        #region 数据

        public static Form1 form1;
        public static String nodeName;
        const int CLOSE_SIZE = 15;//用于绘制选项卡关闭按钮
        NoteDatabase noteDatabase; //新的数据库
        List<KnowledgeBase> KBS; //新的知识库列表
        #endregion

        #region 构造方法与加载

        public Form1()//窗口初始化
        {
            InitializeComponent();
            form1 = this;

        }
        private void Form1_Load(object sender, EventArgs e)//加载里面的项
        {
            list_treeView.LabelEdit = true;        //树目录为可编辑状态
            loadTreeView();
            list_treeView.ExpandAll();

        }
        private void loadTreeView() //加载树目录
        {
            
            noteDatabase = new NoteDatabase();
            KBS = noteDatabase.FetchKnowledgeBase();
            if (0 == KBS.Count) {
                //默认
                KnowledgeBase kb1 = new KnowledgeBase { Name = "默认知识库" };
                Note note = new Note
                {
                    Title = "默认笔记页",
                    Content = "",
                    Directory = "默认知识库"
                };
                noteDatabase.AddKnowledgeBase(kb1);
                
                noteDatabase.AddNote(note);

               
                KBS = noteDatabase.FetchKnowledgeBase();

            }
            
            foreach (KnowledgeBase kb in KBS)
            {
                TreeNode treeNode = addRootNode(kb.Name);
                List<Note> notes = noteDatabase.FetchNote(kb.Name);
                foreach (Note note in notes)
                {
                    addSonNode(treeNode, note.Title);

                }
            }
        }

        #endregion

        #region 编辑框字体及格式设置

        #region 相应数据参数
        public int len = 0;     //用于改变字体
        public int curRtbStart = 0;//用于改变字体
        Boolean boldbool = false, underlinebool = false, inclinesbool = false;//用于判断是否字体加粗还是取消加粗
        #endregion

        #region 字体类型
        /// <summary>
        /// 改变字体的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void font_toolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String fonttype = font_toolStripComboBox.SelectedItem.ToString();
            ChangeFont(fonttype);

        }

        /// <summary>
        /// 改变字体类型
        /// </summary>
        /// <param name="fontName"></param>
        private void ChangeFont(String fontName)
        {
            if (null == NodeForm.nodeForm) return;
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

        #endregion

        /// <summary>
        /// 改变字体大小，第一个参数为字号大小
        /// </summary>
        /// <param name="fontSize"></param>
        private void ChangFontSize(float fontSize)
        {
            if (null == NodeForm.nodeForm) return;
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

        /// <summary>
        /// 改变字体大小的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fontsize_toolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            float q = float.Parse(fontsize_toolStripComboBox.SelectedItem.ToString());
            ChangFontSize(q);
        }

        /// <summary>
        /// 斜体的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inclines_toolStripButton_Click(object sender, EventArgs e)
        {
            inclinesbool = !inclinesbool;
            if (inclinesbool)
                SetFontStyle(FontStyle.Italic);
            else
                RemoveFontStyle(FontStyle.Italic);
        }

        /// <summary>
        /// 下划线的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void underline_toolStripButton_Click(object sender, EventArgs e)
        {
            underlinebool = !underlinebool;
            if (underlinebool)
                SetFontStyle(FontStyle.Underline);
            else
                RemoveFontStyle(FontStyle.Underline);
        }

        /// <summary>
        /// 设置字体粗体、斜体、下划线
        /// </summary>
        /// <param name="fontstyle"></param>
        private void SetFontStyle(FontStyle fontstyle)
        {
            if (null == NodeForm.nodeForm) return;
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

        /// <summary>
        /// 取消字体粗体、斜体、下划线
        /// </summary>
        /// <param name="fontstyle"></param>
        private void RemoveFontStyle(FontStyle fontstyle)
        {
            if (null == NodeForm.nodeForm) return;
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


        /// <summary>
        /// 设置或取消粗体的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bold_toolStripButton_Click(object sender, EventArgs e)
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

        /// <summary>
        /// 改变字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fontcolor_toolStripButton_Click(object sender, EventArgs e)
        {
            if (null == NodeForm.nodeForm) return;
            ColorDialog colorDialor = new ColorDialog();

            if (colorDialor.ShowDialog() == DialogResult.OK)
            {
                NodeForm.nodeForm.edit_richTextBox.SelectionColor = colorDialor.Color;                //设置 richTextbox1 中的文字颜色
                fontcolor_toolStripButton.ForeColor = colorDialor.Color;                              //设置按钮的颜色
            }

        }

        #endregion

        #region 编辑框插入功能
        /// <summary>
        /// 编辑框的插入功能
        /// </summary>
        /// <param name="type_name">插入的类型</param>
        private void insert(string type_name)
        {
            
            RichTextBox rtx = getSelectedRichTextBox();
            if (null == rtx) return;
            try
            {
                OpenFileDialog P_OpenFileDialog = new OpenFileDialog();
                if (type_name == "file")//判别类型名
                {
                    P_OpenFileDialog.Filter = "*.txt|*.txt|*.doc|*.docx|*.xls|*.xlsx|*.pptx|*.pptx|*.rtf|*.rtf";
                }
                else if (type_name == "music")
                {
                    P_OpenFileDialog.Filter = "*.cd|*.cd|*.mp3|*.mp3|*.wav|*.wav";
                }
                else if (type_name == "picture")
                {
                    P_OpenFileDialog.Filter = "*.jpg|*.jpg|*.bmp|*.bmp|*.png|*.png|*.ico|*.ico";//设置搜索的文件格式
                }

                DialogResult P_DialogResult = P_OpenFileDialog.ShowDialog();
                if (P_DialogResult == DialogResult.OK)
                {
                    if (type_name == "picture")
                    {
                        Clipboard.SetDataObject(Image.FromFile(P_OpenFileDialog.FileName), false);//将图像放入剪切板
                        if (rtx.CanPaste(DataFormats.GetFormat(DataFormats.Bitmap)))//判断剪切板内是否是图像
                        {
                            rtx.Paste();//粘贴剪切板的内容到控件中
                            Clipboard.SetDataObject(String.Empty, false);//清空剪切板
                        }
                    }
                    else
                    {
                        string filePath = @P_OpenFileDialog.FileName;
                        System.Collections.Specialized.StringCollection strcoll = new System.Collections.Specialized.StringCollection();
                        strcoll.Add(filePath);
                        Clipboard.SetFileDropList(strcoll);
                        rtx.Paste();//粘贴剪切板的内容到控件中
                        Clipboard.SetDataObject(String.Empty, false);//清空剪切板
                    }

                }
            }
            catch { }
        }

        #region 插入图片
        /// <summary>
        /// 插入图片的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictrueinsert_toolStripButton_Click(object sender, EventArgs e)
        {
            insert("picture");
        }
        #endregion

        #region 插入文件
        /// <summary>
        /// 插入文件点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileinsert_toolStripButton_Click(object sender, EventArgs e)//插入文件的点击事件
        {
            insert("file");
        }
        #endregion

        #region 插入音频
        /// <summary>
        /// 插入音频点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void musicinsert_toolStripButton_Click(object sender, EventArgs e)
        {
            insert("music");
        }
        #endregion

        #region 插入链接
        /// <summary>
        /// 插入链接点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void urlinsert_toolStripButton_Click(object sender, EventArgs e)
        {
            RichTextBox rtx = getSelectedRichTextBox();
            if (null == rtx) return;

            UrlForm urlForm = new UrlForm();
            urlForm.ShowDialog();



            String _fileUrl = urlForm.url_text;
            String fileName = "";
            if (_fileUrl != "")
            {
                if (_fileUrl.IndexOf("http://") != -1)
                {
                    fileName = _fileUrl;
                }
                else
                {
                    fileName = "http://" + _fileUrl;
                }

            }

            rtx.AppendText(fileName);

            rtx.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox1_LinkClicked);


        }


        public System.Diagnostics.Process p = new System.Diagnostics.Process();

        /// <summary>
        /// ???
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richTextBox1_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)//点击链接执行
        {
            // Call Process.Start method to open a browser
            // with link text as URL.
            p = System.Diagnostics.Process.Start("IExplore.exe", e.LinkText);
        }
        #endregion

        #endregion

        #region 编辑框其他功能

        /// <summary>
        /// 获取当前选中的文本编辑框
        /// </summary>
        /// <returns></returns>
        private RichTextBox getSelectedRichTextBox() {
            int index = note_tabControl.SelectedIndex;
            if (index < 0) return null;
            return richTextBoxes.ElementAt(index);
        }

        /// <summary>
        /// 选中文本框的点击保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = note_tabControl.SelectedIndex;
            if (index >= 0) { //检查有没有选中的文本框
                RichTextBox rtx = richTextBoxes.ElementAt(index);
                Note note = noteList.ElementAt(index);
                note.Content = rtx.Rtf;
                noteDatabase.UpdateNote(note);
                
            }
        }

        /// <summary>
        /// 将笔记页和富文本绑定
        /// </summary>
        /// <param name="nodeForm"></param>
        /// <param name="note"></param>
        private void bindingNoteForm(NodeForm nodeForm, Note note)
        {
            richTextBoxes.Add(nodeForm.edit_richTextBox);
            noteList.Add(note);
        }

        #region 关闭和打开

        /// <summary>
        /// 重画选项卡按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void note_tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                //获取当前Tab选项卡的绘图区域
                Rectangle myTabRect = this.note_tabControl.GetTabRect(e.Index);
                //绘制标签头背景色
                using (Brush brBack = new SolidBrush(Color.Yellow))
                {
                    if (e.Index == this.note_tabControl.SelectedIndex)
                    {
                        e.Graphics.FillRectangle(brBack, myTabRect); //设置当前选中的tabgage的背景色
                    }
                }
                //先添加TabPage属性
                e.Graphics.DrawString(this.note_tabControl.TabPages[e.Index].Text,
                    this.Font, SystemBrushes.ControlText, myTabRect.X + 2, myTabRect.Y + 2);

                //再画一个矩形框
                using (Pen p = new Pen(Color.Transparent))
                {
                    myTabRect.Offset(myTabRect.Width - (CLOSE_SIZE + 3), 2);
                    myTabRect.Width = CLOSE_SIZE;
                    myTabRect.Height = CLOSE_SIZE;
                    e.Graphics.DrawRectangle(p, myTabRect);
                }
                //填充矩形框
                Color recColor = (e.State == DrawItemState.Selected) ? Color.Transparent : Color.Transparent;
                using (Brush b = new SolidBrush(recColor))
                {
                    e.Graphics.FillRectangle(b, myTabRect);
                }

                //画Tab选项卡右上方关闭按钮   
                using (Pen objpen = new Pen(Color.SlateBlue, 1.8f))
                {
                    //自己画X
                    //"\"线
                    Point p1 = new Point(myTabRect.X + 3, myTabRect.Y + 3);
                    Point p2 = new Point(myTabRect.X + myTabRect.Width - 3, myTabRect.Y + myTabRect.Height - 3);
                    e.Graphics.DrawLine(objpen, p1, p2);
                    //"/"线
                    Point p3 = new Point(myTabRect.X + 3, myTabRect.Y + myTabRect.Height - 3);
                    Point p4 = new Point(myTabRect.X + myTabRect.Width - 3, myTabRect.Y + 3);
                    e.Graphics.DrawLine(objpen, p3, p4);

                    ////使用图片，可以自定义图片
                    //Bitmap bt = new Bitmap(image);
                    //Point p5 = new Point(myTabRect.X, 4);//获取绘图区域的开始坐标位置
                    //e.Graphics.DrawImage(bt, p5);
                }
                e.Graphics.Dispose();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 选项卡按钮的关闭功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void note_tabControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.note_tabControl.SelectedTab.Text))
            {
                if (e.Button == MouseButtons.Left)
                {
                    int x = e.X, y = e.Y;
                    //计算关闭区域      
                    Rectangle myTabRect = this.note_tabControl.GetTabRect(this.note_tabControl.SelectedIndex); ;
                    //myTabRect.Offset(myTabRect.Width - (CLOSE_SIZE + 3), 2);
                    myTabRect.Offset(myTabRect.Width - 0x12, 2);
                    myTabRect.Width = CLOSE_SIZE;
                    myTabRect.Height = CLOSE_SIZE;
                    //如果鼠标在区域内就关闭选项卡      
                    bool isClose = x > myTabRect.X && x < myTabRect.Right && y > myTabRect.Y && y < myTabRect.Bottom;
                    if (isClose == true)
                    {
                        this.note_tabControl.TabPages.Remove(this.note_tabControl.SelectedTab);
                    }
                }
            }
        }

        /// <summary>
        /// 关闭初始笔记（程序一开始时使用）
        /// </summary>
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

        /// <summary>
        /// 打开一个编辑框（笔记页）
        /// </summary>
        /// <param name="objFrm"></param>
        /// <param name="name"></param>
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
        #endregion

        #endregion

        #region 笔记页和知识库

        #region 相关数据
        
        List<RichTextBox> richTextBoxes = new List<RichTextBox>(); //当前页面的所有编辑框
        List<Note> noteList = new List<Note>(); //和当前页面的编辑框绑定的笔记页
        #endregion

        #region 新建

        /// <summary>
        /// 新建一个笔记
        /// </summary>
        private void newNote()
        {
            TreeNode node;
            if ((node = list_treeView.SelectedNode) != null)
            {
                Tip notepage = new Tip("请输入笔记页名称");
                notepage.ShowDialog();
                if (nodeName == null || nodeName.Equals(""))
                { return; }

                ClosePreForm();//嵌入窗体前判断当前容器中是否有窗口没关掉
                NodeForm nodeForm = new NodeForm();
                nodeForm.edit_richTextBox.Dock = DockStyle.Fill;    //设置富文本框的填充
                String KBName = GetSelectedKnowledgeBaseName();
                if (null != KBName) {
                    Note note = new Note
                    {
                        Title = nodeName,
                        Content = "",
                        Directory = KBName
                    };
                    noteDatabase.AddNote(note);
                    
                    addSonNode(node, nodeName);
                    updateTree();
                    OpenForm(nodeForm, nodeName);
                    bindingNoteForm(nodeForm, note);
                }
                
                

            }
        }

        /// <summary>
        /// 新建一个知识库
        /// </summary>
        private void newKB()
        {
            Tip notepage = new Tip("请输入知识库名称");
            notepage.ShowDialog();
            if (!nodeName.Equals(""))
            {
                KnowledgeBase kb = new KnowledgeBase { Name = nodeName };
                noteDatabase.AddKnowledgeBase(kb);
                updateTree();
            }

        }

        /// <summary>
        /// 新建笔记点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list_treeView_newNoteClick(object sender, EventArgs e)
        {
            newNote();
        }

        /// <summary>
        /// 新建知识库点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list_treeView_newKBCilck(object sender, EventArgs e)
        {
            newKB();
        }

        #endregion

        #region 导入

        /// <summary>
        /// 导入笔记页
        /// </summary>
        private void importNote()
        {
            String KBName = GetSelectedKnowledgeBaseName();
            if (null != KBName) {
                String[] fileNames = showOpenFileDialog();
                for (int i = 0; i < fileNames.Length; i++)
                {
                    importNote(KBName, fileNames[i]);
                    updateTree();
                }
            }
            

        }

        /// <summary>
        /// 由importNote和importKB调用
        /// </summary>
        /// <param name="KBname"></param>
        /// <param name="NotefileName"></param>
        private void importNote(String KBname,String NotefileName) {
            RichTextBox richTextBox = new RichTextBox();
            richTextBox.LoadFile(NotefileName);
            Note note = new Note
            {
                Content = richTextBox.Rtf,
                Title = Note.GetTitleFromFileName(NotefileName),
                Directory = GetKBNameFromFile(KBname)
            };
            noteDatabase.AddNote(note);
        }

        /// <summary>
        /// 从文件名中裁剪出正确的知识库名称
        /// </summary>
        /// <param name="KBFilename"></param>
        /// <returns></returns>
        private String GetKBNameFromFile(String KBFilename)
        {
            int index = KBFilename.LastIndexOf('\\');
            if (index > 0) {
                return KBFilename.Substring(index + 1, KBFilename.Length - index - 1);
            }
            return KBFilename;
        }
        
        /// <summary>
        /// 获取树目录选中的知识库名称
        /// </summary>
        /// <returns></returns>
        private String GetSelectedKnowledgeBaseName() {
            TreeNode node = list_treeView.SelectedNode;
            if (null != node)
            {
                if (1 == node.Level)
                {
                    ClosePreForm();//嵌入窗体前判断当前容器中是否有窗口没关掉

                    NodeForm nodeForm = new NodeForm();
                    nodeForm.edit_richTextBox.Dock = DockStyle.Fill;    //将富文本框设置为自动适应
                    int index = node.Index;
                    TreeNode prant = node.Parent;
                    return prant.Text;
                }
                if (0 == node.Level) {
                    return node.Text;
                }
            }
            return null;
        }

        /// <summary>
        /// GUI代码 分割
        /// 显示文件浏览器并获取用户选择
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        private static String showFolderBrowserDialog(String description)
        {
            try
            {
                // 新建文件浏览器对话
                FolderBrowserDialog dialog = new FolderBrowserDialog();

                // 设置对话框的描述
                dialog.Description = description;

                // 返回用户的选择
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.SelectedPath;
                }
                return null;
            }
            catch
            {
                MessageBox.Show("文件夹选择器打开失败");
                return null;
            }
        }

        /// <summary>
        /// 导入知识库
        /// </summary>
        private void importKB()
        {
            String floderName = showFolderBrowserDialog("导入知识库");
            String KBName = GetKBNameFromFile(floderName);
            KnowledgeBase kb = new KnowledgeBase() { Name = KBName };
            noteDatabase.AddKnowledgeBase(kb);  //添加好知识库

            // 将该路径中所有符合格式（.rtf）的文件都添加到该知识库中作为笔记页
            DirectoryInfo TheFolder = new DirectoryInfo(floderName);
            foreach (FileInfo fi in TheFolder.GetFiles())
            {
                if (fi.Name.EndsWith(Note.GetNoteFormat()))
                {
                    importNote(floderName, floderName+@"\"+fi.Name);
                }
            }
            updateTree();

        }

        /// <summary>
        /// 导入笔记点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list_treeView_importNotelick(object sender, EventArgs e)
        {
            // MessageBox.Show("导入笔记");
            importNote();
        }

        /// <summary>
        /// 导入知识库点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list_treeView_importKBClick(object sender, EventArgs e)
        {
            //MessageBox.Show("导入");
            importKB();
        }

        #endregion

        #region 导出

        /// <summary>
        /// 导出笔记
        /// </summary>
        private void exportNote()
        {
            
            TreeNode node = list_treeView.SelectedNode;
            if (null != node)
            {
                if (node.Parent != null)
                {
                    int index = node.Index;
                    TreeNode prant = node.Parent;
                    int ip = prant.Index;
                    Note note = noteDatabase.FetchNote(node.Parent.Text, node.Text);
                    // 选择导出笔记的目标文件夹
                    String selectedPath = showFolderBrowserDialog("导出笔记");
                    exportNote(selectedPath,note);
                }
            }
        }

        /// <summary>
        /// 导出笔记 由exportNote和exportKB调用
        /// </summary>
        /// <param name="note"></param>
        private void exportNote(String selectedPath,Note note)
        {
            
            // 获取目标路径
            String filePath = selectedPath + @"\" + note.Title + Note.GetNoteFormat();

            // 若该路径中存在同名的笔记页文件，进行提示
            if (File.Exists(filePath))
            {
                // 提示信息错误
                int answer = (int)MessageBox.Show("知识库中已有名为" + note.Title+ "的文件，是否替换", "替换提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (6 == answer)//返回值 是6 否7 取消2
                {
                    File.Delete(filePath);
                    RichTextBox richTextBox = new RichTextBox();
                    richTextBox.Rtf = note.Content;
                    richTextBox.SaveFile(filePath, RichTextBoxStreamType.RichText);
                }
            }
            // 若不存在同名，复制该笔记页文件到目标文件夹
            else
            {
                File.Delete(filePath);
                RichTextBox richTextBox = new RichTextBox();
                richTextBox.Rtf = note.Content;
                richTextBox.SaveFile(filePath, RichTextBoxStreamType.RichText);
            }
        }


        private void exportKB(String KBName) {
            // 创建文件夹浏览器并获得导出目标路径
            String selectedPath = showFolderBrowserDialog("导出笔记");
            String directoryPath = selectedPath + @"\" + KBName;

            // 若该路径下存在与该知识库同名的文件夹，询问是否替换
            if (Directory.Exists(directoryPath))
            {
                int answer = (int)MessageBox.Show("文件夹中已有名为" + KBName + "的文件夹，是否替换", "替换提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (6 == answer)//返回值 是6 否7 取消2
                {
                    Directory.Delete(directoryPath);
                    Directory.CreateDirectory(directoryPath);
                    List<Note> notes = noteDatabase.FetchNote(KBName);
                    for (int i = 0; i < notes.Count; i++) {
                        exportNote(directoryPath, notes.ElementAt(i));
                    }
                }
            }
            // 若不存在，则复制知识库对应的文件夹到目标文件夹
            else
            {
                Directory.CreateDirectory(directoryPath);
                List<Note> notes = noteDatabase.FetchNote(KBName);
                for (int i = 0; i < notes.Count; i++)
                {
                    exportNote(directoryPath, notes.ElementAt(i));
                }
            }
        }
        /// <summary>
        /// 导出知识库
        /// </summary>
        private void exportKB()
        {
            
            TreeNode node;
            if ((node = list_treeView.SelectedNode) != null)
            {
                if (0 == node.Level) { //知识库
                    exportKB(node.Text);
                }
            }
        }

        /// <summary>
        /// 导出知识库点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list_treeView_exportKBClick(object sender, EventArgs e)
        {
            // MessageBox.Show("导出知识库");
            exportKB();
        }

        /// <summary>
        /// 导出笔记点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list_treeView_exportNoteClick(object sender, EventArgs e)
        {
            //MessageBox.Show("导出笔记");
            exportNote();

        }
        #endregion

        #region 添加

        /// <summary>
        /// 添加笔记页
        /// </summary>
        private void addNote() {
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

        /// <summary>
        /// 添加知识库
        /// </summary>
        private void addKB() {
            Tip knowbank = new Tip("请输入知识库名称");
            knowbank.ShowDialog();
            if (nodeName == null)
                nodeName = "";
            if (nodeName.Equals(""))
                return;
            addRootNode(nodeName);
        }
        
        /// <summary>
        /// 添加笔记页的点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addnotepage_label_Click(object sender, EventArgs e)
        {
            addNote();
        }

        /// <summary>
        /// 知识库的点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)//添加知识库的点击事件
        {
            addKB();
        }


        #endregion

        #endregion

        #region 目录树

        /// <summary>
        /// 鼠标点击的坐标
        /// </summary>
        private Point pi;

        /// <summary>
        /// 树的范围内鼠标按下时获取坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list_treeView_MouseDown(object sender, MouseEventArgs e)
        {
            pi = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Right)//右击
            {
                list_treeView.ContextMenuStrip = null;
                TreeNode selectNode = list_treeView.GetNodeAt(e.X, e.Y);
                if (null != selectNode)
                {
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

        /// <summary>
        /// 在树目录上添加一个知识库的根节点
        /// </summary>
        /// <param name="treename"></param>
        /// <returns></returns>
        private TreeNode addRootNode(String treename)
        {

            TreeNode node = new TreeNode();
            node.Text = treename;
            node.BackColor = ColorTranslator.FromHtml("#FF00FF");    //设置背景色
            list_treeView.Nodes.Add(node);                    //添加一个根节点
            return node;
        }

        /// <summary>
        /// 在知识库的根目录下添加一个笔记页结点
        /// </summary>
        /// <param name="roodnode">知识库节点</param>
        /// <param name="treename">名字</param>
        public void addSonNode(TreeNode roodnode, String treename)
        {
            TreeNode node = new TreeNode();
            node.Text = treename;
            node.BackColor = ColorTranslator.FromHtml("#00FF00");    //设置背景色
            roodnode.Nodes.Add(node);                               //添加一个子节点
        }

        /// <summary>
        /// 树的双击点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list_treeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode node = list_treeView.SelectedNode;
            if (null != node)
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
                    Note note = noteDatabase.FetchNote(KBS.ElementAt(ip).Name).ElementAt(index);
                    
                    bindingNoteForm(nodeForm, note);
                    OpenForm(nodeForm, note.Title);
                    showNote(nodeForm, note);
                    //MessageBox.Show("" + note.getRecordLocation());
                }
            }

        }

        /// <summary>
        /// //双击笔记名字时加载内容
        /// </summary>
        /// <param name="nodeForm"></param>
        /// <param name="note"></param>
        private void showNote(NodeForm nodeForm, Note note)
        {
            nodeForm.edit_richTextBox.Rtf = note.Content;

        }
        
        /// <summary>
        /// 更新树目录
        /// </summary>
        private void updateTree()
        {
            list_treeView.Nodes.Clear();
            KBS = noteDatabase.FetchKnowledgeBase();
            foreach (KnowledgeBase kb in KBS)
            {
                TreeNode treeNode = addRootNode(kb.Name);
                List<Note> notes;
                if ((notes = noteDatabase.FetchNote(kb.Name)) != null)
                {
                    foreach (Note note in notes)
                    {
                        addSonNode(treeNode, note.Title);
                    }
                }
            }
            list_treeView.ExpandAll();
        }


        #endregion

        #region 打开文件对话框（GUI控件）：用于导入笔记页
        /// <summary>
        /// GUI代码 分割
        /// 创建新的打开文件对话框
        /// </summary>
        /// <returns></returns>
        private static string[] showOpenFileDialog()
        {
            try
            {
                // 创建打开文件对话框
                OpenFileDialog dialog = new OpenFileDialog();

                // 设置搜索的文件格式
                dialog.Filter = "*" + Note.GetNoteFormat() + "|*" + Note.GetNoteFormat();

                // 启用多选
                dialog.Multiselect = true;

                // 返回用户的选择
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.FileNames;
                }
                return null;
            }
            catch
            {
                MessageBox.Show("文件选择器打开失败");
                return null;
            }
        }
        #endregion

        #region 撤销，重做
        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lastOperation_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NodeForm.NoteUndo();
        }


        /// <summary>
        /// 重做
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextOperation_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NodeForm.NoteRedo();
        }

        #endregion

    }

    #region 撤销，重做
    /// <summary>
    /// 封装一个操作。对于一个操作，记录其进行前后的状态，以供未来可能进行的“撤销”操作。
    /// </summary>
    public class Action
    { }

    public class Do
    {
        #region 成员数据
        /// <summary>
        /// 这个栈保存最近的操作以便撤销。
        /// </summary>
        private static Stack<Action> actionsHistory = new Stack<Action>();
        /// <summary>
        /// 这个栈保存可以重做的操作。
        /// </summary>
        private static Stack<Action> actionsToRedo = new Stack<Action>();
        #endregion

        #region 成员方法
        /// <summary>
        /// 判断是否可以进行“撤销”操作
        /// </summary>
        /// <returns></returns>
        public static bool CanUndo()
        {
            return actionsHistory.Count > 0;
        }

        /// <summary>
        /// 判断是否可以进行“重做”操作
        /// </summary>
        /// <returns></returns>
        public static bool CanRedo()
        {
            return actionsToRedo.Count > 0;
        }

        public static void AddActionHistory(Action action)
        {
            actionsHistory.Push(action);
        }

        /// <summary>
        /// 返回操作栈中最近一次操作以撤销该操作
        /// </summary>
        /// <returns></returns>
        public static Action Undo()
        {
            if (CanUndo())
            {
                Action toUndo = actionsHistory.Pop();
                actionsToRedo.Push(toUndo);
                return toUndo;
            }
            else
            {
                return null;
            }
        }

        public static Action Redo()
        {
            if (CanRedo())
            {
                return actionsToRedo.Pop();
            }
            else
            {
                return null;
            }
        }

        #endregion

    }
    #endregion

}

/*
 * 加载
 * 点击显示
 * 保存
 * 导入导出
 * 
 * 
 */
