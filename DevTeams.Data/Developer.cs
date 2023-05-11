using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeams.Data
{
    // P.O.C.O -> Plain Old Csharp Objects
    // Domain objects

    public class Developer
    {
        public Developer()
        {

        }
        public Developer(string firstName, string lastName, bool hasPluralsight)
        {
            FirstName = FirstName;
            LastName = LastName;
            HasPluralsight = hasPluralsight;
        }
        //we need a primary key
        public int ID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName

        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public bool HasPluralsight { get; set; }
        // the idea is every time I do: Developer.ToString()=
        public override string ToString()
        {
            var str = $"ID: {ID}\n" +
                      $"Full Name: {FullName}\n" +
                      $"Has PluralsightAccess: {HasPluralsight}\n" +
                        "=========================================\n";

            return str;

        }

    }
}