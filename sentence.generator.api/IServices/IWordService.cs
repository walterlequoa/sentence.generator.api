using sentence.generator.api.RequestModel;

namespace sentence.generator.api.IServices
{
    public interface IWordService
    {
        Task<WordList> GetWords(string partOfSpeech, int limit);
    }
}
