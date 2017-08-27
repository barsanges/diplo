//-----------------------------------------------------------------------
// <rights file="ChampOrdre.cs" author="Arnaud de LATOUR">
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
    using diplo.Jeu.Ordres;

    /// <summary>
    /// Champ utilisé pour le remplissage d'un ordre.
    /// </summary>
    public partial class ChampOrdre : UserControl
    {
        #region Champs

        /// <summary>
        /// Image interne utilisée pour la persistance du tracé du drapeau.
        /// </summary>
        private Bitmap drapeauInterne;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe ChampOrdre.
        /// </summary>
        public ChampOrdre(String belligerant, String unite, String region)
        {
            this.ConstructeurCommun(belligerant, unite, region);
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe ChampOrdre.
        /// </summary>
        public ChampOrdre(Boolean cotier, String belligerant, String region)
        {
            this.ConstructeurCommun(belligerant, "A", region);
            this.Unite.Visible = false;

            this.comboBox.Items.Clear();
            if (cotier == true)
            {
                this.comboBox.Items.AddRange(new object[] {
                    "",
                    "Armée",
                    "Flotte"});
            }
            else
            {
                this.comboBox.Items.AddRange(new object[] {
                    "",
                    "Armée"});
            }

            this.region.Location = new Point(62, 6);
            this.comboBox.Visible = true;
            this.Ordre.Visible = false;
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Nom du belligérant associé au contrôle.
        /// </summary>
        public String NomBelligerant { get { return this.Belligerant.Text; } }

        /// <summary>
        /// Type d'unité associé au contrôle.
        /// </summary>
        public String NomUnite { get { return this.Unite.Text; } }

        /// <summary>
        /// Nom de la région associée au contrôle.
        /// </summary>
        public String NomRegion { get { return this.region.Text; } }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Retourne l'ordre associé au contrôle.
        /// </summary>
        /// <returns>Ordre associé au contrôle.</returns>
        public OrdreAbstrait RetourneOrdre()
        {
            String[] ordre = this.Ordre.Text.Split(' ');

            String regionConcernee = this.region.Text;
            EUnite unite = Convertisseurs.DepuisEUniteAbrege(this.Unite.Text);
            EBelligerant belligerant = Convertisseurs.DepuisEBelligerantAbrege(this.Belligerant.Text);

            if (ordre[0] == "-")
            {
                String regionAttaquee = ordre[1];
                Attaquer attaque = new Attaquer(unite, belligerant, regionConcernee, regionAttaquee);
                return attaque;
            }
            else if (ordre[0] == "c")
            {
                String regionAttaquante = ordre[1];
                String regionAttaquee = ordre[3];

                Convoyer convoi = new Convoyer(belligerant, regionConcernee, regionAttaquante, regionAttaquee);
                return convoi;
            }
            else if (ordre[0] == "s")
            {
                if (ordre.Length < 4)
                {
                    String regionSoutenue = ordre[1];

                    SoutenirDefensif soutieDefensif = new SoutenirDefensif(unite, belligerant, regionConcernee, regionSoutenue);
                    return soutieDefensif;
                }
                else
                {
                    String regionAttaquante = ordre[1];
                    String regionAttaquee = ordre[3];

                    SoutenirOffensif soutienOffensif = new SoutenirOffensif(
                        unite,
                        belligerant,
                        regionConcernee,
                        regionAttaquante,
                        regionAttaquee);
                    return soutienOffensif;
                }
            }
            else
            {
                Tenir tenir = new Tenir(unite, belligerant, regionConcernee);
                return tenir;
            }
        }

        /// <summary>
        /// Retourne l'ordre d'ajustement associé au contrôle.
        /// </summary>
        /// <returns>Ordre d'ajustement associé au contrôle.</returns>
        public Ajuster RetourneAjustement()
        {
            EUnite unite;
            EAjustement ajustement;
            String nomRegion = this.region.Text;
            EBelligerant belligerant = Convertisseurs.DepuisEBelligerantAbrege(this.Belligerant.Text);
            if (this.comboBox.Visible == true)
            {
                String recrutement = this.comboBox.Text;
                if (recrutement != "")
                {
                    unite = Convertisseurs.VersEUnite(recrutement);
                    ajustement = EAjustement.Recrutement;
                }
                else
                {
                    unite = EUnite.Aucune;
                    ajustement = EAjustement.Aucun;
                }
            }
            else
            {
                unite = Convertisseurs.DepuisEUniteAbrege(this.Unite.Text);
                if(this.Ordre.Text == "*")
                {
                    ajustement = EAjustement.Congédiement;
                }
                else
                {
                    ajustement = EAjustement.Aucun;
                }
            }

            Ajuster ordre = new Ajuster(ajustement, unite, belligerant, nomRegion);
            return ordre;
        }

        /// <summary>
        /// Affiche le drapeau asocié au belligérant.
        /// </summary>
        public void AfficheDrapeau()
        {
            this.emplacementDrapeau.Visible = true;
        }

        #endregion

        #region Méthodes publiques statiques

        /// <summary>
        /// Classe les deux champs selon un ordre total (par belligérants, unité, et territoire).
        /// </summary>
        /// <param name="premierChamp">L'un des deux champs à comparer.</param>
        /// <param name="secondChamp">L'un des deux champs à comparer.</param>
        /// <returns>Valeur négative si premierChamp est avant premierChamp, et positive dans le cas opposé.</returns>
        public static Int32 ClasseChampsOrdres(ChampOrdre premierChamp, ChampOrdre secondChamp)
        {
            if (premierChamp.NomBelligerant != secondChamp.NomBelligerant)
            {
                EBelligerant premierBelligerant = Convertisseurs.DepuisEBelligerantAbrege(premierChamp.NomBelligerant);
                EBelligerant secondBelligerant = Convertisseurs.DepuisEBelligerantAbrege(secondChamp.NomBelligerant);
                return Convertisseurs.ClasseEBelligerants(premierBelligerant, secondBelligerant);
            }
            else
            {
                if (premierChamp.NomUnite != secondChamp.NomUnite)
                {
                    if (premierChamp.NomUnite == "A")
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    Int32 triAlphabetique = String.Compare(premierChamp.NomRegion, secondChamp.NomRegion);
                    return triAlphabetique;
                }
            }
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Fonction à l'usage inconnu.
        /// </summary>
        private void ChampOrdre_Load(object sender, EventArgs e)
        {
            // Rien à faire.
        }

        /// <summary>
        /// Gère partiellement le rafraîchissement du drapeau.
        /// </summary>
        private void emplacementDrapeau_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.DrawImage(this.drapeauInterne, 0, 0);
        }

        /// <summary>
        /// Partie de l'initialisation commune à tous les constructeurs.
        /// </summary>
        private void ConstructeurCommun(String belligerant, String unite, String region)
        {
            InitializeComponent();
            this.Belligerant.Text = belligerant;
            this.Unite.Text = unite;
            this.region.Text = region;

            this.drapeauInterne = new Bitmap(this.emplacementDrapeau.Size.Width, this.emplacementDrapeau.Size.Height);
            this.DessineDrapeau();
        }

        /// <summary>
        /// Dessine le drapeau associé au belligérant.
        /// </summary>
        private void DessineDrapeau()
        {
            Graphics outilsGraphiques = Graphics.FromImage(this.drapeauInterne);

            Pen traceurContour = new Pen(Color.Black, 2);
            Geographie.Enumerations.EBelligerant nationalite = Geographie.Convertisseurs.DepuisEBelligerantAbrege(this.NomBelligerant);
            if (nationalite == Geographie.Enumerations.EBelligerant.Palavin)
            {
                traceurContour = new Pen(Color.Red, 2);
            }

            outilsGraphiques.DrawRectangle(traceurContour, 0, 0, this.emplacementDrapeau.Width, this.emplacementDrapeau.Height);

            Color couleur = Convertisseurs.VersCouleur(nationalite);
            Brush traceur = new SolidBrush(couleur);
            outilsGraphiques.FillRectangle(traceur, 1, 1, this.emplacementDrapeau.Width - 2, this.emplacementDrapeau.Height - 2);

            outilsGraphiques.Dispose();
        }

        #endregion
    }
}