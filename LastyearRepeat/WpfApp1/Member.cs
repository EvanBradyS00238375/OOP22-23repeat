using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Member : IComparable<Member>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }

        public int CompareTo(Member other)
        {
            if (other == null) return 1;
            return LastName.CompareTo(other.LastName);
        }

        public int GetRaceAge()
        {
            var today = DateTime.Today;
            var age = today.Year - Dob.Year;
            if (Dob.Date > today.AddYears(-age)) age--;
            return age;
        }

        public override string ToString()
        {
            return $"{LastName}, {FirstName} - {Dob.ToShortDateString()} ({GetRaceAge()})";
        }
    }


    public class Team
    {
        public string TeamName { get; set; }
        public List<Member> Players { get; set; } = new List<Member>();

        public override string ToString()
        {
            return $"{TeamName} ({Players.Count} members)";
        }
    }

    public static class RandomMemberGenerator
    {
        private static readonly List<string> FirstNames = new List<string>
        {
            "John", "Jane", "Alice", "Bob", "Charlie", "Dave", "Eve", "Grace", "Heidi", "Ivan",
            
        };

        private static readonly List<string> LastNames = new List<string>
        {
            "Doe", "Smith", "Johnson", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor", "Anderson",
            
        };

        private static readonly Random random = new Random();

        public static List<Member> GenerateRandomMembers(int count)
        {
            var members = new List<Member>();

            for (int i = 0; i < count; i++)
            {
                var firstName = FirstNames[random.Next(FirstNames.Count)];
                var lastName = LastNames[random.Next(LastNames.Count)];
                var dob = GenerateRandomDate(new DateTime(2005, 1, 1), new DateTime(2011, 12, 31));

                members.Add(new Member
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Dob = dob
                });
            }

            members.Sort();
            return members;
        }

        private static DateTime GenerateRandomDate(DateTime start, DateTime end)
        {
            int range = (end - start).Days;
            return start.AddDays(random.Next(range));
        }
    }
}