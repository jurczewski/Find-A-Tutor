using System;

namespace Find_A_Tutor.Core.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
