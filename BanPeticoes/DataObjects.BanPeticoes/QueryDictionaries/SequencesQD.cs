using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace APB.Mercury.DataObjects.BanPeticoes.QueryDictionaries                                   
{                                                                                     
	public static class SequencesQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "Sequences";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gSEQ_NAME = new DataField("SEQ_NAME", 1);

		public static DataField _SEQ_NAME
		{                                                                          
			get { return gSEQ_NAME; }                                           
		}

		private static DataField  gSEQ_CTRLNBR = new DataField("SEQ_CTRLNBR", 0);

		public static DataField _SEQ_CTRLNBR
		{                                                                          
			get { return gSEQ_CTRLNBR; }                                           
		}

		private static DataField  gSEQ_START = new DataField("SEQ_START", 0);

		public static DataField _SEQ_START
		{                                                                          
			get { return gSEQ_START; }                                           
		}

		private static DataField  gSEQ_END = new DataField("SEQ_END", 0);

		public static DataField _SEQ_END
		{                                                                          
			get { return gSEQ_END; }                                           
		}

		private static DataField  gSEQ_VALUE = new DataField("SEQ_VALUE", 0);

		public static DataField _SEQ_VALUE
		{                                                                          
			get { return gSEQ_VALUE; }                                           
		}

		private static DataField  gSEQ_STATUS = new DataField("SEQ_STATUS", 1);

		public static DataField _SEQ_STATUS
		{                                                                          
			get { return gSEQ_STATUS; }                                           
		}

		private static DataField  gSEQ_REGDATE = new DataField("SEQ_REGDATE", 2);

		public static DataField _SEQ_REGDATE
		{                                                                          
			get { return gSEQ_REGDATE; }                                           
		}

		private static DataField  gSEQ_REGUSER = new DataField("SEQ_REGUSER", 1);

		public static DataField _SEQ_REGUSER
		{                                                                          
			get { return gSEQ_REGUSER; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from Sequences  WHERE SEQ_NAME = {0}
		/// </summary>                                                             
		public static string qLoadSequences
		{                                                                          
			get { return " select * from Sequences  WHERE SEQ_NAME = {0} "; }
		}                                                                          

		public static string qSequencesList
		{                                                                          
			get { return @" 
			                 SELECT  SEQ_NAME
	  		                       , SEQ_VALUE
	  		                       , SEQ_STATUS
			                    from Sequences";
		        }                                                                    
		}                                                                          

		public static string qSequencesCount
		{                                                                          
			get { 
                            return @" select count(*) from Sequences";
	 	        }                                                                    
		}

        public static string qSequencesMax
        {
            get
            {
                return @" Select (Max(SEQ_VALUE) + 1) FROM Sequences ";
            }
        }                                                                          


		#endregion
    }
}
