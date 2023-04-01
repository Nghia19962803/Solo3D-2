using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLoseManager : MonoBehaviour
{
    public static WinLoseManager instance;


    public Transform endGamePanel;
    [SerializeField] private TextMeshProUGUI gameStatusText;
    public Button backBtn;
    void Start()
    {
        instance = this;
        endGamePanel.gameObject.SetActive(false);
        backBtn.onClick.AddListener(() =>
        {
            BackToMenu();
        });
    }
    public void ApearEndGamePanel(string status)
    {
        StartCoroutine(DisplayEndGamePanel(status));
    }
    IEnumerator DisplayEndGamePanel(string status)
    {
        yield return new WaitForSeconds(3);
        endGamePanel.gameObject.SetActive(true);
        gameStatusText.text = status;   
        //    float minutes = timeCount / 60;
        //    float seconds = timeCount % 60;
        //    hightScore.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void BackToMenu()    // hàm này gắn vào nút Menu
    {
        SceneManager.LoadScene(0);
    }
}
