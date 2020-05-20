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
        List<KnowledgeBase> knowledgeBases = null;
        public void addKnowledgeBase(KnowledgeBase kb)
        {
            if(null == knowledgeBases)
            {
                knowledgeBases = new List<KnowledgeBase>();
            }
            knowledgeBases.Add(kb);
        }
        public List<KnowledgeBase> getKBS()
        {
            return knowledgeBases;
        }
        public Boolean existKB(KnowledgeBase knowledgeBase)
        {
            String kbLocation = knowledgeBase.getRecordLocation();
            foreach (KnowledgeBase kb in knowledgeBases)
            {
                if (kb.getRecordLocation().Equals(kbLocation)) return true;
            }
            return false;
        }
        public KnowledgeBase getKB(int index)
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
        public int existNote(String path)
        {
            for(int i = 0; i < knowledgeBases.Count; i++)
            {
                if (knowledgeBases[i].existNote(path)) return i;
            }
            return -1;
        }
    }
}
