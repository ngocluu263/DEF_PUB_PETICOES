using System;

using APB.Framework.DataBase;
using APB.Mercury.Exceptions;

namespace APB.Mercury.DataObjects.BanPeticoes
{
	/// <summary>
	/// Summary description for SequenceControl
	/// </summary>
	public static class DataBaseSequenceControl
	{
		public static decimal GetNext(Transaction pTransaction, string pTableName, string pIDFieldName)
		{
			return GetNext(pTransaction, pTableName, pIDFieldName, "1=1");
		}

		public static decimal GetNext(Transaction pTransaction, string pTableName, string pIDFieldName, string pWhereClause)
		{
			SelectCommand lSelectNext;

			string lSelectQuery = "SELECT Max({0}) + 1 FROM {1} WHERE {2}";

			decimal lID;

			object lScalarReturn;

			lSelectQuery = string.Format(lSelectQuery, pIDFieldName, pTableName, pWhereClause);

			lSelectNext = new SelectCommand(lSelectQuery);

			lScalarReturn = lSelectNext.ReturnScalar(pTransaction);

			if (lScalarReturn == null || lScalarReturn == DBNull.Value) lScalarReturn = 1;

			lID = Convert.ToDecimal(lScalarReturn);

			return lID;
		}

        public static decimal GetNext(ConnectionInfo pInfo, string pIDFieldValue)
        {

            Transaction lTransaction;
            DataBase lDataBase;

            lDataBase = Instance.CreateDatabase(pInfo);

            lTransaction = new Transaction(lDataBase);
            try
            {
                decimal lID;

                // Inicializa operação
                OperationResult lReturn = new OperationResult(QueryDictionaries.SequencesControlQD.TableName, QueryDictionaries.SequencesControlQD.TableName);

                // Recupera Valor
                SelectCommand lSelectNext;

                string lSelectQuery = QueryDictionaries.SequencesControlQD.qSequencesControlMax;
                lSelectQuery += String.Format("WHERE {0} = >>{0}", QueryDictionaries.SequencesControlQD._CONTROLNAME.Name);

                object lScalarReturn;
                //APB.Framework.Math.Module11

                lSelectNext = new SelectCommand(lSelectQuery);

                // Passagem dos Valores de Parametros para a Clausula WHERE [comando SELECT]
                lSelectNext.Fields.Add(QueryDictionaries.SequencesControlQD._CONTROLNAME.Name, pIDFieldValue, ItemType.String);

                // Recupera Valor do Select (Seq_Value)
                lScalarReturn = lSelectNext.ReturnScalar(lTransaction);



                if (lScalarReturn == null || lScalarReturn == DBNull.Value) lScalarReturn = 1;
                lID = Convert.ToDecimal(lScalarReturn);

                // Altera Valor da Sequence
                UpdateCommand lUpdate;
                lUpdate = new UpdateCommand(QueryDictionaries.SequencesControlQD.TableName);

                // Identificação dos Campos a serem Alterados
                lUpdate.Fields.Add(QueryDictionaries.SequencesControlQD._CONTROLVALUE.Name, lID, (ItemType)QueryDictionaries.SequencesControlQD._CONTROLVALUE.DBType);

                string lUpdateQuery;

                lUpdateQuery = String.Format("WHERE {0} = >>{0}", QueryDictionaries.SequencesControlQD._CONTROLNAME.Name);
                lUpdate.Condition = lUpdateQuery;

                // Passagem dos Valores para a Condição Where do Update   
                lUpdate.Conditions.Add(QueryDictionaries.SequencesControlQD._CONTROLNAME.Name, pIDFieldValue);

                // Execução do UPDATE
                lUpdate.Execute(lTransaction);

                lTransaction.Commit();
                // Retorna novo valor da chave [CONTROL VALUE]
                return lID;
            }
            catch (Exception ex)
            {
                lTransaction.Rollback();
                throw new UnknownException(ex);
            }
        }

	}
}