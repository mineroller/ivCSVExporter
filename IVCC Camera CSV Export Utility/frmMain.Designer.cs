﻿
namespace IVCC_Camera_CSV_Export_Utility
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnGenerate = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.bgwRequestHandler = new System.ComponentModel.BackgroundWorker();
            this.olvCamList = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn9 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn11 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn12 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn13 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn15 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn14 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn10 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnExport = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblGenerateStatus = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkGetLocMac = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.olvCamList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Location = new System.Drawing.Point(17, 23);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(218, 41);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate Camera List";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(547, 23);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1175, 41);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 1;
            // 
            // bgwRequestHandler
            // 
            this.bgwRequestHandler.WorkerReportsProgress = true;
            this.bgwRequestHandler.WorkerSupportsCancellation = true;
            this.bgwRequestHandler.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwRequestHandler_DoWork);
            this.bgwRequestHandler.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwRequestHandler_ProgressChanged);
            this.bgwRequestHandler.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwRequestHandler_RunWorkerCompleted);
            // 
            // olvCamList
            // 
            this.olvCamList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.olvCamList.AllColumns.Add(this.olvColumn1);
            this.olvCamList.AllColumns.Add(this.olvColumn9);
            this.olvCamList.AllColumns.Add(this.olvColumn3);
            this.olvCamList.AllColumns.Add(this.olvColumn4);
            this.olvCamList.AllColumns.Add(this.olvColumn11);
            this.olvCamList.AllColumns.Add(this.olvColumn12);
            this.olvCamList.AllColumns.Add(this.olvColumn13);
            this.olvCamList.AllColumns.Add(this.olvColumn15);
            this.olvCamList.AllColumns.Add(this.olvColumn14);
            this.olvCamList.AllColumns.Add(this.olvColumn5);
            this.olvCamList.AllColumns.Add(this.olvColumn6);
            this.olvCamList.AllColumns.Add(this.olvColumn2);
            this.olvCamList.AllColumns.Add(this.olvColumn7);
            this.olvCamList.AllColumns.Add(this.olvColumn8);
            this.olvCamList.AllColumns.Add(this.olvColumn10);
            this.olvCamList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olvCamList.CellEditUseWholeCell = false;
            this.olvCamList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn9,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn11,
            this.olvColumn12,
            this.olvColumn13,
            this.olvColumn15,
            this.olvColumn14,
            this.olvColumn5,
            this.olvColumn6,
            this.olvColumn2,
            this.olvColumn7,
            this.olvColumn8,
            this.olvColumn10});
            this.olvCamList.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvCamList.FullRowSelect = true;
            this.olvCamList.HideSelection = false;
            this.olvCamList.Location = new System.Drawing.Point(12, 71);
            this.olvCamList.Name = "olvCamList";
            this.olvCamList.Size = new System.Drawing.Size(1710, 699);
            this.olvCamList.TabIndex = 2;
            this.olvCamList.UseAlternatingBackColors = true;
            this.olvCamList.UseCompatibleStateImageBehavior = false;
            this.olvCamList.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Number";
            this.olvColumn1.Groupable = false;
            this.olvColumn1.Text = "Number";
            this.olvColumn1.Width = 95;
            // 
            // olvColumn9
            // 
            this.olvColumn9.AspectName = "Name";
            this.olvColumn9.Groupable = false;
            this.olvColumn9.Text = "Name";
            this.olvColumn9.Width = 114;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Home_Site";
            this.olvColumn3.Text = "Home Site";
            this.olvColumn3.Width = 117;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "IP_Address";
            this.olvColumn4.Groupable = false;
            this.olvColumn4.Text = "IP Address";
            this.olvColumn4.Width = 119;
            // 
            // olvColumn11
            // 
            this.olvColumn11.AspectName = "Location";
            this.olvColumn11.Text = "Location (ONVIF)";
            this.olvColumn11.Width = 118;
            // 
            // olvColumn12
            // 
            this.olvColumn12.AspectName = "MAC_Address";
            this.olvColumn12.Text = "MAC Address (ONVIF)";
            this.olvColumn12.Width = 136;
            // 
            // olvColumn13
            // 
            this.olvColumn13.AspectName = "Serial_Number";
            this.olvColumn13.DisplayIndex = 7;
            this.olvColumn13.Text = "Serial (ONVIF)";
            this.olvColumn13.Width = 104;
            // 
            // olvColumn15
            // 
            this.olvColumn15.AspectName = "FW_Version";
            this.olvColumn15.DisplayIndex = 14;
            this.olvColumn15.Text = "Firmware (ONVIF)";
            this.olvColumn15.Width = 117;
            // 
            // olvColumn14
            // 
            this.olvColumn14.AspectName = "Hardware_Model";
            this.olvColumn14.Text = "Model (ONVIF)";
            this.olvColumn14.Width = 111;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "Service_ID";
            this.olvColumn5.DisplayIndex = 6;
            this.olvColumn5.Groupable = false;
            this.olvColumn5.Text = "Service ID";
            this.olvColumn5.Width = 123;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "Access_URL";
            this.olvColumn6.DisplayIndex = 9;
            this.olvColumn6.Groupable = false;
            this.olvColumn6.Text = "Camera URL";
            this.olvColumn6.Width = 116;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Online";
            this.olvColumn2.DisplayIndex = 10;
            this.olvColumn2.Text = "Connected";
            this.olvColumn2.Width = 100;
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "Primary_NVR";
            this.olvColumn7.DisplayIndex = 11;
            this.olvColumn7.Text = "Primary NVR";
            this.olvColumn7.Width = 122;
            // 
            // olvColumn8
            // 
            this.olvColumn8.AspectName = "Primary_NVR_IP";
            this.olvColumn8.DisplayIndex = 12;
            this.olvColumn8.Groupable = false;
            this.olvColumn8.Text = "Primary NVR IP";
            this.olvColumn8.Width = 105;
            // 
            // olvColumn10
            // 
            this.olvColumn10.AspectName = "Recording_NVR";
            this.olvColumn10.DisplayIndex = 13;
            this.olvColumn10.Text = "Recording NVR";
            this.olvColumn10.Width = 102;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.BackColor = System.Drawing.Color.DarkGreen;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(1380, 776);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(214, 53);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export to CSV...";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackColor = System.Drawing.Color.OrangeRed;
            this.btnCancel.Enabled = false;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(12, 776);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(180, 38);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel Task";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblGenerateStatus
            // 
            this.lblGenerateStatus.BackColor = System.Drawing.Color.Black;
            this.lblGenerateStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGenerateStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGenerateStatus.ForeColor = System.Drawing.Color.SpringGreen;
            this.lblGenerateStatus.Location = new System.Drawing.Point(241, 23);
            this.lblGenerateStatus.Name = "lblGenerateStatus";
            this.lblGenerateStatus.Size = new System.Drawing.Size(300, 41);
            this.lblGenerateStatus.TabIndex = 5;
            this.lblGenerateStatus.Text = "Ready to Start";
            this.lblGenerateStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(1600, 776);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(122, 53);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 831);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Version 2023-08-07 | Location Enabled";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkGetLocMac);
            this.groupBox1.Location = new System.Drawing.Point(855, 779);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(519, 67);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Additional ONVIF Information";
            // 
            // chkGetLocMac
            // 
            this.chkGetLocMac.AutoSize = true;
            this.chkGetLocMac.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGetLocMac.Location = new System.Drawing.Point(90, 28);
            this.chkGetLocMac.Name = "chkGetLocMac";
            this.chkGetLocMac.Size = new System.Drawing.Size(350, 24);
            this.chkGetLocMac.TabIndex = 0;
            this.chkGetLocMac.Text = "Get additional information using ONVIF Protocol";
            this.chkGetLocMac.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1734, 855);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblGenerateStatus);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.olvCamList);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnGenerate);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IVCC Camera CSV Export Utility";
            ((System.ComponentModel.ISupportInitialize)(this.olvCamList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.ComponentModel.BackgroundWorker bgwRequestHandler;
        private BrightIdeasSoftware.ObjectListView olvCamList;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnCancel;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvColumn9;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private BrightIdeasSoftware.OLVColumn olvColumn8;
        private System.Windows.Forms.Label lblGenerateStatus;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private BrightIdeasSoftware.OLVColumn olvColumn10;
        private System.Windows.Forms.Label label1;
        private BrightIdeasSoftware.OLVColumn olvColumn11;
        private BrightIdeasSoftware.OLVColumn olvColumn12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkGetLocMac;
        private BrightIdeasSoftware.OLVColumn olvColumn13;
        private BrightIdeasSoftware.OLVColumn olvColumn14;
        private BrightIdeasSoftware.OLVColumn olvColumn15;
    }
}

