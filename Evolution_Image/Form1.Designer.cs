namespace Evolution_Image
{
    partial class Form1
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
            this.Panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Genes_Textbox = new System.Windows.Forms.RichTextBox();
            this.Graph_Imagebox = new System.Windows.Forms.PictureBox();
            this.Evo_Imagebox = new System.Windows.Forms.PictureBox();
            this.Source_Imagebox = new System.Windows.Forms.PictureBox();
            this.Source_Image_Path = new System.Windows.Forms.OpenFileDialog();
            this.Panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Graph_Imagebox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Evo_Imagebox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Source_Imagebox)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Panel1.Controls.Add(this.button1);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(704, 100);
            this.Panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(27, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(175, 70);
            this.button1.TabIndex = 0;
            this.button1.Text = "Source Image";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Panel2.Controls.Add(this.Genes_Textbox);
            this.Panel2.Controls.Add(this.Graph_Imagebox);
            this.Panel2.Controls.Add(this.Evo_Imagebox);
            this.Panel2.Controls.Add(this.Source_Imagebox);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel2.Location = new System.Drawing.Point(0, 100);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(704, 331);
            this.Panel2.TabIndex = 1;
            // 
            // Genes_Textbox
            // 
            this.Genes_Textbox.Location = new System.Drawing.Point(27, 24);
            this.Genes_Textbox.Name = "Genes_Textbox";
            this.Genes_Textbox.ReadOnly = true;
            this.Genes_Textbox.Size = new System.Drawing.Size(175, 129);
            this.Genes_Textbox.TabIndex = 3;
            this.Genes_Textbox.Text = "";
            // 
            // Graph_Imagebox
            // 
            this.Graph_Imagebox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Graph_Imagebox.Location = new System.Drawing.Point(27, 159);
            this.Graph_Imagebox.Name = "Graph_Imagebox";
            this.Graph_Imagebox.Size = new System.Drawing.Size(176, 134);
            this.Graph_Imagebox.TabIndex = 2;
            this.Graph_Imagebox.TabStop = false;
            // 
            // Evo_Imagebox
            // 
            this.Evo_Imagebox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Evo_Imagebox.Location = new System.Drawing.Point(263, 24);
            this.Evo_Imagebox.Name = "Evo_Imagebox";
            this.Evo_Imagebox.Size = new System.Drawing.Size(176, 269);
            this.Evo_Imagebox.TabIndex = 1;
            this.Evo_Imagebox.TabStop = false;
            // 
            // Source_Imagebox
            // 
            this.Source_Imagebox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Source_Imagebox.Location = new System.Drawing.Point(499, 24);
            this.Source_Imagebox.Name = "Source_Imagebox";
            this.Source_Imagebox.Size = new System.Drawing.Size(176, 269);
            this.Source_Imagebox.TabIndex = 0;
            this.Source_Imagebox.TabStop = false;
            this.Source_Imagebox.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Source_Image_Path
            // 
            this.Source_Image_Path.FileName = "Source_Image";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 431);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Panel1);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Evolution of Images";
            this.Panel1.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Graph_Imagebox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Evo_Imagebox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Source_Imagebox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.PictureBox Graph_Imagebox;
        private System.Windows.Forms.PictureBox Evo_Imagebox;
        private System.Windows.Forms.PictureBox Source_Imagebox;
        private System.Windows.Forms.RichTextBox Genes_Textbox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog Source_Image_Path;
    }
}

