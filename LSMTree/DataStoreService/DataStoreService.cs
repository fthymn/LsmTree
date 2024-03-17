using System;
using LSMTree.CommitLogService;
using LSMTree.Entity;
using LSMTree.MemTable;
using LSMTree.SSTable;

namespace LSMTree.DataStoreService
{
    public class DataStoreService : IDataStoreService
    {
        private readonly int maxMemTableSize = 30;
        private readonly string TOMBSTONE_VALUE = "tombstone";

        private readonly ICommitLogService _commitLogService;
        private readonly ISSTableService _ssTableService;
        private readonly IMemTableService _memTableService;


        public DataStoreService(ICommitLogService commitLogService, IMemTableService memTableService, ISSTableService ssTableService)
        {
            _commitLogService = commitLogService;
            _memTableService = memTableService;
            _ssTableService = ssTableService;
            var getCommitLog = _commitLogService.LoadCommitLogFile();
            _memTableService.Load(getCommitLog);
        }

        public void Create(LsmTreeEntity lsmTreeEntity)
        {
            this.Put(lsmTreeEntity);
        }

        public void Delete(int id)
        {
            var lsmObject = new LsmTreeEntity() { id = id, value = TOMBSTONE_VALUE };
            this.Put(lsmObject);
        }

        public LsmTreeEntity Read(int id)
        {
            var memTableResult = _memTableService.Read(id);
            if (memTableResult != null)
            {
                return memTableResult.value == TOMBSTONE_VALUE ? null : memTableResult;
            }
            else
            {
                var ssTableResult = _ssTableService.Get(id);
                return ssTableResult != null && ssTableResult.value != TOMBSTONE_VALUE ? ssTableResult : null;
            }
        }

        public void Update(LsmTreeEntity lsmTreeEntity)
        {
            this.Put(lsmTreeEntity);
        }

        private void Put(LsmTreeEntity lsmTreeEntity)
        {
            if (_memTableService.GetSize() >= maxMemTableSize)
            {
                var lsmTree = _memTableService.GetList();
                _ssTableService.Flush(lsmTree);
                this.Clear();
            }

            _commitLogService.AppendCommitLogFile(lsmTreeEntity);
            _memTableService.Put(lsmTreeEntity);
        }

        private void Clear()
        {
            _commitLogService.ResetCommitLogFile();
            _memTableService.Clear();
        }

        public List<LsmTreeEntity> Compaction()
        {
            return _ssTableService.Compaction();
        }
    }
}

