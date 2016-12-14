namespace SHYSInterface
{
    partial class Form5
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.门店 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.门店类型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.结算方式 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.备注 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.门店,
            this.门店类型,
            this.结算方式,
            this.备注});
            this.dataGridView1.Location = new System.Drawing.Point(1, 54);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(855, 489);
            this.dataGridView1.TabIndex = 0;
            // 
            // 门店
            // 
            this.门店.HeaderText = "门店";
            this.门店.Name = "门店";
            // 
            // 门店类型
            // 
            this.门店类型.HeaderText = "门店类型";
            this.门店类型.Name = "门店类型";
            // 
            // 结算方式
            // 
            this.结算方式.HeaderText = "结算方式";
            this.结算方式.Name = "结算方式";
            // 
            // 备注
            // 
            this.备注.HeaderText = "备注";
            this.备注.Name = "备注";
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 555);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form5";
            this.Text = "药企药品基础信息传报";
            this.Load += new System.EventHandler(this.Form5_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 门店;
        private System.Windows.Forms.DataGridViewTextBoxColumn 门店类型;
        private System.Windows.Forms.DataGridViewTextBoxColumn 结算方式;
        private System.Windows.Forms.DataGridViewTextBoxColumn 备注;
    }
}