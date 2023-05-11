using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevTeams.Data;
using DevTeams.Repository;

namespace DevTeams.UI
{
    public class ProgramUI
    {
        private DeveloperRepository _dRepo = new DeveloperRepository();
        private DeveloperTeamRepository _dTeamRepo = new DeveloperTeamRepository();

        bool isRunning = true;
        public ProgramUI()
        {


        }
        public void Run()
        {
            RunApplication();
        }

        private void RunApplication()
        {

            while (isRunning)
            {
                Console.Clear();
                System.Console.WriteLine("Welcome To DevTeam \n" +
                                         "1. Show All Developers\n" +
                                         "2. Show Developer By ID\n" +
                                         "3. Add Developer\n" +
                                         "4. Update Developer\n" +
                                         "5. Delete Developer\n" +
                                         "6. Show All DevTeams\n" +
                                         "7. Show DevTeam By ID\n" +
                                         "8. Add DevTeam\n" +
                                         "9. Update DevTeam\n" +
                                         "10. Delete DevTeam\n" +
                                         "11. Developers with Pluralsight\n" +
                                         "12. Add multiple Developers to a Team\n" +
                                         "00. Close Application");
                var userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        ShowAllDevelopers();
                        break;
                    case "2":
                        ShowDeveloperByID();
                        break;
                    case "3":
                        AddDeveloper();
                        break;
                    case "4":
                        UpdateDeveloper();
                        break;
                    case "5":
                        DeleteDeveloper();
                        break;
                    case "6":
                        ShowAllDevTeams();
                        break;
                    case "7":
                        ShowDevTeamByID();
                        break;
                    case "8":
                        AddDevTeam();
                        break;
                    case "9":
                        UpdateDevTeam();
                        break;
                    case "10":
                        DeleteDevTeam();
                        break;
                    case "11":
                        DeveloperswithPluralsight();
                        break;
                    case "12":
                        AddmultipleDeveloperstoaTeam();
                        break;

                    case "00":
                        isRunning = Quit();
                        break;
                    default:
                        System.Console.WriteLine("Invalid input.");
                        break;
                }
            }
        }

        private void AddmultipleDeveloperstoaTeam()
        {
            try
            {
                Console.Clear();
                System.Console.WriteLine("=== Developer Team Listing ===");
                GetDevTeamData();
                List<DeveloperTeam> dTeam = _dTeamRepo.GetDeveloperTeams();

                if(dTeam.Count() >0 )
                {
                    System.Console.WriteLine("Please select a DevTeam by ID");
                    int userInputDevTeamId = int.Parse(Console.ReadLine()!);
                    DeveloperTeam team = _dTeamRepo.GetDeveloperTeam(userInputDevTeamId);

                    List<Developer> auxDevsInDb = _dRepo.GetDevelopers();

                    List<Developer> devsToAdd = new List<Developer>();

                    if(team != null)
                    {
                        bool hasFilledPositions = false;

                        while (!hasFilledPositions) 
                        {
                            if (auxDevsInDb.Count > 0)
                            {
                                DisplayDevelopersInDb(auxDevsInDb);
                                System.Console.WriteLine("Do you want to add a Developer? y/n \n");
                                string userInputAnyDev = Console.ReadLine()!.ToLower()!;
                                if(userInputAnyDev == "y")
                                {
                                    System.Console.WriteLine("Input Developer ID");
                                    int userInputDevId = int.Parse(Console.ReadLine()!);
                                    Developer dev = _dRepo.GetDeveloperByID(userInputDevId);
                                    if(dev != null)
                                    {
                                        devsToAdd.Add(dev);
                                        auxDevsInDb.Remove(dev);
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("The Developer does NOT exist.");
                                    }                                   
                                }
                                else
                                {
                                    hasFilledPositions = true;
                                }
                            }
                            else 
                            {
                                System.Console.WriteLine("There are NO Developers in the Database.");
                                PressAnyKey();
                                break;
                            }
                        }
                    }
                    if (_dTeamRepo.AddMultipleDevelopers(team.ID, devsToAdd))
                    {
                        System.Console.WriteLine("Success!");
                    }
                    else
                    {
                        System.Console.WriteLine("Fail!");
                    }
                }
                else
                {
                    System.Console.WriteLine("Sorry Invalid DevTeam ID");
                }
                
                PressAnyKey();
            }

            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                SomthingWentWrong();
            }
        }

        private void DisplayDevelopersInDb(List<Developer> auxDevsInDb)
        {
            throw new NotImplementedException();
        }

        private void DeveloperswithPluralsight()
        {
            Console.Clear();
            List<Developer> devsWoPS = _dRepo.GetDevelopersWithOutPluralSight(); 
            if(devsWoPS.Count() > 0)
            {
                foreach (Developer dev in devsWoPS)

                {
                    DisplayDevData(dev);
                }
            }
            else
            {
                System.Console.WriteLine("Every Developer has a Pluralsight!");
            }
            PressAnyKey();
        }

        private void DisplayDevData(Developer dev)
        {
            System.Console.WriteLine(dev);
        }

        private void UpdateDevTeam()
        {
            try
            {
                Console.Clear();
                System.Console.WriteLine("=== Developer Team Listing ===");
                GetDevTeamData();
                List<DeveloperTeam> DTeam = _dTeamRepo.GetDeveloperTeams();
                if(DTeam.Count > 0)
                {
                    System.Console.WriteLine("Please select a DevTeamId for update.\n ");
                    int userInputDevTeamId = int.Parse(Console.ReadLine()!);
                    DeveloperTeam team = _dTeamRepo.GetDeveloperTeam(userInputDevTeamId);

                    if (team != null)
                    {
                        DeveloperTeam updatedTeamData = InitializeDTeamCreation();
                        if(_dRepo.UpdateDeveloperData(team.ID, updatedTeamData))
                        {
                            System.Console.WriteLine("Success!");
                        }
                        else 
                        {
                            System.Console.WriteLine("Fail!");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("Sorry it seems that you used an Invalid ID!");
                    }
                }
            }

            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                SomthingWentWrong();
            }

            PressAnyKey();
        }

        private void DeleteDevTeam()
        {
            try
            {
                Console.Clear();
                System.Console.WriteLine("=== Developer Team Listing ===");
                GetDevTeamData();
                List<DeveloperTeam> dTeam = _dTeamRepo.GetDeveloperTeams();
                if(dTeam.Count >0 )
                {
                    System.Console.WriteLine("Please select the Developer Team that you wish to delete by enetering their ID\n");
                    int userInputDevTeamId = int.Parse(Console.ReadLine()!);
                    DeveloperTeam team = _dTeamRepo.GetDeveloperTeam(userInputDevTeamId);
                    if(team != null)
                    {
                        if(_dTeamRepo.DeleteDeveloperTeam(team.ID))
                        {
                            System.Console.WriteLine("Success!");
                        }
                        else
                        {
                            System.Console.WriteLine("Fail!");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("Sorry there are NOT any DEvTeam to delete.\n");
                    }
                
                }
                else
                {
                    System.Console.WriteLine("There are NOT any available Developer Teams");
                }

                PressAnyKey();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                SomthingWentWrong();
            }
        }

        private void AddDevTeam()
        {
            Console.Clear();
            DeveloperTeam dTeam = InitializeDTeamCreation();
            if(_dTeamRepo.AddDeveloperTeam(dTeam))
            {
                System.Console.WriteLine("Success!");   
            
            }
            else
            {
                System.Console.WriteLine("Fail!");
            }

            PressAnyKey();
        }

        private DeveloperTeam InitializeDTeamCreation()
        {
            
            try
            {
                DeveloperTeam team = new DeveloperTeam();
                System.Console.WriteLine("Please enter the Team Name.");
                team.TeamName = Console.ReadLine()!;

                //lets create a bool that will allow us to add mebmers to our team.
                bool hasFilledPositions = false;

                //lets create a list for a dynamic display 
                List<Developer> auxDevelopers = _dRepo.GetDevelopers();

                while(hasFilledPositions == false)
                {
                    System.Console.WriteLine("Does this Team has any DEvelopers y/n?");
                    string userInputAnyDevs = Console.ReadLine()!.ToLower();

                    if(userInputAnyDevs == "y")
                    {
                        if(auxDevelopers.Count > 0)
                        {
                            DisplayDeveloperInDb(auxDevelopers);
                            System.Console.WriteLine("Please select a Developer by ID.");
                            int userInputDevId = int.Parse(Console.ReadLine()!);

                            Developer selectedDeveloper = _dRepo.GetDeveloperByID(userInputDevId);

                            if(selectedDeveloper != null)
                            {
                                team.Developers.Add(selectedDeveloper);
                                auxDevelopers.Remove(selectedDeveloper);
                            }
                            else
                            {
                                System.Console.WriteLine("Sorry the Developer you entred does NOT exist!\n");
                            }
                        }
                        else
                        {
                            System.Console.WriteLine("There are NO Developers in the Database.\n");
                            PressAnyKey();
                            break;
                        }

                    }
                    else 
                    {
                        hasFilledPositions = true;
                    }
                }
                return team;

            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                SomthingWentWrong();

            }
            return null;
        }

        private void DisplayDeveloperInDb(List<Developer> auxDevelopers)
        {
            if(auxDevelopers.Count > 0)
            {
                foreach(Developer dev in auxDevelopers)
                {
                    System.Console.WriteLine(dev);
                }
            }
        }

        private void ShowDevTeamByID()
        {
            Console.Clear();
            System.Console.WriteLine("=== Developer Team Listing ===");
            GetDevTeamData();
            List<DeveloperTeam> devTeam = _dTeamRepo.GetDeveloperTeams();
            if(devTeam.Count() > 0)
            {
                System.Console.WriteLine("Please select the Dev Team you want by ID");
                int userInputDevTeamId = int.Parse(Console.ReadLine()!);
                var selectedDevTeam = _dTeamRepo.GetDeveloperTeam(userInputDevTeamId);
                ValidateDevTeamData(selectedDevTeam);
            }
            PressAnyKey();
        }

        private bool ValidateDevTeamData(DeveloperTeam selectedDevTeam)
        {
            DeveloperTeam team = _dTeamRepo.GetDeveloperTeam(selectedDevTeam.ID);
            if(team != null)
            {
                DisplayDEveloperTeamData(team);
                return true;
            }
            else
            {
                System.Console.WriteLine("Sorry but the Team you mentioned does NOT exist! try with another Team");
                return false;
            }
        }

        private void ShowAllDevTeams()
        {
            Console.Clear();
            System.Console.WriteLine("=== Developer team Listing ===");
            GetDevTeamData();
            PressAnyKey();
        }

        private void GetDevTeamData()
        {
            List<DeveloperTeam> dTeams = _dTeamRepo.GetDeveloperTeams();
            if(dTeams.Count() > 0)
            {
                foreach (DeveloperTeam team in dTeams)
                {
                    DisplayDEveloperTeamData(team);
                } 
            }
            else
            {
                System.Console.WriteLine("Sorry but there are NO available Developer Teams");
            }
        }

        private void DisplayDEveloperTeamData(DeveloperTeam team)
        {
            System.Console.WriteLine(team);
        }

        private bool ValidateDeveloperInDatabase(int userInputDevId)
        {
            Developer dev = GetDeveloperDataFromDb(userInputDevId);
            if (dev != null)
            {
                Console.Clear();
                DisplayDevData(dev);
                return true;
            }
            else
            {
                System.Console.WriteLine($"The Developer w/ the ID {userInputDevId} does NOT exist");
                return false;
            }
        }

        private Developer GetDeveloperDataFromDb(int userInputDevId)
        {
            return _dRepo.GetDeveloperByID(userInputDevId);
        }

        private void SomthingWentWrong()
        {
            System.Console.WriteLine("something went wrong");
            PressAnyKey();
        }

        private void ShowEnlistedDevs()
        {
            foreach (var d in _dRepo.GetDevelopers())
            {
                System.Console.WriteLine($"{d.ID} {d.FullName}\n"+
                "==============================================\n");
            }
        }

        private void PressAnyKey()
        {
            System.Console.WriteLine("Press Any Key");
            Console.ReadKey();
        }

        private void ShowAllDevelopers()
        {
            Console.Clear();
            System.Console.WriteLine($"Developer Listing");
            // method that calls 
            var developers = _dRepo.GetDevelopers();
            foreach (var dev in developers)
            {
                System.Console.WriteLine($"DeveloperID: {dev.ID}\n" +
                                          $"DeveloperName:{dev.FullName}\n"
                                          + $"HasPluralSight:{dev.HasPluralsight}\n");
            }
            Console.ReadKey();
        }

        private void ShowDeveloperByID()
        {
            Console.Clear();
            System.Console.WriteLine($"Developer info");
            System.Console.WriteLine("please enter the developer ID.");
            int userInput = int.Parse(Console.ReadLine());

            // method that calls 
            var developer = _dRepo.GetDeveloperByID(userInput);


            System.Console.WriteLine($"DeveloperID: {developer.ID}\n" +
                                      $"DeveloperName:{developer.FullName}\n"
                                      + $"HasPluralSight:{developer.HasPluralsight}\n");

            Console.ReadKey();
        }

        private void AddDeveloper()
        {
            Console.Clear();

            Developer developerForm = new Developer();

            System.Console.WriteLine($"Please add a Developer First Name:");
            string userInputFirstName = Console.ReadLine()!;
            developerForm.FirstName = userInputFirstName;

            System.Console.WriteLine($"Please add a Developer First Name:");
            string userInputLastName = Console.ReadLine()!;
            developerForm.LastName = userInputLastName;

            if (_dRepo.AddDeveloper(developerForm))
            {
                System.Console.WriteLine("Succes!");
            }
            else
            {
                System.Console.WriteLine("Fail!");
            }


            // method that calls 
            //var developer = _dRepo.AddDeveloper(userInput);


            Console.ReadKey();
        }

       private void UpdateDeveloper()
    {
        try
        {
            Console.Clear();
            System.Console.WriteLine("Please Enter a Devloper Id:");
            int userInputDevId = int.Parse(Console.ReadLine()!);
            Developer selectedDeveloper = _dRepo.GetDeveloperByID(userInputDevId);

            if (selectedDeveloper != null)
            {

                Developer updatedDevData = new Developer();
                System.Console.WriteLine("Please enter the  Developer First Name:");

                string userInputFirstName = Console.ReadLine()!;
                updatedDevData.FirstName = userInputFirstName;

                System.Console.WriteLine("What is the developers Last Name?");
                string userInputLastName = Console.ReadLine()!;
                updatedDevData.LastName = userInputLastName;

                System.Console.WriteLine("does thiS developer have Pluralsight y/n?");
                string userInputYesorNo = Console.ReadLine()!.ToLower();
                if(userInputYesorNo == "y")
                {
                   updatedDevData.HasPluralsight = true;
                }
                else
                {
                   updatedDevData.HasPluralsight = false ;
                }

                if (_dRepo.UpdateDeveloperData(selectedDeveloper.ID, updatedDevData))
                {
                    System.Console.WriteLine("Success!");
                }
                else
                {
                    System.Console.WriteLine("Fail!");
                }
            }
            else
            {

                System.Console.WriteLine($"Sorry the developer with the ID: {userInputDevId} doesn't exist! ");
            }

            Console.ReadKey();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
    }



    private void DeleteDeveloper()
        {
            Console.Clear();
            
            ShowEnlistedDevs();
            System.Console.WriteLine("======================\n");
                        
            try
            {
                System.Console.WriteLine("Select Developer by Id.");
                int userInputDevId = int.Parse(Console.ReadLine()!);
                             
                var isValidated = ValidateDeveloperInDatabase(userInputDevId);
                if(isValidated)
                {
                    System.Console.WriteLine("Do you want to delete this developer y/n ?");
                    string userInputDeleteDev = Console.ReadLine()!.ToLower()!;
                    if(userInputDeleteDev == "y") 
                    {
                        if(_dRepo.DeleteDeveloperData(userInputDevId))
                        {
                            System.Console.WriteLine($"The Developer was successfully deleted!");
                        }
                        else
                        {
                           System.Console.WriteLine($"unfortunatly the Developer wasn't deleted please check what was wrong!"); 
                        }
                    }
                }
                else
                {
                    System.Console.WriteLine($"The Developer with the {userInputDevId} deosn't exist");
                }
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex);
                SomthingWentWrong();
            }

            PressAnyKey();
        }
        private bool Quit()
        {
            System.Console.WriteLine("Thank you see you soon.");
            Console.ReadKey();
            return false;
        }

    }
}