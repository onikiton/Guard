namespace Guard;

public partial class Department
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public virtual ICollection<Owner> Owners { get; set; } = new List<Owner>();
}
