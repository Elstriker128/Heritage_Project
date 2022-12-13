using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Heritage_Individual_Poject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string AllTXT = "AllData.txt";
            string Result = @"Buvo.csv";
            string FirmResult = @"Atea.csv";
            if(File.Exists(Result) || File.Exists(FirmResult) || File.Exists(AllTXT))
            {
                File.Delete(Result);
                File.Delete(FirmResult);
                File.Delete(AllTXT);
            }
            DateTime Oldest = DateTime.MinValue;
            string Firm = "ATEA";
            Register Date1, Date2, Date3;
            Encoding.GetEncoding(1257);
            Register FirstFile = InOut.ReadStudents($@"2022Data.txt", out Date1);
            Register SecondFile = InOut.ReadStudents($@"2021Data.txt", out Date2);
            Register ThirdFile = InOut.ReadStudents($@"2020Data.txt", out Date3);            

            Member First = FirstFile.ReturnOldestExMember(SecondFile);
            Member Second = FirstFile.ReturnOldestExMember(ThirdFile);

            if(First.BirthDate.CompareTo(Second.BirthDate)>0)
            {
                Oldest=Second.BirthDate;
            }
            else
            {
                Oldest = First.BirthDate;
            }

            Register FinalOldestFirst = FirstFile.ReturnAllOldestExMembers(SecondFile, Oldest);
            Register FinalOldestSecond = FirstFile.ReturnAllOldestExMembers(ThirdFile, Oldest);
            Console.WriteLine(new string('-', 110));
            Console.WriteLine("Oldest students and graduates");
            if (FinalOldestFirst.StudentCount() != 0)
            {
                InOut.PrintOldestStudents(FinalOldestFirst, Date2);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(new string('-', 110));
                Console.WriteLine("Error: No oldest students from these years");
                Console.WriteLine(new string('-', 110));
            }
            if (FinalOldestSecond.StudentCount() != 0)
            {
                InOut.PrintOldestStudents(FinalOldestSecond, Date3);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(new string('-', 110));
                Console.WriteLine("Error: No oldest students from these years");
                Console.WriteLine(new string('-', 110));
            }
            Register FirstBoth = SecondFile.ReturnStudentsWhoStudiedBothYears(FirstFile);
            Register SecondBoth = ThirdFile.ReturnStudentsWhoStudiedBothYears(SecondFile);
            Console.WriteLine(new string('-', 110));
            Console.WriteLine("Students and graduates who were a part of the assosiation two years in a row");
            if (FirstBoth.StudentCount() != 0)
            {
                InOut.PrintStudents(FirstBoth, Date2);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(new string('-', 110));
                Console.WriteLine("No students and graduates who were a part of the assosiation two years in a row for these years");
                Console.WriteLine(new string('-', 110));
            }
            if(SecondBoth.StudentCount() != 0)
            {
                InOut.PrintStudents(SecondBoth, Date3);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(new string('-', 110));
                Console.WriteLine("No students and graduates who were a part of the assosiation two years in a row for these years");
                Console.WriteLine(new string('-', 110));
            }

            Register FirmWork = FirstFile.ReturnPeopleWhoWorkAtWorkspace(Firm);
            Console.WriteLine(new string('-', 110));
            FirmWork.Sort(new Comparator());
            if(FirmWork.StudentCount() != 0)
            {
                InOut.PrintToCSVFile(FirmResult, FirmWork);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(new string('-', 110));
                Console.WriteLine("No students and graduates that work at the ATEA firm");
                Console.WriteLine(new string('-', 110));
            }
            Register Past = SecondFile.PastStudents(FirstFile);
            Console.WriteLine(new string('-', 110));
            if(Past.StudentCount() != 0)
            {
                InOut.PrintToCSVFile(Result, Past);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(new string('-', 110));
                Console.WriteLine("No students and graduates that left the association");
                Console.WriteLine(new string('-', 110));
            }
            if(FirstFile!=null || SecondFile!=null || ThirdFile!=null)
            {
                InOut.PrintToTXTFile(AllTXT, FirstFile, Date1);
                InOut.PrintToTXTFile(AllTXT, SecondFile, Date2);
                InOut.PrintToTXTFile(AllTXT, ThirdFile, Date3);
            }
        }
    }
}
