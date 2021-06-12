using System;

namespace TPICAP.Persons.Domain
{
    public abstract class Entity
    {
        public int Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        protected Entity()
        {
            CreatedAt = DateTime.Now;
        }

        protected void Update()
        {
            UpdatedAt = DateTime.Now;
        }
    }
}
