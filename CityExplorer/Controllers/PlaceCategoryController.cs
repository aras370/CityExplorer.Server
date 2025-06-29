using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.InterFaces;
using Infrastructure;
using Infrastructure.Implementions;
using Infrastructure.Seeding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityExplorer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceCategoryController(IPlaceCategoryRepository _placeCategory, IMapper mapper,Context _dbContext) : ControllerBase
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

        [HttpPost("seed")]
        public async Task<IActionResult> SeedPlaceCategories()
        {
            await SeedData.SeedPlaceCategoriesAsync(_dbContext);
            return Ok(new ApiResponse<string>
            {
                Issuccess = true,
                Data = "Place categories seeded successfully."
            });
        }

    }
}
