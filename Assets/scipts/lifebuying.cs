using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifebuying : MonoBehaviour
{
    public GameObject Canvas;

    public GameObject Heart3;
    public void Buy()
    {
        Time.timeScale = 1;
        Canvas.SetActive(false);
        (Heart3).SetActive(true);
        Lv_movement.Score -= Lv_movement.cost;
    }
}