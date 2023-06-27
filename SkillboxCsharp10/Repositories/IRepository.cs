using System.Collections.Generic;
using SkillboxCsharp10.Clients;

namespace SkillboxCsharp10.Repositories
{
    /// <summary>
    /// Общий интерфейс для всех репозиториев.
    /// Для примера реализован репозиторий по работе с текстовыми файлами FileRepository.
    /// На основе этого интерфейса можно реализовать репозитории по работе 
    ///     с базой данных, Web API и т.д.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Загрузка данных из репозитория.
        /// Возвращает ссылку базового типа для всех коллекций.
        /// Таким образом у каждого реализующего класса 
        ///     может быть свой реальный тип коллекции.
        /// Также IEnumerable не дает менять коллекцию (сами элементы редактировать можно),
        ///     что дает небольшую защиту состояния.
        /// </summary>
        IEnumerable<Client> Load();

        /// <summary>
        /// Сохранение данных в репозиторий.
        /// </summary>
        void Save(IEnumerable<Client> clients);

        /// <summary>
        /// Поиск клиента по идентификатору.
        /// </summary>
        Client FindById(int id);
    }
}