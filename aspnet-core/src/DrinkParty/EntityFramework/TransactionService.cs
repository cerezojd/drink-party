using System.Threading.Tasks;

namespace DrinkParty.EntityFramework
{
    public class TransactionService
    {
        private readonly DrinkDbContext _context;

        public TransactionService(DrinkDbContext context)
        {
            _context = context;
        }

        public async Task Start()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public void Commit()
        {
            _context.Database.CommitTransaction();
        }

        public void Rollback()
        {
            _context.Database.RollbackTransaction();
        }
    }
}
