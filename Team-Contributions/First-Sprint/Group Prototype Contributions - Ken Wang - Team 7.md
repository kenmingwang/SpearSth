### Ken Wang - Team 7

------

### Analysis

- Role: Engineer
- Task / Contributions:
    1. Creating the [Github repo](https://github.com/kenmingwang/SpearSth) for the team as well as managing branching and merging of each build. *(About an hour)*
    2. Implementing the core feature of Spear throw / recall as well as the spear interaction with the main player, enemies, and walls. *(About 12 hours)*
        - [link to code](https://github.com/kenmingwang/SpearSth/blob/master/Assets/Scripts/Spear.cs) 
    3. Implemented the Enemey health system, as well as the dynamic UI health bar on their head. *(About 3 hours)*
        - [link to code](https://github.com/kenmingwang/SpearSth/blob/master/Assets/Scripts/EnemyParent.cs)
    5. Added sound effect scripts*, as well as applied the enemy animation* and other Sprite* UI in to the game. *(About an hour)*
        - *Sound effects, animation, and sprites are provided by other teammates, I just gathered them and put them in the right spot.
        - ```
            // Part of the audio
            public void TriggerThrow()
            {
                audioSource.PlayOneShot(throwAudio, 0.4f);
                isThrowTriggered = true;
            }
          ```
    6. Created the overall game-manager to controll the status of the game. Currently is just a fail status. *(About an hour)*
        - [link to code](https://github.com/kenmingwang/SpearSth/blob/master/Assets/Scripts/GameManager.cs)
    7. Implemented Camera movement.  (About 10 mins).
        - Just dragging the main camera to be the child of the player as well as reposistion it a little.