using System;

using System.Configuration;

namespace APB.Mercury.WebInterface.BanPeticoes.Www
{

	public static class AppParameters
	{

		#region Public Properties

        private static decimal _CD_ID = 0;

        public static decimal CD_ID
        {
            get { return decimal.Parse(ConfigurationManager.AppSettings["CARD_DESIGN"].ToString()); }            
        }
		
		
		#endregion

	}
}