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
        private string _values;
        private string _action;

        public void Login()
        {
            _action = "company/login/";
            _values = new JavaScriptSerializer().Serialize(new
            {
                username = "jan@werren.com",
                password = "Voyager88",
                companyDomain = "veloly"
            });
        }

        public void LockGetAll()
        {
            _action = "/lock/get/all/";
            _values = new JavaScriptSerializer().Serialize(new
            {
                
            });
        }

        public async Task<string> RequestPostAsync()
        {
            var data = new StringContent(_values, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync("https://v1.api.nokepro.com/" + _action, data );

            return await response.Content.ReadAsStringAsync();
        }


    }
}