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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiagramDisplayControl));
            Dataweb.NShape.RoleBasedSecurityManager roleBasedSecurityManager1 = new Dataweb.NShape.RoleBasedSecurityManager();
            this._DiagramSetController = new Dataweb.NShape.Controllers.DiagramSetController();
            this._NShapeProject = new Dataweb.NShape.Project(this.components);
            this._CachedRepository = new Dataweb.NShape.Advanced.CachedRepository();
            this._xmlStore = new Dataweb.NShape.XmlStore();
            this._propertyController = new Dataweb.NShape.Controllers.PropertyController();
            this._toolSetController = new Dataweb.NShape.Controllers.ToolSetController();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._NShapeDisplay = new Dataweb.NShape.WinFormsUI.Display();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _DiagramSetController
            // 
            this._DiagramSetController.ActiveTool = null;
            this._DiagramSetController.Project = this._NShapeProject;
            // 
            // _NShapeProject
            // 
            this._NShapeProject.LibrarySearchPaths = ((System.Collections.Generic.IList<string>)(resources.GetObject("_NShapeProject.LibrarySearchPaths")));
            this._NShapeProject.Name = null;
            this._NShapeProject.Repository = this._CachedRepository;
            roleBasedSecurityManager1.CurrentRole = Dataweb.NShape.StandardRole.Administrator;
            roleBasedSecurityManager1.CurrentRoleName = "Administrator";
            this._NShapeProject.SecurityManager = roleBasedSecurityManager1;
            // 
            // _CachedRepository
            // 
            this._CachedRepository.ProjectName = null;
            this._CachedRepository.Store = this._xmlStore;
            this._CachedRepository.Version = 0;
            // 
            // _xmlStore
            // 
            this._xmlStore.DesignFileName = "";
            this._xmlStore.DirectoryName = ".";
            this._xmlStore.FileExtension = ".xml";
            this._xmlStore.ProjectName = "";
            // 
            // _propertyController
            // 
            this._propertyController.Project = this._NShapeProject;
            this._propertyController.PropertyDisplayMode = Dataweb.NShape.Controllers.NonEditableDisplayMode.Default;
            // 
            // _toolSetController
            // 
            this._toolSetController.DiagramSetController = this._DiagramSetController;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._NShapeDisplay);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Size = new System.Drawing.Size(500, 500);
            this.splitContainer1.SplitterDistance = 393;
            this.splitContainer1.TabIndex = 0;
            // 
            // _NShapeDisplay
            // 
            this._NShapeDisplay.AllowDrop = true;
            this._NShapeDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._NShapeDisplay.BackColorGradient = System.Drawing.SystemColors.Control;
            this._NShapeDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._NShapeDisplay.DiagramSetController = this._DiagramSetController;
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
            this._NShapeDisplay.Size = new System.Drawing.Size(393, 500);
            this._NShapeDisplay.SnapToGrid = false;
            this._NShapeDisplay.TabIndex = 1;
            this._NShapeDisplay.ToolPreviewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(119)))), ((int)(((byte)(136)))), ((int)(((byte)(153)))));
            this._NShapeDisplay.ToolPreviewColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this._NShapeDisplay.ShapeClick += new System.EventHandler<Dataweb.NShape.Controllers.DiagramPresenterShapeClickEventArgs>(this._NShapeDisplay_ShapeClick);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(97, 494);
            this.textBox1.TabIndex = 0;
            // 
            // DiagramDisplayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.splitContainer1);
            this.Name = "DiagramDisplayControl";
            this.Size = new System.Drawing.Size(500, 500);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Dataweb.NShape.Project _NShapeProject;
        private Dataweb.NShape.Controllers.DiagramSetController _DiagramSetController;
        private Dataweb.NShape.Advanced.CachedRepository _CachedRepository;
        private Dataweb.NShape.XmlStore _xmlStore;
        private Dataweb.NShape.Controllers.PropertyController _propertyController;
        private Dataweb.NShape.Controllers.ToolSetController _toolSetController;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBox1;
        private Dataweb.NShape.WinFormsUI.Display _NShapeDisplay;
    }
}
