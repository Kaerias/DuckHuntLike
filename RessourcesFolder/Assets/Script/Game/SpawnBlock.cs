using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour {

    public GameObject wall;
    private GameController GameController;
    private GameObject wallInvoked;
    public float wallTimer = 5f;
	public int ScoreToNeedToDisplayWall = 10;
    private float reInvokeWallTimer;
    private bool isPause = false;
    private int score;
    private int modulo;
    private int nbrSpawnBlock;

    // Use this for initialization
    void Start () {
        GameController = gameObject.GetComponent<GameController>();
        reInvokeWallTimer = wallTimer;
        score = 0;
        modulo = ScoreToNeedToDisplayWall;
        nbrSpawnBlock = 1;
	}

    // Update is called once per frame
    void Update()
    {
        if (GameController.GetIsPause() || GameController.GetEnd())
        {
            isPause = true;
        } else if (!GameController.GetIsPause() && !GameController.GetEnd())
        {
            isPause = false;
        }
        if (GameController.GetEnd())
            DestroyWall();
        if (!isPause)
        {
            CalculNbrSpawnBlock();
            wallTimer -= Time.deltaTime;
            if (wallTimer <= 0)
            {
                DestroyWall();
				NbrSpawnBlockInMap();
                wallTimer = reInvokeWallTimer;
            }
        }
    }

	//laisser à faire au lycéen
	void NbrSpawnBlockInMap()
	{
		int i = 0;

		while (i < nbrSpawnBlock)
		{
			wallInvoked = Instantiate(wall, transform.position, transform.rotation) as GameObject;
			wallInvoked.transform.position = new Vector3(Random.Range(-8f, 8f), Random.Range(-3.5f, 3.5f), -2);
			i = i + 1;
		}
	}

    void CalculNbrSpawnBlock()
    {
        score = GameController.GetScore();
        if (score % modulo == 0 && score != 0)
        {
            nbrSpawnBlock += 1;
            modulo += 5;
        }
    }

	public void ResetScore()
	{
		modulo = ScoreToNeedToDisplayWall;
		nbrSpawnBlock = 1;
	}

    public void DestroyWall()
    {
        GameObject[] allWall = GameObject.FindGameObjectsWithTag("Wall");
        if (allWall != null)
        {
            foreach (GameObject wall in allWall)
            {
                Destroy(wall);
            }
        }
    }
}
