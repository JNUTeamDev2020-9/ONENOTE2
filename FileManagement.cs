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
        
        public static string getRecordLocation()
        {
            string Path = Environment.CurrentDirectory;
            int index = Path.LastIndexOf('\\');
            Path = Path.Remove(index, 6);
            index = Path.LastIndexOf('\\');
            Path = Path.Remove(index, 4);
            return Path;
        }

        public static KonwledgeBaseManagement loadKnowledgeBaseManagement() //加载知识库
        {
            KonwledgeBaseManagement konwledgeBaseManagement;
            String path = getRecordLocation();
            if (File.Exists(getRecordLocation() + @"\KBM.xml"))
            {//如果存在就加载
                XMLTransformation xMLTransformation = new XMLTransformation();
                konwledgeBaseManagement = xMLTransformation.deserialization(path, "KBM");
            }
            else
            {
                konwledgeBaseManagement = new KonwledgeBaseManagement();
                Note note1 = new Note("n1", @"E:\新桌面\团队项目开发\kb1");
                Note note2 = new Note("n2", @"E:\新桌面\团队项目开发\kb1");
                KnowledgeBase kb = new KnowledgeBase(@"E:\新桌面\团队项目开发\kb1");
                kb.addNote(note1);
                kb.addNote(note2);
                konwledgeBaseManagement.addKnowledgeBase(kb);
            }
            return konwledgeBaseManagement;
        }

        public static void addKBDialog(KonwledgeBaseManagement konwledgeBaseManagement)//导入或新建知识库
        {
            String selectedPath = showFolderBrowserDialog("添加知识库");
            if (null != selectedPath) {
                if (konwledgeBaseManagement.exist(selectedPath))
                {
                    MessageBox.Show("请勿重复添加知识库");
                    return;
                }
                konwledgeBaseManagement.addKnowledgeBase(newKB(selectedPath));
            }
        }

        private static String showFolderBrowserDialog(String description) {
            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();//文件夹选择框
                dialog.Description = description;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.SelectedPath;
                }
                return null;
            }
            catch { 
                MessageBox.Show("文件夹选择器打开失败");
                return null;
            }
        }
        
        private static KnowledgeBase newKB(String selectedPath)
        {
            KnowledgeBase knowledgeBase = new KnowledgeBase(selectedPath);//新建了一个知识库
            DirectoryInfo TheFolder = new DirectoryInfo(selectedPath);
            foreach (FileInfo fi in TheFolder.GetFiles())//遍历文件夹下所有文件
            {
                if (fi.Name.EndsWith(Note.getFormat()))
                {
                    Note note = new Note(fi.Name, knowledgeBase.getRecordLocation());
                    knowledgeBase.addNote(note);

                }
            }
            return knowledgeBase;
        }
        
        public static void addNoteDialog(KnowledgeBase knowledgeBase)//添加笔记
        {//导入
            if (null != knowledgeBase)
            {
                String[] fileNames = showOpenFileDialog();
                if(null != fileNames)
                {
                    addNote(fileNames, knowledgeBase);
                }
            }
            else
            {
                MessageBox.Show("请先选择知识库");
            }
        }
        
        private static string[] showOpenFileDialog()
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();//创建打开文件对话框对象
                dialog.Filter = "*"+Note.getFormat()+"|*"+Note.getFormat();//设置搜索的文件格式
                dialog.Multiselect = true;//可以实现多选
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
        
        private static void addNote(String[] fileNames, KnowledgeBase knowledgeBase)
        {
            int index = fileNames[0].LastIndexOf(@"\") + 1;
            for (int i = 0; i < fileNames.Length; i++)
            {
                String name = fileNames[i].Substring(index, fileNames[i].Length - index);
                Note note = new Note(name, knowledgeBase.getRecordLocation());
                knowledgeBase.addNote(note);
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
                File.Copy(fileNames[i], note.getRecordLocation());//另存
            }
        } 

        public static void newNote(KnowledgeBase knowledgeBase, String noteName)
        {//新建
            if (null != knowledgeBase)
            {
                Note note = new Note(noteName, knowledgeBase.getRecordLocation());
                knowledgeBase.addNote(note);
            }
            else
            {
                MessageBox.Show("请先选中知识库");
            }

        }

        public static void exportNote(Note note)//导出笔记
        {
            String selectedPath = showFolderBrowserDialog("导出笔记");
            String filePath = selectedPath + @"\" + note.getName() + Note.getFormat();
            if (File.Exists(filePath))
            {
                int answer = (int)MessageBox.Show("知识库中已有名为" + note.getName() + "的文件，是否替换", "替换提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (6 == answer)//返回值 是6 否7 取消2
                {
                    File.Delete(filePath);
                    File.Copy(note.getRecordLocation(), filePath);
                }
            }
            else
            {
                File.Copy(note.getRecordLocation(), filePath);
            }
        }

        public static void exportKB(KnowledgeBase knowledgeBase)//导出知识库
        {
            String selectedPath = showFolderBrowserDialog("导出笔记");
            String directoryPath = selectedPath + @"\" + knowledgeBase.getName();
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
            else
            {
                Directory.CreateDirectory(directoryPath);
                knowledgeBase.copyNotes(directoryPath);
            }
        }

        public static void saveKBM(KonwledgeBaseManagement konwledgeBaseManagement)
        {
            String Path = getRecordLocation();

            if (File.Exists(Path + @"\KBM.xml"))
            {//如果存在就加载
                File.Delete(Path + @"\KBM.xml");
            }
            XMLTransformation xMLTransformation = new XMLTransformation();
            xMLTransformation.serialize(Path, "KBM", konwledgeBaseManagement);
        }

    }
}
