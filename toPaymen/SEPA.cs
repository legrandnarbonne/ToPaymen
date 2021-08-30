/*
 * Created by SharpDevelop.
 * User: DAPOJERO
 * Date: 06/01/2014
 * Time: 13:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using static toPaymen.Tools;

namespace ToSepa
{
    /// <summary>
    /// Description of SEPA.
    /// </summary>
    public static class SEPA
    {
        public static ArrayList getArray()
        {
            ArrayList Ar = new ArrayList();

            Ar.Add(new objectTxt("Dépatement", 1, 3, true));//011
            Ar.Add(new objectTxt("Collectivité", 4, 7, true));//N107
            Ar.Add(new objectTxt("Num train de vir.", 8, 10, true));//000
            Ar.Add(new objectTxt("Code nature", 11, 13, true));//AID
            Ar.Add(new objectTxt("Matricule", 14, 28, true));
            Ar.Add(new objectTxt("Monnaie", 29, 29, true));//E
            Ar.Add(new objectTxt("Montant", 30, 45, false));
            Ar.Add(new objectTxt("Exercice", 46, 49, true));//2021
            Ar.Add(new objectTxt("Bordereau", 50, 56, true));//vide
            Ar.Add(new objectTxt("Mandat", 57, 64, true));//vide
            Ar.Add(new objectTxt("Domiciliation", 65, 88, true));//non banque
            Ar.Add(new objectTxt("Banque (BIC)", 89, 99, true));
            Ar.Add(new objectTxt("IBAN", 100, 133, true));
            Ar.Add(new objectTxt("Nom", 134, 203, true));
            Ar.Add(new objectTxt("Adresse", 204, 343, true));//vide
            Ar.Add(new objectTxt("Pays", 344, 345, true));//FR
            Ar.Add(new objectTxt("?", 346, 355, true));//FR
            Ar.Add(new objectTxt("Identification", 356, 380, true));//vide
            Ar.Add(new objectTxt("Bénéficiaire", 381, 450, true));//nom
            Ar.Add(new objectTxt("Adresse Bénéf.", 451, 590, true));//vide
            Ar.Add(new objectTxt("Pays Bénéf.", 591, 592, true));//FR
            Ar.Add(new objectTxt("id Bénéf.", 593, 627, true));//vide
            Ar.Add(new objectTxt("Lib. Op.", 628, 767, true));//Remboursement piscine Fleury
            Ar.Add(new objectTxt("Lib. 2 Op.", 768, 793, true));//vide
            Ar.Add(new objectTxt("Filler vide", 794, 993, true));//vide    

            return Ar;
        }

        public static ArrayList setSEPAfixe(ArrayList Ar)
        {
            ((objectTxt)Ar[0]).valeur = "011";
            ((objectTxt)Ar[1]).valeur = "N107";
            ((objectTxt)Ar[2]).valeur = "000";
            ((objectTxt)Ar[3]).valeur = "AID";
            ((objectTxt)Ar[5]).valeur = "E";
            ((objectTxt)Ar[7]).valeur = "2021";
            ((objectTxt)Ar[15]).valeur = "FR";
            ((objectTxt)Ar[20]).valeur = "FR";
            ((objectTxt)Ar[22]).valeur = "Remboursement piscine de Fleury";

            return Ar;
        }
    }
}
