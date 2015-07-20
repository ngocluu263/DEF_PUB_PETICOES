using System;                                                                         

using System.Collections.Generic;                                                     
using System.Data;                                                                    
using System.Xml;                                                                     

using APB.Framework.DataBase;                                                         
using APB.Mercury.DataObjects.BanPeticoes.QueryDictionaries;

namespace APB.Mercury.DataObjects.BanPeticoes
{                                                                                     
     [Serializable] 
     public class PETICAOCONSULTAXNAAPDo
	 {
        #region Private Methods

          private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult) 
          {                                                                                        
              GenericDataObject.ValidateConversion(pValues, pResult);                              
          }                                                                                        

          private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult) 
          {                                                                                        
            GenericDataObject.ValidateRequired(PETICAOCONSULTAXNAAPQD._PCN_REGDATE, pValues, pResult);
            GenericDataObject.ValidateRequired(PETICAOCONSULTAXNAAPQD._PCN_USUARIO, pValues, pResult);
            GenericDataObject.ValidateRequired(PETICAOCONSULTAXNAAPQD._PCN_STATUS , pValues, pResult);
          }                                                                                        

        #endregion

        #region Public Methods
         
         public static DataTable GetAllPETICAOCONSULTAXNAAP
         (                                                           
             ConnectionInfo pInfo                                    
         )                                                           
         {                                                           
             string lQuery = "";
             DataTable lTable = new DataTable();
                                                                     
             lQuery = PETICAOCONSULTAXNAAPQD.qPETICAOCONSULTAXNAAPList;
             lQuery += " WHERE STATUS='A'";
                                                                     
             SelectCommand lSelect = new SelectCommand(lQuery);            
             lTable = lSelect.ReturnData(Instance.CreateDatabase(pInfo));  
                                                                     
             return lTable;                                                
         }                                                           


        #endregion
     }
}
