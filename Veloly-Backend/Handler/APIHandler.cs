using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace Veloly_Backend.Handler
{
    public class APIHandler
    {
        private static readonly HttpClient Client = new HttpClient();
        //Get the Token from the UserRequest which givens all the informations
        private const string Token = "eyJhbGciOiJOT0tFIiwidHlwIjoiSldUIn0.eyJhbGciOiJOT0tFIiwiY29tcGFueSI6NTI3LCJleHAiOjE1MDUxMjAwMzgsImlzcyI6Im5va2UuY29tIiwibG9nb3V0SWQiOiI1YjA4NjMwMmE2NGE1MDU3MjQ2YjVlNDQ2ZTk2ZTFkOGU0ZDQwNDVkIiwibm9rZVVzZXIiOjU4MDQsInRva2VuVHlwZSI6IndlYiJ9.3585e78a70436926733ba4b7d9271f7f0dd65b4e";
        public string Values { get; set; } = new JavaScriptSerializer().Serialize(new { });
        public string Action { get; set; } = "";

        public async Task<string> RequestPostAsync()
        {
            try
            {
                Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
            }
            catch (Exception e)
            {
                // ignored
            }
            var data = new StringContent(Values, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync("https://v1.api.nokepro.com/" + Action, data );

            return await response.Content.ReadAsStringAsync();
        }


    }
}