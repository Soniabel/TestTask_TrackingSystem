using System;
using System.Collections.Generic;

namespace TrackingSystem
{
    public partial class Project
    {
        public Project()
        {
            Activities = new HashSet<Activity>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
