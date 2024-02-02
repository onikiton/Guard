namespace Guard;

public partial class Key
{
    public int Id { get; set; }
    public int? Identifier { get; set; }
    public string? DateTime { get; set; }
    public int? KeyTypeId { get; set; }
    public virtual KeyType? KeyType { get; set; }
    public virtual ICollection<Owner> Owners { get; set; } = new List<Owner>();
}
