using Domain.Entities;
using Domain.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementions
{
    public class PlaceCategoryRepository : BaseRepository<PlaceCategory, int>, IPlaceCategoryRepository
    {

        Context _context;

        public PlaceCategoryRepository(Context context) : base(context)
        {
            _context = context;

        }



    }
}
