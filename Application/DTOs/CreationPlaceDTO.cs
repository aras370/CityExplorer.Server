using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreationPlaceDTO
    {

        [Required(ErrorMessage = "لطفا نام مکان را وارد کنید")]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفا نام شهر را وارد کنید")]
        [MaxLength(25)]
        public string City { get; set; }


        [Required(ErrorMessage = "لطفا نام کشور را وارد کنید")]
        [MaxLength(25)]
        public string Country { get; set; }

        [Required(ErrorMessage = "لطفا دسته بندی را وارد کنید")]
        public int CategoryId { get; set; }


        [Required(ErrorMessage = "لطفا قدمت مکان را وارد کنید")]
        public string Age { get; set; }


        [Required(ErrorMessage = "لطفا توضیحات مکان را وارد کنید")]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        [MaxLength()]

        public string VisitingHours { get; set; }


        public string? ThicketPrice { get; set; }
        [Required]
        [MaxLength(100)]
        public string BestTimeForVisit { get; set; }

        public string? WebSite { get; set; }

        [Required]
        public IFormFile PlaceImage { get; set; }


    }
}
