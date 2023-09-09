using System;
using System.Collections.Generic;
using System.Linq;

using SkillboxCsharp10.Clients;
using SkillboxCsharp10.Employees;
using SkillboxCsharp10.Menus;
using SkillboxCsharp10.Repositories;

namespace SkillboxCsharp10
{
    internal class Program
    {
        /// <summary>
        /// Ссылка на текущего работника банка.
        /// Может содержать объект консультанта либо менеджера.
        /// За счёт использования интерфейса в качестве типа ссылки
        ///     можно добавлять новые типы сотрудников (директор, аудитор, ...)
        ///     без изменения остального кода.
        /// </summary>
        static IBankEmployee employee;

        /// <summary>
        /// Ссылка на репозиторий.
        /// </summary>
        static IRepository repository;

        static void Main(string[] args)
        {
            // Объект для взаимодействия с пользователем через меню.
            Menu menu = new Menu();

            // Спрашиваем у пользователя тип сотрудника,
            // от имени которого он хочет работать.
            MenuEmployees menuEmployeeChoosen = ChooseEmployee(menu);

            // Проверяем корректность выбора. Выходим при некорректном.
            if (employee == null || menuEmployeeChoosen == MenuEmployees.Unknown)
            {
                Console.WriteLine("Нет такого сотрудника, давай до свидания!");
                return;
            }

            // Создаем объект для работы с файловым репозиторием.
            repository = new FileRepository("Clients.txt");

            // Основной цикл программы. Выход по пункту меню.
            MenuActions menuActionChoosen;
            do
            {
                // Спрашиваем у пользователя, что он хочет сделать.
                menuActionChoosen = ChooseAction(menu, menuEmployeeChoosen);
            } 
            while (menuActionChoosen != MenuActions.Quit);
        }

        /// <summary>
        /// Спрашивает у пользователя тип сотрудника, от имени которого он хочет работать.
        /// Заполняет ссылку на сотрудника.
        /// Возвращает выбранный пункт меню.
        /// </summary>
        private static MenuEmployees ChooseEmployee(Menu menu)
        {
            MenuEmployees menuEmployeeChoosen = menu.ChooseEmployee();
            Console.WriteLine();

            switch (menuEmployeeChoosen)
            {
                case MenuEmployees.Consultant:
                    employee = new Consultant();
                    break;
                case MenuEmployees.Manager:
                    employee = new Manager();
                    break;
                default:
                    employee = null;
                    break;
            }

            return menuEmployeeChoosen;
        }

        /// <summary>
        /// Спрашивает у пользователя, что он хочет сделать.
        /// Возвращает выбранный пункт меню.
        /// </summary>
        private static MenuActions ChooseAction(Menu menu, MenuEmployees menuEmployeeChoosen)
        {
            // Загружаем данные из репозитория.
            var clientsFromRepository = repository.Load();

            MenuActions menuActionChoosen = menu.ChooseActions(menuEmployeeChoosen);
            Console.WriteLine();

            switch (menuActionChoosen)
            {
                case MenuActions.AddClient:
                    CreateClient(menu, clientsFromRepository);
                    break;

                case MenuActions.ShowAllClients:
                    ShowClients(clientsFromRepository);
                    break;

                case MenuActions.EditPhone:
                case MenuActions.EditFirstName:
                case MenuActions.EditLastName:
                case MenuActions.EditPatronymic:
                case MenuActions.EditPassportInfo:
                    EditClient(clientsFromRepository, menu, menuActionChoosen);
                    break;

                case MenuActions.Quit:
                default:
                    return menuActionChoosen;
            }

            return menuActionChoosen;
        }

        /// <summary>
        /// Создает нового клиента.
        /// </summary>
        private static void CreateClient(Menu menu, IEnumerable<Client> clientsFromRepository)
        {
            if (employee is not IClientCreator)
            {
                Console.WriteLine("Вы не можете добавлять клиентов.");
                return;
            }

            // Спрашиваем у пользователя значения полей.
            var userInput = new ClientInfo
            {
                Id = repository.GetNextId(),
                LastName = menu.EnterLastName(),
                FirstName = menu.EnterFirstName(),
                Patronymic = menu.EnterPatronymic(),
                Phone = menu.EnterPhone(),
                Passport = menu.EnterPassport(),
            };

            var clientCreator = employee as IClientCreator;
            var client = clientCreator.CreateClient(userInput);

            clientsFromRepository = clientsFromRepository.Append(client);
            repository.Save(clientsFromRepository);
        }

        /// <summary>
        /// Выводит список клиентов на консоль.
        /// </summary>
        private static void ShowClients(IEnumerable<Client> clientsFromRepository)
        {
            foreach (var client in clientsFromRepository)
            {
                // Отображаем каждого клиента,
                //     передавая управление в виртуальный метод ReadClient.
                // Здесь используется полиморфизм.
                // Какой конкретно метод выполнится зависит
                //     от содержимого объекта employee.
                Console.WriteLine(employee.ReadClient(client).ToString());
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Выводит меню редактирования клиента и выполняет выбранное действие.
        /// </summary>
        private static void EditClient(IEnumerable<Client> clientsFromRepository, Menu menu, MenuActions menuActionChoosen)
        {
            // Спрашиваем у пользователя какого клиента он хочет редактировать,
            // предлагаем поиск по идентификатору.
            int id = menu.ChooseClientIdForEdit();
            Client foundClient = repository.FindById(id, clientsFromRepository);

            // Создаем копию этого клиента для хранения отредактированных данных.
            Client editClient = new Client(foundClient);

            // Спрашиваем у пользователя новое значение поля.
            switch (menuActionChoosen)
            {
                case MenuActions.EditPhone:
                    editClient.Phone = menu.EnterPhone();
                    break;
                case MenuActions.EditFirstName:
                    editClient.FirstName = menu.EnterFirstName();
                    break;
                case MenuActions.EditLastName:
                    editClient.LastName = menu.EnterLastName();
                    break;
                case MenuActions.EditPatronymic:
                    editClient.Patronymic = menu.EnterPatronymic();
                    break;
                case MenuActions.EditPassportInfo:
                    editClient.Passport = menu.EnterPassport();
                    break;
                default:
                    break;
            }

            // Пробуем редактировать, передавая управление в метод EditClient.
            if (!employee.EditClient(foundClient, editClient))
            {
                Console.WriteLine("Редактирование не удалось.");
                return;
            }

            // Сохраняем изменения в репозиторий.
            Console.WriteLine("Редактирование прошло успешно.");
            repository.Save(clientsFromRepository);
        }
    }
}