using Microsoft.AspNetCore.Mvc;
using ConstructionApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ConstructionApp.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using ConstructionApp.Dto.ThanhPhanCanDto;
using ConstructionApp.Service.MacService;

namespace ConstructionApp.Controllers
{
    [Authorize]
    [Route("/api/thanh-phan-can")]
    public class ThanhPhanCanController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<ThanhPhanMeTronCan> _repository;

        public ThanhPhanCanController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            _repository = _dbContext.Set<ThanhPhanMeTronCan>();
        }

        [HttpGet("index")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<ThanhPhanMeTronCan>>))]
        public async Task<IActionResult> IndexAction()
        {
            var results = await _repository.ToListAsync();
            var newRs = new List<ThanhPhanMeTronCan>();

            foreach(var item in results)
            {
                var thongTinMeTron = await _dbContext.Set<ThongTinMeTron>().FirstAsync(x => x.Id.Equals(item.ThongTinMeTronId));
                var mac = await _dbContext.Set<MAC>().FirstAsync(x => x.Id == thongTinMeTron.MacId);
                var vehicle = await _dbContext.Set<Vehicle>().FirstAsync(x => x.Id == thongTinMeTron.VehicleId);
                var hopDong = await _dbContext.Set<HopDong>().FirstAsync(x => x.Id == thongTinMeTron.HopDongId);

                item.ThongTinMeTron = thongTinMeTron;
                item.ThongTinMeTron.MAC = mac;
                item.ThongTinMeTron.Vehicle = vehicle;
                item.ThongTinMeTron.HopDong = hopDong;
                newRs.Add(item);
            }

            return Ok(ApiResponse<List<ThanhPhanMeTronCan>>.ApiOk(newRs));
        }

        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<ThanhPhanMeTronCan>))]
        public async Task<IActionResult> UpdateAction([FromBody] InputUpdateThanhPhanCanDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse<ModelStateDictionary>.ApiError(ModelState));
            }
            
            var thanhPhanCan = await _repository.FirstAsync(x => x.Id.Equals(dto.Id));
            var thongTinMeTron = await _dbContext.Set<ThongTinMeTron>().FirstAsync(x => x.Id.Equals(thanhPhanCan.ThongTinMeTronId));

            // thanhPhanCan.ThongTinMeTronId = thongTinMeTron.Id;


            InputUpdateThanhPhanCanDto.UpdateEntity(dto, thanhPhanCan);
            _repository.Update(thanhPhanCan);
            await _dbContext.SaveChangesAsync();
            ConcreteService service = new ConcreteService(_dbContext);
            await service.CapnhatThanhPhanMeTronCan(thongTinMeTron);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<ThanhPhanMeTronCan>.ApiOk(thanhPhanCan));
        }
    }
}