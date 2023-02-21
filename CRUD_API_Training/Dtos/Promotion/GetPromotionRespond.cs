namespace CRUD_API_Training.Dtos.Promotion
{
    public class GetPromotionRespond
    {
        public List<GetPromotionItem> GetPromotionItems { get; set; }
    }
    public class GetPromotionItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Img { get; set; }
        public double OriginalPrice { get; set; }
        public double PromotionPrice { get; set; }
        public int PromotionPercent { get; set; }
        public bool IsFavourited { get; set; }
        public int Qty { get; set; }
    }
}
