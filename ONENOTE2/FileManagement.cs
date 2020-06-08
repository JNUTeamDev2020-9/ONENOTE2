using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONENOTE2 
{
    class FileManagement
    {
        #region 知识库(KnowledgeBase)：新建，导入，导出
        /// <summary>
        /// 初始化指定路径为知识库，并将其返回
        /// </summary>
        /// <param name="selectedPath"></param>
        /// <returns></returns>
        private static KnowledgeBase1 newKB(String selectedPath)
        {
            // 新建了一个知识库实例
            KnowledgeBase1 knowledgeBase = new KnowledgeBase1(selectedPath);

            // 将该路径中所有符合格式（.rtf）的文件都添加到该知识库中作为笔记页
            DirectoryInfo TheFolder = new DirectoryInfo(selectedPath);
            foreach (FileInfo fi in TheFolder.GetFiles())
            {
                if (fi.Name.EndsWith(Note1.getFormat()))
                {
                    Note1 note = new Note1(fi.Name, knowledgeBase.getRecordLocation());
                    knowledgeBase.addNote(note);
                }
            }

            return knowledgeBase;
        }

        /// <summary>
        /// GUI代码 分割
        /// 导入知识库的会话
        /// </summary>
        /// <param name="konwledgeBaseManagement"></param>
        public static void importKBDialog(KonwledgeBaseManagement konwledgeBaseManagement)
        {
            // 选择一个文件夹作为新的知识库对象
            String selectedPath = showFolderBrowserDialog("添加知识库");
            KnowledgeBase1 kb;
            if (null != selectedPath)
            {
                kb = newKB(selectedPath);
                if (konwledgeBaseManagement.existKB(kb))
                {
                    MessageBox.Show("请勿重复添加知识库");
                    return;
                }
                konwledgeBaseManagement.addKnowledgeBase(kb);
            }
        }

        /// <summary>
        /// 导出知识库。
        /// GUI代码。
        /// </summary>
        /// <param name="knowledgeBase"></param>
        public static void exportKB(KnowledgeBase1 knowledgeBase)
        {
            // 创建文件夹浏览器并获得导出目标路径
            String selectedPath = showFolderBrowserDialog("导出笔记");
            String directoryPath = selectedPath + @"\" + knowledgeBase.getName();

            // 若该路径下存在与该知识库同名的文件夹，询问是否替换
            if (Directory.Exists(directoryPath))
            {
                int answer = (int)MessageBox.Show("知识库中已有名为" + knowledgeBase.getName() + "的文件夹，是否替换", "替换提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (6 == answer)//返回值 是6 否7 取消2
                {
                    Directory.Delete(directoryPath);
                    Directory.CreateDirectory(directoryPath);
                    knowledgeBase.copyNotes(directoryPath);
                }
            }
            // 若不存在，则复制知识库对应的文件夹到目标文件夹
            else
            {
                Directory.CreateDirectory(directoryPath);
                knowledgeBase.copyNotes(directoryPath);
            }
        }
        #endregion

        #region 文件夹浏览器对话框（GUI控件）：用于导入、导出知识库，导出笔记
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
            
        #endregion

        #region 知识库管理器（KonwledgeBaseManagement）：新建、持久化存储与加载
        /// <summary>
        /// 获取程序当前工作路径
        /// </summary>
        /// <returns></returns>
        public static string getRecordLocation()
        {
            string Path = Environment.CurrentDirectory;
            int index = Path.LastIndexOf('\\');
            Path = Path.Remove(index, 6);
            index = Path.LastIndexOf('\\');
            Path = Path.Remove(index, 4);
            return Path;
        }

        /// <summary>
        /// 加载并返回知识库管理器（KonwledgeBaseManagement）对象。
        /// 测试和API混了？切割部分另设方法
        /// </summary>        
        private static readonly string RootKnowledgeBaseManagement = @"\KBM.xml";
        public static KonwledgeBaseManagement loadKnowledgeBaseManagement() 
        {
            // 知识库管理器
            KonwledgeBaseManagement konwledgeBaseManagement;

            // 获得当前工作路径
            String path = getRecordLocation();

            // 如果存在持久化存储的知识库管理器实例，则加载
            if (File.Exists(getRecordLocation() + @"\KBM.xml"))
            {
                XMLTransformation xMLTransformation = new XMLTransformation();
                konwledgeBaseManagement = xMLTransformation.deserialization(path, "KBM");
            }
            // 若不存在，则新建知识库管理器实例
            else
            {
                konwledgeBaseManagement = new KonwledgeBaseManagement();

                // 在当前工作路径下新建知识库
                // 测试名？
                KnowledgeBase1 kb = new KnowledgeBase1(getRecordLocation()+@"\默认知识库");

                // 在当前工作路径中，添加与新建的知识库对应的文件夹
                if (!Directory.Exists(kb.getRecordLocation()))
                {
                    Directory.CreateDirectory(kb.getRecordLocation());
                }

                // 新建笔记页
                // 测试代码？
                Note1 note1 = new Note1("默认笔记",kb.getRecordLocation());

                // 在新建知识库路径中，创建与新建的笔记页对应的富文本文件
                // 若对应文件已存在，则将其删除以便新建
                if (!File.Exists(note1.getRecordLocation())) 
                {
                   
                    File.Delete(note1.getRecordLocation());
                }
                creatFile(note1.getRecordLocation());
                kb.addNote(note1);
                
                // 添加新建的知识库实例到当前知识库管理器中
                konwledgeBaseManagement.addKnowledgeBase(kb);
                
            }
            return konwledgeBaseManagement;
        }

        /// <summary>
        /// 持久化存储知识库管理器实例
        /// </summary>
        /// <param name="konwledgeBaseManagement"></param>
        public static void saveKBM(KonwledgeBaseManagement konwledgeBaseManagement)
        {
            // 获得程序当前工作路径
            String Path = getRecordLocation();

            // 若存在则删除该XML文件
            if (File.Exists(Path + @"\KBM.xml"))
            {
                File.Delete(Path + @"\KBM.xml");
            }

            // 序列化为XML文件保存
            XMLTransformation xMLTransformation = new XMLTransformation();
            xMLTransformation.serialize(Path, "KBM", konwledgeBaseManagement);
        }


        /// <summary>
        /// 创建新知识库管理器实例。
        /// </summary>
        /// <param name="konwledgeBaseManagement"></param>
        /// <param name="KBname"></param>
        /// <returns></returns>
        public static Boolean newKB(KonwledgeBaseManagement konwledgeBaseManagement, String KBname)
        {
            KnowledgeBase1 kb = new KnowledgeBase1(getRecordLocation(), KBname);
            if (!konwledgeBaseManagement.existKB(kb))
            {
                konwledgeBaseManagement.addKnowledgeBase(kb);
                return true;
            }
            else
            {
                MessageBox.Show("已存在知识库");
                return false;
            }
        }

        #endregion

        #region 笔记页：新建，导入，导出
        /// <summary>
        /// 导入笔记页到指定的知识库中。
        /// 切割部分另设方法
        /// </summary>
        /// <param name="fileNames"></param>
        /// <param name="konwledgeBaseManagement"></param>
        /// <param name="knowledgeBase"></param>
        private static void importNote(String[] fileNames, KonwledgeBaseManagement konwledgeBaseManagement,KnowledgeBase1 knowledgeBase)
        {
            // 不会用到的初始值？
            int index = fileNames[0].LastIndexOf(@"\") + 1;

            // 
            for (int i = 0; i < fileNames.Length; i++)
            {
                
                // 判断将要添加的笔记页是否已存在于目标知识库中
                index = konwledgeBaseManagement.existNote(fileNames[i]);

                // 若不存在，尝试添加：
                if (-1 == index)
                {
                    // （以下应分割为单独的方法）：

                    // 获取笔记页名
                    String name = fileNames[i].Substring(index, fileNames[i].Length - index);

                    //新建笔记页对象
                    Note1 note = new Note1(name, knowledgeBase.getRecordLocation());

                    // 添加该笔记页到指定的知识库中
                    knowledgeBase.addNote(note);

                    // 若目标知识库对应文件夹中已存在同名文件，
                    // 询问用户的选择
                    if (File.Exists(note.getRecordLocation()))
                    {
                       
                        int answer = (int)MessageBox.Show("知识库中已有名为" + note.getName() + "的文件，是否替换", "替换提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                        if (6 == answer)//返回值 是6 否7 取消2
                        {
                            File.Delete(note.getRecordLocation());
                        }
                        else
                        {
                            continue;
                        }

                    }

                    // 若不存在，将该笔记页文件复制到目标知识库对应的文件夹中
                    File.Copy(fileNames[i], note.getRecordLocation());//另存
                }
                else
                {
                    MessageBox.Show("该笔记已经在" + konwledgeBaseManagement.getKB(index).getName() + "存在");
                }               
                
            } // for循环的结束
        }

        /// <summary>
        /// 导入笔记页面对话框
        /// GUI代码 切割
        /// </summary>
        /// <param name="konwledgeBaseManagement"></param>
        /// <param name="knowledgeBase"></param>        
        public static void importNoteDialog(KonwledgeBaseManagement konwledgeBaseManagement, KnowledgeBase1 knowledgeBase)
        {
            if (null != knowledgeBase)
            {
                // 可以选择数个希望导入的笔记页文件
                String[] fileNames = showOpenFileDialog();
                if (null != fileNames)
                {
                    importNote(fileNames, konwledgeBaseManagement, knowledgeBase);
                }
            }
            else
            {
                MessageBox.Show("请先选择知识库");
            }
        }
              

        /// <summary>
        /// 导出笔记页。
        /// GUI代码。将“判断重名-若重名，覆盖与否”的逻辑单独写成方法，传入string提示信息
        /// </summary>
        /// <param name="note"></param>
        public static void exportNote(Note1 note)//导出笔记
        {
            // 选择导出笔记的目标文件夹
            String selectedPath = showFolderBrowserDialog("导出笔记");

            // 获取目标路径
            String filePath = selectedPath + @"\" + note.getName() + Note1.getFormat();

            // 若该路径中存在同名的笔记页文件，进行提示
            if (File.Exists(filePath))
            {
                // 提示信息错误
                int answer = (int)MessageBox.Show("知识库中已有名为" + note.getName() + "的文件，是否替换", "替换提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (6 == answer)//返回值 是6 否7 取消2
                {
                    File.Delete(filePath);
                    File.Copy(note.getRecordLocation(), filePath);
                }
            }
            // 若不存在同名，复制该笔记页文件到目标文件夹
            else
            {
                File.Copy(note.getRecordLocation(), filePath);
            }
        }

        /// <summary>
        /// 新建笔记页对象。
        /// knowledgeBase是目标知识库
        /// </summary>
        /// <param name="knowledgeBase"></param>
        /// <param name="noteName"></param>
        /// <returns></returns>
        public static Note1 newNote(KnowledgeBase1 knowledgeBase, String noteName)
        {   
            // 若knowledgeBase有效则新建笔记页对象
            if (null != knowledgeBase)
            {
                Note1 note = new Note1(noteName, knowledgeBase.getRecordLocation());

                // 若目标知识库中不存在同名的笔记页对象，则添加
                String path = note.getRecordLocation();
                if (!knowledgeBase.existNote(path))
                {
                    creatFile(path);
                    knowledgeBase.addNote(note);
                    return note;
                }
                // 否则提示用户“已重名”
                else
                {
                    MessageBox.Show("已重名");
                    return null;
                }
                
            }
            else
            {
                MessageBox.Show("请先选中知识库");
                return null;
            }
            
        }

        #endregion

        #region 创建文件（GUI控件）：用于新建笔记页

        /// <summary>
        /// 创建RTF文件。
        /// GUI代码
        /// </summary>
        /// <param name="recordLocation"></param>
        public static void creatFile(String recordLocation)
        {
            RichTextBox richTextBox = new RichTextBox();
            richTextBox.SaveFile(recordLocation);
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
                dialog.Filter = "*" + Note1.getFormat() + "|*" + Note1.getFormat();

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

    }
}
