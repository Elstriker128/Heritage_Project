using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Heritage_Individual_Poject
{
    public class InOut
    {
        /// <summary>
        /// This method reads all the data from the data files
        /// </summary>
        /// <param name="filename">The name of the data file</param>
        /// <param name="dateInfo">A DateTime register that contains the first line of the data file</param>
        /// <returns>The register of all the file's data</returns>
        public static Register ReadStudents(string filename, out Register dateInfo)
        {
            DateTime cur = DateTime.Now;
            Register collection = new Register();
            dateInfo = null;
            string[] Lines = File.ReadAllLines(filename, Encoding.UTF8);
            if (new FileInfo(filename).Length == 0)
            {
                Console.WriteLine("Error: no data input");
            }
            else
            {
                DateTime date = DateTime.Parse(Lines[0]);
                dateInfo = new Register(date);
                foreach (string Line in Lines.Skip(1))
                {
                    string[] Values = Line.Split(';');
                    string surname = Values[0];
                    string name = Values[1];
                    DateTime birthdate = DateTime.Parse(Values[2]);                   
                    string phoneNumber = Values[3];
                    string both = Values[4];
                    switch (date.Year)
                    {
                        case 2022:
                            if (Values[5].Length == 0)
                            {
                                string workSpace = both;
                                Graduate FirstGraduate = new Graduate(surname, name, birthdate, phoneNumber, workSpace);
                                collection.Add(FirstGraduate);
                                break;
                            }
                            else
                            {
                                string studentID = both;
                                int course = int.Parse(Values[5]);
                                Student FirstStudent = new Student(surname, name, birthdate, phoneNumber, studentID, course);
                                collection.Add(FirstStudent);
                                break;
                            }
                        case 2021:
                            if (Values[5].Length == 0)
                            {
                                string workSpace = both;
                                Graduate SecondGraduate = new Graduate(surname, name, birthdate, phoneNumber, workSpace);
                                collection.Add(SecondGraduate);
                                break;
                            }
                            else
                            {
                                string studentID = both;
                                int course = int.Parse(Values[5]);
                                Student SecondStudent = new Student(surname, name, birthdate, phoneNumber, studentID, course);
                                collection.Add(SecondStudent);
                                break;
                            }
                        case 2020:
                            if (Values[5].Length == 0)
                            {
                                string workSpace = both;
                                Graduate ThirdGraduate = new Graduate(surname, name, birthdate, phoneNumber, workSpace);
                                collection.Add(ThirdGraduate);
                                break;
                            }
                            else
                            {
                                string studentID = both;
                                int course = int.Parse(Values[5]);
                                Student ThirdStudent = new Student(surname, name, birthdate, phoneNumber, studentID, course);
                                collection.Add(ThirdStudent);
                                break;
                            }
                    }                    
                }
            }
            return collection;
        }
        /// <summary>
        /// This method prints all the data from a register about the association's members and the DateTime register
        /// </summary>
        /// <param name="students">An object of the register about the association's members</param>
        /// <param name="Date">An object of the DateTime register</param>
        public static void PrintStudents(Register students, Register Date)
        {
            Console.WriteLine(new string('-', 110));
            Console.WriteLine("Date");
            Console.WriteLine(new string('-', 110));
            DateTime date = Date.date;
            Console.WriteLine("{0}", date.Year);
            Console.WriteLine(new string('-', 110));
            Console.WriteLine();
            Console.WriteLine(new string('-', 110));
            Console.WriteLine("| {0,-15} | {1,-15} | {2,-10:yyyy-MM-dd} | {3,-15} | {4,-9} | {5,-15} | {6,-7} | ", "Surname", "Name", "BirthDate",
                 "PhoneNumber", "Course", "StudentID", "Workspace");
            Console.WriteLine(new string('-', 105));

            for (int i = 0; i < students.StudentCount(); i++)
            {
                Member info = students.Get(i);
                if(info is Student)
                {
                    Student student = (Student)info;
                    Console.WriteLine("{0}  {1,-8} | {2,-15} | {3,-10}| ",student.ToString(), student.Course, student.StudentId, "No Data");
                }
                else if(info is Graduate)
                {
                    Graduate graduate = (Graduate)info;
                    Console.WriteLine("{0,-15}  {1,-8} | {2,-15} | {3,-10}| ", graduate.ToString(), "No Data", "No Data", graduate.WorkPlace);
                }
            }
            Console.WriteLine(new string('-', 110));
        }
        /// <summary>
        /// This method prints all the oldest students and thei required info: surname, name and age
        /// </summary>
        /// <param name="oldest">An object of the register about the association's members</param>
        /// <param name="Date">An object of the DateTime register</param>
        public static void PrintOldestStudents(Register oldest, Register Date)
        {
            Console.WriteLine(new string('-', 110));
            Console.WriteLine("Date");
            Console.WriteLine(new string('-', 110));
            DateTime date = Date.date;
            Console.WriteLine("{0}", date.Year);
            Console.WriteLine(new string('-', 110));
            Console.WriteLine("| {0,-15} | {1,-15} | {2,-8} |", "Surname", "Name", "Age");
            Console.WriteLine(new string('-', 110));
            for (int i = 0; i < oldest.StudentCount(); i++)
            {
                Member info=oldest.Get(i);
                Console.WriteLine("| {0,-15} | {1,-15} | {2,-8} |",info.Surname,info.Name,info.Age);
            }
            Console.WriteLine(new string('-', 110));
        }
        /// <summary>
        /// This method prints all the data from a register to a CSV file
        /// </summary>
        /// <param name="filename">The name of the CSV file</param>
        /// <param name="Found">An object of the register about the association's members</param>
        public static void PrintToCSVFile(string filename, Register Found)
        {
            string[] lines = new string[Found.StudentCount() + 1];
            lines[0] = String.Format("{0,-15} , {1,-15} , {2,-10:yyyy-MM-dd} , {3,-15} , {4,-8} , {5,-15} , {6,-10}",
                "Surname", "Name", "BirthDate","PhoneNumber", "Course", "StudentID", "Workspace");
            for (int i = 0; i < Found.StudentCount(); i++)
            {
                Member item = Found.Get(i);
                if(item is Student)
                {
                    Student student = (Student)item;
                    lines[i + 1] = String.Format("{0,-15} , {1,-15} , {2,-10:yyyy-MM-dd} , {3,-15} , {4,-8} , {5,-15} , {6,-10}", student.Surname, student.Name, student.BirthDate,
                      student.PhoneNumber, student.Course, student.StudentId, "No Data");
                }
                else
                {
                    Graduate graduate = (Graduate)item;
                    lines[i + 1] = String.Format("{0,-15} , {1,-15} , {2,-10:yyyy-MM-dd} , {3,-15} ; {4,-8} , {5,-15} , {6,-10}", graduate.Surname, graduate.Name, graduate.BirthDate,
                      graduate.PhoneNumber, "No Data", "No Data", graduate.WorkPlace);
                }
            }
            File.WriteAllLines(filename, lines, Encoding.UTF8);
        }
        /// <summary>
        /// This method prints all the primary data to a TXT file
        /// </summary>
        /// <param name="filename">the name of the TXT file</param>
        /// <param name="All">An object of the register about the association's members</param>
        /// <param name="Date">An object of the DateTime register</param>
        public static void PrintToTXTFile(string filename, Register All, Register Date)
        {
            if (All.StudentCount() > 0)
            {
                string[] lines = new string[All.StudentCount() + 4];
                DateTime date = Date.date;
                lines[0] = String.Format(" {0,-5}", date.Year);
                lines[1] = String.Format("{0,-15} | {1,-15} | {2,-10:yyyy-MM-dd} | {3,-15} | {4,-8} | {5,-15} | {6,-10}",
                "Surname", "Name", "BirthDate", "PhoneNumber", "Course", "StudentID", "Workspace");
                for (int i = 0; i < All.StudentCount(); i++)
                {
                    Member item = All.Get(i);
                    if (item is Student)
                    {
                        Student student = (Student)item;
                        lines[i + 1] = String.Format("{0,-15} | {1,-15} | {2,-10:yyyy-MM-dd} | {3,-15} | {4,-8} | {5,-15} | {6,-10}", student.Surname, student.Name, student.BirthDate,
                          student.PhoneNumber, student.Course, student.StudentId, "No Data");
                    }
                    else
                    {
                        Graduate graduate = (Graduate)item;
                        lines[i + 1] = String.Format("{0,-15} | {1,-15} | {2,-10:yyyy-MM-dd} | {3,-15} | {4,-8} | {5,-15} | {6,-10}", graduate.Surname, graduate.Name, graduate.BirthDate,
                          graduate.PhoneNumber, "No Data", "No Data", graduate.WorkPlace);
                    }
                }
                File.AppendAllLines(filename, lines, Encoding.UTF8);
            }
        }
    }
}
