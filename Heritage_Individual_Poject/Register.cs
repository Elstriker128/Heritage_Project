using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Heritage_Individual_Poject
{
    public class Register
    {
        private List<Member> allMembers;
        public DateTime date { get; private set; }
        /// <summary>
        /// This is a constructor for the DateTime register
        /// </summary>
        /// <param name="date"></param>
        public Register(DateTime date)
        {
            this.date = date;
        }
        /// <summary>
        /// This is a constructor for the the association's members register
        /// </summary>
        public Register()
        {
            this.allMembers = new List<Member>();
        }
        /// <summary>
        /// This is a constructor for the the association's members register
        /// </summary>
        /// <param name="members">An object of the Member class list</param>
        public Register(List<Member> members) : this()
        {
            foreach(Member member in members)
            {
                this.allMembers.Add(member);
            }
        }
        /// <summary>
        /// This method adds an object's value to the register
        /// </summary>
        /// <param name="member">An object of the Member's class</param>
        public void Add(Member member)
        {
            this.allMembers.Add(member);
        }
        /// <summary>
        /// This method returns the amount of people there are in a single register
        /// </summary>
        /// <returns>returns the amount of people there are in a single register</returns>
        public int StudentCount()
        {
            return this.allMembers.Count;
        }
        /// <summary>
        /// This method returns a required register value based on the given index
        /// </summary>
        /// <param name="index">The given index</param>
        /// <returns>returns a required register value</returns>
        public Member Get(int index)
        {
            return this.allMembers[index];
        }
        /// <summary>
        /// This method checks if the given value is contained inside the register
        /// </summary>
        /// <param name="info">An object of the Member's class</param>
        /// <returns>returns either true or false depending if the value is inside th container</returns>
        public bool Contains(Member info)
        {
            return this.allMembers.Contains(info);
        }
        /// <summary>
        /// This method returns the first student who is no longer part of the association
        /// </summary>
        /// <param name="SecondRegister">An object of the second register</param>
        /// <returns>returns the first student who is no longer part of the association</returns>
        public Member ReturnFirstExStudent(Register SecondRegister)
        {
            Member result = SecondRegister.Get(0);
            for (int j = 0; j < SecondRegister.StudentCount(); j++)
            {
                Member second = SecondRegister.Get(j);
                if (!allMembers.Contains(second))
                {
                    result = second;
                    break;
                }
            }
            return result;

        }
        /// <summary>
        /// This method returns the oldest member who is no longer part of the association
        /// </summary>
        /// <param name="SecondRegister">An object of the second register</param>
        /// <returns>eturns the oldest member who is no longer part of the association</returns>
        public Member ReturnOldestExMember(Register SecondRegister)
        {
            int count = 0;
            Member oldest = ReturnFirstExStudent(SecondRegister);
            foreach(Member member in allMembers)
            {
                Member first = member;                
                for (int j = count; j < SecondRegister.StudentCount(); j++)
                {
                    Member second = SecondRegister.Get(j);
                    if (!allMembers.Contains(second) && DateTime.Compare(oldest.BirthDate, second.BirthDate) > 0)
                    {
                        oldest = second;
                    }
                }
                count++;
            }

            return oldest;
        }
        /// <summary>
        /// This method returns all the oldest members who are no longer part of the association
        /// </summary>
        /// <param name="SecondRegister">An object of the second register</param>
        /// <param name="date">The birtdate of the oldest student</param>
        /// <returns>returns all the oldest members who are no longer part of the association</returns>
        public Register ReturnAllOldestExMembers(Register SecondRegister, DateTime date)
        {
            int count = 0;
            Register AllOldest = new Register();
            Member oldest = ReturnOldestExMember(SecondRegister);
            foreach( Member member in allMembers)
            {
                Member first = member;
                for (int j = count; j < SecondRegister.StudentCount(); j++)
                {                    
                    Member second = SecondRegister.Get(j);
                    if (!AllOldest.Contains(second) && !allMembers.Contains(second) && DateTime.Compare(oldest.BirthDate, second.BirthDate) == 0 && oldest==date)
                    {
                        AllOldest.Add(second);
                    }                    
                }
                count++;
            }
            return AllOldest;
        }
        /// <summary>
        /// This method returns association members who studied two years in a row
        /// </summary>
        /// <param name="SecondRegister">An object of the second register</param>
        /// <returns>returns association members who studied two years in a row</returns>
        public Register ReturnStudentsWhoStudiedBothYears(Register SecondRegister)
        {
            Register Found = new Register();
            for (int i = 0; i < SecondRegister.StudentCount(); i++)
            {
                Member second = SecondRegister.Get(i);
                foreach(Member member in allMembers)
                {
                    Member first = member;
                    if (!Found.Contains(first) && SecondRegister.Contains(first))
                    {
                        Found.Add(first);
                    }
                }
            }
            return Found;
        }
        /// <summary>
        /// This method returns the association members that are working in a firm
        /// </summary>
        /// <param name="firm">The name of the firm</param>
        /// <returns>returns the association members that are working in a firm</returns>
        public Register ReturnPeopleWhoWorkAtWorkspace(string firm)
        {
            Register filtered = new Register();
            foreach(Member member in allMembers)
            {
                Member current = member;
                if(member is Graduate)
                {
                    Graduate graduate = (Graduate)member;
                    if (!filtered.Contains(graduate) && allMembers.Contains(graduate) && graduate.WorkPlace==firm)
                    {
                        filtered.Add(graduate);
                    }
                }
            }
            return filtered;
        }
        /// <summary>
        /// This method returns all the students that are no longer part of the association
        /// </summary>
        /// <param name="SecondRegister">An object of the second register</param>
        /// <returns>returns all the students that are no longer part of the association</returns>
        public Register PastStudents(Register SecondRegister)
        {
            Register past = new Register();
            for (int i = 0; i < SecondRegister.StudentCount(); i++)
            {
                foreach(Member member in allMembers)
                {
                    if(!SecondRegister.Contains(member) && !past.Contains(member))
                    {
                        past.Add(member);
                    }
                }
            }
            return past;
        }
        /// <summary>
        /// This methos sorts the registers data based on the sorting key
        /// </summary>
        /// <param name="comp">An object of the Comparator class that contains the sorting key</param>
        public void Sort(Comparator comp)
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                for (int i = 0; i < this.allMembers.Count - 1; i++)
                {
                    Member a = this.allMembers[i];
                    Member b = this.allMembers[i + 1];
                    if (comp.Compare(a, b) > 0)
                    {
                        this.allMembers[i] = b;
                        this.allMembers[i + 1] = a;
                        flag = true;
                    }
                }
            }

        }
    }
}
