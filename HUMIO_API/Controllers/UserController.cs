using Microsoft.AspNetCore.Mvc;
using HUMIO_API.Model;           // Для модели User
using HUMIO_API.Models;          // Для DTO UserCreateDto (если расположена здесь)
using YourNamespace.Data;        // Для AppDbContext, замените на фактическое пространство имен
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

        // Обратите внимание: через DI передаём AppDbContext
        public UserController(ILogger<UserController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        // Пример GET-метода, оставляем существующий (можно удалить, если не нужен)
        [HttpGet(Name = "User")]
        public IActionResult GetUser()
        {
            return Ok("Метод GET пока не реализован");
        }

        // POST-метод для создания пользователя
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Данные пользователя не были предоставлены.");
            }

            // Создаём объект пользователя на основе переданных данных
            var user = new User
            {
                Email = userDto.Email,
                UserName = userDto.UserName,
                Password = userDto.Password  // В реальном приложении пароль следует хэшировать!
            };

            // Добавляем пользователя в контекст базы данных
            _dbContext.Users.Add(user);

            // Сохраняем изменения в базе данных
            await _dbContext.SaveChangesAsync();

            // Возвращаем созданного пользователя и статус 200 OK (можно вернуть 201 Created)
            return Ok(user);
        }

        // GET-метод для получения пользователя по идентификатору
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            // Поиск пользователя по id
            var user = await _dbContext.Users.FindAsync(id);

            // Если пользователь не найден, возвращаем 404
            if (user == null)
            {
                return NotFound($"Пользователь с id {id} не найден.");
            }

            // Возвращаем данные пользователя
            return Ok(user);
        }
    }
}
