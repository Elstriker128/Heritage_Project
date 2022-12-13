using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage_Individual_Poject
{
    /// <summary>
    /// This class inherits the base Member class and adds two extra variables that are
    /// exclusive to the students only
    /// </summary>
    public class Student : Member
    {
        public string StudentId { get; set; }
        public int Course { get; set; }
        public Student(string surname, string name, DateTime birthDate, string phoneNumber, string studentID, int course) : base(surname, name, birthDate, phoneNumber)
        {
            this.StudentId = studentID;
            this.Course = course;
        }        
    }
}
