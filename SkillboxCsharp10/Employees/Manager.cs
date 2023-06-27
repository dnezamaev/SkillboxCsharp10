using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillboxCsharp10.Clients;

namespace SkillboxCsharp10.Employees
{
    /// <summary>
    /// Класс менеджера. Наследуется от конультанта.
    /// Переопределяет поведение консультанта 
    ///   через переопределение виртуальных методов (полиморфизм).
    /// Метод EditClient наследуется в неизменном виде, 
    ///   логика редактирования переопределена в protected методах 
    ///   редактирования отдельных частей.
    /// </summary> 
    public class Manager : Consultant
    {
        public override string Title { get => "Менеджер"; }

        public Manager()
        {
        }

        /// <summary>
        /// Переопределение логики чтения данных клиента.
        /// Возвращает точную копию клиента, 
        ///     т.к. менеджеру доступны все данные для просмотра.
        /// </summary>
        public override Client ReadClient(Client client)
        {
            return new Client(client);
        }

        /// <summary>
        /// Переопределение логики редактирования паспорта клиента.
        /// </summary>
        protected override string ReadPassport(Client client)
        {
            return client.Passport;
        }

        /// <summary>
        /// Переопределение логики редактирования имени клиента.
        /// Всегда возвращает true, т.к. менеджер имеет право редактировать что угодно.
        /// </summary>
        protected override bool EditName(Client original, Client edited)
        {
            if (original.FullName != edited.FullName)
            {
                original.AddHistory($"ФИО {original.FullName} -> {edited.FullName}", Title);
                original.LastName = edited.LastName;
                original.FirstName = edited.FirstName;
                original.Patronymic = edited.Patronymic;
            }

            return true;
        }

        /// <summary>
        /// Переопределение логики редактирования имени клиента.
        /// Всегда возвращает true, т.к. менеджер имеет право редактировать что угодно.
        /// </summary>
        protected override bool EditPassport(Client original, Client edited)
        {
            if (original.Passport != edited.Passport)
            {
                original.AddHistory($"Паспорт {original.Passport} -> {edited.Passport}", Title);
                original.Passport = edited.Passport;
            }

            return true;
        }
    }
}
