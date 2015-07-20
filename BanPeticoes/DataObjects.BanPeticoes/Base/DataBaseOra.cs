using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Text;

namespace APB.Mercury.DataObjects.BanPeticoes
{
    class DataBaseOra
    {
        public OracleConnection conn;
        string conexao = " Data Source=desenv2; User ID=desenv; Password=desenv ";
        //string conexao = "Provider=SQLOLEDB.1;Password=milao;Persist Security Info=True;User ID=sa;Initial Catalog=db_Merlin; Data Source=192.168.1.7 ";
        public OracleCommand cmd;
        public OracleCommand QOperacao;
        public OracleDataReader dr;
        public string sql;

        public string conectar()
        {
            try
            {
                conn = new OracleConnection(conexao);
                conn.Open();
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public void desconectar()
        {
            conn.Close();
            conn = null;
        }


        public string ExecutaComando(OracleCommand Comando)
        {
            conectar();
            Comando.Connection = conn;

            try
            {
                Comando.ExecuteNonQuery();
                desconectar();
                return "";
            }
            catch (Exception e)
            {
                desconectar();
                return e.Message;
            }
        }

        public DataSet ExecutaDataSet(OracleCommand Comando)
        {
            DataSet ds = new DataSet();
            OracleDataAdapter da;

            conectar();
            Comando.Connection = conn;

            try
            {
                da = new OracleDataAdapter("", conn);
                da.SelectCommand = Comando;
                da.SelectCommand.CommandTimeout = 0;
                da.Fill(ds, "consulta");
                desconectar();
                return ds;
            }
            catch
            {
                desconectar();
                return ds;
            }
        }
    }
}
