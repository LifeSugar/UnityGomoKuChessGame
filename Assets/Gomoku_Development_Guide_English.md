
# Gomoku Game

Create By Xi Lin 2024/10/30

## Table of Contents

1. [Overview](#overview)
2. [List of Scripts](#list-of-scripts)
   - [GameManager](#gamemanager)
   - [Board](#board)
   - [OpponentUIManager](#opponentuimanager)
   - [GameSettings](#gamesettings)
3. [Script Details](#script-details)
   - [GameManager Script](#gamemanager-script)
   - [Board Script](#board-script)
   - [OpponentUIManager Script](#opponentuimanager-script)
   - [GameSettings Script](#gamesettings-script)
4. [Additional Notes](#additional-notes)
5. [Conclusion](#conclusion)

## Overview

The Gomoku game consists of multiple scripts, each responsible for different functional modules, including game logic, board generation, UI management, and game settings. The main scripts are:

- **GameManager**: Core game logic, including player input handling, AI algorithm implementation, and win condition checks.
- **Board**: Responsible for generating the board grid and initializing the game interface.
- **OpponentUIManager**: Manages the opponent selection interface, handling player versus AI mode selection.
- **GameSettings**: Used to pass game settings between scenes, such as game mode and AI difficulty.

## List of Scripts

### GameManager

- **Location**: `Assets/Scripts/GameManager.cs`
- **Function**: Manages the main game logic, including:
  - Handling player and AI moves.
  - Implementing AI algorithms (Easy, Medium, Hard).
  - Checking win conditions and game over states.
  - Managing and updating board states.

### Board

- **Location**: `Assets/Scripts/Board.cs`
- **Function**: Generates the game board grid, including:
  - Creating a 15x15 grid.
  - Initializing visual elements of the board.

### OpponentUIManager

- **Location**: `Assets/Scripts/OpponentUIManager.cs`
- **Function**: Manages the opponent selection interface, including:
  - Providing a UI for players to choose the game mode.
  - Handling player versus player (PvP) and player versus AI (PvE) mode selection.
  - Choosing AI difficulty (Easy, Medium, Hard) in PvE mode.

### GameSettings

- **Location**: `Assets/Scripts/GameSettings.cs`
- **Function**: Passes game settings between scenes, including:
  - Storing the game mode (whether playing against AI).
  - Storing AI difficulty settings.

## Script Details

### GameManager Script

**Main Functions**:

- **Player Input Handling**: Listens for player's mouse click events to get the coordinates where the player wants to place a piece.
- **Piece Placement**: Generates a piece (black or white) at the clicked position and updates the board state array.
- **AI Algorithm Implementation**:
  - **Easy Difficulty**: AI randomly selects an empty position to place a piece.
  - **Medium Difficulty**: Uses a scoring function to evaluate each empty position and chooses the one with the highest score.
  - **Hard Difficulty**: Uses the Minimax algorithm with alpha-beta pruning to predict multiple moves ahead and choose the best position.
- **Win Condition Checking**: After each move, checks whether the current player has formed five pieces in a row to determine the winner.
- **Board State Management**: Uses a 2D array `boardState[15,15]` to store the board state for performance improvement.

**Key Variables**:

- `currentPlayer`: The current player (Black or White).
- `boardState`: The board state array; 0 represents empty, 1 represents black, 2 represents white.
- `isAIGame`: Boolean indicating whether the current game is against AI.
- `aiDifficulty`: String indicating AI difficulty level (Easy, Medium, Hard).

**Key Functions**:

- `void Start()`: Initializes game state and retrieves game settings.
- `void Update()`: Main game loop handling player and AI move logic.
- `void PlacePiece()`: Handles player move actions.
- `IEnumerator AITurn()`: Handles AI move actions.
- `Vector2 GetAIMove()`: Calls the appropriate algorithm based on AI difficulty to get AI's move position.
- `bool CheckWin(int[,] board, int x, int y, int playerValue)`: Checks if the specified player has won.
- `int Minimax(int[,] board, int depth, int alpha, int beta, bool maximizingPlayer)`: Implementation of the Minimax algorithm for Hard difficulty.

### Board Script

**Main Functions**:

- **Board Generation**: Generates a 15x15 grid at the start of the game as the visual representation of the board.
- **Grid Prefab**: Uses the `gridPrefab` to instantiate grid intersections.

**Key Variables**:

- `public int rows = 15`: Number of rows on the board.
- `public int columns = 15`: Number of columns on the board.
- `public GameObject gridPrefab`: Prefab used to generate board grid cells.

**Key Functions**:

- `void Start()`: Initializes the board grid.
- `void CreateGrid()`: Creates the grid layout of the board.

### OpponentUIManager Script

**Main Functions**:

- **Game Mode Selection**: Provides options for PvP and PvE modes.
- **AI Difficulty Selection**: Allows players to choose AI difficulty level in PvE mode.
- **Scene Management**: Loads the corresponding game scene based on the player's selection.

**Key Variables**:

- `public GameObject aiDifficultyPanel`: AI difficulty selection panel.
- `public Button pvpButton`: PvP mode button.
- `public Button pveButton`: PvE mode button.
- `public Button easyButton`, `mediumButton`, `hardButton`: AI difficulty buttons.
- `public Button backButton`: Back button.

**Key Functions**:

- `void Start()`: Initializes button event listeners.
- `void OnPvPButtonClicked()`: Handles logic when the player selects PvP mode.
- `void OnPvEButtonClicked()`: Displays the AI difficulty selection panel.
- `void OnDifficultySelected(string difficulty)`: Handles logic when the player selects an AI difficulty.
- `void OnBackButtonClicked()`: Hides the AI difficulty selection panel.

### GameSettings Script

**Main Functions**:

- **Game Settings Storage**: Passes game mode and AI difficulty between scenes.
- **Cross-Scene Data Sharing**: Ensures the settings selected in the main menu are accessible in the game scene.

**Key Variables**:

- `public static bool isAIGame = false`: Indicates whether the game is in PvE mode.
- `public static string aiDifficulty = "Easy"`: Stores the AI difficulty level.

## Additional Notes

- **Advantages of Board State Array**: Using the `boardState` 2D array to store the board state avoids frequent calls to methods like `Physics2D.OverlapPoint()`, improving performance, especially when AI algorithms require frequent access to the board state.
- **AI Algorithm Optimization**:
  - **Medium Difficulty**: Considers only empty positions adjacent to existing pieces, reducing the number of evaluations.
  - **Hard Difficulty**: Limits the search depth of the Minimax algorithm and uses alpha-beta pruning to balance AI intelligence and computational performance.
- **Use of Coroutines**: The `AITurn()` function uses coroutines to implement the AI's thinking process and avoid performing time-consuming calculations on the main thread, preventing game freezes.

## Conclusion

Through the collaboration of the scripts above, we have implemented a fully functional Gomoku game. Players can choose to play against another player or challenge AI opponents of varying difficulty levels.

If you have any questions or need further assistance, please refer to the comments in the scripts.





# 五子棋游戏

Unity小游戏学习资料

## 目录

1. [概述](#概述)
2. [脚本列表](#脚本列表)
    - [GameManager](#gamemanager)
    - [Board](#board)
    - [OpponentUIManager](#opponentuimanager)
    - [GameSettings](#gamesettings)
3. [脚本详细说明](#脚本详细说明)
    - [GameManager 脚本](#gamemanager-脚本)
    - [Board 脚本](#board-脚本)
    - [OpponentUIManager 脚本](#opponentuimanager-脚本)
    - [GameSettings 脚本](#gamesettings-脚本)
4. [附加说明](#附加说明)
5. [结语](#结语)

## 概述

该五子棋游戏由多个脚本组成，每个脚本负责不同的功能模块，包括游戏逻辑、棋盘生成、UI管理以及游戏设置。主要的脚本如下：

- **GameManager**：核心游戏逻辑，包括玩家输入处理、AI算法实现、胜利条件检查等。
- **Board**：负责生成棋盘网格，初始化游戏界面。
- **OpponentUIManager**：管理对手选择界面，处理玩家与AI对战的模式选择。
- **GameSettings**：用于在场景之间传递游戏设置，如游戏模式和AI难度。

## 脚本列表

### GameManager

- **位置**：`Assets/Scripts/GameManager.cs`
- **功能**：管理游戏的主要逻辑，包括：
    - 处理玩家和AI的下棋操作。
    - 实现AI算法（简单、中等、困难）。
    - 检查胜利条件和游戏结束状态。
    - 管理棋盘状态和更新。

### Board

- **位置**：`Assets/Scripts/Board.cs`
- **功能**：生成游戏的棋盘网格，包括：
    - 创建15x15的棋盘格子。
    - 初始化棋盘的视觉元素。

### OpponentUIManager

- **位置**：`Assets/Scripts/OpponentUIManager.cs`
- **功能**：管理对手选择界面，包括：
    - 提供玩家选择对战模式的UI界面。
    - 处理玩家对战（PvP）和玩家对AI（PvE）的模式选择。
    - 在PvE模式下，选择AI的难度（简单、中等、困难）。

### GameSettings

- **位置**：`Assets/Scripts/GameSettings.cs`
- **功能**：在场景之间传递游戏设置，包括：
    - 存储游戏模式（是否与AI对战）。
    - 存储AI的难度设置。

## 脚本详细说明

### GameManager 脚本

**主要功能**：

- **玩家输入处理**：监听玩家的鼠标点击事件，获取玩家希望下子的坐标。
- **棋子放置**：在玩家点击的位置生成棋子（黑子或白子），并更新棋盘状态数组。
- **AI算法实现**：
    - **简单难度（Easy）**：AI随机选择空白位置下子。
    - **中等难度（Medium）**：使用评分函数评估每个空位，选择得分最高的位置下子。
    - **困难难度（Hard）**：使用Minimax算法结合α-β剪枝，预判多步，选择最佳下子位置。
- **胜利条件检查**：在每次下子后，检查当前玩家是否形成五连子，从而判定胜负。
- **棋盘状态管理**：使用二维数组`boardState[15,15]`存储棋盘状态，提高性能。

**关键变量**：

- `currentPlayer`：当前轮到的玩家（黑方或白方）。
- `boardState`：棋盘状态数组，0表示空位，1表示黑子，2表示白子。
- `isAIGame`：布尔值，指示当前游戏是否为玩家对AI。
- `aiDifficulty`：字符串，表示AI的难度级别（Easy、Medium、Hard）。

**关键函数**：

- `void Start()`：初始化游戏状态，获取游戏设置。
- `void Update()`：游戏主循环，处理玩家和AI的下棋逻辑。
- `void PlacePiece()`：处理玩家的下棋操作。
- `IEnumerator AITurn()`：处理AI的下棋操作。
- `Vector2 GetAIMove()`：根据AI难度，调用相应的算法获取AI的下子位置。
- `bool CheckWin(int[,] board, int x, int y, int playerValue)`：检查指定玩家是否胜利。
- `int Minimax(int[,] board, int depth, int alpha, int beta, bool maximizingPlayer)`：困难难度下的Minimax算法实现。

### Board 脚本

**主要功能**：

- **棋盘生成**：在游戏开始时，生成15x15的棋盘格子，作为棋盘的视觉表示。
- **网格预制件**：使用`gridPrefab`预制件，实例化网格交叉点。

**关键变量**：

- `public int rows = 15`：棋盘的行数。
- `public int columns = 15`：棋盘的列数。
- `public GameObject gridPrefab`：用于生成棋盘格子的预制件。

**关键函数**：

- `void Start()`：初始化棋盘网格。
- `void CreateGrid()`：创建棋盘的网格布局。

### OpponentUIManager 脚本

**主要功能**：

- **对战模式选择**：提供玩家对战（PvP）和玩家对AI（PvE）的模式选择。
- **AI难度选择**：允许玩家选择AI难度级别。
- **场景管理**：根据玩家的选择，加载相应的游戏场景。

**关键变量**：

- `public GameObject aiDifficultyPanel`：AI难度选择面板。
- `public Button pvpButton`：玩家对战按钮。
- `public Button pveButton`：玩家对AI按钮。
- `public Button easyButton`、`mediumButton`、`hardButton`：AI难度按钮。
- `public Button backButton`：返回按钮。

**关键函数**：

- `void Start()`：初始化按钮的事件监听。
- `void OnPvPButtonClicked()`：处理玩家选择玩家对战模式的逻辑。
- `void OnPvEButtonClicked()`：显示AI难度选择面板。
- `void OnDifficultySelected(string difficulty)`：处理玩家选择AI难度的逻辑。
- `void OnBackButtonClicked()`：隐藏AI难度选择面板。

### GameSettings 脚本

**主要功能**：

- **游戏设置存储**：在场景之间传递游戏模式和AI难度。
- **跨场景数据共享**：确保在主菜单选择的设置能在游戏场景中被读取。

**关键变量**：

- `public static bool isAIGame = false`：指示是否为玩家对AI的模式。
- `public static string aiDifficulty = "Easy"`：存储AI的难度级别。

## 附加说明

- **棋盘状态数组的优势**：使用`boardState`二维数组存储棋盘状态，避免了频繁调用`Physics2D.OverlapPoint()`等物理检测方法，提高了性能，特别是在AI算法中需要大量访问棋盘状态的情况下。
- **AI算法的优化**：
    - **中等难度**：仅考虑已有棋子周围的空位，减少评分计算的次数。
    - **困难难度**：限制Minimax算法的搜索深度，使用α-β剪枝优化，平衡AI智能和计算性能。
- **协程的使用**：`AITurn()`函数使用协程，实现了AI的思考过程，并避免了在主线程中进行耗时的计算，防止游戏卡顿。

## 结语

通过以上脚本的协同工作，实现了一个功能完善的五子棋游戏。玩家可以选择与另一位玩家对战，或是挑战不同难度的AI对手。

如有任何疑问或需要进一步的帮助，请参考脚本中的注释。
