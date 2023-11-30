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
         * Добавление строк в текстовый бокс с прокруткой, т.к. по умолчанию
         * прокрутка автоматически не работает
         */
        private void appendToRichBox(RichTextBox rtbMessages, string text)
        {
            rtbMessages.AppendText(text);
            rtbMessages.SelectionStart = rtbMessages.Text.Length;
            rtbMessages.ScrollToCaret();
        }

        private void buttonStartClick(object sender, EventArgs e)
        {
            loggerRichTextBox.Clear();
            goodFilesTextBox.Clear();
            badFilesTextBox.Clear();
            newFileTextBox.Clear();
            changedFileTextBox.Clear();

            string rootDirectory = labelDirectory.Text;
            ProcessDirectory(rootDirectory);
            appendToRichBox(loggerRichTextBox, "Рекурсивное прохождение по директории выполнено\n");
        }

        private void ProcessDirectory(string directoryPath)
        {
            if (directoryPath == "System Volume Information" || directoryPath == "$RECYCLE.BIN")
                return;

            // Обработка файлов в текущей директории
            string[] files = Directory.GetFiles(directoryPath);
            foreach (string file in files)
                ProcessFile(file);

            // Рекурсивная обработка поддиректорий
            string[] subdirectories = Directory.GetDirectories(directoryPath);
            foreach (string subdirectory in subdirectories)
                ProcessDirectory(subdirectory);
        }

        /*
         * Обновление MD5 файла и запись в два ADS - md5-хеша и даты изменения файла
         * Дата изменения файла берётся из самого файла, это не текущая дата
         */
        private void RewindMD5AndDate
        (
            string filePath,
            string md5StreamName,
            string dateStreamName,
            string dateCurrent,
            DateTime lastModified,
            DateTime lastAccessed
        )
        {
            ADSWriteData(filePath, md5StreamName, CalculateMD5(filePath));
            ADSWriteData(filePath, dateStreamName, dateCurrent);
            // После добавления данных в ADS у файлов система NTFS меняет даты изменения и открытия
            // Восстанавливаем даты изменения и последнего открытия
            File.SetLastWriteTimeUtc(filePath, lastModified);
            File.SetLastAccessTimeUtc(filePath, lastAccessed);
        }

        private void ProcessFile(string filePath)
        {
            string md5StreamName = "md5";
            string dateStreamName = "date";
            DateTime dateModified = File.GetLastWriteTimeUtc(filePath);
            DateTime dateAccessed = File.GetLastAccessTimeUtc(filePath);
            string dateCurrent = dateModified.ToString("yyyy-MM-ddTHH:mm:ssZ");

            if (!ADSHasData(filePath, md5StreamName))
            {
                if (radioHybridMode.Checked || radioHashAddOnly.Checked)
                {
                    appendToRichBox(loggerRichTextBox, $"Calculating hash for the new file: {filePath}\n\n");
                    RewindMD5AndDate
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
                string md5Old = ADSReadData(filePath, md5StreamName);
                string dateOld = ADSReadData(filePath, dateStreamName);

                appendToRichBox(loggerRichTextBox, $"Hash already exists in ADS for file: {filePath}\nMD5: {md5Old}\nDate: {dateOld}\n\n");

                if (radioHybridMode.Checked || radioHashCheckOnly.Checked)
                {
                    // Файл изменился, проверять хеш смысла нет, просто пересчитаем
                    if (dateCurrent != dateOld)
                    {
                        appendToRichBox(changedFileTextBox, $"changed file: {filePath}\nrestore dates...\n\n");
                        
                        RewindMD5AndDate
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
                        
                        string md5Current = CalculateMD5(filePath);

                        if (md5Current != md5Old)
                            appendToRichBox(badFilesTextBox, $"md5 mismatch in file: {filePath}\n[current hash:{md5Current}]\n[old hash:{md5Old}]\n\n");
                        else
                            appendToRichBox(goodFilesTextBox, $"md5 match in file: {filePath}\n[md5:{md5Current}]\n\n");
                    }
                }
                else
                    loggerRichTextBox.AppendText($"Skip cheking hash in file: {filePath}\n\n");
            }
        }

        /*
         * Существу.т ли данные в ADS?
         */
        bool ADSHasData(string filePath, string streamName)
        {
            string actualHash = ADSReadData(filePath, streamName);
            return !string.IsNullOrEmpty(actualHash);
        }

        /*
         * Запись данных в ADS файла
         */
        void ADSWriteData(string filePath, string streamName, string data)
        {
            DateTime lastModified = File.GetLastWriteTimeUtc(filePath);
            string formattedDate = lastModified.ToString("yyyy-MM-ddTHH:mm:ssZ");

            using (var fileStream = new FileStream(filePath + ":" + streamName, FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(fileStream))
            {
                writer.Write(data);
            }
        }

        /*
         * Чтение данных из ADS
         */
        string ADSReadData(string filePath, string streamName)
        {
            try
            {
                using (var fileStream = new FileStream(filePath + ":" + streamName, FileMode.Open, FileAccess.Read))
                using (var reader = new StreamReader(fileStream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (FileNotFoundException)
            {
                return string.Empty; // Файл ADS не существует
            }
        }

        string CalculateMD5(string filePath)
        {
            using (var md5 = MD5.Create())
            using (var stream = File.OpenRead(filePath))
            {
                byte[] hash = md5.ComputeHash(stream);
                // AA-BB-CC => AABBCC
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        void SelectDirectory()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            // Устанавливаем заголовок диалога
            folderBrowserDialog.Description = "Выберите директорию";

            // Показываем диалог и получаем результат выбора пользователя
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Выбранная пользователем директория
                string selectedPath = folderBrowserDialog.SelectedPath;
                appendToRichBox(loggerRichTextBox, $"Выбранная директория: {selectedPath}\n");
                labelDirectory.Text = selectedPath;
            }
            else
            {
                appendToRichBox(loggerRichTextBox, "Выбор директории отменен\n");
            }
        }

        private void selectDirClick(object sender, EventArgs e)
        {
            SelectDirectory();
        }
    }
}