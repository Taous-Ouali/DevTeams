using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevTeams.Data;

namespace DevTeams.Repository
{
    public class DeveloperTeamRepository
    {
        public DeveloperTeamRepository()
        {
            Seed();
        }
        private DeveloperRepository _dRepo = new DeveloperRepository();
        private List<DeveloperTeam> _DevTeamDb = new List<DeveloperTeam>();
        private int _count = 0;
        // C.R.U.D

        //Create
        public bool AddDeveloperTeam(DeveloperTeam devTeam)
        {
            if (devTeam is null)
            {
                return false;
            }
            else
            {
                _count++;
                devTeam.ID = _count;
                _DevTeamDb.Add(devTeam);
                return true;
            }
        }
        // R -> Read All
        public List<DeveloperTeam> GetDeveloperTeams()
        {
            return _DevTeamDb;

        }
        // R -> Read by Id
        public DeveloperTeam GetDeveloperTeam(int id)
        {
            return _DevTeamDb.FirstOrDefault(w => w.ID == id)!;
        }

        // U -> Update

        public bool UpdateDeveloperTeam(int id, DeveloperTeam newDevTeamData)
        {
            DeveloperTeam oldDevTeamData = GetDeveloperTeam(id);
            if (oldDevTeamData != null)
            {
                oldDevTeamData.TeamName = newDevTeamData.TeamName;
                oldDevTeamData.Developers = newDevTeamData.Developers;
                
                return true;
            }
            return false;
        }

        // delete 
        public bool DeleteDeveloperTeam(int id)
        {
            DeveloperTeam oldDevTeamData = GetDeveloperTeam(id);
            if (oldDevTeamData != null)
            {
                _DevTeamDb.Remove(oldDevTeamData);
                return true;
            }
            return false;
        }

        private void Seed()
        {
           var js= new DeveloperTeam
           {
                TeamName = "JavaScript Team"
           };
            js.Developers.Add(_dRepo.GetDeveloperByID(3));

            var cSharp = new DeveloperTeam
            {
                TeamName = "cSharp Team"
            };
            cSharp.Developers.Add(_dRepo.GetDeveloperByID(1));
            cSharp.Developers.Add(_dRepo.GetDeveloperByID(2));

            var java = new DeveloperTeam 
            {
                TeamName = "java Team"
            };
            AddDeveloperTeam(js);
            AddDeveloperTeam(cSharp);
            AddDeveloperTeam(java);

        }

       

        public bool AddMultipleDevelopers(int iD, List<Developer> devsToAdd)
        {
            throw new NotImplementedException();
        }
    }
          
}