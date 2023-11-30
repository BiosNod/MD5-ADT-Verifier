using System.Security.Cryptography;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace HashVerifyer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /*
         * ���������� ����� � ��������� ���� � ����������, �.�. �� ���������
         * ��������� ������������� �� ��������
         */
        private void appendToRichBox(RichTextBox rtbMessages, string text)
        {
            if (rtbMessages.InvokeRequired)
            {
                rtbMessages.Invoke(new Action(() => appendToRichBox(rtbMessages, text)));
            }
            else
            {
                rtbMessages.AppendText(text);
                rtbMessages.SelectionStart = rtbMessages.Text.Length;
                rtbMessages.ScrollToCaret();
            }
        }

        private async void buttonStartClick(object sender, EventArgs e)
        {
            loggerRichTextBox.Clear();
            goodFilesTextBox.Clear();
            badFilesTextBox.Clear();
            newFileTextBox.Clear();
            changedFileTextBox.Clear();

            var rootDirectory = labelDirectory.Text;

            // ��������� ProcessDirectoryAsync � ��������� ������
            await Task.Run(async () => await ProcessDirectoryAsync(rootDirectory));

            appendToRichBox(loggerRichTextBox, "����������� ����������� �� ���������� ���������\n");
        }

        private async Task ProcessDirectoryAsync(string directoryPath)
        {
            if (directoryPath == "System Volume Information" || directoryPath == "$RECYCLE.BIN")
                return;

            // ��������� ������ � ������� ����������
            var files = Directory.GetFiles(directoryPath);
            foreach (var file in files)
                await ProcessFileAsync(file);

            // ����������� ��������� �������������
            var subdirectories = Directory.GetDirectories(directoryPath);
            foreach (var subdirectory in subdirectories)
                await ProcessDirectoryAsync(subdirectory);
        }

        /*
         * ���������� MD5 ����� � ������ � ��� ADS - md5-���� � ���� ��������� �����
         * ���� ��������� ����� ������ �� ������ �����, ��� �� ������� ����
         */
        private async Task RewindMD5AndDateAsync
        (
            string filePath,
            string md5StreamName,
            string dateStreamName,
            string dateCurrent,
            DateTime lastModified,
            DateTime lastAccessed
        )
        {
            await ADSWriteDataAsync(filePath, md5StreamName, await CalculateMD5Async(filePath));
            await ADSWriteDataAsync(filePath, dateStreamName, dateCurrent);
            // ����� ���������� ������ � ADS � ������ ������� NTFS ������ ���� ��������� � ��������
            // ��������������� ���� ��������� � ���������� ��������
            File.SetLastWriteTimeUtc(filePath, lastModified);
            File.SetLastAccessTimeUtc(filePath, lastAccessed);
        }

        private async Task ProcessFileAsync(string filePath)
        {
            var md5StreamName = "md5";
            var dateStreamName = "date";
            var dateModified = File.GetLastWriteTimeUtc(filePath);
            var dateAccessed = File.GetLastAccessTimeUtc(filePath);
            var dateCurrent = dateModified.ToString("yyyy-MM-ddTHH:mm:ssZ");

            if (!await ADSHasDataAsync(filePath, md5StreamName))
            {
                if (radioHybridMode.Checked || radioHashAddOnly.Checked)
                {
                    appendToRichBox(loggerRichTextBox, $"Calculating hash for the new file: {filePath}\n\n");
                    await RewindMD5AndDateAsync
                    (
                        filePath,
                        md5StreamName,
                        dateStreamName,
                        dateCurrent,
                        dateModified,
                        dateAccessed
                    );
                    appendToRichBox(newFileTextBox, $"Hash and date added to ADS for the new file: {filePath}\n\n");
                }
                else
                    appendToRichBox(loggerRichTextBox, $"Skip adding hash to the file: {filePath}\n\n");
            }
            else
            {
                var md5Old = await ADSReadDataAsync(filePath, md5StreamName);
                var dateOld = await ADSReadDataAsync(filePath, dateStreamName);

                appendToRichBox(loggerRichTextBox, $"Hash already exists in ADS for file: {filePath}\nMD5: {md5Old}\nDate: {dateOld}\n\n");

                if (radioHybridMode.Checked || radioHashCheckOnly.Checked)
                {
                    // ���� ���������, ��������� ��� ������ ���, ������ �����������
                    if (dateCurrent != dateOld)
                    {
                        appendToRichBox(changedFileTextBox, $"changed file: {filePath}\nrestore dates...\n\n");
                        
                        await RewindMD5AndDateAsync
                        (
                            filePath,
                            md5StreamName,
                            dateStreamName,
                            dateCurrent,
                            dateModified,
                            dateAccessed
                        );
                    }
                    else
                    {
                        appendToRichBox(changedFileTextBox, $"2 dates match, comparing 2 MD5 for file: {filePath}\n\n");
                        
                        var md5Current = await CalculateMD5Async(filePath);

                        if (md5Current != md5Old)
                            appendToRichBox(badFilesTextBox, $"md5 mismatch in file: {filePath}\n[current hash:{md5Current}]\n[old hash:{md5Old}]\n\n");
                        else
                            appendToRichBox(goodFilesTextBox, $"md5 match in file: {filePath}\n[md5:{md5Current}]\n\n");
                    }
                }
                else
                    loggerRichTextBox.AppendText($"Skip checking hash in file: {filePath}\n\n");
            }
        }

        /*
         * ��������.� �� ������ � ADS?
         */
        async Task<bool> ADSHasDataAsync(string filePath, string streamName)
        {
            var actualHash = await ADSReadDataAsync(filePath, streamName);
            return !string.IsNullOrEmpty(actualHash);
        }

        /*
         * ������ ������ � ADS �����
         */
        async Task ADSWriteDataAsync(string filePath, string streamName, string data)
        {
            var lastModified = File.GetLastWriteTimeUtc(filePath);
            var formattedDate = lastModified.ToString("yyyy-MM-ddTHH:mm:ssZ");

            await using var fileStream = new FileStream(filePath + ":" + streamName, FileMode.Create, FileAccess.Write);
            await using var writer = new StreamWriter(fileStream);
            await writer.WriteAsync(data);
        }

        /*
         * ������ ������ �� ADS
         */
        async Task<string> ADSReadDataAsync(string filePath, string streamName)
        {
            try
            {
                await using var fileStream = new FileStream(filePath + ":" + streamName, FileMode.Open, FileAccess.Read);
                using var reader = new StreamReader(fileStream);
                return await reader.ReadToEndAsync();
            }
            catch (FileNotFoundException)
            {
                return string.Empty; // ���� ADS �� ����������
            }
        }

        async Task<string> CalculateMD5Async(string filePath)
        {
            using var md5 = MD5.Create();
            await using var stream = File.OpenRead(filePath);
            var hash = await md5.ComputeHashAsync(stream);
            // AA-BB-CC => AABBCC
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        void SelectDirectory()
        {
            var folderBrowserDialog = new FolderBrowserDialog();

            // ������������� ��������� �������
            folderBrowserDialog.Description = "�������� ����������";

            // ���������� ������ � �������� ��������� ������ ������������
            var result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                // ��������� ������������� ����������
                var selectedPath = folderBrowserDialog.SelectedPath;
                appendToRichBox(loggerRichTextBox, $"��������� ����������: {selectedPath}\n");
                labelDirectory.Text = selectedPath;
            }
            else
            {
                appendToRichBox(loggerRichTextBox, "����� ���������� �������\n");
            }
        }

        private void selectDirClick(object sender, EventArgs e)
        {
            SelectDirectory();
        }
    }
}