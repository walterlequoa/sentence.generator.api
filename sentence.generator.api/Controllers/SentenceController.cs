using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sentence.generator.api.IServices;
using sentence.generator.api.RequestModel;

namespace sentence.generator.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SentenceController : BaseApiController
    {
        private readonly ISentenceService _sentenceService;

        public SentenceController(ISentenceService sentenceService)
        {
            this._sentenceService = sentenceService;
        }

        [HttpGet]
        [Route("{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserSentences([FromRoute] string userId)
        {
            var sentences = await _sentenceService.GetSentenceByUser(userId);
            if (!sentences.Any()) return NotFound($"No sentences found.");
            return Ok(sentences);
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> AddSentence([FromBody] SentenceRequestModel sentenceRequestModel)
        {
            var sentence = new Sentence
            {
                Words = sentenceRequestModel.Sentence,
                User = new ApplicationUser
                {
                    Id = sentenceRequestModel.userId
                }
            };

            await _sentenceService.SaveSentence(sentence);
            return Ok(sentence);
        }
    }
}
