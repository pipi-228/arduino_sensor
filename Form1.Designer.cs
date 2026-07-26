namespace ArduinoDistanceControl
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox portList;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.Label connectionText;
        private System.Windows.Forms.Button refreshPortsBtn;
        private System.Windows.Forms.Label distanceLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar thresholdTrackBar;
        private System.Windows.Forms.Label thresholdValueLabel;
        private System.Windows.Forms.Button testBuzzerBtn;
        private System.Windows.Forms.Button testLedBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label buzzerStatusLabel;
        private System.Windows.Forms.Label ledStatusLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown thresholdNumeric;
        private System.Windows.Forms.Button setThresholdBtn;
        private System.Windows.Forms.CheckBox autoModeCheckBox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.distanceLabel = new System.Windows.Forms.Label();
            this.refreshPortsBtn = new System.Windows.Forms.Button();
            this.connectionText = new System.Windows.Forms.Label();
            this.connectBtn = new System.Windows.Forms.Button();
            this.portList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.autoModeCheckBox = new System.Windows.Forms.CheckBox();
            this.setThresholdBtn = new System.Windows.Forms.Button();
            this.thresholdNumeric = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.ledStatusLabel = new System.Windows.Forms.Label();
            this.buzzerStatusLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.testLedBtn = new System.Windows.Forms.Button();
            this.testBuzzerBtn = new System.Windows.Forms.Button();
            this.thresholdValueLabel = new System.Windows.Forms.Label();
            this.thresholdTrackBar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdTrackBar)).BeginInit();
            this.SuspendLayout();

            // groupBox1
            this.groupBox1.Controls.Add(this.distanceLabel);
            this.groupBox1.Controls.Add(this.refreshPortsBtn);
            this.groupBox1.Controls.Add(this.connectionText);
            this.groupBox1.Controls.Add(this.connectBtn);
            this.groupBox1.Controls.Add(this.portList);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 120);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Подключение и данные";

            // distanceLabel
            this.distanceLabel.AutoSize = true;
            this.distanceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.distanceLabel.Location = new System.Drawing.Point(10, 80);
            this.distanceLabel.Name = "distanceLabel";
            this.distanceLabel.Size = new System.Drawing.Size(140, 24);
            this.distanceLabel.TabIndex = 5;
            this.distanceLabel.Text = "Расстояние: -- см";

            // refreshPortsBtn
            this.refreshPortsBtn.Location = new System.Drawing.Point(270, 40);
            this.refreshPortsBtn.Name = "refreshPortsBtn";
            this.refreshPortsBtn.Size = new System.Drawing.Size(75, 23);
            this.refreshPortsBtn.TabIndex = 4;
            this.refreshPortsBtn.Text = "Обновить";
            this.refreshPortsBtn.UseVisualStyleBackColor = true;
            this.refreshPortsBtn.Click += new System.EventHandler(this.refreshPortsBtn_Click);

            // connectionText
            this.connectionText.AutoSize = true;
            this.connectionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectionText.Location = new System.Drawing.Point(100, 15);
            this.connectionText.Name = "connectionText";
            this.connectionText.Size = new System.Drawing.Size(95, 17);
            this.connectionText.TabIndex = 3;
            this.connectionText.Text = "Отключено";

            // connectBtn
            this.connectBtn.Location = new System.Drawing.Point(180, 40);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(80, 23);
            this.connectBtn.TabIndex = 2;
            this.connectBtn.Text = "Подключить";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);

            // portList
            this.portList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portList.FormattingEnabled = true;
            this.portList.Location = new System.Drawing.Point(70, 40);
            this.portList.Name = "portList";
            this.portList.Size = new System.Drawing.Size(100, 21);
            this.portList.TabIndex = 1;

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "COM порт:";

            // groupBox2
            this.groupBox2.Controls.Add(this.autoModeCheckBox);
            this.groupBox2.Controls.Add(this.setThresholdBtn);
            this.groupBox2.Controls.Add(this.thresholdNumeric);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.ledStatusLabel);
            this.groupBox2.Controls.Add(this.buzzerStatusLabel);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.testLedBtn);
            this.groupBox2.Controls.Add(this.testBuzzerBtn);
            this.groupBox2.Controls.Add(this.thresholdValueLabel);
            this.groupBox2.Controls.Add(this.thresholdTrackBar);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 140);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(360, 200);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Управление";

            // autoModeCheckBox
            this.autoModeCheckBox.AutoSize = true;
            this.autoModeCheckBox.Location = new System.Drawing.Point(10, 170);
            this.autoModeCheckBox.Name = "autoModeCheckBox";
            this.autoModeCheckBox.Size = new System.Drawing.Size(130, 17);
            this.autoModeCheckBox.TabIndex = 12;
            this.autoModeCheckBox.Text = "Автоматический режим";
            this.autoModeCheckBox.UseVisualStyleBackColor = true;
            this.autoModeCheckBox.CheckedChanged += new System.EventHandler(this.autoModeCheckBox_CheckedChanged);

            // setThresholdBtn
            this.setThresholdBtn.Location = new System.Drawing.Point(270, 20);
            this.setThresholdBtn.Name = "setThresholdBtn";
            this.setThresholdBtn.Size = new System.Drawing.Size(75, 23);
            this.setThresholdBtn.TabIndex = 11;
            this.setThresholdBtn.Text = "Установить";
            this.setThresholdBtn.UseVisualStyleBackColor = true;
            this.setThresholdBtn.Click += new System.EventHandler(this.setThresholdBtn_Click);

            // thresholdNumeric
            this.thresholdNumeric.Location = new System.Drawing.Point(180, 20);
            this.thresholdNumeric.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.thresholdNumeric.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.thresholdNumeric.Name = "thresholdNumeric";
            this.thresholdNumeric.Size = new System.Drawing.Size(80, 20);
            this.thresholdNumeric.TabIndex = 10;
            this.thresholdNumeric.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});

            // label5
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Порог срабатывания (см):";

            // ledStatusLabel
            this.ledStatusLabel.AutoSize = true;
            this.ledStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ledStatusLabel.Location = new System.Drawing.Point(280, 125);
            this.ledStatusLabel.Name = "ledStatusLabel";
            this.ledStatusLabel.Size = new System.Drawing.Size(41, 17);
            this.ledStatusLabel.TabIndex = 8;
            this.ledStatusLabel.Text = "ВЫКЛ";

            // buzzerStatusLabel
            this.buzzerStatusLabel.AutoSize = true;
            this.buzzerStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buzzerStatusLabel.Location = new System.Drawing.Point(280, 95);
            this.buzzerStatusLabel.Name = "buzzerStatusLabel";
            this.buzzerStatusLabel.Size = new System.Drawing.Size(41, 17);
            this.buzzerStatusLabel.TabIndex = 7;
            this.buzzerStatusLabel.Text = "ВЫКЛ";

            // label4
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Красный светодиод:";

            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Пищалка:";

            // testLedBtn
            this.testLedBtn.Location = new System.Drawing.Point(180, 120);
            this.testLedBtn.Name = "testLedBtn";
            this.testLedBtn.Size = new System.Drawing.Size(80, 23);
            this.testLedBtn.TabIndex = 4;
            this.testLedBtn.Text = "Тест LED";
            this.testLedBtn.UseVisualStyleBackColor = true;
            this.testLedBtn.Click += new System.EventHandler(this.testLedBtn_Click);

            // testBuzzerBtn
            this.testBuzzerBtn.Location = new System.Drawing.Point(180, 90);
            this.testBuzzerBtn.Name = "testBuzzerBtn";
            this.testBuzzerBtn.Size = new System.Drawing.Size(80, 23);
            this.testBuzzerBtn.TabIndex = 3;
            this.testBuzzerBtn.Text = "Тест Buzzer";
            this.testBuzzerBtn.UseVisualStyleBackColor = true;
            this.testBuzzerBtn.Click += new System.EventHandler(this.testBuzzerBtn_Click);

            // thresholdValueLabel
            this.thresholdValueLabel.AutoSize = true;
            this.thresholdValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.thresholdValueLabel.Location = new System.Drawing.Point(310, 50);
            this.thresholdValueLabel.Name = "thresholdValueLabel";
            this.thresholdValueLabel.Size = new System.Drawing.Size(29, 20);
            this.thresholdValueLabel.TabIndex = 2;
            this.thresholdValueLabel.Text = "15";

            // thresholdTrackBar
            this.thresholdTrackBar.LargeChange = 5;
            this.thresholdTrackBar.Location = new System.Drawing.Point(10, 50);
            this.thresholdTrackBar.Maximum = 50;
            this.thresholdTrackBar.Minimum = 5;
            this.thresholdTrackBar.Name = "thresholdTrackBar";
            this.thresholdTrackBar.Size = new System.Drawing.Size(290, 45);
            this.thresholdTrackBar.TabIndex = 1;
            this.thresholdTrackBar.Value = 15;
            this.thresholdTrackBar.Scroll += new System.EventHandler(this.thresholdTrackBar_Scroll);

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Регулировка порога:";

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 351);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Управление ультразвуковым датчиком";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdTrackBar)).EndInit();
            this.ResumeLayout(false);
        }
    }
}