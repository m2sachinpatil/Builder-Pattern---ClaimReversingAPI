using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abstract_Layer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClaimReversingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IClaimDirector _director;
        private readonly IClaimBuilder _builder;

        public HomeController(ILogger<HomeController> logger, IClaimDirector director, IClaimBuilder builder)
        {
            _logger = logger;
            _director = director;
            _builder = builder;
        }

        [HttpPost("UploadFile")]
        public async Task<List<string>> PostFileAsync(IFormFile file)
        {
            try
            {
                var lines = new List<string>();
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        lines.Add(await reader.ReadLineAsync());
                }

                if (lines.Count <= 1) return null;
                _director.SetClaimBuilder(_builder);
                _director.BuildClaim(lines);
                var resultData = _builder.GetData();

                return resultData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
