
namespace QPCR
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonPack = new System.Windows.Forms.Button();
            this.Plate = new System.Windows.Forms.PictureBox();
            this.ReplicatesView = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sample = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reagent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Replicates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlateText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Plate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReplicatesView)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonPack
            // 
            this.buttonPack.Location = new System.Drawing.Point(998, 656);
            this.buttonPack.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonPack.Name = "buttonPack";
            this.buttonPack.Size = new System.Drawing.Size(61, 28);
            this.buttonPack.TabIndex = 0;
            this.buttonPack.Text = "Pack";
            this.buttonPack.UseVisualStyleBackColor = true;
            this.buttonPack.Click += new System.EventHandler(this.buttonPack_Click);
            // 
            // Plate
            // 
            this.Plate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Plate.Location = new System.Drawing.Point(11, 9);
            this.Plate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Plate.Name = "Plate";
            this.Plate.Size = new System.Drawing.Size(600, 400);
            this.Plate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Plate.TabIndex = 1;
            this.Plate.TabStop = false;
            // 
            // ReplicatesView
            // 
            this.ReplicatesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ReplicatesView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.Sample,
            this.Reagent,
            this.Replicates});
            this.ReplicatesView.Location = new System.Drawing.Point(615, 10);
            this.ReplicatesView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ReplicatesView.Name = "ReplicatesView";
            this.ReplicatesView.RowHeadersWidth = 51;
            this.ReplicatesView.RowTemplate.Height = 24;
            this.ReplicatesView.Size = new System.Drawing.Size(444, 399);
            this.ReplicatesView.TabIndex = 2;
            // 
            // Index
            // 
            this.Index.HeaderText = "I";
            this.Index.MinimumWidth = 6;
            this.Index.Name = "Index";
            this.Index.Width = 30;
            // 
            // Sample
            // 
            this.Sample.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Sample.FillWeight = 135.4839F;
            this.Sample.HeaderText = "Sample";
            this.Sample.MinimumWidth = 6;
            this.Sample.Name = "Sample";
            // 
            // Reagent
            // 
            this.Reagent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Reagent.FillWeight = 64.51613F;
            this.Reagent.HeaderText = "Reagent";
            this.Reagent.MinimumWidth = 6;
            this.Reagent.Name = "Reagent";
            // 
            // Replicates
            // 
            this.Replicates.HeaderText = "R";
            this.Replicates.MinimumWidth = 6;
            this.Replicates.Name = "Replicates";
            this.Replicates.Width = 30;
            // 
            // PlateText
            // 
            this.PlateText.Location = new System.Drawing.Point(982, 616);
            this.PlateText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PlateText.Name = "PlateText";
            this.PlateText.Size = new System.Drawing.Size(81, 20);
            this.PlateText.TabIndex = 3;
            this.PlateText.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(855, 616);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Remder Plate:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(11, 425);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(600, 258);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1074, 695);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PlateText);
            this.Controls.Add(this.ReplicatesView);
            this.Controls.Add(this.Plate);
            this.Controls.Add(this.buttonPack);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Plate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReplicatesView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPack;
        private System.Windows.Forms.PictureBox Plate;
        private System.Windows.Forms.DataGridView ReplicatesView;
        private System.Windows.Forms.TextBox PlateText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sample;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reagent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Replicates;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}