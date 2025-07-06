using Application.DTOs;
using Application.Services.Admin;
using Microsoft.AspNetCore.Mvc;

namespace CityExplorer.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class PlacesController(PlaceService _placeService) : ControllerBase
    {


        [HttpPost("[action]")]
        public async Task<IActionResult> AddNewPlace([FromForm] CreationPlaceDTO creationPlaceDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<CreationPlaceDTO>
                {
                    Issuccess = false,
                    Message = "اطلاعات ارسال شده صحیح نیستند",
                    Data = creationPlaceDTO
                });
            }

            var result = await _placeService.AddNewPlace(creationPlaceDTO);

            return Ok(result);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllPlaces()
        {

            var result = await _placeService.GetAllPlaces();

            return Ok(result);


        }


        [HttpPut("[action]")]
        public async Task<IActionResult> UpdatePlace(PlaceDTO Place)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<PlaceDTO>
                {
                    Issuccess = false,
                    Data = Place,
                    Message = "مقادیر ارسال شده معتبر نیستند"
                });
            }


            var result = await _placeService.UpdatePlace(Place);

            return Ok(result);

        }





        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeletePlace(int id)
        {

            var result = await _placeService.DeletePlace(id);

            if (!result.Issuccess)
            {
                if (result.Message!.Contains("یافت نشد") || result.Message.Contains("وجود ندارد"))
                    return NotFound(result);

                return BadRequest(result); // برای سایر خطاها مثل حذف‌نشدن از DB یا خطای حذف عکس
            }

            return Ok(result);

        }

        [HttpPut("[action]/{id:int}/{status}")]
        public async Task<IActionResult> DetermineStatus(int id, string status)
        {
            var result = await _placeService.SetPlaceStatusAsync(id, status);

            if (!result.Issuccess)
            {
                if (result.Message!.Contains("وجود ندارد"))
                {

                    return NotFound(result);
                }
                return BadRequest(result);
            }

            return Ok(result);
        }


    }
}
