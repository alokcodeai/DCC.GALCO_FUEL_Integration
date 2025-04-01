namespace DCC.App.Monitor
{
    partial class Form2
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Collapse = new System.Windows.Forms.Button();
            this.Expand = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.source_db_key = new DevExpress.XtraGrid.Columns.GridColumn();
            this.destinaction_db_key = new DevExpress.XtraGrid.Columns.GridColumn();
            this.source_xml_filename = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.result_code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Currency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Service_Station_Code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Status1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.panel1.BackColor = System.Drawing.Color.Goldenrod;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1143, 48);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(950, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(193, 48);
            this.panel2.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(132, 4);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(80, 4);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 30);
            this.button2.TabIndex = 3;
            this.button2.Text = "Max";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(28, 4);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(56, 30);
            this.button3.TabIndex = 4;
            this.button3.Text = "Min";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(335, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "Integration Monitor";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.gridControl1);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1143, 636);
            this.panel3.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Goldenrod;
            this.panel5.Controls.Add(this.textBox1);
            this.panel5.Controls.Add(this.Collapse);
            this.panel5.Controls.Add(this.Expand);
            this.panel5.Controls.Add(this.button5);
            this.panel5.Controls.Add(this.Save);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 588);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1143, 48);
            this.panel5.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(857, 7);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(83, 22);
            this.textBox1.TabIndex = 9;
            this.textBox1.Visible = false;
            // 
            // Collapse
            // 
            this.Collapse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Collapse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Collapse.ForeColor = System.Drawing.Color.White;
            this.Collapse.Location = new System.Drawing.Point(1045, 4);
            this.Collapse.Margin = new System.Windows.Forms.Padding(4);
            this.Collapse.Name = "Collapse";
            this.Collapse.Size = new System.Drawing.Size(92, 37);
            this.Collapse.TabIndex = 8;
            this.Collapse.Text = "Collapse";
            this.Collapse.UseVisualStyleBackColor = false;
            this.Collapse.Visible = false;
            this.Collapse.Click += new System.EventHandler(this.Collapse_Click);
            // 
            // Expand
            // 
            this.Expand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Expand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Expand.ForeColor = System.Drawing.Color.White;
            this.Expand.Location = new System.Drawing.Point(949, 4);
            this.Expand.Margin = new System.Windows.Forms.Padding(4);
            this.Expand.Name = "Expand";
            this.Expand.Size = new System.Drawing.Size(92, 37);
            this.Expand.TabIndex = 7;
            this.Expand.Text = "Expand";
            this.Expand.UseVisualStyleBackColor = false;
            this.Expand.Visible = false;
            this.Expand.Click += new System.EventHandler(this.Expand_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(112, 4);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(92, 37);
            this.button5.TabIndex = 6;
            this.button5.Text = "Refresh";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Save
            // 
            this.Save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Save.ForeColor = System.Drawing.Color.White;
            this.Save.Location = new System.Drawing.Point(12, 4);
            this.Save.Margin = new System.Windows.Forms.Padding(4);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(92, 37);
            this.Save.TabIndex = 5;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = false;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // 
            // 
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl1.Location = new System.Drawing.Point(0, 48);
            this.gridControl1.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1143, 588);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.gridView2});
            this.gridControl1.Load += new System.EventHandler(this.gridControl1_Load);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            // 
            // gridView2
            // 
            this.gridView2.Appearance.HorzLine.Options.UseForeColor = true;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.bandedGridColumn2,
            this.bandedGridColumn1,
            this.source_db_key,
            this.destinaction_db_key,
            this.source_xml_filename,
            this.Status,
            this.result_code,
            this.Currency,
            this.Service_Station_Code,
            this.Status1,
            this.bandedGridColumn3,
            this.bandedGridColumn4,
            this.bandedGridColumn5});
            this.gridView2.GridControl = this.gridControl1;
            this.gridView2.GroupCount = 2;
            this.gridView2.Name = "gridView2";
            this.gridView2.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.bandedGridColumn2, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.bandedGridColumn4, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView2.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView2_CellValueChanged);
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.Caption = "Id";
            this.bandedGridColumn2.FieldName = "Id";
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn2.Visible = true;
            this.bandedGridColumn2.VisibleIndex = 0;
            this.bandedGridColumn2.Width = 200;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.Caption = "BatchDate";
            this.bandedGridColumn1.FieldName = "BatchDate";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.Visible = true;
            this.bandedGridColumn1.VisibleIndex = 0;
            this.bandedGridColumn1.Width = 95;
            // 
            // source_db_key
            // 
            this.source_db_key.Caption = "Vehicle_Id";
            this.source_db_key.FieldName = "Vehicle_Id";
            this.source_db_key.Name = "source_db_key";
            this.source_db_key.Visible = true;
            this.source_db_key.VisibleIndex = 7;
            // 
            // destinaction_db_key
            // 
            this.destinaction_db_key.Caption = "Shipment_Number";
            this.destinaction_db_key.FieldName = "Shipment_Number";
            this.destinaction_db_key.Name = "destinaction_db_key";
            this.destinaction_db_key.Visible = true;
            this.destinaction_db_key.VisibleIndex = 2;
            // 
            // source_xml_filename
            // 
            this.source_xml_filename.Caption = "Line_Id";
            this.source_xml_filename.FieldName = "Line_Id";
            this.source_xml_filename.Name = "source_xml_filename";
            this.source_xml_filename.Visible = true;
            this.source_xml_filename.VisibleIndex = 3;
            // 
            // Status
            // 
            this.Status.Caption = "Total_Fuel_Litres";
            this.Status.FieldName = "Total_Fuel_Litres";
            this.Status.Name = "Status";
            this.Status.OptionsColumn.AllowEdit = false;
            this.Status.Visible = true;
            this.Status.VisibleIndex = 4;
            this.Status.Width = 57;
            // 
            // result_code
            // 
            this.result_code.Caption = "Per_Litre_Amount";
            this.result_code.FieldName = "Per_Litre_Amount";
            this.result_code.Name = "result_code";
            this.result_code.Visible = true;
            this.result_code.VisibleIndex = 6;
            // 
            // Currency
            // 
            this.Currency.Caption = "Currency";
            this.Currency.FieldName = "Currency";
            this.Currency.Name = "Currency";
            this.Currency.Visible = true;
            this.Currency.VisibleIndex = 8;
            // 
            // Service_Station_Code
            // 
            this.Service_Station_Code.Caption = "Service_Station_Code";
            this.Service_Station_Code.FieldName = "Service_Station_Code";
            this.Service_Station_Code.Name = "Service_Station_Code";
            this.Service_Station_Code.Visible = true;
            this.Service_Station_Code.VisibleIndex = 9;
            // 
            // Status1
            // 
            this.Status1.Caption = "Status";
            this.Status1.FieldName = "Status";
            this.Status1.Name = "Status1";
            this.Status1.Visible = true;
            this.Status1.VisibleIndex = 10;
            // 
            // bandedGridColumn3
            // 
            this.bandedGridColumn3.Caption = "Response";
            this.bandedGridColumn3.FieldName = "Response";
            this.bandedGridColumn3.Name = "bandedGridColumn3";
            this.bandedGridColumn3.Visible = true;
            this.bandedGridColumn3.VisibleIndex = 1;
            this.bandedGridColumn3.Width = 257;
            // 
            // bandedGridColumn4
            // 
            this.bandedGridColumn4.Caption = "IsProcessed";
            this.bandedGridColumn4.FieldName = "IsProcessed";
            this.bandedGridColumn4.Name = "bandedGridColumn4";
            this.bandedGridColumn4.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn4.Visible = true;
            this.bandedGridColumn4.VisibleIndex = 2;
            this.bandedGridColumn4.Width = 50;
            // 
            // bandedGridColumn5
            // 
            this.bandedGridColumn5.Caption = "Retry";
            this.bandedGridColumn5.FieldName = "retry_flag";
            this.bandedGridColumn5.Name = "bandedGridColumn5";
            this.bandedGridColumn5.Visible = true;
            this.bandedGridColumn5.VisibleIndex = 5;
            this.bandedGridColumn5.Width = 57;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1143, 636);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Integration Monitor";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button Collapse;
        private System.Windows.Forms.Button Expand;
        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn source_db_key;
        private DevExpress.XtraGrid.Columns.GridColumn destinaction_db_key;
        private DevExpress.XtraGrid.Columns.GridColumn source_xml_filename;
        private DevExpress.XtraGrid.Columns.GridColumn Status;
        private DevExpress.XtraGrid.Columns.GridColumn result_code;
        private DevExpress.XtraGrid.Columns.GridColumn Currency;
        private DevExpress.XtraGrid.Columns.GridColumn Service_Station_Code;
        private DevExpress.XtraGrid.Columns.GridColumn Status1;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn5;
    }
}