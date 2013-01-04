namespace Dot_NET_Diagram
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
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._menuBar = new System.Windows.Forms.MenuStrip();
            this._fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawRelationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._statusBar = new System.Windows.Forms.StatusStrip();
            this._toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this._diagramDisplayControl = new Dot_NET_Diagram.DiagramDisplayControl();
            this._menuBar.SuspendLayout();
            this._statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuBar
            // 
            this._menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fileToolStripMenuItem,
            this.drawRelationsToolStripMenuItem});
            this._menuBar.Location = new System.Drawing.Point(0, 0);
            this._menuBar.Name = "_menuBar";
            this._menuBar.Size = new System.Drawing.Size(784, 24);
            this._menuBar.TabIndex = 0;
            // 
            // _fileToolStripMenuItem
            // 
            this._fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._openToolStripMenuItem,
            this.toolStripSeparator1,
            this._exitToolStripMenuItem});
            this._fileToolStripMenuItem.Name = "_fileToolStripMenuItem";
            this._fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this._fileToolStripMenuItem.Text = "&File";
            // 
            // _openToolStripMenuItem
            // 
            this._openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_openToolStripMenuItem.Image")));
            this._openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._openToolStripMenuItem.Name = "_openToolStripMenuItem";
            this._openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this._openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this._openToolStripMenuItem.Text = "&Open";
            this._openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // _exitToolStripMenuItem
            // 
            this._exitToolStripMenuItem.Name = "_exitToolStripMenuItem";
            this._exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this._exitToolStripMenuItem.Text = "E&xit";
            this._exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // drawRelationsToolStripMenuItem
            // 
            this.drawRelationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.drawRelationsToolStripMenuItem.Name = "drawRelationsToolStripMenuItem";
            this.drawRelationsToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.drawRelationsToolStripMenuItem.Text = "Relations";
            // 
            // drawToolStripMenuItem
            // 
            this.drawToolStripMenuItem.Name = "drawToolStripMenuItem";
            this.drawToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.drawToolStripMenuItem.Text = "Draw";
            this.drawToolStripMenuItem.Click += new System.EventHandler(this.drawToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // _statusBar
            // 
            this._statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripStatusLabel});
            this._statusBar.Location = new System.Drawing.Point(0, 540);
            this._statusBar.Name = "_statusBar";
            this._statusBar.Size = new System.Drawing.Size(784, 22);
            this._statusBar.TabIndex = 1;
            // 
            // _toolStripStatusLabel
            // 
            this._toolStripStatusLabel.Name = "_toolStripStatusLabel";
            this._toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // _openFileDialog
            // 
            this._openFileDialog.Filter = ".NET Assemblies|*.dll;*.exe";
            // 
            // _diagramDisplayControl
            // 
            this._diagramDisplayControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._diagramDisplayControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._diagramDisplayControl.Location = new System.Drawing.Point(0, 24);
            this._diagramDisplayControl.Name = "_diagramDisplayControl";
            this._diagramDisplayControl.Size = new System.Drawing.Size(784, 516);
            this._diagramDisplayControl.TabIndex = 2;
            this._diagramDisplayControl.Load += new System.EventHandler(this._diagramDisplayControl_Load);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this._diagramDisplayControl);
            this.Controls.Add(this._statusBar);
            this.Controls.Add(this._menuBar);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = ".NET Diagram";
            this._menuBar.ResumeLayout(false);
            this._menuBar.PerformLayout();
            this._statusBar.ResumeLayout(false);
            this._statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _menuBar;
        private System.Windows.Forms.ToolStripMenuItem _fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip _statusBar;
        private System.Windows.Forms.OpenFileDialog _openFileDialog;
        private System.Windows.Forms.ToolStripStatusLabel _toolStripStatusLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private DiagramDisplayControl _diagramDisplayControl;
        private System.Windows.Forms.ToolStripMenuItem drawRelationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;

    }
}

