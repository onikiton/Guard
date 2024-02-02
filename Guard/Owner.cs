namespace Guard;

public partial class Owner
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Patronymic { get; set; }
    public byte[]? Photo { get; set; }
    public int? KeyId { get; set; }
    public int? DepartmentId { get; set; }
    public virtual Department? Department { get; set; }
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    public virtual Key? Key { get; set; }
}
