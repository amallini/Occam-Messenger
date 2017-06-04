namespace OccamMsgr
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DGAbonents = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BDel = new System.Windows.Forms.Button();
            this.BAdd = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.CBLanguage = new System.Windows.Forms.ComboBox();
            this.LAbonentList = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.BSend = new System.Windows.Forms.Button();
            this.BStartRecord = new System.Windows.Forms.Button();
            this.LRecording = new System.Windows.Forms.Label();
            this.BStopRecord = new System.Windows.Forms.Button();
            this.LVoice = new System.Windows.Forms.Label();
            this.EditPanel = new System.Windows.Forms.TableLayoutPanel();
            this.BAttachFile = new System.Windows.Forms.Button();
            this.TEdit = new System.Windows.Forms.TextBox();
            this.PInfo = new System.Windows.Forms.Panel();
            this.LError = new System.Windows.Forms.Label();
            this.LInfo2 = new System.Windows.Forms.Label();
            this.LInfo1 = new System.Windows.Forms.Label();
            this.THistory = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGAbonents)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.EditPanel.SuspendLayout();
            this.PInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.DGAbonents);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Name = "panel1";
            // 
            // DGAbonents
            // 
            resources.ApplyResources(this.DGAbonents, "DGAbonents");
            this.DGAbonents.AllowUserToAddRows = false;
            this.DGAbonents.AllowUserToDeleteRows = false;
            this.DGAbonents.AllowUserToResizeColumns = false;
            this.DGAbonents.AllowUserToResizeRows = false;
            this.DGAbonents.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.DGAbonents.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DGAbonents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGAbonents.Name = "DGAbonents";
            this.DGAbonents.ReadOnly = true;
            this.DGAbonents.RowHeadersVisible = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DGAbonents.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DGAbonents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGAbonents.SelectionChanged += new System.EventHandler(this.DGAbonents_SelectionChanged);
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel3.Name = "panel3";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel2.Controls.Add(this.BDel);
            this.panel2.Controls.Add(this.BAdd);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Name = "panel2";
            // 
            // BDel
            // 
            resources.ApplyResources(this.BDel, "BDel");
            this.BDel.BackgroundImage = global::OccamMsgr.Properties.Resources.delete;
            this.BDel.Name = "BDel";
            this.BDel.UseVisualStyleBackColor = true;
            this.BDel.Click += new System.EventHandler(this.BDel_Click);
            // 
            // BAdd
            // 
            resources.ApplyResources(this.BAdd, "BAdd");
            this.BAdd.BackgroundImage = global::OccamMsgr.Properties.Resources.add;
            this.BAdd.Name = "BAdd";
            this.BAdd.UseVisualStyleBackColor = true;
            this.BAdd.Click += new System.EventHandler(this.BAdd_Click);
            // 
            // panel4
            // 
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Name = "panel4";
            // 
            // panel6
            // 
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel6.Controls.Add(this.CBLanguage);
            this.panel6.Controls.Add(this.LAbonentList);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Name = "panel6";
            // 
            // CBLanguage
            // 
            resources.ApplyResources(this.CBLanguage, "CBLanguage");
            this.CBLanguage.FormattingEnabled = true;
            this.CBLanguage.Name = "CBLanguage";
            // 
            // LAbonentList
            // 
            resources.ApplyResources(this.LAbonentList, "LAbonentList");
            this.LAbonentList.Name = "LAbonentList";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.BSend, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.BStartRecord, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.LRecording, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.BStopRecord, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.LVoice, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // BSend
            // 
            resources.ApplyResources(this.BSend, "BSend");
            this.BSend.Name = "BSend";
            this.BSend.UseVisualStyleBackColor = true;
            this.BSend.Click += new System.EventHandler(this.BSend_Click);
            // 
            // BStartRecord
            // 
            resources.ApplyResources(this.BStartRecord, "BStartRecord");
            this.BStartRecord.BackgroundImage = global::OccamMsgr.Properties.Resources.Запись_48;
            this.BStartRecord.Name = "BStartRecord";
            this.BStartRecord.UseVisualStyleBackColor = true;
            this.BStartRecord.Click += new System.EventHandler(this.BStartRecord_Click);
            // 
            // LRecording
            // 
            resources.ApplyResources(this.LRecording, "LRecording");
            this.LRecording.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LRecording.ForeColor = System.Drawing.Color.Coral;
            this.LRecording.Name = "LRecording";
            // 
            // BStopRecord
            // 
            resources.ApplyResources(this.BStopRecord, "BStopRecord");
            this.BStopRecord.BackgroundImage = global::OccamMsgr.Properties.Resources.Стоп_48__1_;
            this.BStopRecord.Name = "BStopRecord";
            this.BStopRecord.UseVisualStyleBackColor = true;
            this.BStopRecord.Click += new System.EventHandler(this.BStopRecord_Click);
            // 
            // LVoice
            // 
            resources.ApplyResources(this.LVoice, "LVoice");
            this.LVoice.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LVoice.Name = "LVoice";
            // 
            // EditPanel
            // 
            resources.ApplyResources(this.EditPanel, "EditPanel");
            this.EditPanel.Controls.Add(this.BAttachFile, 1, 0);
            this.EditPanel.Controls.Add(this.TEdit, 0, 0);
            this.EditPanel.Name = "EditPanel";
            // 
            // BAttachFile
            // 
            resources.ApplyResources(this.BAttachFile, "BAttachFile");
            this.BAttachFile.Name = "BAttachFile";
            this.BAttachFile.UseVisualStyleBackColor = true;
            this.BAttachFile.Click += new System.EventHandler(this.BAttachFile_Click);
            // 
            // TEdit
            // 
            resources.ApplyResources(this.TEdit, "TEdit");
            this.TEdit.BackColor = System.Drawing.SystemColors.Info;
            this.TEdit.Name = "TEdit";
            this.TEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TEdit_KeyDown);
            // 
            // PInfo
            // 
            resources.ApplyResources(this.PInfo, "PInfo");
            this.PInfo.Controls.Add(this.LError);
            this.PInfo.Controls.Add(this.LInfo2);
            this.PInfo.Controls.Add(this.LInfo1);
            this.PInfo.Name = "PInfo";
            // 
            // LError
            // 
            resources.ApplyResources(this.LError, "LError");
            this.LError.Name = "LError";
            // 
            // LInfo2
            // 
            resources.ApplyResources(this.LInfo2, "LInfo2");
            this.LInfo2.Name = "LInfo2";
            // 
            // LInfo1
            // 
            resources.ApplyResources(this.LInfo1, "LInfo1");
            this.LInfo1.Name = "LInfo1";
            // 
            // THistory
            // 
            resources.ApplyResources(this.THistory, "THistory");
            this.THistory.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.THistory.Name = "THistory";
            this.THistory.ReadOnly = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.THistory);
            this.Controls.Add(this.PInfo);
            this.Controls.Add(this.EditPanel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGAbonents)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.EditPanel.ResumeLayout(false);
            this.EditPanel.PerformLayout();
            this.PInfo.ResumeLayout(false);
            this.PInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView DGAbonents;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label LAbonentList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button BSend;
        private System.Windows.Forms.Button BStartRecord;
        private System.Windows.Forms.Label LRecording;
        private System.Windows.Forms.Button BStopRecord;
        private System.Windows.Forms.Label LVoice;
        private System.Windows.Forms.TableLayoutPanel EditPanel;
        private System.Windows.Forms.Button BAttachFile;
        private System.Windows.Forms.Panel PInfo;
        private System.Windows.Forms.Label LError;
        private System.Windows.Forms.Label LInfo2;
        private System.Windows.Forms.Label LInfo1;
        private System.Windows.Forms.TextBox TEdit;
        private System.Windows.Forms.TextBox THistory;
        private System.Windows.Forms.Button BAdd;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button BDel;
        private System.Windows.Forms.ComboBox CBLanguage;
    }
}

