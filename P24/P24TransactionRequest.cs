using System.Collections.Generic;

namespace P24
{
    /// <summary>
    /// Transaction request with required data
    /// If you need additional data, you can inherit this class
    /// </summary>
    public class P24TransactionRequest
    {
        public int MerchantId { get; set; }
        public int PosId { get; set; }
        public string SessionId { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        //public string Client { get; set; }
        //public string Address { get; set; }
        //public string Zip { get; set; }
        //public string City { get; set; }
        public string Country { get; set; }
        //public string Phone { get; set; }
        public string Language { get; set; }
        //public int Method { get; set; }
        public string UrlReturn { get; set; }
        public string UrlStatus { get; set; }
        //public int TimeLimit { get; set; }
        //public int Channel { get; set; }
        //public bool WaitForResult { get; set; }
        //public bool RegulationAccept { get; set; }
        //public int Shipping { get; set; }
        //public string TransferLabel { get; set; }
        //public int MobileLib { get; set; }
        //public string SdkVersion { get; set; }
        public string Sign { get; set; }
        //public string Encoding { get; set; }
        //public string MethodRefId { get; set; }
        //public List<P24Cart> Cart { get; set; }
        //public object Additional { get; set; }
    }
}