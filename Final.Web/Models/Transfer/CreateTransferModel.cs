namespace Final.Web.Models.Transfer
{
    public class CreateTransferModel
    {
     
        public required string GoingToAccNumber { get; set; }
      
        public int SenderId { get; set; }
       
        public decimal TransferAmount { get; set; }
     
        public required string TransferReason { get; set; }
       
        public int UserId { get; set; }
    }
}
