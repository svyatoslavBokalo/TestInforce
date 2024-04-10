using Backend.Models;

namespace Backend.Services.Interfaces
{
    public interface IUrlService
    {
        Task<List<UrlInfo>> GetAllUrl();
        Task AddUrl(string url, string userName);
        Task DeleteUrl(int id);
        Task<bool> IfExistUrl(string url);
        Task<UrlInfo> GetUrl(string url);

    }
}
