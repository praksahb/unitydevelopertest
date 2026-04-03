# Gravity Manipulation Puzzle - Unity Developer Task

A 3D puzzle-platformer where players must navigate a complex environment by manipulating gravity to collect all cubes within a strict time limit.

## Gameplay & Objectives

The primary goal is to **collect all gold cubes** scattered across the level. The environment requires creative movement and gravity shifts to reach distant platforms and avoid falling into the void.

*   **Time Limit**: 2 Minutes.
*   **Win Condition**: Collect all cubes before the timer hits zero.
*   **Game Over**: Falling off the platform or running out of time.

## Controls

| Action | Input |
| :--- | :--- |
| **Movement** | `W`, `A`, `S`, `D` |
| **Jump** | `Space` |
| **Select Gravity Direction** | `Arrow Keys` (Displays Hologram Indicator) |
| **Apply Gravity Shift** | `Enter` |

---

## Technical Architecture

### 1. Service-Oriented Architecture (SOA)
The core game logic is decoupled into specialized services, managed via a central `GameManager`:
*   **GravityService**: Manages the "World Spin" logic and environmental rotation.
*   **PlayerService**: Handles character states, movement, and input processing.
*   **UIService**: Orchestrates the HUD (Timer, Score) and Game Over screens using event-driven updates.

### 2. Model-View-Controller (MVC) Pattern
Entities like the Player follow a strict MVC separation:
*   **Models**: Reactive data containers for states like gravity direction and player velocity.
*   **Views**: Responsible currently only for connecting the controller with the monobehaviour derived functionalites.
*   **Controllers**: Pure logic layers that process inputs and update Models.

### 3. Key Technical Features
*   **Relative Gravity Physics**: Implemented using a "World Rotation" approach rather than standard `Physics.gravity` for smoother platforming.
*   **Namespace Organization**: All code is strictly organized under the `DevTest.*` namespace.

---

*   **Unity Version**: Unity 6 (6000.0.6f1).
*   **Render Pipeline**: Universal Render Pipeline (URP).
