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
        /// Ordre associé à une région.
        /// </summary>
    public class OrdreRegional
    {
        #region Champs

        /// <summary>
        /// Nom de la région.
        /// </summary>
        private String region;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe OrdreRegional.
        /// </summary>
        /// <param name="region">Nom de la région à laquelle associer un ordre.</param>
        public OrdreRegional(String region)
        {
            this.region = region;
            this.Reinitialise();
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe OrdreRegional (pour la sauvegarde des retraites uniquement).
        /// </summary>
        /// <param name="typeUnite">Type de l'unité présente dans la région.</param>
        /// <param name="possesseur">Possesseur de l'unité présente dans la région.</param>
        /// <param name="region">Nom de la région.</param>
        /// <param name="attaquant">Nom de la région d'où provient l'attaque forçant à faire retraite.</param>
        public OrdreRegional(EUnite typeUnite, EBelligerant possesseur, String region, String attaquant)
        {
            this.region = region;
            this.Retraite = true;
            this.Attaquant = attaquant;
            this.TypeUnite = typeUnite;
            this.Possesseur = possesseur;
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Indique si l'unité doit faire retraite ou non.
        /// </summary>
        public Boolean Retraite { get; set; }

        /// <summary>
        /// Indique si l'ordre est valide ou non.
        /// </summary>
        public Boolean Valide { get; private set; }

        /// <summary>
        /// Indique le type d'ordre associé à la région pour le tour.
        /// </summary>
        public EOrdre TypeOrdre { get; set; }

        /// <summary>
        /// Indique le type de l'unité présente dans la région.
        /// </summary>
        public EUnite TypeUnite { get; private set; }

        /// <summary>
        /// Indique le possesseur de l'unité présente dans la région.
        /// </summary>
        public EBelligerant Possesseur { get; private set; }

        /// <summary>
        /// Force (en défense) de l'unité présente dans la région.
        /// </summary>
        public Int32 ForceDefensive { get; set; }

        /// <summary>
        /// Force (en attaque) de l'unité présente dans la région.
        /// </summary>
        public Int32 ForceOffensive { get; set; }

        /// <summary>
        /// Nom de la région
        /// </summary>
        public String Region { get { return this.region; } }

        /// <summary>
        /// Nom de la région éventuellement ciblée par l'ordre.
        /// </summary>
        public String RegionCiblee { get; private set; }

        /// <summary>
        /// Nom de la région d'où provient l'attaque forçant à faire retraite.
        /// </summary>
        public String Attaquant { get; set; }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Réinitialise les champs qui le nécessitent.
        /// </summary>
        /// <param name="occupationRegion">Informations relatives à l'occupation de la région.</param>
        public void Reinitialise(OccupationRegion occupationRegion)
        {
            this.TypeUnite = occupationRegion.TypeUnite;
            this.Possesseur = occupationRegion.PossesseurUnite;

            this.Reinitialise();
        }

        /// <summary>
        /// Enregistre un ordre d'attaque.
        /// </summary>
        /// <param name="ordre">Ordre à enregistrer dans le dictionnaire.</param>
        public void EnregistreAttaque(Attaquer ordre)
        {
            this.ForceDefensive = 1;
            this.ForceOffensive = 1;
            this.TypeOrdre = EOrdre.Attaque;
            this.RegionCiblee = ordre.RegionAttaquee;
            this.TypeUnite = ordre.Unite;
            this.Possesseur = ordre.Belligerant;
        }

        /// <summary>
        /// Enregistre un ordre de convoi.
        /// </summary>
        /// <param name="ordre">Ordre à enregistrer dans le dictionnaire.</param>
        public void EnregistreConvoi(Convoyer ordre)
        {
            this.ForceDefensive = 1;
            this.TypeOrdre = EOrdre.Convoi;
            this.RegionCiblee = ordre.RegionSoutenue;
            this.TypeUnite = ordre.Unite;
            this.Possesseur = ordre.Belligerant;
        }

        /// <summary>
        /// Enregistre un ordre de soutien (défensif).
        /// </summary>
        /// <param name="ordre">Ordre à enregistrer dans le dictionnaire.</param>
        public void EnregistreSoutienDefensif(SoutenirDefensif ordre)
        {
            this.ForceDefensive = 1;
            this.TypeOrdre = EOrdre.SoutienDefensif;
            this.RegionCiblee = ordre.RegionSoutenue;
            this.TypeUnite = ordre.Unite;
            this.Possesseur = ordre.Belligerant;
        }

        /// <summary>
        /// Enregistre un ordre de soutien (offensif).
        /// </summary>
        /// <param name="ordre">Ordre à enregistrer dans le dictionnaire.</param>
        public void EnregistreSoutienOffensif(SoutenirOffensif ordre)
        {
            this.ForceDefensive = 1;
            this.ForceOffensive = 1;
            this.TypeOrdre = EOrdre.SoutienOffensif;
            this.RegionCiblee = ordre.RegionSoutenue;
            this.TypeUnite = ordre.Unite;
            this.Possesseur = ordre.Belligerant;
        }

        /// <summary>
        /// Enregistre un ordre de type "Tenir".
        /// </summary>
        /// <param name="ordre">Ordre à enregistrer dans le dictionnaire.</param>
        public void EnregistreTenir(Tenir ordre)
        {
            this.ForceDefensive = 1;
            this.TypeUnite = ordre.Unite;
            this.Possesseur = ordre.Belligerant;
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Réinitialise les champs qui le nécessitent.
        /// </summary>
        private void Reinitialise()
        {
            this.Valide = true;
            this.Retraite = false;
            this.Attaquant = null;

            this.ForceDefensive = 0;
            this.ForceOffensive = 0;
            this.TypeOrdre = EOrdre.Tenir;

            this.RegionCiblee = null;
        }

        #endregion
    }
}