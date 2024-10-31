using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFunc : MonoBehaviour
{
    public void endGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
