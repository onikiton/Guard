using Microsoft.EntityFrameworkCore;

namespace Guard;

public partial class SecurityDbContext : DbContext
{
    public SecurityDbContext()
    {
    }
    public SecurityDbContext(DbContextOptions<SecurityDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Event> Events { get; set; }
    public virtual DbSet<EventType> EventTypes { get; set; }
    public virtual DbSet<Key> Keys { get; set; }
    public virtual DbSet<KeyType> KeyTypes { get; set; }
    public virtual DbSet<Owner> Owners { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=D:\\Programs\\SecurityDb.db");
}
