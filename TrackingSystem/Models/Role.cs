using System;
using System.Collections.Generic;

namespace TrackingSystem
{
    public partial class Role
    {
        public Role()
        {
            Activities = new HashSet<Activity>();
        }

        public int Id { get; set; }
        public string Role1 { get; set; } = null!;

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
