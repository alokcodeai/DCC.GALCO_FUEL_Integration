namespace DCC.App.Monitor
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.dtfrom = new System.Windows.Forms.DateTimePicker();
            this.cmbstatus = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtto = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.GridService = new System.Windows.Forms.DataGridView();
            this.UnboundStaus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnboundRetry = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cmbsrc = new System.Windows.Forms.ComboBox();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.GridService)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date";
            // 
            // dtfrom
            // 
            this.dtfrom.CustomFormat = "yyyy-MM-dd";
            this.dtfrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtfrom.Location = new System.Drawing.Point(74, 15);
            this.dtfrom.Name = "dtfrom";
            this.dtfrom.Size = new System.Drawing.Size(98, 20);
            this.dtfrom.TabIndex = 1;
            this.dtfrom.ValueChanged += new System.EventHandler(this.dtfrom_ValueChanged);
            // 
            // cmbstatus
            // 
            this.cmbstatus.FormattingEnabled = true;
            this.cmbstatus.Items.AddRange(new object[] {
            "Please Select",
            "Processed",
            "Not Processed",
            "Failed"});
            this.cmbstatus.Location = new System.Drawing.Point(642, 15);
            this.cmbstatus.Name = "cmbstatus";
            this.cmbstatus.Size = new System.Drawing.Size(98, 21);
            this.cmbstatus.TabIndex = 2;
            this.cmbstatus.SelectedIndexChanged += new System.EventHandler(this.cmbstatus_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "To Date";
            // 
            // dtto
            // 
            this.dtto.CustomFormat = "yyyy-MM-dd";
            this.dtto.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtto.Location = new System.Drawing.Point(252, 15);
            this.dtto.Name = "dtto";
            this.dtto.Size = new System.Drawing.Size(98, 20);
            this.dtto.TabIndex = 4;
            this.dtto.ValueChanged += new System.EventHandler(this.dtto_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(368, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Source Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(584, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Status";
            // 
            // GridService
            // 
            this.GridService.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridService.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridService.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridService.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UnboundStaus,
            this.UnboundRetry});
            this.GridService.Location = new System.Drawing.Point(12, 53);
            this.GridService.Name = "GridService";
            this.GridService.Size = new System.Drawing.Size(731, 354);
            this.GridService.TabIndex = 7;
            this.GridService.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridService_CellFormatting);
            // 
            // UnboundStaus
            // 
            this.UnboundStaus.HeaderText = "Status";
            this.UnboundStaus.Name = "UnboundStaus";
            // 
            // UnboundRetry
            // 
            this.UnboundRetry.HeaderText = "Retry";
            this.UnboundRetry.Name = "UnboundRetry";
            // 
            // cmbsrc
            // 
            this.cmbsrc.FormattingEnabled = true;
            this.cmbsrc.Location = new System.Drawing.Point(442, 15);
            this.cmbsrc.Name = "cmbsrc";
            this.cmbsrc.Size = new System.Drawing.Size(103, 21);
            this.cmbsrc.TabIndex = 8;
            this.cmbsrc.SelectedIndexChanged += new System.EventHandler(this.cmbsrc_SelectedIndexChanged);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(13, 0);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 23);
            this.BtnSave.TabIndex = 9;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(94, 0);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(75, 23);
            this.BtnRefresh.TabIndex = 10;
            this.BtnRefresh.Text = "Refresh";
            this.BtnRefresh.UseVisualStyleBackColor = true;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(175, 0);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 23);
            this.BtnExit.TabIndex = 11;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnSave);
            this.panel1.Controls.Add(this.BtnExit);
            this.panel1.Controls.Add(this.BtnRefresh);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 426);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(752, 46);
            this.panel1.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 472);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmbsrc);
            this.Controls.Add(this.GridService);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtto);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbstatus);
            this.Controls.Add(this.dtfrom);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Integration Service Monitor";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridService)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtfrom;
        private System.Windows.Forms.ComboBox cmbstatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView GridService;
        private System.Windows.Forms.ComboBox cmbsrc;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnboundStaus;
        private System.Windows.Forms.DataGridViewCheckBoxColumn UnboundRetry;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Panel panel1;
    }
}

