# Marauder-No.-9

## Summary 
&emsp;Marauder No. 9 is a 2.5-D shooter-platformer which takes place in a future where human space travel is commonplace and bounty hunters are prevalent. In this game, the player is an infamous bounty hunter known only as 'Marauder-09' aboard a hostile space vessel guarded by robot enemies. Armed with a dangerous arsenal and equipped with advanced power-armor, the player must engage in dangerous shootouts with the ship's robot crew. It is the player's goal to clear the vessel and make their way to the end of the level.

&emsp;This project is targeted towards challenge-motivated gamers who enjoy playing action games and/or shooters. Our targeted audience might show similar interest in games such as Metroid, Dead Cells, FromSoftware games, etc. This project aims to engage this audience through intuitive and compelling combat systems and well-constructed combat encounters.

## Demo Video
[![Gameplay](https://img.itch.zone/aW1hZ2UvMjM3MzU2My8xNDA2NTE1OC5wbmc=/original/Zk20Of.png)](https://www.youtube.com/watch?v=OOYhYw7d50w&t=1s)
Click to Play

## My Role
&emsp;I worked as a programmer on this project. We were working in Unity, so the scripts were written in C#. My job was to create and implement the majority of the functionality present in the game. I communicated with the project manager to understand what systems needed to be present, as well as how he wanted them implemented. This repository contains only all the scripts I worked on; You can find access to the rest of the project in the __Where to Find the Project__ section. You can find a breakdown of what each script does in the __Code Breakdown__ section.

## Where to Find the Project
- For a full list of credits and a playable version of the game, visit the [itch.io](https://matt-012.itch.io/marauder-no-9) page
- For the production breakdown of the entire project, visit the [Miro board](https://miro.com/app/board/uXjVMjeFY_Q=/?share_link_id=106949044377)
- Additionally, you can visit the project's [Github Repository](https://github.com/Matthew078/Marauder-No.-9)

## Code Breakdown
Here is a list of all the scripts I created for this project and the functionality built into each one:

__Bullet__
- Deletes itself based on the distance traveled
 <br>

__Gun__
- Has 3 types of firing mode
  - Auto - Hold to shoot
  - Semi - Click to shoot
  - burst - Click to shoot multiple
- Customizable variables for modular implementation:
  - Bullets - speed, damage, range
  - Gun - Firerate, firetype, ammo
- Sets variables on death
- Processes player click
- Fires gun by instantiating bullets
 <br>

__WeaponControl__
- Process Player Inputs
- Call onClick function of player's current gun
- Swap gun
  - Detect all guns in proximity
  - Drop current gun
  - Equip new gun
 <br>

__Shield__
- Processes player input
  - Turn on and off
- Reflects or Destroys bullets depending on time
- Break at 0 health, remain unusable for a short period
- Update shield health and state
 <br>

__Sound__
- Class containing a string name and an AudioClip
- Used by SoundManager to map string names to AudioClips for easy access
 <br>

__SoundManager__
- Stores all SFX and music in Sound Arrays
- Allows any script to play a sound by calling its functions with the sound's name
- Uses multiple functions to implement overlapping sounds
- Adjusts volume based on variables provided by the Game Manager
- Design originated from [this youtube video](https://www.youtube.com/watch?v=rdX7nhH6jdM)
 <br>

__Enemy__
- Has Four States
  - Idle
      - Enters when patrol reaches destination
      - Stand still, then return to patrol
  - Patrol
      - Walks to a point
      - After reaching point, switch destinations and enter idle
      - Destinations implemented modularly with tranform objects
  - Attack
      - Enters whenever player is in proximity
      - Walk towards player and shoot
      - If player not in vicinity, exit to patrol
  - Stunned
    - Enters when enemy is hit by grenade
    - Used to deactivate NavMesh as to not interfere with Rigidbody
- Has function for processing grenade hits
- Detects when it has been hit by bullet
- Dies upon reach 0 health
 <br>

__Grenade__
- Contains a timer
- Contains a detonate function that detects all enemies in the proximity
 <br>

__GrenadeThrow__
- Processes Player input
  - Single tap to throw
  - Double tap to detonate
- Detonates grenades based on time
- Instanstiate new grenade on throw
 <br>

__Loot__
- Detects when loot hits the ground and stops falling
- Implemented so player can walk through and detect the loot
 <br>

__LootSpawn__
- Creates an explosion of loot by instantiating loot objects and adding an explosiong force
- Called on enemy death
 <br>

