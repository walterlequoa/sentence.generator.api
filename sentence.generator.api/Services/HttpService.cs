namespace sentence.generator.api.Services
{
    public class HttpService
    {
        private readonly string _url;
        private readonly string _apiKey;

        public HttpService(string url, string apiKey)
        {
            _url = url;
            _apiKey = apiKey;
        }

        public string BaseUrl { get { return _url; } }
        public string ApiKey { get { return _apiKey; } }    
    }
}
