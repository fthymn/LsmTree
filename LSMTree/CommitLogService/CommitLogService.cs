using System;
using LSMTree.Entity;
using Newtonsoft.Json;

namespace LSMTree.CommitLogService
{
	public class CommitLogService:ICommitLogService
	{
        private readonly string commitFileName = Directory.GetCurrentDirectory()+ "/Files/commit.json";

        public CommitLogService()
		{
            if (!File.Exists(commitFileName))
                this.ResetCommitLogFile();
		}

        public void AppendCommitLogFile(LsmTreeEntity lsmTreeEntity)
        {
           
            var logList = LoadCommitLogFile();
            logList.Add(lsmTreeEntity);
            File.WriteAllText(commitFileName, JsonConvert.SerializeObject(logList));
        }

        public void ResetCommitLogFile()
        {
            File.WriteAllText(commitFileName, "[]");
        }


        public List<LsmTreeEntity> LoadCommitLogFile()
        {
            var commitLogJson = File.ReadAllText(commitFileName);
            return JsonConvert.DeserializeObject<List<LsmTreeEntity>>(commitLogJson);
        }
    }
}

