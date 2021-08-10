using System;
using System.Configuration;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

namespace P24
{
    public class Przelewy24 : IPrzelewy24
    {
        private RestClient _client;
        private readonly string _baseUrl;
        private readonly int _userName;
        private readonly string _userSecret;
        private readonly string _crc;

        public Przelewy24(string baseUrl, int userName, string userSecret, string crc)
        {
            _baseUrl = baseUrl;
            _userName = userName;
            _userSecret = userSecret;
            _crc = crc;

            InitializeRestClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        }

        private void InitializeRestClient()
        {
            _client = new RestClient(_baseUrl)
            {
                Authenticator = new HttpBasicAuthenticator(_userName.ToString(), _userSecret),
            };

            JsonSerializerSettings serializationSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };
            _client.UseNewtonsoftJson(serializationSettings);
        }

        public async Task<P24TestAccessResponse> TestConnectionAsync()
        {
            var request = new RestRequest("/api/v1/testAccess");

            var response = await _client.ExecuteAsync<P24TestAccessResponse>(request, Method.GET);
            return response.Data;
        }

        private string GenerateSign(string signString)
        {
            using (SHA384 sha384Hash = SHA384.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(signString);
                byte[] hashBytes = sha384Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", "");

                return hash.ToLower();
            }
        }
        public async Task<P24TransactionResponse> NewTransactionAsync(P24TransactionRequest data)
        {
            var signString = $"{{\"sessionId\":\"{data.SessionId}\",\"merchantId\":{data.MerchantId},\"amount\":{data.Amount},\"currency\":\"{data.Currency}\",\"crc\":\"{_crc}\"}}";
            data.Sign = GenerateSign(signString);
            var request = new RestRequest("/api/v1/transaction/register");
            request.AddJsonBody(data);

            var response = await _client.ExecuteAsync<P24TransactionResponse>(request, Method.POST);
            return response.Data;
        }

        public async Task<P24TransactionVerifyResponse> TransactionVerifyAsync(P24TransactionVerifyRequest data)
        {
            var signString = $"{{\"sessionId\":\"{data.SessionId}\",\"orderId\":{data.OrderId},\"amount\":{data.Amount},\"currency\":\"{data.Currency}\",\"crc\":\"{_crc}\"}}";
            data.Sign = GenerateSign(signString);
            var request = new RestRequest("/api/v1/transaction/verify");
            request.AddJsonBody(data);

            
            var response = await _client.ExecuteAsync<P24TransactionVerifyResponse>(request, Method.PUT);
            return response.Data;
        }
    }
}