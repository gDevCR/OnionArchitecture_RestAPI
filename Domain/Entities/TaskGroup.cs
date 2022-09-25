using Domain.Common;

namespace Domain.Entities
{
    public class TaskGroup : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
