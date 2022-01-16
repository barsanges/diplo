//-----------------------------------------------------------------------
// <rights file="ProgrammePrincipal.cs" author="Arnaud de LATOUR">
//     Propriété intellectuelle.
// </rights>
//-----------------------------------------------------------------------

namespace diplo.IHM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using diplo.Contrôles_auxiliaires;
    using diplo.Jeu;
    using diplo.Jeu.Ordres;
    using diplo.Geographie;
    using diplo.Geographie.Enumerations;

    /// <summary>
    /// Point d'entrée et interface graphique de l'application.
    /// </summary>
    public partial class ProgrammePrincipal : Form
    {
        #region Champs

        /// <summary>
        /// Valeur indiquant si une partie a déjà été chargée ou non.
        /// </summary>
        private Boolean estInitialise = false;

        /// <summary>
        /// Valeur indiquant si la carte a été rafraîchie (les flèches d'ordre ne sont plus présentes) ou non.
        /// </summary>
        private Boolean estRafraichi = false;

        /// <summary>
        /// Chemin d'accès au fichier de la dernière partie sauvegardée.
        /// </summary>
        private String dernierePartie;

        /// <summary>
        /// Dictionnaire du total de centres par belligérant.
        /// </summary>
        /// <remarks>
        /// On fait ici une grosse écorne à l'organisation initiale (ce genre de choses devrait être dans le conteneur).
        /// </remarks>
        private Dictionary<String, Int32> nombreCentres;

        /// <summary>
        /// Dictionnaire du total d'unités par belligérant.
        /// </summary>
        /// <remarks>
        /// On fait ici une grosse écorne à l'organisation initiale (ce genre de choses devrait être dans le conteneur).
        /// </remarks>
        private Dictionary<String, Int32> nombreUnites;

        /// <summary>
        /// Image interne utilisée pour la persistance du tracé de la carte.
        /// </summary>
        private Bitmap imageCarteInterne;

        /// <summary>
        /// Ensemble des données relatives à la partie et à la carte.
        /// </summary>
        private DonneesProgrammePrincipal conteneurDonnees;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe ProgrammePrincipal.
        /// </summary>
        public ProgrammePrincipal()
        {
            InitializeComponent();

            this.nombreCentres = new Dictionary<String, Int32>();
            this.nombreUnites = new Dictionary<String, Int32>();
            this.nombreCentres.Add("Lautrec", 0);
            this.nombreCentres.Add("Barsanges", 0);
            this.nombreCentres.Add("Empire", 0);
            this.nombreCentres.Add("Mélinde", 0);
            this.nombreCentres.Add("Brémontrée", 0);
            this.nombreCentres.Add("Gretz", 0);
            this.nombreCentres.Add("Palavin", 0);
            this.nombreCentres.Add("Thymée", 0);

            this.nombreUnites.Add("Lautrec", 0);
            this.nombreUnites.Add("Barsanges", 0);
            this.nombreUnites.Add("Empire", 0);
            this.nombreUnites.Add("Mélinde", 0);
            this.nombreUnites.Add("Brémontrée", 0);
            this.nombreUnites.Add("Gretz", 0);
            this.nombreUnites.Add("Palavin", 0);
            this.nombreUnites.Add("Thymée", 0);

            this.ChargeCarteVierge();
           // TODO_LATER : gérer les exceptions...
        }

        #endregion

        #region Méthodes du menu "Accueil"

        /// <summary>
        /// Crée une nouvelle partie.
        /// </summary>
        private void creerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.estInitialise = true;

                this.ChargeCarteVierge();
                this.InitialiseConteneurDonnees();

                this.Reinitialise("Ordres");
            }
            catch (Exception exceptionRecuperee)
            {
                MessageBox.Show(exceptionRecuperee.Message);
            }
        }

        /// <summary>
        /// Charge une partie préalablement enregistrée.
        /// </summary>
        private void chargerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.creerToolStripMenuItem_Click(sender, e);
                    this.Charge(openFileDialog.FileName);
                }
            }
            catch (Exception exceptionRecuperee)
            {
                MessageBox.Show(exceptionRecuperee.Message);
            }
        }

        /// <summary>
        /// Sauvegarder la partie en cours.
        /// </summary>
        private void sauvegarderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.estInitialise == true)
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        this.Sauvegarde(saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception exceptionRecuperee)
            {
                MessageBox.Show(exceptionRecuperee.Message);
            }
        }

        #endregion

        #region Méthodes du menu "Jeu"

        /// <summary>
        /// Simule le tour de jeu avec les ordres donnés, sans le jouer effectivement.
        /// </summary>
        private void simulerTour(object sender, EventArgs e)
        {
            try
            {
                if ((this.estInitialise == true) && this.emplacementPhase.Text == "Ordres")
                {
                    this.SauvegardeDernierePartie();
                    this.finirTour(sender, e);

                    this.accepterSimulation.Visible = true;
                    this.refuserSimulation.Visible = true;
                    this.metaChampOrdre.Visible = false;
                }
            }
            catch (Exception exceptionRecuperee)
            {
                MessageBox.Show(exceptionRecuperee.Message);
            }
        }

        /// <summary>
        /// Finit le tour de jeu.
        /// </summary>
        private void finirTour(object sender, EventArgs e)
        {
            try
            {
                if (this.estInitialise == true)
                {
                    this.SauvegardeDernierePartie();
                    this.raffraichit(sender, e);
                    this.exportCarte_ToolStripExport(sender, e);

                    this.ChargeCarteVierge();
                    Graphics outilsGraphiques = Graphics.FromImage(this.imageCarteInterne);
                    outilsGraphiques.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

                    if (this.emplacementPhase.Text == "Ordres")
                    {
                        List<OrdreAbstrait> listeOrdres = this.LitOrdres();
                        this.conteneurDonnees.FinitTour(false, listeOrdres, this.imageCarteInterne, outilsGraphiques);
                    }
                    else if (this.emplacementPhase.Text == "Retraites")
                    {
                        List<Attaquer> ordresRetraites = this.LitRetraites();
                        this.conteneurDonnees.GereRetraites(ordresRetraites, this.imageCarteInterne, outilsGraphiques);
                        this.conteneurDonnees.ListeRetraites.Clear();
                    }
                    else if (this.emplacementPhase.Text == "Ajustements")
                    {
                        List<Ajuster> ordresAjustement = this.LitAjustements();
                        this.conteneurDonnees.GereAjustements(ordresAjustement);
                    }
                    else
                    {
                        throw new Exception("La définition / conversion des phases n'a pas été mise à jour.");
                    }

                    this.conteneurDonnees.InitialiseCarte(this.imageCarteInterne, outilsGraphiques);
                    outilsGraphiques.Dispose();
                    this.imageCarte.Invalidate();

                    this.estRafraichi = false;
                    this.exportCarte_ToolStripExport(sender, e);
                    if (this.emplacementPhase.Text == "Ordres" && this.conteneurDonnees.ListeRetraites.Count == 0)
                    {
                        this.conteneurDonnees.PeriodeCourante.Incremente();
                    }

                    this.conteneurDonnees.PeriodeCourante.Incremente();
                    this.emplacementDate.Text = this.conteneurDonnees.PeriodeCourante.ToString();
                    this.emplacementPhase.Text = Convertisseurs.VersPhaseJeuAbregee(this.conteneurDonnees.PhaseCourante);
                    if (this.emplacementPhase.Text == "Ajustements")
                    {
                        this.conteneurDonnees.GereAllegeancesCentres();
                        this.InitialiseCarte();
                    }

                    this.CreeChampsOrdres(this.emplacementPhase.Text);
                }
            }
            catch (Exception exceptionRecuperee)
            {
                MessageBox.Show(exceptionRecuperee.Message);
            }
        }

        /// <summary>
        /// Raffraîchit l'affichage de la carte, et retire les flèches d'ordre.
        /// </summary>
        private void raffraichit(object sender, EventArgs e)
        {
            this.estRafraichi = true;
            this.ChargeCarteVierge();
            this.InitialiseCarte();
        }

        /// <summary>
        /// Accepte la simulation, et termine effectivement le tour.
        /// </summary>
        private void accepterSimulation_Click(object sender, EventArgs e)
        {
            this.accepterSimulation.Visible = false;
            this.refuserSimulation.Visible = false;
            this.metaChampOrdre.Visible = true;
        }

        /// <summary>
        /// Refuse la simulation, et remet le jeu dans la situation pré-simulation.
        /// </summary>
        private void refuserSimulation_Click(object sender, EventArgs e)
        {
            this.ChargeDernierePartie();

            this.accepterSimulation.Visible = false;
            this.refuserSimulation.Visible = false;
            this.metaChampOrdre.Visible = true;
        }

        #endregion

        #region Méthodes du menu "Export"

        /// <summary>
        /// Exporte la carte au format PNG.
        /// </summary>
        private void exportCarte_ToolStripExport(object sender, EventArgs e)
        {
            try
            {
                if (this.estInitialise == true)
                {
                    String dossierActuel = Directory.GetCurrentDirectory();
                    String cheminExport = dossierActuel.Replace(@"diplo.IHM\bin\Debug", @"Partie en cours");
                    String fichierExport;
                    if (this.estRafraichi == true)
                    {
                        fichierExport = String.Format(@"{0}\Carte {1} 0.png", cheminExport, this.conteneurDonnees.PeriodeCourante.ToStringToSort());
                    }
                    else
                    {
                        fichierExport = String.Format(@"{0}\Carte {1} 1.png", cheminExport, this.conteneurDonnees.PeriodeCourante.ToStringToSort());
                    }

                    imageCarteInterne.Save(fichierExport, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            catch (Exception exceptionRecuperee)
            {
                MessageBox.Show(exceptionRecuperee.Message);
            }
        }

        #endregion

        #region Méthodes auxiliaires

        /// <summary>
        /// Gère partiellement le rafraîchissement de la carte.
        /// </summary>
        /// <remarks>Méthode entre autres appelée par this.imageCarte.Invalidate().</remarks>
        private void imageCarte_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (this.estInitialise == true)
            {
                e.Graphics.DrawImage(this.imageCarteInterne, 0, 0);
            }
        }

        /// <summary>
        /// Charge la dernière partie sauvegardée sous un nom prédéfini.
        /// </summary>
        public void ChargeDernierePartie()
        {
            this.Charge(this.dernierePartie);
        }

        /// <summary>
        ///Charge la partie sauvegardée dans le fichier indiqué.
        /// </summary>
        /// <param name="cheminFichier">Chemin d'accès au fichier de sauvegarde.</param>
        public void Charge(String cheminFichier)
        {
            this.estInitialise = true;

            String donneesCalendaires, matriceAdjacence, geographieParRegion, ordres;
            using (StreamReader lecteur = new StreamReader(cheminFichier))
            {
                donneesCalendaires = lecteur.ReadLine();
                matriceAdjacence = lecteur.ReadLine();
                geographieParRegion = lecteur.ReadLine();
                ordres = lecteur.ReadLine();
            }

            this.conteneurDonnees = new DonneesProgrammePrincipal(donneesCalendaires, matriceAdjacence, geographieParRegion);
            this.ChargeCarteVierge();
            this.Reinitialise(ordres);
        }

        /// <summary>
        /// Sauvegarde la partie en cours sous un nom prédéfini.
        /// </summary>
        private void SauvegardeDernierePartie()
        {
            String dossierActuel = Directory.GetCurrentDirectory();
            String nomFichier = String.Format(@"Cartes & parties\Sauvegardes\Dernier Tour {0} ({1})",
                this.conteneurDonnees.PeriodeCourante,
                this.conteneurDonnees.PhaseCourante);
            String cheminFichier = dossierActuel.Replace(@"diplo.IHM\bin\Debug", nomFichier);
            this.dernierePartie = cheminFichier;
            this.Sauvegarde(cheminFichier);
        }

        /// <summary>
        /// Sauvegarde la partie en cours dans le fichier indiqué.
        /// </summary>
        /// <param name="cheminFichier">Chemin d'accès au fichier de sauvegarde.</param>
        private void Sauvegarde(String cheminFichier)
        {
            String dossierActuel = Directory.GetCurrentDirectory();

            String donneesCalendaires = String.Format(
                "{0}:{1}",
                this.conteneurDonnees.PeriodeCourante.Annee,
                (Int32)(this.conteneurDonnees.PhaseCourante));

            String cleUnique = String.Format(
                "{0}, {1}, {2}, {3}",
                DateTime.UtcNow.Hour.ToString(),
                DateTime.UtcNow.Minute.ToString(),
                DateTime.UtcNow.Second.ToString(),
                DateTime.UtcNow.Millisecond.ToString());
            String geographieParRegion = String.Format(
                @"Cartes & parties\Sauvegardes\Géographie {0} ({1}).bdat",
                this.conteneurDonnees.PeriodeCourante.ToString(),
                cleUnique);
            geographieParRegion = dossierActuel.Replace(@"diplo.IHM\bin\Debug", geographieParRegion);
            this.conteneurDonnees.Sauvegarde(geographieParRegion);

            String matriceAdjacence = dossierActuel.Replace(@"diplo.IHM\bin\Debug", @"Cartes & parties\Configuration initiale - Matrice.bdat");
            String ordres = this.emplacementPhase.Text;

            using (StreamWriter redacteur = new StreamWriter(cheminFichier))
            {
                redacteur.WriteLine(donneesCalendaires);
                redacteur.WriteLine(matriceAdjacence);
                redacteur.WriteLine(geographieParRegion);
                redacteur.WriteLine(ordres);
            }
        }

        /// <summary>
        /// Réinitialise la carte et les champs d'ordres.
        /// </summary>
        private void Reinitialise(String ordres)
        {
            if (this.estInitialise == true)
            {
                this.InitialiseCarte();

                this.CreeChampsOrdres(ordres);

                /// Affichage de la date et de la phase
                this.emplacementDate.Visible = true;
                this.emplacementDate.Text = this.conteneurDonnees.PeriodeCourante.ToString();
                this.emplacementPhase.Visible = true;
                this.emplacementPhase.Text = Convertisseurs.VersPhaseJeuAbregee(this.conteneurDonnees.PhaseCourante);
            }
        }

        /// <summary>
        /// Initialise le conteneur de données.
        /// </summary>
        private void InitialiseConteneurDonnees()
        {
            String dossierActuel = Directory.GetCurrentDirectory();
            String donneesCalendaires = "1422:0";
            String matriceAdjacence = dossierActuel.Replace(@"diplo.IHM\bin\Debug", @"Cartes & parties\Configuration initiale - Matrice.bdat");
            String geographieParRegion = dossierActuel.Replace(@"diplo.IHM\bin\Debug", @"Cartes & parties\Configuration initiale - Géographie.bdat"); ;
            this.conteneurDonnees = new DonneesProgrammePrincipal(donneesCalendaires, matriceAdjacence, geographieParRegion);
        }

        /// <summary>
        /// Charge l'image correspondant à la carte vierge.
        /// </summary>
        private void ChargeCarteVierge()
        {
            String dossierActuel = Directory.GetCurrentDirectory();
            String carteACharger = dossierActuel.Replace(@"diplo.IHM\bin\Debug", @"Cartes & parties\Configuration initiale - Carte.png");
            this.imageCarte.Load(carteACharger);
            this.imageCarteInterne = Bitmap.FromFile(carteACharger) as Bitmap;
        }

        /// <summary>
        /// Initialise la carte dans la configuration de départ.
        /// </summary>
        private void InitialiseCarte()
        {
            Graphics outilsGraphiques = Graphics.FromImage(this.imageCarteInterne);

            outilsGraphiques.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.conteneurDonnees.InitialiseCarte(this.imageCarte.Image, outilsGraphiques);

            outilsGraphiques.Dispose();
            this.imageCarte.Invalidate();
        }

        /// <summary>
        /// Crée et place la liste de champs d'ordres à utiliser.
        /// </summary>
        /// <param name="phase">Phase pour laquelle créer les champs.</param>
        private void CreeChampsOrdres(String phase)
        {
            this.metaChampOrdre.NettoieChampsOrdres();
            this.metaChampOrdre.Visible = true;
            this.CompteCentresEtArmees();
            switch (phase)
            {
                case "Ordres":
                    this.metaChampOrdre.CreeChampsOrdres_Ordres(this.conteneurDonnees.DictionnaireOccupation);
                    break;
                case "Retraites":
                    this.metaChampOrdre.CreeChampsOrdres_Retraites(this.conteneurDonnees.ListeRetraites);
                    break;
                case "Ajustements":
                    this.metaChampOrdre.CreeChampsOrdres_Ajustements(
                        this.nombreCentres,
                        this.nombreUnites,
                        this.conteneurDonnees.DictionnaireRegions,
                        this.conteneurDonnees.DictionnaireOccupation,
                        this.conteneurDonnees.DictionnaireRecrutement);
                    break;
                default:
                    throw new Exception("La définition / conversion des phases n'a pas été mise à jour.");
            }

            this.CompteCentresEtArmees();
        }

        /// <summary>
        /// Compte les centres, les armées et les flottes de chacun des belligérants.
        /// </summary>
        /// <remarks>Cette méthode est vraiment très mal codée.</remarks>
        private void CompteCentresEtArmees()
        {
            Int32 centresLautrec = 0;
            Int32 centresBarsanges = 0;
            Int32 centresEmpire = 0;
            Int32 centresMelinde = 0;
            Int32 centresBremontree = 0;
            Int32 centresGretz = 0;
            Int32 centresPalavin = 0;
            Int32 centresThymee = 0;

            foreach (OccupationRegion region in this.conteneurDonnees.DictionnaireOccupation.Values)
            {
                if (this.conteneurDonnees.DictionnaireRegions[region.Region].EstUnCentre == true)
                {
                    OccupationCentre centre = region as OccupationCentre;
                    switch (centre.PossesseurCentre)
                    {
                        case EBelligerant.Lautrec:
                            centresLautrec++;
                            break;
                        case EBelligerant.Barsanges:
                            centresBarsanges++;
                            break;
                        case EBelligerant.Empire:
                            centresEmpire++;
                            break;
                        case EBelligerant.Mélinde:
                            centresMelinde++;
                            break;
                        case EBelligerant.Brémontrée:
                            centresBremontree++;
                            break;
                        case EBelligerant.Gretz:
                            centresGretz++;
                            break;
                        case EBelligerant.Palavin:
                            centresPalavin++;
                            break;
                        case EBelligerant.Thymée:
                            centresThymee++;
                            break;
                        default:
                            break;
                    }
                }
            }

            this.labelCompteurLautrec.Visible = true;
            this.compteurLautrec.Visible = true;
            this.compteurLautrec.Text = String.Format("{0} centre(s), {1} armée(s), {2} flotte(s)",
                centresLautrec,
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Lautrec && item.TypeUnite == EUnite.Armée),
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Lautrec && item.TypeUnite == EUnite.Flotte));
            this.nombreCentres["Lautrec"] = centresLautrec;
            this.nombreUnites["Lautrec"] = this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Lautrec && item.TypeUnite == EUnite.Armée) +
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Lautrec && item.TypeUnite == EUnite.Flotte);

            this.labelCompteurBarsanges.Visible = true;
            this.compteurBarsanges.Visible = true;
            this.compteurBarsanges.Text = String.Format("{0} centre(s), {1} armée(s), {2} flotte(s)",
                centresBarsanges,
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Barsanges && item.TypeUnite == EUnite.Armée),
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Barsanges && item.TypeUnite == EUnite.Flotte));
            this.nombreCentres["Barsanges"] = centresBarsanges;
            this.nombreUnites["Barsanges"] = this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Barsanges && item.TypeUnite == EUnite.Armée) +
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Barsanges && item.TypeUnite == EUnite.Flotte);

            this.labelCompteurEmpire.Visible = true;
            this.compteurEmpire.Visible = true;
            this.compteurEmpire.Text = String.Format("{0} centre(s), {1} armée(s), {2} flotte(s)",
                centresEmpire,
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Empire && item.TypeUnite == EUnite.Armée),
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Empire && item.TypeUnite == EUnite.Flotte));
            this.nombreCentres["Empire"] = centresEmpire;
            this.nombreUnites["Empire"] = this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Empire && item.TypeUnite == EUnite.Armée) +
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Empire && item.TypeUnite == EUnite.Flotte);

            this.labelCompteurMelinde.Visible = true;
            this.compteurMelinde.Visible = true;
            this.compteurMelinde.Text = String.Format("{0} centre(s), {1} armée(s), {2} flotte(s)",
                centresMelinde,
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Mélinde && item.TypeUnite == EUnite.Armée),
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Mélinde && item.TypeUnite == EUnite.Flotte));
            this.nombreCentres["Mélinde"] = centresMelinde;
            this.nombreUnites["Mélinde"] = this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Mélinde && item.TypeUnite == EUnite.Armée) +
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Mélinde && item.TypeUnite == EUnite.Flotte);

            this.labelCompteurBremontree.Visible = true;
            this.compteurBremontree.Visible = true;
            this.compteurBremontree.Text = String.Format("{0} centre(s), {1} armée(s), {2} flotte(s)",
                centresBremontree,
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Brémontrée && item.TypeUnite == EUnite.Armée),
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Brémontrée && item.TypeUnite == EUnite.Flotte));
            this.nombreCentres["Brémontrée"] = centresBremontree;
            this.nombreUnites["Brémontrée"] = this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Brémontrée && item.TypeUnite == EUnite.Armée)+
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Brémontrée && item.TypeUnite == EUnite.Flotte);

            this.labelCompteurGretz.Visible = true;
            this.compteurGretz.Visible = true;
            this.compteurGretz.Text = String.Format("{0} centre(s), {1} armée(s), {2} flotte(s)",
                centresGretz,
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Gretz && item.TypeUnite == EUnite.Armée),
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Gretz && item.TypeUnite == EUnite.Flotte));
            this.nombreCentres["Gretz"] = centresGretz;
            this.nombreUnites["Gretz"] = this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Gretz && item.TypeUnite == EUnite.Armée) +
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Gretz && item.TypeUnite == EUnite.Flotte);

            this.labelCompteurPalavin.Visible = true;
            this.compteurPalavin.Visible = true;
            this.compteurPalavin.Text = String.Format("{0} centre(s), {1} armée(s), {2} flotte(s)",
                centresPalavin,
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Palavin && item.TypeUnite == EUnite.Armée),
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Palavin && item.TypeUnite == EUnite.Flotte));
            this.nombreCentres["Palavin"] = centresPalavin;
            this.nombreUnites["Palavin"] = this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Palavin && item.TypeUnite == EUnite.Armée) +
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Palavin && item.TypeUnite == EUnite.Flotte);

            this.labelCompteurThymee.Visible = true;
            this.compteurThymee.Visible = true;
            this.compteurThymee.Text = String.Format("{0} centre(s), {1} armée(s), {2} flotte(s)",
                centresThymee,
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Thymée && item.TypeUnite == EUnite.Armée),
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Thymée && item.TypeUnite == EUnite.Flotte));
            this.nombreCentres["Thymée"] = centresThymee;
            this.nombreUnites["Thymée"] = this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Thymée && item.TypeUnite == EUnite.Armée) +
                this.conteneurDonnees.DictionnaireOccupation.Values.Count(item => item.PossesseurUnite == EBelligerant.Thymée && item.TypeUnite == EUnite.Flotte);
        }

        /// <summary>
        /// Traque les unités maintenues illégalement durant l'hiver, et force leur congédiement.
        /// </summary>
        /// <param name="compteur">Nombre d'unités congédiées ce tour (si des congédiements sont nécessaires).</param>
        /// <param name="placesDisponibles">Différence entre le nombre de centres et le nombre d'unités.</param>
        /// <param name="congediementTemporaires">Ensemble des ordres d'ajustement potentiels pour le belligérant courant.</param>
        /// <param name="ordresAjustements">Ensemble des ordres d'ajustement pour le tour.</param>
        private void TraqueUnitesSuperfetatoires(
            Int32 compteur,
            Int32 placesDisponibles,
            List<Ajuster> congediementTemporaires,
            List<Ajuster> ordresAjustements)
        {
            if ((placesDisponibles < 0) && (compteur < Math.Abs(placesDisponibles)))
            {
                foreach (Ajuster ordreCongediement in congediementTemporaires)
                {
                    if ((ordreCongediement.Recrutement != EAjustement.Congédiement)
                        && (compteur < Math.Abs(placesDisponibles)))
                    {
                        Ajuster nouveauCongediement = new Ajuster(
                            EAjustement.Congédiement,
                            ordreCongediement.Unite,
                            ordreCongediement.Belligerant,
                            ordreCongediement.Region);
                        ordresAjustements.Add(nouveauCongediement);
                        compteur++;
                    }
                }
            }
        }

        /// <summary>
        /// Lit l'ensemble des ordres notés dans les champs d'ordres.
        /// </summary>
        /// <returns>Ensemble des ordres donnés pour le tour.</returns>
        private List<OrdreAbstrait> LitOrdres()
        {
            List<OrdreAbstrait> listeOrdres = new List<OrdreAbstrait>();
            foreach (ChampOrdre ordre in this.metaChampOrdre.ChampsOrdres)
            {
                listeOrdres.Add(ordre.RetourneOrdre());
            }

            return listeOrdres;
        }

        /// <summary>
        /// Lit l'ensemble des ordres de retraite notés dans les champs d'ordre
        /// (phase de retraite uniquement).
        /// </summary>
        /// <returns>Ensemble des ordres de retraite pour la phase de retraite courante.</returns>
        private List<Attaquer> LitRetraites()
        {
            List<Attaquer> listeRetraites = new List<Attaquer>();
            foreach (ChampOrdre ordre in this.metaChampOrdre.ChampsOrdres)
            {
                OrdreAbstrait ordreRetraite = ordre.RetourneOrdre();
                if (ordreRetraite.GetType() == typeof(Attaquer))
                {
                    listeRetraites.Add(ordreRetraite as Attaquer);
                }
            }

            return listeRetraites;
        }

        /// <summary>
        /// Lit l'ensemble des ordres d'ajustement notés dans les champs d'ordre
        /// (phase d'ajustement uniquement).
        /// </summary>
        /// <returns>Ensemble des ordres d'ajustement donnés pour le tour d'hiver courant.</returns>
        private List<Ajuster> LitAjustements()
        {
            Int32 compteur = 0;
            Int32 placesDisponibles = 0;
            String belligerant = null;
            List<Ajuster> ordresAjustements = new List<Ajuster>();
            List<Ajuster> congediementTemporaires = new List<Ajuster>();
            foreach (ChampOrdre ordre in this.metaChampOrdre.ChampsOrdres)
            {
                Ajuster ordreAjustement = ordre.RetourneAjustement();
                congediementTemporaires.Add(ordreAjustement);
                if (ordre.NomBelligerant != belligerant)
                {
                    this.TraqueUnitesSuperfetatoires(compteur, placesDisponibles, congediementTemporaires, ordresAjustements);

                    congediementTemporaires.Clear();
                    belligerant = ordre.NomBelligerant;
                    compteur = 0;
                    String nomCompletBelligerant = Convertisseurs.DepuisEBelligerantAbrege(ordre.NomBelligerant).ToString();
                    placesDisponibles = this.nombreCentres[nomCompletBelligerant] - this.nombreUnites[nomCompletBelligerant];
                }

                if (((ordreAjustement.Recrutement == EAjustement.Recrutement) && (compteur < placesDisponibles))
                    || (ordreAjustement.Recrutement == EAjustement.Congédiement))
                {
                    ordresAjustements.Add(ordreAjustement);
                    compteur++;
                }
            }

            this.TraqueUnitesSuperfetatoires(compteur, placesDisponibles, congediementTemporaires, ordresAjustements);
            return ordresAjustements;
        }

        #endregion
    }
}