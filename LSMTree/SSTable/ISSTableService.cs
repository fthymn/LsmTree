using System;
using LSMTree.Entity;

namespace LSMTree.SSTable
{
    public interface ISSTableService
    {
        void Flush(List<LsmTreeEntity> lsmTreeEntityList);
        LsmTreeEntity Get(int id);
        List<LsmTreeEntity> Compaction();
    }
}

