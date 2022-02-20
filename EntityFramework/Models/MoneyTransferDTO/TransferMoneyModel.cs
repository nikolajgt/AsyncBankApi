namespace EntityFramework.Models
{
    public class TransferMoneyModel
    {

        public Users userTo { get; set; }
        public Users userFrom { get; set; }

        public TransferMoneyModel(Users to, Users from) 
        {
            userTo = to;
            userFrom = from;
        }
    }
}
