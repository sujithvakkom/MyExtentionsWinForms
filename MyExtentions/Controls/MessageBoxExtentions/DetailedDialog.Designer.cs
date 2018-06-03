using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics.CodeAnalysis;

namespace  MyExtentions.Controls.MessageBoxExtentions
{
    public partial class DetailedDialog : Form
    {
        private TableLayoutPanel overarchingTableLayoutPanel;
        private TableLayoutPanel buttonTableLayoutPanel;
        private PictureBox pictureBox;
        private Label lblMessage;
        private Button detailsBtn;
        private Button cancelBtn;
        private Button okBtn;
        private TableLayoutPanel pictureLabelTableLayoutPanel;
        //private TextBox details;
        private Panel details;



        private void InitializeComponent()
        {
            this.detailsBtn = new System.Windows.Forms.Button();
            this.overarchingTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.pictureLabelTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.details = new System.Windows.Forms.Panel();
            this.overarchingTableLayoutPanel.SuspendLayout();
            this.buttonTableLayoutPanel.SuspendLayout();
            this.pictureLabelTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // detailsBtn
            // 
            this.detailsBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.detailsBtn.Location = new System.Drawing.Point(12, 3);
            this.detailsBtn.Margin = new System.Windows.Forms.Padding(12, 3, 29, 3);
            this.detailsBtn.Name = "detailsBtn";
            this.detailsBtn.Size = new System.Drawing.Size(100, 23);
            this.detailsBtn.TabIndex = 4;
            this.detailsBtn.Click += new System.EventHandler(this.DetailsClick);
            // 
            // overarchingTableLayoutPanel
            // 
            this.overarchingTableLayoutPanel.AutoSize = true;
            this.overarchingTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.overarchingTableLayoutPanel.ColumnCount = 1;
            this.overarchingTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.overarchingTableLayoutPanel.Controls.Add(this.buttonTableLayoutPanel, 0, 1);
            this.overarchingTableLayoutPanel.Controls.Add(this.pictureLabelTableLayoutPanel, 0, 0);
            this.overarchingTableLayoutPanel.Location = new System.Drawing.Point(1, 0);
            this.overarchingTableLayoutPanel.MinimumSize = new System.Drawing.Size(279, 50);
            this.overarchingTableLayoutPanel.Name = "overarchingTableLayoutPanel";
            this.overarchingTableLayoutPanel.RowCount = 2;
            this.overarchingTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.overarchingTableLayoutPanel.Size = new System.Drawing.Size(306, 111);
            this.overarchingTableLayoutPanel.TabIndex = 6;
            // 
            // buttonTableLayoutPanel
            // 
            this.buttonTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTableLayoutPanel.AutoSize = true;
            this.buttonTableLayoutPanel.ColumnCount = 3;
            this.overarchingTableLayoutPanel.SetColumnSpan(this.buttonTableLayoutPanel, 2);
            this.buttonTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.buttonTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.buttonTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.buttonTableLayoutPanel.Controls.Add(this.cancelBtn, 2, 0);
            this.buttonTableLayoutPanel.Controls.Add(this.okBtn, 1, 0);
            this.buttonTableLayoutPanel.Controls.Add(this.detailsBtn, 0, 0);
            this.buttonTableLayoutPanel.Location = new System.Drawing.Point(3, 79);
            this.buttonTableLayoutPanel.Name = "buttonTableLayoutPanel";
            this.buttonTableLayoutPanel.RowCount = 1;
            this.buttonTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.buttonTableLayoutPanel.Size = new System.Drawing.Size(300, 29);
            this.buttonTableLayoutPanel.TabIndex = 8;
            // 
            // cancelBtn
            // 
            this.cancelBtn.AutoSize = true;
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(222, 3);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // okBtn
            // 
            this.okBtn.AutoSize = true;
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Location = new System.Drawing.Point(144, 3);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 1;
            this.okBtn.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // pictureLabelTableLayoutPanel
            // 
            this.pictureLabelTableLayoutPanel.AutoSize = true;
            this.pictureLabelTableLayoutPanel.ColumnCount = 2;
            this.pictureLabelTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pictureLabelTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pictureLabelTableLayoutPanel.Controls.Add(this.lblMessage, 1, 0);
            this.pictureLabelTableLayoutPanel.Controls.Add(this.pictureBox, 0, 0);
            this.pictureLabelTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureLabelTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.pictureLabelTableLayoutPanel.Name = "pictureLabelTableLayoutPanel";
            this.pictureLabelTableLayoutPanel.RowCount = 1;
            this.pictureLabelTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pictureLabelTableLayoutPanel.Size = new System.Drawing.Size(300, 70);
            this.pictureLabelTableLayoutPanel.TabIndex = 4;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(73, 30);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(3, 30, 3, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 0;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(3, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(64, 64);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 5;
            this.pictureBox.TabStop = false;
            // 
            // details
            // 
            this.details.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.details.Location = new System.Drawing.Point(4, 114);
            this.details.Name = "details";
            this.details.Size = new System.Drawing.Size(290, 100);
            this.details.TabIndex = 3;
            this.details.Visible = false;
            // 
            // DetailedDialog
            // 
            this.AcceptButton = this.okBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(299, 219);
            this.Controls.Add(this.details);
            this.Controls.Add(this.overarchingTableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetailedDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.overarchingTableLayoutPanel.ResumeLayout(false);
            this.overarchingTableLayoutPanel.PerformLayout();
            this.buttonTableLayoutPanel.ResumeLayout(false);
            this.buttonTableLayoutPanel.PerformLayout();
            this.pictureLabelTableLayoutPanel.ResumeLayout(false);
            this.pictureLabelTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private TextBox textBoxDetails;
    }
}
