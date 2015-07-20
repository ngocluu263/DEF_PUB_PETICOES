using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Oracle.DataAccess.Client;

namespace APB.Mercury.DataObjects.BanPeticoes
{
    [Serializable]
    public class OracleDo
    {
        public OracleDo()
        {
        }
    

        public DataTable Consulta
        (
            string pQuery,
            string pConnectionString
        )
        {
            DataTable lTable = new DataTable();
            DataSet lDataSet = new DataSet();
            OracleConnection lConnection = new OracleConnection(pConnectionString);

            try
            {
                lConnection.Open();

                OracleDataAdapter lDataAdapter = new OracleDataAdapter(pQuery, pConnectionString);

                lDataAdapter.Fill(lDataSet);

                lTable = lDataSet.Tables[0];

                return lTable;
            }
            finally
            {
                lConnection.Close();
            }
        }

        public void Exclui(string aCommand, string pConnectionString)
        {
            OracleConnection lConnection = new OracleConnection(pConnectionString);
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = aCommand;
                cmd.Connection = lConnection;

                lConnection.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                lConnection.Close();
            }

        }

        public void ExecutaResultadoConcurso(decimal id_concurso, string pConnectionString)
        {
            OracleConnection lConnection = new OracleConnection(pConnectionString);
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "stp_pr_gerar_resultado";
                cmd.Parameters.Add("id_concurso", id_concurso);
                cmd.Connection = lConnection;

                lConnection.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                lConnection.Close();
            }
        }
     
    }
}
