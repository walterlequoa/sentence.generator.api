using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sentence.generator.api.IServices;
using sentence.generator.api.RequestModel;

namespace sentence.generator.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WordController : BaseApiController
    {
        public readonly IWordService wordService;
        public WordController(IWordService wordService)
        {
            this.wordService = wordService;
        }

        

        [HttpPost]
        [Route("generate")]
        public async Task<IActionResult> Index([FromBody] WordRequestModel wordRequestModel)
        {
            var words = await wordService.GetWords(wordRequestModel.PartOfSpeech, wordRequestModel.Limit);
            return Ok(words);
        }
    }
}
