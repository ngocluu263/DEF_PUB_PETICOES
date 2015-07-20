using System;                                                                         

using System.Collections.Generic;                                                     
using System.Data;                                                                    
using System.Xml;                                                                     

using APB.Framework.DataBase;                                                         
using APB.Mercury.DataObjects.BanPeticoes.QueryDictionaries;

namespace APB.Mercury.DataObjects.BanPeticoes
{                                                                                     
     [Serializable]
     public class TIPOPECAXNAAPDo
	 {
        #region Private Methods

          private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult) 
          {                                                                                        
              GenericDataObject.ValidateConversion(pValues, pResult);                              
          }                                                                                        


          private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult) 
          {                                                                                        
            GenericDataObject.ValidateRequired(TIPOPECAXNAAPQD._TPEN_ID, pValues, pResult);
            GenericDataObject.ValidateRequired(TIPOPECAXNAAPQD._TPEN_REGDATE, pValues, pResult);
            GenericDataObject.ValidateRequired(TIPOPECAXNAAPQD._TPEN_USUARIO, pValues, pResult);
            GenericDataObject.ValidateRequired(TIPOPECAXNAAPQD._TPEN_STATUS , pValues, pResult);
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
                                                                                               
             OperationResult lReturn = new OperationResult(TIPOPECAXNAAPQD.TableName, TIPOPECAXNAAPQD.TableName);      
                                                                                               
             if (!lReturn.HasError)                                                                    
             {                                                                                         
                 try                                                                                   
                 {                                                                                     
                     if (lLocalTransaction)                                                            
                     {                                                                                 
                         lReturn.Trace("Transação local, instanciando banco...");               
                     }                                                                                 
                                                                                               
                     lInsert = new InsertCommand(TIPOPECAXNAAPQD.TableName);                                   
                                                                                               
                     lReturn.Trace("Adicionando campos ao objeto de insert");                   
                                                                                               
                     foreach (DataField lField in pValues.Keys)                                        
                     {                                                                                 
                         lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);    
                     }

                     decimal lSequence;
                     lSequence = DataBaseSequenceControl.GetNext(pInfo, "TPEN_ID");
                     lInsert.Fields.Add(TIPOPECAXNAAPQD._TPEN_ID.Name, lSequence, (ItemType)TIPOPECAXNAAPQD._TPEN_ID.DBType);
                                       
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
                                                                                                                                           
             OperationResult lReturn = new OperationResult(TIPOPECAXNAAPQD.TableName, TIPOPECAXNAAPQD.TableName);                                              
                                                                                                                                           
             ValidateUpdate(pValues, lReturn);                                                                                            
                                                                                                                                           
             if (lReturn.IsValid)                                                                                                              
             {                                                                                                                                 
                 try                                                                                                                           
                 {                                                                                                                             
                     if (lLocalTransaction)                                                                                                    
                     {                                                                                                                         
                         lReturn.Trace("Transação local, instanciando banco...");                                                       
                     }                                                                                                                         
                                                                                                                                           
                     lUpdate = new UpdateCommand(TIPOPECAXNAAPQD.TableName);                                                                           
                                                                                                                                           
                     lReturn.Trace("Adicionando campos ao objeto de update");                                                           
                     foreach (DataField lField in pValues.Keys)
                     {
                           lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                     }
                                                                                                                                           
                     string lSql = "";
                     lSql = String.Format("WHERE {0} = <<{0}", TIPOPECAXNAAPQD._TPEN_ID.Name);
                     lUpdate.Condition = lSql;
                     lUpdate.Conditions.Add(TIPOPECAXNAAPQD._TPEN_ID.Name, pValues[TIPOPECAXNAAPQD._TPEN_ID].DBToDecimal());
                                                                                                                                           
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

         public static DataTable GetAllTIPOPECAXNAAP
         (
             ConnectionInfo pInfo
         )
         {
             string lQuery = "";
             DataTable lTable = new DataTable();

             lQuery = TIPOPECAXNAAPQD.qTIPOPECAXNAAPList;
             lQuery += " WHERE TPEN_STATUS='A' ORDER BY TPEN_DESCRICAO ";

             SelectCommand lSelect = new SelectCommand(lQuery);
             lTable = lSelect.ReturnData(Instance.CreateDatabase(pInfo));

             return lTable;
         }

         #endregion
     }
}
