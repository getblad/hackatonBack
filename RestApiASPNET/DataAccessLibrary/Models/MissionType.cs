namespace DataAccessLibrary.Models;

public partial class MissionType
{
    public int MissionTypeId { get; set; }

    public string MissionTypeName { get; set; } = null!;

    public virtual ICollection<Mission> Missions { get; } = new List<Mission>();
}
