using Application.DTOs;
using Application.Services;
using AutoMapper;
using Domain.InterFaces;
using Infrastructure;
using Infrastructure.Seeding;
using Microsoft.AspNetCore.Mvc;

namespace CityExplorer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceCategoryController(Context _dbContext, PlaceCategoryService _placecategoryservice) : ControllerBase
    {


        [HttpGet("GetAllPlaceCetegory")]
        public async Task<IActionResult> GetAllPlaceCetegory()
        {
            
            var result=await _placecategoryservice.GetAllCetegories();

            return Ok(result);

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
