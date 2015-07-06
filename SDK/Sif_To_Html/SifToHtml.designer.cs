namespace SIF_XML_ToHTMLUtility
{
    partial class SifToHtml
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SifToHtml));
            this.btnselectpath = new System.Windows.Forms.Button();
            this.btnshowfolder = new System.Windows.Forms.Button();
            this.lblgivepath = new System.Windows.Forms.Label();
            this.btnProcessXML = new System.Windows.Forms.Button();
            this.lblSIF = new System.Windows.Forms.Label();
            this.browse = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.txtsif = new System.Windows.Forms.TextBox();
            this.lbldbmsg = new System.Windows.Forms.Label();
            this.btnclear = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnltop = new System.Windows.Forms.Panel();
            this.lblSIFtoXML = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.pnltop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnselectpath
            // 
            this.btnselectpath.BackColor = System.Drawing.Color.White;
            this.btnselectpath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnselectpath.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnselectpath.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnselectpath.FlatAppearance.BorderSize = 0;
            this.btnselectpath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnselectpath.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnselectpath.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnselectpath.Location = new System.Drawing.Point(288, 56);
            this.btnselectpath.Name = "btnselectpath";
            this.btnselectpath.Size = new System.Drawing.Size(59, 28);
            this.btnselectpath.TabIndex = 9;
            this.btnselectpath.Text = "Change";
            this.btnselectpath.UseVisualStyleBackColor = false;
            this.btnselectpath.Click += new System.EventHandler(this.btnselectpath_Click);
            // 
            // btnshowfolder
            // 
            this.btnshowfolder.BackColor = System.Drawing.Color.White;
            this.btnshowfolder.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnshowfolder.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnshowfolder.Location = new System.Drawing.Point(26, 141);
            this.btnshowfolder.Name = "btnshowfolder";
            this.btnshowfolder.Size = new System.Drawing.Size(87, 30);
            this.btnshowfolder.TabIndex = 12;
            this.btnshowfolder.Text = "Show Files";
            this.btnshowfolder.UseVisualStyleBackColor = false;
            this.btnshowfolder.Click += new System.EventHandler(this.btnshowfolder_Click);
            // 
            // lblgivepath
            // 
            this.lblgivepath.AutoSize = true;
            this.lblgivepath.BackColor = System.Drawing.Color.LightGray;
            this.lblgivepath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblgivepath.Location = new System.Drawing.Point(12, 63);
            this.lblgivepath.Name = "lblgivepath";
            this.lblgivepath.Size = new System.Drawing.Size(87, 14);
            this.lblgivepath.TabIndex = 7;
            this.lblgivepath.Text = "Default Path  :";
            // 
            // btnProcessXML
            // 
            this.btnProcessXML.BackColor = System.Drawing.Color.White;
            this.btnProcessXML.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnProcessXML.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnProcessXML.Location = new System.Drawing.Point(136, 96);
            this.btnProcessXML.Name = "btnProcessXML";
            this.btnProcessXML.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.btnProcessXML.Size = new System.Drawing.Size(87, 30);
            this.btnProcessXML.TabIndex = 1;
            this.btnProcessXML.Text = "Publish";
            this.btnProcessXML.UseVisualStyleBackColor = false;
            this.btnProcessXML.Click += new System.EventHandler(this.btnProcessXML_Click);
            // 
            // lblSIF
            // 
            this.lblSIF.AutoSize = true;
            this.lblSIF.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSIF.Location = new System.Drawing.Point(21, 25);
            this.lblSIF.Name = "lblSIF";
            this.lblSIF.Size = new System.Drawing.Size(78, 13);
            this.lblSIF.TabIndex = 7;
            this.lblSIF.Text = "Select SIF  :";
            // 
            // browse
            // 
            this.browse.BackColor = System.Drawing.Color.White;
            this.browse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.browse.FlatAppearance.BorderSize = 0;
            this.browse.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browse.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.browse.Location = new System.Drawing.Point(288, 17);
            this.browse.Name = "browse";
            this.browse.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.browse.Size = new System.Drawing.Size(59, 28);
            this.browse.TabIndex = 3;
            this.browse.Text = "Browse";
            this.browse.UseVisualStyleBackColor = false;
            this.browse.Click += new System.EventHandler(this.browse_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.txtPath);
            this.panel1.Controls.Add(this.txtsif);
            this.panel1.Controls.Add(this.lbldbmsg);
            this.panel1.Controls.Add(this.btnshowfolder);
            this.panel1.Controls.Add(this.btnclear);
            this.panel1.Controls.Add(this.btnselectpath);
            this.panel1.Controls.Add(this.btnProcessXML);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.lblgivepath);
            this.panel1.Controls.Add(this.lblSIF);
            this.panel1.Controls.Add(this.browse);
            this.panel1.Location = new System.Drawing.Point(18, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(361, 193);
            this.panel1.TabIndex = 22;
            // 
            // txtPath
            // 
            this.txtPath.AcceptsReturn = true;
            this.txtPath.Location = new System.Drawing.Point(109, 61);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(173, 20);
            this.txtPath.TabIndex = 41;
            // 
            // txtsif
            // 
            this.txtsif.Location = new System.Drawing.Point(109, 22);
            this.txtsif.Name = "txtsif";
            this.txtsif.ReadOnly = true;
            this.txtsif.Size = new System.Drawing.Size(173, 20);
            this.txtsif.TabIndex = 40;
            // 
            // lbldbmsg
            // 
            this.lbldbmsg.AutoSize = true;
            this.lbldbmsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldbmsg.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lbldbmsg.Location = new System.Drawing.Point(106, 172);
            this.lbldbmsg.Name = "lbldbmsg";
            this.lbldbmsg.Size = new System.Drawing.Size(0, 18);
            this.lbldbmsg.TabIndex = 21;
            // 
            // btnclear
            // 
            this.btnclear.BackColor = System.Drawing.Color.White;
            this.btnclear.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnclear.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnclear.Location = new System.Drawing.Point(242, 141);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(87, 30);
            this.btnclear.TabIndex = 39;
            this.btnclear.Text = "Clear";
            this.btnclear.UseVisualStyleBackColor = false;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnExit.Location = new System.Drawing.Point(136, 141);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(87, 30);
            this.btnExit.TabIndex = 38;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnltop
            // 
            this.pnltop.BackColor = System.Drawing.Color.Black;
            this.pnltop.Controls.Add(this.lblSIFtoXML);
            this.pnltop.Controls.Add(this.pictureBox3);
            this.pnltop.Location = new System.Drawing.Point(-1, -1);
            this.pnltop.Name = "pnltop";
            this.pnltop.Size = new System.Drawing.Size(405, 57);
            this.pnltop.TabIndex = 37;
            // 
            // lblSIFtoXML
            // 
            this.lblSIFtoXML.AutoSize = true;
            this.lblSIFtoXML.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSIFtoXML.ForeColor = System.Drawing.Color.White;
            this.lblSIFtoXML.Location = new System.Drawing.Point(165, 19);
            this.lblSIFtoXML.Name = "lblSIFtoXML";
            this.lblSIFtoXML.Size = new System.Drawing.Size(168, 17);
            this.lblSIFtoXML.TabIndex = 22;
            this.lblSIFtoXML.Text = "Beast Web Publisher";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.White;
            this.pictureBox3.Image = global::BEAST_WEB_PUBLISHER.Properties.Resources.Beastlogo;
            this.pictureBox3.Location = new System.Drawing.Point(20, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(112, 57);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 35;
            this.pictureBox3.TabStop = false;
            // 
            // SifToHtml
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(400, 301);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnltop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SifToHtml";
            this.Text = "The Beast Apps";
            this.Load += new System.EventHandler(this.NewSIFtoXML_Load_1);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnltop.ResumeLayout(false);
            this.pnltop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSIF;
        private System.Windows.Forms.Button browse;
        private System.Windows.Forms.Label lblgivepath;
        private System.Windows.Forms.Button btnselectpath;
        private System.Windows.Forms.Button btnshowfolder;
        private System.Windows.Forms.Button btnProcessXML;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnltop;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnclear;
        private System.Windows.Forms.Label lbldbmsg;
        private System.Windows.Forms.Label lblSIFtoXML;
        private System.Windows.Forms.TextBox txtsif;
        private System.Windows.Forms.TextBox txtPath;
    }
}