# 🚖 Taxi Driver — Final Project (Programming Paradigms and Techniques)

**Course:** Paradigmas y Técnicas de Programación  
**University:** Universidad Pontificia Comillas (ICAI)  
**Authors:** Blanca López-Jorrín Pérez · Victoria Guillén de la Torre  
**Year:** 2024–2025  

---

## 🧠 Overview

**Taxi Driver** is a 3D simulation game developed in **Unity (C#)** where the player takes on the role of a taxi driver navigating a city, picking up passengers, and managing time, money, and vehicle conditions — all while avoiding obstacles and police penalties.

The project integrates multiple programming paradigms (object-oriented, event-driven, and component-based programming) and advanced Unity systems such as **NavMesh**, **coroutines**, **custom events**, and **prefab-based object management**.

---

## 🕹️ Gameplay

The player drives a taxi through a low-poly city, picks up passengers, and delivers them to their destinations to earn money.  
The challenge lies in avoiding **speeding fines**, **police chases**, and **collisions with obstacles**, all of which affect the taxi’s health and balance.

---

## ⚙️ Main Features

### 🗺️ Environment
- Modular map system managed through **RoadTile** and **RoadObject** classes.
- Pathfinding implemented via **Breadth-First Search (BFS)** for optimal navigation.
- Dynamic spawning of **passengers** and **obstacles** using random tile selection.

### 🚕 Taxi
- Controlled via a **state machine**:
  1. `LookingForPassenger`
  2. `GoingToPickUp`
  3. `TransportingPassenger`
  4. `FinishingRide`
- Manages **speed**, **health**, and **revenue** in real time.
- Interacts with environment and other game systems.

### 🚓 Police Car
- Uses **NavMeshAgent** for AI-driven pursuits of speeding taxis.
- Event-driven design: listens to `OnSpeedViolationDetected`.
- Includes a **penalty system** integrated with the **Bank** class.
- Returns to base automatically after each pursuit.

### 📡 Radar
- Detects speeding taxis within a defined radius.
- Triggers custom events to alert the police system.

### 🚧 Obstacles
- Two main types:
  - **FenceObstacle**: reduces speed and health.
  - **WeakenerObstacle**: temporarily slows down the taxi.
- Created using a **Factory Pattern** for flexibility and scalability.
- Can be destroyed by the player (if enough balance is available).

### 🧍 Passengers
- Generated dynamically via the **PassengerPool** system.
- Random spawn positions on valid road tiles.
- Implemented behavior for pickup, transport, and drop-off cycles.

### 🏦 Bank
- Central system managing player balance:
  - **Deposits**: successful rides.
  - **Withdrawals**: penalties or obstacle destruction.
- Updates displayed balance through **TextMeshProUGUI**.

### 🕹️ Game Controller
- Coordinates game systems and manages victory/loss conditions.
- Handles spawning, events, and state transitions.

### 🎥 Cameras
- **Main Camera**: follows the taxi dynamically.
- **MiniMap Camera**: provides a top-down view of the city.

### 📨 Notice Manager
- Displays on-screen notifications for key events (fines, ride completions, collisions).
- Event-driven and time-limited messages.

---

## 🧩 Project Structure

```
PF-Taxi_Driver/
│
├── Assets/
│   ├── Bank/
│   ├── NoticeBoard/
│   ├── Obstacles/
│   ├── Passenger/
│   ├── Road/
│   ├── Scenes/
│   ├── Scripts/
│   │   ├── CameraController.cs
│   │   ├── CarController.cs
│   │   ├── GameController.cs
│   │   └── (other managers and helpers)
│   ├── SpeedManager/
│   ├── Vehicle/
│   ├── TrafficCamera/
│   └── TextMesh Pro/
│
├── ProjectSettings/
├── Packages/
├── README.md
├── TaxiDriver_Report.pdf
└── TaxiDriver_UML.pdf
```

---

## 🧱 Technologies Used

- **Unity** (2022 or later)
- **C#**
- **TextMesh Pro**
- **NavMesh & AI Agents**
- **Event-driven programming**
- **OOP & State Machines**

---

## 🚀 How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/victoriaguillen/TaxiDriver-UnityGame.git
   ```
2. Open the folder `PF-Taxi_Driver` in **Unity Hub**.
3. Load the main scene from `Assets/Scenes`.
4. Press ▶️ **Play** to start the game.

---

## 🧾 Documentation

- 📘 **TaxiDriver_Report.pdf** — Full technical documentation.  
- 📈 **TaxiDriver_UML.pdf** — UML diagrams of class relationships and architecture.

---

## 👩‍💻 Authors

- **Victoria Guillén de la Torre**  
- **Blanca López-Jorrín Pérez**

---

## 🏁 Summary

This project combines **game development**, **AI logic**, and **software architecture principles** into a complete interactive simulation.  
It demonstrates strong skills in **object-oriented design**, **Unity scripting**, and **system integration** through modular and event-driven programming.


