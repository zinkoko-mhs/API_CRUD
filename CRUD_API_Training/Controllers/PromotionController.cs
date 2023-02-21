using CRUD_API_Training.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_API_Training.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotion _repo;

        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public PromotionController(IPromotion repo, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _repo = repo;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [Authorize]
        [HttpGet("GetPromotion")]
        public async Task<IActionResult> GetPromotion([FromQuery]int UserId)
        {
            try
            {
                var response = await _repo.GetPromotion(UserId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [AllowAnonymous]
        [HttpPost("GenerateToken")]
        public async Task<IActionResult> GenerateToken([FromBody] AuthUser usr)
        {
            var token = jwtAuthenticationManager.Authentication(usr.username, usr.password);

            if(token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }

    public class AuthUser
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
