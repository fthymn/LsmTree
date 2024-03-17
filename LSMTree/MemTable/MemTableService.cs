using System;
using LSMTree.CommitLogService;
using LSMTree.Entity;
using LSMTree.MemTable;
using LSMTree.SSTable;

namespace LSMTree.MemTable
{
	public class MemTableService : IMemTableService
    {
        private List<LsmTreeEntity> lsmTreeEntityList;

        public MemTableService()
		{
            lsmTreeEntityList = new List<LsmTreeEntity>();
		}

        public void Load(List<LsmTreeEntity> lsmTreeEntities)
        {
            this.lsmTreeEntityList = lsmTreeEntities;
        }

        public void Delete(int id, string TOMBSTONE_VALUE)
        {
            var lsmObject = new LsmTreeEntity() { id = id, value = TOMBSTONE_VALUE };
            this.Put(lsmObject);
        }

        public LsmTreeEntity Read(int id)
        {
           return lsmTreeEntityList.LastOrDefault(f => f.id == id);
        }

        public void Put(LsmTreeEntity lsmTreeEntity)
        {
            lsmTreeEntityList.Add(lsmTreeEntity);
        }

        public void Clear()
        {
            lsmTreeEntityList.Clear();
        }

        public int GetSize() => this.lsmTreeEntityList.Count();

        public List<LsmTreeEntity> GetList() => this.lsmTreeEntityList;
    }
}

