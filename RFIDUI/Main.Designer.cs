namespace RFIDUI
{
    partial class Main
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
            this.btn_open = new System.Windows.Forms.Button();
            this.txt_ip = new System.Windows.Forms.TextBox();
            this.txt_port = new System.Windows.Forms.TextBox();
            this.dg_data = new System.Windows.Forms.DataGridView();
            this.col_epc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_rssi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_temperature = new System.Windows.Forms.TextBox();
            this.btn_inventory = new System.Windows.Forms.Button();
            this.txt_count = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dg_data)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(210, 10);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(75, 23);
            this.btn_open.TabIndex = 0;
            this.btn_open.Text = "打开";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // txt_ip
            // 
            this.txt_ip.Location = new System.Drawing.Point(12, 12);
            this.txt_ip.Name = "txt_ip";
            this.txt_ip.Size = new System.Drawing.Size(106, 21);
            this.txt_ip.TabIndex = 1;
            this.txt_ip.Text = "192.168.100.165";
            // 
            // txt_port
            // 
            this.txt_port.Location = new System.Drawing.Point(124, 12);
            this.txt_port.Name = "txt_port";
            this.txt_port.Size = new System.Drawing.Size(80, 21);
            this.txt_port.TabIndex = 2;
            this.txt_port.Text = "9761";
            // 
            // dg_data
            // 
            this.dg_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_epc,
            this.col_port,
            this.col_rssi,
            this.col_data,
            this.col_time});
            this.dg_data.Location = new System.Drawing.Point(13, 40);
            this.dg_data.Name = "dg_data";
            this.dg_data.RowTemplate.Height = 23;
            this.dg_data.Size = new System.Drawing.Size(649, 361);
            this.dg_data.TabIndex = 3;
            // 
            // col_epc
            // 
            this.col_epc.DataPropertyName = "EPC";
            this.col_epc.HeaderText = "EPC";
            this.col_epc.Name = "col_epc";
            this.col_epc.Width = 160;
            // 
            // col_port
            // 
            this.col_port.DataPropertyName = "PORT";
            this.col_port.HeaderText = "Port";
            this.col_port.Name = "col_port";
            this.col_port.Width = 50;
            // 
            // col_rssi
            // 
            this.col_rssi.DataPropertyName = "RSSI";
            this.col_rssi.HeaderText = "RSSI";
            this.col_rssi.Name = "col_rssi";
            this.col_rssi.Width = 60;
            // 
            // col_data
            // 
            this.col_data.DataPropertyName = "Data";
            this.col_data.HeaderText = "Data";
            this.col_data.Name = "col_data";
            // 
            // col_time
            // 
            this.col_time.DataPropertyName = "Time";
            this.col_time.HeaderText = "Time";
            this.col_time.Name = "col_time";
            this.col_time.Width = 130;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(596, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "温度：";
            // 
            // txt_temperature
            // 
            this.txt_temperature.Location = new System.Drawing.Point(643, 12);
            this.txt_temperature.Name = "txt_temperature";
            this.txt_temperature.ReadOnly = true;
            this.txt_temperature.Size = new System.Drawing.Size(100, 21);
            this.txt_temperature.TabIndex = 5;
            // 
            // btn_inventory
            // 
            this.btn_inventory.Enabled = false;
            this.btn_inventory.Location = new System.Drawing.Point(668, 40);
            this.btn_inventory.Name = "btn_inventory";
            this.btn_inventory.Size = new System.Drawing.Size(75, 23);
            this.btn_inventory.TabIndex = 6;
            this.btn_inventory.Text = "打开盘点";
            this.btn_inventory.UseVisualStyleBackColor = true;
            this.btn_inventory.Click += new System.EventHandler(this.btn_inventory_Click);
            // 
            // txt_count
            // 
            this.txt_count.Location = new System.Drawing.Point(400, 12);
            this.txt_count.Name = "txt_count";
            this.txt_count.ReadOnly = true;
            this.txt_count.Size = new System.Drawing.Size(100, 21);
            this.txt_count.TabIndex = 7;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 413);
            this.Controls.Add(this.txt_count);
            this.Controls.Add(this.btn_inventory);
            this.Controls.Add(this.txt_temperature);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dg_data);
            this.Controls.Add(this.txt_port);
            this.Controls.Add(this.txt_ip);
            this.Controls.Add(this.btn_open);
            this.Name = "Main";
            this.Text = "RFID测试";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dg_data)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.TextBox txt_ip;
        private System.Windows.Forms.TextBox txt_port;
        private System.Windows.Forms.DataGridView dg_data;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_temperature;
        private System.Windows.Forms.Button btn_inventory;
        private System.Windows.Forms.TextBox txt_count;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_epc;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_port;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_rssi;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_time;
    }
}