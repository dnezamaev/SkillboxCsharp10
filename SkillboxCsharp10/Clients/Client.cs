using System;
using System.Collections.Generic;
using System.Text;

namespace SkillboxCsharp10.Clients
{
    /// <summary>
    /// Информация о клиенте.
    /// </summary>
    public class Client
    {
        public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Passport { get; set; }
        public List<ChangeLogItem> ChangeLog { get; set; } = new List<ChangeLogItem>();

        public Client(int id, string lastName, string firstName, string patronymic, string phone, string passportInfo)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            Phone = phone;
            Passport = passportInfo;
        }

        public Client(Client other) : this(
            other.Id, other.LastName, other.FirstName, other.Patronymic,
            other.Phone, other.Passport)
        {
            ChangeLog = other.ChangeLog;
        }

        public void AddHistory(string changeInfo, string employee)
        {
            ChangeLogItem history = new ChangeLogItem(DateTime.Now, changeInfo, employee);
            ChangeLog.Add(history);
        }

        public string FullName { get => $"{LastName} {FirstName} {Patronymic}"; }

        public override string ToString()
        {
            return
                $"{Id}) {FullName}, {Phone}, {Passport}\n" +
                string.Join("\n", ChangeLog) + "\n";
        }
    }
}
