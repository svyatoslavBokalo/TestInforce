using Backend.BusinessMethod;
using Backend.Models;
using Backend.RepositoryFolder;
using Backend.Services.Interfaces;

namespace BackendServices.Realization
{
    public class UrlService : IUrlService
    {

        private readonly IGenericRepository<UrlInfo> repository;
        public UrlService(IGenericRepository<UrlInfo> repository)
        {
            this.repository = repository;
        }

        public async Task AddUrl(string url, string userName)
        {
            WebsiteParser parser = new WebsiteParser();
            WebsiteInfo websiteInfo = parser.ParseWebsite(url);
            UrlInfo urlInfo = new UrlInfo()
            {
                UrlString = WebsiteParser.DecodedUrl(url),
                ShortUrl = websiteInfo.ShortUrl,
                Title = websiteInfo.Title,
                Description = websiteInfo.Description,
                CreatedBy = userName
            };

            repository.AddItem(urlInfo).Wait();
        }

        public async Task DeleteUrl(int id)
        {
            await repository.DeleteItemId(id);
        }

        public async Task<List<UrlInfo>> GetAllUrl()
        {
            return await repository.GetAll();
        }

        public async Task<UrlInfo> GetUrl(string url)
        {
            return await repository.GetItem(WebsiteParser.DecodedUrl(url), "UrlString");
        }

        public async Task<bool> IfExistUrl(string url)
        {
            List<UrlInfo> lst = await repository.GetAll();
            if(lst.FirstOrDefault(el=>el.UrlString == WebsiteParser.DecodedUrl(url)) != null)
            {
                return true;
            }
            return false;
        }
    }
}
