using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.InterFaces;
using Infrastructure.Implementions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityExplorer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceCategoryController(IPlaceCategoryRepository _placeCategory, IMapper mapper) : ControllerBase
    {


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllPlaceCetegory()
        {
            var category = await _placeCategory.GetAll();

            var categorydto = mapper.Map<List<PlaceCategoryDTO>>(category);

            return Ok(new ApiResponse<List<PlaceCategoryDTO>>
            {
                Issuccess = true,
                Data = categorydto
            });

        }

    }
}
