using CRUD_API_Training.Context;
using CRUD_API_Training.Dtos.Promotion;
using CRUD_API_Training.Interfaces;
using CRUD_API_Training.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_API_Training.Repo
{
    public class PromotionRepo : IPromotion
    {
        private readonly EmployeeContext _context;

        public PromotionRepo(EmployeeContext context)
        {
            _context = context;
        }
        
        public async Task<GetPromotionRespond> GetPromotion(int userId)
        {
            try
            {
                var productsOnPromotion = await (from p in _context.Promotions
                                        join pro in _context.Products on p.ProductId equals pro.ProductID
                                        join img in _context.Images on pro.ProductID equals img.ProductID
                                        join f in _context.IsFavourited on p.ProductId equals f.ProductID
                                            //join u in _context.Users on f.UserID equals u.UserId
                                        where f.UserID == userId
                                        orderby p.ProductId
                                        select new GetPromotionItem
                                        {
                                            ProductId= p.ProductId,
                                            ProductName = pro.ProductName,
                                            Img = img.Img,
                                            OriginalPrice = pro.Price,
                                            PromotionPrice = pro.Price-((p.PrmotionPercent*pro.Price)/100),
                                            PromotionPercent = p.PrmotionPercent,
                                            Qty = pro.Qty,
                                            IsFavourited = f.Favourited
                                        }).ToListAsync();

                return new GetPromotionRespond
                {
                    GetPromotionItems= productsOnPromotion
                };


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
