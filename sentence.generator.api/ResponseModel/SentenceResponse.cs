using sentence.generator.api.RequestModel;

namespace sentence.generator.api.ResponseModel
{
    public class SentenceResponse : BaseResponse
    {
        public List<Sentence> sentences { get; set; }
    }
}
