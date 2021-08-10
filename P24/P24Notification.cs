namespace P24
{
    public class P24Notification
    {
        public int MerchantId { get; set; }
        public int PosId { get; set; }
        public string SessionId { get; set; }
        public int Amount { get; set; }
        public int OriginAmount { get; set; }
        public string Currency { get; set; }
        public int OrderId { get; set; }
        public int MethodId { get; set; }
        public string Statement { get; set; }
        public string Sign { get; set; }
    }
}