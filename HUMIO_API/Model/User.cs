using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HUMIO_API.Model
{
    public class User
    {
        // Первичный ключ
        public int Id { get; set; }

        // Имя пользователя
        public string UserName { get; set; }

        // Email
        [EmailAddress]
        public string Email { get; set; }

        // Пароль (рекомендуется хранить в виде хэша)
        public string Password { get; set; }

        // Навигационное свойство: один пользователь может иметь несколько идентификаторов устройств
        public ICollection<DeviceIdentifier> DeviceIdentifiers { get; set; } = new List<DeviceIdentifier>();

        // Навигационные свойства для дополнительных таблиц
        public Trial Trial { get; set; }
        public Premium Premium { get; set; }
        public UserStats UserStats { get; set; }
    }

    // Таблица пробного периода
    public class Trial
    {
        // Используем UserId как первичный ключ (один к одному)
        [Key, ForeignKey("User")]
        public int UserId { get; set; }

        // Дата окончания пробного периода
        public DateTime TrialEndDate { get; set; }

        // Навигационное свойство на пользователя
        public User User { get; set; }
    }

    // Таблица премиум-подписки
    public class Premium
    {
        // Используем UserId как первичный ключ (один к одному)
        [Key, ForeignKey("User")]
        public int UserId { get; set; }

        // Дата окончания подписки
        public DateTime SubscriptionEndDate { get; set; }

        // Стоимость текущей покупки
        public decimal CurrentPurchaseCost { get; set; }

        // Навигационное свойство на пользователя
        public User User { get; set; }
    }

    // Таблица статистики пользователя
    public class UserStats
    {
        // Используем UserId как первичный ключ (один к одному)
        [Key, ForeignKey("User")]
        public int UserId { get; set; }

        // Время, проведенное в приложении
        public TimeSpan TimeSpentInApp { get; set; }

        // Общая сумма покупок пользователя
        public decimal TotalPurchaseAmount { get; set; }

        // Количество покупок пользователя
        public int PurchaseCount { get; set; }

        // Навигационное свойство на пользователя
        public User User { get; set; }
    }
}
