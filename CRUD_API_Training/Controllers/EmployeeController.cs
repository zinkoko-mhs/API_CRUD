using CRUD_API_Training.Dtos;
using CRUD_API_Training.Dtos.Employee;
using CRUD_API_Training.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_API_Training.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _repo;

        public EmployeeController(IEmployee repo)
        {
            _repo = repo;
        }

        [HttpGet("GetEmployees")]
        public async Task<IActionResult> GetEmployees([FromQuery]Pagination req)
        {
            try
            {
                var response = await _repo.GetEmployees(req);
                return Ok(response);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetEmployeeByID")]
        public async Task<IActionResult> GetEmployeeByID(int id)
        {
            try
            {
                var response = await _repo.GetEmployeeByID(id);
                return Ok(response);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(AddEmployeeRequest req)
        {
            try
            {
                var response = await _repo.AddEmployee(req);
                return Ok(response);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        [HttpPost("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeRequest req)
        {
            try
            {
                var response = await _repo.UpdateEmployee(req);
                return Ok(response);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int id)
        { 
            try
            {
                var response = await _repo.DeleteEmployee(id);
                return Ok(response);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
