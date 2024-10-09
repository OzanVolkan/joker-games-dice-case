# Joker Games Dice Case
 A dice game case study that combines elements of inventory management, dynamic map generation and interactive dice mechanics.

## Features

### Inventory System

● Players can collect three types of items: apples, pears, and strawberries.

● To save (when the game is closed) and load (when the game is opened) the amounts of these items, data is used in **JSON** format.

● These item amounts can be seen in the top right corner UI when the game starts and are updated during gameplay.

### 3D Map Generation

● The map is generated in the shape of a **line** using **JSON** data, and for each tile on the map, the **amount of collectables** and the **tile number** are specified.

### Dice Mechanics

● The player can **roll between 1 and 20 dice** at the same time. **Input fields** and **dropdown** UIs have been added to the top left corner.

● The **rolling of the dice** is determined at runtime using **pre-created animations**. When a dice is rolled, a random animation from three options is played based on the value received from the input.

● The **player's movement** is created by combining **scale animations** with **parabolic movement code**.

● There are two types of characters with different movement styles that we can play as.

● **If the player does not specify a dice value, a random value** will be chosen, and **if the number of dice is not specified, 1 dice** will be rolled by default.

● The player **moves** across the map by the **total value of the dice** roll.

● If the player **reaches the end** of the map, they will **teleport back to the first tile** and continue moving if there are still remaining steps to take.

### Creatives

● To enhance the visual and auditory experience, **particle effects** and **sound effects** were used during events like **dice** contact with the ground, player **movement**, and collectable **collection**.

### Additional Notes

● Developed using Unity version 2022.3.16.

● Developed the project with the **Android target platform** selected, optimized for **portrait screen mode**, and **tested** it on a mobile Android device.
