# GameLifeCSharpConsole
Game Life console implementation

Rules: https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life


## Start

- Become contributor
- Create new branch feature/logic from master
- Clone feature/logic branch
- Look through application architecture
- Compile and run application


## field.cs

- Validate input in init function
- Populate neighbors array with actual values in next_generation function
- Render field border in draw function (use '╔', '╗', '╚', '╝', '║', '═' chars)


## cell.cs

- Implement game logic following rules in predict_generation function (remove mock)


## Tests

- Try different inputs https://en.wikipedia.org/wiki/Conway's_Game_of_Life#Examples_of_patterns
- Test border effects (width + 1 => 0, heigth + 1 => 0)
- Implement unit tests


## Finally

- Push changes and create merge request to master
