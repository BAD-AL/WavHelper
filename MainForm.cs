using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using Newtonsoft.Json;

namespace WavHelper
{
    public partial class MainForm : Form
    {
        private SoundPlayer snd_player = new SoundPlayer();
        private WMPLib.WindowsMediaPlayer mp3_player = new WMPLib.WindowsMediaPlayer();

        ToolTip tip = new ToolTip();

        public MainForm()
        {
            InitializeComponent();
            tip.SetToolTip(txt_path, "Paste or type path to top level folder containing .wav files, press 'Enter' to list files");
            tip.SetToolTip(txt_filter,"Reduce file results with a regular expression or file name");
            tip.SetToolTip(btn_listResults, "click to show wav files under specified folder");
            tip.SetToolTip(txt_utility, "Holds files that you double clicked on. YOU decide why they are important");
            tip.SetToolTip(lab_status, "Info on errors, status and stuff.");
            tip.SetToolTip(spin_fileMax, "Will limit results to file sizes under this number (KB)");
            tip.SetToolTip(lab_kbLimit, "Will limit results to file sizes under this number (KB)");
            tip.SetToolTip(txt_selectedITem, "Displays the path to current list item so you can copy, paste & stuff.");
            LoadUsageInfo();
        }

        private void btn_listResults_Click(object sender, EventArgs e)
        {
            ListFiles();
        }

        private void ListFiles()
        {
            Regex reg = new Regex("(?i)"+txt_filter.Text +"(?-i)");
            list_files.Items.Clear();
            DirectoryInfo dInfo = new DirectoryInfo(txt_path.Text);
            if (dInfo.Exists)
            {
                FileInfo[] files = dInfo.GetFiles("*.wav", SearchOption.AllDirectories);
                if (check_showMP3.Checked)
                {
                    FileInfo[] mp3s = dInfo.GetFiles("*.mp3", SearchOption.AllDirectories);
                    if (mp3s.Length > 0)
                    {
                        List<FileInfo> tmp = new List<FileInfo>(files.Length + mp3s.Length);
                        tmp.AddRange(files);
                        tmp.AddRange(mp3s);
                        tmp.Sort(new FileInfoComparer());
                        files = tmp.ToArray();
                    }
                }
                lab_status.Text = string.Format("Processing {0} files...", files.Length);

                List<FileInfoListItem> items = new List<FileInfoListItem>(files.Length);
                foreach (FileInfo info in files)
                {
                    if (reg.IsMatch(info.Name) && info.Length < (spin_fileMax.Value * 1024) && info.Length > (spin_fileMin.Value * 1024))
                    {
                        //list_files.Items.Add(new FileInfoListItem(info));
                        items.Add(new FileInfoListItem(info));
                    }
                }
                list_files.Items.AddRange(items.ToArray());
                lab_status.Text = String.Format("Added {0} files", items.Count);
            }
            else
            {
                lab_status.Text = String.Format("Folder: '{0}' does not exist.", dInfo.FullName);
            }

        }

        private void list_files_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileInfoListItem info = list_files.SelectedItem as FileInfoListItem;
            if (info != null)
            {
                SetListToolTip(info);
                PlayFile(info.FileInfo);
            }
        }

        private void SetListToolTip(FileInfoListItem info)
        {
            string toolTip = GetUsageInfo(info, true);
            tip.SetToolTip(list_files, toolTip);
        }

        private string GetUsageInfo(FileInfoListItem info, bool firstOnly)
        {
            string retVal = "";
            int count = 0;
            for(int i=0; i < mUsageInfo.Count; i++)
            {
                if (mUsageInfo[i].Name.StartsWith(info.FileInfo.Name, StringComparison.InvariantCultureIgnoreCase) ||
                    mUsageInfo[i].Alias.StartsWith(info.FileInfo.Name, StringComparison.InvariantCultureIgnoreCase)
                    )
                {
                    if (firstOnly && retVal == "")
                        retVal += mUsageInfo[i].GetInfo();
                    else if(!firstOnly)
                        retVal += mUsageInfo[i].GetInfo();
                    count++;
                }
            }
            retVal = "Referenced in " + count + " Files\r\n" + retVal;
            return retVal;
        }

        List<SoundReference> mUsageInfo = new List<SoundReference>();

        private void LoadUsageInfo()
        {
            string bf1File = "soundToolTipsBF1.json";
            string bf2File = "soundToolTipsBF2.json";
            mUsageInfo.Clear();
            List<SoundReference> bf1 = null;
            List<SoundReference> bf2 = null;
            if (File.Exists(bf1File))
            {
                string content = File.ReadAllText(bf1File);
                bf1 = JsonConvert.DeserializeObject(content, typeof(List<SoundReference>)) as List<SoundReference>;
                mUsageInfo.AddRange(bf1);
            }
            if (File.Exists(bf2File))
            {
                string content = File.ReadAllText(bf2File);
                bf2 = JsonConvert.DeserializeObject(content, typeof(List<SoundReference>)) as List<SoundReference>;
                mUsageInfo.AddRange(bf2);
            }
            mUsageInfo.Sort();
            if (mUsageInfo.Count > 0)
                menu_usageInfo.Visible = true;
            else
                menu_usageInfo.Visible = false;
        }

        private void PlayFile(FileInfo info)
        {
            if (info != null)
            {
                snd_player.Stop();
                mp3_player.controls.stop();

                txt_selectedITem.Text = info.FullName;
                
                try
                {
                    if (!check_onlyWMP.Checked && info.FullName.EndsWith(".wav", StringComparison.CurrentCultureIgnoreCase))
                    {
                        this.snd_player.SoundLocation = info.FullName;
                        this.snd_player.Play();
                    }
                    else 
                    {
                        mp3_player.URL = info.FullName;
                        mp3_player.controls.play();
                    }
                    lab_status.Text = "Playing: " + info.Name;
                }
                catch (Exception e)
                {
                    lab_status.Text = e.Message + " " + info.Name;
                }
            }
        }

        private void list_files_DoubleClick(object sender, EventArgs e)
        {
            FileInfoListItem item = list_files.SelectedItem as FileInfoListItem;
            if (item != null)
            {
                txt_utility.AppendText(item.FileInfo.FullName+"\r\n");
            }
        }

        private void txt_path_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ListFiles();
            }
        }

        private void btn_copySelected_Click(object sender, EventArgs e)
        {
            DirectoryInfo dInfo = new DirectoryInfo(txt_CopyToFolder.Text);
            if (dInfo.Exists)
            {
                FileInfoListItem item = list_files.SelectedItem as FileInfoListItem;
                FileInfo info = null;
                if (item != null)
                    info = item.FileInfo;
                if (File.Exists(info.FullName))
                {
                    try
                    {
                        info.CopyTo(String.Format("{0}\\{1}", dInfo.FullName, info.Name), true);
                    }
                    catch (Exception ex) 
                    {
                        lab_status.Text = String.Format("Error '{0}' ", ex.Message);
                    }
                }
                else 
                {
                    lab_status.Text = String.Format("File '{0}' is invalid", txt_selectedITem.Text);
                }
            }
            else
            {
                lab_status.Text = String.Format("Folder '{0}' is invalid", txt_CopyToFolder.Text);
            }
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = Directory.GetCurrentDirectory();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txt_path.Text = dlg.SelectedPath;
                ListFiles();
            }
            dlg.Dispose();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            snd_player.Stop();
            mp3_player.controls.stop();
        }

        private void menu_deinterlace_Click(object sender, EventArgs e)
        {
            string programName = "deinterlace.exe";
            if (!File.Exists(programName))
            {
                MessageBox.Show("Could not fine 'deinterlace.exe', place in same folder as WavHelper.");
                return;
            }
            FileInfoListItem item = list_files.SelectedItem as FileInfoListItem;
            if (item != null)
            {
                string arg = " --drop-extra " + item.FileInfo.FullName;
                string result = Program.RunCommandAndGetOutput(programName, arg, true);
                if (result.IndexOf("error", StringComparison.CurrentCultureIgnoreCase) > -1)
                {
                    MessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(result, "Looks Good :)");
                }
            }
        }

        private void menu_findSimilar_Click(object sender, EventArgs e)
        {
            FileInfoListItem item = list_files.SelectedItem as FileInfoListItem;
            if (item != null)
            {
                DirectoryInfo dInfo = new DirectoryInfo(txt_path.Text);
                if (dInfo.Exists)
                {
                    FileInfo[] fInfos = dInfo.GetFiles(item.FileInfo.Name, SearchOption.AllDirectories);
                    if (fInfos.Length > 0)
                    {
                        MainForm detailForm = new MainForm();
                        detailForm.txt_path.Text = this.txt_path.Text;
                        detailForm.txt_filter.Text = item.FileInfo.Name;
                        foreach (FileInfo info in fInfos)
                        {
                            detailForm.list_files.Items.Add(new FileInfoListItem(info));
                        }
                        detailForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("No similar results found under '" + txt_path.Text + "'");
                    }
                }
                else
                {
                    MessageBox.Show("Folder does not exist '" + txt_path.Text + "'");
                }
            }
        }

        private void menu_copyFileName_Click(object sender, EventArgs e)
        {
            FileInfoListItem item = list_files.SelectedItem as FileInfoListItem;
            if (item != null)
            {
                Clipboard.SetText(item.FileInfo.Name, TextDataFormat.Text);
                lab_status.Text = string.Format("'{0}' copied to clipboard.", item.FileInfo.Name);
            }
        }

        private static string EnsureTrailingSlash(string path)
        {
            string retVal = path;
            if (!retVal.EndsWith("\\"))
                retVal += "\\";
            return retVal;
        }

        private void menu_convertToMP3_Click(object sender, EventArgs e)
        {
            string programName = "ffmpeg.exe";
            if (!File.Exists(programName))
            {
                MessageBox.Show("Could not fine 'ffmpeg.exe', place in same folder as WavHelper.");
                return;
            }
            if (!Directory.Exists(txt_CopyToFolder.Text))
            {
                MessageBox.Show("Please Specify a valid output folder");
                return;
            }
            FileInfoListItem item = list_files.SelectedItem as FileInfoListItem;
            if (item != null)
            {
                string output = EnsureTrailingSlash(txt_CopyToFolder.Text) + item.FileInfo.Name.Replace(".wav", ".mp3");
                /*if (File.Exists(output))
                {
                    MessageBox.Show(String.Format("File '{0}' already exists.", output));
                    return;
                }*/
                string arg = String.Format(" -y -i {0} {1}", item.FileInfo.FullName, output);
                string result = Program.RunCommandAndGetOutput(programName, arg, true);
                if (result.IndexOf("error", StringComparison.CurrentCultureIgnoreCase) > -1)
                {
                    MessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(result, "Looks Good :)");
                }
            }
        }

        private void menu_openContainingFolder_Click(object sender, EventArgs e)
        {
            FileInfoListItem item = list_files.SelectedItem as FileInfoListItem;
            if (item != null)
            {
                Program.RunCommand("explorer.exe", item.FileInfo.Directory.FullName);
            }
        }

        private void menu_convertToWav_Click(object sender, EventArgs e)
        {

            string programName = "ffmpeg.exe";
            if (!File.Exists(programName))
            {
                MessageBox.Show("Could not fine 'ffmpeg.exe', place in same folder as WavHelper.");
                return;
            }
            if (!Directory.Exists(txt_CopyToFolder.Text))
            {
                MessageBox.Show("Please Specify a valid output folder");
                return;
            }
            FileInfoListItem item = list_files.SelectedItem as FileInfoListItem;
            if (item != null)
            {
                string output = EnsureTrailingSlash(txt_CopyToFolder.Text) + item.FileInfo.Name.Replace(".wav", ".wav");
                if (item.FileInfo.Name.EndsWith(".mp3", StringComparison.CurrentCultureIgnoreCase))
                    output = EnsureTrailingSlash(txt_CopyToFolder.Text) + item.FileInfo.Name.Replace(".mp3", ".wav");
                string tmp = output;
                /*if (File.Exists(tmp)) 
                {
                    for (int i = 0; i < 100; i++)
                    {
                        tmp = output.Replace(".wav", string.Format("__{0}.wav",i));
                    }
                    output = tmp;
                    if (File.Exists(output))
                    {
                        MessageBox.Show("File already exists");
                        return;
                    }
                }*/
                string arg = String.Format(" -y -i {0} {1}", item.FileInfo.FullName, output);
                Program.RunCommand(programName, arg);
                lab_status.Text = "Saved to: " + output;
            }
        }

        private void menu_showWavInfo_Click(object sender, EventArgs e)
        {

            string programName = "ffprobe.exe";
            if (!File.Exists(programName))
            {
                MessageBox.Show("Could not fine 'ffprobe.exe', place in same folder as WavHelper.");
                return;
            }
            FileInfoListItem item = list_files.SelectedItem as FileInfoListItem;
            if (item != null)
            {
                string msg = Program.RunCommandAndGetOutput(programName, item.FileInfo.FullName, true);
                int index = msg.IndexOf("Input");
                if (index > -1)
                {
                    msg = msg.Substring(index);
                }
                MessageBox.Show(msg);
            }
        }

        private void menu_usageInfo_Click(object sender, EventArgs e)
        {
            FileInfoListItem info = list_files.SelectedItem as FileInfoListItem;
            if (info != null)
            {
                string usageInfo = GetUsageInfo(info, false);
                MessageForm.ShowMessage(info.FileInfo.Name, usageInfo);
            }
        }
    }

    public class FileInfoComparer : IComparer<FileInfo>
    {

        #region IComparer<FileInfo> Members

        public int Compare(FileInfo x, FileInfo y)
        {
            return x.FullName.CompareTo(y.FullName);
        }

        #endregion
    }

    public class FileInfoListItem
    {

        FileInfo mInfo = null;

        public FileInfo FileInfo
        {
            get { return mInfo; }
            private set { mInfo = value; }
        } 
        public FileInfoListItem(FileInfo info)
        {
            mInfo = info;
        }

        public override string ToString()
        {
            string retVal = "NULL";
            if(mInfo != null)
                retVal = string.Format("{0}: {1} KB", mInfo.FullName, mInfo.Length/1024);
            return retVal;
        }
    }
}
