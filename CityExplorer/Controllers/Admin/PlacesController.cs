using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.InterFaces;
using Microsoft.AspNetCore.Mvc;

namespace CityExplorer.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {

        IPlaceRepository _placeRepository;

        IMapper _mapper;

        public PlacesController(IPlaceRepository placeRepository, IMapper mapper)
        {
            _placeRepository = placeRepository;
            _mapper = mapper;
        }

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
            var place = _mapper.Map<Place>(creationPlaceDTO);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(creationPlaceDTO.PlaceImage.FileName);

            place.ImageName = uniqueFileName;
            place.Status = 0;
            if (await _placeRepository.Insert(place))
            {
                // مسیر ذخیره
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

                // اطمینان از وجود پوشه
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // مسیر ذخیره سازی عکس
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // ذخیره فایل
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await creationPlaceDTO.PlaceImage.CopyToAsync(fileStream);
                }

            }


            return Ok(new ApiResponse<object>
            {
                Issuccess = true,
                Message = "مکان مورد نظر با موفقیت افزوده شد"
            });
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllPlaces()
        {
            var places = await _placeRepository.GetPlacesForAdmin();

            var placesdto = _mapper.Map<List<PlaceDTO>>(places);

            return Ok(new ApiResponse<List<PlaceDTO>>
            {
                Issuccess = true,
                Data = placesdto
            });


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

            var newplace = _mapper.Map<Place>(Place);

            await _placeRepository.Update(newplace);

            return Ok(new ApiResponse<PlaceDTO>
            {
                Issuccess = true,
                Data = Place,
                Message = "مکان مورد نظر با موفقیت به روز رسانی شد"
            });

        }





        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeletePlace(int id)
        {

            var place = await _placeRepository.GetById(id);

            if (place == null)
            {
                return NotFound(new ApiResponse<object>
                {

                    Issuccess = false,
                    Message = "مکان مورد نظر وجود ندارد"
                });
            }


            if (await _placeRepository.DeleteById(id))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", place.ImageName);

                if (!System.IO.File.Exists(filePath))

                    return NotFound(new ApiResponse<object>
                    {
                        Issuccess = false,
                        Message = "مکان دیدنی با موفقیت حذف شد اما عکسی برای پاک کردن یافت نشد"
                    });

                try
                {
                    System.IO.File.Delete(filePath);
                    return Ok(new ApiResponse<object>
                    {
                        Issuccess = true,
                        Message = "مکان با موفقیت حذف شد"
                    });
                }
                catch (Exception ex)
                {

                    return BadRequest(new ApiResponse<object>
                    {
                        Issuccess = false,
                        Message = "مکان حذف گردید اما عکس ان حذف نشد",
                        Errors = ex.ToString()
                    });
                }
            }
            else
            {
                return BadRequest(new ApiResponse<object>
                {
                    Issuccess = false,
                    Message = "مکان دیدنی حذف نشد"
                });
            }

        }

        [HttpPut("[action]/{id:int}/{status}")]
        public async Task<IActionResult> DetermineStatuse(int id, string status)
        {
            var place = await _placeRepository.GetById(id);

            var response = new ApiResponse<object>();

            if (place == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Issuccess = false,
                    Message = "مکانی با این مشخصات وجود ندارد"
                });
            }

            if (status == "تاییدشده")
            {
                place.Status = 0;
                await _placeRepository.Update(place);
                response.Issuccess = true;
                response.Message = "مکان مورد نظر تایید شد";
            }
            else
            {
                place.Status = (Domain.enums.PlaceStatus)2;
                await _placeRepository.Update(place);
                response.Issuccess = true;
                response.Message = "مکان مورد نظر رد شد";
            }

            return Ok(response);
        }


    }
}
