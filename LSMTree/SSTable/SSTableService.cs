using System;
using LSMTree.Entity;
using Newtonsoft.Json;

namespace LSMTree.SSTable
{
    public class SSTableService : ISSTableService
    {
        public SSTableService()
        {

        }

        public List<LsmTreeEntity> Compaction()
        {
            var fileList = Directory.GetFiles(Directory.GetCurrentDirectory() + "/Files", "ssTable_*.json");
            var actualDataForSSTableFile = new List<LsmTreeEntity>();

            //dosya bazında uygula
            foreach (var ssTableFileItem in fileList)
            {
                var fileText = File.ReadAllText(ssTableFileItem);
                var lsmObjects = JsonConvert.DeserializeObject<List<LsmTreeEntity>>(fileText);
                for (int i = lsmObjects.Count - 1; i >= 0; i--)
                {
                    var recordId = lsmObjects[i].id;

                    for (int j = 0; j < i; j++)
                    {
                        if (lsmObjects[j].id == recordId)
                        {
                            lsmObjects.RemoveAt(j);
                            i--;
                            j--;
                        }
                    }
                }

                var dataJsonStr = JsonConvert.SerializeObject(lsmObjects);
                File.WriteAllText(ssTableFileItem, dataJsonStr);
                actualDataForSSTableFile.AddRange(lsmObjects);
            }

            //dosya bazında elde kalan datalara uygula
            for (int i = actualDataForSSTableFile.Count - 1; i >= 0; i--)
            {
                var recordId = actualDataForSSTableFile[i].id;
                for (int j = 0; j < i; j++)
                {
                    if (actualDataForSSTableFile[j].id == recordId)
                    {
                        actualDataForSSTableFile.RemoveAt(j);
                        i--;
                        j--;
                    }
                }
            }

            foreach (var ssTableFileItem in fileList)
            {
                File.Delete(ssTableFileItem);
            }

            Flush(actualDataForSSTableFile);

            return actualDataForSSTableFile;
        }

        public void Flush(List<LsmTreeEntity> lsmTreeEntityList)
        {
            var ssTableFileName = Directory.GetCurrentDirectory() + "/Files/" + $"ssTable_{DateTime.Now.Ticks}.json";
            var dataJsonStr = JsonConvert.SerializeObject(lsmTreeEntityList);
            File.WriteAllText(ssTableFileName, dataJsonStr);
        }

        public LsmTreeEntity Get(int id)
        {
            var fileList = Directory.GetFiles(Directory.GetCurrentDirectory() + "/Files", "ssTable_*.json");
            foreach (var ssTableFileItem in fileList.OrderByDescending(o => o))
            {
                var fileText = File.ReadAllText(ssTableFileItem);
                var lsmObjects = JsonConvert.DeserializeObject<List<LsmTreeEntity>>(fileText);
                for (int i = lsmObjects.Count - 1; i >= 0; i--)
                {
                    if (id == lsmObjects[i].id)
                    {
                        return lsmObjects[i];
                    }
                }
            }
            return null;
        }
    }
}
