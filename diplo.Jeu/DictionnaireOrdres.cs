//-----------------------------------------------------------------------
// <rights file="DictionnaireOrdres.cs" author="Arnaud de LATOUR">
//     Propriété intellectuelle.
// </rights>
//-----------------------------------------------------------------------

namespace diplo.Jeu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using diplo.Geographie;
    using diplo.Geographie.Enumerations;
    using diplo.Jeu.Ordres;

    /// <summary>
    /// Dictionnaire d'ordres utilisé pour résoudre un tour de jeu (hors retraites et recrutements).
    /// </summary>
    public class DictionnaireOrdres
    {
        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe DictionnaireOrdres.
        /// </summary>
        /// <param name="listeNomsRegions">Liste des noms des régions de la carte utilisée.</param>
        public DictionnaireOrdres(List<String> listeNomsRegions)
        {
            this.Dictionnaire = new Dictionary<string, OrdreRegional>();
            foreach (var region in listeNomsRegions)
            {
                this.Dictionnaire.Add(region, new OrdreRegional(region));
            }
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Dictionnaire (effectif) des ordres.
        /// </summary>
        public Dictionary<String, OrdreRegional> Dictionnaire { get; private set; }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Enregistre les ordres passés pour un nouveau tour.
        /// </summary>
        /// <param name="ordres">Liste des ordres donnés par les belligérants.</param>
        /// <param name="dictionnaireOccupation">Dictionnaire d'occupation des régions.</param>
        public void EnregistreOrdres(List<OrdreAbstrait> ordres, Dictionary<String, OccupationRegion> dictionnaireOccupation)
        {
            // Normalement, tout ce qui arrive à ce niveau là est d'ores et déjà valide.
            this.Reinitialise(dictionnaireOccupation);
            foreach (var ordre in ordres)
            {
                OrdreRegional ordreRegional = this.Dictionnaire[ordre.Region];
                if (ordre.GetType() == typeof(Attaquer))
                {
                    ordreRegional.EnregistreAttaque(ordre as Attaquer);
                }
                else if (ordre.GetType() == typeof(Convoyer))
                {
                    ordreRegional.EnregistreConvoi(ordre as Convoyer);
                }
                else if (ordre.GetType() == typeof(SoutenirDefensif))
                {
                    ordreRegional.EnregistreSoutienDefensif(ordre as SoutenirDefensif);
                }
                else if (ordre.GetType() == typeof(SoutenirOffensif))
                {
                    ordreRegional.EnregistreSoutienOffensif(ordre as SoutenirOffensif);
                }
                else
                {
                    ordreRegional.EnregistreTenir(ordre as Tenir);
                }
            }
        }

        /// <summary>
        /// Applique les ordres passés pour un nouveau tour.
        /// </summary>
        /// <param name="ordres">Liste des ordres donnés par les belligérants.</param>
        public void AppliqueOrdres(List<OrdreAbstrait> ordres)
        {
            /// L'idée est de faire ainsi :
            /// 0. Tous les ordres non-valides ont *déjà* été passés en "Tenir".
            /// 1. On applique tous les soutiens.
            /// 2. À l'aide des attaques, on coupe tous les soutiens qui doivent l'être.
            /// 3. On détermine si des convois sont coupés.
            /// 4. Si des convois sont coupés, on rétablit les soutiens préalablement coupés...
            /// 5. On résout les attaques.
            /// 6. On détermine les retraites.

            // 1. On applique tous les soutiens.
            foreach (var ordre in ordres)
            {
                if (ordre.GetType() == typeof(SoutenirDefensif))
                {
                    this.AppliqueSoutienDefensif(ordre as SoutenirDefensif);
                }
                else if (ordre.GetType() == typeof(SoutenirOffensif))
                {
                    this.AppliqueSoutienOffensif(ordre as SoutenirOffensif);
                }
            }

            // 2. À l'aide des attaques, on coupe tous les soutiens qui doivent l'être.
            foreach (var ordre in ordres)
            {
                if (ordre.GetType() == typeof(Attaquer))
                {
                    this.CoupeSoutien(ordre as Attaquer);
                }
            }

            // 3. On détermine si des convois sont coupés.
            foreach (var ordre in ordres)
            {
                if (ordre.GetType() == typeof(Convoyer))
                {
                    this.CoupeConvoi(ordre as Convoyer);
                }
            }

            // 4. Si des convois sont coupés, on rétablit les soutiens préalablement coupés...
            foreach (var ordre in ordres)
            {
                if (ordre.GetType() == typeof(Convoyer))
                {
                    this.RetablitSoutien(ordre as Convoyer);
                }
            }

            // 5. On résout les attaques.
            foreach (var ordre in ordres)
            {
                if (ordre.GetType() == typeof(Attaquer))
                {
                    this.AppliqueAttaque(ordre as Attaquer);
                }
            }

            // 6. On détermine les retraites.
            foreach (var ordre in ordres)
            {
                if (ordre.GetType() == typeof(Attaquer))
                {
                    this.MarqueRetraite(ordre as Attaquer);
                }
            }
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Réinitialise les champs du dictionnaire qui nécessitent d'être réinitialisés.
        /// </summary>
        /// <param name="dictionnaireOccupation">Dictionnaire d'occupation des régions.</param>
        private void Reinitialise(Dictionary<String, OccupationRegion> dictionnaireOccupation)
        {
            foreach (String region in this.Dictionnaire.Keys)
            {
                OccupationRegion occupation = dictionnaireOccupation[region];
                this.Dictionnaire[region].Reinitialise(occupation);
            }
        }

        /// <summary>
        /// Applique un ordre de soutien défensif.
        /// </summary>
        /// <param name="ordre">Ordre à appliquer.</param>
        private void AppliqueSoutienDefensif(SoutenirDefensif ordre)
        {
            OrdreRegional ordreSoutenu = this.Dictionnaire[ordre.RegionSoutenue];
            if (ordreSoutenu.TypeOrdre != EOrdre.Attaque)
            {
                ordreSoutenu.ForceDefensive++;
            }
        }

        /// <summary>
        /// Applique un ordre de soutien offensif.
        /// </summary>
        /// <param name="ordre">Ordre à appliquer.</param>
        private void AppliqueSoutienOffensif(SoutenirOffensif ordre)
        {
            OrdreRegional ordreSoutenu = this.Dictionnaire[ordre.RegionSoutenue];
            if (ordreSoutenu.TypeOrdre == EOrdre.Attaque && ordreSoutenu.RegionCiblee == ordre.RegionAttaquee)
            {
                ordreSoutenu.ForceOffensive++;
            }
        }

        /// <summary>
        /// Coupe un soutien à l'aide de l'ordre d'attaque donné.
        /// </summary>
        /// <param name="ordre">Ordre à appliquer.</param>
        private void CoupeSoutien(Attaquer ordre)
        {
            OrdreRegional ordreRegional = this.Dictionnaire[ordre.Region];
            OrdreRegional ordreRegionalCoupe = this.Dictionnaire[ordre.RegionAttaquee];

            if (ordreRegionalCoupe.TypeOrdre != EOrdre.Attaque && ordreRegionalCoupe.TypeOrdre != EOrdre.Tenir)
            {
                OrdreRegional ordreRegionalSoutenu = this.Dictionnaire[ordreRegionalCoupe.RegionCiblee];
                if (ordreRegionalCoupe.TypeOrdre == EOrdre.SoutienDefensif)
                {
                    ordreRegionalCoupe.TypeOrdre = EOrdre.SoutienDefensifCoupe;
                    this.Dictionnaire[ordreRegionalCoupe.RegionCiblee].ForceDefensive--;
                }
                else if (ordreRegionalCoupe.TypeOrdre == EOrdre.SoutienOffensif
                    && ordreRegionalSoutenu.RegionCiblee != ordreRegional.Region)
                {
                    ordreRegionalCoupe.TypeOrdre = EOrdre.SoutienOffensifCoupe;
                    this.Dictionnaire[ordreRegionalCoupe.RegionCiblee].ForceOffensive--;
                }
                else
                {
                    // Rien à faire.
                }
            }
        }

        /// <summary>
        /// Coupe le convoi correspondant à l'ordre donné.
        /// </summary>
        /// <param name="ordre">Ordre à appliquer.</param>
        private void CoupeConvoi(Convoyer ordre)
        {
            OrdreRegional ordreRegional = this.Dictionnaire[ordre.Region];
            if (ordreRegional.ForceDefensive < 0)
            {
                ordreRegional.TypeOrdre = EOrdre.ConvoiCoupe;
            }
        }

        /// <summary>
        /// Rétablit le soutien coupé par un convoi lui-même coupé.
        /// </summary>
        /// <param name="ordre">Ordre à appliquer.</param>
        private void RetablitSoutien(Convoyer ordre)
        {
            OrdreRegional ordreRegional = this.Dictionnaire[ordre.Region];
            if (ordreRegional.TypeOrdre == EOrdre.ConvoiCoupe)
            {
                OrdreRegional ordreSoutenu = this.Dictionnaire[ordre.RegionSoutenue];

                this.Dictionnaire[ordre.RegionAttaquee].ForceDefensive += ordreSoutenu.ForceOffensive;
                ordreSoutenu.ForceOffensive = 0;
            }
        }

        /// <summary>
        /// Applique un ordre d'attaque.
        /// </summary>
        /// <param name="ordre">Ordre à appliquer.</param>
        private void AppliqueAttaque(Attaquer ordre)
        {
            OrdreRegional ordreRegional = this.Dictionnaire[ordre.Region];
            OrdreRegional ordreRegionalCible = this.Dictionnaire[ordre.RegionAttaquee];
            if (((ordreRegional.ForceOffensive > ordreRegionalCible.ForceDefensive)
                && (ordreRegional.Possesseur != ordreRegionalCible.Possesseur))
                || (ordreRegionalCible.TypeOrdre == EOrdre.AttaqueReussie))
            {
                ordreRegional.TypeOrdre = EOrdre.AttaqueReussie;
                foreach (OrdreRegional ordreRegionalConcurrent in this.Dictionnaire.Values)
                {
                    if ((ordreRegionalConcurrent.RegionCiblee == ordreRegional.RegionCiblee)
                        && (ordreRegionalConcurrent.TypeOrdre == EOrdre.AttaqueReussie)
                        && (ordreRegionalConcurrent.Region != ordreRegional.Region))
                    {
                        if (ordreRegionalConcurrent.ForceOffensive < ordreRegional.ForceOffensive)
                        {
                            ordreRegionalConcurrent.TypeOrdre = EOrdre.AttaqueEchouee;
                            this.ReevalueAttaques(ordreRegionalConcurrent);
                        }
                        else if (ordreRegionalConcurrent.ForceOffensive == ordreRegional.ForceOffensive)
                        {
                            ordreRegional.TypeOrdre = EOrdre.AttaqueEchouee;
                            ordreRegionalConcurrent.TypeOrdre = EOrdre.AttaqueEchouee;
                            this.ReevalueAttaques(ordreRegionalConcurrent);
                        }
                    }
                }

                this.ReevalueAttaques(ordreRegional);
            }
            else
            {
                ordreRegional.TypeOrdre = EOrdre.AttaqueEchouee;
            }
        }

        /// <summary>
        /// Marque les régions dans lesquelles les unités seront amenées à faire retraite.
        /// </summary>
        /// <param name="ordre">Ordre à appliquer.</param>
        private void MarqueRetraite(Attaquer ordre)
        {
            OrdreRegional ordreRegional = this.Dictionnaire[ordre.Region];
            if (ordreRegional.TypeOrdre == EOrdre.AttaqueReussie)
            {
                OrdreRegional ordreRegionalCible = this.Dictionnaire[ordre.RegionAttaquee];
                if (ordreRegionalCible.TypeOrdre != EOrdre.AttaqueReussie)
                {
                    ordreRegionalCible.Attaquant = ordreRegional.Region;
                    ordreRegionalCible.Retraite = true;
                }
            }
        }

        #endregion

        #region Méthodes privées auxiliaires

        /// <summary>
        /// Réévalue les ordres d'attaque du fait de la réussite ou de l'échec de l'ordre en argument.
        /// </summary>
        /// <param name="ordre">Ordre impliquant une réévaluation d'autres ordres.</param>
        private void ReevalueAttaques(OrdreRegional ordre)
        {
            List<OrdreRegional> ordresImpactes = this.Dictionnaire.Values.ToList().FindAll(item => item.RegionCiblee == ordre.Region &&
                (item.TypeOrdre == EOrdre.Attaque || item.TypeOrdre == EOrdre.AttaqueEchouee || item.TypeOrdre == EOrdre.AttaqueReussie));

            Int32 forceMax = 0;
            if (ordre.TypeOrdre == EOrdre.AttaqueReussie)
            {
                /// Cette ligne est inutile, mais elle permet de mieux se repérer dans le code.
                forceMax = 0;
            }
            else if (ordre.TypeOrdre == EOrdre.AttaqueEchouee)
            {
                forceMax = ordre.ForceDefensive;
            }

            foreach (OrdreRegional ordreImpacte in ordresImpactes)
            {
                if (ordreImpacte.ForceOffensive > forceMax)
                {
                    forceMax = ordreImpacte.ForceOffensive;
                    ordreImpacte.TypeOrdre = EOrdre.AttaqueReussie;
                    this.ReevalueAttaques(ordreImpacte);
                }
                else if (ordreImpacte.ForceOffensive == forceMax)
                {
                    forceMax++;
                }
            }

            foreach (OrdreRegional ordreImpacte in ordresImpactes)
            {
                if (ordreImpacte.ForceOffensive < forceMax)
                {
                    ordreImpacte.TypeOrdre = EOrdre.AttaqueEchouee;
                }
            }
        }

        #endregion
    }
}