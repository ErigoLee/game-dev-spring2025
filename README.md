# Game Development I
These are the projects I created during the Game Development I graduate course.


## Introduction of Projects
### (1) Breakout game
<img src="https://github.com/user-attachments/assets/933c9d59-90ca-48d0-a374-f9e30923641b" alt="image" width="600" height="372"/> </br>
1. Description
This project is a custom game based on the classic **Breakout** concept.  
Players control a paddle using the arrow keys to bounce a ball and break walls.

2. Gameplay Rules
- Players have **5 lives**. If the ball falls 5 times, the game is over.
- The game ends successfully once all walls are destroyed.

3. Levels
- The game consists of **3 levels**.
- Players can select and start from their desired level.
- Level 3 (Extension of Level 2)</br>
Level 3 reuses the basic structure of Level 2, but adds a control feature: pressing **W / A / S / D** changes the ball’s movement direction.

4. User Behavior Insight
- Some players preferred skipping Level 1 and jumping straight to Level 2.
- Based on this, Level 1 and Level 2 were separated and exposed via a selection screen.

5. Input Handling Fix (Debounce)
- Arrow keys were being registered as continuous inputs on the selection screen, which sometimes prevented choosing the intended level.
- A debounce interval (timeBetweenInputs) was introduced to prevent rapid repeats and ensure accurate level selection.

6. Code
(1) GameManager.cs
The following function is part of the **GameManager** code.  </br>
It implements **page navigation** using the Up and Down arrow keys.  </br>
To prevent continuous key inputs from being registered too quickly, an input delay is applied with the variable `timeBetweenInputs`. </br>
```csharp
void pageUpDown() {
    if (checkingTime == false) {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            page--;
            if (page > page_max)
                page = page_min;
            checkingTime = true;
            lastInputTime = 0.0f;  // Reset the timer whenever an input occurs
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            page++;
            if (page < page_min)
                page = page_max;
            checkingTime = true;
            lastInputTime = 0.0f;  // Reset the timer whenever an input occurs
        }
    }
    else {
        if (lastInputTime >= timeBetweenInputs) {
            checkingTime = false;  // Allow input again after the delay time has passed
        }
        else {
            lastInputTime += Time.deltaTime;  // Increase elapsed time every frame
        }
    }
}
```
(2) BallScript.cs
When the ball collides with a brick, its velocity changes based on the brick’s color.

- **Red** → `(-10, 20, 0)`
- **Orange** → `(9, -18, 0)`
- **Yellow** → `(-8, 16, 0)`
- **Green** → `(7, -14, 0)`
- **Blue (default)** → `(-6, 12, 0)`
```csharp
void OnCollisionEnter(Collision collision)
{
    // Only react to bricks
    if (collision.gameObject.CompareTag("brick"))
    {
        // Read the brick's color
        Color color = collision.gameObject
                               .GetComponent<Renderer>()
                               .material.color;

        // Change ball velocity based on brick color
        if (color == red)
        {
            rb.linearVelocity = new Vector3(-10, 20, 0);
            print("red");
        }
        else if (color == orange)
        {
            rb.linearVelocity = new Vector3(9, -18, 0);
            print("orange");
        }
        else if (color == yellow)
        {
            rb.linearVelocity = new Vector3(-8, 16, 0);
            print("yellow");
        }
        else if (color == green)
        {
            rb.linearVelocity = new Vector3(7, -14, 0);
            print("green");
        }
        else
        {
            rb.linearVelocity = new Vector3(-6, 12, 0);
            print("blue");
        }
    }
}
```

7. Play Link


### (2) Simulator game
<img src="https://github.com/user-attachments/assets/5f6e90cd-896c-4a3a-be74-5da2d0637e4b" alt="game screenshot" width="600" height="372"/>
1. Description
**Grid and Cell System**</br>
- When the game starts, the `GridManager4.cs` script generates a grid of **20 × 20 cells** (total **400 cells**) without overlap.  
- Each cell is randomly assigned one of the following attributes: **Water**, **Fire**, **Grass**, or **Road**.  
- After the grid is created, clicking on any cell with the mouse displays the cell’s information in the corresponding panel UI image.

2. Code
(1) GridManager4.cs
- The nested loops place each cell at `(x * cellSpacing, 0, y * cellSpacing)`, creating a uniform **gridSize × gridSize** layout with consistent spacing in both X (columns) and Z (rows) directions.
- On instantiation, each cell receives one of four materials — **Water**, **Fire**, **Grass**, **Road** — chosen **uniformly at random (25% each)** so that every playthrough yields a different grid composition.
- The cell’s **type** is determined by its material (e.g., Water, Fire, Grass, Road).
```csharp
for (int x = 0; x < gridSize; x++)
{
    for (int y = 0; y < gridSize; y++)
    {
        Vector3 position = new Vector3(x * cellSpacing, 0, y * cellSpacing);
        GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, transform);

        Cell cellScript = cell.AddComponent<Cell>();
        cellScript.SetTypeMaterials(waterMaterial, fireMaterial, grassMaterial, roadMaterial);

        // Apply either water or fire material randomly (for testing purposes)
        Material chosenMat = Random.value > 0.75f ? waterMaterial : Random.value > 0.5f ? fireMaterial :  Random.value > 0.25f ? grassMaterial : roadMaterial;
        cellScript.SetMaterial(chosenMat);

        cell.name = "Cell_" + x + "_" + y;
        cells[x, y] = cellScript;
    }
}
```
- When the player clicks with the mouse, a **raycast** is used to detect which cell was clicked.
- When the player clicks on a cell, the hit point is converted into grid coordinates (x, y).
- If the coordinates are valid, the corresponding **Cell** object is retrieved.
- The **panel text** is updated **only when a cell is clicked**, showing contextual information about that cell.
Example: - Clicking a **Water** cell displays:</br>
**“A calm and stable environment. Reduces fire risk and slows unit movement.”**

```csharp
if (Physics.Raycast(ray, out hit))
{
    Vector3 hitPoint = hit.point;
    Vector2Int gridCoord = new Vector2Int(Mathf.RoundToInt(hitPoint.x / cellSpacing), Mathf.RoundToInt(hitPoint.z / cellSpacing));

    if (gridCoord.x >= 0 && gridCoord.x < gridSize && gridCoord.y >= 0 && gridCoord.y < gridSize)
    {
        Cell clickedCell = cells[gridCoord.x, gridCoord.y];

        string type = clickedCell.GetCellTypeByMaterial();
        Debug.Log($"Clicked Cell Type: {type}");
        string explainStr = "";
        if(type == "Water")
        {
            explainStr = "A calm and stable environment. \nReduces fire risk and slows unit movement.";
        }
    }
}
```
### (3) People-1 game

### (4) platformer game

## License
- All **source code** in this repository is licensed under the [MIT License](./LICENSE).
- Some codes are adapted from **Game Development I course materials** (Graduate coursework).
  Details are listed below.
- In addition, certain parts of the code were not directly taken from ChatGPT but were instead implemented independently with reference to its output.
- Third-party **assets** (models, textures, sounds, fonts, etc.) remain under their original licenses.
  They may **not be licensed for redistribution or in-game use** in this repository.  
  Please check the original source pages for specific license terms.

### Images
Most of the images in the Pictures folder under each Prototype directory were generated by ChatGPT.</br>
However, the player and NPC images in the platformer prototype were generated by ChatGPT based on existing asset images.</br>
Please make sure to review the original asset licenses before using them.</br>

### Fonts
- `LegacyRuntime`
This project uses Unity’s built-in runtime font (`LegacyRuntime.ttf`).
As of Unity 2022.2 and newer, the default built-in `Arial.ttf` reference was renamed to `LegacyRuntime.ttf`.
Please refer to the Unity license terms for usage.

### Asset Reference
> Note: Third-party assets remain under their original licenses.
> They may not be licensed for redistribution or in-game use in this repository.
> Please review each source page for license terms before redistribution or in-game use.

### (1) Breakout game assets
- Fantasy Skybox Free
  - Source: https://assetstore.unity.com/packages/2d/textures-materials/sky/fantasy-skybox-free-18353
- Hit The Deck
  - Source: https://assetstore.unity.com/packages/audio/sound-fx/hit-the-deck-16100
- Sherbbs Particle Collection
  - Source: https://assetstore.unity.com/packages/vfx/particles/sherbb-s-particle-collection-170798

### (2) People-1
- simplePoly City – Low Poly Assets
  - Source: https://assetstore.unity.com/packages/3d/environments/simplepoly-city-low-poly-assets-58899
- City People FREE Samples
  - Folder Name: DenysAlmaral / CityPeople-Free
  - Source: https://assetstore.unity.com/packages/3d/characters/city-people-free-samples-260446?srsltid=AfmBOoq6Bm6wi3Wpo-DOlHjaNx-iG_nuZIuSHKLshNgREvGVxzovgtOZ

### (3) Platformer-1
- RPG Monster DUO PBR Polyart
  - Source: https://assetstore.unity.com/packages/3d/characters/creatures/rpg-monster-duo-pbr-polyart-157762
- 25 Rpg Game Tracks
  - Source: https://assetstore.unity.com/packages/audio/music/25-fantasy-rpg-game-tracks-music-pack-240154
- Animated Platformer Character
  - Prefab name: NPC
  - Source: https://poly.pizza/m/kKtL4zvS3n
- Coin Texture
  - Source: https://www.texturecan.com/details/255/
- Character Animated
  - Prefab name: Character
  - Source: https://poly.pizza/m/DgOCW9ZCRJ
- Old key
  - Prefab name: uploads_files_5202732_Rustic+Key
  - Source: https://sketchfab.com/3d-models/old-key-69e71d81492548de88edb7f81065d80c
- Wall Prefab
  — Source currently unknown.  
  - Please check the original license before using or redistributing this asset.

