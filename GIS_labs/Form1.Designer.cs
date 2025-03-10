
namespace GIS_labs
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.importButton = new System.Windows.Forms.ToolStripButton();
            this.layerElements = new System.Windows.Forms.ListBox();
            this.buttonLayerAdd = new System.Windows.Forms.Button();
            this.buttonDeleteLayer = new System.Windows.Forms.Button();
            this.buttonLayerUp = new System.Windows.Forms.Button();
            this.buttonLayerDown = new System.Windows.Forms.Button();
            this.map = new GIS_labs.Classes.Map();
            this.layersList = new System.Windows.Forms.CheckedListBox();
            this.coordinatesLabel = new System.Windows.Forms.Label();
            this.bigObjectStatsLabel = new System.Windows.Forms.Label();
            this.buttonLayerZoom = new System.Windows.Forms.Button();
            this.buttonMapZoom = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(888, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // importButton
            // 
            this.importButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.importButton.Image = global::GIS_labs.Properties.Resources._360_F_593549119_JOuhPhytRMiZLlvAZ5WHhpoDsRGd0Kzi;
            this.importButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(23, 22);
            this.importButton.Text = "toolStripButton1";
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // layerElements
            // 
            this.layerElements.Dock = System.Windows.Forms.DockStyle.Left;
            this.layerElements.FormattingEnabled = true;
            this.layerElements.ItemHeight = 15;
            this.layerElements.Location = new System.Drawing.Point(225, 25);
            this.layerElements.Name = "layerElements";
            this.layerElements.Size = new System.Drawing.Size(130, 372);
            this.layerElements.TabIndex = 2;
            this.layerElements.SelectedIndexChanged += new System.EventHandler(this.layerElements_SelectedIndexChanged);
            // 
            // buttonLayerAdd
            // 
            this.buttonLayerAdd.Location = new System.Drawing.Point(44, 3);
            this.buttonLayerAdd.Name = "buttonLayerAdd";
            this.buttonLayerAdd.Size = new System.Drawing.Size(35, 23);
            this.buttonLayerAdd.TabIndex = 3;
            this.buttonLayerAdd.Text = "+L";
            this.buttonLayerAdd.UseVisualStyleBackColor = true;
            this.buttonLayerAdd.Click += new System.EventHandler(this.buttonLayerAdd_Click);
            // 
            // buttonDeleteLayer
            // 
            this.buttonDeleteLayer.Location = new System.Drawing.Point(44, 32);
            this.buttonDeleteLayer.Name = "buttonDeleteLayer";
            this.buttonDeleteLayer.Size = new System.Drawing.Size(35, 23);
            this.buttonDeleteLayer.TabIndex = 4;
            this.buttonDeleteLayer.Text = "-L";
            this.buttonDeleteLayer.UseVisualStyleBackColor = true;
            this.buttonDeleteLayer.Click += new System.EventHandler(this.buttonDeleteLayer_Click);
            // 
            // buttonLayerUp
            // 
            this.buttonLayerUp.Location = new System.Drawing.Point(3, 32);
            this.buttonLayerUp.Name = "buttonLayerUp";
            this.buttonLayerUp.Size = new System.Drawing.Size(35, 23);
            this.buttonLayerUp.TabIndex = 5;
            this.buttonLayerUp.Text = "vL";
            this.buttonLayerUp.UseVisualStyleBackColor = true;
            this.buttonLayerUp.Click += new System.EventHandler(this.buttonLayerUp_Click);
            // 
            // buttonLayerDown
            // 
            this.buttonLayerDown.Location = new System.Drawing.Point(3, 3);
            this.buttonLayerDown.Name = "buttonLayerDown";
            this.buttonLayerDown.Size = new System.Drawing.Size(35, 23);
            this.buttonLayerDown.TabIndex = 6;
            this.buttonLayerDown.Text = "^L";
            this.buttonLayerDown.UseVisualStyleBackColor = true;
            this.buttonLayerDown.Click += new System.EventHandler(this.buttonLayerDown_Click);
            // 
            // map
            // 
            this.map.BackColor = System.Drawing.SystemColors.Window;
            this.map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.map.ForeColor = System.Drawing.SystemColors.ControlText;
            this.map.Location = new System.Drawing.Point(355, 25);
            this.map.MapScale = 1D;
            this.map.Name = "map";
            this.map.Size = new System.Drawing.Size(533, 372);
            this.map.TabIndex = 7;
            this.map.Paint += new System.Windows.Forms.PaintEventHandler(this.map_Paint);
            this.map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.map_MouseDown);
            this.map.MouseMove += new System.Windows.Forms.MouseEventHandler(this.map_MouseMove);
            this.map.MouseUp += new System.Windows.Forms.MouseEventHandler(this.map_MouseUp);
            // 
            // layersList
            // 
            this.layersList.Dock = System.Windows.Forms.DockStyle.Left;
            this.layersList.FormattingEnabled = true;
            this.layersList.Location = new System.Drawing.Point(0, 25);
            this.layersList.Name = "layersList";
            this.layersList.Size = new System.Drawing.Size(225, 372);
            this.layersList.TabIndex = 8;
            this.layersList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.layersList_ItemCheck);
            this.layersList.SelectedIndexChanged += new System.EventHandler(this.layersList_SelectedIndexChanged);
            // 
            // coordinatesLabel
            // 
            this.coordinatesLabel.AutoSize = true;
            this.coordinatesLabel.Location = new System.Drawing.Point(225, 7);
            this.coordinatesLabel.Name = "coordinatesLabel";
            this.coordinatesLabel.Size = new System.Drawing.Size(101, 15);
            this.coordinatesLabel.TabIndex = 9;
            this.coordinatesLabel.Text = "(X: none, Y: none)";
            // 
            // bigObjectStatsLabel
            // 
            this.bigObjectStatsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bigObjectStatsLabel.AutoSize = true;
            this.bigObjectStatsLabel.Location = new System.Drawing.Point(225, 34);
            this.bigObjectStatsLabel.Name = "bigObjectStatsLabel";
            this.bigObjectStatsLabel.Size = new System.Drawing.Size(160, 15);
            this.bigObjectStatsLabel.TabIndex = 10;
            this.bigObjectStatsLabel.Text = "Периметр/Площадь/Длина";
            // 
            // buttonLayerZoom
            // 
            this.buttonLayerZoom.Location = new System.Drawing.Point(85, 3);
            this.buttonLayerZoom.Name = "buttonLayerZoom";
            this.buttonLayerZoom.Size = new System.Drawing.Size(35, 23);
            this.buttonLayerZoom.TabIndex = 11;
            this.buttonLayerZoom.Text = "=L";
            this.buttonLayerZoom.UseVisualStyleBackColor = true;
            this.buttonLayerZoom.Click += new System.EventHandler(this.buttonLayerZoom_Click);
            // 
            // buttonMapZoom
            // 
            this.buttonMapZoom.Location = new System.Drawing.Point(85, 32);
            this.buttonMapZoom.Name = "buttonMapZoom";
            this.buttonMapZoom.Size = new System.Drawing.Size(35, 23);
            this.buttonMapZoom.TabIndex = 12;
            this.buttonMapZoom.Text = "=M";
            this.buttonMapZoom.UseVisualStyleBackColor = true;
            this.buttonMapZoom.Click += new System.EventHandler(this.buttonMapZoom_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 397);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(888, 58);
            this.panel1.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.coordinatesLabel);
            this.panel2.Controls.Add(this.bigObjectStatsLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(888, 58);
            this.panel2.TabIndex = 13;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonMapZoom);
            this.panel3.Controls.Add(this.buttonLayerDown);
            this.panel3.Controls.Add(this.buttonLayerZoom);
            this.panel3.Controls.Add(this.buttonLayerUp);
            this.panel3.Controls.Add(this.buttonDeleteLayer);
            this.panel3.Controls.Add(this.buttonLayerAdd);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(225, 58);
            this.panel3.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 455);
            this.Controls.Add(this.map);
            this.Controls.Add(this.layerElements);
            this.Controls.Add(this.layersList);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "MiniMap GIS";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ListBox layerElements;
        private System.Windows.Forms.Button buttonLayerAdd;
        private System.Windows.Forms.Button buttonDeleteLayer;
        private System.Windows.Forms.Button buttonLayerUp;
        private System.Windows.Forms.Button buttonLayerDown;
        private Classes.Map map;
        private System.Windows.Forms.CheckedListBox layersList;
        private System.Windows.Forms.Label coordinatesLabel;
        private System.Windows.Forms.Label bigObjectStatsLabel;
        private System.Windows.Forms.ToolStripButton importButton;
        private System.Windows.Forms.Button buttonLayerZoom;
        private System.Windows.Forms.Button buttonMapZoom;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
    }
}

