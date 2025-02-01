using Microsoft.AspNetCore.Mvc;
using HUMIO_API.Model;           // ��� ������ User
using HUMIO_API.Models;          // ��� DTO UserCreateDto (���� ����������� �����)
using YourNamespace.Data;        // ��� AppDbContext, �������� �� ����������� ������������ ����
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HUMIO_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly AppDbContext _dbContext;

        // �������� ��������: ����� DI ������� AppDbContext
        public UserController(ILogger<UserController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        // ������ GET-������, ��������� ������������ (����� �������, ���� �� �����)
        [HttpGet(Name = "User")]
        public IActionResult GetUser()
        {
            return Ok("����� GET ���� �� ����������");
        }

        // POST-����� ��� �������� ������������
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("������ ������������ �� ���� �������������.");
            }

            // ������ ������ ������������ �� ������ ���������� ������
            var user = new User
            {
                Email = userDto.Email,
                UserName = userDto.UserName,
                Password = userDto.Password  // � �������� ���������� ������ ������� ����������!
            };

            // ��������� ������������ � �������� ���� ������
            _dbContext.Users.Add(user);

            // ��������� ��������� � ���� ������
            await _dbContext.SaveChangesAsync();

            // ���������� ���������� ������������ � ������ 200 OK (����� ������� 201 Created)
            return Ok(user);
        }

        // GET-����� ��� ��������� ������������ �� ��������������
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            // ����� ������������ �� id
            var user = await _dbContext.Users.FindAsync(id);

            // ���� ������������ �� ������, ���������� 404
            if (user == null)
            {
                return NotFound($"������������ � id {id} �� ������.");
            }

            // ���������� ������ ������������
            return Ok(user);
        }
    }
}
