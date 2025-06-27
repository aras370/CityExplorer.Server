using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InterFaces
{
    public interface IPlaceRepository:IBaseRepository<Place,int>
    {

        Task<List<Place>> MostPopularPlaces();

        Task<List<Place>> GetPlacesForUsers();

        Task<List<Place>> GetPlacesForAdmin();

        Task<Place> GetPlaceById(int id);

    }
}
