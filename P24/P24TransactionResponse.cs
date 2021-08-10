namespace P24
{
    public class P24TransactionResponse
    {
        public class TransactionData
        {
            public string Token { get; set; }
        }

        public TransactionData Data { get; set; }
        public int ResponseCode { get; set; }

        public string Error { get; set; }
        public int Code  { get; set; }
    }
}