
namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ModelContext _db;
        public UnitOfWork(ModelContext db)
        {
            _db = db;
        }
        public void Dispose()
        {
            _db.Dispose();
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
