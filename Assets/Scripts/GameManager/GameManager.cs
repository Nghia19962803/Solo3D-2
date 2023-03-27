using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI hightScore;
    private float timeCount = 0;
    public Button backBtn;
    public Transform endGamePanel;
    public bool startGame { get;set; }
    private void Awake()
    {
        startGame = false;
        instance = this;
        endGamePanel.gameObject.SetActive(false);
        backBtn.onClick.AddListener(() =>
        {
            BackToMenu();
        });
    }
    private void FixedUpdate()
    {
        if (!startGame) return;
        CountTime();
    }
    public bool StartGame()
    {
        return startGame;
    }
    public void StopGame()
    {
        startGame = false ;
    }
    public void CountTime()
    {
        float minutes = timeCount/60;
        float seconds = timeCount%60;
        timeCount += Time.fixedDeltaTime;
        timeText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }
    public void EndGame()
    {
        MenuManager menu = FindObjectOfType<MenuManager>().GetComponent<MenuManager>();
        if (menu.GetTimeCount() > this.timeCount)
        {
            SaveTime();
        }
        startGame = false;
        
        StartCoroutine(DisplayEndGamePanel());
    }
    public void SaveTime()
    {
        SaveData data = new SaveData();
        data._timeCount = timeCount;
        
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    IEnumerator DisplayEndGamePanel()
    {
        yield return new WaitForSeconds(3);
        endGamePanel.gameObject.SetActive(true);
        float minutes = timeCount / 60;
        float seconds = timeCount % 60;
        hightScore.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
[System.Serializable]
public class SaveData
{
    public float _timeCount;

}
