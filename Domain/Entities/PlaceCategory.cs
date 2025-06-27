using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PlaceCategory:BaseEntity
    {

        public string Name { get; set; }


        public List<Place> Places { get; set; }

    }
}
