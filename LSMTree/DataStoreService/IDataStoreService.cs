using System;
using LSMTree.Entity;
using Microsoft.AspNetCore.Mvc;

namespace LSMTree.DataStoreService
{
	public interface IDataStoreService
	{
        void Create(LsmTreeEntity lsmTreeEntity);
        LsmTreeEntity Read(int id);
        void Update(LsmTreeEntity lsmTreeEntity);
        void Delete(int id);
        List<LsmTreeEntity> Compaction();
    }
}

