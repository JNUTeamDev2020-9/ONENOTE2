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
        private List<Note> notes = null;//笔记的名字
        private String recordLocation;//文件存储位置 E:\新桌面\团队项目开发\kb1
        private String name;//kb1
        public KnowledgeBase(String recordLocation)//导入的时候
        {
            this.recordLocation = recordLocation;
            int index = recordLocation.LastIndexOf(@"\") + 1;
            name = recordLocation.Substring(index, recordLocation.Length-index);
            
        }
        public KnowledgeBase(String recordLocation,String name)//新建的时候
        {
            this.recordLocation = recordLocation + @"\" + name;
            this.name = name;

        }

        public void addNote(Note note)
        {
            if(null == notes)
            {
                notes = new List<Note>();
            }
            notes.Add(note);
            
        }
        public String getName()
        {
            return name;
        }
        public string getRecordLocation() {
            return recordLocation;
        }
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
        public void copyNotes(String directoryName)//要复制到的文件夹的名字
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
        public List<Note> GetNotes()
        {
            return notes;
        }
        public Boolean existNote(String path)
        {
            foreach(Note note in notes)
            {
                if (path.Equals(note.getRecordLocation())) return true;
            }
            return false;
        }

    }
}
