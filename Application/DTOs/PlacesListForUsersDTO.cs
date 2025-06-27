using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PlacesListForUsersDTO
    {

        public int Id { get; set; }

        public string Name { get; set; }

       
        public string City { get; set; }


     
        public string Country { get; set; }

      
        public string PlaceCategory { get; set; }

        public string Description { get; set; }


        public string ImageName { get; set; }

    }
}
