# Generic Tower Defense

> [!WARNING]  
> This project is a work in progress.

> [!NOTE]  
> This project utilizes free assets created by [Foozle](https://foozlecc.itch.io/).

## Project Overview

**Generic Tower Defense** is a straightforward tower defense game prototype featuring three distinct types of towers and three different enemy types spread across two levels. All interactions within the game are mouse-based.

## Features Implemented

- **Grid System:** Enhances management and interaction with tilemaps for a more intuitive building and navigation experience.
- **Configurable Entities:** Both enemies and towers are configured using scriptable objects, simplifying extension and customization.
- **UI Enhancements:** Includes several helper buttons for resetting tilemaps sizes post-modification, displaying, and saving level data.
- **Event System:** Utilizes scriptable objects as mediators to ensure a scalable and loosely coupled architecture.
- **General Save System:** Employs scriptable objects to record global game progress, ensuring players can pick up where they left off.

## Planned Improvements

- **Save System Enhancements:** Future updates will extend the save system to MonoBehaviour objects, allowing in-level progress saving. Additionally, the system will support multiple save files, accommodating different playthroughs and players.
- **Enemy Spawners:** Reworking enemy spawners to facilitate wave-based spawning. This includes editor enhancements to streamline wave configuration for game designers.
- **Scoring:** Introduction of a score system to provide feedback and rewards based on player performance.
- **Tower Upgrades:** Implementation of a tower upgrade system, allowing players to enhance tower capabilities and adapt strategies over time.

## How to Launch

The project contains all the necessary assets and can be imported into Unity via Unity HUB. It is developed in Unity version 2022.3.15f1.

1. Ensure you have Unity HUB installed.
2. Download or clone this repository to your local machine.
3. Open Unity HUB and go to the 'Projects' tab.
4. Click 'Add' and select the project folder you have just downloaded.
5. Ensure you set the Unity version to 2022.3.15f1.
6. Once the project is loaded, navigate to the `Assets/Scenes` folder and open the `MainMenu` scene to start the game. Alternatively, you can select `File > Build And Run` from the Unity menu; Unity will prompt you to create or choose a folder for the game build. After specifying the folder, Unity will compile the game and launch it.
