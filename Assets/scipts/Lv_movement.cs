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
    float _timeLeft = 3f;
    bool _timerOn = false;
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
    public Text CostTx;
    public static int Score;
    public static int cost = 5;
    int BonusSpeed = 1;
    bool invincibility = false;
    private void Start()
    {
        GlobalScore.GlobalCount = 0;
        BuyLife.SetActive(false);
    }
    void Update()
    {
        transform.Translate(new Vector3(0, 0, Score/20+10*BonusSpeed) * Time.deltaTime);
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
                BonusSpeed = 1;
                invincibility = false;
                Stun.Stop();
                Trail.Stop();
                //BonusDisaper.Stop();
                //print("ABOBA");
            }
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(Derection*3, transform.position.y, transform.position.z), Time.deltaTime*5);
        Tx.text = Score.ToString();
    }

    public void jampLeft()
    {
        if  (Derection > -1)
        {
            Jp.Play(0);
            Derection = Derection - 1;
        }
        
    }

    public void jampRight()
    {
        if (Derection < 1)
        {
            Jp.Play(0);
            Derection = Derection + 1;
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
            Cn.Play(0);
            Score = Score + 1;
            GlobalScore.GlobalCount = GlobalScore.GlobalCount + 1;
        }
        if (col.gameObject.tag == "Bonus")
        {
            Rb.velocity=new Vector3(0, 15, 0);
            BonusDisaper.Play();
            Trail.Play();
            _timerOn = true;
            BonusSpeed = 5;
            Destroy(col.gameObject);
            invincibility = true;            
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
                //print(Health);
            }
            if (Health == 0)
            {
                cost *= 2;
                Heart3.SetActive(false);
                if (Score >= cost)
                {
                    BuyLife.SetActive(true);
                    Time.timeScale = 0;
                    Health = 1;
                    CostTx.text = "For only "+cost.ToString();
                }
                else
                {
                    SceneManager.LoadScene("Menu");
                }
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
                case 3:
                    GameObject roadClone4 = Instantiate(Pre1, new Vector3(0, 0, transform.position.z + 175), transform.rotation);
                    roadClone4.transform.localScale = new Vector3(-1, 1, 1);
                    Destroy(roadClone4, 30f);
                    break;
                case 4:
                    GameObject roadClone5 = Instantiate(Pre2, new Vector3(0, 0, transform.position.z + 175), transform.rotation);
                    roadClone5.transform.localScale = new Vector3(-1, 1, 1);
                    Destroy(roadClone5, 30f);
                    break;
                case 5:
                    GameObject roadClone6 = Instantiate(Pre3, new Vector3(0, 0, transform.position.z + 175), transform.rotation);
                    roadClone6.transform.localScale = new Vector3(-1, 1, 1);
                    Destroy(roadClone6, 30f);
                    break;
            }
            GameObject background = Instantiate(Background, new Vector3(0, 2, transform.position.z + 110), transform.rotation);
            Destroy(background, 30f);
        }
    }
}