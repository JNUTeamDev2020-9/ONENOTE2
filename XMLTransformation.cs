using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ONENOTE2
{
    class XMLTransformation
    {
        public void serialize(String path, String fileName, KonwledgeBaseManagement kbm)//该函数用于将题库类序列化为XML文件，第一个参数为存储路径，第二个参数为存储文件名称，第三个参数是要序列化的管理类
        {
            IFormatter binaryFormatter = new BinaryFormatter();//实例一个序列化对象
            Stream infileStream = new FileStream(path + "\\" + fileName + ".xml", FileMode.Create, FileAccess.Write, FileShare.None);//创建文件流
            binaryFormatter.Serialize(infileStream, kbm);         //序列化对象                                       
            infileStream.Close();                                          //关闭文件流
                                                                           //以上为序列化题库
        }

        public KonwledgeBaseManagement deserialization(string path, string fileName)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();                  //实例一个序列化对象
                Stream outfileStream = new FileStream(path + "\\" + fileName + ".xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                KonwledgeBaseManagement obj = (KonwledgeBaseManagement)formatter.Deserialize(outfileStream);
                outfileStream.Close();
                //以上为反序列化
                return obj;
            }
            catch (Exception e)
            {
                throw new Exception("选择的XML文件格式不对，导入失败");
            }
        }
    }
}
