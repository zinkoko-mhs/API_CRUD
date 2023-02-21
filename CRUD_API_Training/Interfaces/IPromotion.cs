using CRUD_API_Training.Dtos.Promotion;

namespace CRUD_API_Training.Interfaces
{
    public interface IPromotion
    {
        Task<GetPromotionRespond> GetPromotion(int userID);
    }
}
