using System.ComponentModel.DataAnnotations;

namespace CRUD_API_Training.Models
{
    public class Images
    {
        [Key]
        public int ImageID { get; set; }
        public int ProductID { get; set; }
        public string Img { get; set; }
        public virtual Product Product { get; set; }
    }
}
