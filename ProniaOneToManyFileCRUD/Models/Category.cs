using ProniaOneToManyFileCRUD.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace ProniaOneToManyFileCRUD.Models
{
    public class Category:BaseIdentity
    {
        public string Name { get; set; }
    }
}
