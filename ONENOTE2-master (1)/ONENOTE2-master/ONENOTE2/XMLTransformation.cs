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
        /// <summary>
        /// 将一个KnowLedgeBaseManagement对象序列化为XML文件
        /// 第一个参数为存储路径，第二个参数为存储文件名称，第三个参数是要序列化的对象
        /// 可以改为static
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="kbm"></param>
        public void serialize(String path, String fileName, KonwledgeBaseManagement kbm)
        {
            //实例一个序列化对象
            IFormatter binaryFormatter = new BinaryFormatter();

            //创建文件流
            Stream infileStream = new FileStream(path + "\\" + fileName + ".xml", FileMode.Create, FileAccess.Write, FileShare.None);

            //序列化对象    
            binaryFormatter.Serialize(infileStream, kbm);

            //关闭文件流
            infileStream.Close();                                                                                                                   
        }

        /// <summary>
        /// 从指定的XML文件中反序列化并返回一个将一个KnowLedgeBaseManagement对象
        /// 第一个参数为存储路径，第二个参数为存储文件名称
        /// 可以改为static
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public KonwledgeBaseManagement deserialization(string path, string fileName)
        {
            try
            {
                //实例一个序列化对象
                IFormatter formatter = new BinaryFormatter();     
                
                //创建文件流
                Stream outfileStream = new FileStream(path + "\\" + fileName + ".xml", FileMode.Open, FileAccess.Read, FileShare.Read);

                //反序列化对象
                KonwledgeBaseManagement obj = (KonwledgeBaseManagement)formatter.Deserialize(outfileStream);

                //关闭文件流
                outfileStream.Close();

                return obj;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
