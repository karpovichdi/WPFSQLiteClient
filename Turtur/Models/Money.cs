namespace Turtur.Models
{
    public class Money
    {
        public readonly string TransactionName;
        public readonly int Id;
        public readonly int Cost;
        
        public Money(int id, string transactionName, int cost)
        {
            TransactionName = transactionName;
            Cost = cost;
            Id = id;
        }
    }
}