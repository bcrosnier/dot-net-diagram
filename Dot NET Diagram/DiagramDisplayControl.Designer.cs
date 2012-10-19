namespace Dot_NET_Diagram
{
    partial class DiagramDisplayControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._NShapeDisplay = new Dataweb.NShape.WinFormsUI.Display();
            this.SuspendLayout();
            // 
            // _NShapeDisplay
            // 
            this._NShapeDisplay.BackColorGradient = System.Drawing.SystemColors.Control;
            this._NShapeDisplay.DiagramSetController = null;
            this._NShapeDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this._NShapeDisplay.GridColor = System.Drawing.Color.Gainsboro;
            this._NShapeDisplay.GridSize = 19;
            this._NShapeDisplay.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._NShapeDisplay.Location = new System.Drawing.Point(0, 0);
            this._NShapeDisplay.Name = "_NShapeDisplay";
            this._NShapeDisplay.PropertyController = null;
            this._NShapeDisplay.SelectionHilightColor = System.Drawing.Color.Firebrick;
            this._NShapeDisplay.SelectionInactiveColor = System.Drawing.Color.Gray;
            this._NShapeDisplay.SelectionInteriorColor = System.Drawing.Color.WhiteSmoke;
            this._NShapeDisplay.SelectionNormalColor = System.Drawing.Color.DarkGreen;
            this._NShapeDisplay.Size = new System.Drawing.Size(150, 150);
            this._NShapeDisplay.TabIndex = 0;
            this._NShapeDisplay.ToolPreviewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(119)))), ((int)(((byte)(136)))), ((int)(((byte)(153)))));
            this._NShapeDisplay.ToolPreviewColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            // 
            // DiagramDisplayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._NShapeDisplay);
            this.Name = "DiagramDisplayControl";
            this.ResumeLayout(false);

        }

        #endregion

        private Dataweb.NShape.WinFormsUI.Display _NShapeDisplay;
    }
}
