namespace Guard;

public partial class KeyType
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public virtual ICollection<Key> Keys { get; set; } = new List<Key>();   
}
