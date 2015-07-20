using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace APB.Mercury.DataObjects.BanPeticoes.QueryDictionaries                                   
{
    public static class SequencesControlQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{
            get { return "SequencesControl"; }                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

        private static DataField gCONTROLNAME = new DataField("CONTROLNAME", 1);

        public static DataField _CONTROLNAME
		{
            get { return gCONTROLNAME; }                                           
		}

        private static DataField gCONTROLVALUE = new DataField("CONTROLVALUE", 0);

        public static DataField _CONTROLVALUE
		{
            get { return gCONTROLVALUE; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from CardDesign  WHERE CD_ID = {0}
		/// </summary>                                                             
        public static string qLoadSequencesControl
		{
            get { return " select * from SequencesControl  WHERE CONTROLNAME = {0} "; }
		}

        public static string qSequencesControlList
		{                                                                          
			get { return @" 
			                 SELECT  CONTROLNAME
	  		                       , CONTROLVALUE
			                    from SequencesControl";
		        }                                                                    
		}

        public static string qSequencesControlCount
		{                                                                          
			get {
                    return @" select count(*) from SequencesControl";
	 	        }                                                                    
		}

        public static string qSequencesControlMax
        {
            get
            {
                return @" Select (Max(CONTROLVALUE) + 1) FROM SequencesControl ";
            }
        }          

		#endregion
    }
}
