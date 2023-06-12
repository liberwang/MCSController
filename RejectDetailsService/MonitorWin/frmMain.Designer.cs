
namespace MonitorWin {
    partial class frmMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
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
            this.timeRefresh = new System.Windows.Forms.Timer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvCurrentTotal = new System.Windows.Forms.DataGridView();
            this.lblCurrentShift = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblLastShift = new System.Windows.Forms.Label();
            this.dgvLastTotal = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvSecond = new System.Windows.Forms.DataGridView();
            this.lblSecond = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.bindingSourceCurrentTotal = new System.Windows.Forms.BindingSource();
            this.bindingSourceLast = new System.Windows.Forms.BindingSource();
            this.bindingSourceSecondLast = new System.Windows.Forms.BindingSource();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.colProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastProd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastPass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastReject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSecondProd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSecondTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSecondPass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSecondReject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentTotal)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLastTotal)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSecond)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCurrentTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceLast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSecondLast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timeRefresh
            // 
            this.timeRefresh.Interval = 60000;
            this.timeRefresh.Tick += new System.EventHandler(this.timeRefresh_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1948, 1320);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Controls.Add(this.dgvCurrentTotal, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblCurrentShift, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 103);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1942, 396);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // dgvCurrentTotal
            // 
            this.dgvCurrentTotal.AllowUserToAddRows = false;
            this.dgvCurrentTotal.AllowUserToDeleteRows = false;
            this.dgvCurrentTotal.AutoGenerateColumns = false;
            this.dgvCurrentTotal.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvCurrentTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCurrentTotal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCurrentTotal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProduct,
            this.colTotal,
            this.colPass,
            this.colReject});
            this.dgvCurrentTotal.DataSource = this.bindingSourceCurrentTotal;
            this.dgvCurrentTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCurrentTotal.Location = new System.Drawing.Point(30, 80);
            this.dgvCurrentTotal.Margin = new System.Windows.Forms.Padding(30);
            this.dgvCurrentTotal.Name = "dgvCurrentTotal";
            this.dgvCurrentTotal.ReadOnly = true;
            this.dgvCurrentTotal.RowHeadersVisible = false;
            this.dgvCurrentTotal.RowHeadersWidth = 50;
            this.dgvCurrentTotal.RowTemplate.Height = 33;
            this.dgvCurrentTotal.ShowCellToolTips = false;
            this.dgvCurrentTotal.ShowEditingIcon = false;
            this.dgvCurrentTotal.ShowRowErrors = false;
            this.dgvCurrentTotal.Size = new System.Drawing.Size(1687, 286);
            this.dgvCurrentTotal.TabIndex = 0;
            // 
            // lblCurrentShift
            // 
            this.lblCurrentShift.AutoSize = true;
            this.lblCurrentShift.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCurrentShift.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentShift.Location = new System.Drawing.Point(3, 25);
            this.lblCurrentShift.Name = "lblCurrentShift";
            this.lblCurrentShift.Size = new System.Drawing.Size(1741, 25);
            this.lblCurrentShift.TabIndex = 1;
            this.lblCurrentShift.Text = "CurrentShift";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.Controls.Add(this.lblLastShift, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.dgvLastTotal, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 505);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1942, 396);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // lblLastShift
            // 
            this.lblLastShift.AutoSize = true;
            this.lblLastShift.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblLastShift.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastShift.Location = new System.Drawing.Point(3, 25);
            this.lblLastShift.Name = "lblLastShift";
            this.lblLastShift.Size = new System.Drawing.Size(1741, 25);
            this.lblLastShift.TabIndex = 0;
            this.lblLastShift.Text = "LastShift";
            // 
            // dgvLastTotal
            // 
            this.dgvLastTotal.AllowUserToAddRows = false;
            this.dgvLastTotal.AllowUserToDeleteRows = false;
            this.dgvLastTotal.AllowUserToResizeColumns = false;
            this.dgvLastTotal.AllowUserToResizeRows = false;
            this.dgvLastTotal.AutoGenerateColumns = false;
            this.dgvLastTotal.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvLastTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLastTotal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLastTotal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLastProd,
            this.colLastTotal,
            this.colLastPass,
            this.colLastReject});
            this.dgvLastTotal.DataSource = this.bindingSourceLast;
            this.dgvLastTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLastTotal.Location = new System.Drawing.Point(30, 80);
            this.dgvLastTotal.Margin = new System.Windows.Forms.Padding(30);
            this.dgvLastTotal.Name = "dgvLastTotal";
            this.dgvLastTotal.ReadOnly = true;
            this.dgvLastTotal.RowHeadersVisible = false;
            this.dgvLastTotal.RowHeadersWidth = 82;
            this.dgvLastTotal.RowTemplate.Height = 33;
            this.dgvLastTotal.Size = new System.Drawing.Size(1687, 286);
            this.dgvLastTotal.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.Controls.Add(this.dgvSecond, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.lblSecond, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 907);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1942, 410);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // dgvSecond
            // 
            this.dgvSecond.AllowUserToAddRows = false;
            this.dgvSecond.AllowUserToDeleteRows = false;
            this.dgvSecond.AutoGenerateColumns = false;
            this.dgvSecond.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvSecond.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSecond.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSecond.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSecondProd,
            this.colSecondTotal,
            this.colSecondPass,
            this.colSecondReject});
            this.dgvSecond.DataSource = this.bindingSourceSecondLast;
            this.dgvSecond.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSecond.Location = new System.Drawing.Point(30, 80);
            this.dgvSecond.Margin = new System.Windows.Forms.Padding(30);
            this.dgvSecond.Name = "dgvSecond";
            this.dgvSecond.ReadOnly = true;
            this.dgvSecond.RowHeadersVisible = false;
            this.dgvSecond.RowHeadersWidth = 82;
            this.dgvSecond.RowTemplate.Height = 33;
            this.dgvSecond.Size = new System.Drawing.Size(1687, 300);
            this.dgvSecond.TabIndex = 0;
            // 
            // lblSecond
            // 
            this.lblSecond.AutoSize = true;
            this.lblSecond.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSecond.Location = new System.Drawing.Point(3, 25);
            this.lblSecond.Name = "lblSecond";
            this.lblSecond.Size = new System.Drawing.Size(1741, 25);
            this.lblSecond.TabIndex = 1;
            this.lblSecond.Text = "SecondLastShift";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.BackColor = System.Drawing.Color.Blue;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1942, 94);
            this.tableLayoutPanel5.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1666, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 51);
            this.label1.TabIndex = 1;
            this.label1.Text = "Monitor Tool";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MonitorWin.Properties.Resources.logo_final;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(360, 88);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // colProduct
            // 
            this.colProduct.DataPropertyName = "prod_name";
            this.colProduct.HeaderText = "Product";
            this.colProduct.MinimumWidth = 10;
            this.colProduct.Name = "colProduct";
            this.colProduct.ReadOnly = true;
            this.colProduct.Width = 300;
            // 
            // colTotal
            // 
            this.colTotal.DataPropertyName = "tag_cnt";
            this.colTotal.HeaderText = "Total";
            this.colTotal.MinimumWidth = 10;
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            this.colTotal.Width = 200;
            // 
            // colPass
            // 
            this.colPass.DataPropertyName = "pass_cnt";
            this.colPass.HeaderText = "Pass";
            this.colPass.MinimumWidth = 10;
            this.colPass.Name = "colPass";
            this.colPass.ReadOnly = true;
            this.colPass.Width = 200;
            // 
            // colReject
            // 
            this.colReject.DataPropertyName = "reject_cnt";
            this.colReject.HeaderText = "Reject";
            this.colReject.MinimumWidth = 10;
            this.colReject.Name = "colReject";
            this.colReject.ReadOnly = true;
            this.colReject.Width = 200;
            // 
            // colLastProd
            // 
            this.colLastProd.DataPropertyName = "prod_name";
            this.colLastProd.HeaderText = "Product";
            this.colLastProd.MinimumWidth = 10;
            this.colLastProd.Name = "colLastProd";
            this.colLastProd.ReadOnly = true;
            this.colLastProd.Width = 300;
            // 
            // colLastTotal
            // 
            this.colLastTotal.DataPropertyName = "tag_cnt";
            this.colLastTotal.HeaderText = "Total";
            this.colLastTotal.MinimumWidth = 10;
            this.colLastTotal.Name = "colLastTotal";
            this.colLastTotal.ReadOnly = true;
            this.colLastTotal.Width = 200;
            // 
            // colLastPass
            // 
            this.colLastPass.DataPropertyName = "pass_cnt";
            this.colLastPass.HeaderText = "Pass";
            this.colLastPass.MinimumWidth = 10;
            this.colLastPass.Name = "colLastPass";
            this.colLastPass.ReadOnly = true;
            this.colLastPass.Width = 200;
            // 
            // colLastReject
            // 
            this.colLastReject.DataPropertyName = "reject_cnt";
            this.colLastReject.HeaderText = "Reject";
            this.colLastReject.MinimumWidth = 10;
            this.colLastReject.Name = "colLastReject";
            this.colLastReject.ReadOnly = true;
            this.colLastReject.Width = 200;
            // 
            // colSecondProd
            // 
            this.colSecondProd.DataPropertyName = "prod_name";
            this.colSecondProd.HeaderText = "Product";
            this.colSecondProd.MinimumWidth = 10;
            this.colSecondProd.Name = "colSecondProd";
            this.colSecondProd.ReadOnly = true;
            this.colSecondProd.Width = 300;
            // 
            // colSecondTotal
            // 
            this.colSecondTotal.DataPropertyName = "tag_cnt";
            this.colSecondTotal.HeaderText = "Total";
            this.colSecondTotal.MinimumWidth = 10;
            this.colSecondTotal.Name = "colSecondTotal";
            this.colSecondTotal.ReadOnly = true;
            this.colSecondTotal.Width = 200;
            // 
            // colSecondPass
            // 
            this.colSecondPass.DataPropertyName = "pass_cnt";
            this.colSecondPass.HeaderText = "Pass";
            this.colSecondPass.MinimumWidth = 10;
            this.colSecondPass.Name = "colSecondPass";
            this.colSecondPass.ReadOnly = true;
            this.colSecondPass.Width = 200;
            // 
            // colSecondReject
            // 
            this.colSecondReject.DataPropertyName = "reject_cnt";
            this.colSecondReject.HeaderText = "Reject";
            this.colSecondReject.MinimumWidth = 10;
            this.colSecondReject.Name = "colSecondReject";
            this.colSecondReject.ReadOnly = true;
            this.colSecondReject.Width = 200;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1948, 1320);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tag Statistics";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentTotal)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLastTotal)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSecond)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCurrentTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceLast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSecondLast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timeRefresh;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblCurrentShift;
        private System.Windows.Forms.DataGridView dgvCurrentTotal;
        private System.Windows.Forms.BindingSource bindingSourceCurrentTotal;
        private System.Windows.Forms.BindingSource bindingSourceLast;
        private System.Windows.Forms.BindingSource bindingSourceSecondLast;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblLastShift;
        private System.Windows.Forms.DataGridView dgvLastTotal;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.DataGridView dgvSecond;
        private System.Windows.Forms.Label lblSecond;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPass;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReject;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastProd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastPass;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastReject;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSecondProd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSecondTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSecondPass;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSecondReject;
    }
}

