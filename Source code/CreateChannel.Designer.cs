namespace OccamMsgr
{
    partial class CreateChannel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateChannel));
            this.BClose = new System.Windows.Forms.Button();
            this.BCancel = new System.Windows.Forms.Button();
            this.BCreate = new System.Windows.Forms.Button();
            this.LError = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.TCFPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.LChannelGUID = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RBAbonent = new System.Windows.Forms.RadioButton();
            this.RBInitiator = new System.Windows.Forms.RadioButton();
            this.PInitiator = new System.Windows.Forms.Panel();
            this.LMakeSureNewFolderCreated = new System.Windows.Forms.Label();
            this.LShareThisFolder = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.TAbonent1NickName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.LChooseNickNames = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TAbonent2NickName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LCreateNewFolder = new System.Windows.Forms.Label();
            this.PAbonent = new System.Windows.Forms.Panel();
            this.LWaitDB3 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.LAcceptInvitation = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.TDB3Path = new System.Windows.Forms.TextBox();
            this.LRegister = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.PInitiator.SuspendLayout();
            this.PAbonent.SuspendLayout();
            this.SuspendLayout();
            // 
            // BClose
            // 
            resources.ApplyResources(this.BClose, "BClose");
            this.BClose.Name = "BClose";
            this.BClose.UseVisualStyleBackColor = true;
            this.BClose.Click += new System.EventHandler(this.BClose_Click);
            // 
            // BCancel
            // 
            resources.ApplyResources(this.BCancel, "BCancel");
            this.BCancel.Name = "BCancel";
            this.BCancel.UseVisualStyleBackColor = true;
            this.BCancel.Click += new System.EventHandler(this.BCancel_Click);
            // 
            // BCreate
            // 
            resources.ApplyResources(this.BCreate, "BCreate");
            this.BCreate.Name = "BCreate";
            this.BCreate.UseVisualStyleBackColor = true;
            this.BCreate.Click += new System.EventHandler(this.BCreate_Click);
            // 
            // LError
            // 
            resources.ApplyResources(this.LError, "LError");
            this.LError.Name = "LError";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.BSelectFile2_Click);
            // 
            // TCFPath
            // 
            resources.ApplyResources(this.TCFPath, "TCFPath");
            this.TCFPath.Name = "TCFPath";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // LChannelGUID
            // 
            resources.ApplyResources(this.LChannelGUID, "LChannelGUID");
            this.LChannelGUID.Name = "LChannelGUID";
            this.LChannelGUID.UseWaitCursor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.RBAbonent);
            this.panel1.Controls.Add(this.RBInitiator);
            this.panel1.Name = "panel1";
            // 
            // RBAbonent
            // 
            resources.ApplyResources(this.RBAbonent, "RBAbonent");
            this.RBAbonent.Name = "RBAbonent";
            this.RBAbonent.UseVisualStyleBackColor = true;
            // 
            // RBInitiator
            // 
            resources.ApplyResources(this.RBInitiator, "RBInitiator");
            this.RBInitiator.Checked = true;
            this.RBInitiator.Name = "RBInitiator";
            this.RBInitiator.TabStop = true;
            this.RBInitiator.UseVisualStyleBackColor = true;
            this.RBInitiator.CheckedChanged += new System.EventHandler(this.RBInitiator_CheckedChanged);
            // 
            // PInitiator
            // 
            resources.ApplyResources(this.PInitiator, "PInitiator");
            this.PInitiator.Controls.Add(this.LMakeSureNewFolderCreated);
            this.PInitiator.Controls.Add(this.LShareThisFolder);
            this.PInitiator.Controls.Add(this.label10);
            this.PInitiator.Controls.Add(this.TAbonent1NickName);
            this.PInitiator.Controls.Add(this.label9);
            this.PInitiator.Controls.Add(this.LChooseNickNames);
            this.PInitiator.Controls.Add(this.label7);
            this.PInitiator.Controls.Add(this.TAbonent2NickName);
            this.PInitiator.Controls.Add(this.label3);
            this.PInitiator.Controls.Add(this.LCreateNewFolder);
            this.PInitiator.Name = "PInitiator";
            // 
            // LMakeSureNewFolderCreated
            // 
            resources.ApplyResources(this.LMakeSureNewFolderCreated, "LMakeSureNewFolderCreated");
            this.LMakeSureNewFolderCreated.Name = "LMakeSureNewFolderCreated";
            // 
            // LShareThisFolder
            // 
            resources.ApplyResources(this.LShareThisFolder, "LShareThisFolder");
            this.LShareThisFolder.Name = "LShareThisFolder";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // TAbonent1NickName
            // 
            resources.ApplyResources(this.TAbonent1NickName, "TAbonent1NickName");
            this.TAbonent1NickName.Name = "TAbonent1NickName";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // LChooseNickNames
            // 
            resources.ApplyResources(this.LChooseNickNames, "LChooseNickNames");
            this.LChooseNickNames.Name = "LChooseNickNames";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // TAbonent2NickName
            // 
            resources.ApplyResources(this.TAbonent2NickName, "TAbonent2NickName");
            this.TAbonent2NickName.Name = "TAbonent2NickName";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // LCreateNewFolder
            // 
            resources.ApplyResources(this.LCreateNewFolder, "LCreateNewFolder");
            this.LCreateNewFolder.Name = "LCreateNewFolder";
            // 
            // PAbonent
            // 
            resources.ApplyResources(this.PAbonent, "PAbonent");
            this.PAbonent.Controls.Add(this.LWaitDB3);
            this.PAbonent.Controls.Add(this.label14);
            this.PAbonent.Controls.Add(this.LAcceptInvitation);
            this.PAbonent.Controls.Add(this.button2);
            this.PAbonent.Controls.Add(this.TDB3Path);
            this.PAbonent.Name = "PAbonent";
            // 
            // LWaitDB3
            // 
            resources.ApplyResources(this.LWaitDB3, "LWaitDB3");
            this.LWaitDB3.Name = "LWaitDB3";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // LAcceptInvitation
            // 
            resources.ApplyResources(this.LAcceptInvitation, "LAcceptInvitation");
            this.LAcceptInvitation.Name = "LAcceptInvitation";
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // TDB3Path
            // 
            resources.ApplyResources(this.TDB3Path, "TDB3Path");
            this.TDB3Path.Name = "TDB3Path";
            // 
            // LRegister
            // 
            resources.ApplyResources(this.LRegister, "LRegister");
            this.LRegister.Name = "LRegister";
            // 
            // CreateChannel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LRegister);
            this.Controls.Add(this.PAbonent);
            this.Controls.Add(this.PInitiator);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.LChannelGUID);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TCFPath);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LError);
            this.Controls.Add(this.BCreate);
            this.Controls.Add(this.BCancel);
            this.Controls.Add(this.BClose);
            this.Name = "CreateChannel";
            this.Load += new System.EventHandler(this.CreateChannel_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PInitiator.ResumeLayout(false);
            this.PInitiator.PerformLayout();
            this.PAbonent.ResumeLayout(false);
            this.PAbonent.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BCancel;
        private System.Windows.Forms.Label LError;
        public System.Windows.Forms.Button BClose;
        public System.Windows.Forms.Button BCreate;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox TCFPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label LChannelGUID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton RBAbonent;
        private System.Windows.Forms.RadioButton RBInitiator;
        private System.Windows.Forms.Panel PInitiator;
        private System.Windows.Forms.Label LMakeSureNewFolderCreated;
        private System.Windows.Forms.Label LShareThisFolder;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox TAbonent1NickName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox TAbonent2NickName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LCreateNewFolder;
        private System.Windows.Forms.Panel PAbonent;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label LAcceptInvitation;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.TextBox TDB3Path;
        private System.Windows.Forms.Label LChooseNickNames;
        private System.Windows.Forms.Label LWaitDB3;
        private System.Windows.Forms.Label LRegister;
    }
}