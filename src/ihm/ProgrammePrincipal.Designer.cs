namespace diplo.IHM
{
    partial class ProgrammePrincipal
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.accueilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chargerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sauvegarderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jeuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulerLeTourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.finirLeTourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.raffraîchirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageCarte = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.emplacementDate = new System.Windows.Forms.Label();
            this.emplacementPhase = new System.Windows.Forms.Label();
            this.labelCompteurLautrec = new System.Windows.Forms.Label();
            this.labelCompteurBarsanges = new System.Windows.Forms.Label();
            this.labelCompteurEmpire = new System.Windows.Forms.Label();
            this.labelCompteurMelinde = new System.Windows.Forms.Label();
            this.labelCompteurBremontree = new System.Windows.Forms.Label();
            this.labelCompteurGretz = new System.Windows.Forms.Label();
            this.labelCompteurPalavin = new System.Windows.Forms.Label();
            this.labelCompteurThymee = new System.Windows.Forms.Label();
            this.compteurLautrec = new System.Windows.Forms.Label();
            this.compteurBarsanges = new System.Windows.Forms.Label();
            this.compteurEmpire = new System.Windows.Forms.Label();
            this.compteurMelinde = new System.Windows.Forms.Label();
            this.compteurBremontree = new System.Windows.Forms.Label();
            this.compteurGretz = new System.Windows.Forms.Label();
            this.compteurPalavin = new System.Windows.Forms.Label();
            this.compteurThymee = new System.Windows.Forms.Label();
            this.metaChampOrdre = new diplo.Contrôles_auxiliaires.MetaChampOrdre();
            this.accepterSimulation = new System.Windows.Forms.Button();
            this.refuserSimulation = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCarte)).BeginInit();
            this.SuspendLayout();
            //
            // menuStrip
            //
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accueilToolStripMenuItem,
            this.jeuToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            //
            // accueilToolStripMenuItem
            //
            this.accueilToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creerToolStripMenuItem,
            this.chargerToolStripMenuItem,
            this.sauvegarderToolStripMenuItem});
            this.accueilToolStripMenuItem.Name = "accueilToolStripMenuItem";
            this.accueilToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.accueilToolStripMenuItem.Text = "Accueil";
            //
            // creerToolStripMenuItem
            //
            this.creerToolStripMenuItem.Name = "creerToolStripMenuItem";
            this.creerToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.creerToolStripMenuItem.Text = "Créer une nouvelle partie";
            this.creerToolStripMenuItem.Click += new System.EventHandler(this.creerToolStripMenuItem_Click);
            //
            // chargerToolStripMenuItem
            //
            this.chargerToolStripMenuItem.Name = "chargerToolStripMenuItem";
            this.chargerToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.chargerToolStripMenuItem.Text = "Charger une partie existante";
            this.chargerToolStripMenuItem.Click += new System.EventHandler(this.chargerToolStripMenuItem_Click);
            //
            // sauvegarderToolStripMenuItem
            //
            this.sauvegarderToolStripMenuItem.Name = "sauvegarderToolStripMenuItem";
            this.sauvegarderToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.sauvegarderToolStripMenuItem.Text = "Sauvegarder la partie en cours";
            this.sauvegarderToolStripMenuItem.Click += new System.EventHandler(this.sauvegarderToolStripMenuItem_Click);
            //
            // jeuToolStripMenuItem
            //
            this.jeuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simulerLeTourToolStripMenuItem,
            this.finirLeTourToolStripMenuItem,
            this.raffraîchirToolStripMenuItem});
            this.jeuToolStripMenuItem.Name = "jeuToolStripMenuItem";
            this.jeuToolStripMenuItem.Size = new System.Drawing.Size(36, 20);
            this.jeuToolStripMenuItem.Text = "Jeu";
            //
            // simulerLeTourToolStripMenuItem
            //
            this.simulerLeTourToolStripMenuItem.Name = "simulerLeTourToolStripMenuItem";
            this.simulerLeTourToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.simulerLeTourToolStripMenuItem.Text = "Simuler le tour";
            this.simulerLeTourToolStripMenuItem.Click += new System.EventHandler(this.simulerTour);
            //
            // finirLeTourToolStripMenuItem
            //
            this.finirLeTourToolStripMenuItem.Name = "finirLeTourToolStripMenuItem";
            this.finirLeTourToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.finirLeTourToolStripMenuItem.Text = "Finir le tour";
            this.finirLeTourToolStripMenuItem.Click += new System.EventHandler(this.finirTour);
            //
            // raffraîchirToolStripMenuItem
            //
            this.raffraîchirToolStripMenuItem.Name = "raffraîchirToolStripMenuItem";
            this.raffraîchirToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.raffraîchirToolStripMenuItem.Text = "Raffraîchir";
            this.raffraîchirToolStripMenuItem.Click += new System.EventHandler(this.raffraichit);
            //
            // exportToolStripMenuItem
            //
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportCarte_ToolStripExport);
            //
            // imageCarte
            //
            this.imageCarte.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageCarte.Location = new System.Drawing.Point(12, 45);
            this.imageCarte.Name = "imageCarte";
            this.imageCarte.Size = new System.Drawing.Size(858, 568);
            this.imageCarte.TabIndex = 1;
            this.imageCarte.TabStop = false;
            this.imageCarte.Paint += new System.Windows.Forms.PaintEventHandler(this.imageCarte_Paint);
            //
            // emplacementDate
            //
            this.emplacementDate.AutoSize = true;
            this.emplacementDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emplacementDate.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.emplacementDate.Location = new System.Drawing.Point(12, 26);
            this.emplacementDate.Name = "emplacementDate";
            this.emplacementDate.Size = new System.Drawing.Size(108, 13);
            this.emplacementDate.TabIndex = 2;
            this.emplacementDate.Text = "Texte pour repère";
            this.emplacementDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.emplacementDate.Visible = false;
            //
            // emplacementPhase
            //
            this.emplacementPhase.AutoSize = true;
            this.emplacementPhase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emplacementPhase.Location = new System.Drawing.Point(149, 26);
            this.emplacementPhase.Name = "emplacementPhase";
            this.emplacementPhase.Size = new System.Drawing.Size(39, 13);
            this.emplacementPhase.TabIndex = 3;
            this.emplacementPhase.Text = "Texte";
            this.emplacementPhase.Visible = false;
            //
            // labelCompteurLautrec
            //
            this.labelCompteurLautrec.AutoSize = true;
            this.labelCompteurLautrec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCompteurLautrec.Location = new System.Drawing.Point(9, 671);
            this.labelCompteurLautrec.Name = "labelCompteurLautrec";
            this.labelCompteurLautrec.Size = new System.Drawing.Size(124, 13);
            this.labelCompteurLautrec.TabIndex = 4;
            this.labelCompteurLautrec.Text = "Royaume de Lautrec";
            this.labelCompteurLautrec.Visible = false;
            //
            // labelCompteurBarsanges
            //
            this.labelCompteurBarsanges.AutoSize = true;
            this.labelCompteurBarsanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCompteurBarsanges.Location = new System.Drawing.Point(9, 694);
            this.labelCompteurBarsanges.Name = "labelCompteurBarsanges";
            this.labelCompteurBarsanges.Size = new System.Drawing.Size(125, 13);
            this.labelCompteurBarsanges.TabIndex = 5;
            this.labelCompteurBarsanges.Text = "Duché de Barsanges";
            this.labelCompteurBarsanges.Visible = false;
            //
            // labelCompteurEmpire
            //
            this.labelCompteurEmpire.AutoSize = true;
            this.labelCompteurEmpire.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCompteurEmpire.Location = new System.Drawing.Point(338, 671);
            this.labelCompteurEmpire.Name = "labelCompteurEmpire";
            this.labelCompteurEmpire.Size = new System.Drawing.Size(45, 13);
            this.labelCompteurEmpire.TabIndex = 6;
            this.labelCompteurEmpire.Text = "Empire";
            this.labelCompteurEmpire.Visible = false;
            //
            // labelCompteurMelinde
            //
            this.labelCompteurMelinde.AutoSize = true;
            this.labelCompteurMelinde.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCompteurMelinde.Location = new System.Drawing.Point(338, 694);
            this.labelCompteurMelinde.Name = "labelCompteurMelinde";
            this.labelCompteurMelinde.Size = new System.Drawing.Size(125, 13);
            this.labelCompteurMelinde.TabIndex = 7;
            this.labelCompteurMelinde.Text = "Royaume de Mélinde";
            this.labelCompteurMelinde.Visible = false;
            //
            // labelCompteurBremontree
            //
            this.labelCompteurBremontree.AutoSize = true;
            this.labelCompteurBremontree.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCompteurBremontree.Location = new System.Drawing.Point(9, 719);
            this.labelCompteurBremontree.Name = "labelCompteurBremontree";
            this.labelCompteurBremontree.Size = new System.Drawing.Size(157, 13);
            this.labelCompteurBremontree.TabIndex = 8;
            this.labelCompteurBremontree.Text = "République de Brémontrée";
            this.labelCompteurBremontree.Visible = false;
            //
            // labelCompteurGretz
            //
            this.labelCompteurGretz.AutoSize = true;
            this.labelCompteurGretz.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCompteurGretz.Location = new System.Drawing.Point(338, 719);
            this.labelCompteurGretz.Name = "labelCompteurGretz";
            this.labelCompteurGretz.Size = new System.Drawing.Size(175, 13);
            this.labelCompteurGretz.TabIndex = 9;
            this.labelCompteurGretz.Text = "Royaumes de Gretz et Vélage";
            this.labelCompteurGretz.Visible = false;
            //
            // labelCompteurPalavin
            //
            this.labelCompteurPalavin.AutoSize = true;
            this.labelCompteurPalavin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCompteurPalavin.Location = new System.Drawing.Point(715, 671);
            this.labelCompteurPalavin.Name = "labelCompteurPalavin";
            this.labelCompteurPalavin.Size = new System.Drawing.Size(123, 13);
            this.labelCompteurPalavin.TabIndex = 10;
            this.labelCompteurPalavin.Text = "Royaume de Palavin";
            this.labelCompteurPalavin.Visible = false;
            //
            // labelCompteurThymee
            //
            this.labelCompteurThymee.AutoSize = true;
            this.labelCompteurThymee.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCompteurThymee.Location = new System.Drawing.Point(715, 694);
            this.labelCompteurThymee.Name = "labelCompteurThymee";
            this.labelCompteurThymee.Size = new System.Drawing.Size(125, 13);
            this.labelCompteurThymee.TabIndex = 11;
            this.labelCompteurThymee.Text = "Royaume de Thymée";
            this.labelCompteurThymee.Visible = false;
            //
            // compteurLautrec
            //
            this.compteurLautrec.AutoSize = true;
            this.compteurLautrec.Location = new System.Drawing.Point(139, 671);
            this.compteurLautrec.Name = "compteurLautrec";
            this.compteurLautrec.Size = new System.Drawing.Size(35, 13);
            this.compteurLautrec.TabIndex = 12;
            this.compteurLautrec.Text = "label1";
            this.compteurLautrec.Visible = false;
            //
            // compteurBarsanges
            //
            this.compteurBarsanges.AutoSize = true;
            this.compteurBarsanges.Location = new System.Drawing.Point(139, 694);
            this.compteurBarsanges.Name = "compteurBarsanges";
            this.compteurBarsanges.Size = new System.Drawing.Size(35, 13);
            this.compteurBarsanges.TabIndex = 13;
            this.compteurBarsanges.Text = "label2";
            this.compteurBarsanges.Visible = false;
            //
            // compteurEmpire
            //
            this.compteurEmpire.AutoSize = true;
            this.compteurEmpire.Location = new System.Drawing.Point(389, 671);
            this.compteurEmpire.Name = "compteurEmpire";
            this.compteurEmpire.Size = new System.Drawing.Size(35, 13);
            this.compteurEmpire.TabIndex = 14;
            this.compteurEmpire.Text = "label3";
            this.compteurEmpire.Visible = false;
            //
            // compteurMelinde
            //
            this.compteurMelinde.AutoSize = true;
            this.compteurMelinde.Location = new System.Drawing.Point(469, 694);
            this.compteurMelinde.Name = "compteurMelinde";
            this.compteurMelinde.Size = new System.Drawing.Size(35, 13);
            this.compteurMelinde.TabIndex = 15;
            this.compteurMelinde.Text = "label4";
            this.compteurMelinde.Visible = false;
            //
            // compteurBremontree
            //
            this.compteurBremontree.AutoSize = true;
            this.compteurBremontree.Location = new System.Drawing.Point(171, 719);
            this.compteurBremontree.Name = "compteurBremontree";
            this.compteurBremontree.Size = new System.Drawing.Size(35, 13);
            this.compteurBremontree.TabIndex = 16;
            this.compteurBremontree.Text = "label5";
            this.compteurBremontree.Visible = false;
            //
            // compteurGretz
            //
            this.compteurGretz.AutoSize = true;
            this.compteurGretz.Location = new System.Drawing.Point(519, 719);
            this.compteurGretz.Name = "compteurGretz";
            this.compteurGretz.Size = new System.Drawing.Size(35, 13);
            this.compteurGretz.TabIndex = 17;
            this.compteurGretz.Text = "label6";
            this.compteurGretz.Visible = false;
            //
            // compteurPalavin
            //
            this.compteurPalavin.AutoSize = true;
            this.compteurPalavin.Location = new System.Drawing.Point(844, 671);
            this.compteurPalavin.Name = "compteurPalavin";
            this.compteurPalavin.Size = new System.Drawing.Size(35, 13);
            this.compteurPalavin.TabIndex = 18;
            this.compteurPalavin.Text = "label7";
            this.compteurPalavin.Visible = false;
            //
            // compteurThymee
            //
            this.compteurThymee.AutoSize = true;
            this.compteurThymee.Location = new System.Drawing.Point(846, 694);
            this.compteurThymee.Name = "compteurThymee";
            this.compteurThymee.Size = new System.Drawing.Size(35, 13);
            this.compteurThymee.TabIndex = 19;
            this.compteurThymee.Text = "label8";
            this.compteurThymee.Visible = false;
            //
            // metaChampOrdre
            //
            this.metaChampOrdre.ChampsOrdres = null;
            this.metaChampOrdre.Location = new System.Drawing.Point(876, 45);
            this.metaChampOrdre.Name = "metaChampOrdre";
            this.metaChampOrdre.TabIndex = 20;
            this.metaChampOrdre.Visible = false;
            //
            // accepterSimulation
            //
            this.accepterSimulation.Location = new System.Drawing.Point(936, 275);
            this.accepterSimulation.Name = "accepterSimulation";
            this.accepterSimulation.Size = new System.Drawing.Size(116, 52);
            this.accepterSimulation.TabIndex = 21;
            this.accepterSimulation.Text = "Accepter";
            this.accepterSimulation.UseVisualStyleBackColor = true;
            this.accepterSimulation.Visible = false;
            this.accepterSimulation.Click += new System.EventHandler(this.accepterSimulation_Click);
            //
            // refuserSimulation
            //
            this.refuserSimulation.Location = new System.Drawing.Point(936, 333);
            this.refuserSimulation.Name = "refuserSimulation";
            this.refuserSimulation.Size = new System.Drawing.Size(115, 57);
            this.refuserSimulation.TabIndex = 22;
            this.refuserSimulation.Text = "Reprogrammer";
            this.refuserSimulation.UseVisualStyleBackColor = true;
            this.refuserSimulation.Visible = false;
            this.refuserSimulation.Click += new System.EventHandler(this.refuserSimulation_Click);
            //
            // ProgrammePrincipal
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 762);
            this.Controls.Add(this.refuserSimulation);
            this.Controls.Add(this.accepterSimulation);
            this.Controls.Add(this.metaChampOrdre);
            this.Controls.Add(this.compteurThymee);
            this.Controls.Add(this.compteurPalavin);
            this.Controls.Add(this.compteurGretz);
            this.Controls.Add(this.compteurBremontree);
            this.Controls.Add(this.compteurMelinde);
            this.Controls.Add(this.compteurEmpire);
            this.Controls.Add(this.compteurBarsanges);
            this.Controls.Add(this.compteurLautrec);
            this.Controls.Add(this.labelCompteurThymee);
            this.Controls.Add(this.labelCompteurPalavin);
            this.Controls.Add(this.labelCompteurGretz);
            this.Controls.Add(this.labelCompteurBremontree);
            this.Controls.Add(this.labelCompteurMelinde);
            this.Controls.Add(this.labelCompteurEmpire);
            this.Controls.Add(this.labelCompteurBarsanges);
            this.Controls.Add(this.labelCompteurLautrec);
            this.Controls.Add(this.emplacementPhase);
            this.Controls.Add(this.emplacementDate);
            this.Controls.Add(this.imageCarte);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "ProgrammePrincipal";
            this.Text = "diplo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCarte)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem accueilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chargerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sauvegarderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jeuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem finirLeTourToolStripMenuItem;
        private System.Windows.Forms.PictureBox imageCarte;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem simulerLeTourToolStripMenuItem;
        private System.Windows.Forms.Label emplacementDate;
        private System.Windows.Forms.Label emplacementPhase;
        private System.Windows.Forms.Label labelCompteurLautrec;
        private System.Windows.Forms.Label labelCompteurBarsanges;
        private System.Windows.Forms.Label labelCompteurEmpire;
        private System.Windows.Forms.Label labelCompteurMelinde;
        private System.Windows.Forms.Label labelCompteurBremontree;
        private System.Windows.Forms.Label labelCompteurGretz;
        private System.Windows.Forms.Label labelCompteurPalavin;
        private System.Windows.Forms.Label labelCompteurThymee;
        private System.Windows.Forms.Label compteurLautrec;
        private System.Windows.Forms.Label compteurBarsanges;
        private System.Windows.Forms.Label compteurEmpire;
        private System.Windows.Forms.Label compteurMelinde;
        private System.Windows.Forms.Label compteurBremontree;
        private System.Windows.Forms.Label compteurGretz;
        private System.Windows.Forms.Label compteurPalavin;
        private System.Windows.Forms.Label compteurThymee;
        private Contrôles_auxiliaires.MetaChampOrdre metaChampOrdre;
        private System.Windows.Forms.ToolStripMenuItem raffraîchirToolStripMenuItem;
        private System.Windows.Forms.Button accepterSimulation;
        private System.Windows.Forms.Button refuserSimulation;
    }
}
