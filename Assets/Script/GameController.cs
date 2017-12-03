using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text Score;
    public Text Life;
    public GameObject buttonEndGame;
    public GameObject buttonRetry;
    public GameObject sprite;
    public float speed = 1;
    public float speedUp = 0.5f;

    GameObject invokeFairy;
    GameObject Fairy;

    public AudioClip[] Music;
    private AudioSource m_MyAudioSource;

    public int countLife = 3;
    int countScore = 0;
   // int nbrFairydisplay = 1;
    bool shootFail = false;
    bool End = false;
    bool isActive = false;

    private void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
        InvokeFairyOnBoard();
        Score.text = "Score = " + countScore.ToString();
        Life.text = "Life = " + countLife.ToString();
        //  StartCoroutine(NumberGen());
    }

    void Update()
    {
        if (!End)
        {
            Fairy = GameObject.FindGameObjectWithTag("Fairy");
            SpawnFairy();
            LifeScore();
            DisplayScore();
            ExitGameWithButton();
        }
        else
        {
            if (!isActive)
            {
                buttonEndGame.SetActive(true);
                buttonRetry.SetActive(true);
                isActive = !isActive;
            }
        }
    }

    void SpawnFairy()
    {
        if (Input.GetKeyDown("space"))
        {
            InvokeFairyOnBoard();
        }
        if (Fairy.GetComponent<ShootFairy>().GetIsShooted())
        {
            m_MyAudioSource.PlayOneShot(Music[0], 0.7f);
            InvokeFairyOnBoard();
            Fairy.GetComponent<ShootFairy>().SetIsShooted();
            countScore += 1;
            speed += speedUp;
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
        countLife = 3;
        countScore = 0;
        speed = 1;
        isActive = !isActive;
        End = false;
        buttonEndGame.SetActive(false);
        buttonRetry.SetActive(false);
        InvokeFairyOnBoard();

    }

    void InvokeFairyOnBoard()
    {
        //   for (int i = 0; i < nbrFairydisplay; i++)
        // {
        invokeFairy = Instantiate(sprite, transform.position, transform.rotation) as GameObject;
        invokeFairy.transform.position = new Vector3(-8, Random.Range(-3.5f, 3.5f), -1);
        //}
    }

    void DisplayScore()
    {
        Score.text = "Score = " + countScore.ToString();
        Life.text = "Life = " + countLife.ToString();
    }

    public void EndGame()
    {
        Application.Quit();
    }


    void ExitGameWithButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetShootFail(bool b)
    {
        shootFail = b;
    }


    /*  IEnumerator NumberGen()
  {
      while (true)
      {
          direction = Random.Range(0, 5);
          yield return new WaitForSeconds(1);
      }
  }
*/
}
