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

namespace Application.Services
{
    public class PlaceCategoryService(IPlaceCategoryRepository _placeCategory, IMapper mapper)
    {

        public async Task<ApiResponse<List<PlaceCategoryDTO>>> GetAllCetegories()
        {

            var categories = await _placeCategory.GetAll();

            var categorydto = mapper.Map<List<PlaceCategoryDTO>>(categories);

            return new ApiResponse<List<PlaceCategoryDTO>>()
            {
                Issuccess = true,
                Data = categorydto,
                
            };
        }

    }
}
