using SkillboxCsharp10.Clients;

namespace SkillboxCsharp10.Employees
{
    /// <summary>
    /// Консультант.
    /// Базовый класс в иерархии банковских сотрудников.
    /// Определяет основное поведение.
    /// </summary>
    public class Consultant : IBankEmployee
    {
        /// <summary>
        /// Текст для сокрытия паспортных данных.
        /// </summary>
        private const string HiddenPassport = "** ** ******";

        /// <summary>
        /// Название должности.
        /// </summary>
        public virtual string Title { get => "Консультант"; }

        public Consultant()
        {
        }

        /// <summary>
        /// Виртуальный метод чтения данных о клиенте.
        /// Возвращает копию клиента с измененными для отображения данными.
        /// </summary>
        public virtual Client ReadClient(Client client)
        {
            var result = new Client(client);
            result.Passport = ReadPassport(client);
            return result;
        }

        /// <summary>
        /// Возвращает скрытый паспорт.
        /// Виртуальный метод. Переопределен в менеджере.
        /// </summary>
        protected virtual string ReadPassport(Client client)
        {
            return HiddenPassport;
        }

        /// <summary>
        /// Редактирует клиента.
        /// Этот метод не переопределяется в менеджере.
        /// Вместо него перепредлеляются внутренние protected методы
        ///     EditName, EditPassport, EditPhone.
        /// Уже внутри них будет реализовано разное поведение
        ///     для каждого сотрудника.
        /// </summary>
        /// <param name="original">Исходные данные клиента.</param>
        /// <param name="edited">Отредактированные данные клиента.</param>
        /// <returns>Успешно ли прошло редактировани.</returns>
        public virtual bool EditClient(Client original, Client edited)
        {
            return
                EditName(original, edited) &&
                EditPassport(original, edited) &&
                EditPhone(original, edited);
        }

        /// <summary>
        /// Проверяет, что имя измененного и исходного клиента совпадают.
        /// Для простоты редактирование всех частей имени объединено в один метод.
        /// Вернёт false, если отличаются, т.к. консультанту запрещено редактирование.
        /// </summary>
        protected virtual bool EditName(Client original, Client edited)
        {
            return original.FullName == edited.FullName;
        }


        /// <summary>
        /// Проверяет, что паспорт измененного и исходного клиента совпадают.
        /// Вернёт false, если отличаются, т.к. консультанту запрещено редактирование.
        /// </summary>
        protected virtual bool EditPassport(Client original, Client edited)
        {
            return original.Passport == edited.Passport;
        }


        /// <summary>
        /// Редактирует телефон клиента.
        /// Вернёт false, если телефон пустой.
        /// </summary>
        protected virtual bool EditPhone(Client original, Client edited)
        {
            if (string.IsNullOrEmpty(edited.Phone))
            {
                return false;
            }

            if (original.Phone != edited.Phone)
            {
                original.AddHistory($"Телефон {original.Phone} -> {edited.Phone}", Title);
                original.Phone = edited.Phone;
            }

            return true;
        }
    }
}
