# 🎮 LP2B – Arcade Game Collection  

This project brings together **three mini-games** developed as part of the **LP2B** course at UTBM in 2023:
- 🍎 **Apple Catcher**
- 🧱 **Breakout**  
- 🐦 **Furapi Bird**  

Each game has its own atmosphere, mechanics, and music.  

---

## 🍎 Apple Catcher  

### 🧩 Main Features
- **Pause Menu**:
  - `ESC` → Opens a menu with:
    - 🔁 Restart the game  
    - 🏠 Return to the main menu  
  - `ESC` again → Resume the game  

- **Game Duration**: 1 min 30 sec  
- At the end → **Game over whistle** + **end screen**  

### 🍏 Collectibles
Each object has:
- A **unique sound**
- A **specific effect**
- A **different spawn probability**

| Object | Effect | Details |
|:-------|:-------|:--------|
| 🍎 **Normal Apple** | +1 point | Base object |
| 🪙 **Coin** | +5 points | Rarer |
| 🍏 **Rotten Apple** | -10 points | Penalty |
| ⏱️ **Timer** | +10 sec | Extends duration |
| 🌈 **Rainbow Apple** | +5 points + activates **FRENZY TIME** | Special state |
| ✨ **Golden Apple** | +1 point | Appears **only during FRENZY TIME** |

### 🔥 FRENZY TIME
- Activated by the **rainbow apple**  
- Duration: **20 seconds**  
- Effects:
  - **Golden apples** fall twice as fast  
  - A **special music** plays

---

## 🧱 Breakout  

### 🕹️ General Operation
- Upon game launch:
  - A **startup music** plays.  
  - The text **“Press SPACE to start”** appears.  
  - The **paddle** and **ball** are visible in their starting positions.  
- The interface displays:
  - ❤️ Number of lives (top right)  
  - 🔢 Current level (top center)  
  - 💯 Score (top left)  

### 🚀 Game Flow
- Pressing **SPACE** **generates the level** (2 alternating level patterns).  
- After **2 seconds**, the ball is launched toward the bricks.  
- Each **hit brick** plays a **sound**, and has a **5% chance** of dropping an **item**.  

### 🎁 Items
- 🪙 **Coin**: +100 points  
- 🧩 **Small paddle**: +1 life  
- Upon collection, a **success sound** plays.  

### 🧱 Level Progression
- When all bricks are destroyed:
  - A **congratulations message** appears.  
  - You are prompted to **press space** to advance to the next level.  
- Every **2 levels**, brick **resistance** increases (1 extra hit required).  
- **Brick colors** change based on their resistance.  
- There are **18 levels** in total → at the end, it takes **10 hits** to break a brick.  

### 🔊 Sounds
- Ball/paddle collision: **specific sound**  
- Ball/wall collision: **another sound**  
- Ball lost: **failure sound**  
- Game over: **game over music**  

### 💀 Game Over
- If the player:
  - Loses all lives  
  - Or completes level 18  
  → An **end screen** appears with:
    - The **final score**  
    - A **button to replay** or **return to the main menu**  

### 🧰 Special Functions
- Automatic correction if the **ball gets stuck** between walls.  
- The **ball speed** is always constant.  

### 🧪 Cheat Codes
- `TAB` → +1 life  
- `A` → Skip a **2-level pattern**  

---

## 🐦 Furapi Bird  

### 🎬 Game Launch
- A **music** starts.  
- The text **“Ready”** appears.  
- **Mountains** and **clouds** move at different speeds to create a **depth effect**.  

### 🎮 Gameplay
- Pressing **SPACE**:
  - Starts the game.  
  - **Pipes** appear with:
    - **Random colors**  
    - **Random Y position**  
  - The **bird** jumps, **crows**, and **opens its beak** in a small animation.  

### 🧾 Score
- The score increases by **1 meter per second** (distance traveled).  

### 💀 Game Over
- If the bird **hits a pipe** or **falls off-screen**:
  - It **screams**, a **death animation** plays.  
  - Time stops.  
- The **end screen** displays:
  - The **final score**  
  - A **button to replay** or **return to the main menu**  

### 🔊 Secret Sounds
- Score < 50 → sound 1  
- 50 ≤ Score < 100 → sound 2  
- Score ≥ 100 → sound 3  

### 🧪 Cheat Code
- `A` → Increases the score (for testing)  

---

### 🏁 Authors Jzedek and Yannthomas000
LP2B Project – Retro Mini-Game Collection.
