namespace KepCom
{
    partial class Frm_OpcClient
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tsslblServerState = new System.Windows.Forms.Label();
            this.tsslblItemCount = new System.Windows.Forms.Label();
            this.tsslblGroupCount = new System.Windows.Forms.Label();
            this.tvwGroupList = new System.Windows.Forms.TreeView();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.btn_RefreshList = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_ServerName = new System.Windows.Forms.ComboBox();
            this.cmb_ServerNode = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_CurrentTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.opcServercontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.opcGroupcontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dgv_data = new System.Windows.Forms.DataGridView();
            this.Tag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.List_Items = new System.Windows.Forms.ListBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_data)).BeginInit();
            this.SuspendLayout();
            // 
            // tsslblServerState
            // 
            this.tsslblServerState.AutoSize = true;
            this.tsslblServerState.Location = new System.Drawing.Point(58, 106);
            this.tsslblServerState.Name = "tsslblServerState";
            this.tsslblServerState.Size = new System.Drawing.Size(41, 12);
            this.tsslblServerState.TabIndex = 0;
            this.tsslblServerState.Text = "label1";
            // 
            // tsslblItemCount
            // 
            this.tsslblItemCount.AutoSize = true;
            this.tsslblItemCount.Location = new System.Drawing.Point(341, 106);
            this.tsslblItemCount.Name = "tsslblItemCount";
            this.tsslblItemCount.Size = new System.Drawing.Size(41, 12);
            this.tsslblItemCount.TabIndex = 1;
            this.tsslblItemCount.Text = "label2";
            // 
            // tsslblGroupCount
            // 
            this.tsslblGroupCount.AutoSize = true;
            this.tsslblGroupCount.Location = new System.Drawing.Point(573, 106);
            this.tsslblGroupCount.Name = "tsslblGroupCount";
            this.tsslblGroupCount.Size = new System.Drawing.Size(41, 12);
            this.tsslblGroupCount.TabIndex = 2;
            this.tsslblGroupCount.Text = "label3";
            // 
            // tvwGroupList
            // 
            this.tvwGroupList.Location = new System.Drawing.Point(16, 135);
            this.tvwGroupList.Name = "tvwGroupList";
            this.tvwGroupList.Size = new System.Drawing.Size(206, 175);
            this.tvwGroupList.TabIndex = 3;
            this.tvwGroupList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvwGroupList_MouseDown);
            // 
            // btn_Connect
            // 
            this.btn_Connect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_Connect.Location = new System.Drawing.Point(354, 52);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(90, 23);
            this.btn_Connect.TabIndex = 17;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // btn_RefreshList
            // 
            this.btn_RefreshList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_RefreshList.Location = new System.Drawing.Point(354, 14);
            this.btn_RefreshList.Name = "btn_RefreshList";
            this.btn_RefreshList.Size = new System.Drawing.Size(90, 23);
            this.btn_RefreshList.TabIndex = 18;
            this.btn_RefreshList.Text = "Refresh List";
            this.btn_RefreshList.UseVisualStyleBackColor = true;
            this.btn_RefreshList.Click += new System.EventHandler(this.btn_RefreshList_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(48, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "Server Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(49, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "Server Node:";
            // 
            // cmb_ServerName
            // 
            this.cmb_ServerName.FormattingEnabled = true;
            this.cmb_ServerName.Location = new System.Drawing.Point(146, 55);
            this.cmb_ServerName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmb_ServerName.Name = "cmb_ServerName";
            this.cmb_ServerName.Size = new System.Drawing.Size(175, 20);
            this.cmb_ServerName.TabIndex = 13;
            // 
            // cmb_ServerNode
            // 
            this.cmb_ServerNode.FormattingEnabled = true;
            this.cmb_ServerNode.Location = new System.Drawing.Point(146, 13);
            this.cmb_ServerNode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmb_ServerNode.Name = "cmb_ServerNode";
            this.cmb_ServerNode.Size = new System.Drawing.Size(177, 20);
            this.cmb_ServerNode.TabIndex = 14;
            this.cmb_ServerNode.SelectedIndexChanged += new System.EventHandler(this.cmb_ServerNode_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.lbl_Status,
            this.toolStripStatusLabel4,
            this.lbl_CurrentTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 499);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(685, 22);
            this.statusStrip1.TabIndex = 19;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(84, 17);
            this.toolStripStatusLabel1.Text = "当前版本:V1.0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(104, 17);
            this.toolStripStatusLabel2.Text = "                        ";
            // 
            // lbl_Status
            // 
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(70, 17);
            this.lbl_Status.Text = "Connected";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(40, 17);
            this.toolStripStatusLabel4.Text = "        ";
            // 
            // lbl_CurrentTime
            // 
            this.lbl_CurrentTime.Name = "lbl_CurrentTime";
            this.lbl_CurrentTime.Size = new System.Drawing.Size(116, 17);
            this.lbl_CurrentTime.Text = "当前时间：00:00:00";
            // 
            // opcServercontextMenuStrip
            // 
            this.opcServercontextMenuStrip.Name = "opcServercontextMenuStrip";
            this.opcServercontextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // opcGroupcontextMenuStrip
            // 
            this.opcGroupcontextMenuStrip.Name = "opcGroupcontextMenuStrip";
            this.opcGroupcontextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // dgv_data
            // 
            this.dgv_data.AllowUserToAddRows = false;
            this.dgv_data.AllowUserToDeleteRows = false;
            this.dgv_data.AllowUserToResizeColumns = false;
            this.dgv_data.AllowUserToResizeRows = false;
            this.dgv_data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_data.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgv_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tag,
            this.Value,
            this.Time});
            this.dgv_data.Location = new System.Drawing.Point(16, 325);
            this.dgv_data.MultiSelect = false;
            this.dgv_data.Name = "dgv_data";
            this.dgv_data.ReadOnly = true;
            this.dgv_data.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_data.RowTemplate.Height = 23;
            this.dgv_data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_data.Size = new System.Drawing.Size(614, 153);
            this.dgv_data.TabIndex = 20;
            // 
            // Tag
            // 
            this.Tag.DataPropertyName = "Tag";
            this.Tag.HeaderText = "Tag";
            this.Tag.Name = "Tag";
            this.Tag.ReadOnly = true;
            // 
            // Value
            // 
            this.Value.DataPropertyName = "Value";
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            // 
            // Time
            // 
            this.Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Time.DataPropertyName = "Time";
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            // 
            // List_Items
            // 
            this.List_Items.FormattingEnabled = true;
            this.List_Items.ItemHeight = 12;
            this.List_Items.Location = new System.Drawing.Point(228, 135);
            this.List_Items.Name = "List_Items";
            this.List_Items.Size = new System.Drawing.Size(402, 172);
            this.List_Items.TabIndex = 21;
            this.List_Items.DoubleClick += new System.EventHandler(this.List_Items_DoubleClick);
            // 
            // Frm_OpcClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 521);
            this.Controls.Add(this.List_Items);
            this.Controls.Add(this.dgv_data);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.btn_RefreshList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_ServerName);
            this.Controls.Add(this.cmb_ServerNode);
            this.Controls.Add(this.tvwGroupList);
            this.Controls.Add(this.tsslblGroupCount);
            this.Controls.Add(this.tsslblItemCount);
            this.Controls.Add(this.tsslblServerState);
            this.Name = "Frm_OpcClient";
            this.Text = "OPC Client";
            this.Load += new System.EventHandler(this.Frm_OpcClient_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_data)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tsslblServerState;
        private System.Windows.Forms.Label tsslblItemCount;
        private System.Windows.Forms.Label tsslblGroupCount;
        private System.Windows.Forms.TreeView tvwGroupList;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.Button btn_RefreshList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_ServerName;
        private System.Windows.Forms.ComboBox cmb_ServerNode;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lbl_Status;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel lbl_CurrentTime;
        private System.Windows.Forms.ContextMenuStrip opcServercontextMenuStrip;
        private System.Windows.Forms.ContextMenuStrip opcGroupcontextMenuStrip;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dgv_data;
        private System.Windows.Forms.ListBox List_Items;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tag;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
    }
}

