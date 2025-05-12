using ProniaOneToManyFileCRUD.Models.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProniaOneToManyFileCRUD.Models
{
    public class Product:BaseIdentity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

    }
}
