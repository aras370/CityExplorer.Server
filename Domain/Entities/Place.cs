using Domain.enums;
using System.ComponentModel.DataAnnotations.Schema;




namespace Domain.Entities
{
    public class Place : BaseEntity
    {

        public string Name { get; set; }

        public string City { get; set; }

        public string Country { get; set; }


        [ForeignKey("PlaceCategory")]
        public int CategoryId { get; set; }


        public string Age { get; set; }

        public string Description { get; set; }

        public int? CountLike { get; set; }

        public int? CountDisLike { get; set; }

        public string  VisitingHours{ get; set; }

        public string?  ThicketPrice{ get; set; }

        public string? BestTimeForVisit{ get; set; }

        public string? WebSite { get; set; }

    

        public string ImageName { get; set; }

        public PlaceStatus Status { get; set; }


        public PlaceCategory PlaceCategory { get; set; }


    }
}
