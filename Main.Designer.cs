namespace Whatsapp_Sender_Mini_Edition {
    partial class Main {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.clientsList = new System.Windows.Forms.DataGridView();
            this.imagesList = new System.Windows.Forms.DataGridView();
            this.btnUploadImages = new System.Windows.Forms.Button();
            this.status = new System.Windows.Forms.StatusStrip();
            this.lblStatusTitle = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.endWsVendor = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnOpenVcard = new System.Windows.Forms.Button();
            this.btnRunSenderVendor = new System.Windows.Forms.Button();
            this.messageBox = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblTitleMessage = new System.Windows.Forms.Label();
            this.btnDeleteImages = new System.Windows.Forms.Button();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.systemMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.installComponents = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.clientsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagesList)).BeginInit();
            this.status.SuspendLayout();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // clientsList
            // 
            this.clientsList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.clientsList.BackgroundColor = System.Drawing.Color.White;
            this.clientsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.clientsList.Location = new System.Drawing.Point(12, 39);
            this.clientsList.Name = "clientsList";
            this.clientsList.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.clientsList.Size = new System.Drawing.Size(430, 506);
            this.clientsList.TabIndex = 0;
            // 
            // imagesList
            // 
            this.imagesList.AllowUserToAddRows = false;
            this.imagesList.AllowUserToDeleteRows = false;
            this.imagesList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.imagesList.BackgroundColor = System.Drawing.Color.White;
            this.imagesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.imagesList.Location = new System.Drawing.Point(448, 39);
            this.imagesList.Name = "imagesList";
            this.imagesList.ReadOnly = true;
            this.imagesList.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.imagesList.Size = new System.Drawing.Size(370, 506);
            this.imagesList.TabIndex = 1;
            // 
            // btnUploadImages
            // 
            this.btnUploadImages.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnUploadImages.ForeColor = System.Drawing.Color.DarkCyan;
            this.btnUploadImages.Location = new System.Drawing.Point(981, 95);
            this.btnUploadImages.Name = "btnUploadImages";
            this.btnUploadImages.Size = new System.Drawing.Size(146, 50);
            this.btnUploadImages.TabIndex = 3;
            this.btnUploadImages.Text = "تعيين صور جديدة";
            this.btnUploadImages.UseVisualStyleBackColor = true;
            this.btnUploadImages.Click += new System.EventHandler(this.btnUploadImages_Click);
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusTitle,
            this.lblStatus,
            this.endWsVendor,
            this.statusLabel});
            this.status.Location = new System.Drawing.Point(0, 562);
            this.status.Name = "status";
            this.status.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.status.Size = new System.Drawing.Size(1139, 22);
            this.status.TabIndex = 5;
            this.status.Text = "statusStrip1";
            // 
            // lblStatusTitle
            // 
            this.lblStatusTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatusTitle.Name = "lblStatusTitle";
            this.lblStatusTitle.Size = new System.Drawing.Size(72, 17);
            this.lblStatusTitle.Text = "مزود الرسائل:";
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.Color.IndianRed;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(43, 17);
            this.lblStatus.Text = "لا يعمل";
            // 
            // endWsVendor
            // 
            this.endWsVendor.IsLink = true;
            this.endWsVendor.Name = "endWsVendor";
            this.endWsVendor.Size = new System.Drawing.Size(101, 17);
            this.endWsVendor.Text = "إعادة تشغيل المزود";
            this.endWsVendor.Visible = false;
            this.endWsVendor.Click += new System.EventHandler(this.endWsVendor_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusLabel.Size = new System.Drawing.Size(877, 17);
            this.statusLabel.Spring = true;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOpenVcard
            // 
            this.btnOpenVcard.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnOpenVcard.ForeColor = System.Drawing.Color.Black;
            this.btnOpenVcard.Location = new System.Drawing.Point(824, 39);
            this.btnOpenVcard.Name = "btnOpenVcard";
            this.btnOpenVcard.Size = new System.Drawing.Size(303, 50);
            this.btnOpenVcard.TabIndex = 8;
            this.btnOpenVcard.Text = " (.VCF ) تحميل جهات اتصال";
            this.btnOpenVcard.UseVisualStyleBackColor = true;
            this.btnOpenVcard.Click += new System.EventHandler(this.btnOpenVcard_Click);
            // 
            // btnRunSenderVendor
            // 
            this.btnRunSenderVendor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRunSenderVendor.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnRunSenderVendor.Location = new System.Drawing.Point(824, 495);
            this.btnRunSenderVendor.Name = "btnRunSenderVendor";
            this.btnRunSenderVendor.Size = new System.Drawing.Size(303, 50);
            this.btnRunSenderVendor.TabIndex = 4;
            this.btnRunSenderVendor.Text = "تشغيل مزود الرسائل";
            this.btnRunSenderVendor.UseVisualStyleBackColor = true;
            this.btnRunSenderVendor.Click += new System.EventHandler(this.btnRunSenderVendor_Click);
            // 
            // messageBox
            // 
            this.messageBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.messageBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.messageBox.Location = new System.Drawing.Point(824, 195);
            this.messageBox.Name = "messageBox";
            this.messageBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.messageBox.Size = new System.Drawing.Size(303, 294);
            this.messageBox.TabIndex = 9;
            this.messageBox.Text = "";
            this.messageBox.TextChanged += new System.EventHandler(this.messageBox_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // lblTitleMessage
            // 
            this.lblTitleMessage.AutoSize = true;
            this.lblTitleMessage.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitleMessage.Location = new System.Drawing.Point(1010, 163);
            this.lblTitleMessage.Name = "lblTitleMessage";
            this.lblTitleMessage.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTitleMessage.Size = new System.Drawing.Size(121, 20);
            this.lblTitleMessage.TabIndex = 11;
            this.lblTitleMessage.Text = "ادخل نص الرسالة:";
            // 
            // btnDeleteImages
            // 
            this.btnDeleteImages.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnDeleteImages.ForeColor = System.Drawing.Color.Crimson;
            this.btnDeleteImages.Location = new System.Drawing.Point(824, 95);
            this.btnDeleteImages.Name = "btnDeleteImages";
            this.btnDeleteImages.Size = new System.Drawing.Size(146, 50);
            this.btnDeleteImages.TabIndex = 12;
            this.btnDeleteImages.Text = "حذف جميع الصور";
            this.btnDeleteImages.UseVisualStyleBackColor = true;
            this.btnDeleteImages.Click += new System.EventHandler(this.btnDeleteImages_Click);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemMenu});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menu.Size = new System.Drawing.Size(1139, 24);
            this.menu.TabIndex = 13;
            this.menu.Text = "menuStrip1";
            // 
            // systemMenu
            // 
            this.systemMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.installComponents});
            this.systemMenu.Name = "systemMenu";
            this.systemMenu.Size = new System.Drawing.Size(50, 20);
            this.systemMenu.Text = "النظام";
            // 
            // installComponents
            // 
            this.installComponents.Name = "installComponents";
            this.installComponents.Size = new System.Drawing.Size(148, 22);
            this.installComponents.Text = "تثبيت المكونات";
            this.installComponents.Click += new System.EventHandler(this.installComponents_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 584);
            this.Controls.Add(this.btnDeleteImages);
            this.Controls.Add(this.lblTitleMessage);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.btnOpenVcard);
            this.Controls.Add(this.status);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.btnRunSenderVendor);
            this.Controls.Add(this.btnUploadImages);
            this.Controls.Add(this.imagesList);
            this.Controls.Add(this.clientsList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Whatsapp Sender (Mini Edition)";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.clientsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagesList)).EndInit();
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView clientsList;
        private System.Windows.Forms.DataGridView imagesList;
        private System.Windows.Forms.Button btnUploadImages;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusTitle;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Button btnOpenVcard;
        private System.Windows.Forms.Button btnRunSenderVendor;
        private System.Windows.Forms.RichTextBox messageBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label lblTitleMessage;
        private System.Windows.Forms.Button btnDeleteImages;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem systemMenu;
        private System.Windows.Forms.ToolStripMenuItem installComponents;
        private System.Windows.Forms.ToolStripStatusLabel endWsVendor;
    }
}

