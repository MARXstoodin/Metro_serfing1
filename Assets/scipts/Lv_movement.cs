using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Lv_movement : MonoBehaviour
{
    public GameObject BuyLife;
    int Rnd;
    int Health = 3;
    int Derection = 0;
    int PreDerection = 0;
    float _timeLeft = 3f;
    bool _timerOn = false;
    float _timeLeft2 = 3.1f;
    bool _timer2On = false;
    public GameObject Pre1;
    public GameObject Pre2;
    public GameObject Pre3;
    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;
    public GameObject Background;
    public Rigidbody Rb;
    public AudioSource Cn;
    public ParticleSystem Trail;
    public ParticleSystem Stun;
    public ParticleSystem BonusDisaper;
    public ParticleSystem CoinDisaper;
    public AudioSource Jp;
    public Text Tx;
    public Text distanceText;
    public Text CostTx;
    public static int Score;
    public static int distance;
    public static int cost = 100;
    public AudioSource music;
    int BonusSpeed = 1;
    public static bool coinbehave = false;
    bool invincibility = false;
    public static bool ADwached = false;
    private void Start()
    {
        GlobalScore.GlobalCount = 0;
        BuyLife.SetActive(false);
        Lv_movement.distance = 0;
    }
    void Update()
    {
        if (ADwached == true & Health == 1)
        {
            music.UnPause();
        }
        distance = (int)this.transform.position.z/10;
        transform.Translate(new Vector3(0, 0, distance/20 + 10 * BonusSpeed) * Time.deltaTime);
        if (_timerOn == true)
        {
            if (_timeLeft > 0)
            {
                _timeLeft = _timeLeft - Time.deltaTime;
            }
            else
            {
                _timeLeft = 3f;
                _timerOn = false;
                invincibility = false;
                Stun.Stop();
            }
        }
        if (_timer2On == true)
        {
            if (_timeLeft2 > 0)
            {
                _timeLeft2 = _timeLeft2 - Time.deltaTime;
            }
            else
            {
                _timeLeft2 = 3.1f;
                _timer2On = false;
                BonusSpeed = 1;
                invincibility = false;
                Trail.Stop();
            }
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(Derection*3, transform.position.y, transform.position.z), Time.deltaTime*5);
        distanceText.text = distance.ToString() + "m";
        Tx.text = Score.ToString() + "$";
    }

    public void jampLeft()
    {
        if  (Derection > -1)
        {
            Jp.Play(0);
            Derection = Derection - 1;
            PreDerection = Derection + 1;
        }
        
    }
    public void jampRight()
    {
        if (Derection < 1)
        {
            Jp.Play(0);
            Derection = Derection + 1;
            PreDerection = Derection -1;
        }
        
    }
    public void jampUp()
    {
        if ((transform.position.y<0.6)||(transform.position.y > 4.8)&&(transform.position.y < 4.9))
        {
            Jp.Play(0);
            Rb.AddForce(0, 2000, 0);            
        }
        
    }
    public void jampDown()
    {
        if ((transform.position.y > 0.6) || (transform.position.y < 4.8) && (transform.position.y > 4.9))
        {
            Jp.Play(0);
            Rb.AddForce(0, -2000, 0);
        }

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Coin")
        {
            CoinDisaper.Play();
            Destroy(col.gameObject);
            Cn.Play();
            Score = Score + 1;
        }
        if (col.gameObject.tag == "Bonus")
        {
            Rb.velocity=new Vector3(0, 15, 0);
            BonusDisaper.Play();
            Trail.Play();
            _timer2On = true;
            BonusSpeed = 5;
            Destroy(col.gameObject);
            invincibility = true;
        }
        if (col.gameObject.tag == "Platform")
        {
            Derection = PreDerection;
        }
        if (col.gameObject.tag == "Obstacle")
        {
            Destroy(col.gameObject);
            if (invincibility == false)
            {
                Stun.Play();
                Health = Health - 1;
                invincibility = true;
                _timerOn = true;
            }
            if (Health == 0)
            {
                Heart3.SetActive(false);
                BuyLife.SetActive(true);
                Time.timeScale = 0;
                Health = 1;
                CostTx.text = "For only " + cost.ToString() + "$";
                music.Pause();
            }
            else if (Health == 1)
            {
                Destroy(Heart1);
            }
            else if (Health == 2)
            {
                Destroy(Heart2);
            }
        }
        if (col.gameObject.tag == "Finish")
        {
            Rnd = Random.Range(0, 6);
            switch (Rnd)
            {
                case 0:
                    GameObject roadClone1 = Instantiate(Pre1, new Vector3(0, 0, transform.position.z + 180), transform.rotation);
                    Destroy(roadClone1, 30f);
                    break;
                case 1:
                    GameObject roadClone2 = Instantiate(Pre2, new Vector3(0, 0, transform.position.z + 180), transform.rotation);
                    Destroy(roadClone2, 30f);
                    break;
                case 2:
                    GameObject roadClone3 = Instantiate(Pre3, new Vector3(0, 0, transform.position.z + 180), transform.rotation);
                    Destroy(roadClone3, 30f);
                    break;
                case 3:
                    GameObject roadClone4 = Instantiate(Pre1, new Vector3(0, 0, transform.position.z + 180), transform.rotation);
                    roadClone4.transform.localScale = new Vector3(-1, 1, 1);
                    Destroy(roadClone4, 30f);
                    break;
                case 4:
                    GameObject roadClone5 = Instantiate(Pre2, new Vector3(0, 0, transform.position.z + 180), transform.rotation);
                    roadClone5.transform.localScale = new Vector3(-1, 1, 1);
                    Destroy(roadClone5, 30f);
                    break;
                case 5:
                    GameObject roadClone6 = Instantiate(Pre3, new Vector3(0, 0, transform.position.z + 180), transform.rotation);
                    roadClone6.transform.localScale = new Vector3(-1, 1, 1);
                    Destroy(roadClone6, 30f);
                    break;
            }
            GameObject background = Instantiate(Background, new Vector3(0, 2, transform.position.z + 110), transform.rotation);
            Destroy(background, 30f);
        }
    }
}