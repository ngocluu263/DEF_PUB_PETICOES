using System;

using APB.Framework.DataBase;
using System.Configuration;
using APB.Mercury.DataObjects.BanPeticoes;

namespace APB.Mercury.WebInterface.BanPeticoes.Www.DataAccess
{
	/// <summary>
	/// Summary description for Instance
	/// </summary>
	public static class LocalInstance
	{

		#region Properties

		private static ConnectionStringSettings _ActiveConnection = null;

        private static ConnectionInfo _ConnectionInfo = null;

        private static string _StatusAtivo = "";

        private static string _StatusInativo = "";

        private static string _ApplicationMode = "";

		public static ConnectionInfo ConnectionInfo
		{
			get
			{
				if (_ConnectionInfo == null)
					LoadConnectionInfo();

				return _ConnectionInfo;
			}
		}

        public static ConnectionInfo ConnectionWebService
        {
            get
            {
                if (_ConnectionInfo == null)
                    LoadConnectionInfo("web");

                return _ConnectionInfo;
            }
        }

        public static string StatusAtivo
        {
            get
            {
                if (_StatusAtivo == "")
                    LoadStatusAtivo();

                return _StatusAtivo;
            }
        }

        public static string StatusInativo
        {
            get
            {
                if (_StatusInativo == "")
                    LoadStatusInativo();

                return _StatusInativo;
            }
        }
		
		#endregion

		#region Public Methods
		
		public static void LoadConnectionInfo()
		{
			_ActiveConnection = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["ActiveConnectionString"]];

			_ConnectionInfo = new ConnectionInfo(
												_ActiveConnection.ConnectionString,
												(_ActiveConnection.ProviderName.ToLower() == "oracle") ? 0 : 1,
												(ConfigurationManager.AppSettings["ApplicationServerMode"].ToLower() == "web") ? 0 : 1
												);
		}

        public static void LoadConnectionInfo(string ApplicationServer)
        {
            _ActiveConnection = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["ActiveConnectionString"]];

            _ConnectionInfo = new ConnectionInfo(
                                                _ActiveConnection.ConnectionString,
                                                (_ActiveConnection.ProviderName.ToLower() == "oracle") ? 0 : 1,
                                                (ApplicationServer == "web") ? 0 : 1
                                                );
        }

        public static void LoadStatusAtivo()
        {
            _ApplicationMode = ConfigurationManager.AppSettings["ApplicationMode"].ToString();

            if (_ApplicationMode == "OnLine")
                _StatusAtivo = "A";
            else
                _StatusAtivo = "F";
        }

        public static void LoadStatusInativo()
        {
            _ApplicationMode = ConfigurationManager.AppSettings["ApplicationMode"].ToString();

            if (_ApplicationMode == "OnLine")
                _StatusInativo = "I";
            else
                _StatusInativo = "E";
        }

		#endregion

	}
}