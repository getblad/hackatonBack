namespace DataAccessLibrary.Models;

public partial class Role
{
    public string RoleId { get; set; } = null!;

	public string RoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
