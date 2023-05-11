using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevTeams.Data;

namespace DevTeams.Repository
{
    public class DeveloperRepository
    {
      //we need a variable container that will hold the collection of developers
        private List<Developer> _developerDb = new List<Developer>(); 
        private int _count = 0;

      // C.R.U.D

        // Create

        public DeveloperRepository()
        {
            Seed();
        }
        public bool AddDeveloper(Developer developer) 
        {
            if(developer is null)
            {
                return false;
            }
            else
            {
                //increment the _count
                _count++;

                //assign the dveloper to _count
                developer.ID = _count;

                //save to the database
                _developerDb.Add(developer);
                
                return true; 
            }
        }

        // Read by all
        public List<Developer> GetDevelopers()
        {
            return _developerDb;
        }
       

        //Read by ID
        public Developer GetDeveloperByID(int id)
        {
            // loop through all the developers inside of the data base (_developersDb)
            // in order to find a single developer based the ID that the user passes in 
            
            //1. this is the temprory variable type
            //2. this the temporary variable container name : this represents one in many  
            //3. this represent the collection (the developerDataBase)
            
            //          1          2             3 
            foreach (Developer developer in _developerDb)
            {
                // if we find a developer with the neede id
                if (developer.ID == id)
                {
                    //we will return the developer for future use 
                    return developer;
                }
                // otherwise we will return "null or nothing"
            }
                return null;

        }

        // Update

        public bool UpdateDeveloperData(int id,Developer newDeveloperData)
        {
            // find a character in the data base
           Developer oldDeveloperData = GetDeveloperByID(id);
            if (oldDeveloperData != null)
            {
                oldDeveloperData.FirstName = newDeveloperData.FirstName;
                 
                
                oldDeveloperData.LastName = newDeveloperData.LastName;
                
                return true;

            }
            //
            return false;
        }

        //Delete  
          public bool DeleteDeveloperData(int id)
        {
           Developer oldDeveloperData = GetDeveloperByID(id);
            if (oldDeveloperData != null)
            {
                _developerDb.Remove(oldDeveloperData);
                return true;
            }
            return false;
        }


        private void Seed()
    {
        Developer Rebbeca = new Developer
        {
           // ID = 1,
            FirstName = "Rebecca",
            LastName = "Thompson"
        };

        Developer David = new Developer
        {
            //ID = 2,
            FirstName = "David",
            LastName = "Smith"
        };

        Developer Flora = new Developer
        {
            //ID = 3,
            FirstName = "Flora",
            LastName = "Davis"
        };
        Developer Leo = new Developer
        {
            //ID = 4,
            FirstName = "Leo",
            LastName = "Harris"
        };

        //we need to add them to the database: AddPerson(Person person)
        AddDeveloper(Rebbeca);
        AddDeveloper(David);
        AddDeveloper(Flora);
        AddDeveloper(Leo);
    }

        public List<Developer> GetDevelopersWithOutPluralSight()
        {
            var devList = new List<Developer>();
            foreach (Developer d in _developerDb)
            {
                if (d.HasPluralsight == false)
                {
                    devList.Add(d);
                }
            }
            return devList;
            //return _developerDb.Where(d =>d.HasPluralsight == false).ToList();
        }

        public bool UpdateDeveloperData(int iD, DeveloperTeam updatedTeamData)
        {
            throw new NotImplementedException();
        }
    }
}