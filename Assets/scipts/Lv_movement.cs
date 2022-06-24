using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Lv_movement : MonoBehaviour
{
    int Rnd;
    int Derection = 0;
    public GameObject Pre1;
    public GameObject Pre2;
    public GameObject Pre3;
    public GameObject Background;
    public Rigidbody Rb;
    public AudioSource Cn;
    public ParticleSystem CoinDisaper;
    public AudioSource Jp;
    public Text Tx;
    int Score;
    void Update()
    {
        transform.Translate(new Vector3(0, 0, 10) * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Derection > -1)
            {
                Derection = Derection - 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Derection < 1)
            {
                Derection = Derection + 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if ((transform.position.y<0.6)||(transform.position.y > 4.8)&&(transform.position.y < 4.9))
            {
                Jp.Play(0);
                Rb.AddForce(0, 2000, 0);
            }
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(Derection*3, transform.position.y, transform.position.z), Time.deltaTime*5);
        Tx.text = Score.ToString();
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Coin")
        {
            CoinDisaper.Play();
            Destroy(col.gameObject);
            Cn.Play(0);
            Score = Score + 1;
        }
        if (col.gameObject.tag == "Obstacle")
        {
            SceneManager.LoadScene("menu");
        }
        if (col.gameObject.tag == "Finish")
        {
            Rnd = Random.Range(0, 3);
            switch (Rnd)
            {
                case 0:
                    GameObject roadClone1 = Instantiate(Pre1, new Vector3(0, 0, transform.position.z + 175), transform.rotation);
                    Destroy(roadClone1, 30f);
                    break;
                case 1:
                    GameObject roadClone2 = Instantiate(Pre2, new Vector3(0, 0, transform.position.z + 175), transform.rotation);
                    Destroy(roadClone2, 30f);
                    break;
                case 2:
                    GameObject roadClone3 = Instantiate(Pre3, new Vector3(0, 0, transform.position.z + 175), transform.rotation);
                    Destroy(roadClone3, 30f);
                    break;
            }
            GameObject background = Instantiate(Background, new Vector3(0, 2, transform.position.z + 110), transform.rotation);
            Destroy(background, 30f);
        }
    }
}