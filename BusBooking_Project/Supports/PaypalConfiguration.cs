using Microsoft.Extensions.Configuration;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supports
{
    public class PaypalConfiguration
    {
        public IConfiguration Configuration;
        private string ClientId;
        private string ClientSecrect;

        public PaypalConfiguration(IConfiguration _Configuration)
        {
            Configuration = _Configuration;
            ClientId = Configuration["Paypal:ClientID"];
            ClientSecrect = Configuration["Paypal:ClientSecrect"];
        }

        public Dictionary<string, string> GetConfig()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("Mode", "sanbox");
            keyValuePairs.Add("ConnectionTimeout", "360000");
            keyValuePairs.Add("RequestRetries", "1");
            keyValuePairs.Add("ClientID", ClientId);
            keyValuePairs.Add("ClientSecrect", ClientSecrect);
            return keyValuePairs;

        }

        public string getAccessToken() => new OAuthTokenCredential(ClientId, ClientSecrect, GetConfig()).GetAccessToken();

        public APIContext GetApiConetxt()
        {
            APIContext aPIContext = new APIContext(getAccessToken());
            aPIContext.Config = GetConfig();
            return aPIContext;
        }
    }

    //public class PaypalConfiguration
    //{
    //    public readonly static string ClientId;
    //    public readonly static string ClientSecrect;

    //    static PaypalConfiguration()
    //    {
    //        ClientId = "ASEmrNjq0ijAVugfFLhJij-KZcCfk1fmcUCTrJFDE2D9QlO6Cl3OKqGui4hNbe7ooI96_URCY8FxVKIW";
    //        ClientSecrect = "EBF7WhCSU1KjTzYZuZz3V2Nbiel34J7gIoWT9o2-V5AKSoBcHSw5BqohYOv-YkUlE7VOvYL-M4GCl3W5";
    //    }

    //    public static Dictionary<string, string> GetConfig()
    //    {
    //        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
    //        keyValuePairs.Add("Mode", "sanbox");
    //        keyValuePairs.Add("ConnectionTimeout", "360000");
    //        keyValuePairs.Add("RequestRetries", "1");
    //        keyValuePairs.Add("ClientID", "ASEmrNjq0ijAVugfFLhJij-KZcCfk1fmcUCTrJFDE2D9QlO6Cl3OKqGui4hNbe7ooI96_URCY8FxVKIW");
    //        keyValuePairs.Add("ClientSecrect", "EBF7WhCSU1KjTzYZuZz3V2Nbiel34J7gIoWT9o2-V5AKSoBcHSw5BqohYOv-YkUlE7VOvYL-M4GCl3W5");
    //        return keyValuePairs;

    //    }

    //    public static string getAccessToken() => new OAuthTokenCredential(ClientId, ClientSecrect, GetConfig()).GetAccessToken();

    //    public static APIContext GetApiConetxt()
    //    {
    //        APIContext aPIContext = new APIContext(getAccessToken());
    //        aPIContext.Config = GetConfig();
    //        return aPIContext;
    //    }
    //}
}
