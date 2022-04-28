using Microsoft.EntityFrameworkCore;
using sentence.generator.api.Data;
using sentence.generator.api.IServices;
using sentence.generator.api.RequestModel;

namespace sentence.generator.api.Services
{
    public class SentenceService : ISentenceService
    {
        private readonly ApplicationDbContext _context;

        public SentenceService(ApplicationDbContext dbContext)
        {
            this._context = dbContext;
        }
        public async Task<bool> DeleteSentence(int sentenceId)
        {
            var sentence = await _context.Sentences.FindAsync(sentenceId);
            if (sentence == null)
            {
                return false;
            }
            else
            {
                _context.Sentences.Remove(sentence);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<Sentence>> GetSentenceByUser(string userId)
        {
            var sentences = await _context.Sentences.Include(x => x.User).Where(y => y.User.Id == userId).ToListAsync();
            return sentences;
        }

        public async Task SaveSentence(Sentence sentence)
        {
            await _context.Sentences.AddAsync(sentence);
            await _context.SaveChangesAsync();
        }
    }
}
