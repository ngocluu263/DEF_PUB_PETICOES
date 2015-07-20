using System;

using APB.Framework.DataBase;

namespace APB.Mercury.DataObjects.BanPeticoes
{
	/// <summary>
	/// Summary description for Sequence
	/// </summary>
	public static class DataBaseSequence
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

        public static decimal GetNext(Transaction pTransaction, string pIDFieldValue)
        {
            decimal lID;

            // Inicializa operação
            OperationResult lReturn = new OperationResult(QueryDictionaries.SequencesQD.TableName, QueryDictionaries.SequencesQD.TableName);

            // Recupera Valor
            SelectCommand lSelectNext;

            string lSelectQuery = QueryDictionaries.SequencesQD.qSequencesMax;
            lSelectQuery += String.Format("WHERE {0} = >>{0}", QueryDictionaries.SequencesQD._SEQ_NAME.Name);
            
            object lScalarReturn;

            lSelectNext = new SelectCommand(lSelectQuery);

            // Passagem dos Valores de Parametros para a Clausula WHERE [comando SELECT]
            lSelectNext.Fields.Add(QueryDictionaries.SequencesQD._SEQ_NAME.Name, pIDFieldValue, ItemType.String);

            // Recupera Valor do Select (Seq_Value)
            lScalarReturn = lSelectNext.ReturnScalar(pTransaction);

            if (lScalarReturn == null || lScalarReturn == DBNull.Value) lScalarReturn = 1;
            lID = Convert.ToDecimal(lScalarReturn);
            
            // Altera Valor da Sequence
            UpdateCommand lUpdate;
            lUpdate = new UpdateCommand(QueryDictionaries.SequencesQD.TableName);

            // Identificação dos Campos a serem Alterados
            lUpdate.Fields.Add(QueryDictionaries.SequencesQD._SEQ_VALUE.Name, lID, (ItemType) QueryDictionaries.SequencesQD._SEQ_VALUE.DBType);

            string lUpdateQuery;

            lUpdateQuery = String.Format("WHERE {0} = >>{0}", QueryDictionaries.SequencesQD._SEQ_NAME.Name);
            lUpdate.Condition = lUpdateQuery;

            // Passagem dos Valores para a Condição Where do Update   
            lUpdate.Conditions.Add(QueryDictionaries.SequencesQD._SEQ_NAME.Name, pIDFieldValue);
            
            // Execução do UPDATE
            lUpdate.Execute(pTransaction);

            // Retorna novo valor da chave [SEQUENCE VALUE]
            return lID;
        }


        public static decimal GetNext(ConnectionInfo pInfo, string pIDFieldValue)
        {
            decimal lID = 1;
            Transaction lTransaction;
            DataBase lDataBase;

            lDataBase = Instance.CreateDatabase(pInfo);

            lTransaction = new Transaction(lDataBase);

            try
            {

                // Inicializa operação
                OperationResult lReturn = new OperationResult(QueryDictionaries.SequencesQD.TableName, QueryDictionaries.SequencesQD.TableName);

                // Recupera Valor
                SelectCommand lSelectNext;

                string lSelectQuery = QueryDictionaries.SequencesQD.qSequencesMax;
                lSelectQuery += String.Format("WHERE {0} = >>{0}", QueryDictionaries.SequencesQD._SEQ_NAME.Name);

                object lScalarReturn;

                lSelectNext = new SelectCommand(lSelectQuery);

                // Passagem dos Valores de Parametros para a Clausula WHERE [comando SELECT]
                lSelectNext.Fields.Add(QueryDictionaries.SequencesQD._SEQ_NAME.Name, pIDFieldValue, ItemType.String);

                // Recupera Valor do Select (Seq_Value)
                lScalarReturn = lSelectNext.ReturnScalar(lTransaction);

                if (lScalarReturn == null || lScalarReturn == DBNull.Value) lScalarReturn = 1;
                lID = Convert.ToDecimal(lScalarReturn);

                // Altera Valor da Sequence
                UpdateCommand lUpdate;
                lUpdate = new UpdateCommand(QueryDictionaries.SequencesQD.TableName);

                // Identificação dos Campos a serem Alterados
                lUpdate.Fields.Add(QueryDictionaries.SequencesQD._SEQ_VALUE.Name, lID, (ItemType)QueryDictionaries.SequencesQD._SEQ_VALUE.DBType);

                string lUpdateQuery;

                lUpdateQuery = String.Format("WHERE {0} = >>{0}", QueryDictionaries.SequencesQD._SEQ_NAME.Name);
                lUpdate.Condition = lUpdateQuery;

                // Passagem dos Valores para a Condição Where do Update   
                lUpdate.Conditions.Add(QueryDictionaries.SequencesQD._SEQ_NAME.Name, pIDFieldValue);

                // Execução do UPDATE
                lUpdate.Execute(lTransaction);

                lTransaction.Commit();
            }
            catch (Exception ex)
            {
                lTransaction.Rollback();
            }

            // Retorna novo valor da chave [SEQUENCE VALUE]
            return lID;
        }
	}
}