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

namespace Application.Services.Admin
{
    public class PlaceService
    {

        IPlaceRepository _placeRepository;

        IMapper _mapper;

        public PlaceService(IPlaceRepository placeRepository, IMapper mapper)
        {
            _placeRepository = placeRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<object>> AddNewPlace(CreationPlaceDTO creationPlaceDTO)
        {

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


            return new ApiResponse<object>
            {
                Issuccess = true,
                Message = "مکان مورد نظر با موفقیت افزوده شد"
            };
        }

        public async Task<ApiResponse<List<PlaceDTO>>> GetAllPlaces()
        {

            var places = await _placeRepository.GetPlacesForAdmin();

            var placesdto = _mapper.Map<List<PlaceDTO>>(places);

            return new ApiResponse<List<PlaceDTO>>{
                Issuccess = true,
                Data = placesdto
            };

        }

        public async Task<ApiResponse<PlaceDTO>> UpdatePlace(PlaceDTO Place)
        {
            var newplace = _mapper.Map<Place>(Place);

            await _placeRepository.Update(newplace);

            return new ApiResponse<PlaceDTO>
            {
                Issuccess = true,
                Data = Place,
                Message = "مکان مورد نظر با موفقیت به روز رسانی شد"
            };

        }

        public async Task<Place> GetPlaceById(int id)
        {
            return await _placeRepository.GetPlaceById(id);
        }

        public async Task<ApiResponse<object>> DeletePlace(int placeid)
        {

            var place = await _placeRepository.GetById(placeid);

            if (place == null)
            {
                return new ApiResponse<object>
                {

                    Issuccess = false,
                    Message = "مکان مورد نظر وجود ندارد"
                };
            }

            if (await _placeRepository.DeleteById(placeid))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", place.ImageName);

                if (!System.IO.File.Exists(filePath))

                    return new ApiResponse<object>
                    {
                        Issuccess = false,
                        Message = "مکان دیدنی با موفقیت حذف شد اما عکسی برای پاک کردن یافت نشد"
                    };

                try
                {
                    System.IO.File.Delete(filePath);
                    return new ApiResponse<object>
                    {
                        Issuccess = true,
                        Message = "مکان با موفقیت حذف شد"
                    };
                }
                catch (Exception ex)
                {

                    return new ApiResponse<object>
                    {
                        Issuccess = false,
                        Message = "مکان حذف گردید اما عکس ان حذف نشد",
                        Errors = ex.ToString()
                    };
                }
            }
            else
            {
                return new ApiResponse<object>
                {
                    Issuccess = false,
                    Message = "مکان دیدنی حذف نشد"
                };
            }

        }

        public async Task<ApiResponse<object>> SetPlaceStatusAsync(int id, string statusText)
        {
            var place = await _placeRepository.GetById(id);
            if (place == null)
            {
                return new ApiResponse<object>
                {
                    Issuccess = false,
                    Message = "مکانی با این مشخصات وجود ندارد"
                };
            }

            // تطبیق string به enum با ایمنی بالا
            Domain.enums.PlaceStatus? newStatus = statusText switch
            {
                "تاییدشده" => Domain.enums.PlaceStatus.تاییدشده, // فرضاً مقدار 0
                "ردشده" => Domain.enums.PlaceStatus.ردشده, // مقدار 2
                _ => null
            };

            if (newStatus == null)
            {
                return new ApiResponse<object>
                {
                    Issuccess = false,
                    Message = "وضعیت ارسالی معتبر نیست"
                };
            }

            place.Status = newStatus.Value;
            await _placeRepository.Update(place);

            return new ApiResponse<object>
            {
                Issuccess = true,
                Message = $"وضعیت مکان به «{statusText}» تغییر کرد"
            };
        }



    }
}
