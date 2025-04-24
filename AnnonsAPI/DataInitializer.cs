using Microsoft.EntityFrameworkCore;

namespace AnnonsAPI
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        public DataInitializer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Migrate()
        {
            _dbContext.Database.Migrate();
        }
    }
}
