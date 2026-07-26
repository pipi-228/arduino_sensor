[Русский](README.md) | **English**

# Arduino Sensor Control — Ultrasonic Sensor Control via Windows Forms

Coursework project for the "Programming Interfaces of Computing Systems" course.
A C# (Windows Forms) application communicates with an **Arduino UNO** board over a serial (COM) port to control a system built around an ultrasonic range finder: an RGB LED and a piezo buzzer.

## How it works

1. The Arduino continuously measures distance with an ultrasonic range finder (HC-SR04).
2. If the distance is below a set threshold, the RGB LED turns red and the buzzer sounds; otherwise the LED stays green.
3. The Windows Forms application connects to the Arduino over a COM port, displays the current status, and lets you:
   - select a COM port and connect/disconnect;
   - set the trigger threshold (via a slider or an input field) and send it to the board;
   - test the buzzer and the RGB LED with dedicated buttons;
   - switch between automatic and manual mode.

Data is exchanged as plain text commands over `SerialPort` (9600 baud).

## Repository contents

| File | Purpose |
|---|---|
| `sketch_arduido.ino` | Arduino sketch: distance measurement, RGB LED and buzzer control, handling commands from the PC |
| `Form1.cs` | Form logic: COM port connection, sending commands, handling incoming data |
| `Form1.Designer.cs` | Auto-generated UI layout (buttons, slider, ComboBox, etc.) |
| `Form1.resx` | Form resources |
| `Program.cs` | Application entry point |
| `WinFormsApp1.csproj` / `WinFormsApp1.csproj.user` | .NET project files |
| `WinFormsApp1.sln` | Visual Studio solution file |

## Wiring (Arduino)

| Arduino pin | Purpose |
|---|---|
| 12 (Trig) | Sends the ultrasonic pulse (HC-SR04) |
| 11 (Echo) | Receives the reflected signal (HC-SR04) |
| 7 | Piezo buzzer |
| 4 | RGB LED — red channel |
| 3 | RGB LED — green channel |
| 2 | RGB LED — blue channel |

## Command protocol (PC → Arduino)

| Command | Action |
|---|---|
| `SET_THRESHOLD:<number>` | Set the trigger threshold in cm (allowed range 5–100) |
| `GET_SETTINGS` | Request current settings (threshold, buzzer/LED state, mode) |
| `TEST_BUZZER` | Test buzzer signal (0.5 s) |
| `TEST_LED` | Test RGB LED cycle (red → green → blue) |
| `MODE:AUTO` | Enable automatic mode (reacts to the sensor) |
| `MODE:MANUAL` | Enable manual mode (buzzer off, LED green) |

In turn, the Arduino sends lines to the PC such as `DIST:<cm>`, `LED:RED`/`LED:GREEN`, `BUZZER:ON`/`BUZZER:OFF`, `THRESHOLD:<number>`, `MODE:AUTO`/`MODE:MANUAL`.

## Requirements

**For the Arduino:**
- Arduino UNO board (or compatible)
- HC-SR04 ultrasonic range finder
- RGB LED (common cathode) and resistors
- Piezo buzzer
- [Arduino IDE](https://www.arduino.cc/en/software)

**For the Windows Forms application:**
- Windows
- [.NET SDK 8.0](https://dotnet.microsoft.com/download) or Visual Studio 2022+ with .NET Desktop Development workload

## Running the project

1. **Flash the Arduino:**
   - Wire the circuit according to the pin table above.
   - Open `sketch_arduido.ino` in the Arduino IDE, select your board and COM port, and upload the sketch.

2. **Run the application:**
   - Open `WinFormsApp1.sln` in Visual Studio (or run `dotnet run` in the project folder).
   - Build and run the project.
   - In the app, select the correct COM port from the list and click "Connect".
   - Set the trigger threshold and click "Set", or test the buzzer/LED with the corresponding buttons.

## Technologies

- C#, Windows Forms, `System.IO.Ports.SerialPort`
- Arduino (C++/Arduino Core), HC-SR04 distance measurement via `pulseIn`
