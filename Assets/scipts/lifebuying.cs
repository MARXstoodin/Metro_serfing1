using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class lifebuying : MonoBehaviour
{
    public VideoPlayer vid;
    public GameObject Canvas2;
    public GameObject Canvas;
    public AudioSource NotEnoghMoney;
    public GameObject Heart3;
    public void Buy()
    {
        if (Lv_movement.cost <= Lv_movement.Score)
        {
            Heart3.SetActive(true);
            Time.timeScale = 1;
            Canvas.SetActive(false);
            Lv_movement.Score -= Lv_movement.cost;
            Lv_movement.cost *= 2;
        }
        else
        {
            NotEnoghMoney.Play();
        }
    }
    public void AD()
    {
        if(Lv_movement.ADwached == false)
        {
            vid.Play();
            vid.loopPointReached += CheckOver;
            Canvas2.SetActive(false);
            Canvas.SetActive(false);
        }
        else
        {
            NotEnoghMoney.Play();
        }
    }
    public void Exit()
    {
        SceneManager.LoadScene("Menu");
        GlobalScore.GlobalCount = Lv_movement.distance;
        Time.timeScale = 1;
        Lv_movement.Score = 0;
        Lv_movement.cost = 100;
        Lv_movement.ADwached = false;
    }
    void CheckOver(VideoPlayer vp)
    {
        Heart3.SetActive(true);
        Time.timeScale = 1;
        Canvas2.SetActive(true);
        vid.Stop();;
        Lv_movement.ADwached = true;
    }
}