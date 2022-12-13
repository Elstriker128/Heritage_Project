using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage_Individual_Poject
{
    public class Comparator
    {
        /// <summary>
        /// This method returns the compared value based on the sorting criteria
        /// </summary>
        /// <param name="a">The first object of the Member class</param>
        /// <param name="b">The second object of the Member class</param>
        /// <returns></returns>
        public int Compare(Member a, Member b)
        {
            if (a.Surname != b.Surname)
            {
                return a.Surname.CompareTo(b.Surname);
            }
            else if (a.Name != b.Name)
            {
                return a.Name.CompareTo(b.Name);
            }
            else
            {
                return -1;
            }
        }
    }
}
