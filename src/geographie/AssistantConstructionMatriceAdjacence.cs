//-----------------------------------------------------------------------
// <rights file="Region.cs" author="Arnaud de LATOUR">
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

    /// <summary>
    /// Assistant pour la construction de matrice d'adjacence : transforme des fichiers parcimonieux (pour chaque région, on
    /// indique juste les régions adjacentes) en fichier complet (pour chaque région, on précise le type d'ajdacence vis-à-vis
    /// de toutes les autres). Classe annexe, pour développement uniquement.
    /// </summary>
    static class AssistantConstructionMatriceAdjacence
    {
        #region Méthode principale

        /// <summary>
        /// Crée un fichier complet à partir d'un fichier parcimonieux.
        /// </summary>
        /// <param name="fichierDeBase">Chemin d'accès au fichier parcimonieux à charger.</param>
        public static void RemplitNouveauFichier(String fichierDeBase)
        {
            List<String> nomsRegions;
            using (StreamReader lecteur = new StreamReader(fichierDeBase))
            {
                nomsRegions = lecteur.ReadLine().Split(';').ToList();
            }

            String nouveauFichier = fichierDeBase.Replace(".txt", " complété.bdat");

            Int32[,] matriceComplète = LitFichierBase(fichierDeBase);
            ExporteNouveauFichier(nouveauFichier, nomsRegions, matriceComplète);
        }

        #endregion

        #region Sous-méthodes

        /// <summary>
        /// Lit un fichier d'adjacence sous forme parcimonieuse et crée une matrice complète.
        /// </summary>
        /// <param name="fichierDeBase">Chemin d'accès au fichier parcimonieux à charger.</param>
        /// <returns>Matrice d'ajdacence complète.</returns>
        private static Int32[,] LitFichierBase(String fichierDeBase)
        {
            Int32[,] matriceARetourner;
            using (StreamReader lecteur = new StreamReader(fichierDeBase))
            {
                List<String> nomsRegions = lecteur.ReadLine().Split(';').ToList();
                matriceARetourner = new Int32[nomsRegions.Count, nomsRegions.Count];

                Int32 indexRegion = 0;
                foreach (String nom in nomsRegions)
                {
                    String[] donnees = lecteur.ReadLine().Split(';');
                    for (Int32 p = 1; p < donnees.Length; p++)
                    {
                        String nomAutreRegion = (donnees[p].Split(':'))[0];
                        Int32 indexAutreRegion = nomsRegions.IndexOf(nomAutreRegion);

                        Int32 typeAdjacence = Int32.Parse((donnees[p].Split(':'))[1]);

                        matriceARetourner[indexRegion, indexAutreRegion] = typeAdjacence;
                    }

                    indexRegion++;
                }
            }

            return matriceARetourner;
        }

        /// <summary>
        /// Exporte la matrice complète dans un fichier au format .bdat.
        /// </summary>
        /// <param name="fichierExport">Nom du fichier (à créer) contenant la matrice complète.</param>
        /// <param name="nomsRegions">Liste des noms des régions de la carte.</param>
        /// <param name="matriceAEcrire">Matrice complète à exporter.</param>
        private static void ExporteNouveauFichier(String fichierExport, List<String> nomsRegions, Int32[,] matriceAEcrire)
        {
            using (StreamWriter redacteur = new StreamWriter(fichierExport))
            {
                foreach (String region in nomsRegions)
                {
                    if (region != nomsRegions.Last())
                    {
                        redacteur.Write("{0};", region);
                    }
                }

                redacteur.WriteLine(nomsRegions.Last());

                Int32 indexRegion = 0;
                foreach (String region in nomsRegions)
                {
                    redacteur.Write(region);
                    for (Int32 k = 0; k < nomsRegions.Count; k++)
                    {
                        redacteur.Write(";{0}", matriceAEcrire[indexRegion, k]);
                    }

                    redacteur.WriteLine();
                    indexRegion++;
                }
            }
        }

        #endregion
    }
}
