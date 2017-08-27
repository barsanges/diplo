//-----------------------------------------------------------------------
// <rights file="Pictogrammes.cs" author="Arnaud de LATOUR">
//     Propriété intellectuelle.
// </rights>
//-----------------------------------------------------------------------

namespace diplo.Carte
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Linq;
    using System.Text;
    using diplo.Geographie;
    using diplo.Geographie.Enumerations;

    using DRegion = System.Drawing.Region;
    using GRegion = diplo.Geographie.Region;

    /// <summary>
    /// Ensemble des méthodes statiques utilisées pour modifier la carte.
    /// </summary>
    public static class Pictogrammes
    {
        #region Méthode touchant aux centres

        /// <summary>
        /// Affilie un centre à un belligérant.
        /// </summary>
        /// <param name="region">Région dont le centre est à affilier.</param>
        /// <param name="occupation">Occupation du centre.</param>
        /// <param name="carte">Image représentant la carte.</param>
        /// <param name="outilsGraphiques">Outils graphiques utilisés pour effectuer le dessin.</param>
        public static void AffilieCentre(Centre region, OccupationCentre occupation, Image carte, Graphics outilsGraphiques)
        {
            if (region.EstUnCentre == true)
            {
                /// Les centres mesurent 7x7 pixels.
                Pen traceurContour = new Pen(Color.Black);
                if (occupation.PossesseurCentre == EBelligerant.Palavin)
                {
                    traceurContour = new Pen(Color.Red);
                }

                outilsGraphiques.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                outilsGraphiques.DrawRectangle(traceurContour, region.CoordonneesCentre.X, region.CoordonneesCentre.Y, 7, 7);

                Color couleur = Convertisseurs.VersCouleur(occupation.PossesseurCentre);
                Brush traceur = new SolidBrush(couleur);
                outilsGraphiques.FillRectangle(traceur, region.CoordonneesCentre.X + 1, region.CoordonneesCentre.Y + 1, 6, 6);
            }
            else
            {
                throw new Exception(String.Format("La région {0} ne contient pas de centre.", region.Nom));
            }
        }

        #endregion

        #region Méthode touchant au placement des unités

        /// <summary>
        /// Place une unité dans une région.
        /// </summary>
        /// <param name="region">Région dans laquelle placer une unité.</param>
        /// <param name="occupation">Occupation de la région.</param>
        /// <param name="carte">Image représentant la carte.</param>
        /// <param name="outilsGraphiques">Outils graphiques utilisés pour effectuer le dessin.</param>
        public static void PlaceUnite(GRegion region, OccupationRegion occupation, Image carte, Graphics outilsGraphiques)
        {
            if (occupation.TypeUnite != EUnite.Aucune)
            {
                String dossierActuel = Directory.GetCurrentDirectory();
                String imageACharger;
                if (occupation.TypeUnite == EUnite.Flotte)
                {
                    imageACharger = dossierActuel.Replace(@"diplo.IHM\bin\Debug", @"Images\Flotte");
                }
                else
                {
                    imageACharger = dossierActuel.Replace(@"diplo.IHM\bin\Debug", @"Images\Armée");
                }

                String identificateurBelligerant = ((Int32)(occupation.PossesseurUnite)).ToString();
                imageACharger = imageACharger + "0" + identificateurBelligerant + ".png";

                Image imageUnite = new Bitmap(imageACharger);
                outilsGraphiques.DrawImage(imageUnite, region.CoordonneesUnite.X, region.CoordonneesUnite.Y);
            }
        }

        #endregion

        #region Méthodes touchant aux déplacements des unités

        /// <summary>
        /// Trace une flèche correspondant à une attaque réussie.
        /// </summary>
        /// <param name="regionAttaquante">La région d'où provient l'attaque.</param>
        /// <param name="regionAttaquee">La région où aboutit l'attaque.</param>
        /// <param name="belligerant">Identificateur du belligérant à l'origine de l'attaque.</param>
        /// <param name="carte">Image représentant la carte.</param>
        /// <param name="outilsGraphiques">Outils graphiques utilisés pour effectuer le dessin.</param>
        public static void TraceAttaque(
            GRegion regionAttaquante,
            GRegion regionAttaquee,
            EBelligerant belligerant,
            Image carte,
            Graphics outilsGraphiques)
        {
            TraceLigne(false, LineCap.ArrowAnchor, regionAttaquante, regionAttaquee, belligerant, carte, outilsGraphiques);
        }

        /// <summary>
        /// Trace une flèche correspondant à une attaque ratée.
        /// </summary>
        /// <param name="regionAttaquante">La région d'où provient l'attaque.</param>
        /// <param name="regionAttaquee">La région où aboutit l'attaque.</param>
        /// <param name="belligerant">Identificateur du belligérant à l'origine de l'attaque.</param>
        /// <param name="carte">Image représentant la carte.</param>
        /// <param name="outilsGraphiques">Outils graphiques utilisés pour effectuer le dessin.</param>
        public static void TraceAttaqueRatee(
            GRegion regionAttaquante,
            GRegion regionAttaquee,
            EBelligerant belligerant,
            Image carte,
            Graphics outilsGraphiques)
        {
            TraceLigne(true, LineCap.ArrowAnchor, regionAttaquante, regionAttaquee, belligerant, carte, outilsGraphiques);
        }

        /// <summary>
        /// Trace une flèche correspondant à un soutien défensif.
        /// </summary>
        /// <param name="regionSoutenant">La région d'où est initié le soutien.</param>
        /// <param name="regionSoutenue">La région à laquelle profite le soutien.</param>
        /// <param name="belligerant">Identificateur du belligérant qui soutient.</param>
        /// <param name="carte">Image représentant la carte.</param>
        /// <param name="outilsGraphiques">Outils graphiques utilisés pour effectuer le dessin.</param>
        public static void TraceSoutienDefensif(
            GRegion regionSoutenant,
            GRegion regionSoutenue,
            EBelligerant belligerant,
            Image carte,
            Graphics outilsGraphiques)
        {
            TraceLigne(false, LineCap.SquareAnchor, regionSoutenant, regionSoutenue, belligerant, carte, outilsGraphiques);
        }

        /// <summary>
        /// Trace une flèche correspondant à un soutien défensif coupé.
        /// </summary>
        /// <param name="regionSoutenant">La région d'où est initié le soutien.</param>
        /// <param name="regionSoutenue">La région à laquelle profite le soutien.</param>
        /// <param name="belligerant">Identificateur du belligérant qui soutient.</param>
        /// <param name="carte">Image représentant la carte.</param>
        /// <param name="outilsGraphiques">Outils graphiques utilisés pour effectuer le dessin.</param>
        public static void TraceSoutienDefensifCoupe(
            GRegion regionSoutenant,
            GRegion regionSoutenue,
            EBelligerant belligerant,
            Image carte,
            Graphics outilsGraphiques)
        {
            TraceLigne(true, LineCap.SquareAnchor, regionSoutenant, regionSoutenue, belligerant, carte, outilsGraphiques);
        }

        /// <summary>
        /// Trace une flèche correspondant à un soutien offensif.
        /// </summary>
        /// <param name="regionSoutenant">La région d'où est initié le soutien.</param>
        /// <param name="regionSoutenue">La région à laquelle profite le soutien.</param>
        /// <param name="regionAttaquee">La région à laquelle nuit le soutien.</param>
        /// <param name="belligerant">Identificateur du belligérant qui soutient.</param>
        /// <param name="carte">Image représentant la carte.</param>
        /// <param name="outilsGraphiques">Outils graphiques utilisés pour effectuer le dessin.</param>
        public static void TraceSoutienOffensif(
            GRegion regionSoutenant,
            GRegion regionSoutenue,
            GRegion regionAttaquee,
            EBelligerant belligerant,
            Image carte,
            Graphics outilsGraphiques)
        {
            Point pointDepart = PlaceExtremiteFleches(regionSoutenant);
            Point pointArrivee = PlaceExtremiteFleches(regionSoutenue, regionAttaquee);
            TraceLigne(false, LineCap.SquareAnchor, pointDepart, pointArrivee, belligerant, carte, outilsGraphiques);
        }

        /// <summary>
        /// Trace une flèche correspondant à un soutien offensif coupé.
        /// </summary>
        /// <param name="regionSoutenant">La région d'où est initié le soutien.</param>
        /// <param name="regionSoutenue">La région à laquelle profite le soutien.</param>
        /// <param name="regionAttaquee">La région à laquelle nuit le soutien.</param>
        /// <param name="belligerant">Identificateur du belligérant qui soutient.</param>
        /// <param name="carte">Image représentant la carte.</param>
        /// <param name="outilsGraphiques">Outils graphiques utilisés pour effectuer le dessin.</param>
        public static void TraceSoutienOffensifCoupe(
            GRegion regionSoutenant,
            GRegion regionSoutenue,
            GRegion regionAttaquee,
            EBelligerant belligerant,
            Image carte,
            Graphics outilsGraphiques)
        {
            Point pointDepart = PlaceExtremiteFleches(regionSoutenant);
            Point pointArrivee = PlaceExtremiteFleches(regionSoutenue, regionAttaquee);
            TraceLigne(true, LineCap.SquareAnchor, pointDepart, pointArrivee, belligerant, carte, outilsGraphiques);
        }

        /// <summary>
        /// Trace une flèche correspondant à un transport.
        /// </summary>
        /// <param name="regionTransportant">La région où est effectué le transport.</param>
        /// <param name="regionTransportee">La région dont l'unité est transportée.</param>
        /// <param name="regionArrivee">La région où arrive l'unité transportée.</param>
        /// <param name="belligerant">Identificateur du belligérant qui effectue le transport.</param>
        /// <param name="carte">Image représentant la carte.</param>
        /// <param name="outilsGraphiques">Outils graphiques utilisés pour effectuer le dessin.</param>
        public static void TraceTransport(
            GRegion regionTransportant,
            GRegion regionTransportee,
            GRegion regionArrivee,
            EBelligerant belligerant,
            Image carte,
            Graphics outilsGraphiques)
        {
            Point pointDepart = PlaceExtremiteFleches(regionTransportant);
            Point pointArrivee = PlaceExtremiteFleches(regionTransportee, regionArrivee);
            TraceLigne(false, LineCap.RoundAnchor, pointDepart, pointArrivee, belligerant, carte, outilsGraphiques);
        }

        /// <summary>
        /// Trace une flèche correspondant à un transport interrompu.
        /// </summary>
        /// <param name="regionTransportant">La région où est effectué le transport.</param>
        /// <param name="regionTransportee">La région dont l'unité est transportée.</param>
        /// <param name="regionArrivee">La région où arrive l'unité transportée.</param>
        /// <param name="belligerant">Identificateur du belligérant qui effectue le transport.</param>
        /// <param name="carte">Image représentant la carte.</param>
        /// <param name="outilsGraphiques">Outils graphiques utilisés pour effectuer le dessin.</param>
        public static void TraceTransportInterrompu(
            GRegion regionTransportant,
            GRegion regionTransportee,
            GRegion regionArrivee,
            EBelligerant belligerant,
            Image carte,
            Graphics outilsGraphiques)
        {
            Point pointDepart = PlaceExtremiteFleches(regionTransportant);
            Point pointArrivee = PlaceExtremiteFleches(regionTransportee, regionArrivee);
            TraceLigne(true, LineCap.RoundAnchor, pointDepart, pointArrivee, belligerant, carte, outilsGraphiques);
        }

        #endregion

        #region Méthodes auxiliaires

        /// <summary>
        /// Fournit les coordonnées de l'extrémité d'une flèche dans la région donnée.
        /// </summary>
        /// <param name="region">Région dans laquelle placer l'extrémité d'une flèche.</param>
        /// <returns>Coordonnées de l'extrémité d'une flèche dans la région donnée.</returns>
        private static Point PlaceExtremiteFleches(GRegion region)
        {
            return PlaceExtremiteFleches(region, region);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="premiereRegion"></param>
        /// <param name="secondeRegion"></param>
        /// <returns></returns>
        private static Point PlaceExtremiteFleches(GRegion premiereRegion, GRegion secondeRegion)
        {
            /// L'image pour les flottes mesure 23x20 pixels, celle pour les armées 20x20 pixels.
            Int32 decalageHorizontal = 10;
            Int32 decalageVertical = 10;

            Point debut = new Point(
                premiereRegion.CoordonneesUnite.X + decalageHorizontal,
                premiereRegion.CoordonneesUnite.Y + decalageVertical);
            Point fin = new Point(
                secondeRegion.CoordonneesUnite.X + decalageHorizontal,
                secondeRegion.CoordonneesUnite.Y + decalageVertical);

            Point moyenne = new Point((debut.X + fin.X) / 2, (debut.Y + fin.Y) / 2);
            return moyenne;
        }

        /// <summary>
        /// Trace une ligne entre les deux régions indiquées.
        /// </summary>
        /// <param name="pointilles">Indique si la ligne doit être en pointillés ou non.</param>
        /// <param name="typeBout">Type de bout (flèche, carré) souhaité pour la ligne.</param>
        /// <param name="regionDebut">Région où débute le trait.</param>
        /// <param name="regionFin">Région où se termine le trait.</param>
        /// <param name="belligerant">Identificateur du belligérant associé à l'ordre.</param>
        /// <param name="carte">Image représentant la carte.</param>
        /// <param name="outilsGraphiques">Outils graphiques utilisés pour effectuer le dessin.</param>
        private static void TraceLigne(
            Boolean pointilles,
            LineCap typeBout,
            GRegion regionDebut,
            GRegion regionFin,
            EBelligerant belligerant,
            Image carte,
            Graphics outilsGraphiques)
        {
            Point pointDepart = PlaceExtremiteFleches(regionDebut);
            Point pointArrivee = PlaceExtremiteFleches(regionFin);

            TraceLigne(pointilles, typeBout, pointDepart, pointArrivee, belligerant, carte, outilsGraphiques);
        }

        /// <summary>
        /// Trace une ligne entre les deux régions indiquées.
        /// </summary>
        /// <param name="pointilles">Indique si la ligne doit être en pointillés ou non.</param>
        /// <param name="typeBout">Type de bout (flèche, carré) souhaité pour la ligne.</param>
        /// <param name="pointDepart">Point où débute le trait.</param>
        /// <param name="pointArrivee">Point où se termine le trait.</param>
        /// <param name="belligerant">Identificateur du belligérant associé à l'ordre.</param>
        /// <param name="carte">Image représentant la carte.</param>
        /// <param name="outilsGraphiques">Outils graphiques utilisés pour effectuer le dessin.</param>
        private static void TraceLigne(
            Boolean pointilles,
            LineCap typeBout,
            Point pointDepart,
            Point pointArrivee,
            EBelligerant belligerant,
            Image carte,
            Graphics outilsGraphiques)
        {
            Color couleur = Convertisseurs.VersCouleur(belligerant);
            Color couleurBis;
            if (belligerant == EBelligerant.Mélinde)
            {
                couleurBis = Color.Black;

            }
            else if (belligerant == EBelligerant.Palavin)
            {
                couleurBis = Color.Red;
            }
            else
            {
                couleurBis = couleur;
            }

            Pen traceur = new Pen(couleur, 2.0F);
            Pen traceurBis = new Pen(couleurBis, 3.0F);
            traceur.EndCap = typeBout;
            traceurBis.EndCap = typeBout;
            if (pointilles == true)
            {
                traceur.DashStyle = DashStyle.Dash;
                traceurBis.DashStyle = DashStyle.Dash;
            }

            outilsGraphiques.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            outilsGraphiques.DrawLine(traceurBis, pointDepart, pointArrivee);
            outilsGraphiques.DrawLine(traceur, pointDepart, pointArrivee);
        }

        #endregion
    }
}
