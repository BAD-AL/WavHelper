namespace WavHelper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.list_files = new System.Windows.Forms.ListBox();
            this.menu_list = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_deinterlace = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_findSimilar = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_copyFileName = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_openContainingFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_convertToMP3 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_convertToWav = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_showWavInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_usageInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.txt_path = new System.Windows.Forms.TextBox();
            this.lab_path = new System.Windows.Forms.Label();
            this.lab_filter = new System.Windows.Forms.Label();
            this.txt_filter = new System.Windows.Forms.TextBox();
            this.btn_listResults = new System.Windows.Forms.Button();
            this.spin_fileMax = new System.Windows.Forms.NumericUpDown();
            this.lab_kbLimit = new System.Windows.Forms.Label();
            this.lab_status = new System.Windows.Forms.Label();
            this.txt_selectedITem = new System.Windows.Forms.TextBox();
            this.txt_utility = new System.Windows.Forms.TextBox();
            this.lab_hint = new System.Windows.Forms.Label();
            this.txt_CopyToFolder = new System.Windows.Forms.TextBox();
            this.btn_copySelected = new System.Windows.Forms.Button();
            this.btn_browse = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.spin_fileMin = new System.Windows.Forms.NumericUpDown();
            this.check_showMP3 = new System.Windows.Forms.CheckBox();
            this.check_onlyWMP = new System.Windows.Forms.CheckBox();
            this.menu_list.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spin_fileMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_fileMin)).BeginInit();
            this.SuspendLayout();
            // 
            // list_files
            // 
            this.list_files.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.list_files.ContextMenuStrip = this.menu_list;
            this.list_files.FormattingEnabled = true;
            this.list_files.Location = new System.Drawing.Point(12, 68);
            this.list_files.Name = "list_files";
            this.list_files.Size = new System.Drawing.Size(318, 342);
            this.list_files.TabIndex = 0;
            this.list_files.SelectedIndexChanged += new System.EventHandler(this.list_files_SelectedIndexChanged);
            this.list_files.DoubleClick += new System.EventHandler(this.list_files_DoubleClick);
            // 
            // menu_list
            // 
            this.menu_list.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_deinterlace,
            this.menu_findSimilar,
            this.menu_copyFileName,
            this.menu_openContainingFolder,
            this.menu_convertToMP3,
            this.menu_convertToWav,
            this.menu_showWavInfo,
            this.menu_usageInfo});
            this.menu_list.Name = "menu_list";
            this.menu_list.Size = new System.Drawing.Size(229, 202);
            // 
            // menu_deinterlace
            // 
            this.menu_deinterlace.Name = "menu_deinterlace";
            this.menu_deinterlace.Size = new System.Drawing.Size(228, 22);
            this.menu_deinterlace.Text = "de-interlace";
            this.menu_deinterlace.Click += new System.EventHandler(this.menu_deinterlace_Click);
            // 
            // menu_findSimilar
            // 
            this.menu_findSimilar.Name = "menu_findSimilar";
            this.menu_findSimilar.Size = new System.Drawing.Size(228, 22);
            this.menu_findSimilar.Text = "Find similar";
            this.menu_findSimilar.Click += new System.EventHandler(this.menu_findSimilar_Click);
            // 
            // menu_copyFileName
            // 
            this.menu_copyFileName.Name = "menu_copyFileName";
            this.menu_copyFileName.Size = new System.Drawing.Size(228, 22);
            this.menu_copyFileName.Text = "Copy File name";
            this.menu_copyFileName.Click += new System.EventHandler(this.menu_copyFileName_Click);
            // 
            // menu_openContainingFolder
            // 
            this.menu_openContainingFolder.Name = "menu_openContainingFolder";
            this.menu_openContainingFolder.Size = new System.Drawing.Size(228, 22);
            this.menu_openContainingFolder.Text = "Open Containing Folder";
            this.menu_openContainingFolder.Click += new System.EventHandler(this.menu_openContainingFolder_Click);
            // 
            // menu_convertToMP3
            // 
            this.menu_convertToMP3.Name = "menu_convertToMP3";
            this.menu_convertToMP3.Size = new System.Drawing.Size(228, 22);
            this.menu_convertToMP3.Text = "Convert to MP3 (ffmpeg.exe)";
            this.menu_convertToMP3.Click += new System.EventHandler(this.menu_convertToMP3_Click);
            // 
            // menu_convertToWav
            // 
            this.menu_convertToWav.Name = "menu_convertToWav";
            this.menu_convertToWav.Size = new System.Drawing.Size(228, 22);
            this.menu_convertToWav.Text = "Convert to wav";
            this.menu_convertToWav.Click += new System.EventHandler(this.menu_convertToWav_Click);
            // 
            // menu_showWavInfo
            // 
            this.menu_showWavInfo.Name = "menu_showWavInfo";
            this.menu_showWavInfo.Size = new System.Drawing.Size(228, 22);
            this.menu_showWavInfo.Text = "Show basic sound data";
            this.menu_showWavInfo.Click += new System.EventHandler(this.menu_showWavInfo_Click);
            // 
            // menu_usageInfo
            // 
            this.menu_usageInfo.Name = "menu_usageInfo";
            this.menu_usageInfo.Size = new System.Drawing.Size(228, 22);
            this.menu_usageInfo.Text = "Show Full Usage Info";
            this.menu_usageInfo.Click += new System.EventHandler(this.menu_usageInfo_Click);
            // 
            // txt_path
            // 
            this.txt_path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_path.Location = new System.Drawing.Point(72, 4);
            this.txt_path.Name = "txt_path";
            this.txt_path.Size = new System.Drawing.Size(237, 20);
            this.txt_path.TabIndex = 1;
            this.txt_path.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_path_KeyDown);
            // 
            // lab_path
            // 
            this.lab_path.AutoSize = true;
            this.lab_path.Location = new System.Drawing.Point(20, 7);
            this.lab_path.Name = "lab_path";
            this.lab_path.Size = new System.Drawing.Size(29, 13);
            this.lab_path.TabIndex = 2;
            this.lab_path.Text = "Path";
            // 
            // lab_filter
            // 
            this.lab_filter.AutoSize = true;
            this.lab_filter.Location = new System.Drawing.Point(20, 33);
            this.lab_filter.Name = "lab_filter";
            this.lab_filter.Size = new System.Drawing.Size(48, 13);
            this.lab_filter.TabIndex = 4;
            this.lab_filter.Text = "File Filter";
            // 
            // txt_filter
            // 
            this.txt_filter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_filter.Location = new System.Drawing.Point(72, 30);
            this.txt_filter.Name = "txt_filter";
            this.txt_filter.Size = new System.Drawing.Size(237, 20);
            this.txt_filter.TabIndex = 3;
            this.txt_filter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_path_KeyDown);
            // 
            // btn_listResults
            // 
            this.btn_listResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_listResults.Location = new System.Drawing.Point(443, 4);
            this.btn_listResults.Name = "btn_listResults";
            this.btn_listResults.Size = new System.Drawing.Size(75, 23);
            this.btn_listResults.TabIndex = 5;
            this.btn_listResults.Text = "List Results";
            this.btn_listResults.UseVisualStyleBackColor = true;
            this.btn_listResults.Click += new System.EventHandler(this.btn_listResults_Click);
            // 
            // spin_fileMax
            // 
            this.spin_fileMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.spin_fileMax.Location = new System.Drawing.Point(443, 53);
            this.spin_fileMax.Maximum = new decimal(new int[] {
            200000,
            0,
            0,
            0});
            this.spin_fileMax.Name = "spin_fileMax";
            this.spin_fileMax.Size = new System.Drawing.Size(63, 20);
            this.spin_fileMax.TabIndex = 6;
            this.spin_fileMax.Value = new decimal(new int[] {
            200000,
            0,
            0,
            0});
            this.spin_fileMax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_path_KeyDown);
            // 
            // lab_kbLimit
            // 
            this.lab_kbLimit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_kbLimit.AutoSize = true;
            this.lab_kbLimit.Location = new System.Drawing.Point(440, 37);
            this.lab_kbLimit.Name = "lab_kbLimit";
            this.lab_kbLimit.Size = new System.Drawing.Size(43, 13);
            this.lab_kbLimit.TabIndex = 7;
            this.lab_kbLimit.Text = "KB max";
            // 
            // lab_status
            // 
            this.lab_status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lab_status.AutoSize = true;
            this.lab_status.Location = new System.Drawing.Point(9, 479);
            this.lab_status.Name = "lab_status";
            this.lab_status.Size = new System.Drawing.Size(35, 13);
            this.lab_status.TabIndex = 8;
            this.lab_status.Text = "status";
            // 
            // txt_selectedITem
            // 
            this.txt_selectedITem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_selectedITem.Location = new System.Drawing.Point(12, 416);
            this.txt_selectedITem.Name = "txt_selectedITem";
            this.txt_selectedITem.Size = new System.Drawing.Size(318, 20);
            this.txt_selectedITem.TabIndex = 9;
            // 
            // txt_utility
            // 
            this.txt_utility.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_utility.Location = new System.Drawing.Point(336, 161);
            this.txt_utility.Multiline = true;
            this.txt_utility.Name = "txt_utility";
            this.txt_utility.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_utility.Size = new System.Drawing.Size(182, 224);
            this.txt_utility.TabIndex = 10;
            // 
            // lab_hint
            // 
            this.lab_hint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_hint.AutoSize = true;
            this.lab_hint.Location = new System.Drawing.Point(336, 145);
            this.lab_hint.Name = "lab_hint";
            this.lab_hint.Size = new System.Drawing.Size(136, 13);
            this.lab_hint.TabIndex = 11;
            this.lab_hint.Text = "Double click list item to add";
            // 
            // txt_CopyToFolder
            // 
            this.txt_CopyToFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_CopyToFolder.Location = new System.Drawing.Point(111, 450);
            this.txt_CopyToFolder.Name = "txt_CopyToFolder";
            this.txt_CopyToFolder.Size = new System.Drawing.Size(318, 20);
            this.txt_CopyToFolder.TabIndex = 12;
            // 
            // btn_copySelected
            // 
            this.btn_copySelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_copySelected.Location = new System.Drawing.Point(12, 442);
            this.btn_copySelected.Name = "btn_copySelected";
            this.btn_copySelected.Size = new System.Drawing.Size(93, 34);
            this.btn_copySelected.TabIndex = 13;
            this.btn_copySelected.Text = "Copy Selected to Folder";
            this.btn_copySelected.UseVisualStyleBackColor = true;
            this.btn_copySelected.Click += new System.EventHandler(this.btn_copySelected_Click);
            // 
            // btn_browse
            // 
            this.btn_browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_browse.Location = new System.Drawing.Point(315, 2);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(75, 23);
            this.btn_browse.TabIndex = 14;
            this.btn_browse.Text = "Browse";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_stop.Location = new System.Drawing.Point(336, 77);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(75, 23);
            this.btn_stop.TabIndex = 15;
            this.btn_stop.Text = "Stop";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(440, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "KB min";
            // 
            // spin_fileMin
            // 
            this.spin_fileMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.spin_fileMin.Location = new System.Drawing.Point(443, 93);
            this.spin_fileMin.Maximum = new decimal(new int[] {
            200000,
            0,
            0,
            0});
            this.spin_fileMin.Name = "spin_fileMin";
            this.spin_fileMin.Size = new System.Drawing.Size(63, 20);
            this.spin_fileMin.TabIndex = 16;
            this.spin_fileMin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_path_KeyDown);
            // 
            // check_showMP3
            // 
            this.check_showMP3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.check_showMP3.AutoSize = true;
            this.check_showMP3.Location = new System.Drawing.Point(315, 37);
            this.check_showMP3.Name = "check_showMP3";
            this.check_showMP3.Size = new System.Drawing.Size(83, 17);
            this.check_showMP3.TabIndex = 18;
            this.check_showMP3.Text = "Show MP3s";
            this.check_showMP3.UseVisualStyleBackColor = true;
            // 
            // check_onlyWMP
            // 
            this.check_onlyWMP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.check_onlyWMP.AutoSize = true;
            this.check_onlyWMP.Location = new System.Drawing.Point(336, 125);
            this.check_onlyWMP.Name = "check_onlyWMP";
            this.check_onlyWMP.Size = new System.Drawing.Size(176, 17);
            this.check_onlyWMP.TabIndex = 19;
            this.check_onlyWMP.Text = "use only Windows Media Player";
            this.check_onlyWMP.UseVisualStyleBackColor = true;
            this.check_onlyWMP.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 497);
            this.Controls.Add(this.check_onlyWMP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.check_showMP3);
            this.Controls.Add(this.spin_fileMin);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_browse);
            this.Controls.Add(this.btn_copySelected);
            this.Controls.Add(this.txt_CopyToFolder);
            this.Controls.Add(this.lab_hint);
            this.Controls.Add(this.txt_utility);
            this.Controls.Add(this.txt_selectedITem);
            this.Controls.Add(this.lab_status);
            this.Controls.Add(this.lab_kbLimit);
            this.Controls.Add(this.btn_listResults);
            this.Controls.Add(this.spin_fileMax);
            this.Controls.Add(this.lab_filter);
            this.Controls.Add(this.txt_filter);
            this.Controls.Add(this.lab_path);
            this.Controls.Add(this.txt_path);
            this.Controls.Add(this.list_files);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Wav Helper";
            this.menu_list.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spin_fileMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_fileMin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox list_files;
        private System.Windows.Forms.TextBox txt_path;
        private System.Windows.Forms.Label lab_path;
        private System.Windows.Forms.Label lab_filter;
        private System.Windows.Forms.TextBox txt_filter;
        private System.Windows.Forms.Button btn_listResults;
        private System.Windows.Forms.NumericUpDown spin_fileMax;
        private System.Windows.Forms.Label lab_kbLimit;
        private System.Windows.Forms.Label lab_status;
        private System.Windows.Forms.TextBox txt_selectedITem;
        private System.Windows.Forms.TextBox txt_utility;
        private System.Windows.Forms.Label lab_hint;
        private System.Windows.Forms.TextBox txt_CopyToFolder;
        private System.Windows.Forms.Button btn_copySelected;
        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.ContextMenuStrip menu_list;
        private System.Windows.Forms.ToolStripMenuItem menu_deinterlace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown spin_fileMin;
        private System.Windows.Forms.ToolStripMenuItem menu_findSimilar;
        private System.Windows.Forms.ToolStripMenuItem menu_copyFileName;
        private System.Windows.Forms.ToolStripMenuItem menu_convertToMP3;
        private System.Windows.Forms.ToolStripMenuItem menu_openContainingFolder;
        private System.Windows.Forms.CheckBox check_showMP3;
        private System.Windows.Forms.ToolStripMenuItem menu_convertToWav;
        private System.Windows.Forms.CheckBox check_onlyWMP;
        private System.Windows.Forms.ToolStripMenuItem menu_showWavInfo;
        private System.Windows.Forms.ToolStripMenuItem menu_usageInfo;
    }
}

