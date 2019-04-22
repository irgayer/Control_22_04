namespace Control_22_04
{
    using Control_22_04.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class AppContext : DbContext
    {
        // Контекст настроен для использования строки подключения "AppContext" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "Control_22_04.AppContext" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "AppContext" 
        // в файле конфигурации приложения.
        public AppContext()
            : base("name=AppContext")
        {
            Database.SetInitializer(new DataInitializer());   
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
    }
}