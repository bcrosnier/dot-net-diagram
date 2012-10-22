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
            Dataweb.NShape.RoleBasedSecurityManager roleBasedSecurityManager4 = new Dataweb.NShape.RoleBasedSecurityManager();
            this._NShapeDisplay = new Dataweb.NShape.WinFormsUI.Display();
            this._DiagramSetController = new Dataweb.NShape.Controllers.DiagramSetController();
            this._NShapeProject = new Dataweb.NShape.Project(this.components);
            this._CachedRepository = new Dataweb.NShape.Advanced.CachedRepository();
            this._xmlStore = new Dataweb.NShape.XmlStore();
            this.SuspendLayout();
            // 
            // _NShapeDisplay
            // 
            this._NShapeDisplay.AllowDrop = true;
            this._NShapeDisplay.BackColorGradient = System.Drawing.SystemColors.Control;
            this._NShapeDisplay.DiagramSetController = this._DiagramSetController;
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
            this._NShapeDisplay.Size = new System.Drawing.Size(500, 500);
            this._NShapeDisplay.TabIndex = 0;
            this._NShapeDisplay.ToolPreviewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(119)))), ((int)(((byte)(136)))), ((int)(((byte)(153)))));
            this._NShapeDisplay.ToolPreviewColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
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
            roleBasedSecurityManager4.CurrentRole = Dataweb.NShape.StandardRole.Administrator;
            roleBasedSecurityManager4.CurrentRoleName = "Administrator";
            this._NShapeProject.SecurityManager = roleBasedSecurityManager4;
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
            // DiagramDisplayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this._NShapeDisplay);
            this.Name = "DiagramDisplayControl";
            this.Size = new System.Drawing.Size(500, 500);
            this.ResumeLayout(false);

        }

        #endregion

        private Dataweb.NShape.WinFormsUI.Display _NShapeDisplay;
        private Dataweb.NShape.Project _NShapeProject;
        private Dataweb.NShape.Controllers.DiagramSetController _DiagramSetController;
        private Dataweb.NShape.Advanced.CachedRepository _CachedRepository;
        private Dataweb.NShape.XmlStore _xmlStore;
    }
}
