using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Whatsapp_Sender_Mini_Edition {
    public partial class Main : Form {
        private string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "numbers.txt");
        private int processId;
        private Process runningProcess;

        public Main() {
            InitializeComponent();

            clientsList.CellValidating += clientsList_CellValidating;
            clientsList.RowValidated += clientsList_RowValidated;
            clientsList.UserDeletingRow += clientsList_UserDeletingRow;
        }

        private void LoadDataInBackground() {
            LoadDataFromTextFile();
        }

        private void LoadDataFromTextFile() {
            try {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("اسم العميل");
                dataTable.Columns.Add("رقم الهاتف");

                if (File.Exists(filePath)) {
                    var lines = File.ReadAllLines(filePath);
                    foreach (var line in lines) {
                        var row = line.Split(',');
                        if (row.Length == 2) {
                            dataTable.Rows.Add(row[0].Trim(), row[1].Trim());
                        }
                    }
                }

                this.Invoke((MethodInvoker)delegate {
                    clientsList.DataSource = dataTable;
                    clientsList.Refresh();
                });
            } catch (Exception ex) {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void ConvertVCFToText() {
            OpenFileDialog openFileDialog = new OpenFileDialog {
                Filter = "VCF Files (*.vcf)|*.vcf",
                Title = "اختر ملف VCF"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                string vcfFilePath = openFileDialog.FileName;
                try {
                    using (StreamWriter writer = new StreamWriter(filePath, true)) {
                        string[] lines = File.ReadAllLines(vcfFilePath);

                        string name = "";
                        string phone = "";

                        foreach (string line in lines) {
                            if (line.StartsWith("FN:")) {
                                name = line.Substring(3).Trim();
                            }
                            else if (line.StartsWith("TEL:") || line.Contains("TEL;")) {
                                phone = Regex.Replace(line, @"[^0-9+]", "").Trim();
                            }

                            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(phone)) {
                                writer.WriteLine($"{name},{phone}");
                                name = "";
                                phone = "";
                            }
                        }
                    }

                    MessageBox.Show("تم استيراد ملف جهات الاتصالات بنجاح!");
                    LoadDataFromTextFile();
                } catch (Exception ex) {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void clientsList_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
            string columnName = clientsList.Columns[e.ColumnIndex].Name;
            string input = e.FormattedValue?.ToString().Trim();

            if (columnName == "اسم العميل" && string.IsNullOrEmpty(input)) {
                statusLableMessage("يرجى كتابة اسم العميل", Color.Crimson);
                e.Cancel = true;
            } else if (columnName == "رقم الهاتف" && (string.IsNullOrEmpty(input) || !Regex.IsMatch(input, @"^\+?[0-9]+$"))) {
                statusLableMessage("يرجى كتابة رقم هاتف صحيح", Color.Crimson);
                e.Cancel = true;
            }
        }

        private void clientsList_RowValidated(object sender, DataGridViewCellEventArgs e) {
            if (clientsList.Rows[e.RowIndex].IsNewRow) return;

            if (e.RowIndex >= 0 && e.RowIndex < clientsList.Rows.Count) {
                string clientName = clientsList.Rows[e.RowIndex].Cells["اسم العميل"].Value?.ToString().Trim();
                string phoneNumber = clientsList.Rows[e.RowIndex].Cells["رقم الهاتف"].Value?.ToString().Trim();

                if (string.IsNullOrEmpty(clientName) || string.IsNullOrEmpty(phoneNumber)) return;

                UpdateOrAddEntry(clientName, phoneNumber);
            }
        }

        private void UpdateOrAddEntry(string clientName, string phoneNumber) {
            var lines = new List<string>();

            foreach (DataGridViewRow row in clientsList.Rows) {
                if (row.IsNewRow) continue;
                string name = row.Cells["اسم العميل"].Value?.ToString().Trim();
                string phone = row.Cells["رقم الهاتف"].Value?.ToString().Trim();

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(phone)) {
                    lines.Add($"{name},{phone}");
                }
            }

            try {
                File.WriteAllLines(filePath, new string[] { });
            } catch (Exception ex) {
                MessageBox.Show($"حدث خطأ أثناء مسح البيانات من الملف: {ex.Message}");
                return;
            }

            try {
                File.WriteAllLines(filePath, lines);
                statusLableMessage("تم تحديث البيانات بنجاح.", Color.Green);
            } catch (Exception ex) {
                MessageBox.Show($"حدث خطأ أثناء كتابة البيانات إلى الملف: {ex.Message}");
            }
        }


        private void clientsList_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e) {
            if (e.Row.Index >= 0 && e.Row.Index < clientsList.Rows.Count) {
                string clientName = e.Row.Cells["اسم العميل"].Value?.ToString().Trim();
                string phoneNumber = e.Row.Cells["رقم الهاتف"].Value?.ToString().Trim();

                if (string.IsNullOrEmpty(clientName) || string.IsNullOrEmpty(phoneNumber)) {
                    statusLableMessage("لا يمكن حذف صف بدون بيانات صالحة.", Color.Crimson);
                    return;
                }

                DeleteEntry(clientName, phoneNumber);
            }
        }

        private void DeleteEntry(string clientName, string phoneNumber) {
            try {
                var lines = File.ReadAllLines(filePath)
                    .Where(line => line != $"{clientName},{phoneNumber}")
                    .ToList();
                File.WriteAllLines(filePath, lines);
                statusLableMessage("تم حذف البيانات بنجاح.", Color.Green);
            } catch (Exception ex) {
                MessageBox.Show($"حدث خطأ أثناء محاولة حذف البيانات: {ex.Message}");
            }
        }

        private void statusLableMessage(string message, Color color, int delay = 5000) {
            statusLabel.Text = message;
            statusLabel.ForeColor = color;
            Task.Delay(delay).ContinueWith(_ => {
                statusLabel.Text = "";
                statusLabel.ForeColor = Color.Black;
            });
        }

        private void statusLable(string message, Color color, int delay = 5000) {
            lblStatus.Text = message;
            lblStatus.ForeColor = color;
        }

        public void CopySelectedImages() {
            OpenFileDialog openFileDialog = new OpenFileDialog {
                Multiselect = true,
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tiff;*.ico",
                Title = "اختر الصور التي تريد نسخها"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                string imagesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");

                if (!Directory.Exists(imagesDirectory)) {
                    Directory.CreateDirectory(imagesDirectory);
                }

                foreach (var file in Directory.GetFiles(imagesDirectory)) {
                    using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.ReadWrite, FileShare.Delete)) {
                        File.Delete(file);
                    }
                }

                foreach (var filePath in openFileDialog.FileNames) {
                    try {
                        string fileName = Path.GetFileName(filePath);

                        string destinationPath = Path.Combine(imagesDirectory, fileName);

                        File.Copy(filePath, destinationPath, true);

                    } catch (Exception ex) {
                        MessageBox.Show($"حدث خطأ أثناء نسخ الصورة: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                LoadImagesToTable();
            }
        }

        private void LoadImagesToTable() {
            imagesList.Rows.Clear();

            string imagesFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");

            if (Directory.Exists(imagesFolderPath)) {
                string[] imageFiles = Directory.GetFiles(imagesFolderPath, "*.*")
                                               .Where(f => f.ToLower().EndsWith(".jpg") || f.ToLower().EndsWith(".png") || f.ToLower().EndsWith(".jpeg"))
                                               .ToArray();

                foreach (var imageFile in imageFiles) {
                    if (File.Exists(imageFile)) {
                        string imageName = Path.GetFileName(imageFile);
                        try {
                            using (FileStream fs = new FileStream(imageFile, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                                Image img = Image.FromStream(fs);

                                imagesList.Rows.Add(img, imageName);
                            }
                        } catch (Exception ex) {
                            Console.WriteLine($"فشل تحميل الصورة {imageFile}: {ex.Message}");
                        }
                    }
                }
            } else {
                MessageBox.Show("مجلد الصور غير موجود!");
            }
        }



        private bool IsImageValid(string imagePath) {
            try {
                using (Image img = Image.FromFile(imagePath)) {
                    return true;
                }
            } catch {
                return false;
            }
        }

        private void btnOpenVcard_Click(object sender, EventArgs e) {
            ConvertVCFToText();
        }

        private void btnUploadImages_Click(object sender, EventArgs e) {
            CopySelectedImages();
        }

        private async void btnRunSenderVendor_Click(object sender, EventArgs e) {
            var src = Application.StartupPath;
            string pythonScript = src + "\\ws-vendor.py";

            statusLable("يعمل", Color.Green);

            //restartService.Visible = true;
            //endService.Visible = true;

            AppendTextToOutput("Running script, please wait...");

            try {
                runningProcess = await RunPythonScriptAsync(pythonScript);

            } catch (Exception ex) {
                AppendTextToOutput($"Error: {ex.Message}");
            }
        }

        private void Main_Load(object sender, EventArgs e) {
            if (!imagesList.Columns.Contains("معاينة")) {
                DataGridViewImageColumn previewColumn = new DataGridViewImageColumn {
                    Name = "معاينة",
                    HeaderText = "معاينة الصورة",
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                };

                imagesList.Columns.Add(previewColumn);
            }

            if (!imagesList.Columns.Contains("اسم الصورة")) {
                imagesList.Columns.Add("اسم الصورة", "اسم الصورة");
            }

            LoadDataInBackground();
            LoadImagesToTable();
            ReadContentFromFile();
        }

        private void SaveContentToFile() {
            string filePath = "message.txt";

            File.WriteAllText(filePath, messageBox.Text);
        }

        private void ReadContentFromFile() {
            string filePath = "message.txt";

            if (File.Exists(filePath)) {
                string fileContent = File.ReadAllText(filePath);

                messageBox.Text = fileContent;
            } else {
                MessageBox.Show("لا تنسي كتابة الرسالة!", "معلومات");
            }
        }

        private void messageBox_TextChanged(object sender, EventArgs e) {
            Timer timer = new Timer();
            timer.Interval = 500;
            timer.Tick += (s, args) => {
                timer.Stop();
                SaveContentToFile();
            };
            timer.Start();
        }

        private void btnDeleteImages_Click(object sender, EventArgs e) {
            string imagesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");
            foreach (var file in Directory.GetFiles(imagesDirectory)) {
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.ReadWrite, FileShare.Delete)) {
                    File.Delete(file);
                }
            }

            LoadImagesToTable();
        }

        private void installComponents_Click(object sender, EventArgs e) {
            var src = Application.StartupPath;
            Process.Start(src + "\\install.bat");
        }

        private void endWsVendor_Click(object sender, EventArgs e) {
            
        }

        private async Task<Process> RunPythonScriptAsync(string scriptPath) {
            try {
                var processStartInfo = new ProcessStartInfo {
                    FileName = "python",
                    Arguments = $"\"{scriptPath}\"",
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    UseShellExecute = false,
                    CreateNoWindow = false
                };

                using (var process = new Process { StartInfo = processStartInfo }) {
                    process.Start();

                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();

                    await Task.Run(() => process.WaitForExit());

                    if (!string.IsNullOrEmpty(error)) {
                        AppendTextToOutput($"Error: {error}");
                        return null;
                    }
                    AppendTextToOutput($"Output: {output}");
                    return process;
                }
            } catch (Exception ex) {
                AppendTextToOutput($"Exception occurred:\n{ex.Message}");
                return null;
            }
        }

        private void AppendTextToOutput(string text) {
            statusLabel.Text = text;
            //if (txtOutput.InvokeRequired) {
            //    txtOutput.Invoke(new Action(() => txtOutput.AppendText(text + Environment.NewLine)));
            //} else {
            //    txtOutput.AppendText(text + Environment.NewLine);
            //}
        }

        private void endService_Click(object sender, EventArgs e) {
            if (runningProcess != null && !runningProcess.HasExited) {
                try {
                    runningProcess.Kill();
                    AppendTextToOutput($"Process with PID {runningProcess.Id} has been terminated.");
                } catch (Exception ex) {
                    AppendTextToOutput($"Error stopping process: {ex.Message}");
                }
            } else {
                AppendTextToOutput("No running process to stop.");
            }
        }

    }
}
