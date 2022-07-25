using System;
using System.Collections.Generic;

namespace TrackingSystem
{
    public partial class Activity
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public int ProjectId { get; set; }
        public int ActivityTypeId { get; set; }
        public string Duration { get; set; }

        public virtual ActivityType ActivityType { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;
        public virtual Project Project { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
