namespace Guard;

public partial class Event
{
    public int Id { get; set; }
    public int? OwnerId { get; set; }
    public int? EventTypeId { get; set; }
    public DateTime? DateTime { get; set; }
    public virtual EventType? EventType { get; set; }
    public virtual Owner? Owner { get; set; }
}
