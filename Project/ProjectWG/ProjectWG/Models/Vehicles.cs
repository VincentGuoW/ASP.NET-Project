using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace ProjectWG.Models
{
    public class Vehicles
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Make { get; set; }
        public string Year { get; set; }
        public string BodyType {  get; set; }
        public string Color { get; set; }

        public string Picture { get; set; }
/*
        [NotMapped] //what this for?
        public IFormFile PictureFile { get; set; } // what this for?*/
    }
}
