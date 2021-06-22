using Maganizer_Project.DAL.Entities;
using Maganizer_Project.DAL.Repositories;
using System;

namespace Maganizer_Project.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts { get; }
        IProfileRepository UserProfiles { get; }
        IRepository<Post> Posts { get; }
        IRepository<Tag> Tags { get; }
        IRepository<Comment> Comments { get; }
        //IRepository<Category> Categories { get; }
        void Save();
    }
}
