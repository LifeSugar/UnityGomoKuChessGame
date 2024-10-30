using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpponentUIManager : MonoBehaviour
{
    public GameObject aiDifficultyPanel;
    public GameObject basePanel;
    public Button pvpButton;
    public Button pveButton;
    // public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    public Button backButton;

    void Start()
    {
        pvpButton.onClick.AddListener(OnPvPButtonClicked);
        pveButton.onClick.AddListener(OnPvEButtonClicked);
        // easyButton.onClick.AddListener(() => OnDifficultySelected("Easy"));
        mediumButton.onClick.AddListener(() => OnDifficultySelected("Medium"));
        hardButton.onClick.AddListener(() => OnDifficultySelected("Hard"));
        backButton.onClick.AddListener(OnBackButtonClicked);
    }

    void OnPvPButtonClicked()
    {
        GameSettings.isAIGame = false;
        SceneManager.LoadScene("GameScene");
    }

    void OnPvEButtonClicked()
    {
   
        aiDifficultyPanel.SetActive(true);
        basePanel.SetActive(false);
    }

    void OnDifficultySelected(string difficulty)
    {
        GameSettings.isAIGame = true;
        GameSettings.aiDifficulty = difficulty;
        
        SceneManager.LoadScene("GameScene");
    }

    void OnBackButtonClicked()
    {
        Debug.Log("Back");
        
        aiDifficultyPanel.SetActive(false);
        basePanel.SetActive(true);
    }
}