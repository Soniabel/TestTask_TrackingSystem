using System;
using System.Collections.Generic;

namespace TrackingSystem
{
    public partial class Employee
    {
        public Employee()
        {
            Activities = new HashSet<Activity>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Sex { get; set; } = null!;
        public DateTime? Birthday { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
