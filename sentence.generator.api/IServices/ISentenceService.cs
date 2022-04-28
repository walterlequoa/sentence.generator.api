using sentence.generator.api.RequestModel;

namespace sentence.generator.api.IServices
{
    public interface ISentenceService
    {
        Task<List<Sentence>> GetSentenceByUser(string userId);
        Task SaveSentence(Sentence sentence);
        Task<bool> DeleteSentence(int sentenceId);
    }
}
