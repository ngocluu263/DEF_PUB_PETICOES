using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace APB.Mercury.DataObjects.BanPeticoes.QueryDictionaries
{                                                                                     
	public static class PETICAOCONSULTAXNAAPQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "PETICAOCONSULTAXNAAP";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gPETN_NUMERO = new DataField("PETN_NUMERO", 1);

		public static DataField _PETN_NUMERO
		{                                                                          
			get { return gPETN_NUMERO; }                                           
		}

		private static DataField  gPETN_TEXTO = new DataField("PETN_TEXTO", 3);

		public static DataField _PETN_TEXTO
		{                                                                          
			get { return gPETN_TEXTO; }                                           
		}

		private static DataField  gTPN_ID = new DataField("TPN_ID", 0);

		public static DataField _TPN_ID
		{                                                                          
			get { return gTPN_ID; }                                           
		}

		private static DataField  gPETN_EMENTA = new DataField("PETN_EMENTA", 1);

		public static DataField _PETN_EMENTA
		{                                                                          
			get { return gPETN_EMENTA; }                                           
		}

		private static DataField  gPCN_SESSAO = new DataField("PCN_SESSAO", 1);

		public static DataField _PCN_SESSAO
		{                                                                          
			get { return gPCN_SESSAO; }                                           
		}

		private static DataField  gPCN_STATUS = new DataField("PCN_STATUS", 1);

		public static DataField _PCN_STATUS
		{                                                                          
			get { return gPCN_STATUS; }                                           
		}

		private static DataField  gPCN_REGDATE = new DataField("PCN_REGDATE", 2);

		public static DataField _PCN_REGDATE
		{                                                                          
			get { return gPCN_REGDATE; }                                           
		}

		private static DataField  gPCN_USUARIO = new DataField("PCN_USUARIO", 1);

		public static DataField _PCN_USUARIO
		{                                                                          
			get { return gPCN_USUARIO; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from PETICAOCONSULTAXNAAP 
		/// </summary>                                                             
		public static string qLoadPETICAOCONSULTAXNAAP
		{                                                                          
			get { return " select * from PETICAOCONSULTAXNAAP  "; }
		}                                                                          

		public static string qPETICAOCONSULTAXNAAPList
		{                                                                          
			get { return @" 
			                select * 
			                    from PETICAOCONSULTAXNAAP";
		        }                                                                    
		}                                                                          

		public static string qPETICAOCONSULTAXNAAPCount
		{                                                                          
			get { 
                            return @" select count(*) from PETICAOCONSULTAXNAAP";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
