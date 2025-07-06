using Application.DTOs;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.InterFaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace CityExplorer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController(PlaceService _placeService) : ControllerBase
    {

     

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetPlaceById(int id)
        {
           var result = await _placeService.GetPlaceById(id);

            if (result.Data == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Issuccess = false,
                    Message = "مکان مورد نظر وجود ندارد"
                });
            }

          
            return Ok(result);

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetMostPopularPlaces()
        {

            var result=await _placeService.GetMostPopularPlaces();


            return Ok(result);

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPlacesForUsers()
        {
           var result= await _placeService.GetPlacesForUsers();


            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreatePlaceByUser(PlaceCreationByUserDTO placedto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Issuccess=false,
                    Data = placedto,
                    Message="مقادیر ارسال شده معتبر نیستند"
                });
            }

        
            var result=await _placeService.CreatePlaceByUser(placedto);

            return Ok(result);

        }
     

    }
}
