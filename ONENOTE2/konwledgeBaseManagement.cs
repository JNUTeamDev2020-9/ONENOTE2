using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONENOTE2
{
    [Serializable()]
    class KonwledgeBaseManagement
    {
        #region 成员数据 List<KnowledgeBase>
        List<KnowledgeBase1> knowledgeBases = new List<KnowledgeBase1>();
        #endregion

        #region 成员方法

        /// <summary>
        /// 向知识库列表（knowledgeBases）中添加新的知识库（KnowledgeBase）对象
        /// </summary>
        /// <param name="kb"></param>
        public void addKnowledgeBase(KnowledgeBase1 kb)
        {
            knowledgeBases.Add(kb);
        }

        /// <summary>
        /// 获取knowledgeBases列表
        /// </summary>
        /// <returns></returns>
        public List<KnowledgeBase1> getKBS()
        {
            return knowledgeBases;
        }

        /// <summary>
        /// 判断一个知识库对象（KnowledgeBase）是否已经存在于当前知识库列表（knowledgeBases）中
        /// </summary>
        /// <param name="knowledgeBase"></param>
        /// <returns></returns>
        public Boolean existKB(KnowledgeBase1 knowledgeBase)
        {
            String kbLocation = knowledgeBase.getRecordLocation();
            foreach (KnowledgeBase1 kb in knowledgeBases)
            {
                if (kb.getRecordLocation().Equals(kbLocation))
                { 
                    return true; 
                }
            }
            return false;
        }

        /// <summary>
        /// 获取列表中下标为index的知识库
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public KnowledgeBase1 getKB(int index)
        {
            if(index < knowledgeBases.Count)
            {
                return knowledgeBases.ElementAt(index);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 判断给定路径的笔记页是否在当前列表中任意一个知识库中。
        /// 若存在，则返回包含该笔记页的列表索引值最小的知识库的索引值；
        /// 否则返回-1.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        // 
        public int existNote(String path)
        {
            for(int i = 0; i < knowledgeBases.Count; i++)
            {
                if (knowledgeBases[i].existNote(path))
                {
                    return i;
                }
            }
            return -1;
        }
        
        #endregion
    }
}
