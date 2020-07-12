using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ConstructionApp.Dto.ThongTinMeTronDto;
using ConstructionApp.Entity;
using ConstructionApp.Entity.Identity;
using ConstructionApp.Service.MacService;
using ConstructionApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ConstructionApp.Controllers
{
    [Authorize]
    [Route("/api/thong-tin-me-tron")]
    public class ThongTinMeTronController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<ThongTinMeTron> _repository;
        private readonly DbSet<ThanhPhanMeTronDat> _dat;
        private readonly DbSet<ThanhPhanMeTronCan> _can;
        private readonly DbSet<CapPhoi> _capPhoi;
        private readonly DbSet<SaiSo> _saiSo;
        // private readonly IConcreteService _concreteService;
        private readonly UserManager<User> _userManager;
        public ThongTinMeTronController(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            this._userManager = userManager;
            // _concreteService = concreteService;
            this._dbContext = dbContext;
            _repository = _dbContext.Set<ThongTinMeTron>();
            _dat = _dbContext.Set<ThanhPhanMeTronDat>();
            _can = _dbContext.Set<ThanhPhanMeTronCan>();
            _capPhoi = _dbContext.Set<CapPhoi>();
            _saiSo = _dbContext.Set<SaiSo>();
        }

        [HttpGet("index")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<ThongTinMeTron>>))]
        public async Task<IActionResult> IndexAction()
        {
            var results = await _repository.ToListAsync();
            var newRs = new List<ThongTinMeTron>();
            foreach (var item in results)
            {
                var mac = await _dbContext.Set<MAC>().FirstAsync(x => x.Id == item.MacId);
                var vehicle = await _dbContext.Set<Vehicle>().FirstAsync(x => x.Id == item.VehicleId);
                var hopDong = await _dbContext.Set<HopDong>().FirstAsync(x => x.Id == item.HopDongId);
                item.MAC = mac;
                item.HopDong = hopDong;
                item.Vehicle = vehicle;
                newRs.Add(item);
            }

            return Ok(ApiResponse<List<ThongTinMeTron>>.ApiOk(newRs));
        }

        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<ThongTinMeTron>))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]

        public async Task<IActionResult> CreateAction(
            [FromBody] InputCreateTTMTDto dto,
            [FromServices] IHttpContextAccessor httpContext
            )
        {
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse<ModelStateDictionary>.ApiError(ModelState));
            }

            // httpContext.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(httpContext.HttpContext.User.Identity.Name);


            // check thông tin mẻ trộn này đã được add hay chưa

            var find = await _dbContext.Set<ThongTinMeTron>().Where(x => x.VehicleId.Equals(dto.VehicleId)
            && x.MacId.Equals(dto.MacId)
            && x.HopDongId.Equals(dto.HopDongId)
            && x.NgayTron.Equals(dto.NgayTron)
            && x.KhoiLuong.Equals(dto.KhoiLuong))
            .CountAsync();
            if (find > 0)
            {
                ModelState.AddModelError(nameof(dto.NgayTron), "Thông tin mẻ trộn này đã được tạo trên hệ thống");
            }

            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse<object>.ApiError(ModelState));
            }

            // var mac = await _dbContext.Set<MAC>().FirstAsync(x => x.Id == dto.MacId);
            // var hopDong = await _dbContext.Set<HopDong>().FirstAsync(x => x.Id == dto.HopDongId);
            // var vehicle = await _dbContext.Set<Vehicle>().FirstAsync(x => x.Id == dto.VehicleId);

            // dto.MacId = mac;
            // dto.HopDongId = hopDong;
            // dto.VehicleId = vehicle;

            var newTTMT = InputCreateTTMTDto.ToEntity(dto);
            newTTMT.UserId = user.Id;
            await _dbContext.Set<ThongTinMeTron>().AddAsync(newTTMT);
            SaiSo saiSo = new SaiSo()
            {
                ThongTinMeTron = newTTMT,
                ThongTinMeTronId = newTTMT.Id
            };
            await _dbContext.Set<SaiSo>().AddAsync(saiSo);
            ConcreteService service = new ConcreteService(_dbContext);
            await service.TaoMeTron(newTTMT);

            await _dbContext.SaveChangesAsync();

            return Ok(ApiResponse<ThongTinMeTron>.ApiOk(newTTMT));
        }

        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<ThongTinMeTron>))]
        public async Task<IActionResult> UpdateAction([FromBody] InputUpdateTTMTDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse<ModelStateDictionary>.ApiError(ModelState));
            }

            var thongTinMeTron = await _repository.FirstAsync(x => x.Id.Equals(dto.Id));

            var mac = await _dbContext.Set<MAC>().FirstAsync(x => x.Id == dto.MacId);
            var hopDong = await _dbContext.Set<HopDong>().FirstAsync(x => x.Id == dto.HopDongId);
            var vehicle = await _dbContext.Set<Vehicle>().FirstAsync(x => x.Id == dto.VehicleId);

            thongTinMeTron.MAC = mac;
            thongTinMeTron.HopDong = hopDong;
            thongTinMeTron.Vehicle = vehicle;

            InputUpdateTTMTDto.UpdateEntity(dto, thongTinMeTron);
            _repository.Update(thongTinMeTron);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<ThongTinMeTron>.ApiOk(thongTinMeTron));
        }


        [HttpPost("delete")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> DeleteAction(Guid Id)
        {
            // check đã được add hay chưa
            var find = await _repository.Where(x => x.Id.Equals(Id)).FirstAsync();

            _repository.Remove(find);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<string>.ApiOk("Xoá thành công"));
        }
    }
}