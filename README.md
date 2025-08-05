# 🧟‍♂️ Apocalypse Survivors VR

**A terrifying VR horror FPS experience where survival is the only victory.**

[![Unity](https://img.shields.io/badge/Unity-2022.3+-blue.svg)](https://unity3d.com/)
[![VR](https://img.shields.io/badge/VR-Oculus%20Quest%20|%20PCVR-green.svg)](https://www.meta.com/quest/)
[![Platform](https://img.shields.io/badge/Platform-Windows%20|%20Android-orange.svg)](https://unity3d.com/unity/features/multiplatform)

## 🎮 Game Overview

**Apocalypse Survivors VR** is an immersive first-person horror shooter that drops you into a post-apocalyptic cityscape overrun by terrifying monsters. Inspired by the intensity of *Left 4 Dead 2 VR*, this game delivers heart-pounding action where every moment could be your last.

### 🎯 Core Experience
- **Survive at all costs** in a realistic urban environment
- **Face relentless enemies** that spawn continuously across four strategic hotspots
- **Scavenge for resources** - ammo and health syringes drop from defeated foes
- **Track your survival time** with an integrated wristwatch UI
- **Compete for high scores** - how long can you stay alive?

## 🏙️ Environment & Setting

The game takes place in a meticulously crafted **4-way street intersection** surrounded by:
- **Blocked streets** with police cars and rubble creating natural boundaries
- **Realistic urban assets** including vehicles, buildings, and environmental details
- **Dynamic lighting** that enhances the horror atmosphere
- **Spatial audio** that lets you hear enemies approaching from all directions

### 🔫 Arsenal & Equipment
- **Handguns** - Reliable sidearms for close encounters
- **Shotguns** - Devastating close-range stopping power
- **Rifles** - Precision for distant threats
- **Utility Belt** - Store ammo and health syringes for quick access

## 🎮 Controls & VR Features

### Locomotion
- **Thumb stick navigation** for smooth movement
- **Realistic physics** with proper collision detection
- **Grounded movement** system preventing unrealistic traversal

### Interaction System
- **UxrAvatar integration** for hand presence and manipulation
- **Grab mechanics** for picking up weapons and items
- **Weapon handling** with realistic reloading and aiming

### UI Elements
- **Wrist-mounted display** showing health and survival time
- **Enemy health bars** visible when threats are nearby
- **Contextual UI** for weapon pickups and interactions

## 🧟‍♂️ Enemy System

### Enemy Types
- **Standard infected** - Fast, aggressive melee attackers
- **Special variants** - Unique behaviors and attack patterns
- **Continuous spawning** from 4 strategic hotspot locations

### AI Behavior
- **Proximity-based attacks** - Enemies engage when you're within range
- **Dynamic spawning** - New threats appear based on player location
- **Loot drops** - Enemies provide essential resources upon defeat

## 🎵 Audio & Atmosphere

### Sound Design
- **Ominous background music** that builds tension
- **Spatial audio** for enemy location awareness
- **Realistic weapon sounds** and environmental audio
- **Heartbeat audio** that intensifies as health decreases

### Visual Effects
- **Damage vignette** system for injury feedback
- **Post-processing effects** for horror atmosphere
- **Realistic lighting** with dynamic shadows

## 🛠️ Technical Implementation

### Core Systems
- **UltimateXR framework** for VR interactions and locomotion
- **Unity's Universal Render Pipeline** for optimized graphics
- **NavMesh system** for enemy AI navigation
- **Physics-based interactions** for realistic object handling

### Key Scripts
- `Player.cs` - Comprehensive player health, damage, and VR interaction system
- `Enemy.cs` - Enemy AI behavior and combat mechanics
- `EnemySpawnController.cs` - Dynamic enemy spawning system
- `PlayerHealthBar.cs` - VR-optimized health display system

### Performance Features
- **Optimized for Quest/PCVR** with scalable graphics settings
- **Efficient enemy spawning** to maintain performance
- **Memory management** for extended play sessions

## 🚀 Getting Started

### Prerequisites
- **Unity 2022.3 LTS** or newer
- **UltimateXR** package installed
- **VR Headset** (Oculus Quest, Quest 2, or PCVR compatible)

### Installation
1. Clone this repository
2. Open the project in Unity
3. Install required packages:
   - UltimateXR
   - TextMeshPro
   - Universal Render Pipeline
4. Configure VR settings for your target platform
5. Build and deploy to your VR device

### Build Instructions
```bash
# For Oculus Quest
1. Switch platform to Android
2. Configure XR settings for Oculus
3. Build APK and sideload to Quest

# For PCVR
1. Switch platform to Windows
2. Configure XR settings for OpenXR
3. Build and run executable
```

## 🎯 Gameplay Tips

### Survival Strategies
- **Keep moving** - Standing still makes you an easy target
- **Manage resources** - Don't waste ammo on distant enemies
- **Use cover** - Buildings and vehicles provide temporary protection
- **Monitor health** - The wristwatch is your lifeline

### Advanced Techniques
- **Kiting enemies** - Lead them away from spawn points
- **Ammo conservation** - Switch weapons based on enemy distance
- **Health management** - Use syringes strategically, not reactively

## 🎥 Media & Resources

### Gameplay Videos
- **[Walkthrough Video](https://www.youtube.com/watch?v=U-XgCW5fjq4)** - Complete gameplay demonstration

### Screenshots
- **Horror atmosphere** with realistic urban decay
- **VR interaction** showing weapon handling
- **Enemy encounters** demonstrating the intensity

## 🤝 Contributing

We welcome contributions! Please see our [Contributing Guidelines](CONTRIBUTING.md) for details on:
- Bug reports and feature requests
- Code contributions and pull requests
- Testing and feedback

## 📄 License

This project is created for educational purposes. Assets from the Unity Asset Store are used under their respective licenses.

## 🙏 Acknowledgments

- **Unity Technologies** for the game engine
- **UltimateXR team** for the VR framework
- **Asset Store creators** for 3D models and environments
- **Left 4 Dead 2** for gameplay inspiration

---

**Ready to face the apocalypse?** Strap on your VR headset and see how long you can survive in this terrifying urban nightmare.

*Built with ❤️ for the VR horror gaming community*
