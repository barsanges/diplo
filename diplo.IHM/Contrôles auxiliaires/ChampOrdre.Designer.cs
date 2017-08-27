namespace diplo.Contrôles_auxiliaires
{
    partial class ChampOrdre
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

        #region Code généré par le Concepteur de composants

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.Belligerant = new System.Windows.Forms.Label();
            this.Unite = new System.Windows.Forms.Label();
            this.Ordre = new System.Windows.Forms.TextBox();
            this.region = new System.Windows.Forms.Label();
            this.emplacementDrapeau = new System.Windows.Forms.PictureBox();
            this.comboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.emplacementDrapeau)).BeginInit();
            this.SuspendLayout();
            //
            // Belligerant
            //
            this.Belligerant.AutoSize = true;
            this.Belligerant.Location = new System.Drawing.Point(24, 6);
            this.Belligerant.Name = "Belligerant";
            this.Belligerant.Size = new System.Drawing.Size(27, 13);
            this.Belligerant.TabIndex = 0;
            this.Belligerant.Text = "Bell.";
            this.Belligerant.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // Unite
            //
            this.Unite.AutoSize = true;
            this.Unite.Location = new System.Drawing.Point(57, 6);
            this.Unite.Name = "Unite";
            this.Unite.Size = new System.Drawing.Size(14, 13);
            this.Unite.TabIndex = 1;
            this.Unite.Text = "A";
            this.Unite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // Ordre
            //
            this.Ordre.Location = new System.Drawing.Point(113, 3);
            this.Ordre.Name = "Ordre";
            this.Ordre.Size = new System.Drawing.Size(75, 20);
            this.Ordre.TabIndex = 2;
            //
            // region
            //
            this.region.AutoSize = true;
            this.region.Location = new System.Drawing.Point(77, 6);
            this.region.Name = "region";
            this.region.Size = new System.Drawing.Size(30, 13);
            this.region.TabIndex = 3;
            this.region.Text = "REG";
            this.region.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // emplacementDrapeau
            //
            this.emplacementDrapeau.Location = new System.Drawing.Point(8, 6);
            this.emplacementDrapeau.Name = "emplacementDrapeau";
            this.emplacementDrapeau.Size = new System.Drawing.Size(13, 13);
            this.emplacementDrapeau.TabIndex = 4;
            this.emplacementDrapeau.TabStop = false;
            this.emplacementDrapeau.Visible = false;
            this.emplacementDrapeau.Paint += new System.Windows.Forms.PaintEventHandler(this.emplacementDrapeau_Paint);
            //
            // comboBox
            //
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Items.AddRange(new object[] {
            "",
            "Armée",
            "Flotte"});
            this.comboBox.Location = new System.Drawing.Point(94, 3);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(94, 21);
            this.comboBox.TabIndex = 5;
            this.comboBox.Visible = false;
            //
            // ChampOrdre
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.emplacementDrapeau);
            this.Controls.Add(this.region);
            this.Controls.Add(this.Ordre);
            this.Controls.Add(this.Unite);
            this.Controls.Add(this.Belligerant);
            this.Name = "ChampOrdre";
            this.Size = new System.Drawing.Size(191, 26);
            this.Load += new System.EventHandler(this.ChampOrdre_Load);
            ((System.ComponentModel.ISupportInitialize)(this.emplacementDrapeau)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Belligerant;
        private System.Windows.Forms.Label Unite;
        private System.Windows.Forms.TextBox Ordre;
        private System.Windows.Forms.Label region;
        private System.Windows.Forms.PictureBox emplacementDrapeau;
        private System.Windows.Forms.ComboBox comboBox;
    }
}
