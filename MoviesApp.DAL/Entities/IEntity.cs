using System;

namespace MoviesApp.DAL.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
