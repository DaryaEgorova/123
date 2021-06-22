using Maganizer_Project.DAL.EF;
using Maganizer_Project.DAL.Entities;
using Maganizer_Project.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maganizer_Project.DAL.Repositories
{
    public class TagRepository : IRepository<Tag>
    {
        private readonly MaganizerContext db;
        public TagRepository(MaganizerContext context)
        {
            this.db = context;
        }
        public void Create(Tag item)
        {
            db.Tags.Add(item);
        }

        public void Delete(int id)
        {
            Tag item = db.Tags.Find(id);
            if (item != null)
            {
                db.Tags.Remove(item);
            }
        }

        public IEnumerable<Tag> Find(Func<Tag, bool> predicate)
        {
            return db.Tags.Include(x => x.Posts).Where(predicate).ToList();
        }

        public Tag Get(int id)
        {
            return db.Tags.Find(id);
        }

        public IEnumerable<Tag> GetAll()
        {
            return db.Tags;
        }

        public void Update(Tag item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
