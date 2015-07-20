using System;                                                                         

using System.Collections.Generic;                                                     
using System.Data;                                                                    
using System.Xml;                                                                     

using APB.Framework.DataBase;                                                         
using APB.Mercury.DataObjects.BanPeticoes.QueryDictionaries;

namespace APB.Mercury.DataObjects.BanPeticoes
{
    [Serializable]
    public class ModeloPeticaoDo
    {

        #region Private Methods

        private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateConversion(pValues, pResult);
        }


        private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateRequired(ModeloPeticaoQD._MDPT_ID, pValues, pResult);
            GenericDataObject.ValidateRequired(ModeloPeticaoQD._MDPT_REGDATE, pValues, pResult);
            GenericDataObject.ValidateRequired(ModeloPeticaoQD._MDPT_REGUSER, pValues, pResult);
            GenericDataObject.ValidateRequired(ModeloPeticaoQD._MDPT_STATUS, pValues, pResult);
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

            OperationResult lReturn = new OperationResult(ModeloPeticaoQD.TableName, ModeloPeticaoQD.TableName);

            if (!lReturn.HasError)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lInsert = new InsertCommand(ModeloPeticaoQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de insert");

                    foreach (DataField lField in pValues.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }
                    decimal lSequence;
                    lSequence = DataBaseSequenceControl.GetNext(pInfo, "MDPT_ID");
                    lInsert.Fields.Add(ModeloPeticaoQD._MDPT_ID.Name, lSequence, (ItemType)ModeloPeticaoQD._MDPT_ID.DBType);
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
            ConnectionInfo pInfo
        )
        {

            Transaction pTransaction;

            pTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (pTransaction != null);

            UpdateCommand lUpdate;

            OperationResult lReturn = new OperationResult(ModeloPeticaoQD.TableName, ModeloPeticaoQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lUpdate = new UpdateCommand(ModeloPeticaoQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de update");
                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != ModeloPeticaoQD._MDPT_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", ModeloPeticaoQD._MDPT_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(ModeloPeticaoQD._MDPT_ID.Name, pValues[ModeloPeticaoQD._MDPT_ID].DBToDecimal());

                    lReturn.Trace("Executando o Update");

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

        public static DataTable GetAllModeloPeticao
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = ModeloPeticaoQD.qModeloPeticaoTipo;

            SelectCommand lSelect = new SelectCommand(lQuery);
            lTable = lSelect.ReturnData(Instance.CreateDatabase(pInfo));

            return lTable;
        }

        public static DataTable GetModeloPeticaoByID
        (
            decimal pMDPT_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = ModeloPeticaoQD.qModeloPeticaoTexto;
            lQuery += string.Format(" WHERE MDPT_STATUS='A' AND {0} = >>{0}", ModeloPeticaoQD._MDPT_ID.Name);

            SelectCommand lSelect = new SelectCommand(lQuery);

            lSelect.Fields.Add(ModeloPeticaoQD._MDPT_ID.Name, pMDPT_ID, (ItemType)ModeloPeticaoQD._MDPT_ID.DBType);

            lTable = lSelect.ReturnData(Instance.CreateDatabase(pInfo));

            return lTable;
        }

        public static DataTable GetModeloPeticaoByTipo
        (
            decimal pTPPT_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = ModeloPeticaoQD.qModeloPeticaoTexto;
            lQuery += string.Format(" WHERE MDPT_STATUS='A' AND {0} = >>{0}", ModeloPeticaoQD._TPPT_ID.Name);

            SelectCommand lSelect = new SelectCommand(lQuery);

            lSelect.Fields.Add(ModeloPeticaoQD._TPPT_ID.Name, pTPPT_ID, (ItemType)ModeloPeticaoQD._TPPT_ID.DBType);

            lTable = lSelect.ReturnData(Instance.CreateDatabase(pInfo));

            return lTable;
        }

        public static DataTable GetAllTipoPeticao
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = ModeloPeticaoQD.qTipoPeticao;
            lQuery += " WHERE TPPT_STATUS='A' ORDER BY TPPT_DESCRICAO ";

            SelectCommand lSelect = new SelectCommand(lQuery);
            lTable = lSelect.ReturnData(Instance.CreateDatabase(pInfo));

            return lTable;
        }

        #endregion

    }
}
