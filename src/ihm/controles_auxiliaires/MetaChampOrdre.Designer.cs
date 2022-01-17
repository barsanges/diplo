namespace diplo.Contrôles_auxiliaires
{
    partial class MetaChampOrdre
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
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.barreDrapeaux = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.barreDrapeaux)).BeginInit();
            this.SuspendLayout();
            //
            // vScrollBar
            //
            this.vScrollBar.LargeChange = 20;
            this.vScrollBar.Location = new System.Drawing.Point(222, 6);
            this.vScrollBar.Maximum = 300;
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(18, 570);
            this.vScrollBar.SmallChange = 20;
            this.vScrollBar.TabIndex = 0;
            this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
            //
            // barreDrapeaux
            //
            this.barreDrapeaux.Location = new System.Drawing.Point(2, 6);
            this.barreDrapeaux.Name = "barreDrapeaux";
            this.barreDrapeaux.Size = new System.Drawing.Size(7, 694);
            this.barreDrapeaux.TabIndex = 1;
            this.barreDrapeaux.TabStop = false;
            //
            // MetaChampOrdre
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barreDrapeaux);
            this.Controls.Add(this.vScrollBar);
            this.Name = "MetaChampOrdre";
            this.Size = new System.Drawing.Size(240, 700);
            ((System.ComponentModel.ISupportInitialize)(this.barreDrapeaux)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.PictureBox barreDrapeaux;
    }
}
