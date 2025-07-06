using Application.DTOs;
using AutoMapper;
using CityExplorer;
using Domain.Entities;
using Domain.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Application.Services
{
    public class PlaceService(IPlaceRepository placeRepository, IMapper mapper)
    {

        public async Task<ApiResponse<PlaceDetailDTO>> GetPlaceById(int id)
        {

            var place = await placeRepository.GetPlaceById(id);

            if (place == null)
            {
                return new ApiResponse<PlaceDetailDTO>
                {
                    Issuccess = false,
                    Message = "مکان مورد نظر وجود ندارد",
                    Data=null
                };
            }

            var placedto = mapper.Map<PlaceDetailDTO>(place);

            return new ApiResponse<PlaceDetailDTO>
            {
                Issuccess = true,
                Data = placedto
            };

        }

        
        public async Task<ApiResponse<List<PlaceDTO>>> GetMostPopularPlaces()
        {

            var places = await placeRepository.MostPopularPlaces();

            var placedto = mapper.Map<List<PlaceDTO>>(places);

            return new ApiResponse<List<PlaceDTO>>
            {
                Data = placedto,
                Issuccess = true
            };

        }

       
        public async Task<ApiResponse<List<PlacesListForUsersDTO>>> GetPlacesForUsers()
        {
            var places = await placeRepository.GetPlacesForUsers();

            var placesdto = mapper.Map<List<PlacesListForUsersDTO>>(places);

            return new ApiResponse<List<PlacesListForUsersDTO>>
            {
                Data = placesdto,
                Issuccess = true
            };

        }


        
        public async Task<ApiResponse<object>> CreatePlaceByUser(PlaceCreationByUserDTO placedto)
        {

       

            var place = mapper.Map<Place>(placedto);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(placedto.PlaceImage.FileName);

            place.ImageName = uniqueFileName;

            place.Status = (Domain.enums.PlaceStatus)1;

            if (await placeRepository.Insert(place))
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

            return new ApiResponse<object>
            {
                Issuccess = true,
                Message = "اطلاعات با موفقیت ارسال گردید،پس از تایید در سایت نمایش داده میشود"
            };

        }




    }
}
