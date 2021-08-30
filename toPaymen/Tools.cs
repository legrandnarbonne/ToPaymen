using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace toPaymen
{
    class Tools
    {
        public class objectTxt
        {
            public string nom;
            public string valeur = "";
            public int debut;
            public int fin;
            public bool chaine;

            public objectTxt(string Nom, int Debut, int Fin, bool Chaine) : base()
            {
                nom = Nom;
                debut = Debut;
                fin = Fin;
                chaine = Chaine;
            }

        }
        /// <summary>
        /// écriture du fichier RCT
        /// </summary>
        /// <param name="Ar"></param>
        public static void toDest(ArrayList Ar, StreamWriter sw)
        {
            for (int a = 1; a <= Ar.Count; a++)//passe tout les champs pour l'individu
            {
                objectTxt ot = ((objectTxt)Ar[a - 1]);
                sw.Write(formatString(ot.valeur, ot.fin - ot.debut + 1, ot.chaine));
            }
            sw.Write('\x0D');
            sw.Write('\x0A');
        }

        static string toCentime(string s)
        {
            float f = float.Parse(s.Replace('.', ','));
            f = f * 100;
            return f.ToString();
        }
        /// <summary>
        /// retaille la chaine pour recalibrer la longuer
        /// </summary>
        /// <param name="str">chaine à retailler</param>
        /// <param name="longueur">longueur à obtenir</param>
        /// <param name="chaine">type de valeur</param>
        /// <returns></returns>
        static string formatString(string str, int longueur, bool chaine)
        {
            int manque = longueur - str.Length;
            if (chaine)
            {
                if (str.Length > longueur) str = str.Substring(0, longueur);
                else
                {
                    for (int a = 1; a <= manque; a++)
                    {
                        str = str + " ";
                    }
                }
            }
            else
            {
                if (str.Length > longueur) str = str.Substring(0, longueur);
                else
                {
                    for (int a = 1; a <= manque; a++)
                    {
                        str = "0" + str;
                    }
                }
            }
            return str;
        }
        /// <summary>
        /// place les valeurs du doc excel dans l'Ar pour l'individu en cours
        /// </summary>
        /// <param name="Ar"></param>
        public static bool dbToArray(ArrayList Ar, int ligne, DataTable dt)
        {
            foreach (DataColumn dtcol in dt.Columns)
            {
                var o = trouveObject(dtcol.ColumnName, Ar);
                var value = dt.Rows[ligne - 2][dtcol.ColumnName].ToString();
                o.valeur = RemoveUTF8.RemoveDiacritics(value);

                if (dtcol.ColumnName == "Montant")
                    o.valeur = toCentime(value);

                if (dtcol.ColumnName == "IBAN")
                    o.valeur = value.Replace(" ", "");
            }
            return ligne > dt.Rows.Count;
        }
        /// <summary>
        /// retourne l'objet de l'Array du nom demandé
        /// </summary>
        /// <param name="str">nom recherché</param>
        /// <param name="Ar">Array</param>
        public static objectTxt trouveObject(string nom, ArrayList Ar)
        {
            for (int a = 1; a <= Ar.Count; a++)
            {
                objectTxt ot = ((objectTxt)Ar[a - 1]);
                if (ot.nom == nom)
                {
                    return ot;
                }
            }
            return null;
        }

        public static class RemoveUTF8
        {
            public static string RemoveDiacritics(string strMessage)
            {
                strMessage = Regex.Replace(strMessage, "[éèëêð]", "e");
                strMessage = Regex.Replace(strMessage, "[ÉÈËÊ]", "E");
                strMessage = Regex.Replace(strMessage, "[àâä]", "a");
                strMessage = Regex.Replace(strMessage, "[ÀÁÂÃÄÅ]", "A");
                strMessage = Regex.Replace(strMessage, "[àáâãäå]", "a");
                strMessage = Regex.Replace(strMessage, "[ÙÚÛÜ]", "U");
                strMessage = Regex.Replace(strMessage, "[ùúûüµ]", "u");
                strMessage = Regex.Replace(strMessage, "[òóôõöø]", "o");
                strMessage = Regex.Replace(strMessage, "[ÒÓÔÕÖØ]", "O");
                strMessage = Regex.Replace(strMessage, "[ìíîï]", "i");
                strMessage = Regex.Replace(strMessage, "[ÌÍÎÏ]", "I");
                strMessage = Regex.Replace(strMessage, "[š]", "s");
                strMessage = Regex.Replace(strMessage, "[Š]", "S");
                strMessage = Regex.Replace(strMessage, "[ñ]", "n");
                strMessage = Regex.Replace(strMessage, "[Ñ]", "N");
                strMessage = Regex.Replace(strMessage, "[ç]", "c");
                strMessage = Regex.Replace(strMessage, "[Ç]", "C");
                strMessage = Regex.Replace(strMessage, "[ÿ]", "y");
                strMessage = Regex.Replace(strMessage, "[Ÿ]", "Y");
                strMessage = Regex.Replace(strMessage, "[ž]", "z");
                strMessage = Regex.Replace(strMessage, "[Ž]", "Z");
                strMessage = Regex.Replace(strMessage, "[Ð]", "D");
                strMessage = Regex.Replace(strMessage, "[œ]", "oe");
                strMessage = Regex.Replace(strMessage, "[Œ]", "Oe");
                strMessage = Regex.Replace(strMessage, "[«»\u201C\u201D\u201E\u201F\u2033\u2036]", "\"");
                return Regex.Replace(strMessage, "[\u2026]", "...").ToUpper();
            }
        }
    }
}
