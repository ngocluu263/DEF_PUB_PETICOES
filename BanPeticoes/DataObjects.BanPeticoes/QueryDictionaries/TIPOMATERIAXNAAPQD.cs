using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace APB.Mercury.DataObjects.BanPeticoes.QueryDictionaries
{
    public static class TIPOMATERIAXNAAPQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{
            get { return "TIPOMATERIAXNAAP"; }                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gTPMN_ID = new DataField("TPMN_ID", 0);

		public static DataField _TPMN_ID
		{                                                                          
			get { return gTPMN_ID; }                                           
		}

		private static DataField  gTPMN_DESCRICAO = new DataField("TPMN_DESCRICAO", 1);

		public static DataField _TPMN_DESCRICAO
		{                                                                          
			get { return gTPMN_DESCRICAO; }                                           
		}

		private static DataField  gTPMN_STATUS = new DataField("TPMN_STATUS", 1);

		public static DataField _TPMN_STATUS
		{                                                                          
			get { return gTPMN_STATUS; }                                           
		}

		private static DataField  gTPMN_REGDATE = new DataField("TPMN_REGDATE", 2);

		public static DataField _TPMN_REGDATE
		{                                                                          
			get { return gTPMN_REGDATE; }                                           
		}

		private static DataField  gTPMN_USUARIO = new DataField("TPMN_USUARIO", 1);

		public static DataField _TPMN_USUARIO
		{                                                                          
			get { return gTPMN_USUARIO; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from TIPOPETICAOXNAAP 
		/// </summary>                                                             
		public static string qLoadTIPOPETICAOXNAAP
		{
            get { return " select * from TIPOMATERIAXNAAP  "; }
		}                                                                          

		public static string qTIPOPETICAOXNAAPList
		{
            get { return @" SELECT * FROM TIPOMATERIAXNAAP "; }                                                                    
		}                                                                          

		public static string qTIPOPETICAOXNAAPCount
		{                                                                          
			get {
                return @" select count(*) from TIPOMATERIAXNAAP ";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
