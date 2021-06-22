using Maganizer_Project.DAL.EF;
using Maganizer_Project.DAL.Entities;
using Maganizer_Project.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maganizer_Project.DAL.Repositories
{
    public class CommentRepository : IRepository<Comment>
    {
        private MaganizerContext db;
        public CommentRepository(MaganizerContext context)
        {
            this.db = context;
        }
        public void Create(Comment item)
        {
            db.Comments.Add(item);
        }

        public void Delete(int id)
        {
            Comment item = db.Comments.Find(id);
            if (item != null)
            {
                db.Comments.Remove(item);
            }
        }

        public IEnumerable<Comment> Find(Func<Comment, bool> predicate)
        {
            return db.Comments.Include(x => x.ApplicationUser).Where(predicate).ToList();
        }

        public Comment Get(int id)
        {
            return db.Comments.Find(id);
        }

        public IEnumerable<Comment> GetAll()
        {
            return db.Comments.Include(x => x.ApplicationUser);
        }

        public void Update(Comment item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
