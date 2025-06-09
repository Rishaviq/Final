namespace Final.Web.Models.Transfer
{
    public class TransferModel
    {
        public int TransferId { get; set; }

        public string? GoingToNumber { get; set; }

        public string? SenderNumber { get; set; }

        public decimal TransferAmount { get; set; }

        public required string TransferReason { get; set; }

        public string? TransferStatus { get; set; }

        public int UserId { get; set; }
    }
}
