﻿namespace KepCom
{
    partial class Form1
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
            this.opcServercontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.opcGroupcontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnStart = new System.Windows.Forms.Button();
            this.lstviewItems = new System.Windows.Forms.ListView();
            this.lbShow = new System.Windows.Forms.Label();
            this.dgvShowValue = new System.Windows.Forms.DataGridView();
            this.Tag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeStap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowValue)).BeginInit();
            this.SuspendLayout();
            // 
            // tsslblServerState
            // 
            this.tsslblServerState.AutoSize = true;
            this.tsslblServerState.Location = new System.Drawing.Point(61, 28);
            this.tsslblServerState.Name = "tsslblServerState";
            this.tsslblServerState.Size = new System.Drawing.Size(41, 12);
            this.tsslblServerState.TabIndex = 0;
            this.tsslblServerState.Text = "label1";
            // 
            // tsslblItemCount
            // 
            this.tsslblItemCount.AutoSize = true;
            this.tsslblItemCount.Location = new System.Drawing.Point(344, 28);
            this.tsslblItemCount.Name = "tsslblItemCount";
            this.tsslblItemCount.Size = new System.Drawing.Size(41, 12);
            this.tsslblItemCount.TabIndex = 1;
            this.tsslblItemCount.Text = "label2";
            // 
            // tsslblGroupCount
            // 
            this.tsslblGroupCount.AutoSize = true;
            this.tsslblGroupCount.Location = new System.Drawing.Point(576, 28);
            this.tsslblGroupCount.Name = "tsslblGroupCount";
            this.tsslblGroupCount.Size = new System.Drawing.Size(41, 12);
            this.tsslblGroupCount.TabIndex = 2;
            this.tsslblGroupCount.Text = "label3";
            // 
            // tvwGroupList
            // 
            this.tvwGroupList.Location = new System.Drawing.Point(54, 101);
            this.tvwGroupList.Name = "tvwGroupList";
            this.tvwGroupList.Size = new System.Drawing.Size(252, 175);
            this.tvwGroupList.TabIndex = 3;
            this.tvwGroupList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvwGroupList_MouseDown);
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
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(549, 64);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "开始加载";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lstviewItems
            // 
            this.lstviewItems.Location = new System.Drawing.Point(312, 101);
            this.lstviewItems.Name = "lstviewItems";
            this.lstviewItems.Size = new System.Drawing.Size(281, 175);
            this.lstviewItems.TabIndex = 7;
            this.lstviewItems.UseCompatibleStateImageBehavior = false;
            this.lstviewItems.View = System.Windows.Forms.View.List;
            this.lstviewItems.SelectedIndexChanged += new System.EventHandler(this.lstviewItems_SelectedIndexChanged);
            // 
            // lbShow
            // 
            this.lbShow.AutoSize = true;
            this.lbShow.Location = new System.Drawing.Point(54, 322);
            this.lbShow.Name = "lbShow";
            this.lbShow.Size = new System.Drawing.Size(41, 12);
            this.lbShow.TabIndex = 9;
            this.lbShow.Text = "label1";
            // 
            // dgvShowValue
            // 
            this.dgvShowValue.AllowUserToOrderColumns = true;
            this.dgvShowValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShowValue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tag,
            this.Value,
            this.TimeStap});
            this.dgvShowValue.Location = new System.Drawing.Point(599, 101);
            this.dgvShowValue.Name = "dgvShowValue";
            this.dgvShowValue.RowTemplate.Height = 23;
            this.dgvShowValue.Size = new System.Drawing.Size(347, 175);
            this.dgvShowValue.TabIndex = 10;
            // 
            // Tag
            // 
            this.Tag.HeaderText = "Tag";
            this.Tag.Name = "Tag";
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // TimeStap
            // 
            this.TimeStap.HeaderText = "TimeStap";
            this.TimeStap.Name = "TimeStap";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 511);
            this.Controls.Add(this.dgvShowValue);
            this.Controls.Add(this.lbShow);
            this.Controls.Add(this.lstviewItems);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.tvwGroupList);
            this.Controls.Add(this.tsslblGroupCount);
            this.Controls.Add(this.tsslblItemCount);
            this.Controls.Add(this.tsslblServerState);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tsslblServerState;
        private System.Windows.Forms.Label tsslblItemCount;
        private System.Windows.Forms.Label tsslblGroupCount;
        private System.Windows.Forms.TreeView tvwGroupList;
        private System.Windows.Forms.ContextMenuStrip opcServercontextMenuStrip;
        private System.Windows.Forms.ContextMenuStrip opcGroupcontextMenuStrip;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListView lstviewItems;
        private System.Windows.Forms.Label lbShow;
        private System.Windows.Forms.DataGridView dgvShowValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tag;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeStap;
    }
}

