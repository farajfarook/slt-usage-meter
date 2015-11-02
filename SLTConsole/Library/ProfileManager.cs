using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace SLTConsole.Library
{
    class ProfileManager
    {
        private const String SERVICE_URL = "application/GetProfile";
        private SLTConnection connection;

        public ProfileManager(SLTConnection connection)
        {
            this.connection = connection;
        }

        public Profile GetProfile()
        {
            RestRequest request = new RestRequest(SERVICE_URL, Method.GET);
            return connection.Client.Execute<Profile>(request).Data;
        }
    }
}
