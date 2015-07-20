using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;

namespace APB.Mercury.DataObjects.BanPeticoes.QueryDictionaries
{                                                                                     
	public static class ModeloPeticaoQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "ModeloPeticao";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gMDPT_ID = new DataField("MDPT_ID", 0);

		public static DataField _MDPT_ID
		{                                                                          
			get { return gMDPT_ID; }                                           
		}

		private static DataField  gMDPT_DESCRICAO = new DataField("MDPT_DESCRICAO", 1);

		public static DataField _MDPT_DESCRICAO
		{                                                                          
			get { return gMDPT_DESCRICAO; }                                           
		}

        private static DataField gMDPT_TEXTO = new DataField("MDPT_TEXTO", 1);

        public static DataField _MDPT_TEXTO
        {
            get { return gMDPT_TEXTO; }
        }


        private static DataField gTPPT_ID = new DataField("TPPT_ID", 0);

        public static DataField _TPPT_ID
        {
            get { return gTPPT_ID; }
        }

		private static DataField  gMDPT_REGDATE = new DataField("MDPT_REGDATE", 2);

		public static DataField _MDPT_REGDATE
		{                                                                          
			get { return gMDPT_REGDATE; }                                           
		}

		private static DataField  gMDPT_REGUSER = new DataField("MDPT_REGUSER", 1);

		public static DataField _MDPT_REGUSER
		{                                                                          
			get { return gMDPT_REGUSER; }                                           
		}

		private static DataField  gMDPT_STATUS = new DataField("MDPT_STATUS", 1);

		public static DataField _MDPT_STATUS
		{                                                                          
			get { return gMDPT_STATUS; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from ModeloPeticao  WHERE MDPT_ID = {0}
		/// </summary>                                                             
		public static string qLoadModeloPeticao
		{                                                                          
			get { return " select * from ModeloPeticao  WHERE MDPT_ID = {0} "; }
		}                                                                          

		public static string qModeloPeticaoList
		{                                                                          
			get { return @" 
			                select MDPT_ID,
                                    MDPT_DESCRICAO,
                                    MDPT_REGDATE,
                                    MDPT_REGUSER,
                                    MDPT_STATUS
			                    from ModeloPeticao";
		        }                                                                    
		}

        public static string qModeloPeticaoTipo
        {
            get
            {
                return @" 
			                SELECT MDPT_ID, MDPT_DESCRICAO, MDPT_REGDATE, MDPT_REGUSER, MDPT_STATUS,
                                    TPPT.TPPT_ID, TPPT.TPPT_DESCRICAO
			                FROM MODELOPETICAO MDPT, TIPOPETICAO TPPT
                            WHERE MDPT.TPPT_ID = TPPT.TPPT_ID
                            AND MDPT.MDPT_STATUS = 'A'
                            AND TPPT.TPPT_STATUS = 'A'
                        ";
            }
        }                                                              

		public static string qModeloPeticaoCount
		{                                                                          
			get { 
                            return @" select count(*) from ModeloPeticao";
	 	        }                                                                    
		}

        public static string qModeloPeticaoTexto
        {
            get
            {
                return @" 
			                select MDPT_ID,
                                    MDPT_DESCRICAO,
                                    MDPT_TEXTO,
                                    MDPT_REGDATE,
                                    MDPT_REGUSER,
                                    MDPT_STATUS
			                    from ModeloPeticao";
            }
        }

        public static string qTipoPeticao
        {
            get
            {
                return @"
                            SELECT TPPT_ID, TPPT_DESCRICAO
                            FROM TIPOPETICAO
                        ";
            }
        }   

		#endregion
    }
}
