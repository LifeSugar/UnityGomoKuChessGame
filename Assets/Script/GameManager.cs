using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public enum Player { Black, White }
    public Player currentPlayer = Player.Black;

    public GameObject blackPiecePrefab;
    public GameObject whitePiecePrefab;
    public AudioSource luoZi;

    private int[,] boardState = new int[15, 15]; // 0: 空，1: 黑子，2: 白子

    private bool isAIGame;
    private string aiDifficulty;
    const int HARD_AI_DEPTH = 2;

    private bool isAITurning = false;
    
    private bool aiWin = false;

    
    int[,] scoreBoard = new int[15, 15];

    public GameObject UI;
    public GameObject buttonAI;
    public GameObject buttonPlayer;

    void Start()
    {
        // 初始化棋盘状态
        for (int x = 0; x < 15; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                boardState[x, y] = 0;
            }
        }

        isAIGame = GameSettings.isAIGame;
        aiDifficulty = GameSettings.aiDifficulty;

    }

    void Update()
    {
        bool isOver = IsGameOver(boardState);
        if (isOver)
        {
            if (aiWin)
            {
                UI.SetActive(true);
                buttonAI.SetActive(true);
            }
            else
            {
                UI.SetActive(true);
                buttonAI.SetActive(true);
            }
            foreach (Transform child in transform)
            {
                Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    Debug.Log($"Changed {child.name}'s Rigidbody2D bodyType to Dynamic.");
                }
                else
                {
                    Debug.LogWarning($"No Rigidbody2D found on {child.name}.");
                }
            }
        }
        else
        {
            if (currentPlayer == Player.Black || (!isAIGame))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    PlacePiece();
                }
            }
            else if (isAIGame && currentPlayer == Player.White)
            {
                if (!isAITurning)
                {
                    StartCoroutine(AITurn());
                }
            }
        }
    }

    void PlacePiece()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = Mathf.RoundToInt(mousePosition.x);
        int y = Mathf.RoundToInt(mousePosition.y);

        // 检查位置是否有效
        if (x < 0 || x >= 15 || y < 0 || y >= 15)
            return;

        // 检查该位置是否已有棋子
        if (boardState[x, y] != 0)
            return;

        // 放置棋子
        GameObject piecePrefab = currentPlayer == Player.Black ? blackPiecePrefab : whitePiecePrefab;
        GameObject newP = Instantiate(piecePrefab, new Vector2(x, y), Quaternion.identity);
        newP.transform.SetParent(transform);
        luoZi.Play();

        // 更新棋盘状态
        boardState[x, y] = currentPlayer == Player.Black ? 1 : 2;

        // 检查胜利条件
        if (CheckWin(x, y))
        {
            Debug.Log($"{currentPlayer} wins!");
            
        }

        // 切换玩家
        currentPlayer = currentPlayer == Player.Black ? Player.White : Player.Black;
    }

    bool CheckWin(int x, int y)
    {
        // 检查四个方向
        if (CheckDirection(x, y, 1, 0) ||    // 横向
            CheckDirection(x, y, 0, 1) ||    // 纵向
            CheckDirection(x, y, 1, 1) ||    // 正斜线
            CheckDirection(x, y, 1, -1))     // 反斜线
        {
            return true;
        }
        return false;
    }

    bool CheckDirection(int x, int y, int dx, int dy)
    {
        int count = 1;
        count += CountPieces(x, y, dx, dy);
        count += CountPieces(x, y, -dx, -dy);
        return count >= 5;
    }

    int CountPieces(int x, int y, int dx, int dy)
    {
        int count = 0;
        int playerValue = boardState[x, y];

        int tx = x + dx;
        int ty = y + dy;

        while (tx >= 0 && tx < 15 && ty >= 0 && ty < 15 && boardState[tx, ty] == playerValue)
        {
            count++;
            tx += dx;
            ty += dy;
        }

        return count;
    }

    IEnumerator AITurn()
    {
        isAITurning = true;

        
        yield return new WaitForSeconds(0.5f); // 模拟AI思考时间

        Vector2 aiMove = GetAIMove();

        int x = (int)aiMove.x;
        int y = (int)aiMove.y;

        // 放置AI的棋子
        GameObject newP = Instantiate(whitePiecePrefab, new Vector2(x, y), Quaternion.identity);
        newP.transform.SetParent(transform);
        luoZi.Play();
        

        // 更新棋盘状态
        boardState[x, y] = 2; // AI为白子

        // 检查胜利条件
        if (CheckWin(x, y))
        {
            Debug.Log("AI wins!");
            aiWin = true;
            
        }

        // 切换玩家
        currentPlayer = Player.Black;

        isAITurning = false;
    }

    Vector2 GetAIMove()
    {
        if (aiDifficulty == "Easy")
        {
            return GetRandomMove();
        }
        else if (aiDifficulty == "Medium")
        {
            return GetMediumMove();
        }
        else if (aiDifficulty == "Hard")
        {
            return GetHardMove();
        }
        else
        {
            return GetRandomMove();
        }
    }

    Vector2 GetRandomMove()
    {
        int x, y;
        do
        {
            x = Random.Range(0, 15);
            y = Random.Range(0, 15);
        } while (boardState[x, y] != 0);

        return new Vector2(x, y);
    }

    Vector2 GetMediumMove()
    {
        CalculateScore();
        int maxScore = 0;
        List<Vector2> maxPoints = new List<Vector2>();

        for (int x = 0; x < 15; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                if (boardState[x, y] != 0)
                    continue;

                if (scoreBoard[x, y] > maxScore)
                {
                    maxScore = scoreBoard[x, y];
                    maxPoints.Clear();
                    maxPoints.Add(new Vector2(x, y));
                }
                else if (scoreBoard[x, y] == maxScore)
                {
                    maxPoints.Add(new Vector2(x, y));
                }
            }
        }

        if (maxPoints.Count == 0)
        {
            Debug.LogWarning("未找到得分最高的位置，使用随机位置。");
            return GetRandomMove();
        }

        // 从得分最高的位置中随机选择一个
        int index = Random.Range(0, maxPoints.Count);
        return maxPoints[index];
    }

    void CalculateScore()
    {
        // 清空评分板
        for (int x = 0; x < 15; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                scoreBoard[x, y] = 0;
            }
        }

        // 获取所有需要计算的位置（已有棋子周围）
        List<Vector2> positions = GetNeighboringEmptyPositions();

        foreach (Vector2 pos in positions)
        {
            int x = (int)pos.x;
            int y = (int)pos.y;

            // 计算进攻分数
            int attackScore = GetScore(x, y, Player.White);
            // 计算防守分数
            int defenseScore = GetScore(x, y, Player.Black);
            // 总得分
            scoreBoard[x, y] = attackScore + defenseScore;
        }
    }

    List<Vector2> GetNeighboringEmptyPositions()
    {
        HashSet<Vector2> positions = new HashSet<Vector2>();

        for (int x = 0; x < 15; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                if (boardState[x, y] != 0)
                {
                    // 添加周围的空位
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            int nx = x + dx;
                            int ny = y + dy;
                            if (nx >= 0 && nx < 15 && ny >= 0 && ny < 15)
                            {
                                if (boardState[nx, ny] == 0)
                                {
                                    positions.Add(new Vector2(nx, ny));
                                }
                            }
                        }
                    }
                }
            }
        }

        // 如果棋盘上还没有任何棋子，就返回中心位置
        if (positions.Count == 0)
        {
            positions.Add(new Vector2(7, 7));
        }

        return new List<Vector2>(positions);
    }

    int GetScore(int x, int y, Player player)
    {
        int totalScore = 0;

        totalScore += CountScoreInDirection(x, y, 1, 0, player);  // 横向
        totalScore += CountScoreInDirection(x, y, 0, 1, player);  // 纵向
        totalScore += CountScoreInDirection(x, y, 1, 1, player);  // 正斜线
        totalScore += CountScoreInDirection(x, y, 1, -1, player); // 反斜线

        return totalScore;
    }

    int CountScoreInDirection(int x, int y, int dx, int dy, Player player)
    {
        int count = 0;
        int block = 0;

        int playerValue = player == Player.Black ? 1 : 2;
        int opponentValue = player == Player.Black ? 2 : 1;

        // 向正方向
        for (int i = 1; i <= 4; i++)
        {
            int tx = x + i * dx;
            int ty = y + i * dy;
            if (tx < 0 || tx >= 15 || ty < 0 || ty >= 15)
            {
                block++;
                break;
            }
            if (boardState[tx, ty] == opponentValue)
            {
                block++;
                break;
            }
            else if (boardState[tx, ty] == playerValue)
            {
                count++;
            }
            else // 空位
            {
                break;
            }
        }

        // 向反方向
        for (int i = 1; i <= 4; i++)
        {
            int tx = x - i * dx;
            int ty = y - i * dy;
            if (tx < 0 || tx >= 15 || ty < 0 || ty >= 15)
            {
                block++;
                break;
            }
            if (boardState[tx, ty] == opponentValue)
            {
                block++;
                break;
            }
            else if (boardState[tx, ty] == playerValue)
            {
                count++;
            }
            else // 空位
            {
                break;
            }
        }

        return EvaluateScore(count, block);
    }

    int EvaluateScore(int count, int block)
    {
        if (block >= 2)
            return 0;

        if (count >= 4)
        {
            if (block == 0)
                return 100000; // 活四或连五，胜利在望
            else if (block == 1)
                return 10000;  // 冲四
        }
        else if (count == 3)
        {
            if (block == 0)
                return 1000;  // 活三
            else if (block == 1)
                return 100;   // 眠三
        }
        else if (count == 2)
        {
            if (block == 0)
                return 100;   // 活二
            else if (block == 1)
                return 10;    // 眠二
        }
        else if (count == 1)
        {
            return 10;        // 一子
        }
        return 0;
    }

    Vector2 GetHardMove()
    {
        int bestScore = int.MinValue;
        Vector2 bestMove = Vector2.zero;

        List<Vector2> moves = GetPossibleMoves(boardState);

        foreach (Vector2 move in moves)
        {
            int x = (int)move.x;
            int y = (int)move.y;

            boardState[x, y] = 2; // AI下子

            int score = Minimax(boardState, HARD_AI_DEPTH, int.MinValue, int.MaxValue, false); // 设定搜索深度为3，可根据需要调整

            boardState[x, y] = 0; // 撤销下子

            if (score > bestScore)
            {
                bestScore = score;
                bestMove = move;
            }
        }

        return bestMove;
    }

    int Minimax(int[,] board, int depth, int alpha, int beta, bool maximizingPlayer)
    {
        if (depth == 0 || IsGameOver(board))
        {
            return EvaluateBoard(board, maximizingPlayer ? 2 : 1);
        }

        List<Vector2> moves = GetPossibleMoves(board);

        if (maximizingPlayer)
        {
            int maxEval = int.MinValue;
            foreach (Vector2 move in moves)
            {
                int x = (int)move.x;
                int y = (int)move.y;
                board[x, y] = 2; // AI下子

                int eval = Minimax(board, depth - 1, alpha, beta, false);

                board[x, y] = 0; // 撤销下子
                maxEval = Mathf.Max(maxEval, eval);
                alpha = Mathf.Max(alpha, eval);
                if (beta <= alpha)
                    break; // β剪枝
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            foreach (Vector2 move in moves)
            {
                int x = (int)move.x;
                int y = (int)move.y;
                board[x, y] = 1; // 玩家下子

                int eval = Minimax(board, depth - 1, alpha, beta, true);

                board[x, y] = 0; // 撤销下子
                minEval = Mathf.Min(minEval, eval);
                beta = Mathf.Min(beta, eval);
                if (beta <= alpha)
                    break; // α剪枝
            }
            return minEval;
        }
    }

    List<Vector2> GetPossibleMoves(int[,] board)
    {
        List<Vector2> moves = new List<Vector2>();

        for (int x = 0; x < 15; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                if (board[x, y] == 0)
                {
                    if (HasNeighbor(board, x, y))
                    {
                        moves.Add(new Vector2(x, y));
                    }
                }
            }
        }

        // 如果棋盘为空，返回中心点
        if (moves.Count == 0)
        {
            moves.Add(new Vector2(7, 7));
        }

        return moves;
    }

    bool HasNeighbor(int[,] board, int x, int y)
    {
        for (int dx = -2; dx <= 2; dx++)
        {
            for (int dy = -2; dy <= 2; dy++)
            {
                if (dx == 0 && dy == 0)
                    continue;
                int nx = x + dx;
                int ny = y + dy;
                if (nx >= 0 && nx < 15 && ny >= 0 && ny < 15)
                {
                    if (board[nx, ny] != 0)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    int EvaluateBoard(int[,] board, int player)
    {
        int totalScore = 0;

        // 对AI和玩家分别进行评分
        totalScore += EvaluateScoreForPlayer(board, 2); // AI为2
        totalScore -= EvaluateScoreForPlayer(board, 1); // 玩家为1

        return totalScore;
    }

    int EvaluateScoreForPlayer(int[,] board, int playerValue)
    {
        int score = 0;

        for (int x = 0; x < 15; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                if (board[x, y] == playerValue)
                {
                    score += EvaluatePosition(board, x, y, playerValue);
                }
            }
        }

        return score;
    }

    int EvaluatePosition(int[,] board, int x, int y, int playerValue)
    {
        int totalScore = 0;

        totalScore += CountScoreInDirectionForEval(board, x, y, 1, 0, playerValue);  // 横向
        totalScore += CountScoreInDirectionForEval(board, x, y, 0, 1, playerValue);  // 纵向
        totalScore += CountScoreInDirectionForEval(board, x, y, 1, 1, playerValue);  // 正斜线
        totalScore += CountScoreInDirectionForEval(board, x, y, 1, -1, playerValue); // 反斜线

        return totalScore;
    }

    int CountScoreInDirectionForEval(int[,] board, int x, int y, int dx, int dy, int playerValue)
    {
        int count = 1;
        int block = 0;

        // 向正方向
        int tx = x + dx;
        int ty = y + dy;
        while (tx >= 0 && tx < 15 && ty >= 0 && ty < 15)
        {
            if (board[tx, ty] == playerValue)
            {
                count++;
                tx += dx;
                ty += dy;
            }
            else if (board[tx, ty] == 0)
            {
                break;
            }
            else
            {
                block++;
                break;
            }
        }

        // 向反方向
        tx = x - dx;
        ty = y - dy;
        while (tx >= 0 && tx < 15 && ty >= 0 && ty < 15)
        {
            if (board[tx, ty] == playerValue)
            {
                count++;
                tx -= dx;
                ty -= dy;
            }
            else if (board[tx, ty] == 0)
            {
                break;
            }
            else
            {
                block++;
                break;
            }
        }

        return EvaluateScore(count - 1, block); // 减去自身的一个子
    }

    bool IsGameOver(int[,] board)
    {
        // 检查是否有玩家获胜
        for (int x = 0; x < 15; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                if (board[x, y] != 0)
                {
                    if (CheckWin(x, y))
                    {
                        return true;
                    }
                }
            }
        }
        // 检查棋盘是否已满
        for (int x = 0; x < 15; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                if (board[x, y] == 0)
                {
                    return false;
                }
            }
        }
        return true;
    }
}