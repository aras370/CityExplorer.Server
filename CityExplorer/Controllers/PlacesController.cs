using Application.DTOs;
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
    public class PlacesController : ControllerBase
    {

        IPlaceRepository _placeRepository;
        IMapper _mapper;
       

        public PlacesController(IPlaceRepository placeRepository, IMapper mapper,IPlaceCategoryRepository placeCategoryRepository)
        {
            _placeRepository = placeRepository;
            _mapper = mapper;
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetPlaceById(int id)
        {
            var place = await _placeRepository.GetPlaceById(id);

            if (place == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Issuccess = false,
                    Message = "مکان مورد نظر وجود ندارد"
                });
            }

            var placedto = _mapper.Map<PlaceDetailDTO>(place);
            return Ok(new ApiResponse<PlaceDetailDTO>
            {
                Issuccess = true,
                Data = placedto
            });

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetMostPopularPlaces()
        {

            var places=await _placeRepository.MostPopularPlaces();

            var placedto=_mapper.Map<List<PlaceDTO>>(places);

            return Ok(new ApiResponse<List< PlaceDTO>>
            {
                Data = placedto,
                Issuccess = true
            });

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPlacesForUsers()
        {
            var places=await _placeRepository.GetPlacesForUsers();

            var placesdto = _mapper.Map<List<PlacesListForUsersDTO>>(places);

            return Ok(new ApiResponse<List<PlacesListForUsersDTO>>
            {
                Data = placesdto,
                Issuccess=true
            });

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

            var place=_mapper.Map<Place>(placedto);
            
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(placedto.PlaceImage.FileName);

            place.ImageName = uniqueFileName;

            place.Status = (Domain.enums.PlaceStatus)1;

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
                    await placedto.PlaceImage.CopyToAsync(fileStream);
                }

            }

            return Ok(new ApiResponse<object>
            {
                Issuccess = true,
                Message = "اطلاعات با موفقیت ارسال گردید،پس از تایید در سایت نمایش داده میشود"
            });

        }
     

    }
}
