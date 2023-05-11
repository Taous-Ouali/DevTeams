using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeams.Data
{
    public class DeveloperTeam
{
    //empty constructor
    public DeveloperTeam(){}

    // partial constructor 
    public DeveloperTeam(string teamName)
    {
       TeamName = teamName; 
    }
    public DeveloperTeam(string teamName, List<Developer> developers)
    {
       TeamName = teamName; 
       Developers = developers;

    }
    //key
    public int ID { get; set; }

    public string TeamName { get; set; } = string.Empty;

    public List<Developer> Developers { get; set; } = new List<Developer>();

    public override string ToString()
    {
        var str = $"ID: {ID}\n"+
        $"Team Name: {TeamName}\n"+
        "=== Team Members ===";
        foreach(var member in Developers)
        {
            str += $"{member}\n";
        }
        return str;
    }
}  
}