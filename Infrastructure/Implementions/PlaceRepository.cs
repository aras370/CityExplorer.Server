using Domain.Entities;
using Domain.InterFaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementions
{
    public class PlaceRepository : BaseRepository<Place, int>, IPlaceRepository
    {

        Context _context;

        public PlaceRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Place> GetPlaceById(int id)
        {
            return await _context.Places.Include(p=>p.PlaceCategory).FirstAsync(p=>p.ID==id);
        }

        public async Task<List<Place>> GetPlacesForAdmin()
        {
            return await _context.Places.Include(p=>p.PlaceCategory).ToListAsync();
        }

        public async Task<List<Place>> GetPlacesForUsers()
        {
            return await _context.Places.Include(P => P.PlaceCategory)
                .Where(p => p.Status == 0).ToListAsync();


        }

        public async Task<List<Place>> MostPopularPlaces()
        {
            return await _context.Places.Include(P=>P.PlaceCategory).OrderByDescending(p => p.CountLike).Take(3).ToListAsync();
        }
    }
}
