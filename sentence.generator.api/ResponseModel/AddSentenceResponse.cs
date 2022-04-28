using sentence.generator.api.RequestModel;

namespace sentence.generator.api.ResponseModel
{
    public class AddSentenceResponse : BaseResponse
    {
        public Sentence sentence { get; set; }
    }
}
