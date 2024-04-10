using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlInfoController : ControllerBase
    {
        private readonly IUrlService _urlService;

        public UrlInfoController(IUrlService urlService)
        {
            this._urlService = urlService;
        }


        [HttpGet("GetAllUrl")]
        public async Task<JsonResult> GetAllUrl()
        {
            List<UrlInfo> list = await _urlService.GetAllUrl();
            return new JsonResult(list);
        }

        [HttpGet("IfExist/{url}")]
        public async Task<JsonResult> IfExist(string url)
        {
            if(await _urlService.IfExistUrl(url))
            {
                return new JsonResult("true");
            }
            return new JsonResult("false"); 
        }

        [HttpGet("GetUrl/{url}")]
        public async Task<JsonResult> GetUrl(string url)
        {
            if (await _urlService.IfExistUrl(url))
            {
                return new JsonResult(await _urlService.GetUrl(url));
            }
            return new JsonResult("not founded");
        }

        [HttpPost("AddNewUrl/{Url},{userName}")]
        public async Task<IActionResult> AddNewUrl(string Url, string userName)
        {
            try
            {
                await _urlService.AddUrl(Url, userName);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _urlService.DeleteUrl(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        
    }
}
