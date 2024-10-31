using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpponentUIManager : MonoBehaviour
{
    public GameObject aiDifficultyPanel;
    public GameObject basePanel;
    public GameObject sceneMode;
    
    public Button pvpButton;
    public Button pveButton;
    // public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    public Button backButton;
    public Button backToSceneCollect;
    public Button thrmode;
    public Button twoMode;
    public Button quit;

    public string gameScene;

    void Start()
    {
        pvpButton.onClick.AddListener(OnPvPButtonClicked);
        pveButton.onClick.AddListener(OnPvEButtonClicked);
        // easyButton.onClick.AddListener(() => OnDifficultySelected("Easy"));
        mediumButton.onClick.AddListener(() => OnDifficultySelected("Medium"));
        hardButton.onClick.AddListener(() => OnDifficultySelected("Hard"));
        backButton.onClick.AddListener(OnBackButtonClicked);
        
        thrmode.onClick.AddListener(OnThrmodeClicked);
        twoMode.onClick.AddListener(OnTwoModeClicked);
        backToSceneCollect.onClick.AddListener(OnBackToSceneClicked);
        quit.onClick.AddListener(OnQuitClick);
        
    }

    void OnPvPButtonClicked()
    {
        GameSettings.isAIGame = false;
        SceneManager.LoadScene(gameScene);
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
        
        SceneManager.LoadScene(gameScene);
    }

    void OnBackButtonClicked()
    {
        Debug.Log("Back");
        
        aiDifficultyPanel.SetActive(false);
        basePanel.SetActive(true);
    }

    void OnBackToSceneClicked()
    {
        sceneMode.SetActive(true);
        basePanel.SetActive(false);
    }

    void OnThrmodeClicked()
    {
        gameScene = "3D";
        sceneMode.SetActive(false);
        basePanel.SetActive(true);
    }

    void OnTwoModeClicked()
    {
        gameScene = "GameScene";
        sceneMode.SetActive(false);
        basePanel.SetActive(true);
    }

    void OnQuitClick()
    {
        Application.Quit();
    }
}
