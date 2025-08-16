using Microsoft.EntityFrameworkCore;

namespace DotBudget.Data.Services
{
    public class ReceiptService : IReceiptService
    {
        protected readonly ApplicationDbContext context;
        public ReceiptService(ApplicationDbContext dbcontext)
        {
            context = dbcontext;
        }

        public Receipt AddReceipt(Receipt receipt)
        {
            if (receipt == null) {
                throw new ArgumentNullException(nameof(receipt));
            }
            try
            {
                context.Receipts.Add(receipt);
                context.SaveChanges();
                return receipt;
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
                return receipt;
            }
        }

        public void DeleteReceipt(string id)
        {
            context.Receipts.Remove(GetReceipt(id));
            context.SaveChanges();
        }

        public Receipt GetReceipt(string id)
        {
            if (!string.IsNullOrEmpty(id)) {
                throw new ArgumentNullException(nameof(id));
            }

            var receipt = context.Receipts.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            return receipt ?? throw new KeyNotFoundException($"Receipt with ID {id} not found!");
        }
    }
}
