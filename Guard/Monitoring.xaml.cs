using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;

namespace Guard
{
    public partial class Monitoring : Page
    {
        public Monitoring()
        {
            InitializeComponent();
            using SecurityDbContext db = new();
            eventlist.ItemsSource = db.Events.Include(o => o.Owner).
                Include(e => e.EventType).OrderByDescending(t => t.DateTime).
                Take(4).ToList();
        }
    }
}
