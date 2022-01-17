//-----------------------------------------------------------------------
// <rights file="MetaChampOrdre.cs" author="Arnaud de LATOUR">
//     Propriété intellectuelle.
// </rights>
//-----------------------------------------------------------------------

namespace diplo.Contrôles_auxiliaires
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using diplo.Geographie;
    using diplo.Geographie.Enumerations;
    using diplo.Jeu;

    using DRegion = System.Drawing.Region;
    using GRegion = diplo.Geographie.Region;

    /// <summary>
    /// Contrôle utilisé pour accueillir l'ensemble des champs d'ordre pour un tour donné.
    /// </summary>
    public partial class MetaChampOrdre : UserControl
    {
        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe MetaChampOrdre.
        /// </summary>
        public MetaChampOrdre()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(vScrollBar_MouseWheel);
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Liste des champs d'ordres utilisés dans le contrôle.
        /// </summary>
        public List<ChampOrdre> ChampsOrdres { get; set; }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Crée les champs d'ordres correspondant aux ordres, et les place de manière adéquate.
        /// </summary>
        /// <param name="dictionnaireOccupation">Dictionnaire d'occupation correspondant à la partie courante.</param>
        public void CreeChampsOrdres_Ordres(Dictionary<String, OccupationRegion> dictionnaireOccupation)
        {
            /// Crée les champs.
            this.ChampsOrdres = new List<ChampOrdre>();
            foreach (OccupationRegion regionOccupee in dictionnaireOccupation.Values)
            {
                if (regionOccupee.TypeUnite != Geographie.Enumerations.EUnite.Aucune)
                {
                    String nomBelligerant = Geographie.Convertisseurs.VersEBelligerantAbrege(regionOccupee.PossesseurUnite);
                    String typeUnite = Geographie.Convertisseurs.VersEUniteAbrege(regionOccupee.TypeUnite);
                    String nomRegion = regionOccupee.Region;

                    this.ChampsOrdres.Add(new ChampOrdre(nomBelligerant, typeUnite, nomRegion));
                }
            }

            this.CreeChampsOrdres_General();
        }

        /// <summary>
        /// Crée les champs d'ordres correspondant aux retraites, et les place de manière adéquate.
        /// </summary>
        /// <param name="listeRetraites">Liste des retraites à effectuer pour le tour.</param>
        public void CreeChampsOrdres_Retraites(List<OrdreRegional> listeRetraites)
        {
            this.ChampsOrdres = new List<ChampOrdre>();
            foreach (OrdreRegional retraite in listeRetraites)
            {
                String nomBelligerant = Geographie.Convertisseurs.VersEBelligerantAbrege(retraite.Possesseur);
                String typeUnite = Geographie.Convertisseurs.VersEUniteAbrege(retraite.TypeUnite);
                String nomRegion = retraite.Region;

                this.ChampsOrdres.Add(new ChampOrdre(nomBelligerant, typeUnite, nomRegion));
            }

            this.CreeChampsOrdres_General();
        }

        /// <summary>
        /// Crée les champs d'ordres correspondant aux ajustements, et les place de manière adéquate.
        /// </summary>
        /// <param name="nombreCentres">Nombre de centres par belligérants.</param>
        /// <param name="nombreUnites">Nombre d'unités par belligérants.</param>
        /// <param name="dictionnaireRegions">Dictionnaire des régions correspondant à la carte.</param>
        /// <param name="dictionnaireOccupation">Dictionnaire d'occupation correspondant à la partie courante.</param>
        /// <param name="dictionnaireRecrutement">Dictionnaire des lieux de recrutement autorisés.</param>
        public void CreeChampsOrdres_Ajustements(
            Dictionary<String, Int32> nombreCentres,
            Dictionary<String, Int32> nombreUnites,
            Dictionary<String, GRegion> dictionnaireRegions,
            Dictionary<String, OccupationRegion> dictionnaireOccupation,
            Dictionary<EBelligerant, List<String>> dictionnaireRecrutement)
        {
            foreach (String nomBelligerant in nombreCentres.Keys)
            {
                EBelligerant belligerantCourant = Convertisseurs.VersEBelligerant(nomBelligerant);
                if (nombreCentres[nomBelligerant] > nombreUnites[nomBelligerant])
                {
                    foreach (String region in dictionnaireRecrutement[belligerantCourant])
                    {
                        OccupationCentre centre = dictionnaireOccupation[region] as OccupationCentre;
                        if (centre.TypeUnite == EUnite.Aucune && centre.PossesseurCentre == belligerantCourant)
                        {
                            Boolean regionCotiere = (dictionnaireRegions[region].TypeRegion == ETypeRegion.Côtière) ? true : false;
                            String nomBelligerantAbrege = Convertisseurs.VersEBelligerantAbrege(belligerantCourant);
                            this.ChampsOrdres.Add(new ChampOrdre(regionCotiere, nomBelligerantAbrege, region));
                        }
                    }
                }
                else if (nombreCentres[nomBelligerant] < nombreUnites[nomBelligerant])
                {
                    foreach (OccupationRegion regionOccupee in dictionnaireOccupation.Values)
                    {
                        if ((regionOccupee.TypeUnite != Geographie.Enumerations.EUnite.Aucune) && (regionOccupee.PossesseurUnite == belligerantCourant))
                        {
                            String nomBelligerantAbrege = Convertisseurs.VersEBelligerantAbrege(belligerantCourant);
                            String typeUnite = Geographie.Convertisseurs.VersEUniteAbrege(regionOccupee.TypeUnite);
                            String nomRegion = regionOccupee.Region;

                            this.ChampsOrdres.Add(new ChampOrdre(nomBelligerantAbrege, typeUnite, nomRegion));
                        }
                    }
                }
            }

            this.CreeChampsOrdres_General();
        }

        /// <summary>
        /// Nettoie la liste des champs d'ordres en les détruisant proprement.
        /// </summary>
        public void NettoieChampsOrdres()
        {
            if (this.ChampsOrdres != null)
            {
                foreach (ChampOrdre ordre in this.ChampsOrdres)
                {
                    this.Controls.Remove(ordre);
                }

                this.ChampsOrdres.Clear();
            }
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Place les champs dans le contrôle.
        /// </summary>
        private void PlaceChampsOrdres()
        {
            if (this.ChampsOrdres.Count > 0)
            {
                Int32 largeurCourante = 20;
                Int32 hauteurCourante = 04 - vScrollBar.Value;
                Int32 incrementHauteur = 25;
                String belligerantPrecedent = this.ChampsOrdres[0].NomBelligerant;
                this.ChampsOrdres[0].AfficheDrapeau();
                foreach (ChampOrdre ordre in this.ChampsOrdres)
                {
                    if (ordre.NomBelligerant != belligerantPrecedent)
                    {
                        belligerantPrecedent = ordre.NomBelligerant;
                        hauteurCourante += incrementHauteur;

                        ordre.AfficheDrapeau();
                    }

                    ordre.Location = new Point(largeurCourante, hauteurCourante);
                    ordre.Visible = true;
                    ordre.Enabled = true;
                    if (this.Controls.Contains(ordre) == false)
                    {
                        this.Controls.Add(ordre);
                    }

                    hauteurCourante += incrementHauteur;
                }
            }
        }

        /// <summary>
        /// Crée les champs d'ordres, et les place de manière adéquate.
        /// </summary>
        private void CreeChampsOrdres_General()
        {
            /// Trie les champs.
            this.ChampsOrdres.Sort(ChampOrdre.ClasseChampsOrdres);

            /// Place les champs.
            this.PlaceChampsOrdres();
        }

        #endregion

        #region Méthodes privées de manipulation du contrôle

        /// <summary>
        /// Permet de faire défiler le contrôle en cliquant sur la barre.
        /// </summary>
        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            this.PlaceChampsOrdres();
        }

        /// <summary>
        /// Permet de faire défiler le contrôle avec la roulette de la souris.
        /// </summary>
        private void vScrollBar_MouseWheel(object sender, MouseEventArgs e)
        {
            vScrollBar.Focus();
        }

        #endregion
    }
}
