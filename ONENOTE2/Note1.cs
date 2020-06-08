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
    class Note1
    {
        #region 构造方法
        /// <summary>
        /// Note构造函数，注意参数顺序
        /// </summary>
        /// <param name="name"></param>
        /// <param name="recordLocation"></param>
        public Note1(String name,String recordLocation) {
            if (name.EndsWith(format))
            {
                this.name = name.Substring(0, name.Length - format.Length);
                this.recordLocation = recordLocation + @"\" + name;
            }
            else {
                this.name = name;
                this.recordLocation = recordLocation + @"\" + name + format;
            }
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 返回笔记页名称
        /// </summary>
        /// <returns></returns>
        public String getName()
        {
            return name;
        }

        /// <summary>
        /// 返回笔记页文件位置
        /// </summary>
        /// <returns></returns>
        public String getRecordLocation() 
        {
            return recordLocation;
        }

        /// <summary>
        /// 返回笔记页文件的格式（扩展名）
        /// </summary>
        /// <returns></returns>
        public static String getFormat()
        {
            return format;
        }
        #endregion

        #region 成员数据
        /// <summary>
        /// 预设的笔记页格式（扩展名）
        /// </summary>
        private static String format = ".rtf";

        /// <summary>
        /// 笔记页名字（不带扩展名）
        /// </summary>
        private String name;

        /// <summary>
        /// 笔记页在文件系统中的存储位置
        /// </summary>
        private String recordLocation;
        #endregion

    }
}
