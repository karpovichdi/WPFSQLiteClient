namespace Turtur.Models
{
    public class Money
    {
        public string TransactionName;
        public readonly int Id;
        public int Cost;
        public int Sale;

        public string TRANSACTIONNAME
        {
            get => TransactionName;
            set => TransactionName = value;
        }

        public int COST
        {
            get => Cost;
            set => Cost = value;
        }

        public int SALE
        {
            get => Sale;
            set => Sale = value;
        }

        public Money(int id, string transactionName, int cost, int sale)
        {
            TransactionName = transactionName;
            Cost = cost;
            Id = id;
            SALE = sale;
        }
    }
}