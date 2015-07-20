using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace APB.Mercury.DataObjects.BanPeticoes.QueryDictionaries
{
    public static class PECASXNAAPQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{
            get { return "PECASXNAAP"; }
		}

		#endregion                                                                         

		#region Database Fields

		private static DataField  gPECN_ID = new DataField("PECN_ID", 0);

		public static DataField _PECN_ID
		{                                                                          
			get { return gPECN_ID; }                                           
		}

		private static DataField  gTPMN_ID = new DataField("TPMN_ID", 0);

		public static DataField _TPMN_ID
		{                                                                          
			get { return gTPMN_ID; }                                           
		}

        private static DataField gTPEN_ID = new DataField("TPEN_ID", 0);

        public static DataField _TPEN_ID
        {
            get { return gTPEN_ID; }
        }

		private static DataField  gPECN_NUMERO = new DataField("PECN_NUMERO", 1);

		public static DataField _PECN_NUMERO
		{                                                                          
			get { return gPECN_NUMERO; }                                           
		}

		private static DataField  gPECN_TEXTO = new DataField("PECN_TEXTO", 5);

		public static DataField _PECN_TEXTO
		{                                                                          
			get { return gPECN_TEXTO; }                                           
		}

		private static DataField  gPECN_TAGS = new DataField("PECN_TAGS", 1);

		public static DataField _PECN_TAGS
		{                                                                          
			get { return gPECN_TAGS; }                                           
		}

		private static DataField  gPECN_EMENTA = new DataField("PECN_EMENTA", 1);

		public static DataField _PECN_EMENTA
		{                                                                          
			get { return gPECN_EMENTA; }                                           
		}

		private static DataField  gPECN_STATUS = new DataField("PECN_STATUS", 1);

		public static DataField _PECN_STATUS
		{                                                                          
			get { return gPECN_STATUS; }                                           
		}

		private static DataField  gPECN_REGDATE = new DataField("PECN_REGDATE", 2);

		public static DataField _PECN_REGDATE
		{                                                                          
			get { return gPECN_REGDATE; }                                           
		}

		private static DataField  gPECN_USUARIO = new DataField("PECN_USUARIO", 1);

		public static DataField _PECN_USUARIO
		{                                                                          
			get { return gPECN_USUARIO; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from PETICAOXNAAP 
		/// </summary>                                                             
		public static string qLoadPETICAOXNAAP
		{
            get { return " select * from PECASXNAAP  "; }
		}

        public static string qConsultaPeticao {
            get {
                return @" 
						SELECT 
							PECN.PECN_ID,
							PECN.TPEN_ID,
							PECN.TPMN_ID,
							PECN.PECN_TEXTO,
							TPMN.TPMN_DESCRICAO,
							TPEN.TPEN_DESCRICAO,
							PECN.PECN_NUMERO,
							PECN.PECN_EMENTA,
							PECN.PECN_TAGS 
						FROM
							PECASXNAAP PECN 
						JOIN TIPOMATERIAXNAAP TPMN
							ON (PECN.TPMN_ID = TPMN.TPMN_ID)
						JOIN TIPOPECAXNAAP TPEN
							ON (PECN.TPEN_ID = TPEN.TPEN_ID) ";
		        }                                            
		}                                                                          

		public static string qPETICAOXNAAPCount
		{                                                                          
			get {
                return @" SELECT COUNT(*) AS CONT FROM PECASXNAAP";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
