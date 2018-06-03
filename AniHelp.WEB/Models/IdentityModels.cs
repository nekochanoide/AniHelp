using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AniHelp.WEB.Migrations;
using System;

namespace AniHelp.WEB.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
                : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<DbAnimalData> AnimalDatas { get; set; }
        public DbSet<DbHealthTrouble> HealthTroubles { get; set; }
    }

    public class DbAnimalData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Дата заполнения
        /// </summary>
        public DateTime Filled { get; set; }
        /// <summary>
        /// Название животного (имя животного)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Время поимки
        /// </summary>
        public string SeizurePlace { get; set; }
   
        /// <summary>
        /// Необходимое действие
        /// </summary>
        public Actions Action { get; set; }
        /// <summary>
        /// Наличие и подпись ошейника
        /// </summary>
        public string Collar { get; set; }
        /// <summary>
        /// Беременность
        /// </summary>
        public bool Pregnancy { get; set; }
        /// <summary>
        /// Проблемы со здоровьем
        /// </summary>
        public virtual Collection<DbHealthTrouble> HealthTroubles { get; set; }
        /// <summary>
        /// Следы плохого обращения
        /// </summary>
        public bool CrueltySigns { get; set; }
        /// <summary>
        /// Факт усыпления и его причины
        /// </summary>
        public string EuthanasiaCause { get; set; }
    }

    public class DbHealthTrouble
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string HealthTrouble { get; set; }
    }
    public enum Actions
    {
        /// <summary>
        /// Отпущен
        /// </summary>
        Отпущен,
        /// <summary>
        /// Госпитализация
        /// </summary>
        Госпитализирован,
        /// <summary>
        /// Поиск хозяина
        /// </summary>
        Поиск_Хозяина,
        /// <summary>
        /// Отправить в приют для животных
        /// </summary>
        Отправлен_в_приют,
        /// <summary>
        /// Утилизация тела
        /// </summary>
        Утилизация,
    }
}