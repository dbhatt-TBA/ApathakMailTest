namespace SIF_XML_ToHTMLUtility
{
    partial class HtmlOps
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
            this.btnStartOperation = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.grpBxFolderSelect = new System.Windows.Forms.GroupBox();
            this.btnListOutExisting = new System.Windows.Forms.Button();
            this.btnLoadFiles = new System.Windows.Forms.Button();
            this.lblDstDir = new System.Windows.Forms.Label();
            this.lblSrcDir = new System.Windows.Forms.Label();
            this.btnSelectDest = new System.Windows.Forms.Button();
            this.btnSelectSrc = new System.Windows.Forms.Button();
            this.lstBxAddedFiles = new System.Windows.Forms.ListBox();
            this.btnProcessForClassAttrib = new System.Windows.Forms.Button();
            this.btnRemoveName = new System.Windows.Forms.Button();
            this.gpBoxOperations = new System.Windows.Forms.GroupBox();
            this.grpBxFolderSelect.SuspendLayout();
            this.gpBoxOperations.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartOperation
            // 
            this.btnStartOperation.Location = new System.Drawing.Point(6, 19);
            this.btnStartOperation.Name = "btnStartOperation";
            this.btnStartOperation.Size = new System.Drawing.Size(210, 32);
            this.btnStartOperation.TabIndex = 5;
            this.btnStartOperation.Text = "Insert NAME attr";
            this.btnStartOperation.UseVisualStyleBackColor = true;
            this.btnStartOperation.Click += new System.EventHandler(this.btnStartOperation_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(20, 133);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 13);
            this.lblMsg.TabIndex = 6;
            // 
            // grpBxFolderSelect
            // 
            this.grpBxFolderSelect.Controls.Add(this.btnListOutExisting);
            this.grpBxFolderSelect.Controls.Add(this.btnLoadFiles);
            this.grpBxFolderSelect.Controls.Add(this.lblDstDir);
            this.grpBxFolderSelect.Controls.Add(this.lblMsg);
            this.grpBxFolderSelect.Controls.Add(this.lblSrcDir);
            this.grpBxFolderSelect.Controls.Add(this.btnSelectDest);
            this.grpBxFolderSelect.Controls.Add(this.btnSelectSrc);
            this.grpBxFolderSelect.Location = new System.Drawing.Point(15, 13);
            this.grpBxFolderSelect.Name = "grpBxFolderSelect";
            this.grpBxFolderSelect.Size = new System.Drawing.Size(704, 164);
            this.grpBxFolderSelect.TabIndex = 8;
            this.grpBxFolderSelect.TabStop = false;
            this.grpBxFolderSelect.Text = "Select Files";
            // 
            // btnListOutExisting
            // 
            this.btnListOutExisting.Location = new System.Drawing.Point(209, 91);
            this.btnListOutExisting.Name = "btnListOutExisting";
            this.btnListOutExisting.Size = new System.Drawing.Size(136, 23);
            this.btnListOutExisting.TabIndex = 9;
            this.btnListOutExisting.Text = "List Existing Files";
            this.btnListOutExisting.UseVisualStyleBackColor = true;
            this.btnListOutExisting.Click += new System.EventHandler(this.btnListOutExisting_Click);
            // 
            // btnLoadFiles
            // 
            this.btnLoadFiles.Location = new System.Drawing.Point(13, 91);
            this.btnLoadFiles.Name = "btnLoadFiles";
            this.btnLoadFiles.Size = new System.Drawing.Size(156, 23);
            this.btnLoadFiles.TabIndex = 8;
            this.btnLoadFiles.Text = "Bring HTML Files";
            this.btnLoadFiles.UseVisualStyleBackColor = true;
            this.btnLoadFiles.Click += new System.EventHandler(this.btnLoadFiles_Click);
            // 
            // lblDstDir
            // 
            this.lblDstDir.AutoSize = true;
            this.lblDstDir.Location = new System.Drawing.Point(205, 54);
            this.lblDstDir.Name = "lblDstDir";
            this.lblDstDir.Size = new System.Drawing.Size(83, 13);
            this.lblDstDir.TabIndex = 5;
            this.lblDstDir.Text = "Destination...";
            // 
            // lblSrcDir
            // 
            this.lblSrcDir.AutoSize = true;
            this.lblSrcDir.Location = new System.Drawing.Point(205, 25);
            this.lblSrcDir.Name = "lblSrcDir";
            this.lblSrcDir.Size = new System.Drawing.Size(59, 13);
            this.lblSrcDir.TabIndex = 4;
            this.lblSrcDir.Text = "Source...";
            // 
            // btnSelectDest
            // 
            this.btnSelectDest.Location = new System.Drawing.Point(13, 49);
            this.btnSelectDest.Name = "btnSelectDest";
            this.btnSelectDest.Size = new System.Drawing.Size(174, 23);
            this.btnSelectDest.TabIndex = 1;
            this.btnSelectDest.Text = "Select Destination Folder";
            this.btnSelectDest.UseVisualStyleBackColor = true;
            this.btnSelectDest.Click += new System.EventHandler(this.btnSelectDest_Click);
            // 
            // btnSelectSrc
            // 
            this.btnSelectSrc.Location = new System.Drawing.Point(13, 20);
            this.btnSelectSrc.Name = "btnSelectSrc";
            this.btnSelectSrc.Size = new System.Drawing.Size(174, 23);
            this.btnSelectSrc.TabIndex = 0;
            this.btnSelectSrc.Text = "Select Source Folder";
            this.btnSelectSrc.UseVisualStyleBackColor = true;
            this.btnSelectSrc.Click += new System.EventHandler(this.btnSelectSrc_Click);
            // 
            // lstBxAddedFiles
            // 
            this.lstBxAddedFiles.FormattingEnabled = true;
            this.lstBxAddedFiles.Location = new System.Drawing.Point(28, 198);
            this.lstBxAddedFiles.Name = "lstBxAddedFiles";
            this.lstBxAddedFiles.Size = new System.Drawing.Size(250, 277);
            this.lstBxAddedFiles.TabIndex = 10;
            // 
            // btnProcessForClassAttrib
            // 
            this.btnProcessForClassAttrib.Location = new System.Drawing.Point(6, 57);
            this.btnProcessForClassAttrib.Name = "btnProcessForClassAttrib";
            this.btnProcessForClassAttrib.Size = new System.Drawing.Size(210, 32);
            this.btnProcessForClassAttrib.TabIndex = 11;
            this.btnProcessForClassAttrib.Text = "Remove numeric CLASS attr";
            this.btnProcessForClassAttrib.UseVisualStyleBackColor = true;
            this.btnProcessForClassAttrib.Click += new System.EventHandler(this.btnProcessForClassAttrib_Click);
            // 
            // btnRemoveName
            // 
            this.btnRemoveName.Location = new System.Drawing.Point(7, 95);
            this.btnRemoveName.Name = "btnRemoveName";
            this.btnRemoveName.Size = new System.Drawing.Size(209, 31);
            this.btnRemoveName.TabIndex = 12;
            this.btnRemoveName.Text = "Replace NAME to TITLE";
            this.btnRemoveName.UseVisualStyleBackColor = true;
            this.btnRemoveName.Click += new System.EventHandler(this.btnRemoveName_Click);
            // 
            // gpBoxOperations
            // 
            this.gpBoxOperations.Controls.Add(this.btnStartOperation);
            this.gpBoxOperations.Controls.Add(this.btnRemoveName);
            this.gpBoxOperations.Controls.Add(this.btnProcessForClassAttrib);
            this.gpBoxOperations.Location = new System.Drawing.Point(297, 183);
            this.gpBoxOperations.Name = "gpBoxOperations";
            this.gpBoxOperations.Size = new System.Drawing.Size(225, 292);
            this.gpBoxOperations.TabIndex = 13;
            this.gpBoxOperations.TabStop = false;
            this.gpBoxOperations.Text = "  Operations  ";
            // 
            // HtmlOps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 508);
            this.Controls.Add(this.gpBoxOperations);
            this.Controls.Add(this.lstBxAddedFiles);
            this.Controls.Add(this.grpBxFolderSelect);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "HtmlOps";
            this.Text = "HtmlOps";
            this.grpBxFolderSelect.ResumeLayout(false);
            this.grpBxFolderSelect.PerformLayout();
            this.gpBoxOperations.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartOperation;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.GroupBox grpBxFolderSelect;
        private System.Windows.Forms.Button btnSelectSrc;
        private System.Windows.Forms.Button btnSelectDest;
        private System.Windows.Forms.Button btnLoadFiles;
        private System.Windows.Forms.Label lblDstDir;
        private System.Windows.Forms.Label lblSrcDir;
        private System.Windows.Forms.ListBox lstBxAddedFiles;
        private System.Windows.Forms.Button btnListOutExisting;
        private System.Windows.Forms.Button btnProcessForClassAttrib;
        private System.Windows.Forms.Button btnRemoveName;
        private System.Windows.Forms.GroupBox gpBoxOperations;
    }
}