using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;  // <-- Tambahkan ini
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    //[ApiController]
    [Route("LoadingOrder/api/[controller]")]
    [Authorize]  // <-- Tambahkan ini agar semua endpoint di controller ini butuh autentikasi JWT
    //[ApiExplorerSettings(GroupName = "LoadingOrder")]
    public class GetLoadingOrderController : ControllerBase
    {
        private readonly IListDOService _service;
        private readonly IHttpClientFactory _httpClientFactory;

        public GetLoadingOrderController(IListDOService service, IHttpClientFactory httpClientFactory)
        {
            _service = service;
            _httpClientFactory = httpClientFactory;
        }

        // Endpoint utama, mendukung query OData, hanya bisa diakses kalau ada token valid
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var publicIp = await client.GetStringAsync("http://checkip.amazonaws.com/");
                publicIp = publicIp.Trim();
            }
            catch
            {
                return StatusCode(500, "Gagal mengambil IP publik dari AWS.");
            }

            var list = _service.GetListDO().AsQueryable();

            return Ok(list);
        }

        // Endpoint untuk mendapatkan IP publik server, juga butuh token valid
        [HttpGet("server-ip")]
        public async Task<IActionResult> GetServerPublicIp()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var publicIp = await client.GetStringAsync("http://checkip.amazonaws.com/");
                publicIp = publicIp.Trim();

                return Ok(new { PublicIp = publicIp });
            }
            catch
            {
                return StatusCode(500, "Gagal mengambil IP publik dari AWS.");
            }
        }
    }
}
