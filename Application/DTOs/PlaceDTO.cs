using Domain.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PlaceDTO
    {

        public int ID { get; set; }

        [Required(ErrorMessage = "لطفا نام مکان را وارد کنید")]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفا نام شهر را وارد کنید")]
        [MaxLength(25)]
        public string City { get; set; }


        [Required(ErrorMessage = "لطفا نام کشور را وارد کنید")]
        [MaxLength(25)]
        public string Country { get; set; }

        [Required(ErrorMessage ="لطفا دسته بندی را وارد کنید")]
        public string PlaceCategory { get; set; }


        [Required(ErrorMessage = "لطفا قدمت مکان را وارد کنید")]
        public string Age { get; set; }


        [Required(ErrorMessage = "لطفا توضیحات مکان را وارد کنید")]
        [MaxLength (500)]
        public string Description { get; set; }


        public int? CountLike { get; set; }

        public int? CountDisLike { get; set; }

        public string VisitingHours { get; set; }

        public string? ThicketPrice { get; set; }

        public string BestTimeForVisit { get; set; }

        public string? WebSite { get; set; }

        public string Status { get; set; }

        public string ImageName { get; set; }



    }
}
