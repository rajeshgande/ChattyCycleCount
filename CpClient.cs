using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;

namespace ChattyCycleCount
{
    public class CpClient
    {
        public int CycleCount(string itemName, int quanity, string strength, string units)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = ConfigurationManager.AppSettings["CPURL"];
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Add("x-clientId", "00000000-0000-0000-0000-000000000000");
                    client.DefaultRequestHeaders.Add("x-apikey", "sdkfjnb983t47h39gn7fh92fg39ng9h87ghf298h");

                    var content = new FormUrlEncodedContent(new[]
                    {
                new KeyValuePair<string, string>("ItemName", itemName),
                new KeyValuePair<string, string>("QuantityOnHand", quanity.ToString())
            });

                    var result = client.PostAsync(string.Format("/api/items?ItemName={0}&QuantityOnHand={1}&strength={2}&units={3}", itemName, quanity, strength, units), content).Result;
                    var resutlstr = result.Content.ReadAsStringAsync().Result;
                    int resultint = 0;
                    int.TryParse(resutlstr, out resultint);
                    return resultint;
                }
            }
            catch (Exception)
            {

                return -1;
            }
           
        }
    }
}
