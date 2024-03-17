using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSMTree.Entity;
using LSMTree.DataStoreService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LSMTree.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LSMTreeController : ControllerBase
    {
        private readonly IDataStoreService _dataStoreService;
        public LSMTreeController(IDataStoreService dataStoreService)
        {
            _dataStoreService = dataStoreService;
        }

        [HttpPost]
        public IActionResult Create(LsmTreeEntity lSMTreeEntity)
        {
            _dataStoreService.Create(lSMTreeEntity);
            return Ok();
        }

        [HttpGet]
        public LsmTreeEntity Read(int id)
        {
            return _dataStoreService.Read(id);
        }

        [HttpPut]
        public IActionResult Update(LsmTreeEntity lsmTreeEntity)
        {
            _dataStoreService.Update(lsmTreeEntity);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _dataStoreService.Delete(id);
            return Ok();
        }


        [HttpPost]
        public IActionResult Compaction()
        {
            return Ok(_dataStoreService.Compaction());
        }
    }
}

