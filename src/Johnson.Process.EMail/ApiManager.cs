using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using EDoc2;
using System.Configuration;
using Johnson.Process.EMail.Exceptions;

namespace Johnson.Process.EMail
{
	public class ApiManager
	{
		private static Hashtable _apis = new Hashtable();
        private static string Server;
        private static int Port;

        static ApiManager()
        {
            Server = ConfigurationManager.AppSettings["edoc2_serverIp"];
            int.TryParse(ConfigurationManager.AppSettings["edoc2_port"], out Port);
            if (string.IsNullOrEmpty(Server))
            {
                Server = "localhost";
            }
            if (Port <= 0)
            {
                Port = 6260;
            }
        }

        public static void NewApi()
        {
            EDoc2Api api = new EDoc2Api();
            api.Server = Server;
            api.InstanceName = "default";
            bool connected = api.Connect();
            if (!connected)
            {
                throw new TaskEmailNotifySerivceException("api连接失败");
            }
            Api = api;
        }

        private static EDoc2Api api;
        public static EDoc2Api Api
		{
            set
            {
                api = value;
            }
			get
            {
                return api;
			}
		}

        private static string _currentUserToken;
        public static string CurrentUserToken
		{
			get
			{
                return _currentUserToken;
			}
		}
	}
}
