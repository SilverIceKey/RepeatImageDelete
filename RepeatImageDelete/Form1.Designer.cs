using System;

namespace RepeatImageDelete
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
            this.selectImageFolder = new System.Windows.Forms.Button();
            this.qqDefault = new System.Windows.Forms.Button();
            this.originImages = new System.Windows.Forms.ListBox();
            this.repeatImages = new System.Windows.Forms.ListBox();
            this.originImage = new System.Windows.Forms.PictureBox();
            this.repeatImage = new System.Windows.Forms.PictureBox();
            this.deleteSelected = new System.Windows.Forms.Button();
            this.deleteAll = new System.Windows.Forms.Button();
            this.repeatNumLabel = new System.Windows.Forms.Label();
            this.deleteProgressBar = new System.Windows.Forms.ProgressBar();
            this.refreshCurrent = new System.Windows.Forms.Button();
            this.currentFileRepeat = new System.Windows.Forms.Label();
            this.next = new System.Windows.Forms.Button();
            this.deleteSelectedAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.originImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repeatImage)).BeginInit();
            this.SuspendLayout();
            // 
            // selectImageFolder
            // 
            this.selectImageFolder.Location = new System.Drawing.Point(13, 13);
            this.selectImageFolder.Name = "selectImageFolder";
            this.selectImageFolder.Size = new System.Drawing.Size(126, 23);
            this.selectImageFolder.TabIndex = 0;
            this.selectImageFolder.Text = "选择图片文件夹";
            this.selectImageFolder.UseVisualStyleBackColor = true;
            this.selectImageFolder.Click += new System.EventHandler(this.selectImageFolder_Click);
            // 
            // qqDefault
            // 
            this.qqDefault.Location = new System.Drawing.Point(146, 12);
            this.qqDefault.Name = "qqDefault";
            this.qqDefault.Size = new System.Drawing.Size(104, 23);
            this.qqDefault.TabIndex = 1;
            this.qqDefault.Text = "qq默认文件夹";
            this.qqDefault.UseVisualStyleBackColor = true;
            this.qqDefault.Click += new System.EventHandler(this.qqDefault_Click);
            // 
            // originImages
            // 
            this.originImages.FormattingEnabled = true;
            this.originImages.ItemHeight = 12;
            this.originImages.Location = new System.Drawing.Point(12, 247);
            this.originImages.Name = "originImages";
            this.originImages.Size = new System.Drawing.Size(434, 268);
            this.originImages.TabIndex = 2;
            this.originImages.SelectedIndexChanged += new System.EventHandler(this.originImages_SelectedIndexChanged);
            // 
            // repeatImages
            // 
            this.repeatImages.FormattingEnabled = true;
            this.repeatImages.ItemHeight = 12;
            this.repeatImages.Location = new System.Drawing.Point(453, 247);
            this.repeatImages.Name = "repeatImages";
            this.repeatImages.Size = new System.Drawing.Size(440, 268);
            this.repeatImages.TabIndex = 3;
            this.repeatImages.SelectedIndexChanged += new System.EventHandler(this.repeatImages_SelectedIndexChanged);
            // 
            // originImage
            // 
            this.originImage.Location = new System.Drawing.Point(13, 43);
            this.originImage.Name = "originImage";
            this.originImage.Size = new System.Drawing.Size(433, 198);
            this.originImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.originImage.TabIndex = 4;
            this.originImage.TabStop = false;
            // 
            // repeatImage
            // 
            this.repeatImage.Location = new System.Drawing.Point(453, 43);
            this.repeatImage.Name = "repeatImage";
            this.repeatImage.Size = new System.Drawing.Size(440, 198);
            this.repeatImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.repeatImage.TabIndex = 5;
            this.repeatImage.TabStop = false;
            // 
            // deleteSelected
            // 
            this.deleteSelected.Location = new System.Drawing.Point(257, 11);
            this.deleteSelected.Name = "deleteSelected";
            this.deleteSelected.Size = new System.Drawing.Size(104, 23);
            this.deleteSelected.TabIndex = 6;
            this.deleteSelected.Text = "删除选中";
            this.deleteSelected.UseVisualStyleBackColor = true;
            this.deleteSelected.Click += new System.EventHandler(this.deleteSelected_Click);
            // 
            // deleteAll
            // 
            this.deleteAll.Location = new System.Drawing.Point(541, 11);
            this.deleteAll.Name = "deleteAll";
            this.deleteAll.Size = new System.Drawing.Size(133, 23);
            this.deleteAll.TabIndex = 7;
            this.deleteAll.Text = "删除所有";
            this.deleteAll.UseVisualStyleBackColor = true;
            this.deleteAll.Click += new System.EventHandler(this.deleteAll_Click);
            // 
            // repeatNumLabel
            // 
            this.repeatNumLabel.AutoSize = true;
            this.repeatNumLabel.Location = new System.Drawing.Point(453, 522);
            this.repeatNumLabel.Name = "repeatNumLabel";
            this.repeatNumLabel.Size = new System.Drawing.Size(95, 12);
            this.repeatNumLabel.TabIndex = 8;
            this.repeatNumLabel.Text = "重复图片数量：0";
            // 
            // deleteProgressBar
            // 
            this.deleteProgressBar.Location = new System.Drawing.Point(453, 538);
            this.deleteProgressBar.Name = "deleteProgressBar";
            this.deleteProgressBar.Size = new System.Drawing.Size(440, 23);
            this.deleteProgressBar.TabIndex = 9;
            // 
            // refreshCurrent
            // 
            this.refreshCurrent.Location = new System.Drawing.Point(680, 11);
            this.refreshCurrent.Name = "refreshCurrent";
            this.refreshCurrent.Size = new System.Drawing.Size(132, 23);
            this.refreshCurrent.TabIndex = 10;
            this.refreshCurrent.Text = "刷新当前文件夹";
            this.refreshCurrent.UseVisualStyleBackColor = true;
            this.refreshCurrent.Click += new System.EventHandler(this.refreshCurrent_Click);
            // 
            // currentFileRepeat
            // 
            this.currentFileRepeat.AutoSize = true;
            this.currentFileRepeat.Location = new System.Drawing.Point(12, 522);
            this.currentFileRepeat.Name = "currentFileRepeat";
            this.currentFileRepeat.Size = new System.Drawing.Size(119, 12);
            this.currentFileRepeat.TabIndex = 11;
            this.currentFileRepeat.Text = "当前图片重复数量：0";
            // 
            // next
            // 
            this.next.Location = new System.Drawing.Point(818, 11);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(75, 23);
            this.next.TabIndex = 12;
            this.next.Text = "下一张";
            this.next.UseVisualStyleBackColor = true;
            this.next.Click += new System.EventHandler(this.next_Click);
            // 
            // deleteSelectedAll
            // 
            this.deleteSelectedAll.Location = new System.Drawing.Point(368, 11);
            this.deleteSelectedAll.Name = "deleteSelectedAll";
            this.deleteSelectedAll.Size = new System.Drawing.Size(167, 23);
            this.deleteSelectedAll.TabIndex = 13;
            this.deleteSelectedAll.Text = "删除当前图片所有重复";
            this.deleteSelectedAll.UseVisualStyleBackColor = true;
            this.deleteSelectedAll.Click += new System.EventHandler(this.deleteSelectedAll_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 573);
            this.Controls.Add(this.deleteSelectedAll);
            this.Controls.Add(this.next);
            this.Controls.Add(this.currentFileRepeat);
            this.Controls.Add(this.refreshCurrent);
            this.Controls.Add(this.deleteProgressBar);
            this.Controls.Add(this.repeatNumLabel);
            this.Controls.Add(this.deleteAll);
            this.Controls.Add(this.deleteSelected);
            this.Controls.Add(this.repeatImage);
            this.Controls.Add(this.originImage);
            this.Controls.Add(this.repeatImages);
            this.Controls.Add(this.originImages);
            this.Controls.Add(this.qqDefault);
            this.Controls.Add(this.selectImageFolder);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.originImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repeatImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selectImageFolder;
        private System.Windows.Forms.Button qqDefault;
        private System.Windows.Forms.ListBox originImages;
        private System.Windows.Forms.ListBox repeatImages;
        private System.Windows.Forms.PictureBox originImage;
        private System.Windows.Forms.PictureBox repeatImage;
        private System.Windows.Forms.Button deleteSelected;
        private System.Windows.Forms.Button deleteAll;
        private System.Windows.Forms.Label repeatNumLabel;
        private System.Windows.Forms.ProgressBar deleteProgressBar;
        private System.Windows.Forms.Button refreshCurrent;
        private System.Windows.Forms.Label currentFileRepeat;
        private System.Windows.Forms.Button next;
        private System.Windows.Forms.Button deleteSelectedAll;
    }
}

