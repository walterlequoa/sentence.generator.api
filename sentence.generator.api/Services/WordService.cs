using sentence.generator.api.IServices;
using sentence.generator.api.RequestModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace sentence.generator.api.Services
{
    public class WordService : IWordService
    {
        public WordService(HttpService httpService)
        {
            service = httpService;
            client = new HttpClient();
        }

        private readonly HttpService service;
        private readonly HttpClient client;

        public async Task<WordList> GetWords(string partOfSpeech, int limit)
        {
            string result = string.Empty;
            WordList wordsList = new WordList();
            var url = $"{service.BaseUrl}hasDictionaryDef=true&includePartOfSpeech={partOfSpeech}&limit={limit}&api_key={service.ApiKey}";

            try
            {
                using var webRequest = new HttpRequestMessage(HttpMethod.Get, new Uri(url));

                var response = await client.SendAsync(webRequest);
                
                using (Stream stream = await response.Content.ReadAsStreamAsync())
                {
                    result = new StreamReader(stream).ReadToEnd();
                }

                var options = new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = false,
                    NumberHandling = JsonNumberHandling.AllowReadingFromString
                };
                wordsList = JsonSerializer.Deserialize<WordList>(result, options);

            } catch (Exception) {
                throw;
            }
            
            return wordsList;
        }
    }
}
