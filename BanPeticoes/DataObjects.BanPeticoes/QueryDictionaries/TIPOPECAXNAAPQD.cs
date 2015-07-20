using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace APB.Mercury.DataObjects.BanPeticoes.QueryDictionaries
{
    public static class TIPOPECAXNAAPQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{
            get { return "TIPOPECAXNAAP"; }                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gTPEN_ID = new DataField("TPEN_ID", 0);

		public static DataField _TPEN_ID
		{                                                                          
			get { return gTPEN_ID; }                                           
		}

		private static DataField  gTPEN_DESCRICAO = new DataField("TPEN_DESCRICAO", 1);

		public static DataField _TPEN_DESCRICAO
		{                                                                          
			get { return gTPEN_DESCRICAO; }                                           
		}

		private static DataField  gTPEN_STATUS = new DataField("TPEN_STATUS", 1);

		public static DataField _TPEN_STATUS
		{                                                                          
			get { return gTPEN_STATUS; }                                           
		}

		private static DataField  gTPEN_REGDATE = new DataField("TPEN_REGDATE", 2);

		public static DataField _TPEN_REGDATE
		{                                                                          
			get { return gTPEN_REGDATE; }                                           
		}

		private static DataField  gTPEN_USUARIO = new DataField("TPEN_USUARIO", 1);

		public static DataField _TPEN_USUARIO
		{                                                                          
			get { return gTPEN_USUARIO; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from TIPOPETICAOXNAAP 
		/// </summary>                                                             
        public static string qLoadTIPOPECAXNAAP
		{
            get { return " select * from TIPOPECAXNAAP  "; }
		}

        public static string qTIPOPECAXNAAPList
		{
            get { return @" SELECT * FROM TIPOPECAXNAAP "; }                                                                    
		}

        public static string qTIPOPECAXNAAPCount
		{                                                                          
			get {
                return @" select count(*) from TIPOPECAXNAAP ";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
