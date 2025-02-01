namespace HUMIO_API.Model
{
    public class DeviceIdentifier
    {
        // Первичный ключ
        public int Id { get; set; }

        // Строковое поле для хранения идентификатора устройства
        public string Identifier { get; set; }

        // Внешний ключ для связи с пользователем
        public int UserId { get; set; }

        // Навигационное свойство для пользователя, которому принадлежит данный идентификатор устройства
        public User User { get; set; }
    }
}
