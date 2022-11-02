# 3110-BonusQuiz

## Student Info

- Jeffrey Li - 100712344
- 1 + 0 + 0 + 7 + 1 + 2 + 3 + 4 + 4 = 22 (Even number)

## Singleton pattern implementation explaination
- The health manager would create a static and public instance for itself, so that all other systems are able to use it. It also checks if there is any other instance to avoid recreating. It has an integer variable as health point and a method to change the health point.
## Command pattern implementation explaination
- The brick braking system is created by command pattern. By using this pattern, it would allow players to undo what they broke. Everytime that the player brakes a brick, the system would add that brick into the command list. When player clicks Z to undo the brake, the command system would get the broken bricks from command list, and then instantiate it to the game scene.
## Observer Pattern implementation explaination
- Flowchart
  - ![Flowchart](https://raw.githubusercontent.com/jeffrey9911/3110-Tutorial-Project/main/ExternalAssetsKits/Diagram.png)
- Explaination
  - The use case for observer pattern in my game would be an adaptive scene audio system. An observer system would be integrated into the player controller system. It would allow other systems to subscribe it. When certain event happen, it would notify different subscribers depending on the event. In my case, when the player gets into certain area, the observer system would notify the scene controller and tell the scene controller which area the player is in. Once the scene controller gets the notification from the observer system, it would change the background music. (for example, when player gets into a boss area, the background music would change).

## Some Instructions added after submission
- Game basic mechanic
  - ***WASD to move***: A is to move left. D is to move right. W is to jump. S has no functions yet.
  - ***SPACE to brake***: Space is to brake **all the bricks that are in the range of 3 units around the player**. So if you want to brake the bricks above the player, **plase jump first**, otherwise it would brake all the bricks under the player.
  - ***Z to undo the brake***
- Main Character health manager
  - Health system is created and linked to the UI text. If the player hits the bee, he would lose 10 health. If the player hits the skull, he would lose 50 health.
- Brick restore system
  - Brick system can restore the destroyed bricks that were broken by the player.

## Some comments added
The class time is way too short to build a fully functioning game demo from nothing. The work was rushed during class time so there are some bugs. I am really into this activity so I fixed those bugs after class and created a specific branch called *AfterClassWork*. The second executable is also released for it.
### What I did in second release:
- Bugs are fixed and undo system is now fully working.
- The restored bricks now can be destoryed again.
- Now player can destory only one brick that is the nearest to the player. 
- Quit button added to quit the application.
### These are just to prove my knowledge and also implementation of it. Hopefully I can get some more marks :) 
