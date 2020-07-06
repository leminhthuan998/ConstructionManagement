using Microsoft.AspNetCore.Mvc;
using ConstructionApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ConstructionApp.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;

namespace ConstructionApp.Controllers
{
    [Authorize]
    [Route("/api/sai-so")]
    public class SaiSoController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<SaiSo> _repository;

        public SaiSoController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            _repository = _dbContext.Set<SaiSo>();
        }

        [HttpGet("index")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<SaiSo>>))]
        public async Task<IActionResult> IndexAction()
        {
            var results = await _repository.ToListAsync();
            var newRs = new List<SaiSo>();

            foreach(var item in results)
            {
                var thongTinMeTron = await _dbContext.Set<ThongTinMeTron>().FirstAsync(x => x.Id.Equals(item.ThongTinMeTronId));
                item.ThongTinMeTron = thongTinMeTron;
                newRs.Add(item);
            }

            return Ok(ApiResponse<List<SaiSo>>.ApiOk(newRs));
        }
    }
}