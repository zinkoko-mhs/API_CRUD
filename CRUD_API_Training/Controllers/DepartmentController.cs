using CRUD_API_Training.Dtos;
using CRUD_API_Training.Dtos.Department;
using CRUD_API_Training.Dtos.Employee;
using CRUD_API_Training.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_API_Training.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartment _repo;

        public DepartmentController(IDepartment repo)
        {
            _repo = repo;
        }

        [HttpGet("GetDepartments")]
        public async Task<IActionResult> GetDepartments([FromQuery]Pagination req)
        {
            try
            {
                var response = await _repo.GetDepartments(req);
                return Ok(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetEmployeesByDepartment")]
        public async Task<IActionResult> GetEmployeesByDepartment([FromQuery] GetEmployeesRequest req)
        {
            try
            {
                var response = await _repo.GetEmployeesByDepartment(req);
                return Ok(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("CreateDepartent")]
        public async Task<IActionResult> CreateDepartment(AddDepartmentRequest req)
        {
            try
            {
                var response = await _repo.CreateDepartment(req);
                return Ok(response);    
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(UpdateDepartmentRequest req)
        {
            try
            {
                var response = await _repo.UpdateDepartment(req);
                return Ok(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                var response = await _repo.DeleteDepartment(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
