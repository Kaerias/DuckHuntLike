using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootFairy : MonoBehaviour {

    public GameObject sprite;
    private GameObject gameBoard;

    public int countLife = 3;
    bool isShooted = false;
    float speed;
    int direction;
    bool isPause = false;

    private void Start()
    {
        gameBoard = GameObject.Find("GameBoard");
        speed = gameBoard.GetComponent<GameController>().GetSpeed();
        direction = -1;
    }

    // Update is called once per frame
    void Update () {
        if (!isPause)
        {
            SetDirection();
            Shooted();
            Translated();
        }
	}

    private void OnMouseDown()
    {
        if (!isPause)
        {
            isShooted = true;
            Destroy(gameObject);
        }
    }

    void SetDirection()
    {
        if (transform.position.x <= -8 || direction == -1)
            direction = 0;
        else if (transform.position.x >= 8)
            direction = 1;
    }

    void Translated()
    {
        if (direction == 0)
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        else if (direction == 1)
            transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void Shooted()
    {
        if (!isShooted && Input.GetKeyDown(KeyCode.Mouse0))
        {
            gameBoard.GetComponent<GameController>().SetShootFail(true);
        }
    }

    public bool GetIsShooted()
    {
        return isShooted;
    }

    public void SetIsShooted()
    {
        isShooted = false;
    }

    public void SetPause(bool b)
    {
        isPause = b;
    }

}
