using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillboxCsharp10.Clients
{
    /// <summary>
    /// Информация об изменении данных клиента.
    /// </summary>
    public class ChangeLogItem
    {
        public DateTime Date { get; }

        public string ChangeInfo { get; }

        public string Employee { get; }

        public ChangeLogItem(DateTime Date, string ChangeInfo, string Employee)
        {
            this.Date = Date;
            this.ChangeInfo = ChangeInfo;
            this.Employee = Employee;
        }

        public override string ToString()
        {
            return $"{Date}: {Employee} изменил {ChangeInfo}";
        }
    }
}
