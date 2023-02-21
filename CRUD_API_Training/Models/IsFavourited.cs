namespace CRUD_API_Training.Models
{
    public class IsFavourited
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public bool Favourited { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
