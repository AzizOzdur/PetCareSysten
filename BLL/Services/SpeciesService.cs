
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface ISpeciesService
    {
        public IQueryable<SpeciesModel> Query();
        public ServiceBase Create(Species record);
        public ServiceBase Update(Species record);
        public ServiceBase Delete(int id);
    }
    public class SpeciesService : ServiceBase, ISpeciesService
    {
        public SpeciesService(Db db): base(db) {
         
        }

        public ServiceBase Create(Species record)
        {
            if (_db.Species.Any(s => s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("This species name already EXISTS!");
            record.Name = record.Name?.Trim();
            _db.Species.Add(record);
            _db.SaveChanges();
            return Success("Species created.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Species.Include(s => s.Pets).SingleOrDefault(s => s.Id == id);
            if (entity is null)
                return Error("Species can not found");
            if (entity.Pets.Any())
                return Error("Species has relational PETS DATA!");
            _db.Species.Remove(entity);
            _db.SaveChanges();
            return Success("Species Deleted.");
        }

        public IQueryable<SpeciesModel> Query()
        {
            return _db.Species.OrderBy(s => s.Name).Select(s => new SpeciesModel() { Record = s });     
        }

        public ServiceBase Update(Species record)
        {
            if (_db.Species.Any(s => s.Id != record.Id && s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("This species name already EXISTS!");
            var entity = _db.Species.SingleOrDefault(s =>  s.Id == record.Id);
            if (entity is null)
                return Error("Species can not be found!");
            entity.Name = record.Name?.Trim();
            _db.Species.Update(entity);
            _db.SaveChanges();
            return Success("Species Updated.");
        }
    }
}
