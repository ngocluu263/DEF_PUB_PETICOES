using System;                                                                         

using System.Collections.Generic;                                                     
using System.Data;                                                                    
using System.Xml;                                                                     

using APB.Framework.DataBase;                                                         
using APB.Mercury.DataObjects.BanPeticoes.QueryDictionaries;                                      

namespace APB.Mercury.DataObjects.BanPeticoes                                                 
{
    [Serializable]
    public class SystemUserDo
    {
        #region Private Methods

        private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult)
        {

            GenericDataObject.ValidateConversion(pValues, pResult);

        }

        private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateRequired(SystemUserQD._SUSR_ID, pValues, pResult);

            GenericDataObject.ValidateRequired(SystemUserQD._SUSR_REGDATE, pValues, pResult);

            GenericDataObject.ValidateRequired(SystemUserQD._SUSR_REGUSR, pValues, pResult);

            GenericDataObject.ValidateRequired(SystemUserQD._SUSR_STATUS, pValues, pResult);

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

            OperationResult lReturn = new OperationResult(SystemUserQD.TableName, SystemUserQD.TableName);

            ValidateInsert(pValues, lReturn);

            if (!lReturn.HasError)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lInsert = new InsertCommand(SystemUserQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de insert");

                    foreach (DataField lField in pValues.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    decimal lSequence;

                    lSequence = DataBaseSequenceControl.GetNext(pInfo, "SUSR_ID");

                    lInsert.Fields.Add(SystemUserQD._SUSR_ID.Name, lSequence, (ItemType)SystemUserQD._SUSR_ID.DBType);

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

        public static OperationResult Insert
        (
           DataFieldCollection pValues,
           Transaction pTransaction,
           ConnectionInfo pInfo
        )
        {
            Transaction lTransaction;

            bool lLocalTransaction = (pTransaction == null);

            if (lLocalTransaction)
                lTransaction = new Transaction(Instance.CreateDatabase(pInfo));
            else
                lTransaction = pTransaction;

            InsertCommand lInsert;

            OperationResult lReturn = new OperationResult(SystemUserQD.TableName, SystemUserQD.TableName);

            ValidateInsert(pValues, lReturn);

            if (!lReturn.HasError)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lInsert = new InsertCommand(SystemUserQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de insert");

                    foreach (DataField lField in pValues.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    decimal lSequence;

                    lSequence = DataBaseSequenceControl.GetNext(pInfo, "SUSR_ID");

                    lInsert.Fields.Add(SystemUserQD._SUSR_ID.Name, lSequence, (ItemType)SystemUserQD._SUSR_ID.DBType);

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

            OperationResult lReturn = new OperationResult(SystemUserQD.TableName, SystemUserQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lUpdate = new UpdateCommand(SystemUserQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de update");

                    foreach (DataField lField in pValues.Keys)
                    {
                        lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", SystemUserQD._SUSR_ID.Name);

                    lUpdate.Condition = lSql;

                    lUpdate.Conditions.Add(SystemUserQD._SUSR_ID.Name, pValues[SystemUserQD._SUSR_ID].DBToDecimal());

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


        public static DataTable GetSystemUser(ConnectionInfo pInfo, string pUsuario, string pSenha)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();
            pSenha = APB.Framework.Encryption.Crypto.Encode(pSenha);

            lQuery = SystemUserQD.qSystemUserList;
            lQuery += " WHERE SUSR_STATUS = 'A' AND SUSR_LOGIN = '" + pUsuario + "' AND SUSR_PASSWORD='" + pSenha + "' ";

            SelectCommand lSelect = new SelectCommand(lQuery);

            lTable = lSelect.ReturnData(Instance.CreateDatabase(pInfo));

            return lTable;
        }

        public static string GetPassword(string pSusr_Login, ConnectionInfo pConnectionInfo)
        {
            bool lReturn;
            string lQuery = "";
            string lPassWord = "";
            DataTable lTable;

            lQuery = SystemUserQD.qSystemUserList;
            lQuery += String.Format(" WHERE {0} = >>{0} ", SystemUserQD._SUSR_LOGIN.Name);
            lQuery += " AND SUSR_STATUS='A' ";

            SelectCommand lSelect = new SelectCommand(lQuery);

            lSelect.Fields.Add(SystemUserQD._SUSR_LOGIN.Name, pSusr_Login, (ItemType)SystemUserQD._SUSR_LOGIN.DBType);

            lTable = lSelect.ReturnData(Instance.CreateDatabase(pConnectionInfo));

            lReturn = (lTable.Rows.Count > 0) ? true : false;

            // User Accept, Get Password
            if (lReturn)
            {
                // Decodificar Senha para Envio
                lPassWord = APB.Framework.Encryption.Crypto.Decode(lTable.Rows[0]["SUSR_PASSWORD"].ToString());
            }

            return lPassWord;
        }

        #endregion
    }
}
