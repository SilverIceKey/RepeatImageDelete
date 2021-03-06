﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Security.Cryptography;

namespace RepeatImageDelete
{
    public partial class Form1 : Form
    {
        private SynchronizationContext _syncContext;
        private string folderPath;
        private Dictionary<string, string> originImagesList;
        private Dictionary<string, List<string>> repeatImageList;
        private List<object> selectedRepeatItem = new List<object>();
        private int selectedRepeatItemIndex = 0;
        private int repeatNum = 0;
        private int currentSelectedRepeatNum = 0;
        private int allFileNum = 0;
        private int scanNum = 0;

        public Form1()
        {
            InitializeComponent();
            _syncContext = SynchronizationContext.Current;
            checkButtonStatus("");
        }

        private void selectImageFolder_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件夹";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                originImages.Items.Clear();
                repeatImages.Items.Clear();
                folderPath = dialog.SelectedPath;
                deleteProgressBar.Value = 0;
                scanNum = 0;
                var task1 = new Task(() =>
                {
                    repeatImageList = new Dictionary<string, List<string>>();
                    originImagesList = new Dictionary<string, string>();
                    repeatNum = 0;
                    allFileNum = GetFilesCount(new DirectoryInfo(folderPath));
                    listFileOrFolder(new DirectoryInfo(folderPath));
                    MessageBox.Show("扫描完成");
                    _syncContext.Post(setProgress, 0);
                    _syncContext.Post(checkButtonStatus, "");
                });
                task1.Start();
            }
        }

        private void qqDefault_Click(object sender, EventArgs e)
        {
            folderPath = "C:\\Users\\zhouw\\Documents\\Tencent Files";
            originImages.Items.Clear();
            repeatImages.Items.Clear();
            deleteProgressBar.Value = 0;
            scanNum = 0;
            var task1 = new Task(() =>
            {
                repeatImageList = new Dictionary<string, List<string>>();
                originImagesList = new Dictionary<string, string>();
                repeatNum = 0;
                allFileNum = GetFilesCount(new DirectoryInfo(folderPath));
                listFileOrFolder(new DirectoryInfo(folderPath));
                MessageBox.Show("扫描完成");
                _syncContext.Post(setProgress, 0);
                _syncContext.Post(checkButtonStatus, "");
            });
            task1.Start();
        }

        private void listFileOrFolder(DirectoryInfo fileSystemInfo)
        {
            if (!fileSystemInfo.Exists) return;
            var dir = fileSystemInfo.GetDirectories();
            if (dir.Length != 0)
            {
                foreach (var item in dir)
                {
                    listFileOrFolder(new DirectoryInfo(item.FullName));
                }
            }
            foreach (var item in fileSystemInfo.GetFileSystemInfos())
            {
                if (File.Exists(item.FullName))
                {
                    if (isVideoOn.Checked)
                    {
                        if (item.Extension == ".jpg" || item.Extension == ".gif" || item.Extension == ".png" ||
                            item.Extension == ".bmp" || item.Extension == ".mp4" || item.Extension == ".avi" ||
                            item.Extension == ".flv") dealFile(item);
                    }
                    else
                    {
                        if (item.Extension == ".jpg" || item.Extension == ".gif" || item.Extension == ".png" ||
                            item.Extension == ".bmp") dealFile(item);
                    }
                }
            }
        }

        private void dealFile(FileSystemInfo item)
        {
            if (originImagesList.Count <= 0)
            {
                originImagesList.Add(item.FullName, item.FullName);
                _syncContext.Post(setProgress, Convert.ToDouble(scanNum) / Convert.ToDouble(allFileNum) * 100);
                _syncContext.Post(originImagesAddItem, item.FullName);
                scanNum++;
            }
            else
            {
                string originImageName = "";
                double score = 0;
                foreach (var keyValuePair in originImagesList)
                {
                    double calcScore = ImageHashUtils.CalcSimilarDegree(keyValuePair.Value, item.FullName);
                    if (calcScore > score)
                    {
                        score = calcScore;
                        originImageName = keyValuePair.Value;
                    }
                }

                if (score >= 95)
                {
                    if (repeatImageList.ContainsKey(originImageName))
                    {
                        if (!repeatImageList[originImageName].Contains(item.FullName))
                            repeatImageList[originImageName].Add(item.FullName);
                    }
                    else
                    {
                        var repeatImagesList = new List<string>();
                        repeatImagesList.Add(item.FullName);
                        repeatImageList.Add(originImageName, repeatImagesList);
                    }
                    repeatNum++;
                    _syncContext.Post(setRepeatNumLabel, "重复图片数量：" + repeatNum);
                    _syncContext.Post(repeatImagesAddItem, item.FullName);
                }
                else
                {
                    originImagesList.Add(item.FullName, item.FullName);
                    _syncContext.Post(setProgress, Convert.ToDouble(scanNum) / Convert.ToDouble(allFileNum) * 100);
                    _syncContext.Post(originImagesAddItem, item.FullName);
                    scanNum++;
                }
            }
        }

        private void originImages_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var index = originImages.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches) openFileInExplorer(originImages.SelectedItem.ToString());
        }

        private void repeatImages_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var index = repeatImages.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches) openFileInExplorer(repeatImages.SelectedItem.ToString());
        }

        private void originImagesAddItem(object path)
        {
            originImages.Items.Add(path.ToString());
        }

        private void openFileInExplorer(string filepath)
        {
            if (!File.Exists(filepath)) return;

            var psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            psi.Arguments = " /select," + filepath;
            System.Diagnostics.Process.Start(psi);
        }

        private void setRepeatNumLabel(object text)
        {
            repeatNumLabel.Text = text.ToString();
        }

        private void repeatImagesAddItem(object path)
        {
            repeatImages.Items.Add(path.ToString());
        }

        private void repeatImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (repeatImages.SelectedItem == null) return;
            foreach (var images in repeatImageList)
            foreach (var image in images.Value)
                if (image.Equals(repeatImages.SelectedItem.ToString()))
                    originImages.SelectedItem = originImagesList[images.Key];
        }

        private void originImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (originImages.SelectedItem == null) return;
            originImage.Load(originImages.SelectedItem.ToString());
            originFileSize.Text = getFileSize(originImages.SelectedItem.ToString());
            if (repeatImageList.ContainsKey(originImages.SelectedItem.ToString()))
            {
                currentSelectedRepeatNum = 0;
                selectedRepeatItemIndex = 0;
                repeatImages.ClearSelected();
                selectedRepeatItem.Clear();
                foreach (var item in repeatImages.Items)
                    if (repeatImageList[originImages.SelectedItem.ToString()].Contains(item.ToString()))
                    {
                        selectedRepeatItem.Add(item);
                        currentSelectedRepeatNum++;
                    }

                if (selectedRepeatItem.Count == 0)
                {
                    repeatImage.Image = null;
                    currentFileRepeat.Text = "当前图片重复数量：" + 0;
                    MessageBox.Show("该图片没有重复");
                    return;
                }

                checkButtonStatus("");
                repeatImages.SelectedItem = selectedRepeatItem[0];
                repeatImageBoxLoad(selectedRepeatItem[0].ToString());
                repeatFileSize.Text = getFileSize(selectedRepeatItem[0].ToString());
                currentFileRepeat.Text = "当前图片重复数量：" + currentSelectedRepeatNum;
            }
            else
            {
                selectedRepeatItem.Clear();
                selectedRepeatItemIndex = 0;
                repeatImage.Image = null;
                currentFileRepeat.Text = "当前图片重复数量：" + 0;
            }
        }

        private void deleteSelected_Click(object sender, EventArgs e)
        {
            if (selectedRepeatItem.Count == 0) return;

            selectedRepeatItem.RemoveAt(selectedRepeatItemIndex);
            if (selectedRepeatItem.Count != 0)
            {
                selectedRepeatItemIndex = selectedRepeatItemIndex % selectedRepeatItem.Count;
                repeatImageBoxLoad(selectedRepeatItem[selectedRepeatItemIndex % selectedRepeatItem.Count].ToString());
            }
            else
            {
                repeatImage.Image.Dispose();
            }
            var image = repeatImages.SelectedItem.ToString();
            if (File.Exists(image))
            {
                File.Delete(image);
                repeatNum--;
                repeatImages.Items.Remove(repeatImages.SelectedItem);
                if (selectedRepeatItem.Count != 0)
                    repeatImages.SelectedItem = selectedRepeatItem[selectedRepeatItemIndex % selectedRepeatItem.Count];
                _syncContext.Post(setRepeatNumLabel, "重复图片数量：" + repeatNum);
                currentFileRepeat.Text = "当前图片重复数量：" + selectedRepeatItem.Count;
            }

            if (Directory.Exists(image.Substring(0, image.LastIndexOf("\\"))) &&
                new DirectoryInfo(image.Substring(0, image.LastIndexOf("\\"))).GetFileSystemInfos().Length == 0)
                Directory.Delete(image.Substring(0, image.LastIndexOf("\\")));
        }

        private string getFileSize(string filepath)
        {
            var fileSizeStr = "当前文件大小";
            var fileInfo = new FileInfo(filepath);
            var unit = 0;
            var fileSize = Math.Round(Convert.ToDouble(fileInfo.Length), 2);
            while (fileSize > 1024)
            {
                fileSize = Math.Round(fileSize / 1024.0, 2);
                unit++;
            }

            fileSizeStr += fileSize;
            switch (unit)
            {
                case 0:
                    fileSizeStr += "B";
                    break;
                case 1:
                    fileSizeStr += "KB";
                    break;
                case 2:
                    fileSizeStr += "MB";
                    break;
                case 3:
                    fileSizeStr += "GB";
                    break;
                case 4:
                    fileSizeStr += "TB";
                    break;
                case 5:
                    fileSizeStr += "PB";
                    break;
                default:
                    break;
            }

            return fileSizeStr;
        }

        private void deleteAll_Click(object sender, EventArgs e)
        {
            repeatImage.Image = null;
            var task1 = new Task(() =>
            {
                double allRepeatNum = repeatNum;
                double deleteNum = 0;
                var repeatImgs = new List<string>();
                foreach (var item in repeatImages.Items) repeatImgs.Add(item.ToString());
                foreach (var image in repeatImgs)
                {
                    if (File.Exists(image))
                    {
                        File.Delete(image);
                        deleteNum++;
                        repeatNum--;
                        _syncContext.Post(setRepeatNumLabel, "重复图片数量：" + repeatNum);
                        _syncContext.Post(setProgress, deleteNum / allRepeatNum * 100);
                    }

                    if (Directory.Exists(image.Substring(0, image.LastIndexOf("\\"))) &&
                        new DirectoryInfo(image.Substring(0, image.LastIndexOf("\\"))).GetFileSystemInfos().Length == 0)
                        Directory.Delete(image.Substring(0, image.LastIndexOf("\\")));
                }

                _syncContext.Post(initRepeatListBox, deleteNum);
            });
            task1.Start();
        }

        private void setProgress(object progress)
        {
            if (Convert.ToInt32(progress) > 100)
                deleteProgressBar.Value = 100;
            else if (Convert.ToInt32(progress) < 0)
                deleteProgressBar.Value = 0;
            else
                deleteProgressBar.Value = Convert.ToInt32(progress);
        }

        private void initRepeatListBox(object deleteNum)
        {
            repeatImageList = new Dictionary<string, List<string>>();
            originImagesList = new Dictionary<string, string>();
            repeatImages.Items.Clear();
            originImages.Items.Clear();
            repeatNum = 0;
            deleteProgressBar.Value = 0;
            var task1 = new Task(() => { listFileOrFolder(new DirectoryInfo(folderPath)); });
            task1.Start();
            MessageBox.Show("删除成功 共" + deleteNum.ToString() + "张");
        }

        private void refreshCurrent_Click(object sender, EventArgs e)
        {
            repeatImageList = new Dictionary<string, List<string>>();
            originImagesList = new Dictionary<string, string>();
            originImages.Items.Clear();
            repeatImages.Items.Clear();
            deleteProgressBar.Value = 0;
            repeatNum = 0;
            currentFileRepeat.Text = "当前图片重复数量：" + 0;
            var task1 = new Task(() =>
            {
                listFileOrFolder(new DirectoryInfo(folderPath));
                _syncContext.Post(setProgress, 0);
            });
            task1.Start();
        }

        private void next_Click(object sender, EventArgs e)
        {
            selectedRepeatItemIndex++;
            repeatImageBoxLoad(selectedRepeatItem[selectedRepeatItemIndex % selectedRepeatItem.Count].ToString());
            repeatImages.SelectedItem = selectedRepeatItem[selectedRepeatItemIndex % selectedRepeatItem.Count];
            repeatFileSize.Text =
                getFileSize(selectedRepeatItem[selectedRepeatItemIndex % selectedRepeatItem.Count].ToString());
        }

        private void checkButtonStatus(object data)
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                deleteSelected.Enabled = false;
                deleteAll.Enabled = false;
                next.Enabled = false;
                deleteSelectedAll.Enabled = false;
                refreshCurrent.Enabled = false;
            }
            else
            {
                deleteSelected.Enabled = true;
                deleteAll.Enabled = true;
                deleteSelectedAll.Enabled = true;
                refreshCurrent.Enabled = true;
            }

            if (originImages.SelectedItem != null) next.Enabled = true;
        }

        private void repeatImageBoxLoad(string filepath)
        {
            var img = Image.FromFile(filepath);
            Image bmp = new Bitmap(img);
            repeatImage.Image = bmp;
            img.Dispose();
        }

        private void deleteSelectedAll_Click(object sender, EventArgs e)
        {
            if (selectedRepeatItem.Count == 0) return;
            var task1 = new Task(() =>
            {
                double allRepeatNum = repeatNum;
                double deleteNum = 0;
                repeatImage.Image = null;
                foreach (var item in selectedRepeatItem)
                {
                    if (File.Exists(item.ToString()))
                    {
                        File.Delete(item.ToString());
                        deleteNum++;
                        repeatNum--;
                        _syncContext.Post(removeRepeatItems, "");
                        _syncContext.Post(setRepeatNumLabel, "重复图片数量：" + repeatNum);
                        currentFileRepeat.Text = "当前图片重复数量：" + selectedRepeatItem.Count;
                        _syncContext.Post(setProgress, deleteNum / allRepeatNum * 100);
                    }

                    if (Directory.Exists(item.ToString().Substring(0, item.ToString().LastIndexOf("\\"))) &&
                        new DirectoryInfo(item.ToString().Substring(0, item.ToString().LastIndexOf("\\")))
                            .GetFileSystemInfos().Length ==
                        0) Directory.Delete(item.ToString().Substring(0, item.ToString().LastIndexOf("\\")));
                }

                selectedRepeatItem.Clear();
                _syncContext.Post(refreshRepeatListBox, deleteNum);
            });
            task1.Start();
        }

        private void refreshRepeatListBox(object deleteNum)
        {
            repeatImageList = new Dictionary<string, List<string>>();
            originImagesList = new Dictionary<string, string>();
            originImages.Items.Clear();
            repeatImages.Items.Clear();
            repeatNum = 0;
            deleteProgressBar.Value = 0;
            currentFileRepeat.Text = "当前图片重复数量：" + 0;
            var task1 = new Task(() =>
            {
                listFileOrFolder(new DirectoryInfo(folderPath));
                _syncContext.Post(setProgress, 0);
            });
            task1.Start();
            MessageBox.Show("删除成功 共" + deleteNum.ToString() + "张");
        }

        private void removeRepeatItems(object data)
        {
            repeatImages.Items.Remove(repeatImages.SelectedItem);
        }

        public static int GetFilesCount(DirectoryInfo dirInfo)
        {
            var totalFile = 0;
            //totalFile += dirInfo.GetFiles().Length;//获取全部文件
            foreach (var item in dirInfo.GetFiles())
                if (item.Extension == ".jpg" || item.Extension == ".gif" || item.Extension == ".png" ||
                    item.Extension == ".bmp" || item.Extension == ".mp4" || item.Extension == ".avi" ||
                    item.Extension == ".flv")
                    totalFile += 1;
            foreach (var subdir in dirInfo.GetDirectories()) totalFile += GetFilesCount(subdir);
            return totalFile;
        }
    }
}