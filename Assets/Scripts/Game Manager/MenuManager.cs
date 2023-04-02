using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public Button startBtn;
    public Button quitBtn;
    public TextMeshProUGUI timeText;
    private float timeCount;
    public static MenuManager Instance { get; private set; }

    public int gameRound;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        startBtn.onClick.AddListener(() =>
        {
            StartNew();
        });
        quitBtn.onClick.AddListener(() =>
        {
            QuitGame();
        });
        gameRound = 1;
    }
    public void StartNew()
    {
        SceneManager.LoadScene(1);  // load main scene
    }
    // quit game when press quit button
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);  // load main scene
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
