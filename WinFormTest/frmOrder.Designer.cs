namespace WinFormTest
{
    partial class frmOrder
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpStation = new System.Windows.Forms.TabPage();
            this.tpScanceRecorde = new System.Windows.Forms.TabPage();
            this.dgvScaneList = new System.Windows.Forms.DataGridView();
            this.tpErrorList1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1.SuspendLayout();
            this.tpStation.SuspendLayout();
            this.tpScanceRecorde.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScaneList)).BeginInit();
            this.tpErrorList1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 4);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1156, 512);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpStation);
            this.tabControl1.Controls.Add(this.tpScanceRecorde);
            this.tabControl1.Controls.Add(this.tpErrorList1);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabControl1.Location = new System.Drawing.Point(2, 142);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 3;
            this.tabControl1.Size = new System.Drawing.Size(1170, 553);
            this.tabControl1.TabIndex = 3;
            // 
            // tpStation
            // 
            this.tpStation.Controls.Add(this.flowLayoutPanel1);
            this.tpStation.Location = new System.Drawing.Point(4, 29);
            this.tpStation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpStation.Name = "tpStation";
            this.tpStation.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpStation.Size = new System.Drawing.Size(1162, 520);
            this.tpStation.TabIndex = 0;
            this.tpStation.Text = "生产工位";
            this.tpStation.UseVisualStyleBackColor = true;
            // 
            // tpScanceRecorde
            // 
            this.tpScanceRecorde.Controls.Add(this.dgvScaneList);
            this.tpScanceRecorde.Location = new System.Drawing.Point(4, 29);
            this.tpScanceRecorde.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpScanceRecorde.Name = "tpScanceRecorde";
            this.tpScanceRecorde.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpScanceRecorde.Size = new System.Drawing.Size(1162, 520);
            this.tpScanceRecorde.TabIndex = 1;
            this.tpScanceRecorde.Text = "采集记录";
            this.tpScanceRecorde.UseVisualStyleBackColor = true;
            // 
            // dgvScaneList
            // 
            this.dgvScaneList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScaneList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvScaneList.Location = new System.Drawing.Point(3, 4);
            this.dgvScaneList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvScaneList.Name = "dgvScaneList";
            this.dgvScaneList.Size = new System.Drawing.Size(1156, 512);
            this.dgvScaneList.TabIndex = 0;
            this.dgvScaneList.Text = "dataGridView1";
            // 
            // tpErrorList1
            // 
            this.tpErrorList1.Controls.Add(this.dataGridView1);
            this.tpErrorList1.Location = new System.Drawing.Point(4, 29);
            this.tpErrorList1.Name = "tpErrorList1";
            this.tpErrorList1.Padding = new System.Windows.Forms.Padding(3);
            this.tpErrorList1.Size = new System.Drawing.Size(1162, 520);
            this.tpErrorList1.TabIndex = 3;
            this.tpErrorList1.Text = "错误记录";
            this.tpErrorList1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1156, 514);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.Text = "dataGridView2";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(6, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1152, 114);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "订单信息";
            // 
            // frmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 694);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmOrder";
            this.Text = "订单列表";
            this.tabControl1.ResumeLayout(false);
            this.tpStation.ResumeLayout(false);
            this.tpScanceRecorde.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvScaneList)).EndInit();
            this.tpErrorList1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpStation;
        private System.Windows.Forms.TabPage tpScanceRecorde;
        private System.Windows.Forms.DataGridView dgvScaneList;
        private System.Windows.Forms.TabPage tpErrorList1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}