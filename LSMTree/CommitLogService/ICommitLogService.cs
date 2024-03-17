using System;
using LSMTree.Entity;

namespace LSMTree.CommitLogService
{
	public interface ICommitLogService
	{
		void ResetCommitLogFile();
		List<LsmTreeEntity> LoadCommitLogFile();
		void AppendCommitLogFile(LsmTreeEntity lsmTreeEntity);
	}
}

