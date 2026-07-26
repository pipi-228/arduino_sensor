using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;

namespace ArduinoDistanceControl
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort;
        private bool isConnected = false;

        public Form1()
        {
            InitializeComponent();
            LoadAvailablePorts();
            UpdateConnectionStatus();

            // Разблокируем элементы управления, чтобы можно было настроить параметры перед подключением
            groupBox2.Enabled = true;
            thresholdTrackBar.Enabled = true;
            thresholdNumeric.Enabled = true;
            setThresholdBtn.Enabled = true;
            autoModeCheckBox.Enabled = true;
        }

        private void LoadAvailablePorts()
        {
            portList.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            portList.Items.AddRange(ports);
            if (portList.Items.Count > 0)
                portList.SelectedIndex = 0;
        }

        private void UpdateConnectionStatus()
        {
            if (isConnected)
            {
                connectionText.Text = "Подключено";
                connectionText.ForeColor = Color.Green;
                connectBtn.Text = "Отключить";
                groupBox1.Enabled = true;

                // Включаем тестовые кнопки только при подключении
                testBuzzerBtn.Enabled = true;
                testLedBtn.Enabled = true;
            }
            else
            {
                connectionText.Text = "Отключено";
                connectionText.ForeColor = Color.Red;
                connectBtn.Text = "Подключить";
                groupBox1.Enabled = true; // Всегда разрешаем доступ к настройкам подключения

                // Отключаем тестовые кнопки при отключении
                testBuzzerBtn.Enabled = false;
                testLedBtn.Enabled = false;
                distanceLabel.Text = "Расстояние: -- см";
                distanceLabel.ForeColor = Color.Black;
            }
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                Disconnect();
            }
            else
            {
                Connect();
            }
        }

        private void Connect()
        {
            if (portList.SelectedItem == null)
            {
                MessageBox.Show("Выберите COM порт", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                serialPort = new SerialPort(portList.SelectedItem.ToString(), 9600);
                serialPort.DataReceived += SerialPort_DataReceived;
                serialPort.Open();
                isConnected = true;
                UpdateConnectionStatus();

                // Даем Arduino время на инициализацию
                System.Threading.Thread.Sleep(2000);

                // Запрашиваем текущие настройки с Arduino
                serialPort.WriteLine("GET_SETTINGS");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Disconnect()
        {
            try
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.Close();
                    serialPort.DataReceived -= SerialPort_DataReceived;
                }
                isConnected = false;
                UpdateConnectionStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка отключения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = serialPort.ReadLine().Trim();
                this.Invoke(new Action(() => ProcessReceivedData(data)));
            }
            catch (Exception ex)
            {
                // Игнорируем ошибки при чтении
                Console.WriteLine($"Ошибка чтения: {ex.Message}");
            }
        }

        private void ProcessReceivedData(string data)
        {
            // Пропускаем пустые строки
            if (string.IsNullOrWhiteSpace(data))
                return;

            // Выводим все сообщения в консоль для отладки
            Console.WriteLine($"Получено: {data}");

            // Проверяем, содержит ли строка данные о расстоянии
            if (data.Contains("Расстояние: ") && data.Contains(" см"))
            {
                // Извлекаем числовое значение расстояния
                try
                {
                    int startIndex = data.IndexOf("Расстояние: ") + "Расстояние: ".Length;
                    int endIndex = data.IndexOf(" см", startIndex);
                    if (startIndex >= 0 && endIndex > startIndex)
                    {
                        string distanceStr = data.Substring(startIndex, endIndex - startIndex).Trim();
                        if (int.TryParse(distanceStr, out int distance))
                        {
                            distanceLabel.Text = $"Расстояние: {distance} см";

                            // Изменение цвета в зависимости от расстояния
                            if (distance < thresholdTrackBar.Value && distance > 0)
                            {
                                distanceLabel.ForeColor = Color.Red;
                            }
                            else
                            {
                                distanceLabel.ForeColor = Color.Green;
                            }
                        }
                    }
                }
                catch
                {
                    // Если не удалось распарсить, игнорируем
                }
            }
            else if (data.StartsWith("DIST:"))
            {
                // Обработка данных о расстоянии в формате DIST:число
                string distanceStr = data.Substring(5);
                if (int.TryParse(distanceStr, out int distance))
                {
                    distanceLabel.Text = $"Расстояние: {distance} см";

                    // Изменение цвета в зависимости от расстояния
                    if (distance < thresholdTrackBar.Value && distance > 0)
                    {
                        distanceLabel.ForeColor = Color.Red;
                    }
                    else
                    {
                        distanceLabel.ForeColor = Color.Green;
                    }
                }
            }
            else if (data.StartsWith("THRESHOLD:"))
            {
                // Получение текущего порога с Arduino
                string thresholdStr = data.Substring(10);
                if (int.TryParse(thresholdStr, out int threshold))
                {
                    thresholdTrackBar.Value = Math.Max(thresholdTrackBar.Minimum,
                        Math.Min(thresholdTrackBar.Maximum, threshold));
                    thresholdValueLabel.Text = $"{threshold} см";
                    thresholdNumeric.Value = threshold;
                }
            }
            else if (data.StartsWith("BUZZER:"))
            {
                // Состояние пищалки
                string buzzerState = data.Substring(7);
                if (buzzerState == "ON")
                {
                    buzzerStatusLabel.Text = "ВКЛ";
                    buzzerStatusLabel.ForeColor = Color.Red;
                }
                else
                {
                    buzzerStatusLabel.Text = "ВЫКЛ";
                    buzzerStatusLabel.ForeColor = Color.Green;
                }
            }
            else if (data.StartsWith("LED:"))
            {
                // Состояние светодиода
                string ledState = data.Substring(4);
                if (ledState == "RED")
                {
                    ledStatusLabel.Text = "КРАСНЫЙ";
                    ledStatusLabel.ForeColor = Color.Red;
                }
                else if (ledState == "GREEN")
                {
                    ledStatusLabel.Text = "ЗЕЛЕНЫЙ";
                    ledStatusLabel.ForeColor = Color.Green;
                }
                else if (ledState == "BLUE")
                {
                    ledStatusLabel.Text = "СИНИЙ";
                    ledStatusLabel.ForeColor = Color.Blue;
                }
                else
                {
                    ledStatusLabel.Text = ledState;
                    ledStatusLabel.ForeColor = Color.Black;
                }
            }
            else if (data.StartsWith("MODE:"))
            {
                // Режим работы
                string mode = data.Substring(5);
                if (mode == "AUTO")
                {
                    autoModeCheckBox.Checked = true;
                }
                else if (mode == "MANUAL")
                {
                    autoModeCheckBox.Checked = false;
                }
            }
            else if (data.Contains("Порог изменен") || data.Contains("Автоматический режим включен") ||
                     data.Contains("Ручной режим включен") || data.Contains("Тест"))
            {
                // Игнорируем информационные сообщения
            }
            else if (data.Contains("Опасность! Слишком близко!"))
            {
                // Обработка предупреждения об опасности
                // Можно добавить звуковое оповещение или мигание в интерфейсе
                if (!distanceLabel.Text.Contains("ОПАСНОСТЬ"))
                {
                    distanceLabel.Text = distanceLabel.Text + " (ОПАСНОСТЬ!)";
                }
            }
            else if (data.Contains("Нормальное расстояние"))
            {
                // Убираем пометку об опасности
                if (distanceLabel.Text.Contains("(ОПАСНОСТЬ!)"))
                {
                    distanceLabel.Text = distanceLabel.Text.Replace(" (ОПАСНОСТЬ!)", "");
                }
            }
        }

        private void thresholdTrackBar_Scroll(object sender, EventArgs e)
        {
            int threshold = thresholdTrackBar.Value;
            thresholdValueLabel.Text = $"{threshold} см";
            thresholdNumeric.Value = threshold;

            // Отправляем команду только при подключении
            if (isConnected && serialPort != null && serialPort.IsOpen)
            {
                serialPort.WriteLine($"SET_THRESHOLD:{threshold}");
            }
        }

        private void testBuzzerBtn_Click(object sender, EventArgs e)
        {
            if (isConnected && serialPort != null && serialPort.IsOpen)
            {
                try
                {
                    serialPort.WriteLine("TEST_BUZZER");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка отправки команды: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Подключитесь к Arduino для тестирования пищалки", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void testLedBtn_Click(object sender, EventArgs e)
        {
            if (isConnected && serialPort != null && serialPort.IsOpen)
            {
                try
                {
                    serialPort.WriteLine("TEST_LED");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка отправки команды: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Подключитесь к Arduino для тестирования светодиода", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void refreshPortsBtn_Click(object sender, EventArgs e)
        {
            LoadAvailablePorts();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        private void setThresholdBtn_Click(object sender, EventArgs e)
        {
            int threshold = (int)thresholdNumeric.Value;
            thresholdTrackBar.Value = threshold;
            thresholdValueLabel.Text = $"{threshold} см";

            // Отправляем команду только при подключении
            if (isConnected && serialPort != null && serialPort.IsOpen)
            {
                try
                {
                    serialPort.WriteLine($"SET_THRESHOLD:{threshold}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка отправки команды: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Подключитесь к Arduino для установки порога", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void autoModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Отправляем команду только при подключении
            if (isConnected && serialPort != null && serialPort.IsOpen)
            {
                try
                {
                    if (autoModeCheckBox.Checked)
                    {
                        serialPort.WriteLine("MODE:AUTO");
                    }
                    else
                    {
                        serialPort.WriteLine("MODE:MANUAL");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка отправки команды: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (isConnected)
            {
                // Если подключены, но команда не отправилась
                MessageBox.Show("Не удалось отправить команду", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void thresholdNumeric_ValueChanged(object sender, EventArgs e)
        {
            int threshold = (int)thresholdNumeric.Value;
            thresholdTrackBar.Value = threshold;
            thresholdValueLabel.Text = $"{threshold} см";
        }

        private void portList_DropDown(object sender, EventArgs e)
        {
            // Автоматически обновляем список портов при открытии выпадающего списка
            LoadAvailablePorts();
        }
    }
}