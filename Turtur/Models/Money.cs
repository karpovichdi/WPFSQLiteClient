namespace Turtur.Models
{
    public class Money
    {
        public readonly string TransactionName;
        public readonly int Id;
        public readonly int Cost;
        
        public Money(string transactionName, int cost, int id)
        {
            TransactionName = transactionName;
            Cost = cost;
            Id = id;
        }
    }
}