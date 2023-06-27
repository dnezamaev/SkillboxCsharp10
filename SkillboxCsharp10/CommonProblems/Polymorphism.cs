using SkillboxCsharp10.Clients;
using SkillboxCsharp10.Employees;

namespace SkillboxCsharp10.CommonProblems
{
    public class Polymorphism
    {
        /// <summary>
        /// Создаем классы с виртуальным и переопределенным методом.
        /// Но не пользуемся полиморфизмом.
        /// В итоге получаем дублирование кода.
        /// </summary>
        void NotUsingPolymorphism()
        {
            // Создаем объекты сотрудников.
            Consultant consultant = new Consultant();
            Manager manager = new Manager();

            // Какой-то клиент, не важно откуда взялся.
            Client client = null;

            // Переменная, определяющая работает ли сейчас консультант или менеджер.
            bool consultantIsWorking = false;

            // Теперь для использования нужного метода
            //      нам нужно городить условные конструкции и
            //      дублировать код вызова метода ReadClient, EditClient.
            // А если сотрудников будет больше, то эта конструкция разростется
            //      до непомерных масштабов.
            if (consultantIsWorking)
            {
                consultant.ReadClient(client);
                consultant.EditClient(client, client);
            }
            else
            {
                manager.ReadClient(client);
                manager.EditClient(client, client);
            }
        }

        void UsingPolymorphism()
        {
            // Объявляем ссылку на текущего сотрудника с общим типом для них.
            // Сюда можно поместить любой объект, реализующий этот интерфейс.
            IBankEmployee currentEmployee;

            // Какой-то клиент, не важно откуда взялся.
            Client client = null;

            // Переменная, определяющая работает ли сейчас консультант или менеджер.
            bool consultantIsWorking = false;

            // Сюда переносим создание фактического объекта.
            // На который будет указываеть ссылка.
            if (consultantIsWorking)
            {
                currentEmployee = new Consultant();
            }
            else
            {
                currentEmployee = new Manager();
            }

            // Теперь у нас есть универсальный способ обратиться
            // к фактической реализации методов,
            // не прибегая к дублированию кода.
            currentEmployee.ReadClient(client);
            currentEmployee.EditClient(client, client);
        }
    }
}
