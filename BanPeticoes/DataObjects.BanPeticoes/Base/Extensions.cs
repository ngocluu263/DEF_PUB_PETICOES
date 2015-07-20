using System;
using System.Collections.Generic;
using System.Text;

using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace APB.Mercury.DataObjects.BanPeticoes
{
	public static class Extensions
	{
		public static decimal DBToDecimal(this object pObject)
		{
			if (pObject == null || pObject == DBNull.Value) return 0;

			decimal lReturn;

			if(Decimal.TryParse(pObject.ToString(), out lReturn))
			{
				return lReturn;
			}
			else
			{
				return 0;
			}
		}

		public static DateTime DBToDateTime(this object pObject)
		{
			if (pObject == null || pObject == DBNull.Value) return DateTime.MinValue;

			DateTime lReturn;

			if (DateTime.TryParse(pObject.ToString(), out lReturn))
			{
				return lReturn;
			}
			else
			{
				return DateTime.MinValue;
			}
		}

		public static string DBToString(this object pObject)
		{
			if (pObject == null || pObject == DBNull.Value) return "";

			return pObject.ToString();
		}

		public static bool IsDecimalNumeric(this string pString)
		{
			return Regex.IsMatch(pString, "^\\d+(\\.\\d+)?$");
		}

		public static bool IsIntegerNumeric(this string pString)
		{
			//return Regex.IsMatch(pString, "\\d.");
            return Regex.IsMatch(pString, "^[0-9]+$");
            
		}

        public static bool ItHasTwoDigits(this string pString)
        {
            if (pString.Length == 2) return Convert.ToBoolean("");
            return Convert.ToBoolean(pString.ToString());
        }


        //validar telefone
        public static bool ItHasDigits(this string pString, int pLen)
        {
            bool retorno;
            int result;

            if (pString.Length != pLen)
            {
                retorno = false;
            }
            else
            {
                if (int.TryParse(pString, out result))
                {
                    retorno = true;
                }
                else
                {
                    retorno = false;
                }
            };

            return retorno;
        }


        /// <summary>
        /// Este método tem o objetivo de receber os parâmetros pISS_ID, pCD_ID, pCRD_SNR
        /// do banco e fazer a concatenação que faz a montagem do número do cartão
        /// </summary>
        /// <param name="pISS_ID"></param>
        /// <param name="pCD_ID"></param>
        /// <param name="pCRD_SNR"></param>
        /// <returns></returns>
        public static string ConcatenateCrdNumber(this string pISS_ID, string pCD_ID, string pCRD_SNR)
        {
            string retorno;

            for (int i = 0; pCD_ID.Length < 2; i++)
            {
                pCD_ID = "0" + pCD_ID;
            }

            for (int i = 0; pCRD_SNR.Length < 8; i++)
            {
                pCRD_SNR = "0" + pCRD_SNR;
            }

            retorno = pISS_ID + pCD_ID + pCRD_SNR;

            return retorno;
        }
        
        //public static bool ItHasEightDigits(this string pString)
        //{
        //    return Regex.IsMatch(pString, "\\d{4}.d{4}");
        //}

        //Formata Inteiro

        public static string FormatarInteiro(string valor)
        {
            if (valor == null)
                return "";
            string ret, auxParteInteira = "", parteInteira = "", parteDecimal = "";
            int tamanho = valor.Length, virgula = valor.IndexOf(",");
            if (virgula == -1)
            {
                auxParteInteira = valor;
                parteDecimal = "00";
            }
            else
            {
                parteDecimal = valor.Substring(virgula + 1, tamanho - virgula - 1);
                if (parteDecimal.Length == 1) parteDecimal += "0";
                auxParteInteira = valor.Substring(0, virgula);
            }
            tamanho = auxParteInteira.Length;

            while (tamanho > 0)
            {
                tamanho = auxParteInteira.Length;
                if (tamanho > 3)
                {
                    parteInteira = "." + auxParteInteira.Substring(tamanho - 3, 3) + parteInteira;
                    auxParteInteira = auxParteInteira.Substring(0, tamanho - 3);
                }
                else
                {
                    parteInteira = auxParteInteira + parteInteira;
                    tamanho = 0;
                }
            }

            ret = parteInteira;
            if (ret == "&nb.sp;") ret = "0";
            return ret;
        }

        //Formata Data
        public static string FormatarData(string data)
        {
            return Convert.ToDateTime(data).ToShortDateString();
        }

        //Valida CNPJ
        public static bool ValidaCNPJ(string vrCNPJ)
        {

            string CNPJ = vrCNPJ.Replace(".", "");

            CNPJ = CNPJ.Replace("/", "");

            CNPJ = CNPJ.Replace("-", "");



            int[] digitos, soma, resultado;

            int nrDig;

            string ftmt;

            bool[] CNPJOk;



            ftmt = "6543298765432";

            digitos = new int[14];

            soma = new int[2];

            soma[0] = 0;

            soma[1] = 0;

            resultado = new int[2];

            resultado[0] = 0;

            resultado[1] = 0;

            CNPJOk = new bool[2];

            CNPJOk[0] = false;

            CNPJOk[1] = false;



            try
            {

                for (nrDig = 0; nrDig < 14; nrDig++)
                {

                    digitos[nrDig] = int.Parse(

                        CNPJ.Substring(nrDig, 1));

                    if (nrDig <= 11)

                        soma[0] += (digitos[nrDig] *

                          int.Parse(ftmt.Substring(

                          nrDig + 1, 1)));

                    if (nrDig <= 12)

                        soma[1] += (digitos[nrDig] *

                          int.Parse(ftmt.Substring(

                          nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {

                    resultado[nrDig] = (soma[nrDig] % 11);

                    if ((resultado[nrDig] == 0) || (

                         resultado[nrDig] == 1))

                        CNPJOk[nrDig] = (

                        digitos[12 + nrDig] == 0);

                    else

                        CNPJOk[nrDig] = (

                        digitos[12 + nrDig] == (

                        11 - resultado[nrDig]));

                }

                return (CNPJOk[0] && CNPJOk[1]);

            }

            catch
            {

                return false;

            }

        }

        //Valida CPF
        public static bool ValidaCPF(string vrCPF)
        {

            string valor = vrCPF.Replace(".", "");

            valor = valor.Replace("-", "");



            if (valor.Length != 11)

                return false;



            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)

                if (valor[i] != valor[0])

                    igual = false;



            if (igual || valor == "12345678909")

                return false;



            int[] numeros = new int[11];



            for (int i = 0; i < 11; i++)

                numeros[i] = int.Parse(

                  valor[i].ToString());



            int soma = 0;

            for (int i = 0; i < 9; i++)

                soma += (10 - i) * numeros[i];



            int resultado = soma % 11;



            if (resultado == 1 || resultado == 0)
            {

                if (numeros[9] != 0)

                    return false;

            }

            else if (numeros[9] != 11 - resultado)

                return false;



            soma = 0;

            for (int i = 0; i < 10; i++)

                soma += (11 - i) * numeros[i];

            resultado = soma % 11;


            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)

                    return false;
            }

            else

                if (numeros[10] != 11 - resultado)

                    return false;

            return true;

        }


        //Formata Moeda colocando o R$
        public static string FormatarMoeda(string valor, bool Real)
        {
            string ret, auxParteInteira = "", parteInteira = "", parteDecimal = "", sinal = "";

            if (valor == null)
                ret = "";
            else if (valor.Trim() == "&nbsp;")
                ret = "";
            else if (valor.Trim() == "")
                ret = "0.00";
            else
            {
                //Verificar se é negativo
                if (valor.Length > 1)
                {
                    sinal = valor.Substring(0, 1);
                    if (sinal != "-")
                        sinal = "";
                    else valor = valor.Substring(1, valor.Length - 1);
                }

                int tamanho = valor.Length, virgula = valor.IndexOf(",");
                if (virgula == 0)
                {
                    parteInteira = "0";
                }
                else if (virgula == -1)
                {
                    virgula = valor.IndexOf(".");
                }

                if (virgula == -1)
                {
                    auxParteInteira = valor;
                    parteDecimal = "00";
                }
                else
                {
                    parteDecimal = valor.Substring(virgula + 1, tamanho - virgula - 1);
                    if (parteDecimal.Length == 1) { parteDecimal += "0"; }
                    else parteDecimal = parteDecimal.Substring(0, 2);
                    auxParteInteira = valor.Substring(0, virgula);
                }
                tamanho = auxParteInteira.Length;

                while (tamanho > 0)
                {
                    tamanho = auxParteInteira.Length;
                    if (tamanho > 3)
                    {
                        parteInteira = "." + auxParteInteira.Substring(tamanho - 3, 3) + parteInteira;
                        auxParteInteira = auxParteInteira.Substring(0, tamanho - 3);
                    }
                    else
                    {
                        parteInteira = auxParteInteira + parteInteira;
                        tamanho = 0;
                    }
                }

                ret = sinal + parteInteira + "," + parteDecimal;
            }
            if (!Real) return ret;
            else return "R$ " + ret;
        }

        //Formata Moeda sem o R$
        public static string FormatarMoedaSemRS(string valor, bool Real)
        {
            string ret, auxParteInteira = "", parteInteira = "", parteDecimal = "", sinal = "";

            if (valor == null)
                ret = "";
            else if (valor.Trim() == "&nbsp;")
                ret = "";
            else if (valor.Trim() == "")
                ret = "0.00";
            else
            {
                //Verificar se é negativo
                if (valor.Length > 1)
                {
                    sinal = valor.Substring(0, 1);
                    if (sinal != "-")
                        sinal = "";
                    else valor = valor.Substring(1, valor.Length - 1);
                }

                int tamanho = valor.Length, virgula = valor.IndexOf(",");
                if (virgula == 0)
                {
                    parteInteira = "0";
                }
                else if (virgula == -1)
                {
                    virgula = valor.IndexOf(".");
                }

                if (virgula == -1)
                {
                    auxParteInteira = valor;
                    parteDecimal = "00";
                }
                else
                {
                    parteDecimal = valor.Substring(virgula + 1, tamanho - virgula - 1);
                    if (parteDecimal.Length == 1) { parteDecimal += "0"; }
                    else parteDecimal = parteDecimal.Substring(0, 2);
                    auxParteInteira = valor.Substring(0, virgula);
                }
                tamanho = auxParteInteira.Length;

                while (tamanho > 0)
                {
                    tamanho = auxParteInteira.Length;
                    if (tamanho > 3)
                    {
                        parteInteira = "." + auxParteInteira.Substring(tamanho - 3, 3) + parteInteira;
                        auxParteInteira = auxParteInteira.Substring(0, tamanho - 3);
                    }
                    else
                    {
                        parteInteira = auxParteInteira + parteInteira;
                        tamanho = 0;
                    }
                }

                ret = sinal + parteInteira + "," + parteDecimal;
            }
            if (!Real) return ret;
            else return ret;
        }
	}
}
