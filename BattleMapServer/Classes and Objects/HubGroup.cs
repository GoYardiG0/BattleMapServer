using BattleMapServer.DTO;

namespace BattleMapServer.Classes_and_Objects
{
    public class HubGroup
    {
        public string Name { get; set; }
        public MapDetails Details { get; set; }
        public List<User> Users {  get; set; } 
        public HubGroup() { }
        public HubGroup (string name)
        {
            Name = name;
            Details = new MapDetails();

            Users = new List<User>();
        }
        public HubGroup(string name, MapDetails details)
        {
            Name = name;
            Details = details;
            Users = new List<User>();
        }
    }
}
