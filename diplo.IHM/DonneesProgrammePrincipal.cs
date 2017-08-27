//-----------------------------------------------------------------------
// <rights file="DonneesProgrammePrincipal.cs" author="Arnaud de LATOUR">
//     Propriété intellectuelle.
// </rights>
//-----------------------------------------------------------------------

namespace diplo.IHM
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using diplo.Carte;
    using diplo.Geographie;
    using diplo.Geographie.Enumerations;
    using diplo.Jeu;
    using diplo.Jeu.Ordres;

    using DRegion = System.Drawing.Region;
    using GRegion = diplo.Geographie.Region;

    /// <summary>
    /// Conteneur pour toutes les données utilisées par le programme principal.
    /// </summary>
    public class DonneesProgrammePrincipal
    {
        #region Champs

        /// <summary>
        /// Dictionnaire des régions, triées par noms.
        /// </summary>
        private Dictionary<String, GRegion> dictionnaireRegions;

        /// <summary>
        /// Dictionnaire d'adjacence spécifique à la carte utilisée.
        /// </summary>
        private DictionnaireAdjacence dictionnaireAdjacence;

        /// <summary>
        /// Dictionnaire d'ordres utilisé pour résoudre un tour de jeu (hors retraites et recrutements).
        /// </summary>
        private DictionnaireOrdres dictionnaireOrdres;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe DonneesProgrammePrincipal.
        /// </summary>
        /// <param name="donneesCalendaires">Année et phase de jeu sous forme d'une chaîne de caractères.</param>
        /// <param name="cheminDictionnaireAdjacence">Chemin d'accès au fichier à charger (matrice d'adjacence).</param>
        /// <param name="cheminDictionnaireRegions">Chemin d'accès au fichier à charger (dictionnaire des régions).</param>
        public DonneesProgrammePrincipal(
            String donneesCalendaires,
            String cheminDictionnaireAdjacence,
            String cheminDictionnaireRegions)
        {
            this.ListeRetraites = new List<OrdreRegional>();
            this.InitialisePeriodeJeu(donneesCalendaires);
            this.dictionnaireAdjacence = new DictionnaireAdjacence(cheminDictionnaireAdjacence);
            this.InitialiseDictionnaireRegions(cheminDictionnaireRegions);
            this.InitialiseDictionnaireOrdre();
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Phase de jeu courante.
        /// </summary>
        public EPhaseJeu PhaseCourante { get { return this.PeriodeCourante.PhaseJeu; } }

        /// <summary>
        /// Période (année et saison) courante.
        /// </summary>
        public Periode PeriodeCourante { get; set; }

        /// <summary>
        /// Liste complète des noms des régions.
        /// </summary>
        public List<String> ListeNomsRegions { get { return this.DictionnaireAdjacence.ListeNomsRegions; } }

        /// <summary>
        /// Liste des retraites imposées au tour courant.
        /// Pour chaque élément, la clé correspond à la région d'où part la retraite,
        /// et la valeur à la région qui a forcé la retraite.
        /// </summary>
        public List<OrdreRegional> ListeRetraites { get; set; }

        /// <summary>
        /// Dictionnaire d'adjacence spécifique à la carte utilisée.
        /// </summary>
        public DictionnaireAdjacence DictionnaireAdjacence { get { return this.dictionnaireAdjacence; } }

        /// <summary>
        /// Dictionnaire des régions, triées par noms.
        /// </summary>
        public Dictionary<String, GRegion> DictionnaireRegions { get { return this.dictionnaireRegions; } }

        /// <summary>
        /// Dictionnaire des centres de recrutement, triés par belligérant.
        /// </summary>
        public Dictionary<EBelligerant, List<String>> DictionnaireRecrutement { get; private set; }

        /// <summary>
        /// Dictionnaire d'occupation des régions, triées par noms.
        /// </summary>
        public Dictionary<String, OccupationRegion> DictionnaireOccupation { get; set; }

        #endregion

        #region Méthodes publiques de gestion globale

        /// <summary>
        /// Initialise l'image de la carte.
        /// </summary>
        /// <param name="carte">Image actuelle de la carte.</param>
        public void InitialiseCarte(Image carte, Graphics outilsGraphiques)
        {
            foreach (String nomRegion in this.ListeNomsRegions)
            {
                /// On fait deux fois les tests (y a-t-il un centre ? y a-t-il une unité ?), puisqu'ils sont aussi faits
                /// dans les fonctions de Pictogrammes. Mais cela permet de mieux saisir ce que l'on fait...
                GRegion regionActuelle = this.DictionnaireRegions[nomRegion];
                OccupationRegion occupation = this.DictionnaireOccupation[nomRegion];

                if (regionActuelle.EstUnCentre == true)
                {
                    Pictogrammes.AffilieCentre(regionActuelle as Centre, occupation as OccupationCentre, carte, outilsGraphiques);
                }

                if(occupation.TypeUnite != EUnite.Aucune)
                {
                    Pictogrammes.PlaceUnite(regionActuelle, occupation, carte, outilsGraphiques);
                }
            }
        }

        /// <summary>
        /// Sauvegarde les données de la partie courante.
        /// </summary>
        /// <param name="cheminDictionnaireRegions">Fichier dans lequel sauvegarder le dictionnaire des régions.</param>
        public void Sauvegarde(String cheminDictionnaireRegions)
        {
            using (StreamWriter redacteur = new StreamWriter(cheminDictionnaireRegions))
            {
                foreach (String nomRegion in this.ListeNomsRegions)
                {
                    GRegion regionASauvegarder = this.DictionnaireRegions[nomRegion];
                    String abreviation = regionASauvegarder.Abreviation;
                    String centre = regionASauvegarder.EstUnCentre.ToString();
                    String typeRegion = ((Int32)(regionASauvegarder.TypeRegion)).ToString();
                    String coordonneesUnites = regionASauvegarder.CoordonneesUnite.Convertit();

                    OccupationRegion occupationASauvegarder = this.DictionnaireOccupation[nomRegion];
                    String possesseurUnite = ((Int32)(occupationASauvegarder.PossesseurUnite)).ToString();
                    String unite = ((Int32)(occupationASauvegarder.TypeUnite)).ToString();

                    String donneesCompletes;
                    if (regionASauvegarder.EstUnCentre == false)
                    {
                        donneesCompletes = String.Format(
                            "{0};{1};{2};{3};{4};{5};{6}",
                            nomRegion,
                            abreviation,
                            centre,
                            typeRegion,
                            coordonneesUnites,
                            possesseurUnite,
                            unite);
                    }
                    else
                    {
                        Centre centreASAuvegarder = regionASauvegarder as Centre;
                        String coordonneesCentre = centreASAuvegarder.CoordonneesCentre.Convertit();

                        OccupationCentre occupationCentre = occupationASauvegarder as OccupationCentre;
                        String possesseurCentre = ((Int32)(occupationCentre.PossesseurCentre)).ToString();

                        String recrutement = ((Int32)(centreASAuvegarder.Recrutement)).ToString();

                        donneesCompletes = String.Format(
                            "{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}",
                            nomRegion,
                            abreviation,
                            centre,
                            typeRegion,
                            coordonneesUnites,
                            possesseurUnite,
                            unite,
                            coordonneesCentre,
                            possesseurCentre,
                            recrutement);
                    }

                    redacteur.WriteLine(donneesCompletes);
                }

                foreach (var retraite in this.ListeRetraites)
                {
                    redacteur.WriteLine(
                        "{0};{1};{2};{3}",
                        retraite.Attaquant,
                        retraite.Possesseur,
                        retraite.Region,
                        retraite.TypeUnite);
                }
            }
        }

        #endregion

        #region Méthodes de gestion des tours

        /// <summary>
        /// Finit le tour en cours.
        /// </summary>
        /// <param name="retraites">Indique s'il s'agit d'un tour de retraites ou non.</param>
        /// <param name="ordres">Liste des ordres donnés pour le tour.</param>
        /// <param name="carte">Image représentant la carte.</param>
        /// <param name="outilsGraphiques">Outils graphiques utilisés pour effectuer le dessin.</param>
        public void FinitTour(Boolean retraites, List<OrdreAbstrait> ordres, Image carte, Graphics outilsGraphiques)
        {
            ListeOrdres listeOrdres = new ListeOrdres(
                ordres,
                this.DictionnaireAdjacence,
                this.dictionnaireRegions,
                this.DictionnaireOccupation);
            List<OrdreAbstrait> ordresValides = listeOrdres.OrdresValides;

            this.dictionnaireOrdres.EnregistreOrdres(ordresValides, this.DictionnaireOccupation);
            this.dictionnaireOrdres.AppliqueOrdres(ordresValides);

            foreach (OrdreRegional ordreRegional in this.dictionnaireOrdres.Dictionnaire.Values)
            {
                if (ordreRegional.TypeOrdre != EOrdre.Tenir)
                {
                    GRegion regionConcerne = this.DictionnaireRegions[ordreRegional.Region];
                    GRegion regionCiblee = this.DictionnaireRegions[ordreRegional.RegionCiblee];
                    switch (ordreRegional.TypeOrdre)
                    {
                        case EOrdre.AttaqueReussie:
                            Pictogrammes.TraceAttaque(regionConcerne, regionCiblee, ordreRegional.Possesseur, carte, outilsGraphiques);
                            this.IntegreAttaqueReussie(retraites, ordreRegional);
                            break;
                        case EOrdre.AttaqueEchouee:
                            Pictogrammes.TraceAttaqueRatee(regionConcerne, regionCiblee, ordreRegional.Possesseur, carte, outilsGraphiques);
                            break;
                        case EOrdre.Convoi:
                            GRegion regionArrivee = this.DictionnaireRegions[this.dictionnaireOrdres.Dictionnaire[regionCiblee.Nom].RegionCiblee];
                            Pictogrammes.TraceTransport(regionConcerne, regionCiblee, regionArrivee, ordreRegional.Possesseur, carte, outilsGraphiques);
                            break;
                        case EOrdre.ConvoiCoupe:
                            GRegion regionArriveeBis = this.DictionnaireRegions[this.dictionnaireOrdres.Dictionnaire[regionCiblee.Nom].RegionCiblee];
                            Pictogrammes.TraceTransportInterrompu(regionConcerne, regionCiblee, regionArriveeBis, ordreRegional.Possesseur, carte, outilsGraphiques);
                            break;
                        case EOrdre.SoutienDefensif:
                            Pictogrammes.TraceSoutienDefensif(regionConcerne, regionCiblee, ordreRegional.Possesseur, carte, outilsGraphiques);
                            break;
                        case EOrdre.SoutienDefensifCoupe:
                            Pictogrammes.TraceSoutienDefensifCoupe(regionConcerne, regionCiblee, ordreRegional.Possesseur, carte, outilsGraphiques);
                            break;
                        case EOrdre.SoutienOffensif:
                            GRegion regionAttaquee = this.DictionnaireRegions[this.dictionnaireOrdres.Dictionnaire[regionCiblee.Nom].RegionCiblee];
                            Pictogrammes.TraceSoutienOffensif(regionConcerne, regionCiblee, regionAttaquee, ordreRegional.Possesseur, carte, outilsGraphiques);
                            break;
                        case EOrdre.SoutienOffensifCoupe:
                            GRegion regionAttaqueeBis = this.DictionnaireRegions[this.dictionnaireOrdres.Dictionnaire[regionCiblee.Nom].RegionCiblee];
                            Pictogrammes.TraceSoutienOffensifCoupe(regionConcerne, regionCiblee, regionAttaqueeBis, ordreRegional.Possesseur, carte, outilsGraphiques);
                            break;
                        default:
                            break;
                    }
                }
            }

            this.NettoieDictionnaireOccupation();
            this.ListeRetraites = this.dictionnaireOrdres.Dictionnaire.Values.Where(item => item.Retraite == true && item.TypeUnite != EUnite.Aucune).ToList();
        }

        /// <summary>
        /// Gère les ordres de retraite.
        /// </summary>
        /// <param name="ordresRetraites">Liste des ordres de retraite donnés pour le tour.</param>
        /// <param name="carte">Image représentant la carte.</param>
        /// <param name="outilsGraphiques">Outils graphiques utilisés pour effectuer le dessin.</param>
        public void GereRetraites(List<Attaquer> ordresRetraites, Image carte, Graphics outilsGraphiques)
        {
            List<OrdreAbstrait> ordresApplicables = new List<OrdreAbstrait>();
            foreach (OrdreRegional retraite in this.ListeRetraites)
            {
                Attaquer ordreCorrespondant = ordresRetraites.Find(item => item.Region == retraite.Region);
                if (ordreCorrespondant != null)
                {
                    if (ordreCorrespondant.RegionAttaquee == retraite.Attaquant)
                    {
                        // Rien n'est à faire.
                    }
                    else
                    {
                        ordresApplicables.Add(ordreCorrespondant as OrdreAbstrait);
                    }
                }
            }

            this.FinitTour(true, ordresApplicables, carte, outilsGraphiques);
        }

        /// <summary>
        /// Gère les ordres d'ajustement.
        /// </summary>
        /// <param name="ordresAjustement">Liste des ordres d'ajustement donnés pour le tour.</param>
        public void GereAjustements(List<Ajuster> ordresAjustement)
        {
            foreach (Ajuster ordre in ordresAjustement)
            {
                if (ordre.Recrutement == EAjustement.Recrutement)
                {
                    this.DictionnaireOccupation[ordre.Region].Recrute(ordre.Unite, ordre.Belligerant);
                }
                else if (ordre.Recrutement == EAjustement.Congédiement)
                {
                    this.DictionnaireOccupation[ordre.Region].Congedie();
                }
            }
        }

        /// <summary>
        /// Gère le changement d'allégeance hivernal des centres.
        /// </summary>
        public void GereAllegeancesCentres()
        {
            foreach (GRegion region in this.DictionnaireRegions.Values)
            {
                if (region.EstUnCentre == true && this.DictionnaireOccupation[region.Nom].TypeUnite != EUnite.Aucune)
                {
                    OccupationCentre occupation = this.DictionnaireOccupation[region.Nom] as OccupationCentre;
                    occupation.ChangeAllegeanceCentre(occupation.PossesseurUnite);
                }
            }
        }

        #endregion

        #region Méthodes liées au maître de jeu

        /// <summary>
        /// Ajoute arbitrairement une unité sur la carte.
        /// </summary>
        /// <param name="identificateurBelligerant">Belligérant possesseur de l'unité à ajouter.</param>
        /// <param name="identificateurUnite">Type d'unité à ajouter.</param>
        /// <param name="nomRegion">Nom de la région où ajouter l'unité.</param>
        /// <param name="carte">Image représentant la carte.</param>
        /// <param name="outilsGraphiques">Outils graphiques utilisés pour effectuer le dessin.</param>
        public void AjouteUnite(String identificateurBelligerant, String identificateurUnite, String nomRegion, Image carte, Graphics outilsGraphiques)
        {
            if(this.DictionnaireOccupation.Keys.Contains(nomRegion) == true)
            {
                EBelligerant belligerantConcerne = Convertisseurs.VersEBelligerant(identificateurBelligerant);
                EUnite uniteAjoutee = Convertisseurs.VersEUnite(identificateurUnite);

                this.DictionnaireOccupation[nomRegion].ChangeUnite(uniteAjoutee, belligerantConcerne);
                Pictogrammes.PlaceUnite(this.DictionnaireRegions[nomRegion], this.DictionnaireOccupation[nomRegion], carte, outilsGraphiques);
            }
            else
            {
                throw new Exception("L'allégeance et/ou le nom de la région sont mal définis.");
            }
        }

        /// <summary>
        /// Change arbitrairement l'allégeance d'une unité sur la carte.
        /// </summary>
        /// <param name="identificateurBelligerant">Belligérant prenant possession de l'unité.</param>
        /// <param name="nomRegion">Nom de la région où l'unité change d'allégeance.</param>
        /// <param name="carte">Image représentant la carte.</param>
        /// <param name="outilsGraphiques">Outils graphiques utilisés pour effectuer le dessin.</param>
        public void ChangeAllegeanceUnite(String identificateurBelligerant, String nomRegion, Image carte, Graphics outilsGraphiques)
        {
            if(this.DictionnaireOccupation.Keys.Contains(nomRegion) == true)
            {
                EUnite unite = this.DictionnaireOccupation[nomRegion].TypeUnite;
                EBelligerant belligerantConcerne = Convertisseurs.VersEBelligerant(identificateurBelligerant);
                this.DictionnaireOccupation[nomRegion].ChangeUnite(unite, belligerantConcerne);
                Pictogrammes.PlaceUnite(this.DictionnaireRegions[nomRegion], this.DictionnaireOccupation[nomRegion], carte, outilsGraphiques);
            }
            else
            {
                throw new Exception("L'allégeance et/ou le nom de la région sont mal définis.");
            }
        }

        /// <summary>
        /// Supprime arbitrairement une unité de la carte.
        /// </summary>
        /// <param name="nomRegion">Nom de la région dans laquelle supprimer une unité.</param>
        /// <param name="carte">Image représentant la carte.</param>
        /// <param name="outilsGraphiques">Outils graphiques utilisés pour effectuer le dessin.</param>
        public void SupprimeUnite(String nomRegion, Image carte, Graphics outilsGraphiques)
        {
            if(this.DictionnaireOccupation.Keys.Contains(nomRegion) == true)
            {
                this.DictionnaireOccupation[nomRegion].DetruitOuAbandonne();
                Pictogrammes.PlaceUnite(
                    this.DictionnaireRegions[nomRegion],
                    this.DictionnaireOccupation[nomRegion],
                    carte,
                    outilsGraphiques);
            }
            else
            {
                throw new Exception("La région n'existe pas, ou est mal définie.");
            }
        }

        #endregion

        #region Sous-méthodes du constructeur

        /// <summary>
        /// Initialise le champ correspondant à la période de jeu.
        /// </summary>
        /// <param name="donneesCalendaires">Année et phase de jeu sous forme d'une chaîne de caractères.</param>
        private void InitialisePeriodeJeu(String donneesCalendaires)
        {
            Int32 anneeDeJeu = Int32.Parse(donneesCalendaires.Split(':')[0]);
            EPhaseJeu phaseDeJeu = (EPhaseJeu)(Int32.Parse(donneesCalendaires.Split(':')[1]));

            this.PeriodeCourante = new Periode(anneeDeJeu, phaseDeJeu);
        }

        /// <summary>
        /// Initialise le dictionnaire des régions.
        /// </summary>
        /// <param name="fichierACharger">Chemin d'accès au fichier à charger contenant le dictionnaire des régions.</param>
        private void InitialiseDictionnaireRegions(String fichierACharger)
        {
            this.DictionnaireRecrutement = new Dictionary<EBelligerant, List<String>>();
            this.dictionnaireRegions = new Dictionary<string, GRegion>();
            this.DictionnaireOccupation = new Dictionary<string, OccupationRegion>();
            using (StreamReader lecteur = new StreamReader(fichierACharger))
            {
                foreach (String nomRegion in this.ListeNomsRegions)
                {
                    String donnees = lecteur.ReadLine();
                    String[] donneesDetaillees = donnees.Split(';');

                    if (donneesDetaillees[0] == nomRegion)
                    {
                        String abreviation = donneesDetaillees[1];
                        Boolean centre = Boolean.Parse(donneesDetaillees[2]);
                        ETypeRegion typeRegion = (ETypeRegion)(Int32.Parse(donneesDetaillees[3]));
                        Coordonnees coordonneesUnite = Coordonnees.Convertit(donneesDetaillees[4]);
                        EBelligerant possesseurUnite = (EBelligerant)(Int32.Parse(donneesDetaillees[5]));
                        EUnite unite = (EUnite)(Int32.Parse(donneesDetaillees[6]));

                        if (centre == false)
                        {
                            GRegion nouvelleRegion = new GRegion(nomRegion, abreviation, typeRegion, coordonneesUnite);
                            this.dictionnaireRegions.Add(nomRegion, nouvelleRegion);

                            OccupationRegion occupation = new OccupationRegion(nomRegion, unite, possesseurUnite);
                            this.DictionnaireOccupation.Add(nomRegion, occupation);
                        }
                        else
                        {
                            Coordonnees coordonneesCentre = Coordonnees.Convertit(donneesDetaillees[7]);
                            EBelligerant possesseurCentre = (EBelligerant)(Int32.Parse(donneesDetaillees[8]));

                            EBelligerant recrutement = (EBelligerant)(Int32.Parse(donneesDetaillees[9]));

                            Centre nouveauCentre = new Centre(
                                nomRegion,
                                abreviation,
                                typeRegion,
                                coordonneesUnite,
                                coordonneesCentre,
                                recrutement);
                            this.dictionnaireRegions.Add(nomRegion, nouveauCentre);

                            OccupationCentre occupation = new OccupationCentre(nomRegion, unite, possesseurUnite, possesseurCentre);
                            this.DictionnaireOccupation.Add(nomRegion, occupation);

                            if (recrutement != EBelligerant.Aucun)
                            {
                                if(this.DictionnaireRecrutement.ContainsKey(recrutement))
                                {
                                    this.DictionnaireRecrutement[recrutement].Add(nomRegion);
                                }
                                else
                                {
                                    this.DictionnaireRecrutement.Add(recrutement, new List<String>() { nomRegion });
                                }
                            }
                        }

                    }
                    else
                    {
                        throw new Exception("Le fichier de régions ne correspond pas à la matrice d'adjacence.");
                    }
                }

                String retraite = lecteur.ReadLine();
                while (retraite != null)
                {
                    String[] retraiteDetaillee = retraite.Split(';');

                    String attaquant = retraiteDetaillee[0];
                    EBelligerant possesseur = Convertisseurs.VersEBelligerant(retraiteDetaillee[1]);
                    String region = retraiteDetaillee[2];
                    EUnite typeUnite = Convertisseurs.VersEUnite(retraiteDetaillee[3]);

                    OrdreRegional nouvelleRetraite = new OrdreRegional(typeUnite, possesseur, region, attaquant);
                    this.ListeRetraites.Add(nouvelleRetraite);

                    retraite = lecteur.ReadLine();
                }
            }
        }

        /// <summary>
        /// Initialise le dictionnaire des ordres.
        /// </summary>
        private void InitialiseDictionnaireOrdre()
        {
            this.dictionnaireOrdres = new DictionnaireOrdres(this.ListeNomsRegions);
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Intègre au dictionnaire d'occupation les effets d'une attaque réussie.
        /// </summary>
        /// <param name="retraite">Indique s'il s'agit d'une retraite ou non.</param>
        /// <param name="ordreRegional">Ordre régional d'attaque réussie à prendre en compte.</param>
        private void IntegreAttaqueReussie(Boolean retraite, OrdreRegional ordreRegional)
        {
            String regionQuittee = ordreRegional.Region;
            String regionRejointe = ordreRegional.RegionCiblee;

            if (retraite == false)
            {
                this.DictionnaireOccupation[regionQuittee].DetruitOuAbandonne();
            }

            this.DictionnaireOccupation[regionRejointe].ChangeUnite(ordreRegional.TypeUnite, ordreRegional.Possesseur);
        }

        /// <summary>
        /// Nettoie le contenu du dictionnaire d'occupation des régions.
        /// </summary>
        private void NettoieDictionnaireOccupation()
        {
            foreach (OccupationRegion occupation in this.DictionnaireOccupation.Values)
            {
                occupation.NettoieChampsFuturs();
            }
        }

        #endregion
    }
}