namespace BattleMapServer.Classes_and_Objects
{
    public class HubGroup
    {
        public string Name { get; set; }
        public MapDetails Details { get; set; }
        public HubGroup() { }
        public HubGroup (string name)
        {
            Name = name;
            Details = new MapDetails();
        }
        public HubGroup(string name, MapDetails details)
        {
            Name = name;
            Details = details;
        }
    }
}
