//-----------------------------------------------------------------------
// <rights file="Periode.cs" author="Arnaud de LATOUR">
//     Propriété intellectuelle.
// </rights>
//-----------------------------------------------------------------------

namespace diplo.Jeu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using diplo.Geographie.Enumerations;

    /// <summary>
    /// Période de jeu, correspondant à une année et une phase de jeu.
    /// </summary>
    public class Periode
    {
        #region Champs

        /// <summary>
        /// Année de la période.
        /// </summary>
        private Int32 annee;

        /// <summary>
        /// Phase de jeu de la période.
        /// </summary>
        private EPhaseJeu phase;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe Periode.
        /// </summary>
        /// <param name="annee">Année de la période.</param>
        /// <param name="phase">Phase de jeu de la période.</param>
        public Periode(Int32 annee, EPhaseJeu phase)
        {
            this.annee = annee;
            this.phase = phase;
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Année de la période.
        /// </summary>
        public Int32 Annee { get { return this.annee; } }

        /// <summary>
        /// Phase de jeu de la période.
        /// </summary>
        public EPhaseJeu PhaseJeu { get { return this.phase; } }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Incrémente la période considérée d'une phase de jeu.
        /// </summary>
        public void Incremente()
        {
            Int32 periodecourante = (Int32)(this.phase);

            try
            {
                if (periodecourante + 1 < 7)
                {
                    this.phase = (EPhaseJeu)(periodecourante + 1);
                }
                else
                {
                    this.phase = (EPhaseJeu)(0);
                    this.annee = this.annee + 1;
                }
            }
            catch
            {
                // Un problème peut survenir si, au fur et à mesure des développements,
                // on change l'énumération EPhaseJeu et que celle-ci ne contient
                // plus sept éléments.
                throw new Exception("L'énumération EPhaseJeu a été modifiée indépendamment de la classe Periode.");
            }
        }

        /// <summary>
        /// Retourne la période sous forme de string.
        /// </summary>
        /// <returns>La période sous forme de string.</returns>
        public override String ToString()
        {
            String saison;
            switch(this.phase)
            {
                case (EPhaseJeu.Printemps):
                    saison = "Printemps";
                    break;
                case (EPhaseJeu.Ete):
                    saison = "Été";
                    break;
                case (EPhaseJeu.Automne):
                    saison = "Automne";
                    break;
                case (EPhaseJeu.RetraitesPrintemps):
                    saison = "Printemps";
                    break;
                case (EPhaseJeu.RetraitesEte):
                    saison = "Été";
                    break;
                case (EPhaseJeu.RetraitesAutomne):
                    saison = "Automne";
                    break;
                case (EPhaseJeu.Ajustements):
                    saison = "Hiver";
                    break;
                default:
                    throw new Exception("L'énumération EPhaseJeu a été modifiée indépendamment de la classe Periode.");
            }

            String aRetourner = String.Format("{0} {1}", saison, this.annee);
            return aRetourner;
        }

        /// <summary>
        /// Retourne la période sous forme de string.
        /// </summary>
        /// <returns>La période sous forme de string.</returns>
        public String ToStringToSort()
        {
            String saison;
            switch (this.phase)
            {
                case (EPhaseJeu.Printemps):
                    saison = "Printemps";
                    break;
                case (EPhaseJeu.Ete):
                    saison = "Été";
                    break;
                case (EPhaseJeu.Automne):
                    saison = "Automne";
                    break;
                case (EPhaseJeu.RetraitesPrintemps):
                    saison = "Retraites de printemps";
                    break;
                case (EPhaseJeu.RetraitesEte):
                    saison = "Retraites d'été";
                    break;
                case (EPhaseJeu.RetraitesAutomne):
                    saison = "Retraites d'automne";
                    break;
                case (EPhaseJeu.Ajustements):
                    saison = "Ajustements d'hiver";
                    break;
                default:
                    throw new Exception("L'énumération EPhaseJeu a été modifiée indépendamment de la classe Periode.");
            }

            String aRetourner = String.Format("{0} {1} ({2})", this.annee, (Int32)(this.phase), saison);
            return aRetourner;
        }

        #endregion
    }
}