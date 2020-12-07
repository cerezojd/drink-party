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

        public async Task StartAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
}
