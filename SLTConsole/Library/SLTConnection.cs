using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace SLTConsole.Library
{
    public class SLTConnection
    {
        private const String BASE_URL = "https://www.internetvas.slt.lk/SLTVasPortal-war";
        private const String USERNAME_KEY = "j_username";
        private const String PASSWORD_KEY = "j_password";

        private RestClient client;

        private bool isLogged = false;

        public bool IsLogged { get { return isLogged; } }

        public SLTConnection ()
	    {
            client = new RestClient(BASE_URL);
            client.CookieContainer = new System.Net.CookieContainer();
	    }

        public bool Login(String username, String password){
            RestRequest loginRequest = new RestRequest("login/j_security_check", Method.POST);
            loginRequest.AddParameter(USERNAME_KEY, username);
            loginRequest.AddParameter(PASSWORD_KEY, password);
            RestResponse response = (RestResponse)client.Execute(loginRequest);
            isLogged = !response.Content.Contains("login_error");
            return isLogged;
        }

        public void Logout()
        {
            isLogged = false;
            client.CookieContainer = new System.Net.CookieContainer();
        }

        public RestClient Client { get { return client; } }
    }
}
