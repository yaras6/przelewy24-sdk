namespace P24
{
    public class P24TransactionVerifyResponse
    {
        public class TransactionVerifyData
        {
            public string Status { get; set; }
        }
        public TransactionVerifyData Data { get; set; }
        public int ResponseCode { get; set; }
        public string Error { get; set; }
        public int Code { get; set; }
    }
}