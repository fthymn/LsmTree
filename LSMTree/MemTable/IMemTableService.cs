using System;
using LSMTree.Entity;

namespace LSMTree.MemTable
{
	public interface IMemTableService
	{
		void Load(List<LsmTreeEntity> lsmTreeEntities);
        void Put(LsmTreeEntity lsmTreeEntity);
		LsmTreeEntity Read(int id);
		void Delete(int id, string TOMBSTONE_VALUE);
		void Clear();
		int GetSize();
        List<LsmTreeEntity> GetList();

    }
}

