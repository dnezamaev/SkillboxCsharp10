using System;
using System.Collections.Generic;
using System.Text;

namespace SkillboxCsharp10.Clients
{
    /// <summary>
    /// Полная информация о клиенте.
    /// Дополнительно включает историю изменений этого клиента.
    /// </summary>
    public class Client : ClientInfo
    {
        public List<ChangeLogItem> ChangeLog { get; set; }

        /// <summary>
        /// Конструктор. Создает нового клиента с пустой историей изменений
        /// по предоставленной базовой информации.
        /// </summary>
        public Client(ClientInfo clientInfo)
        {
            Id = clientInfo.Id;
            FirstName = clientInfo.FirstName;
            LastName = clientInfo.LastName;
            Patronymic = clientInfo.Patronymic;
            Phone = clientInfo.Phone;
            Passport = clientInfo.Passport;

            ChangeLog = new List<ChangeLogItem>();
        }

        /// <summary>
        /// Конструктор копирования. Создает копию клиента.
        /// История изменений передается по ссылке.
        /// </summary>
        /// <param name="other"></param>
        public Client(Client other) : this(other as ClientInfo)
        {
            ChangeLog = other.ChangeLog;
        }

        public void AddHistory(string changeInfo, string employee)
        {
            ChangeLogItem history = new ChangeLogItem(DateTime.Now, changeInfo, employee);
            ChangeLog.Add(history);
        }

        public override string ToString()
        {
            return
                $"{Id}) {FullName}, {Phone}, {Passport}\n" +
                string.Join("\n", ChangeLog) + "\n";
        }
    }
}
