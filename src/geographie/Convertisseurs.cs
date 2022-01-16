//-----------------------------------------------------------------------
// <rights file="Convertisseurs.cs" author="Arnaud de LATOUR">
//     Propriété intellectuelle.
// </rights>
//-----------------------------------------------------------------------

namespace diplo.Geographie
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using diplo.Geographie.Enumerations;


    /// <summary>
    /// Différents convertisseurs depuis ou vers différentes énumérations.
    /// </summary>
    public static class Convertisseurs
    {
        #region Méthodes principales : couleurs

        /// <summary>
        /// Convertit l'élément de EBelligerant en la couleur associée.
        /// </summary>
        /// <param name="identificateurBelligerant">Identificateur du belligérant en tant qu'élément de EBelligerant.</param>
        /// <returns>Couleur associée au belligérant.</returns>
        public static Color VersCouleur(EBelligerant identificateurBelligerant)
        {
            Color couleur;
            switch (identificateurBelligerant)
            {
                case EBelligerant.Lautrec:
                    couleur = Color.Blue;
                    break;
                case EBelligerant.Barsanges:
                    couleur = Color.Red;
                    break;
                case EBelligerant.Empire:
                    couleur = Color.Black;
                    break;
                case EBelligerant.Mélinde:
                    couleur = Color.White;
                    break;
                case EBelligerant.Brémontrée:
                    couleur = Color.Brown;
                    break;
                case EBelligerant.Gretz:
                    couleur = Color.Yellow;
                    break;
                case EBelligerant.Palavin:
                    couleur = Color.White;
                    break;
                case EBelligerant.Thymée:
                    couleur = Color.Cyan;
                    break;
                case EBelligerant.Aucun:
                    goto default;
                default:
                    couleur = Color.Silver;
                    break;
            }

            return couleur;
        }

        #endregion

        #region Méthodes principales : phases de jeu

        /// <summary>
        /// Convertit l'élément de EPhaseJeu en une chaîne de caractères spécifiant le type de la phase.
        /// </summary>
        /// <param name="phaseJeu">Identificateur de la phase de jeu en tant qu'élément de EPhaseJeu.</param>
        /// <returns>Identificateur du type de la phase sous forme d'une chaîne de caractères.</returns>
        public static String VersPhaseJeuAbregee(EPhaseJeu phaseJeu)
        {
            String typePhase;
            switch (phaseJeu)
            {
                case EPhaseJeu.Printemps:
                    typePhase = "Ordres";
                    break;
                case EPhaseJeu.Ete:
                    goto case EPhaseJeu.Printemps;
                case EPhaseJeu.Automne:
                    goto case EPhaseJeu.Printemps;
                case EPhaseJeu.RetraitesPrintemps:
                    typePhase = "Retraites";
                    break;
                case EPhaseJeu.RetraitesEte:
                    goto case EPhaseJeu.RetraitesPrintemps;
                case EPhaseJeu.RetraitesAutomne:
                    goto case EPhaseJeu.RetraitesPrintemps;
                case EPhaseJeu.Ajustements:
                    typePhase = "Ajustements";
                    break;
                default:
                    throw new Exception("La phase de jeu fournie en argument est inconnue.");
            }

            return typePhase;
        }

        #endregion

        #region Méthodes principales : belligérants

        /// <summary>
        /// Convertit l'élément de EBelligerant en une chaîne de caractères abrégée.
        /// </summary>
        /// <param name="identificateurBelligerant">Identificateur du belligérant en tant qu'élément de EBelligerant.</param>
        /// <returns>Identificateur du belligérant sous forme d'une chaîne de caractères abrégée.</returns>
        public static String VersEBelligerantAbrege(EBelligerant identificateurBelligerant)
        {
            String identificateurAbrege;
            switch (identificateurBelligerant)
            {
                case EBelligerant.Lautrec:
                    identificateurAbrege = "Lau.";
                    break;
                case EBelligerant.Barsanges:
                    identificateurAbrege = "Bar.";
                    break;
                case EBelligerant.Empire:
                    identificateurAbrege = "Emp.";
                    break;
                case EBelligerant.Mélinde:
                    identificateurAbrege = "Mél.";
                    break;
                case EBelligerant.Brémontrée:
                    identificateurAbrege = "Bré.";
                    break;
                case EBelligerant.Gretz:
                    identificateurAbrege = "Gre.";
                    break;
                case EBelligerant.Palavin:
                    identificateurAbrege = "Pal.";
                    break;
                case EBelligerant.Thymée:
                    identificateurAbrege = "Thy.";
                    break;
                case EBelligerant.Aucun:
                    throw new Exception("Le convertisseur ne devrait pas être appelé dans un cas pareil.");
                default:
                    identificateurAbrege = "Bar.";
                    break;
            }

            return identificateurAbrege;
        }

        /// <summary>
        /// Convertit la chaîne de caractères abrégée en un élément de EBelligerant.
        /// </summary>
        /// <param name="identificateurAbrege">Identificateur du belligérant sous forme d'une chaîne de caractères.</param>
        /// <returns>Identificateur du belligérant en tant qu'élément de EBelligerant.</returns>
        public static EBelligerant DepuisEBelligerantAbrege(String identificateurAbrege)
        {
            EBelligerant identificateurBelligerant;
            switch (identificateurAbrege)
            {
                case "Lau.":
                    identificateurBelligerant = EBelligerant.Lautrec;
                    break;
                case "Bar.":
                    identificateurBelligerant = EBelligerant.Barsanges;
                    break;
                case "Emp.":
                    identificateurBelligerant = EBelligerant.Empire;
                    break;
                case "Mél.":
                    identificateurBelligerant = EBelligerant.Mélinde;
                    break;
                case "Bré.":
                    identificateurBelligerant = EBelligerant.Brémontrée;
                    break;
                case "Gre.":
                    identificateurBelligerant = EBelligerant.Gretz;
                    break;
                case "Pal.":
                    identificateurBelligerant = EBelligerant.Palavin;
                    break;
                case "Thy.":
                    identificateurBelligerant = EBelligerant.Thymée;
                    break;
                default:
                    throw new Exception("Le convertisseur ne reconnaît pas le nom du belligérant.");
            }

            return identificateurBelligerant;
        }

        /// <summary>
        /// Convertit la chaîne de caractères en un élément de EBelligerant.
        /// </summary>
        /// <param name="identificateurBelligerant">Identificateur du belligérant sous forme d'une chaîne de caractères.</param>
        /// <returns>Identificateur du belligérant en tant qu'élément de EBelligerant.</returns>
        public static EBelligerant VersEBelligerant(String identificateurBelligerant)
        {
            EBelligerant belligerant = (EBelligerant)(Enum.Parse(typeof(EBelligerant), identificateurBelligerant));
            return belligerant;
        }

        #endregion

        #region Méthodes principales : unités

        /// <summary>
        /// Convertit l'élément de EUnite en une chaîne de caractères abrégée.
        /// </summary>
        /// <param name="identificateurUnite">Identificateur de l'unité en tant qu'élément de EUnite.</param>
        /// <returns>Identificateur de l'unité sous forme d'une chaîne de caractères abrégée.</returns>
        public static String VersEUniteAbrege(EUnite identificateurUnite)
        {
            String identificateurAbrege;
            switch (identificateurUnite)
            {
                case EUnite.Flotte:
                    identificateurAbrege = "F";
                    break;
                case EUnite.Aucune:
                    throw new Exception("Le convertisseur ne devrait pas être appelé dans un cas pareil.");
                default:
                    identificateurAbrege = "A";
                    break;
            }

            return identificateurAbrege;
        }

        /// <summary>
        /// Convertit la chaîne de caractères abrégée en un élément de EUnite.
        /// </summary>
        /// <param name="identificateurUnite">Identificateur de l'unité sous forme d'une chaîne de caractères.</param>
        /// <returns>Identificateur de l'unité en tant qu'élément de EUnite.</returns>
        public static EUnite DepuisEUniteAbrege(String identificateurUnite)
        {
            EUnite unite;
            switch (identificateurUnite)
            {
                case "A":
                    unite = EUnite.Armée;
                    break;
                case "F":
                    unite = EUnite.Flotte;
                    break;
                default:
                    throw new Exception("L'iditenfiant d'unité ne correspond à aucune donnée.");
            }

            return unite;
        }

        /// <summary>
        /// Convertit la chaîne de caractères en un élément de EUnite.
        /// </summary>
        /// <param name="identificateurUnite">Identificateur de l'unité sous forme d'une chaîne de caractères.</param>
        /// <returns>Identificateur de l'unité en tant qu'élément de EUnite.</returns>
        public static EUnite VersEUnite(String identificateurUnite)
        {
            EUnite unite = (EUnite)(Enum.Parse(typeof(EUnite), identificateurUnite));
            return unite;
        }

        #endregion

        #region Méthodes publiques apparentées

        /// <summary>
        /// Classe les deux belligérants selon un ordre total.
        /// </summary>
        /// <param name="premierBelligerant">Identificateur d'un belligérant.</param>
        /// <param name="secondBelligerant">Identificateur d'un autre belligérant.</param>
        /// <returns>Valeur négative si premierBelligerant est avant secondBelligerant, et positive dans le cas opposé.</returns>
        public static Int32 ClasseEBelligerants(EBelligerant premierBelligerant, EBelligerant secondBelligerant)
        {
            Int32 entierPremierBelligerant = (Int32)(premierBelligerant);
            Int32 entierSecondBelligerant = (Int32)(secondBelligerant);

            return (entierPremierBelligerant - entierSecondBelligerant);
        }

        #endregion
    }
}