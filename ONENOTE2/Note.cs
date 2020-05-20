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
    class Note
    {
        private static String format = ".rtf";
        private String name;//n1
        private String recordLocation;
        public Note(String name,String recordLocation) {
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
        public String getName() {
            return name;
        }
        public String getRecordLocation() {
            return recordLocation;
        }
        public static String getFormat()
        {
            return format;
        }
        
        
    }
}
