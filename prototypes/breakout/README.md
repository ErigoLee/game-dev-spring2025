# game-dev-spring2025
Repository for game development I class in Spring 2025


# Assignment1: [Break-out game](https://erigolee.github.io/game-dev-spring2025/builds/breakout-1/)
Based on the materials from the lecture, I customized the Breakout game.
I divided the blocks into five different colors.
If the ball hits a blue block, its velocity remains unchanged.
However, if the ball hits a red block, its velocity increases.
If the ball goes below the base object, it enters the trigger box and then disappears.
If you press the space bar, the ball reappears above the base object.
![image](https://github.com/user-attachments/assets/c65cd4e2-b68b-4329-a94d-e9d84140c7fc)


# Assignmnet2: Break-out game
By incorporating the Polish element with my groupmate, each Breakout game adds more fun elements than before.

2a: Based on my own Breakout game, we made several improvements. I added particle and sound effects to enhance the overall gaming experience. While reviewing the game, my groupmate pointed out that the paddle passed through the wall and suggested using the Mathf.Clamp function to restrict the paddle's movement. Reflecting on her feedback, I implemented the restriction. Additionally, she proposed adding a level-up feature after completing the game. However, I do not have enough time to implement this feature at the moment. Despite this, I plan to work on this function in the future.

2b: While reviewing my groupmate's Breakout game, I suggested adding sound and particle effects to enhance the fun of the game. Sound effects, like the paddle hitting the ball or bricks breaking, would improve the gaming experience. Particle effects could be used to visually emphasize moments like breaking bricks. Additionally, the structure of her game, where each player competes, is designed to invoke fun, so I will reference her approach when I develop other games.

[2a Break-out game](https://erigolee.github.io/game-dev-spring2025/builds/breakout-2a/)
Please play my Breakout game! 
![image](https://github.com/user-attachments/assets/cf598934-c2cc-4a17-ae57-8685287d7720)

[2b Break-out game](https://erigolee.github.io/game-dev-spring2025/builds/breakout-2b/)
Please play my Breakout game! 
![image](https://github.com/user-attachments/assets/0439783a-e38e-464f-837a-ffc21b0920c6)

# Assignment3: [Break-out game](https://erigolee.github.io/game-dev-spring2025/builds/breakout-3/)
I received feedback that my game lacked innovation, so I implemented a feature to change the ball's direction using the mouse click function in the level-2 game. However, I encountered an issue where the direction of the ball did not change in the web version of the game after building, even though the ball's direction changed when I clicked the mouse in the Unity editor. Initially, I thought that mouse clicks might not be reflected in the web version, but after debugging, I found that mouse clicks were working properly. Therefore, I tried increasing the size of the 'directionToMouse' vector. I discovered that the ball's direction changed correctly when I adjusted the vector, even though the direction change was gradual in the Unity environment. I believe the Unity environment and the web environment behave differently, and I think the vector is being mimicked or scaled differently in the web environment.

<img width="957" alt="Screenshot 2025-02-15 at 4 07 48 PM" src="https://github.com/user-attachments/assets/751a7295-034a-4124-aa98-3868433241f7" />



# Assignment-final: [Break-out game](https://erigolee.github.io/game-dev-spring2025/builds/breakout-final/)
I found that some users prefer to skip the level-1 game and go straight to the level-2 game. As a result, I decided to separate level 1 and level 2. Before playing, I implemented a feature that allows users to select either the level-1 game or the level-2 game. Additionally, I created a level-3 game, even though its basic structure is similar to level-2. However, in level 3, I implemented a feature where the direction of the ball changes when users press the 'W,' 'A,' 'S,' and 'D' keys.

<img width="960" alt="Screenshot 2025-02-15 at 4 08 23 PM" src="https://github.com/user-attachments/assets/81641aed-5518-419d-95eb-8f98e3961edb" />


