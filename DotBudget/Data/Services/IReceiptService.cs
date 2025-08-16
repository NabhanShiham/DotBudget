namespace DotBudget.Data.Services
{
    public interface IReceiptService
    {
        public Receipt GetReceipt(string id);
        public Receipt AddReceipt(Receipt receipt);
        public void DeleteReceipt(string id);
    }
}
