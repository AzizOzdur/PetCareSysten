using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class PetService : ServiceBase, IService<Pet, PetModel>
    {
        public PetService(Db db) : base(db)  {  }

        public ServiceBase Create(Pet record)
        {
            if (_db.Pets.Any(p => p.Name.ToLower() == record.Name.ToLower().Trim() && p.IsFemale == record.IsFemale && p.Age == record.Age))
                return Error("This pet already EXISTS!");
            record.Name = record.Name?.Trim();
            _db.Pets.Add(record);
            _db.SaveChanges();
            return Success("Pet is created.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Pets.Include(p => p.User).SingleOrDefault(p => p.Id == id);
            if (entity is null) {
                return Error("Pet can not be found!");            
            }
            if (entity.User is not null)
                return Error("Species has relational USER DATA!");
            _db.Pets.Remove(entity);
            _db.SaveChanges();
            return Success("Pet is deleted.");
        }

        public IQueryable<PetModel> Query()
        {
           return _db.Pets.OrderBy(p => p.Age).ThenByDescending(p => p.Weight).Select(p => new PetModel() { Record = p });
        }

        public ServiceBase Update(Pet record)
        {
            if (_db.Pets.Any(p => p.Id != record.Id && p.Name.ToLower() == record.Name.ToLower().Trim() && p.IsFemale == record.IsFemale && p.Age == record.Age))
                return Error("This pet already EXISTS!");
            record.Name = record.Name?.Trim();
            _db.Pets.Update(record);
            _db.SaveChanges();
            return Success("Pet is updated.");

        }
    }
}
