using System;                                                                         

using System.Collections.Generic;                                                     
using System.Data;                                                                    
using System.Xml;                                                                     

using APB.Framework.DataBase;                                                         
using APB.Mercury.DataObjects.BanPeticoes.QueryDictionaries;

namespace APB.Mercury.DataObjects.BanPeticoes
{                                                                                     
     [Serializable]
     public class TIPOMATERIAXNAAPDo
	 {
        #region Private Methods

          private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult) 
          {                                                                                        
              GenericDataObject.ValidateConversion(pValues, pResult);                              
          }                                                                                        


          private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult) 
          {                                                                                        
            GenericDataObject.ValidateRequired(TIPOMATERIAXNAAPQD._TPMN_ID, pValues, pResult);
            GenericDataObject.ValidateRequired(TIPOMATERIAXNAAPQD._TPMN_REGDATE, pValues, pResult);
            GenericDataObject.ValidateRequired(TIPOMATERIAXNAAPQD._TPMN_USUARIO, pValues, pResult);
            GenericDataObject.ValidateRequired(TIPOMATERIAXNAAPQD._TPMN_STATUS , pValues, pResult);
          }                                                                                        

         #endregion

        #region Public Methods
         public static OperationResult Insert                                                          
         (                                                                                             
            DataFieldCollection pValues,                                                               
            ConnectionInfo pInfo                                                                       
         )         
         {                                                                                             
             Transaction lTransaction;                                                                 
                                                                                               
             lTransaction = new Transaction(Instance.CreateDatabase(pInfo));                           
                                                                                               
             bool lLocalTransaction = (lTransaction != null);                                          
                                                                                               
             InsertCommand lInsert;                                                                    
                                                                                               
             OperationResult lReturn = new OperationResult(TIPOMATERIAXNAAPQD.TableName, TIPOMATERIAXNAAPQD.TableName);      
                                                                                               
             if (!lReturn.HasError)                                                                    
             {                                                                                         
                 try                                                                                   
                 {                                                                                     
                     if (lLocalTransaction)                                                            
                     {                                                                                 
                         lReturn.Trace("Transação local, instanciando banco...");               
                     }                                                                                 
                                                                                               
                     lInsert = new InsertCommand(TIPOMATERIAXNAAPQD.TableName);                                   
                                                                                               
                     lReturn.Trace("Adicionando campos ao objeto de insert");                   
                                                                                               
                     foreach (DataField lField in pValues.Keys)                                        
                     {                                                                                 
                         lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);    
                     }

                     decimal lSequence;
                     lSequence = DataBaseSequenceControl.GetNext(pInfo, "TPMN_ID");
                     lInsert.Fields.Add(TIPOMATERIAXNAAPQD._TPMN_ID.Name, lSequence, (ItemType)TIPOMATERIAXNAAPQD._TPMN_ID.DBType);
                                       
                     lReturn.Trace("Executando o Insert");                                      
                                                                                               
                     lInsert.Execute(lTransaction);                                                    
                                                                                               
                     if (!lReturn.HasError)                                                            
                     {                                                                                 
                         if (lLocalTransaction)                                                        
                         {                                                                             
                             if (!lReturn.HasError)                                                    
                             {                                                                         
                                 lReturn.Trace("Insert finalizado, executando commit");         
                                                                                               
                                 lTransaction.Commit();                                                
                             }                                                                         
                             else                                                                      
                             {                                                                         
                                 lTransaction.Rollback();                                              
                             }                                                                         
                         }                                                                             
                     }                                                                                 
                     else                                                                              
                     {                                                                                 
                         if (lLocalTransaction)                                                        
                             lTransaction.Rollback();                                                  
                     }                                                                                 
                 }                                                                                     
                 catch (Exception ex)                                                                  
                 {
                     lReturn.OperationException = new SerializableException(ex);

                     if (lLocalTransaction)
                         lTransaction.Rollback();
                 }
             }

             return lReturn;
         }

         public static OperationResult Update
         (                                                                                                                                     
             DataFieldCollection pValues,                                                                                                 
             ConnectionInfo pInfo                                                                                                              
         )                                                                                                                                     
         {                                                                                                                                     
                                                                                                                                           
             Transaction pTransaction;                                                                                                         
                                                                                                                                           
             pTransaction = new Transaction(Instance.CreateDatabase(pInfo));                                                                   
                                                                                                                                           
             bool lLocalTransaction = (pTransaction != null);                                                                                  
                                                                                                                                           
             UpdateCommand lUpdate;                                                                                                            
                                                                                                                                           
             OperationResult lReturn = new OperationResult(TIPOMATERIAXNAAPQD.TableName, TIPOMATERIAXNAAPQD.TableName);                                              
                                                                                                                                           
             ValidateUpdate(pValues, lReturn);                                                                                            
                                                                                                                                           
             if (lReturn.IsValid)                                                                                                              
             {                                                                                                                                 
                 try                                                                                                                           
                 {                                                                                                                             
                     if (lLocalTransaction)                                                                                                    
                     {                                                                                                                         
                         lReturn.Trace("Transação local, instanciando banco...");                                                       
                     }                                                                                                                         
                                                                                                                                           
                     lUpdate = new UpdateCommand(TIPOMATERIAXNAAPQD.TableName);                                                                           
                                                                                                                                           
                     lReturn.Trace("Adicionando campos ao objeto de update");                                                           
                     foreach (DataField lField in pValues.Keys)
                     {
                           lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                     }
                                                                                                                                           
                     string lSql = "";
                     lSql = String.Format("WHERE {0} = <<{0}", TIPOMATERIAXNAAPQD._TPMN_ID.Name);
                     lUpdate.Condition = lSql;
                     lUpdate.Conditions.Add(TIPOMATERIAXNAAPQD._TPMN_ID.Name, pValues[TIPOMATERIAXNAAPQD._TPMN_ID].DBToDecimal());
                                                                                                                                           
                     lReturn.Trace("Executando o Update");                                                                              
                                                                                                                                           
                     lUpdate.Execute(pTransaction);                                                                                            
                                                                                                                                           
                     if (!lReturn.HasError)                                                                                                    
                     {                                                                                                                         
                         if (lLocalTransaction)                                                                                                
                         {                                                                                                                     
                             if (!lReturn.HasError)                                                                                            
                             {                                                                                                                 
                                 lReturn.Trace("Update finalizado, executando commit");                                                 
                                                                                                                                           
                                 pTransaction.Commit();                                                                                        
                             }                                                                                                                 
                             else                                                                                                              
                             {                                                                                                                 
                                 pTransaction.Rollback();                                                                                      
                             }                                                                                                                 
                         }                                                                                                                     
                     }                                                                                                                         
                     else                                                                                                                      
                     {                                                                                                                         
                         if (lLocalTransaction)                                                                                                
                             pTransaction.Rollback();                                                                                          
                     }                                                                                                                         
                 }                                                                                                                             
                 catch (Exception ex)                                                                                                          
                 {                                                                                                                             
                     lReturn.OperationException = new SerializableException(ex);                                                               
                                                                                                                                           
                     if (lLocalTransaction)                                                                                                    
                         pTransaction.Rollback();                                                                                              
                 }                                                                                                                             
             }                                                                                                                                 
                                                                                                                                           
             return lReturn;                                                                                                                   
         }

         public static DataTable GetAllTIPOMATERIAXNAAP
         (
             ConnectionInfo pInfo
         )
         {
             string lQuery = "";
             DataTable lTable = new DataTable();

             lQuery = TIPOMATERIAXNAAPQD.qTIPOPETICAOXNAAPList;
             lQuery += " WHERE TPMN_STATUS='A' ORDER BY TPMN_DESCRICAO ";

             SelectCommand lSelect = new SelectCommand(lQuery);
             lTable = lSelect.ReturnData(Instance.CreateDatabase(pInfo));

             return lTable;
         }

         #endregion
     }
}
