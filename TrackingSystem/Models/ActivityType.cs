using System;
using System.Collections.Generic;

namespace TrackingSystem
{
    public partial class ActivityType
    {
        public ActivityType()
        {
            Activities = new HashSet<Activity>();
        }

        public int Id { get; set; }
        public string ActivityType1 { get; set; } = null!;

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
