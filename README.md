# Fire Department Quiz - Extinguishing Agents and Methods
---
## 🚀 Overview
The **Fire Department Quiz** is an interactive **Android educational app** developed with the **Unity Engine**. This specialized training application is designed specifically for **fire department members** to test and deepen their knowledge of extinguishing agents and firefighting methods across all fire classes.

The app presents users with **5 realistic 3D fire scenarios** featuring animated fire and smoke effects, where each scenario represents one of the official fire classes. Users must identify the fire class and answer **3 questions per scenario** (15 total questions) about appropriate extinguishing agents and methods. All educational content is based on official fire safety guidelines from the **Würzburg Fire Academy**.

This project serves as a **practical demonstration** of developing an immersive educational mobile application using Unity, highlighting concepts such as 3D scene development, interactive quiz mechanics, and Android deployment for professional training purposes.

---
## 🛠️ Technologies
* **Game Engine:** Unity (version: **2022.3 LTS**)
* **Programming Language:** C#
* **Target Platform:** Android (API Level 24+)
* **3D Assets:** Unity Asset Store resources
* **Educational Content:** Based on Würzburg Fire Academy guidelines

---
## 📦 Installation & Usage
You have two main options to explore and test this project:

### Testing in Unity Editor with Unity Remote (Recommended for Developers)
This method is ideal for quickly testing changes and debugging the app on your Android device in real-time, without building a full APK every time.

1.  **Clone the Repository:**
    ```bash
    git clone https://github.com/YOUR_USERNAME/FireDepartmentQuiz.git
    cd FireDepartmentQuiz
    ```
    *Replace *`YOUR_USERNAME`* with your GitHub username.*

2.  **Set up Unity Remote 5 on your Android Device:**
    * Install the **"Unity Remote 5"** app from the Google Play Store on your Android device.
    * Enable **Developer Options** on your device (usually by tapping the "Build number" in phone information 7 times).
    * Enable **USB Debugging** in Developer Options.
    * Connect your device to your computer via USB cable and select **"File transfer"** or **"PTP"** as the USB connection mode.

3.  **Open the Project in Unity:**
    * Start **Unity Hub**.
    * Click on **"Add Project from Disk"** and select the cloned `FireDepartmentQuiz` folder.
    * Ensure the **correct Unity version** (2022.3 LTS) is selected.

4.  **Configure Unity Editor for Remote Connection:**
    * In Unity, go to `Edit > Project Settings`.
    * In the "Project Settings" window, select the **`Editor`** section on the left.
    * Scroll down to the **"Unity Remote"** section and select your connected Android device under **"Device"**.
    * Make sure the Unity Remote 5 app is running on your phone.

5.  **Start the Quiz:**
    * Open the main scene of the app (usually located under `Assets/Scenes/MainMenu.unity` or `Assets/Scenes/QuizScene.unity`).
    * Click the **Play button** in the Unity Editor. The quiz should now be displayed synchronously on your Android device, and you can interact with it via your phone's touchscreen.

### Building APK for Standalone Installation

1.  **Clone and Open Project** (steps 1-3 from above)

2.  **Configure Android Build Settings:**
    * Go to `File > Build Settings`
    * Select **Android** as the target platform
    * Click **"Switch Platform"** if not already selected

3.  **Configure Project Settings for Android:**
    * Go to `Edit > Project Settings`
    * Navigate to **Player Settings**
    * Set **Target Architecture** to ARM64 or ARM32 (depending on your target devices)
    * Configure **Package Name**, **Version**, and other Android-specific settings

4.  **Build APK:**
    * Return to `File > Build Settings`
    * Click **"Build"** or **"Build and Run"** to generate the APK file
    * Choose a location to save the APK

5.  **Install on Android Device:**
    * Enable **"Unknown Sources"** in your Android device settings
    * Transfer the APK to your device and install it
    * Launch the **Fire Department Quiz** app

---
**Developed with ❤️ for our brave firefighters - Supporting those who risk their lives to protect others!**
