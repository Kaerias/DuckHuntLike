using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text Score;
    public Text Life;
    public GameObject buttonEndGame;
    public GameObject buttonRetry;
    public GameObject sprite;
    public GameObject BloodParticle;

    public float speed = 1;
    public float speedUp = 0.5f;
    public int countLife = 3;

    public AudioClip[] Music;

    GameObject BloodDelete;
    GameObject invokeFairy;
    GameObject Fairy;
    GameObject[] allFairy;

    AudioSource m_MyAudioSource;

    int i = 0;
    int countScore = 0;
    bool shootFail = false;
    bool End = false;
    bool isActive = false;
    bool isPause = false;

    private void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
        InvokeFairyOnBoard();
        Score.text = "Score = " + countScore.ToString();
        Life.text = "Life = " + countLife.ToString();
    }

    void Update()
    {
        if (!isPause)
        {
            if (!End)
            {
                Fairy = GameObject.FindGameObjectWithTag("Fairy");
                SpawnFairy();
                LifeScore();
                DisplayScore();
            }
            else
            {
                if (!isActive)
                {
                    StatusButton(true);
                    isActive = !isActive;
                }
            }
        }
        ExitGameWithButton();
    }

    void SpawnFairy()
    {
        if (Input.GetKeyDown("space"))
        {
            InvokeFairyOnBoard();
        }
        if (Fairy.GetComponent<ShootFairy>().GetIsShooted())
        {
            BloodDelete = Instantiate(BloodParticle, Fairy.transform.position, transform.rotation) as GameObject;
            BloodDelete.transform.position = Fairy.transform.position;
            m_MyAudioSource.PlayOneShot(Music[0], 0.7f);
            InvokeFairyOnBoard();
            i++;
            Fairy.GetComponent<ShootFairy>().SetIsShooted();
            countScore += 1;
            speed += speedUp;
            Destroy(BloodDelete, 2f);
        }
    }

    public void LifeScore()
    {
        if (shootFail)
        {
            shootFail = !shootFail;
            countLife -= 1;
            m_MyAudioSource.PlayOneShot(Music[1], 0.7f);
            if (countLife == 0)
            {
                Destroy(Fairy);
                End = true;
            }
        }
    }

    public void Retry()
    {
        if (Fairy != null)
        {
            Fairy.GetComponent<ShootFairy>().SetPause(false);
            allFairy = GameObject.FindGameObjectsWithTag("Fairy");
            if (allFairy != null)
            {
                foreach (GameObject Fairy in allFairy)
                {
                    Destroy(Fairy);
                }
            }
        }
        isPause = false;
        countLife = 3;
        countScore = 0;
        speed = 1;
        isActive = !isActive;
        End = false;
        StatusButton(false);
        InvokeFairyOnBoard();
    }

    void InvokeFairyOnBoard()
    {
        invokeFairy = Instantiate(sprite, transform.position, transform.rotation) as GameObject;
        invokeFairy.transform.position = new Vector3(-8, Random.Range(-3.5f, 3.5f), -1);
    }

    void DisplayScore()
    {
        Score.text = "Score = " + countScore.ToString();
        Life.text = "Life = " + countLife.ToString();
    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }


    void ExitGameWithButton()
    {
        if (countLife != 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPause)
                {
                    isPause = true;
                    Fairy.GetComponent<ShootFairy>().SetPause(true);
                    StatusButton(true);
                }
                else
                {
                    isPause = !isPause;
                    Fairy.GetComponent<ShootFairy>().SetPause(false);
                    StatusButton(false);
                }
            }
        }
    }

    void StatusButton(bool b)
    {
        buttonEndGame.SetActive(b);
        buttonRetry.SetActive(b);
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetShootFail(bool b)
    {
        shootFail = b;
    }

}
