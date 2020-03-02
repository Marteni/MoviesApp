using System;

namespace MoviesApp.DAL
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; }
    }
}