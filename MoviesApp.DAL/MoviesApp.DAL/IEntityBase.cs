using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.DAL
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}
