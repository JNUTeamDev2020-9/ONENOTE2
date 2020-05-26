using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONENOTE2
{
    [Serializable()]
    class KnowledgeBase
    {
        #region 成员数据

        /// <summary>
        /// 知识库包含的笔记页
        /// </summary>
        // 
        private List<Note> notes = new List<Note>();

        /// <summary>
        /// 知识库对应的文件夹在文件系统中的路径
        /// </summary>
        private String recordLocation;

        /// <summary>
        /// 知识库的名字
        /// </summary>
        private String name;

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法：导入知识库
        /// </summary>
        /// <param name="recordLocation"></param>
        public KnowledgeBase(String recordLocation)
        {
            //知识库在文件系统中的路径
            this.recordLocation = recordLocation;

            //知识库名
            int index = recordLocation.LastIndexOf(@"\") + 1;
            name = recordLocation.Substring(index, recordLocation.Length-index);            
        }

        /// <summary>
        /// 构造方法：新建知识库
        /// </summary>
        /// <param name="recordLocation"></param>
        /// <param name="name"></param>
        public KnowledgeBase(String recordLocation,String name)
        {
            //知识库在文件系统中的路径
            this.recordLocation = recordLocation + @"\" + name;

            //知识库名
            this.name = name;
        }

        #endregion

        #region 成员方法

        /// <summary>
        /// 添加笔记页
        /// </summary>
        /// <param name="note"></param>
        public void addNote(Note note)
        {            
            notes.Add(note);            
        }

        /// <summary>
        /// 获取知识库名
        /// </summary>
        /// <returns></returns>
        public String getName()
        {
            return name;
        }

        /// <summary>
        /// 获取知识库路径
        /// </summary>
        /// <returns></returns>
        public string getRecordLocation() {
            return recordLocation;
        }

        /// <summary>
        /// 获取笔记页
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Note getNote(int index)
        {
            if(index < notes.Count)
            {
                return notes.ElementAt(index);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 复制知识库内所有笔记页到知识库directoryName中。
        /// directoryName是目标文件夹的名字
        /// </summary>
        /// <param name="directoryName"></param>
        public void copyNotes(String directoryName)
        {
            try
            {
                foreach (Note note in notes)
                {
                    String destFileName = directoryName + @"\" + note.getName() + Note.getFormat();
                    File.Copy(note.getRecordLocation(),destFileName,true);
                }
            }
            catch (Exception e)
            {
                StringBuilder m_sb = new StringBuilder();
                m_sb.Append(e.Message + "\n");
                m_sb.Append(directoryName);
                MessageBox.Show(m_sb.ToString());
            }
        }

        /// <summary>
        /// 获取笔记页列表
        /// </summary>
        /// <returns></returns>
        public List<Note> GetNotes()
        {
            return notes;
        }

        /// <summary>
        /// 判断该知识库中是否包含path指向的笔记页
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Boolean existNote(String path)
        {
            foreach(Note note in notes)
            {
                if (path.Equals(note.getRecordLocation())) return true;
            }
            return false;
        }

        #endregion

    }
}
