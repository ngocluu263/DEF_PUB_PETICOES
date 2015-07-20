using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace APB.Mercury.DataObjects.BanPeticoes.QueryDictionaries                                   
{                                                                                     
	public static class SystemUserQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "SystemUser";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gSUSR_ID = new DataField("SUSR_ID", 0);

		public static DataField _SUSR_ID
		{                                                                          
			get { return gSUSR_ID; }                                           
		}

        private static DataField gPES_ID = new DataField("PES_ID", 0);

        public static DataField _PES_ID
        {
            get { return gPES_ID; }
        }

		private static DataField  gSUSR_NAME = new DataField("SUSR_NAME", 1);

		public static DataField _SUSR_NAME
		{                                                                          
			get { return gSUSR_NAME; }                                           
		}

		private static DataField  gSUSR_LOGIN = new DataField("SUSR_LOGIN", 1);

		public static DataField _SUSR_LOGIN
		{                                                                          
			get { return gSUSR_LOGIN; }                                           
		}

		private static DataField  gSUSR_PASSWORD = new DataField("SUSR_PASSWORD", 1);

		public static DataField _SUSR_PASSWORD
		{                                                                          
			get { return gSUSR_PASSWORD; }                                           
		}

		private static DataField  gSUSR_REGDATE = new DataField("SUSR_REGDATE", 2);

		public static DataField _SUSR_REGDATE
		{                                                                          
			get { return gSUSR_REGDATE; }                                           
		}

		private static DataField  gSUSR_REGUSR = new DataField("SUSR_REGUSR", 1);

		public static DataField _SUSR_REGUSR
		{                                                                          
			get { return gSUSR_REGUSR; }                                           
		}

		private static DataField  gSUSR_STATUS = new DataField("SUSR_STATUS", 1);

		public static DataField _SUSR_STATUS
		{                                                                          
			get { return gSUSR_STATUS; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from SystemUser  WHERE SUSR_ID = {0}
		/// </summary>                                                             
		public static string qLoadSystemUser
		{                                                                          
			get { return " select * from SystemUser  WHERE SUSR_ID = {0} "; }
		}                                                                          

		public static string qSystemUserList
		{                                                                          
			get { return @" 
			                 SELECT  SUSR_ID
	  		                       , SUSR_NAME
	  		                       , SUSR_LOGIN
	  		                       , SUSR_PASSWORD
                                   , PES_ID
	  		                       , SUSR_REGDATE
	  		                       , SUSR_REGUSR
	  		                       , SUSR_STATUS
			                    from SystemUser";
		        }                                                                    
		}

        public static string qSystemUserPessoa
        {
            get
            {
                return @" 
			                 SELECT  SUSR_ID
	  		                       , SUSR_NAME
	  		                       , SUSR_LOGIN
	  		                       , SUSR_PASSWORD
                                   , PES.PES_ID
                                   , PES.PES_NOME
	  		                       , SUSR_REGDATE
	  		                       , SUSR_REGUSR
	  		                       , SUSR_STATUS
			                    from SystemUser SUSR, PESSOA PES
                                WHERE SUSR.PES_ID = PES.PES_ID
                                AND PES.PES_STATUS NOT IN ('I','E')
                         ";
            }
        }                                                                  

		public static string qSystemUserCount
		{                                                                          
			get { 
                            return @" select count(*) from SystemUser";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
