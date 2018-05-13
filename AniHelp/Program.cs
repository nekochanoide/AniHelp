﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GoHome
{
    /// <summary>
    /// Информация о животном
    /// </summary>
    public class AnimalDataDto
    {
        /// <summary>
        /// Дата заполнения
        /// </summary>
        public DateTime Filled { get; set; }
        /// <summary>
        /// Название животного (имя животного)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Место поимки
        /// </summary>
        public string SeizurePlace { get; set; }
        /// <summary>
        /// Состояние животного
        /// </summary>
        public AnimalStatus Status { get; set; }
        /// <summary>
        /// Необходимые действия
        /// </summary>
        public List<Actions> Actions { get; set; }
    }

    /// <summary>
    /// Состояние животного
    /// </summary>
    public class AnimalStatus
    {
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
        public List<string> HealthTroubles { get; set; } 
        /// <summary>
        /// Следы плохого обращения
        /// </summary>
        public bool CrueltySigns { get; set; }
        /// <summary>
        /// Факт усыпления и его причины
        /// </summary>
        public string EuthanasiaCause { get; set; }
    }

    /// <summary>
    /// Действия
    /// </summary>
    public class Actions
    {
       /// <summary>
       /// Вид действия
       /// </summary>
        public ActionType Type { get; set; }

    }

    /// <summary>
    /// Вид действия
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// Отпущен
        /// </summary>
        Released,
        /// <summary>
        /// Госпитализация
        /// </summary>
        Hospitalization,
        /// <summary>
        /// Первая помощь
        /// </summary>
        FirstAid,
        /// <summary>
        /// Поиск хозяина
        /// </summary>
        OwnerSearch,
        /// <summary>
        /// Отправить в приют для животных
        /// </summary>
        SendToAnimalShelter,
        /// <summary>
        /// Утилизация тела
        /// </summary>
        BodyDispose,
    }
}
