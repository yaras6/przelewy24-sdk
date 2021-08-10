namespace P24
{
    public class P24TransactionVerifyRequest
    {
        public int MerchantId { get; set; }
        public int PosId { get; set; }
        public string SessionId { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public int OrderId { get; set; }
        public string Sign { get; set; }
    }
}