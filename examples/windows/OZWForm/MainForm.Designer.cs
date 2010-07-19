﻿namespace OZWForm
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.NodeGridView = new System.Windows.Forms.DataGridView();
            this.NodeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.requestNodeNeighborUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.NodeGridView)).BeginInit();
            this.NodeContextMenuStrip.SuspendLayout();
            this.MenuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // NodeGridView
            // 
            this.NodeGridView.AllowUserToAddRows = false;
            this.NodeGridView.AllowUserToDeleteRows = false;
            this.NodeGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.NodeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NodeGridView.ContextMenuStrip = this.NodeContextMenuStrip;
            this.NodeGridView.Location = new System.Drawing.Point(13, 37);
            this.NodeGridView.MultiSelect = false;
            this.NodeGridView.Name = "NodeGridView";
            this.NodeGridView.ReadOnly = true;
            this.NodeGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.NodeGridView.Size = new System.Drawing.Size(609, 343);
            this.NodeGridView.TabIndex = 0;
            this.NodeGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.NodeGridView_CellMouseDown);
            // 
            // NodeContextMenuStrip
            // 
            this.NodeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.requestNodeNeighborUpdateToolStripMenuItem});
            this.NodeContextMenuStrip.Name = "NodeContextMenuStrip";
            this.NodeContextMenuStrip.Size = new System.Drawing.Size(243, 26);
            // 
            // requestNodeNeighborUpdateToolStripMenuItem
            // 
            this.requestNodeNeighborUpdateToolStripMenuItem.Name = "requestNodeNeighborUpdateToolStripMenuItem";
            this.requestNodeNeighborUpdateToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.requestNodeNeighborUpdateToolStripMenuItem.Text = "Request Node Neighbor Update";
            this.requestNodeNeighborUpdateToolStripMenuItem.Click += new System.EventHandler(this.RequestNodeNeighborUpdateToolStripMenuItem_Click);
            // 
            // MenuBar
            // 
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem});
            this.MenuBar.Location = new System.Drawing.Point(0, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Size = new System.Drawing.Size(634, 24);
            this.MenuBar.TabIndex = 1;
            this.MenuBar.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Text = "&File";
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.SaveToolStripMenuItem.Text = "&Save";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 392);
            this.Controls.Add(this.NodeGridView);
            this.Controls.Add(this.MenuBar);
            this.MainMenuStrip = this.MenuBar;
            this.Name = "MainForm";
            this.Text = "OpenZWave Test";
            ((System.ComponentModel.ISupportInitialize)(this.NodeGridView)).EndInit();
            this.NodeContextMenuStrip.ResumeLayout(false);
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView NodeGridView;
        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip NodeContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem requestNodeNeighborUpdateToolStripMenuItem;
    }
}

