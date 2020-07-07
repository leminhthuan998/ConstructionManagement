using ConstructionApp.Dto.VehicleDto;
using ConstructionApp.Entity;
using ConstructionApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionApp.Controllers
{
    [Authorize]
    [Route("/api/vehicle")]
    public class VehicleController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Vehicle> _repository;

        public VehicleController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            _repository = _dbContext.Set<Vehicle>();
        }


        [HttpGet("index")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<Vehicle>>))]
        public async Task<IActionResult> IndexAction()
        {
            // check biển số xe này đã được add hay chưa
            var results = await _repository.ToListAsync();
            return Ok(ApiResponse<List<Vehicle>>.ApiOk(results));
        }


        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<Vehicle>))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> CreateAction([FromBody] InputCreateVehicleDto dto)
        {
            // check biển số xe này đã được add hay chưa
            var find = await _dbContext.Set<Vehicle>().Where(x => x.SerialNumber.Equals(dto.SerialNumber)).CountAsync();
            if(find > 0)
            {
                ModelState.AddModelError(nameof(dto.SerialNumber), "Biển số này đã được tạo trên hệ thống");
            }

            if(!ModelState.IsValid)
            {
                return Ok(ApiResponse<object>.ApiError(ModelState));
            }

            var newVehicle = InputCreateVehicleDto.ToEntity(dto);
            await _dbContext.Set<Vehicle>().AddAsync(newVehicle);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<Vehicle>.ApiOk(newVehicle));
        }


        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<Vehicle>))]
        public async Task<IActionResult> UpdateAction([FromBody] InputUpdateVehicleDto dto)
        {
            // check biển số xe này đã được add hay chưa
            var find = await _repository.Where(x => x.SerialNumber.Equals(dto.SerialNumber) && !x.Id.Equals(dto.Id)).CountAsync();
            if (find > 0)
            {
                ModelState.AddModelError(nameof(dto.SerialNumber), "Biển số này đã được tạo trên hệ thống");
            }

            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse<ModelStateDictionary>.ApiError(ModelState));
            }

            var vehicle = await _repository.FirstAsync(x => x.Id.Equals(dto.Id));
            InputUpdateVehicleDto.UpdateEntity(dto, vehicle);
            _repository.Update(vehicle);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<Vehicle>.ApiOk(vehicle));
        }


        [HttpPost("delete")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> DeleteAction(Guid vehicleId)
        {
            // check biển số xe này đã được add hay chưa
            var find = await _repository.Where(x => x.Id.Equals(vehicleId)).FirstAsync();
            _repository.Remove(find);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<string>.ApiOk("Xoá thành công"));
        }
    }
}
