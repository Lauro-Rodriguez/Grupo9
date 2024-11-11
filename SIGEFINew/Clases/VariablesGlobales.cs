using System;
using System.Text.RegularExpressions;
namespace SIGEFINew.Clases
{
    class VariablesGlobales
    {
        public static int IdInstitucion;
        public static decimal TipoPresup;
        public static string NomInstitucion;
        public static string RUC;
        public static int IdPeriodo;
        public static int PeriodoCerrado;
        public static int Codigorol;
        public static int UsuaTipo;
        public static int CodArea;
        public static int CodDepto;
        public static int NumMaxNiv;
        public static string Anio;
        public static int Mes;
        public static string AppName = "SIGEFI";
        //public static AppMain mAppMain = new AppMain();
        public static NumLetra mNumLetra = new NumLetra();
        public static MesLetra mMesLetra = new MesLetra();
        public static string Usuario;
        public static string MsgDelete = "Este Registro se Eliminará Definitivamente... Desea Eliminarlo?";
        public static string MsgSuccess = "Los Datos se Guardaron Correctamente...!";
        public static string CadenaConex;
        public static string CorreoInstitucion;
        public static int IVAGasto;
        public static int IdBase;
        public static DataOracle.DataFacturacion DMClase = new DataOracle.DataFacturacion();
        //public static SIGEFIDataManagerMySQL.DataManager DMClaseMySQL = new SIGEFIDataManagerMySQL.DataManager();
        //public static ClsControl ClsControlData = new ClsControl();

    }

    class NumLetra
    {
        private String[] UNIDADES = { "", "un ", "dos ", "tres ", "cuatro ", "cinco ", "seis ", "siete ", "ocho ", "nueve " };
        private String[] DECENAS = {"diez ", "once ", "doce ", "trece ", "catorce ", "quince ", "dieciseis ",
        "diecisiete ", "dieciocho ", "diecinueve", "veinte ", "treinta ", "cuarenta ",
        "cincuenta ", "sesenta ", "setenta ", "ochenta ", "noventa "};
        private String[] CENTENAS = {"", "ciento ", "doscientos ", "trecientos ", "cuatrocientos ", "quinientos ", "seiscientos ",
        "setecientos ", "ochocientos ", "novecientos "};

        private Regex r;

        public String Convertir(String numero, bool mayusculas)
        {

            String literal = "";
            String parte_decimal;
            //si el numero utiliza (.) en lugar de (,) -> se reemplaza
            numero = numero.Replace(".", ",");

            //si el numero no tiene parte decimal, se le agrega ,00
            if (numero.IndexOf(",") == -1)
            {
                numero = numero + ",00";
            }
            //se valida formato de entrada -> 0,00 y 999 999 999,00
            r = new Regex(@"\d{1,9},\d{1,2}");
            MatchCollection mc = r.Matches(numero);
            if (mc.Count > 0)
            {
                //se divide el numero 0000000,00 -> entero y decimal
                String[] Num = numero.Split(',');

                //de da formato al numero decimal
                parte_decimal = Num[1] + "/100";
                //se convierte el numero a literal
                if (int.Parse(Num[0]) == 0)
                {//si el valor es cero                
                    literal = "cero ";
                }
                else if (int.Parse(Num[0]) > 999999)
                {//si es millon
                    literal = getMillones(Num[0]);
                }
                else if (int.Parse(Num[0]) > 999)
                {//si es miles
                    literal = getMiles(Num[0]);
                }
                else if (int.Parse(Num[0]) > 99)
                {//si es centena
                    literal = getCentenas(Num[0]);
                }
                else if (int.Parse(Num[0]) > 9)
                {//si es decena
                    literal = getDecenas(Num[0]);
                }
                else
                {//sino unidades -> 9
                    literal = getUnidades(Num[0]);
                }
                //devuelve el resultado en mayusculas o minusculas
                if (mayusculas)
                {
                    return (literal.ToUpper() + "Dolares con " + parte_decimal.ToUpper() + " Ctvs");
                }
                else
                {
                    return (literal + "dolares con " + parte_decimal + " Ctvs");
                }
            }
            else
            {//error, no se puede convertir
                return literal = null;
            }
        }

        /* funciones para convertir los numeros a literales */

        private String getUnidades(String numero)
        {   // 1 - 9            
            //si tuviera algun 0 antes se lo quita -> 09 = 9 o 009=9
            String num = numero.Substring(numero.Length - 1);
            return UNIDADES[int.Parse(num)];
        }

        private String getDecenas(String num)
        {// 99                        
            int n = int.Parse(num);
            if (n < 10)
            {//para casos como -> 01 - 09
                return getUnidades(num);
            }
            else if (n > 19)
            {//para 20...99
                String u = getUnidades(num);
                if (u.Equals(""))
                { //para 20,30,40,50,60,70,80,90
                    return DECENAS[int.Parse(num.Substring(0, 1)) + 8];
                }
                else
                {
                    return DECENAS[int.Parse(num.Substring(0, 1)) + 8] + "y " + u;
                }
            }
            else
            {//numeros entre 11 y 19
                return DECENAS[n - 10];
            }
        }

        private String getCentenas(String num)
        {// 999 o 099
            if (int.Parse(num) > 99)
            {//es centena
                if (int.Parse(num) == 100)
                {//caso especial
                    return " cien ";
                }
                else
                {
                    return CENTENAS[int.Parse(num.Substring(0, 1))] + getDecenas(num.Substring(1));
                }
            }
            else
            {//por Ej. 099 
             //se quita el 0 antes de convertir a decenas
                return getDecenas(int.Parse(num) + "");
            }
        }

        private String getMiles(String numero)
        {// 999 999
         //obtiene las centenas
            String c = numero.Substring(numero.Length - 3);
            //obtiene los miles
            String m = numero.Substring(0, numero.Length - 3);
            String n = "";
            //se comprueba que miles tenga valor entero
            if (int.Parse(m) > 0)
            {
                n = getCentenas(m);
                return n + "mil " + getCentenas(c);
            }
            else
            {
                return "" + getCentenas(c);
            }

        }

        private String getMillones(String numero)
        { //000 000 000        
          //se obtiene los miles
            String miles = numero.Substring(numero.Length - 6);
            //se obtiene los millones
            String millon = numero.Substring(0, numero.Length - 6);
            String n = "";
            if (millon.Length > 1)
            {
                n = getCentenas(millon) + "millones ";
            }
            else
            {
                n = getUnidades(millon) + "millon ";
            }
            return n + getMiles(miles);
        }
    }

    class MesLetra
    {
        public string GetMesLetra(int Mes)
        {
            string MesLetra = "";
            switch (Mes)
            {
                case 1:
                    MesLetra = "Enero";
                    break;
                case 2:
                    MesLetra = "Febrero";
                    break;
                case 3:
                    MesLetra = "Marzo";
                    break;
                case 4:
                    MesLetra = "Abril";
                    break;
                case 5:
                    MesLetra = "Mayo";
                    break;
                case 6:
                    MesLetra = "Junio";
                    break;
                case 7:
                    MesLetra = "Julio";
                    break;
                case 8:
                    MesLetra = "Agosto";
                    break;
                case 9:
                    MesLetra = "Septiembre";
                    break;
                case 10:
                    MesLetra = "Octubre";
                    break;
                case 11:
                    MesLetra = "Noviembre";
                    break;
                case 12:
                    MesLetra = "Diciembre";
                    break;
            }
            return MesLetra;
        }
    }
}
