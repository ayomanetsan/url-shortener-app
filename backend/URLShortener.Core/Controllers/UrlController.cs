using Microsoft.AspNetCore.Mvc;
using System;
using URLShortener.Core.BLL.Interfaces;
using URLShortener.Core.Common.DTO;
using URLShortener.Core.DAL.Entitites;

namespace URLShortener.Core.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlController : Controller
    {
        private readonly IUrlService _urlService;

        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<ICollection<Url>>> GetAll()
        {
            return Ok(await _urlService.GetUrls());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Url>> Get(uint id)
        {
            try
            {
                var dbUrl = await _urlService.GetUrl(id);
                return Ok(dbUrl);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("shorten")]
        public async Task<ActionResult<Url>> Shorten([FromBody] RequestUrl url)
        {
            try
            {
                var dbUrl = await _urlService.ShortenUrl(url.Url, HttpContext.User.Identity.Name);
                return Ok(dbUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ICollection<Url>>> Delete(string shortUrl)
        {
            try
            {
                var urlList = await _urlService.DeleteUrl(shortUrl);
                return Ok(urlList);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("redirect/{shortUrl}")]
        public async Task<ActionResult<RequestUrl>> Redirect(string shortUrl)
        {
            try
            {
                var dbUrl = await _urlService.Redirect(shortUrl);
                return Ok(new RequestUrl { Url = dbUrl });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
