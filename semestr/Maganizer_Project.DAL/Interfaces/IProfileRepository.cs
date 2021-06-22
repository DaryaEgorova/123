using Maganizer_Project.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maganizer_Project.DAL.Interfaces
{
    public interface IProfileRepository
    {
        IEnumerable<UserProfile> GetAll();
        UserProfile Get(int? id);
        UserProfile GetByAccountId(string id);
        IEnumerable<UserProfile> Find(Func<UserProfile, Boolean> predicate);
        void Create(UserProfile item);
        void Update(UserProfile item);
        void Delete(int id);
    }
}
