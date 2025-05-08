using ProniaOneToManyFileCRUD.Models.Base;

namespace ProniaOneToManyFileCRUD.Models
{
    public class Product:BaseIdentity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
