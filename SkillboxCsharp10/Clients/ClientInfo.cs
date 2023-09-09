namespace SkillboxCsharp10.Clients
{
    /// <summary>
    /// Базовая информация о клиенте для заданий 1-2.
    /// </summary>
    public class ClientInfo
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Passport { get; set; }
        public string Phone { get; set; }

        public string FullName { get => $"{LastName} {FirstName} {Patronymic}"; }
    }
}