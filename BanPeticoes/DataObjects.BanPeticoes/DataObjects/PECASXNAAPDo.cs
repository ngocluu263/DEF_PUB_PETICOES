using System;                                                                         

using System.Collections.Generic;                                                     
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Xml;

using APB.Framework.DataBase;
using APB.Mercury.DataObjects.BanPeticoes.QueryDictionaries;

namespace APB.Mercury.DataObjects.BanPeticoes
{                                                                                     
     [Serializable]
     public class PECASXNAAPDo
	 {
        #region Private Methods

          private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult) 
          {                                                                                        
              GenericDataObject.ValidateConversion(pValues, pResult);                              
          }                                                                                        

          private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult) 
          {                                                                                        
              GenericDataObject.ValidateRequired(PECASXNAAPQD._PECN_ID, pValues, pResult);
              GenericDataObject.ValidateRequired(PECASXNAAPQD._PECN_REGDATE, pValues, pResult);
              GenericDataObject.ValidateRequired(PECASXNAAPQD._PECN_USUARIO, pValues, pResult);
              GenericDataObject.ValidateRequired(PECASXNAAPQD._PECN_STATUS, pValues, pResult);
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
                                                                                               
             OperationResult lReturn = new OperationResult(PECASXNAAPQD.TableName, PECASXNAAPQD.TableName);      
                                                                                               
             if (!lReturn.HasError)                                                                    
             {                                                                                         
                 try                                                                                   
                 {                                                                                     
                     if (lLocalTransaction)                                                            
                     {                                                                                 
                         lReturn.Trace("Transação local, instanciando banco...");               
                     }                                                                                 
                                                                                               
                     lInsert = new InsertCommand(PECASXNAAPQD.TableName);                                   
                                                                                               
                     lReturn.Trace("Adicionando campos ao objeto de insert");
                                                                                               
                     foreach (DataField lField in pValues.Keys)                                        
                     {                                                                                 
                         lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                     }

                     decimal lSequence;
                     lSequence = DataBaseSequenceControl.GetNext(pInfo, "PECN_ID");
                     lInsert.Fields.Add(PECASXNAAPQD._PECN_ID.Name, lSequence, (ItemType)PECASXNAAPQD._PECN_ID.DBType);
                     lReturn.SequenceControl = lSequence;
                     lReturn.Trace("Executando o Insert");                               
                                                                                               
                     lInsert.Execute(lTransaction, false);                                                    
                                                                                               
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
                 ConnectionInfo pInfo,
                 string pParam
             )                                                                                                                                     
             {                                                                                                                                     
                                                                                                                                           
                 Transaction pTransaction;                                                                                                         
                                                                                                                                           
                 pTransaction = new Transaction(Instance.CreateDatabase(pInfo));                                                                   
                                                                                                                                           
                 bool lLocalTransaction = (pTransaction != null);                                                                                  
                                                                                                                                           
                 UpdateCommand lUpdate;                                                                                                            
                                                                                                                                           
                 OperationResult lReturn = new OperationResult(PECASXNAAPQD.TableName, PECASXNAAPQD.TableName);                                              
                                                                                                                                           
                 ValidateUpdate(pValues, lReturn);                                                                                            
                                                                                                                                           
                 if (lReturn.IsValid)                                                                                                              
                 {                                                                                                                                 
                     try                                                                                                                           
                     {                                                                                                                             
                         if (lLocalTransaction)                                                                                                    
                         {                                                                                                                         
                             lReturn.Trace("Transação local, instanciando banco...");                                                       
                         }                                                                                                                         
                                                                                                                                           
                         lUpdate = new UpdateCommand(PECASXNAAPQD.TableName);                                                                           
                                                                                                                                           
                         lReturn.Trace("Adicionando campos ao objeto de update");                                                           
                         foreach (DataField lField in pValues.Keys)
                         {
                             lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                         }
                                                                                                                                           
                         string lSql = "";
                         lSql = String.Format("WHERE {0} = <<{0}", PECASXNAAPQD._PECN_ID.Name);
                         lUpdate.Condition = lSql;
                         lUpdate.Conditions.Add(PECASXNAAPQD._PECN_ID.Name, pValues[PECASXNAAPQD._PECN_ID].DBToDecimal());

                         lReturn.Trace("Executando o Update");
                         
                         if(pParam == "UPER")
                             lUpdate.Execute(pTransaction);
                         else
                             lUpdate.Execute(pTransaction, false);
                                                                                                                                           
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

                public static DataTable GetAllPETICAOXNAAP
                (                                                           
                    ConnectionInfo pInfo                                    
                )                                                           
                {                                                           
                    string lQuery = "";
                    DataTable lTable = new DataTable();
                                                                    
                    lQuery = PECASXNAAPQD.qLoadPETICAOXNAAP;
                    lQuery += " WHERE PECN_STATUS='A'";
                                                                    
                    SelectCommand lSelect = new SelectCommand(lQuery);            
                    lTable = lSelect.ReturnData(Instance.CreateDatabase(pInfo));  
                                                                    
                    return lTable;                                                
                }

                public static DataTable GetPecasEnviadasHomolog
                (
                    ConnectionInfo pInfo
                    ,string pData
                )
                {
                    string lQuery = "";
                    DataTable lTable = new DataTable();

                    lQuery = PECASXNAAPQD.qConsultaPeticao;
                    lQuery += " WHERE PECN_STATUS='E' ";
                    if (pData != "")
                        lQuery += " AND PECN_REGDATE = TO_DATE('" + pData + "','DD/MM/YYYY') ";
                    lQuery += " ORDER BY PECN_REGDATE DESC ";

                    SelectCommand lSelect = new SelectCommand(lQuery);
                    lTable = lSelect.ReturnData(Instance.CreateDatabase(pInfo));

                    return lTable;
                }

                public static DataTable GetConsultaPeticaoporTags
                (
                    ConnectionInfo pInfo
                    , string pCondicao
                    , int pTipo
                )
                {
                    string lQuery = "";
                    DataTable lTable = new DataTable();

                    lQuery = PECASXNAAPQD.qConsultaPeticao;
                    lQuery += " WHERE PECN_STATUS='A' ";
                    if (pTipo == 0)
                        lQuery += " AND PECN_TAGS LIKE '%" + pCondicao + "%' ";
                    else
                        lQuery += " AND PECN_TAGS LIKE '" + pCondicao + "' ";
                    lQuery += " ORDER BY PECN_ID DESC ";

                    SelectCommand lSelect = new SelectCommand(lQuery);
                    lTable = lSelect.ReturnData(Instance.CreateDatabase(pInfo));

                    return lTable;
                }

                public static DataTable GetConsultaPeticaoporTagsAvancado
                (
                    ConnectionInfo pInfo
                    , string pCondicao
                )
                {
                    string lQuery = "";
                    DataTable lTable = new DataTable();

                    lQuery = PECASXNAAPQD.qConsultaPeticao;
                    lQuery += " WHERE PECN_STATUS='A' ";
                    lQuery += pCondicao;
                    lQuery += " ORDER BY PECN_ID DESC ";

                    SelectCommand lSelect = new SelectCommand(lQuery);
                    lTable = lSelect.ReturnData(Instance.CreateDatabase(pInfo));

                    return lTable;
                }

                public static string InsertPeticao(ConnectionInfo pInfo, string pTPN_ID, string pPETN_TAGS, string pPETN_EMENTA, string pPETN_TEXTO, string pPETN_USUARIO)
                {
                    string lSql = "";
                    string constr = "Data Source=desenv2; User ID=desenv; Password=desenv";

                    try
                    {
                        OracleConnection con = new OracleConnection(constr);

                        lSql = " INSERT INTO PETICAOXNAAP(PETN_ID,TPN_ID,PETN_TEXTO,PETN_TAGS,PETN_EMENTA,PETN_STATUS,PETN_REGDATE,PETN_USUARIO) ";
                        lSql += " VALUES(:PETN_ID,:TPN_ID,:PETN_TEXTO,:PETN_TAGS,:PETN_EMENTA,'A',SYSDATE,:PETN_USUARIO) ";

                        OracleCommand cmd = new OracleCommand();
                        cmd.CommandText = lSql;
                        cmd.Connection = con;

                        decimal lSequence;
                        lSequence = DataBaseSequenceControl.GetNext(pInfo, "PETN_ID");

                        OracleParameter OraParameter1 = new OracleParameter();
                        OracleParameter OraParameter2 = new OracleParameter();
                        OracleParameter OraParameter3 = new OracleParameter();
                        OracleParameter OraParameter4 = new OracleParameter();
                        OracleParameter OraParameter5 = new OracleParameter();
                        OracleParameter OraParameter6 = new OracleParameter();

                        OraParameter1.OracleType = OracleType.Number;
                        OraParameter1.ParameterName = ":PETN_ID";
                        OraParameter1.Value = lSequence;

                        OraParameter2.OracleType = OracleType.Number;
                        OraParameter2.ParameterName = ":TPN_ID";
                        OraParameter2.Value = Convert.ToDecimal(pTPN_ID);

                        OraParameter3.OracleType = OracleType.Clob;
                        OraParameter3.ParameterName = ":PETN_TEXTO";
                        OraParameter3.Value = pPETN_TEXTO;

                        OraParameter4.OracleType = OracleType.VarChar;
                        OraParameter4.ParameterName = ":PETN_TAGS";
                        OraParameter4.Value = pPETN_TAGS;

                        OraParameter5.OracleType = OracleType.VarChar;
                        OraParameter5.ParameterName = ":PETN_EMENTA";
                        OraParameter5.Value = pPETN_EMENTA;

                        OraParameter6.OracleType = OracleType.VarChar;
                        OraParameter6.ParameterName = ":PETN_USUARIO";
                        OraParameter6.Value = pPETN_USUARIO;

                        cmd = new OracleCommand(lSql, con);
                        cmd.Parameters.Add(OraParameter1);
                        cmd.Parameters.Add(OraParameter2);
                        cmd.Parameters.Add(OraParameter3);
                        cmd.Parameters.Add(OraParameter4);
                        cmd.Parameters.Add(OraParameter5);
                        cmd.Parameters.Add(OraParameter6);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        cmd.Dispose();
                        con.Close();

                        return lSequence.ToString();
                    }
                    catch (Exception err)
                    {
                        return "ERRO: " + err.ToString();
                    }

                }

                public static string GetConsultaPeticaoTotal
                (
                    ConnectionInfo pInfo
                )
                {
                    string lQuery = "";
                    DataTable lTable = new DataTable();

                    lQuery = PECASXNAAPQD.qPETICAOXNAAPCount;
                    lQuery += " WHERE PECN_STATUS IN ('A') ";

                    SelectCommand lSelect = new SelectCommand(lQuery);
                    lTable = lSelect.ReturnData(Instance.CreateDatabase(pInfo));

                    return lTable.Rows[0]["CONT"].ToString();
                }

                public static string GetConsultaPecaEnviada
                (
                    ConnectionInfo pInfo
                )
                {
                    string lQuery = "";
                    DataTable lTable = new DataTable();

                    lQuery = PECASXNAAPQD.qPETICAOXNAAPCount;
                    lQuery += " WHERE PECN_STATUS='E' ";

                    SelectCommand lSelect = new SelectCommand(lQuery);
                    lTable = lSelect.ReturnData(Instance.CreateDatabase(pInfo));

                    return lTable.Rows[0]["CONT"].ToString();
                }

                public static string GetConsultaPeticaoTotalPorDia
                (
                    ConnectionInfo pInfo
                    , string pData
                )
                {
                    string lQuery = "";
                    DataTable lTable = new DataTable();

                    lQuery = PECASXNAAPQD.qPETICAOXNAAPCount;
                    lQuery += " WHERE PECN_STATUS IN ('A','E') AND PECN_REGDATE BETWEEN TO_DATE('" + pData + "','DD/MM/YYYY') ";
                    lQuery += " AND TO_DATE('" + pData + " 23:59:59','DD/MM/YYYY HH24:MI:SS') ";

                    SelectCommand lSelect = new SelectCommand(lQuery);
                    lTable = lSelect.ReturnData(Instance.CreateDatabase(pInfo));

                    return lTable.Rows[0]["CONT"].ToString();
                }

                public static DataTable GetConsultaPeticaoPorCondicao
                (
                    ConnectionInfo pInfo
                    , string pCondicao
                )
                {
                    string lQuery = "";
                    DataTable lTable = new DataTable();

                    lQuery = PECASXNAAPQD.qConsultaPeticao;
                    lQuery += " WHERE PECN_STATUS='A' ";
                    lQuery += pCondicao;
                    lQuery += " ORDER BY PECN_ID DESC ";

                    SelectCommand lSelect = new SelectCommand(lQuery);
                    lTable = lSelect.ReturnData(Instance.CreateDatabase(pInfo));

                    return lTable;
                }

                public static DataTable GetConsultaPecaGeral (ConnectionInfo pInfo, string pCondicao){
                    string lQuery = "";
                    DataTable lTable = new DataTable();

                    lQuery = PECASXNAAPQD.qConsultaPeticao;
                    lQuery += " WHERE PECN_STATUS='A' ";
                    lQuery += pCondicao;
                    lQuery += " ORDER BY PECN_ID DESC ";

                    SelectCommand lSelect = new SelectCommand(lQuery);
                    lTable = lSelect.ReturnData(Instance.CreateDatabase(pInfo));

                    return lTable;
                }

        #endregion
     }
}
