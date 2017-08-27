//-----------------------------------------------------------------------
// <rights file="ListeOrdres.cs" author="Arnaud de LATOUR">
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
    /// Liste d'ordres valides.
    /// </summary>
    public class ListeOrdres
    {
        #region Champs

        /// <summary>
        /// Dictionnaire d'adjacence spécifique de la carte utilisée.
        /// </summary>
        private DictionnaireAdjacence dictionnaireAdjacence;

        /// <summary>
        /// Dictionnaire des régions, triées par noms.
        /// </summary>
        private Dictionary<String, Region> dictionnaireRegions;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe ListeOrdres.
        /// </summary>
        /// <param name="listeOrdres">Liste d'ordres.</param>
        /// <param name="dictionnaireAdjacence">Dictionnaire d'adjacence spécifique à la carte utilisée.</param>
        /// <param name="dictionnaireRegions">Dictionnaire des régions, triées par noms.</param>
        /// <param name="dictionnaireOccupation">Dictionnaire d'occupation des régions.</param>
        public ListeOrdres(
            List<OrdreAbstrait> listeOrdres,
            DictionnaireAdjacence dictionnaireAdjacence,
            Dictionary<String, Region> dictionnaireRegions,
            Dictionary<String, OccupationRegion> dictionnaireOccupation)
        {
            this.dictionnaireAdjacence = dictionnaireAdjacence;
            this.dictionnaireRegions = dictionnaireRegions;

            List<OrdreAbstrait> ordresApplicables = this.TrieOrdresApplicables(listeOrdres, dictionnaireOccupation);

            this.OrdresTenir = new List<Tenir>();
            ordresApplicables.FindAll(item => item.TypeOrdre == EOrdre.Tenir).ForEach(item => this.OrdresTenir.Add(item as Tenir));

            List<Attaquer> ordresAttaque = new List<Attaquer>();
            ordresApplicables.FindAll(item => item.TypeOrdre == EOrdre.Attaque).ForEach(item => ordresAttaque.Add(item as Attaquer));
            this.OrdresAttaqueImmediate = this.TrieAttaquesImmediatesValides(ordresAttaque);

            List<Convoyer> ordresConvoi = new List<Convoyer>();
            ordresApplicables.FindAll(item => item.TypeOrdre == EOrdre.Convoi).ForEach(item => ordresConvoi.Add(item as Convoyer));
            this.OrdresConvoi = this.TrieConvoisValides(ordresConvoi, ordresAttaque);
            this.OrdresAttaqueConvoyee = this.TrieAttaquesConvoyeesValides(ordresAttaque);

            List<SoutenirDefensif> ordresSoutienDefensif = new List<SoutenirDefensif>();
            ordresApplicables.FindAll(item => item.TypeOrdre == EOrdre.SoutienDefensif).ForEach(item => ordresSoutienDefensif.Add(item as SoutenirDefensif));
            this.OrdresSoutienDefensif = this.TrieSoutiensDefensifsValides(ordresSoutienDefensif, this.OrdresTenir);

            List<SoutenirOffensif> ordresSoutienOffensif = new List<SoutenirOffensif>();
            ordresApplicables.FindAll(item => item.TypeOrdre == EOrdre.SoutienOffensif).ForEach(item => ordresSoutienOffensif.Add(item as SoutenirOffensif));
            this.OrdresSoutienOffensif = this.TrieSoutiensOffensifsValides(ordresSoutienOffensif);

            this.OrdresValides = new List<OrdreAbstrait>();
            this.OrdresValides.AddRange(this.OrdresAttaqueConvoyee);
            this.OrdresValides.AddRange(this.OrdresAttaqueImmediate);
            this.OrdresValides.AddRange(this.OrdresConvoi);
            this.OrdresValides.AddRange(this.OrdresSoutienDefensif);
            this.OrdresValides.AddRange(this.OrdresSoutienOffensif);
            this.OrdresValides.AddRange(this.OrdresTenir);
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Liste des ordres valides.
        /// </summary>
        public List<OrdreAbstrait> OrdresValides { get; set; }

        /// <summary>
        /// Liste des ordres d'attaque par convoi valides.
        /// </summary>
        public List<Attaquer> OrdresAttaqueConvoyee { get; set; }

        /// <summary>
        /// Liste des ordres d'attaque imédiats valides.
        /// </summary>
        public List<Attaquer> OrdresAttaqueImmediate { get; set; }

        /// <summary>
        /// Liste des ordres de convoi valides.
        /// </summary>
        public List<Convoyer> OrdresConvoi { get; set; }

        /// <summary>
        /// Liste des ordres de soutien défensif valides.
        /// </summary>
        public List<SoutenirDefensif> OrdresSoutienDefensif { get; set; }

        /// <summary>
        /// Liste des ordres de soutien offensif valides.
        /// </summary>
        public List<SoutenirOffensif> OrdresSoutienOffensif { get; set; }

        /// <summary>
        /// Liste des ordres de type "Tenir" valides.
        /// </summary>
        public List<Tenir> OrdresTenir { get; set; }

        #endregion

        #region Méthodes privées pour déterminer l'ensemble des ordres valides

        /// <summary>
        /// Trie l'ensemble des ordres donnés pour le tour, et retourne ceux qui s'appliquent effectivement à une unité.
        /// </summary>
        /// <param name="listeOrdres">Ensemble des ordres donnés pour le tour.</param>
        /// <param name="dictionnaireOccupation">Dictionnaire de l'occupation des régions de la carte.</param>
        /// <returns>Liste des ordres pour lesquels le doublet (Unité / Région) correspond à l'occupation de la carte.</returns>
        private List<OrdreAbstrait> TrieOrdresApplicables(
            List<OrdreAbstrait> listeOrdres,
            Dictionary<String, OccupationRegion> dictionnaireOccupation)
        {
            List<OrdreAbstrait> ordresApplicables = new List<OrdreAbstrait>();
            foreach (var ordre in listeOrdres)
            {
                if (ordre.Unite == dictionnaireOccupation[ordre.Region].TypeUnite)
                {
                    ordresApplicables.Add(ordre);
                }
            }

            return ordresApplicables;
        }

        /// <summary>
        /// Trie l'ensemble des ordres d'attaque données pour le tour, et retourne ceux qui sont valides.
        /// </summary>
        /// <param name="ordresAttaque">Ensemble des ordres d'attaque données pour le tour.</param>
        /// <returns>Liste des ordres d'attaque valides.</returns>
        private List<Attaquer> TrieAttaquesImmediatesValides(List<Attaquer> ordresAttaque)
        {
            List<Attaquer> ordresAttaquesImmediatesValides = new List<Attaquer>();
            foreach (var attaque in ordresAttaque)
            {
                if (this.TesteAdjacencePourAttaque(attaque) == true)
                {
                    ordresAttaquesImmediatesValides.Add(attaque);
                }
                else
                {
                    this.OrdresTenir.Add(new Tenir(attaque.Unite, attaque.Belligerant, attaque.Region));
                }
            }

            return ordresAttaquesImmediatesValides;
        }

        /// <summary>
        /// Trie l'ensemble des ordres de convoi donnés pour le tour, et retourne ceux qui sont valides.
        /// </summary>
        /// <param name="ordresConvoi">Ensemble des ordres de convoi donnés pour le tour.</param>
        /// <param name="ordresAttaque">Ensemble des ordres d'attaque (partiellement trié) donnés pour le tour.</param>
        /// <returns>Liste des ordres de convoi valides.</returns>
        private List<Convoyer> TrieConvoisValides(List<Convoyer> ordresConvoi, List<Attaquer> ordresAttaque)
        {
            List<Convoyer> ordresConvoiValides = new List<Convoyer>();
            foreach (var convoi in ordresConvoi)
            {
                List<String> regionsMaritimesImpliquees = new List<string>();
                ordresConvoi.FindAll(item => item.RegionSoutenue == convoi.RegionSoutenue).ForEach(item => regionsMaritimesImpliquees.Add(item.Region));

                if (regionsMaritimesImpliquees.Count > 0)
                {
                    Attaquer essaiOrdre = ordresAttaque.Find(item => item.Region == convoi.RegionSoutenue);
                    Console.WriteLine(essaiOrdre.Region);

                    Boolean essai = this.TesteValiditeConvoi(convoi, regionsMaritimesImpliquees);
                    Console.WriteLine(essai);

                    if ((ordresAttaque.Find(item => item.Region == convoi.RegionSoutenue) != null)
                        && (this.TesteValiditeConvoi(convoi, regionsMaritimesImpliquees) == true))
                    {
                        // il faut peut-être vérifier que ce machin va bien et tout et tout...
                        ordresConvoiValides.Add(convoi);
                    }
                    else
                    {
                        this.OrdresTenir.Add(new Tenir(convoi.Unite, convoi.Belligerant, convoi.Region));
                    }
                }
                else
                {
                    this.OrdresTenir.Add(new Tenir(convoi.Unite, convoi.Belligerant, convoi.Region));
                }
            }

            return ordresConvoiValides;
        }

        /// <summary>
        /// Trie l'ensemble des ordres d'attaque par convoi données pour le tour, et retourne ceux qui sont valides.
        /// </summary>
        /// <param name="ordresAttaque">Liste des ordres d'attaque.</param>
        /// <returns>Liste des ordres d'attaque par convoi valides.</returns>
        private List<Attaquer> TrieAttaquesConvoyeesValides(List<Attaquer> ordresAttaque)
        {
            List<Attaquer> ordresAttaquesConvoyeesValides = new List<Attaquer>();
            foreach (var attaque in ordresAttaque)
            {
                // REMARK : cette instruction avait été marquée comme étant erronée...?
                if (this.OrdresConvoi.Find(item => item.RegionSoutenue == attaque.Region) != null)
                {
                    ordresAttaquesConvoyeesValides.Add(attaque);
                }
            }

            return ordresAttaquesConvoyeesValides;
        }

        /// <summary>
        /// Trie l'ensemble des ordres de soutien défensif donnés pour le tour, et retourne ceux qui sont valides.
        /// </summary>
        /// <param name="ordresSoutienDefensif">Ensemble des ordres de soutien défensif donnés pour le tour.</param>
        /// <param name="ordresTenir">>Ensemble des ordres de défense (valides) donnés pour le tour.</param>
        /// <returns>Liste des ordres de soutien défensif valides.</returns>
        private List<SoutenirDefensif> TrieSoutiensDefensifsValides(
            List<SoutenirDefensif> ordresSoutienDefensif,
            List<Tenir> ordresTenir)
        {
            List<SoutenirDefensif> ordresSoutiensDefensifsValides = new List<SoutenirDefensif>();
            foreach (var soutienDefensif in ordresSoutienDefensif)
            {
                // REMARK : cette instruction avait été marquée comme étant erronée...?
                if ((ordresTenir.Find(item => item.Region == soutienDefensif.RegionSoutenue) != null)
                    && (this.TesteValiditeSoutienDefensif(soutienDefensif) == true))
                {
                    ordresSoutiensDefensifsValides.Add(soutienDefensif);
                }
                else
                {
                    this.OrdresTenir.Add(new Tenir(soutienDefensif.Unite, soutienDefensif.Belligerant, soutienDefensif.Region));
                }
            }

            return ordresSoutiensDefensifsValides;
        }

        /// <summary>
        /// Trie l'ensemble des ordres de soutien offensif donnés pour le tour, et retourne ceux qui sont valides.
        /// </summary>
        /// <param name="ordresSoutienOffensif">Ensemble des ordres de soutien offensif donnés pour le tour.</param>
        /// <returns>Liste des ordres de soutien offensif valides.</returns>
        private List<SoutenirOffensif> TrieSoutiensOffensifsValides(List<SoutenirOffensif> ordresSoutienOffensif)
        {
            List<SoutenirOffensif> ordresSoutiensOffensifsValides = new List<SoutenirOffensif>();
            foreach (var soutienOffensif in ordresSoutienOffensif)
            {
                if ((this.OrdresAttaqueImmediate.Find(item => item.Region == soutienOffensif.RegionSoutenue) != null)
                    || (this.OrdresAttaqueConvoyee.Find(item => item.Region == soutienOffensif.RegionSoutenue) != null))
                {
                    if (this.TesteValiditeSoutienOffensif(soutienOffensif) == true)
                    {
                        ordresSoutiensOffensifsValides.Add(soutienOffensif);
                    }
                    else
                    {
                        this.OrdresTenir.Add(new Tenir(soutienOffensif.Unite, soutienOffensif.Belligerant, soutienOffensif.Region));
                    }
                }
                else
                {
                    this.OrdresTenir.Add(new Tenir(soutienOffensif.Unite, soutienOffensif.Belligerant, soutienOffensif.Region));
                }
            }

            return ordresSoutiensOffensifsValides;
        }

        #endregion

        #region Méthodes privées pour tester la validité d'un ordre

        /// <summary>
        /// Teste l'adjacence entre deux régions (hors emploi de convoi), en fonction de l'unité mise en jeu.
        /// </summary>
        /// <param name="ordreATester">Ordre d'attaque pour lequel il faut tester l'adjacence des régions mises en jeu.</param>
        /// <returns>Vrai si l'ordre d'attaque est valide et peut être exécuté sans convoi, faux sinon.</returns>
        private Boolean TesteAdjacencePourAttaque(Attaquer ordreATester)
        {
            Boolean valeurARetourner;
            Region regionAttaquee = this.dictionnaireRegions[ordreATester.RegionAttaquee];
            if ((this.dictionnaireAdjacence.DetermineAdjacence(ordreATester.Region, ordreATester.RegionAttaquee) != EAdjacence.NonAdjacent)
                && (this.dictionnaireAdjacence.DetermineAdjacence(ordreATester.Region, ordreATester.RegionAttaquee) != EAdjacence.AdjacentMer)
                && ordreATester.Unite == EUnite.Armée)
            {
                valeurARetourner = true;
            }
            else if ((this.dictionnaireAdjacence.DetermineAdjacence(ordreATester.Region, ordreATester.RegionAttaquee) != EAdjacence.NonAdjacent)
                && (this.dictionnaireAdjacence.DetermineAdjacence(ordreATester.Region, ordreATester.RegionAttaquee) != EAdjacence.AdjacentTerre)
                && (ordreATester.Unite == EUnite.Flotte))
            {
                valeurARetourner = true;
            }
            else
            {
                valeurARetourner = false;
            }

            return valeurARetourner;
        }

        /// <summary>
        /// Teste la validité d'un ordre de convoi.
        /// </summary>
        /// <param name="ordreATester">Ordre de convoi dont il faut tester la validité.</param>
        /// <param name="regionsMaritimesImpliquees">Liste des régions maritimes impliquées dans le convoi.</param>
        /// <returns>Vrai si l'ordre est valide, faux sinon.</returns>
        private Boolean TesteValiditeConvoi(Convoyer ordreATester, List<String> regionsMaritimesImpliquees)
        {
            Boolean valeurARetourner = (this.dictionnaireRegions[ordreATester.Region].TypeRegion == ETypeRegion.Maritime);
            valeurARetourner = valeurARetourner && (this.dictionnaireRegions[ordreATester.RegionSoutenue].TypeRegion == ETypeRegion.Côtière);
            valeurARetourner = valeurARetourner && (this.dictionnaireRegions[ordreATester.RegionAttaquee].TypeRegion == ETypeRegion.Côtière);
            valeurARetourner = valeurARetourner && this.dictionnaireAdjacence.DetermineConvoi(
                ordreATester.RegionSoutenue,
                ordreATester.RegionAttaquee,
                regionsMaritimesImpliquees);

            return valeurARetourner;
        }

        /// <summary>
        /// Teste la validité d'un ordre de soutien défensif.
        /// </summary>
        /// <param name="ordreATester">Ordre de soutien défensif dont il faut tester la validité.</param>
        /// <returns>Vrai si l'ordre est valide, faux sinon.</returns>
        private Boolean TesteValiditeSoutienDefensif(SoutenirDefensif ordreATester)
        {
            if (ordreATester.Unite == EUnite.Armée)
            {
                if ((this.dictionnaireAdjacence.DetermineAdjacence(ordreATester.Region, ordreATester.RegionSoutenue) == EAdjacence.AdjacentTerre)
                    || (this.dictionnaireAdjacence.DetermineAdjacence(ordreATester.Region, ordreATester.RegionSoutenue) == EAdjacence.AjacentTerreEtMer))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (ordreATester.Unite == EUnite.Flotte)
            {
                if ((this.dictionnaireAdjacence.DetermineAdjacence(ordreATester.Region, ordreATester.RegionSoutenue) == EAdjacence.AdjacentMer)
                    || (this.dictionnaireAdjacence.DetermineAdjacence(ordreATester.Region, ordreATester.RegionSoutenue) == EAdjacence.AjacentTerreEtMer))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Teste la validité d'un ordre de soutien offensif.
        /// </summary>
        /// <param name="ordreATester">Ordre de soutien offensif. dont il faut tester la validité.</param>
        /// <returns>Vrai si l'ordre est valide, faux sinon.</returns>
        private Boolean TesteValiditeSoutienOffensif(SoutenirOffensif ordreATester)
        {
            if (ordreATester.Unite == EUnite.Armée)
            {
                if ((this.dictionnaireAdjacence.DetermineAdjacence(ordreATester.Region, ordreATester.RegionAttaquee) == EAdjacence.AdjacentTerre)
                    || (this.dictionnaireAdjacence.DetermineAdjacence(ordreATester.Region, ordreATester.RegionAttaquee) == EAdjacence.AjacentTerreEtMer))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (ordreATester.Unite == EUnite.Flotte)
            {
                if ((this.dictionnaireAdjacence.DetermineAdjacence(ordreATester.Region, ordreATester.RegionAttaquee) == EAdjacence.AdjacentMer)
                    || (this.dictionnaireAdjacence.DetermineAdjacence(ordreATester.Region, ordreATester.RegionAttaquee) == EAdjacence.AjacentTerreEtMer))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
