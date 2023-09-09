using System;

namespace SkillboxCsharp10.Menus
{
    /// <summary>
    /// Класс для взаимодействия с пользователем через меню.  
    /// </summary>
    public class Menu
    {
        public MenuEmployees ChooseEmployee()
        {
            Console.Write(
                "1 - консультант\n" +
                "2 - менеджер\n");

            if (!int.TryParse(Console.ReadLine(), out int parsedMenuNumber))
            {
                return MenuEmployees.Unknown;
            }

            return (MenuEmployees)parsedMenuNumber;
        }

        public MenuActions ChooseActions(MenuEmployees employee)
        {
            MenuActions result = MenuActions.Quit;

            switch (employee)
            {
                case MenuEmployees.Consultant:
                    result = ConsultantActionsMenu();
                    break;
                case MenuEmployees.Manager:
                    result = ManagerActionsMenu();
                    break;
                default:
                    Console.WriteLine("Ошибка! Неизвестный тип сотрудника.");
                    break;
            }


            return result;
        }

        private static MenuActions ManagerActionsMenu()
        {
            Console.Write(
                "1 - Показать всех клиентов\n" +
                "2 - Добавить клиента\n" +
                "3 - Изменить фамилию\n" +
                "4 - Изменить имя\n" +
                "5 - Изменить отчество\n" +
                "6 - Изменить телефон\n" +
                "7 - Изменить паспорт\n" +
                "8 - Выйти из программы\n");

            if (!int.TryParse(Console.ReadLine(), out int parsedMenuNumber))
            {
                return MenuActions.Unknown;
            }

            return (MenuActions)parsedMenuNumber;
        }

        private static MenuActions ConsultantActionsMenu()
        {
            MenuActions result;

            Console.Write(
                "1 - Показать всех клиентов\n" +
                "2 - Изменить телефон\n" +
                "3 - Выйти из программы\n" +
                "4 - (тест) Изменить фамилию\n" +
                "5 - (тест) Изменить имя\n" +
                "6 - (тест) Изменить отчество\n" +
                "7 - (тест) Изменить паспорт\n" + 
                "8 - (тест) Добавить клиента\n"
                );

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    result = MenuActions.ShowAllClients;
                    break;
                case "2":
                    result = MenuActions.EditPhone;
                    break;
                case "3":
                    result = MenuActions.Quit;
                    break;
                case "4":
                    result = MenuActions.EditLastName;
                    break;
                case "5":
                    result = MenuActions.EditFirstName;
                    break;
                case "6":
                    result = MenuActions.EditPatronymic;
                    break;
                case "7":
                    result = MenuActions.EditPassportInfo;
                    break;
                case "8":
                    result = MenuActions.AddClient;
                    break;
                default:
                    result = MenuActions.Unknown;
                    break;
            }

            return result;
        }

        public int ChooseClientIdForEdit()
        {
            Console.WriteLine("Введите id клиента для редактирования:");
            return int.Parse(Console.ReadLine());
        }

        public string EnterLastName()
        {
            Console.WriteLine("Введите фамилию:");
            return Console.ReadLine();
        }

        public string EnterFirstName()
        {
            Console.WriteLine("Введите имя:");
            return Console.ReadLine();
        }

        public string EnterPatronymic()
        {
            Console.WriteLine("Введите отчество:");
            return Console.ReadLine();
        }

        public string EnterPhone()
        {
            Console.WriteLine("Введите телефон (не пустой):");
            return Console.ReadLine();
        }

        public string EnterPassport()
        {
            Console.WriteLine("Введите паспортные данные:");
            return Console.ReadLine();
        }
    }
}
