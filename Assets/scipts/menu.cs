using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public Text FinalScore;
    public Button enterButton;
    public Button exitButton;
    void Start()
    {
        exitButton.onClick.AddListener(exit);
        enterButton.onClick.AddListener(enter);
        FinalScore.text = "Your score: " + GlobalScore.GlobalCount.ToString();
    }
    void exit()
    {
        Application.Quit();
    }
    void enter()
    {
        SceneManager.LoadScene("SampleScene");
    }
   
}