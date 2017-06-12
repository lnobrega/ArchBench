namespace ArchBench.Server
{
    partial class PlugInsForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlugInsForm));
            this.mTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.mAppendButton = new System.Windows.Forms.Button();
            this.mRemoveButton = new System.Windows.Forms.Button();
            this.mCloseButton = new System.Windows.Forms.Button();
            this.mPlugInsListView = new System.Windows.Forms.ListView();
            this.mNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mVersionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mAuthorColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mDescriptionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mPlugInsImageList = new System.Windows.Forms.ImageList(this.components);
            this.mTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mTableLayoutPanel
            // 
            this.mTableLayoutPanel.ColumnCount = 4;
            this.mTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mTableLayoutPanel.Controls.Add(this.mAppendButton, 1, 1);
            this.mTableLayoutPanel.Controls.Add(this.mRemoveButton, 2, 1);
            this.mTableLayoutPanel.Controls.Add(this.mCloseButton, 3, 1);
            this.mTableLayoutPanel.Controls.Add(this.mPlugInsListView, 0, 0);
            this.mTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mTableLayoutPanel.Name = "mTableLayoutPanel";
            this.mTableLayoutPanel.RowCount = 2;
            this.mTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mTableLayoutPanel.Size = new System.Drawing.Size(1576, 636);
            this.mTableLayoutPanel.TabIndex = 0;
            // 
            // mAppendButton
            // 
            this.mAppendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.mAppendButton.AutoSize = true;
            this.mAppendButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mAppendButton.Image = ((System.Drawing.Image)(resources.GetObject("mAppendButton.Image")));
            this.mAppendButton.Location = new System.Drawing.Point(1201, 583);
            this.mAppendButton.MinimumSize = new System.Drawing.Size(120, 50);
            this.mAppendButton.Name = "mAppendButton";
            this.mAppendButton.Size = new System.Drawing.Size(120, 50);
            this.mAppendButton.TabIndex = 2;
            this.mAppendButton.Text = "Append";
            this.mAppendButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.mAppendButton.UseVisualStyleBackColor = true;
            this.mAppendButton.Click += new System.EventHandler(this.OnAppendPlugIn);
            // 
            // mRemoveButton
            // 
            this.mRemoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.mRemoveButton.AutoSize = true;
            this.mRemoveButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mRemoveButton.Image = ((System.Drawing.Image)(resources.GetObject("mRemoveButton.Image")));
            this.mRemoveButton.Location = new System.Drawing.Point(1327, 583);
            this.mRemoveButton.MinimumSize = new System.Drawing.Size(120, 50);
            this.mRemoveButton.Name = "mRemoveButton";
            this.mRemoveButton.Size = new System.Drawing.Size(120, 50);
            this.mRemoveButton.TabIndex = 3;
            this.mRemoveButton.Text = "Remove";
            this.mRemoveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.mRemoveButton.UseVisualStyleBackColor = true;
            this.mRemoveButton.Click += new System.EventHandler(this.OnRemovePlugIn);
            // 
            // mCloseButton
            // 
            this.mCloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.mCloseButton.AutoSize = true;
            this.mCloseButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mCloseButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mCloseButton.Location = new System.Drawing.Point(1453, 583);
            this.mCloseButton.MinimumSize = new System.Drawing.Size(120, 50);
            this.mCloseButton.Name = "mCloseButton";
            this.mCloseButton.Size = new System.Drawing.Size(120, 50);
            this.mCloseButton.TabIndex = 1;
            this.mCloseButton.Text = "Close";
            this.mCloseButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.mCloseButton.UseVisualStyleBackColor = true;
            // 
            // mPlugInsListView
            // 
            this.mPlugInsListView.CheckBoxes = true;
            this.mPlugInsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.mNameColumnHeader,
            this.mVersionColumnHeader,
            this.mAuthorColumnHeader,
            this.mDescriptionColumnHeader});
            this.mTableLayoutPanel.SetColumnSpan(this.mPlugInsListView, 4);
            this.mPlugInsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mPlugInsListView.FullRowSelect = true;
            this.mPlugInsListView.GridLines = true;
            this.mPlugInsListView.Location = new System.Drawing.Point(3, 3);
            this.mPlugInsListView.Name = "mPlugInsListView";
            this.mPlugInsListView.Size = new System.Drawing.Size(1570, 574);
            this.mPlugInsListView.SmallImageList = this.mPlugInsImageList;
            this.mPlugInsListView.TabIndex = 1;
            this.mPlugInsListView.UseCompatibleStateImageBehavior = false;
            this.mPlugInsListView.View = System.Windows.Forms.View.Details;
            this.mPlugInsListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.OnItemChecked);
            // 
            // mNameColumnHeader
            // 
            this.mNameColumnHeader.Text = "Name";
            this.mNameColumnHeader.Width = 200;
            // 
            // mVersionColumnHeader
            // 
            this.mVersionColumnHeader.Text = "Version";
            this.mVersionColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mVersionColumnHeader.Width = 100;
            // 
            // mAuthorColumnHeader
            // 
            this.mAuthorColumnHeader.Text = "Author";
            this.mAuthorColumnHeader.Width = 300;
            // 
            // mDescriptionColumnHeader
            // 
            this.mDescriptionColumnHeader.Text = "Description";
            this.mDescriptionColumnHeader.Width = 1024;
            // 
            // mPlugInsImageList
            // 
            this.mPlugInsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("mPlugInsImageList.ImageStream")));
            this.mPlugInsImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.mPlugInsImageList.Images.SetKeyName(0, "plugin");
            this.mPlugInsImageList.Images.SetKeyName(1, "plugin_disabled");
            // 
            // PlugInsForm
            // 
            this.AcceptButton = this.mCloseButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1576, 636);
            this.Controls.Add(this.mTableLayoutPanel);
            this.Name = "PlugInsForm";
            this.Text = "ArchBench Plug-ins";
            this.mTableLayoutPanel.ResumeLayout(false);
            this.mTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mTableLayoutPanel;
        private System.Windows.Forms.Button mAppendButton;
        private System.Windows.Forms.Button mRemoveButton;
        private System.Windows.Forms.Button mCloseButton;
        private System.Windows.Forms.ListView mPlugInsListView;
        private System.Windows.Forms.ColumnHeader mNameColumnHeader;
        private System.Windows.Forms.ColumnHeader mVersionColumnHeader;
        private System.Windows.Forms.ImageList mPlugInsImageList;
        private System.Windows.Forms.ColumnHeader mAuthorColumnHeader;
        private System.Windows.Forms.ColumnHeader mDescriptionColumnHeader;
    }
}