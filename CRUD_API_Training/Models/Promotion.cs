namespace CRUD_API_Training.Models
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public int ProductId { get; set; }
        public int PrmotionPercent { get; set; }

        public virtual Product Product {get; set;}
    }
}
