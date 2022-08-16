using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Video;

public class adver : MonoBehaviour
{
    public VideoPlayer vid;
    void Start()
    {
        vid.loopPointReached += CheckOver;
    }
    void CheckOver(VideoPlayer vp)
    {
        SceneManager.LoadScene("SampleScene");
    }
}