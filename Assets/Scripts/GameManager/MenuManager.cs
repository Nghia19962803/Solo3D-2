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


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        //LoadScore();
        //DisplayScore();
        startBtn.onClick.AddListener(() =>
        {
            StartNew();
        });
        quitBtn.onClick.AddListener(() =>
        {
            QuitGame();
        });

    }
    public void StartNew()
    {
        SceneManager.LoadScene(1);  // load main scene
    }
    //public void LoadScore() // read saved data
    //{
    //    Debug.Log("Load Hight Score");
    //    string path = Application.persistentDataPath + "/savefile.json";
    //    if (File.Exists(path))
    //    {
    //        string json = File.ReadAllText(path);
    //        SaveData data = JsonUtility.FromJson<SaveData>(json);

    //        timeCount = data._timeCount;
    //    }
    //}
    //public float GetTimeCount() //get time finished this game in main scene
    //{
    //    return timeCount;
    //}
    //public void DisplayScore()  //display best time to finish game in menu scene
    //{
    //    float minutes = timeCount / 60;
    //    float seconds = timeCount % 60;
    //    timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    //}
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
