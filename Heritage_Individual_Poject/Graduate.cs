using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage_Individual_Poject
{
    /// <summary>
    /// This class inherits the base Member class and adds one extra variable that is
    /// exclusive to the graduates only
    /// </summary>
    public class Graduate : Member
    {
        public string WorkPlace { get; set; }
        public Graduate(string surname, string name, DateTime birthDate, string phoneNumber, string workPlace) : base(surname,name,birthDate,phoneNumber)
        {
            WorkPlace = workPlace;
        }
    }
}
