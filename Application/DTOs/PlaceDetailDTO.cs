using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PlaceDetailDTO
    {

        public string Name { get; set; }

        public string City { get; set; }


        public string Country { get; set; }

        public string PlaceCategory { get; set; }


        public string Age { get; set; }

        public string ImageName { get; set; }

        public string Description { get; set; }

        public string? WebSite { get; set; }


        public int? CountLike { get; set; }

        public int? CountDisLike { get; set; }

        public string VisitingHours { get; set; }


        public string? ThicketPrice { get; set; }

        public string BestTimeForVisit { get; set; }




    }
}
