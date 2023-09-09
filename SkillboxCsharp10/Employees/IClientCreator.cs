using SkillboxCsharp10.Clients;

namespace SkillboxCsharp10.Employees
{
    /// <summary>
    /// Интерфейс для сотрудников, которые могут создавать клиентов.
    /// </summary>
    public interface IClientCreator
    {
        /// <summary>
        /// Создает нового клиента на основе базовой информации о нём.
        /// </summary>
        Client CreateClient(ClientInfo clientInfo);
    }
}
