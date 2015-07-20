using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using APB.Framework.DataBase;

namespace APB.Mercury.DataObjects.BanPeticoes
{
	public static class Select
	{
		#region Public Methods

        public static DataTable TableFrom(string pSelectQuery, DataFieldCollection pConditionalParameters, ConnectionInfo pInfo)
        {
            DataSet lSet = From(pInfo, pSelectQuery, pConditionalParameters);

            return lSet.Tables[0];
        }

        public static DataSet From(ConnectionInfo pInfo, string pSelectQuery, DataFieldCollection pConditionalParameters)
        {

            DataBase lDB;
            Transaction lTransaction = null;
            DataSet lReturn = new DataSet();

            try
            {

                lDB = Instance.CreateDatabase(pInfo);

                lTransaction = new Transaction(lDB);

                lReturn = From(lTransaction, pSelectQuery, pConditionalParameters);

                lTransaction.Commit();
            }
            catch (Exception lException)
            {

                if (lTransaction != null)
                    lTransaction.Rollback();

                throw lException;
            }


            return lReturn;

        }

        public static DataSet From(Transaction pTransaction, string pSelectQuery, DataFieldCollection pConditionalParameters)
        {
            bool lSplitQuery = false;

            List<string> lQueries = new List<string>();

            SqlQuery lSelect;

            DataSet lReturn = new DataSet();

            DataTable lTable;

            if (pTransaction.DataBase.DataBaseType == 0 && pSelectQuery.Contains("-- table"))
            {
                lSplitQuery = true;

                //divide queries que tem mais de uma tabela em várias queries diferentes,
                //pra pegar no oracle. o indicador deve estar no formato:
                // -- table XX
                // com dois traços, um espaço, a palavra "table" em lower case, outro espaco, 
                // e o numero da tabela com pad de 1 zero. suporta até 99 tabelas, e a tabela zero não
                // precisa de indicador.

                byte lCounter = 1;

                int lStartIndex = 0, lEndIndex = 0;

                string lFormat = "-- table {0}";

                string lTempQuery, lPreviousTable = "", lCurrentTable = "";

                while (lCurrentTable == "" || pSelectQuery.Contains(lCurrentTable))
                {
                    lCurrentTable = string.Format(lFormat, lCounter.ToString().PadLeft(2, '0'));

                    if (lPreviousTable != "")
                        lStartIndex = pSelectQuery.IndexOf(lPreviousTable);

                    lEndIndex = pSelectQuery.IndexOf(lCurrentTable);

                    if (lEndIndex == -1) lEndIndex = pSelectQuery.Length;

                    lTempQuery = pSelectQuery.Substring(lStartIndex, lEndIndex - lStartIndex);

                    lQueries.Add(lTempQuery);

                    lPreviousTable = lCurrentTable;

                    lCounter++;
                }
            }
            else
            {
                lQueries.Add(pSelectQuery);
            }

            for (int a = 0; a < lQueries.Count; a++)
            {
                lSelect = new SqlQuery(pTransaction, lQueries[a]);

                if (pConditionalParameters != null)
                {
                    foreach (DataField lField in pConditionalParameters.Keys)
                    {
                        if (lSelect.Query.Contains(string.Format(">>{0}", lField.Name)))
                            lSelect.AddParameter(lField.Name, pConditionalParameters[lField]);
                    }
                }

                if (lSplitQuery)
                {
                    lTable = lSelect.ExecuteDataTable().Copy();

                    lTable.TableName = string.Format("Table {0}", a);

                    lReturn.Tables.Add(lTable);
                }
                else
                {
                    //não tem problema estar dentro do loop pq vai ser sempre 1 query só,
                    //mas retornando várias tabelas no dataset
                    lReturn = lSelect.ExecuteDataSet();
                }
            }

            return lReturn;
        }

		public static DataSet From(ConnectionInfo pInfo, string pSelectQuery)
		{
			SqlQuery lSelect = new SqlQuery(Instance.CreateDatabase(pInfo), pSelectQuery);

			DataSet lReturn = lSelect.ExecuteDataSet();

			return lReturn;
		}

		public static DataTable From(ConnectionInfo pInfo, string pSelectQuery, int pPage, int pRowCount)
		{
			//TODO: Depois vai ter que criptografar pConnectionString e pSelectQuery

			SelectCommand lSelect = new SelectCommand(pSelectQuery);

			DataTable lTable = lSelect.ReturnData(Instance.CreateDatabase(pInfo), pPage, pRowCount).dataTable;

			return lTable;
		}

		/// <summary>
		/// Retorna o count, espera já uma query de count
		/// </summary>
		/// <param name="pConnectionString">String de conexão com o banco</param>
		/// <param name="pSelectQuery">Query para rodar (ex.: SELECT Count(PRV_ID) FROM Providers)</param>
		/// <returns></returns>
		public static decimal Count(ConnectionInfo pInfo, string pSelectQuery)
		{
			//TODO: Depois vai ter que criptografar pConnectionString e pSelectQuery

			SelectCommand lSelect = new SelectCommand(pSelectQuery);

			object lValue = lSelect.ReturnScalar(Instance.CreateDatabase(pInfo));

			return lValue.DBToDecimal();
		}


        /// <summary>
        /// Altera Session NLS_COMP e NLS_SORT, para iguinorar acentos na pesquisa
        /// </summary>
        /// <returns></returns>
        public static void AlterSessionBinary(ConnectionInfo pInfo)
        {
            SqlQuery lSqlQuery;
            int i;
            lSqlQuery = new SqlQuery(Instance.CreateDatabase(pInfo), "ALTER SESSION SET NLS_COMP=LINGUISTIC");
            i = lSqlQuery.ExecuteNonQuery();

            lSqlQuery = new SqlQuery(Instance.CreateDatabase(pInfo), "ALTER SESSION SET NLS_SORT=BINARY_AI");
            i = lSqlQuery.ExecuteNonQuery();
        }

		#endregion
	}
}
