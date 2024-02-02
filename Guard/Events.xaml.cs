using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;

namespace Guard
{
    public partial class Events : Page
    {
        public Events()
        {
            InitializeComponent();
            SecurityDbContext db = new();
            eventgrid.ItemsSource = db.Events.Include(o => o.Owner).Include(o => o.Owner.Department).
                Include(e => e.EventType).OrderByDescending(t => t.DateTime).ToList();
        }
        public static void AddEvent(string[] msg)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(long.Parse(msg[0]));
            DateTime dateTime = dateTimeOffset.DateTime;
            using SecurityDbContext db = new();
            Owner? Owner = db.Owners.FirstOrDefault(o => o.Key.Identifier == int.Parse(msg[2]));
            if (Owner != null)
            {
                Event addEvent = new()
                {
                    EventTypeId = int.Parse(msg[1]),
                    OwnerId = Owner.Id,
                    DateTime = dateTime
                };
                db.Events.Add(addEvent);
                db.SaveChanges();
            }
            else
            {
                Event addEvent = new()
                {
                    EventTypeId = int.Parse(msg[1]),
                    DateTime = dateTime
                };
                db.Events.Add(addEvent);
                db.SaveChanges();
            }
        }
    }
}
