namespace NSites_V.ApplicationObjects.UserInterfaces.Lendings.Transactions.Details
{
    partial class EndOfDayDetailUI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EndOfDayDetailUI));
            this.pnlBody = new System.Windows.Forms.Panel();
            this.txtTotalLoanRelease = new System.Windows.Forms.TextBox();
            this.txtTotalServiceFee = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvDetailList = new System.Windows.Forms.DataGridView();
            this.CollectorId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CollectorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmountDue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalRunningBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalCollection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalVariance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalLoanRelease = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalServiceFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTotalVariance = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboBranch = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotalCollection = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label20 = new System.Windows.Forms.Label();
            this.lblMaturityDate = new System.Windows.Forms.Label();
            this.txtTotalAmountDue = new System.Windows.Forms.TextBox();
            this.txtTotalRunningBalance = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetailList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBody
            // 
            this.pnlBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBody.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBody.Controls.Add(this.txtTotalLoanRelease);
            this.pnlBody.Controls.Add(this.txtTotalServiceFee);
            this.pnlBody.Controls.Add(this.label3);
            this.pnlBody.Controls.Add(this.label4);
            this.pnlBody.Controls.Add(this.dgvDetailList);
            this.pnlBody.Controls.Add(this.txtTotalVariance);
            this.pnlBody.Controls.Add(this.label2);
            this.pnlBody.Controls.Add(this.cboBranch);
            this.pnlBody.Controls.Add(this.label1);
            this.pnlBody.Controls.Add(this.btnSave);
            this.pnlBody.Controls.Add(this.txtRemarks);
            this.pnlBody.Controls.Add(this.label7);
            this.pnlBody.Controls.Add(this.txtTotalCollection);
            this.pnlBody.Controls.Add(this.label5);
            this.pnlBody.Controls.Add(this.dtpDate);
            this.pnlBody.Controls.Add(this.label20);
            this.pnlBody.Controls.Add(this.lblMaturityDate);
            this.pnlBody.Controls.Add(this.txtTotalAmountDue);
            this.pnlBody.Controls.Add(this.txtTotalRunningBalance);
            this.pnlBody.Controls.Add(this.label16);
            this.pnlBody.Controls.Add(this.label19);
            this.pnlBody.Location = new System.Drawing.Point(12, 12);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(998, 475);
            this.pnlBody.TabIndex = 8;
            // 
            // txtTotalLoanRelease
            // 
            this.txtTotalLoanRelease.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalLoanRelease.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtTotalLoanRelease.Location = new System.Drawing.Point(828, 70);
            this.txtTotalLoanRelease.Name = "txtTotalLoanRelease";
            this.txtTotalLoanRelease.ReadOnly = true;
            this.txtTotalLoanRelease.Size = new System.Drawing.Size(139, 25);
            this.txtTotalLoanRelease.TabIndex = 271;
            this.txtTotalLoanRelease.Text = "0.00";
            this.txtTotalLoanRelease.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalServiceFee
            // 
            this.txtTotalServiceFee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalServiceFee.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtTotalServiceFee.Location = new System.Drawing.Point(828, 101);
            this.txtTotalServiceFee.Name = "txtTotalServiceFee";
            this.txtTotalServiceFee.ReadOnly = true;
            this.txtTotalServiceFee.Size = new System.Drawing.Size(139, 25);
            this.txtTotalServiceFee.TabIndex = 273;
            this.txtTotalServiceFee.Text = "0.00";
            this.txtTotalServiceFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(708, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 17);
            this.label3.TabIndex = 270;
            this.label3.Text = "Total Loan Release";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(708, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 17);
            this.label4.TabIndex = 272;
            this.label4.Text = "Total Service Fee";
            // 
            // dgvDetailList
            // 
            this.dgvDetailList.AllowUserToAddRows = false;
            this.dgvDetailList.AllowUserToDeleteRows = false;
            this.dgvDetailList.AllowUserToResizeRows = false;
            this.dgvDetailList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetailList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDetailList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetailList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetailList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDetailList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CollectorId,
            this.CollectorName,
            this.TotalAmountDue,
            this.TotalRunningBalance,
            this.TotalCollection,
            this.TotalVariance,
            this.TotalLoanRelease,
            this.TotalServiceFee,
            this.Remarks});
            this.dgvDetailList.Location = new System.Drawing.Point(23, 192);
            this.dgvDetailList.MultiSelect = false;
            this.dgvDetailList.Name = "dgvDetailList";
            this.dgvDetailList.RowHeadersVisible = false;
            this.dgvDetailList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetailList.Size = new System.Drawing.Size(944, 205);
            this.dgvDetailList.TabIndex = 269;
            // 
            // CollectorId
            // 
            this.CollectorId.HeaderText = "CollectorId";
            this.CollectorId.Name = "CollectorId";
            this.CollectorId.ReadOnly = true;
            this.CollectorId.Visible = false;
            this.CollectorId.Width = 96;
            // 
            // CollectorName
            // 
            this.CollectorName.HeaderText = "Collector Name";
            this.CollectorName.Name = "CollectorName";
            this.CollectorName.ReadOnly = true;
            this.CollectorName.Width = 124;
            // 
            // TotalAmountDue
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TotalAmountDue.DefaultCellStyle = dataGridViewCellStyle2;
            this.TotalAmountDue.HeaderText = "Total Amount Due";
            this.TotalAmountDue.Name = "TotalAmountDue";
            this.TotalAmountDue.ReadOnly = true;
            this.TotalAmountDue.Width = 137;
            // 
            // TotalRunningBalance
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TotalRunningBalance.DefaultCellStyle = dataGridViewCellStyle3;
            this.TotalRunningBalance.HeaderText = "Total Running Balance";
            this.TotalRunningBalance.Name = "TotalRunningBalance";
            this.TotalRunningBalance.ReadOnly = true;
            this.TotalRunningBalance.Width = 160;
            // 
            // TotalCollection
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TotalCollection.DefaultCellStyle = dataGridViewCellStyle4;
            this.TotalCollection.HeaderText = "Total Collection";
            this.TotalCollection.Name = "TotalCollection";
            this.TotalCollection.ReadOnly = true;
            this.TotalCollection.Width = 122;
            // 
            // TotalVariance
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TotalVariance.DefaultCellStyle = dataGridViewCellStyle5;
            this.TotalVariance.HeaderText = "Total Variance";
            this.TotalVariance.Name = "TotalVariance";
            this.TotalVariance.ReadOnly = true;
            this.TotalVariance.Width = 114;
            // 
            // TotalLoanRelease
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TotalLoanRelease.DefaultCellStyle = dataGridViewCellStyle6;
            this.TotalLoanRelease.HeaderText = "Total Loan Release";
            this.TotalLoanRelease.Name = "TotalLoanRelease";
            this.TotalLoanRelease.Width = 142;
            // 
            // TotalServiceFee
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TotalServiceFee.DefaultCellStyle = dataGridViewCellStyle7;
            this.TotalServiceFee.HeaderText = "Total Service Fee";
            this.TotalServiceFee.Name = "TotalServiceFee";
            this.TotalServiceFee.Width = 130;
            // 
            // Remarks
            // 
            this.Remarks.HeaderText = "Remarks";
            this.Remarks.Name = "Remarks";
            this.Remarks.Width = 83;
            // 
            // txtTotalVariance
            // 
            this.txtTotalVariance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalVariance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtTotalVariance.Location = new System.Drawing.Point(546, 161);
            this.txtTotalVariance.Name = "txtTotalVariance";
            this.txtTotalVariance.ReadOnly = true;
            this.txtTotalVariance.Size = new System.Drawing.Size(139, 25);
            this.txtTotalVariance.TabIndex = 268;
            this.txtTotalVariance.Text = "0.00";
            this.txtTotalVariance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(408, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 17);
            this.label2.TabIndex = 267;
            this.label2.Text = "Total Variance";
            // 
            // cboBranch
            // 
            this.cboBranch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboBranch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboBranch.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboBranch.Location = new System.Drawing.Point(106, 99);
            this.cboBranch.Name = "cboBranch";
            this.cboBranch.Size = new System.Drawing.Size(250, 25);
            this.cboBranch.TabIndex = 247;
            this.cboBranch.SelectedIndexChanged += new System.EventHandler(this.cboBranch_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 246;
            this.label1.Text = "Branch";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(440, 414);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = " &Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtRemarks.Location = new System.Drawing.Point(106, 130);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(250, 56);
            this.txtRemarks.TabIndex = 185;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 17);
            this.label7.TabIndex = 184;
            this.label7.Text = "Remarks";
            // 
            // txtTotalCollection
            // 
            this.txtTotalCollection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalCollection.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalCollection.Location = new System.Drawing.Point(546, 130);
            this.txtTotalCollection.Name = "txtTotalCollection";
            this.txtTotalCollection.ReadOnly = true;
            this.txtTotalCollection.Size = new System.Drawing.Size(139, 25);
            this.txtTotalCollection.TabIndex = 266;
            this.txtTotalCollection.Text = "0.00";
            this.txtTotalCollection.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(117)))));
            this.label5.Location = new System.Drawing.Point(834, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 32);
            this.label5.TabIndex = 174;
            this.label5.Text = "End of Day";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "MM-dd-yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(106, 68);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(110, 25);
            this.dtpDate.TabIndex = 171;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(408, 133);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(100, 17);
            this.label20.TabIndex = 265;
            this.label20.Text = "Total Collection";
            // 
            // lblMaturityDate
            // 
            this.lblMaturityDate.AutoSize = true;
            this.lblMaturityDate.Location = new System.Drawing.Point(20, 73);
            this.lblMaturityDate.Name = "lblMaturityDate";
            this.lblMaturityDate.Size = new System.Drawing.Size(35, 17);
            this.lblMaturityDate.TabIndex = 170;
            this.lblMaturityDate.Text = "Date";
            // 
            // txtTotalAmountDue
            // 
            this.txtTotalAmountDue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalAmountDue.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtTotalAmountDue.Location = new System.Drawing.Point(546, 68);
            this.txtTotalAmountDue.Name = "txtTotalAmountDue";
            this.txtTotalAmountDue.ReadOnly = true;
            this.txtTotalAmountDue.Size = new System.Drawing.Size(139, 25);
            this.txtTotalAmountDue.TabIndex = 260;
            this.txtTotalAmountDue.Text = "0.00";
            this.txtTotalAmountDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalRunningBalance
            // 
            this.txtTotalRunningBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalRunningBalance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtTotalRunningBalance.Location = new System.Drawing.Point(546, 99);
            this.txtTotalRunningBalance.Name = "txtTotalRunningBalance";
            this.txtTotalRunningBalance.ReadOnly = true;
            this.txtTotalRunningBalance.Size = new System.Drawing.Size(139, 25);
            this.txtTotalRunningBalance.TabIndex = 264;
            this.txtTotalRunningBalance.Text = "0.00";
            this.txtTotalRunningBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(408, 71);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(112, 17);
            this.label16.TabIndex = 259;
            this.label16.Text = "Total Amount Due";
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(408, 102);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(135, 17);
            this.label19.TabIndex = 263;
            this.label19.Text = "Total Running Balance";
            // 
            // EndOfDayDetailUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(207)))), ((int)(((byte)(140)))));
            this.ClientSize = new System.Drawing.Size(1022, 499);
            this.Controls.Add(this.pnlBody);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "EndOfDayDetailUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "End of Day Detail";
            this.Load += new System.EventHandler(this.EndOfDayDetailUI_Load);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetailList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.TextBox txtTotalCollection;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtTotalRunningBalance;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtTotalAmountDue;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cboBranch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblMaturityDate;
        private System.Windows.Forms.TextBox txtTotalVariance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvDetailList;
        private System.Windows.Forms.DataGridViewTextBoxColumn CollectorId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CollectorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmountDue;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalRunningBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalCollection;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalVariance;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalLoanRelease;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalServiceFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
        private System.Windows.Forms.TextBox txtTotalLoanRelease;
        private System.Windows.Forms.TextBox txtTotalServiceFee;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}