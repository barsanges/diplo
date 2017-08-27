//-----------------------------------------------------------------------
// <rights file="DictionnaireAdjacence.cs" author="Arnaud de LATOUR">
//     Propriété intellectuelle.
// </rights>
//-----------------------------------------------------------------------

namespace diplo.Geographie
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using diplo.Geographie.Enumerations;

    /// <summary>
    /// Matrice d'adjacence entre les différentes régions, triée par noms.
    /// Attention ! Cette classe ne doit être initialisée qu'une fois par utilisation du programme et par partie.
    /// </summary>
    public class DictionnaireAdjacence
    {
        #region Champs

        /// <summary>
        /// Liste complète des noms des régions.
        /// </summary>
        private List<String> listeNomsRegions;

        /// <summary>
        /// Matrice d'adjacence : indique si deux régions sont adjacentes.
        /// </summary>
        private EAdjacence[,] matriceAdjacence;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe DictionnaireAdjacence.
        /// Attention ! Cette classe ne doit être initialisée qu'une fois par utilisation du programme et par partie.
        /// </summary>
        /// <param name="fichierACharger">Fichier de données à charger.</param>
        public DictionnaireAdjacence(String fichierACharger)
        {
            using (StreamReader lecteur = new StreamReader(fichierACharger))
            {
                String[] nomsRegions = lecteur.ReadLine().Split(';');
                this.listeNomsRegions = new List<string>();
                this.matriceAdjacence = new EAdjacence[nomsRegions.Length, nomsRegions.Length];
                for (Int32 k = 1; k <= nomsRegions.Length; k++)
                {
                    this.listeNomsRegions.Add(nomsRegions[k - 1]);

                    String[] donnees = lecteur.ReadLine().Split(';');
                    for (Int32 p = 1; p <= nomsRegions.Length; p++)
                    {
                        // Avec cette manière de faire, on écrit deux fois chaque champ...
                        // C'est un peu superfétatoire :).
                        this.matriceAdjacence[k - 1, p - 1] = (EAdjacence)(Int32.Parse(donnees[p]));
                        this.matriceAdjacence[p - 1, k - 1] = (EAdjacence)(Int32.Parse(donnees[p]));
                    }
                }
            }
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Liste complète des noms des régions.
        /// </summary>
        public List<String> ListeNomsRegions { get { return this.listeNomsRegions; } }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Détermine si deux régions sont adjacentes.
        /// </summary>
        /// <param name="region1">Nom de la première région.</param>
        /// <param name="region2">Nom de la second région.</param>
        /// <returns>Vrai si les deux régions sont adjacentes, faux sinon.</returns>
        public EAdjacence DetermineAdjacence(String region1, String region2)
        {
            Int32 indexRegion1 = this.listeNomsRegions.FindIndex(item => item == region1);
            Int32 indexRegion2 = this.listeNomsRegions.FindIndex(item => item == region2);

            return this.matriceAdjacence[indexRegion1, indexRegion2];
        }

        /// <summary>
        /// Détermine si une armée peut être convoyée entre deux régions côtières via un ensemble de régions maritimes spécifiées.
        /// </summary>
        /// <param name="regionDepart">Nom de la région d'où part l'unité convoyée.</param>
        /// <param name="regionArrivee">Nom de la région où arrive l'unité convoyée.</param>
        /// <param name="regionsMaritimes">Liste des noms des régions maritimes participant au convoyage.</param>
        /// <returns>
        /// Vrai si une armée peut être convoyée de regionDepart à regionArrivee via les régions maritimes
        /// données, faux sinon.
        /// </returns>
        public Boolean DetermineConvoi(String regionDepart, String regionArrivee, List<String> regionsMaritimes)
        {
            try
            {
                List<String> listeRegions = new List<string>(regionsMaritimes);

                String derniereRegionUtilisee = listeRegions.Find(item =>
                    this.DetermineAdjacence(item, regionDepart) == EAdjacence.AdjacentMer);
                listeRegions.Remove(derniereRegionUtilisee);

                while ((this.DetermineAdjacence(derniereRegionUtilisee, regionArrivee) != EAdjacence.AdjacentMer)
                    && (listeRegions.Count > 0))
                {
                    derniereRegionUtilisee = listeRegions.Find(item => ((item != derniereRegionUtilisee)
                        && (this.DetermineAdjacence(item, derniereRegionUtilisee) == EAdjacence.AdjacentMer)));
                    listeRegions.Remove(derniereRegionUtilisee);
                }

                if (this.DetermineAdjacence(derniereRegionUtilisee, regionArrivee) == EAdjacence.AdjacentMer)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                //throw new Exception(String.Format(
                //    "La succession de régions maritimes proposées pour le convoi de {0} à {1} n'est pas valide.",
                //    regionDepart,
                //    regionArrivee));
                return false;
            }
        }

        #endregion
    }
}