#define Trig 12     // Пин для отправки ультразвукового импульса
#define Echo 11     // Пин для приема отраженного сигнала
#define Buzzer 7    // Пин для пьезодинамика
#define RedPin 4    // Красный пин RGB светодиода
#define GreenPin 3  // Зеленый пин RGB светодиода
#define BluePin 2   // Синий пин RGB светодиода

int distance = 0;
int thresholdDistance = 15; // Порог по умолчанию 15 см
bool autoMode = true; // Автоматический режим по умолчанию

String inputString = "";
bool stringComplete = false;

void setup() {
    // Настройка пинов
    pinMode(Trig, OUTPUT);    // Пин Trig настроен как выход
    pinMode(Echo, INPUT);     // Пин Echo настроен как вход
    pinMode(Buzzer, OUTPUT);  // Пин для пьезодинамика
    pinMode(RedPin, OUTPUT);  // Красный пин RGB
    pinMode(GreenPin, OUTPUT);// Зеленый пин RGB
    pinMode(BluePin, OUTPUT); // Синий пин RGB
    
    // Изначально выключаем все цвета
    digitalWrite(RedPin, LOW);
    digitalWrite(GreenPin, LOW);
    digitalWrite(BluePin, LOW);
    
    // Включаем зеленый при запуске
    setRgbColor(0, 255, 0); // Зеленый
    
    Serial.begin(9600);      // Скорость передачи
    Serial.println("Система ультразвукового датчика с RGB светодиодом готова");
    Serial.println("Команды:");
    Serial.println("SET_THRESHOLD:<число> - установить порог (см)");
    Serial.println("GET_SETTINGS - получить текущие настройки");
    Serial.println("TEST_BUZZER - тест пищалки");
    Serial.println("TEST_LED - тест светодиода");
    Serial.println("MODE:AUTO - автоматический режим");
    Serial.println("MODE:MANUAL - ручной режим");
}

void loop() {
    // Обработка входящих команд
    serialEvent();
    
    if (stringComplete) {
        processCommand(inputString);
        inputString = "";
        stringComplete = false;
    }
    
    // Измерение расстояния
    measureDistance();
    
    // Отправка данных на компьютер
    Serial.print("DIST:");
    Serial.println(distance);
    
    // Автоматический режим управления
    if (autoMode) {
        // Проверяем расстояние и управляем светодиодом и пьезодинамиком
        if (distance < thresholdDistance && distance > 0) {
            // Близко - красный свет и звук
            setRgbColor(255, 0, 0); // Красный
            tone(Buzzer, 1000);     // Включаем звук
            Serial.println("LED:RED");
            Serial.println("BUZZER:ON");
            Serial.println("Опасность! Слишком близко!");
        } else {
            // Нормальное расстояние - зеленый свет и нет звука
            setRgbColor(0, 255, 0); // Зеленый
            noTone(Buzzer);         // Выключаем звук
            Serial.println("LED:GREEN");
            Serial.println("BUZZER:OFF");
        }
    }
    
    delay(100); // Задержка между измерениями
}

void measureDistance() {
    // Генерация ультразвукового импульса
    digitalWrite(Trig, HIGH);
    delayMicroseconds(10);
    digitalWrite(Trig, LOW);

    // Измерение длительности отраженного импульса
    unsigned long impulse = pulseIn(Echo, HIGH);
    distance = impulse / 58; // Расстояние в сантиметрах
}

void setRgbColor(int red, int green, int blue) {
    // Для общего катода - напрямую
    analogWrite(RedPin, red);
    analogWrite(GreenPin, green);
    analogWrite(BluePin, blue);
}

void serialEvent() {
    while (Serial.available()) {
        char inChar = (char)Serial.read();
        if (inChar == '\n') {
            stringComplete = true;
        } else {
            inputString += inChar;
        }
    }
}

void processCommand(String command) {
    command.trim();
    
    if (command.startsWith("SET_THRESHOLD:")) {
        // Установка нового порога
        String valueStr = command.substring(14);
        int newThreshold = valueStr.toInt();
        
        if (newThreshold >= 5 && newThreshold <= 100) {
            thresholdDistance = newThreshold;
            Serial.print("THRESHOLD:");
            Serial.println(thresholdDistance);
            Serial.println("Порог изменен");
        } else {
            Serial.println("Ошибка: порог должен быть от 5 до 100 см");
        }
    }
    else if (command == "GET_SETTINGS") {
        // Отправка текущих настроек
        Serial.print("THRESHOLD:");
        Serial.println(thresholdDistance);
        
        Serial.print("BUZZER:");
        Serial.println(digitalRead(Buzzer) == HIGH ? "ON" : "OFF");
        
        // Определяем текущий цвет
        if (distance < thresholdDistance && distance > 0) {
            Serial.println("LED:RED");
        } else {
            Serial.println("LED:GREEN");
        }
        
        Serial.print("MODE:");
        Serial.println(autoMode ? "AUTO" : "MANUAL");
    }
    else if (command == "TEST_BUZZER") {
        // Тест пищалки
        Serial.println("BUZZER:ON");
        tone(Buzzer, 1000);
        delay(500);
        noTone(Buzzer);
        Serial.println("BUZZER:OFF");
        Serial.println("Тест пищалки завершен");
    }
    else if (command == "TEST_LED") {
        // Тест RGB светодиода
        Serial.println("LED:RED");
        setRgbColor(255, 0, 0);
        delay(500);
        
        Serial.println("LED:GREEN");
        setRgbColor(0, 255, 0);
        delay(500);
        
        Serial.println("LED:BLUE");
        setRgbColor(0, 0, 255);
        delay(500);
        
        // Возвращаемся к нормальному состоянию
        if (autoMode) {
            if (distance < thresholdDistance && distance > 0) {
                setRgbColor(255, 0, 0);
                Serial.println("LED:RED");
            } else {
                setRgbColor(0, 255, 0);
                Serial.println("LED:GREEN");
            }
        } else {
            // В ручном режиме возвращаемся к последнему цвету
            setRgbColor(0, 255, 0);
            Serial.println("LED:GREEN");
        }
        
        Serial.println("Тест RGB светодиода завершен");
    }
    else if (command == "MODE:AUTO") {
        // Включение автоматического режима
        autoMode = true;
        Serial.println("MODE:AUTO");
        Serial.println("Автоматический режим включен");
    }
    else if (command == "MODE:MANUAL") {
        // Включение ручного режима
        autoMode = false;
        // Выключаем пищалку при переходе в ручной режим
        noTone(Buzzer);
        // Включаем зеленый свет
        setRgbColor(0, 255, 0);
        Serial.println("MODE:MANUAL");
        Serial.println("BUZZER:OFF");
        Serial.println("LED:GREEN");
        Serial.println("Ручной режим включен");
    }
    else {
        Serial.print("Неизвестная команда: ");
        Serial.println(command);
    }
}