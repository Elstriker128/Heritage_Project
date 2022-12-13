using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage_Individual_Poject
{
    /// <summary>
    /// This is the base class for the Graduate and Student classes
    /// </summary>
    public abstract class Member
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - this.BirthDate.Year;
                if (this.BirthDate.Date > today.AddYears(-age))
                {
                    age--;
                }
                return age;
            }
        }
        /// <summary>
        /// This is a constructor for the Member object variable
        /// </summary>
        /// <param name="surname">The surname of a student</param>
        /// <param name="name">The name of a student</param>
        /// <param name="birthDate">The birth date of a student</param>
        /// <param name="phoneNumber">The phone number of a student</param>
        public Member(string surname, string name, DateTime birthDate, string phoneNumber)
        {
            Surname = surname;
            Name = name;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            
        }
        /// <summary>
        /// This method overrides the Equals method of the base class
        /// </summary>
        /// <param name="obj">The object of the base class</param>
        /// <returns>Either true or false if the values are equal</returns>
        public override bool Equals(object obj)
        {
            return obj is Member member &&
                   Surname == member.Surname &&
                   Name == member.Name &&
                   PhoneNumber == member.PhoneNumber;
        }
        /// <summary>
        /// This method overrides the GetHashCode method of the base class
        /// </summary>
        /// <returns>Either true or false if the values are equal</returns>
        public override int GetHashCode()
        {
            int hashCode = 1850742378;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Surname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PhoneNumber);
            return hashCode;
        }
        /// <summary>
        /// This method overlays the operator ==
        /// </summary>
        /// <param name="temp">The object of a Member class</param>
        /// <param name="i">A DateTime variable</param>
        /// <returns>Either true or false if the values are equal</returns>
        public static bool operator ==(Member temp, DateTime i)
        {
            return temp.BirthDate == i;
        }
        /// <summary>
        /// This method overlays the operator !=
        /// </summary>
        /// <param name="temp">The object of a Member class</param>
        /// <param name="i">A DateTime variable</param>
        /// <returns>Either true or false if the values are equal</returns>
        public static bool operator !=(Member temp, DateTime i)
        {
            return temp.BirthDate != i;
        }
        /// <summary>
        /// This method overrides the ToString() method of the base class
        /// </summary>
        /// <returns>A string of the required output format</returns>
        public override string ToString()
        {
            string info;
            info = string.Format("| {0,-15} | {1,-15} | {2,-10:yyyy-MM-dd} | {3,-15} |", this.Surname, this.Name,
                this.BirthDate, this.PhoneNumber);
            return info;
        }
    }
}
