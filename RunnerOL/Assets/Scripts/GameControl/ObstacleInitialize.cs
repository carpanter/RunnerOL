using RunnerCommon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleInitialize : Singleton<ObstacleInitialize> {

    public System.Random seed;

	public GameObject board;
	public GameObject boardUp;
	public GameObject bridge;
	public GameObject bus;
	public GameObject car;
	public GameObject stone;
	public GameObject tunnel;
	public GameObject tunnelL;
	public GameObject tunnelR;
    public GameObject cons;
    public GameObject magnetBuff;
    public GameObject multiplyBuff;
    public GameObject shoeBuff;
    public GameObject starBuff;

	float[] arryX = { -1.5f, 0, 1.5f };

    override protected void Awake()
    {
        base.Awake();
        PhotonManager.Instance.Request(OpCode.GameCode, GameCode.GameInit);
    }
	// Use this for initialization
	public void Init(int seedInt) 
    {
        this.seed = new System.Random(seedInt);
        for(int i=0;i<5;i++)
        {
            Debug.Log(seed.Next(0, 10));
        }

        for (int i=0;i<25; i++)
        {
			if (seed.Next(0, 15) < 4)
				continue;
			Vector3 pos = new Vector3(arryX[seed.Next(0, 3)], 0.8f, seed.Next(30, 320));
			GameObject go = Instantiate(board, pos, Quaternion.identity);
			go.transform.SetParent(GameObject.Find("Env/Obstacle").transform);
        }
        for (int i = 0; i < 25; i++)
        {
            if (seed.Next(0, 15) < 4)
                continue;
            Vector3 pos = new Vector3(arryX[seed.Next(0, 3)], 0.8f, seed.Next(320, 640));
            GameObject go = Instantiate(board, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Obstacle").transform);
        }

        for (int i = 0; i < 4; i++)
        {
            if (seed.Next(0, 4) < 1)
                continue;
            Vector3 pos = new Vector3(arryX[seed.Next(0, 3)], 0, seed.Next(30, 320));
            GameObject go = Instantiate(boardUp, pos, Quaternion.Euler(new Vector3(0, 180, 0)));
            go.transform.SetParent(GameObject.Find("Env/Obstacle").transform);
        }
        for (int i = 0; i < 4; i++)
        {
            if (seed.Next(0, 4) < 1)
                continue;
            Vector3 pos = new Vector3(arryX[seed.Next(0, 3)], 0, seed.Next(320, 640));
            GameObject go = Instantiate(boardUp, pos, Quaternion.Euler(new Vector3(0, 180, 0)));
            go.transform.SetParent(GameObject.Find("Env/Obstacle").transform);
        }

        for (int i = 0; i < 1; i++)
        {
            //if (seed.Next(0, 2) < 1)
            //    continue;
            Vector3 pos = new Vector3(0, 0, seed.Next(30, 320));
            GameObject go = Instantiate(bridge, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Obstacle").transform);
        }
        for (int i = 0; i < 1; i++)
        {
            //if (seed.Next(0, 2) < 1)
            //    continue;
            Vector3 pos = new Vector3(0, 0, seed.Next(320, 640));
            GameObject go = Instantiate(bridge, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Obstacle").transform);
        }

        for (int i = 0; i < 8; i++)
        {
            if (seed.Next(0, 8) < 2)
                continue;
            Vector3 pos = new Vector3(arryX[seed.Next(0, 3)], 0, seed.Next(30, 320));
            GameObject go = Instantiate(bus, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Obstacle").transform);
        }
        for (int i = 0; i < 8; i++)
        {
            if (seed.Next(0, 8) < 2)
                continue;
            Vector3 pos = new Vector3(arryX[seed.Next(0, 3)], 0, seed.Next(320, 640));
            GameObject go = Instantiate(bus, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Obstacle").transform);
        }

        for (int i = 0; i < 8; i++)
        {
            if (seed.Next(0, 8) < 2)
                continue;
            Vector3 pos = new Vector3(arryX[seed.Next(0, 3)], 0, seed.Next(30, 320));
            GameObject go = Instantiate(car, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Obstacle").transform);
        }
        for (int i = 0; i < 8; i++)
        {
            if (seed.Next(0, 8) < 2)
                continue;
            Vector3 pos = new Vector3(arryX[seed.Next(0, 3)], 0, seed.Next(320, 640));
            GameObject go = Instantiate(car, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Obstacle").transform);
        }

        for (int i = 0; i < 15; i++)
        {
            if (seed.Next(0, 10) < 2)
                continue;
            Vector3 pos = new Vector3(arryX[seed.Next(0, 3)], 0.4f, seed.Next(30, 320));
            GameObject go = Instantiate(stone, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Obstacle").transform);
        }
        for (int i = 0; i < 15; i++)
        {
            if (seed.Next(0, 10) < 2)
                continue;
            Vector3 pos = new Vector3(arryX[seed.Next(0, 3)], 0.4f, seed.Next(320, 640));
            GameObject go = Instantiate(stone, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Obstacle").transform);
        }

        for (int i = 0; i < 1; i++)
        {
            //if (seed.Next(0, 2) < 1)
            //    continue;
            Vector3 pos = new Vector3(0, 0, seed.Next(30, 320));
            GameObject go = Instantiate(tunnel, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Obstacle").transform);
        }
        for (int i = 0; i < 1; i++)
        {
            //if (seed.Next(0, 2) < 1)
            //    continue;
            Vector3 pos = new Vector3(0, 0, seed.Next(320, 640));
            GameObject go = Instantiate(tunnel, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Obstacle").transform);
        }

        for (int i = 0; i < 1; i++)
        {
            //if (seed.Next(0, 2) < 1)
            //    continue;
            Vector3 pos = new Vector3(0, 0, seed.Next(30, 320));
            GameObject go = Instantiate(tunnelL, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Obstacle").transform);
        }
        for (int i = 0; i < 1; i++)
        {
            //if (seed.Next(0, 2) < 1)
            //    continue;
            Vector3 pos = new Vector3(0, 0, seed.Next(320, 640));
            GameObject go = Instantiate(tunnelL, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Obstacle").transform);
        }

        for (int i = 0; i < 1; i++)
        {
            //if (seed.Next(0, 2) < 1)
            //    continue;
            Vector3 pos = new Vector3(0, 0, seed.Next(30, 320));
            GameObject go = Instantiate(tunnelR, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Obstacle").transform);
        }
        for (int i = 0; i < 1; i++)
        {
            //if (seed.Next(0, 2) < 1)
            //    continue;
            Vector3 pos = new Vector3(0, 0, seed.Next(320, 640));
            GameObject go = Instantiate(tunnelR, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Obstacle").transform);
        }

        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = new Vector3(arryX[seed.Next(0, 3)], 20, seed.Next(40, 320));
            GameObject go = Instantiate(cons, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Cons").transform);
        }
        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = new Vector3(arryX[seed.Next(0, 3)], 20, seed.Next(320, 640));
            GameObject go = Instantiate(cons, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Cons").transform);
        }

        //for (int i = 0; i < 2; i++)
        //{
        //    Vector3 pos = new Vector3(arryX[seed.Next(0, 3)], 20, seed.Next(40, 320));
        //    GameObject go = Instantiate(magnetBuff, pos, Quaternion.identity);
        //    go.transform.SetParent(GameObject.Find("Env/Buff").transform);
        //}
        //for (int i = 0; i < 2; i++)
        //{
        //    Vector3 pos = new Vector3(arryX[seed.Next(0, 3)], 20, seed.Next(320, 640));
        //    GameObject go = Instantiate(magnetBuff, pos, Quaternion.identity);
        //    go.transform.SetParent(GameObject.Find("Env/Buff").transform);
        //}

        for (int i = 0; i < 2; i++)
        {
            Vector3 pos = new Vector3(arryX[seed.Next(0, 3)], 20, seed.Next(40, 320));
            GameObject go = Instantiate(multiplyBuff, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Buff").transform);
        }
        for (int i = 0; i < 2; i++)
        {
            Vector3 pos = new Vector3(arryX[seed.Next(0, 3)], 20, seed.Next(320, 640));
            GameObject go = Instantiate(multiplyBuff, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Buff").transform);
        }

        for (int i = 0; i < 2; i++)
        {
            Vector3 pos = new Vector3(arryX[seed.Next(0, 3)], 20, seed.Next(40, 320));
            GameObject go = Instantiate(shoeBuff, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Buff").transform);
        }
        for (int i = 0; i < 2; i++)
        {
            Vector3 pos = new Vector3(arryX[seed.Next(0, 3)], 20, seed.Next(320, 640));
            GameObject go = Instantiate(shoeBuff, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Buff").transform);
        }

        for (int i = 0; i < 2; i++)
        {
            Vector3 pos = new Vector3(arryX[seed.Next(0, 3)], 20, seed.Next(40, 320));
            GameObject go = Instantiate(starBuff, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Buff").transform);
        }
        for (int i = 0; i < 2; i++)
        {
            Vector3 pos = new Vector3(arryX[seed.Next(0, 3)], 20, seed.Next(320, 640));
            GameObject go = Instantiate(starBuff, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Env/Buff").transform);
        }

    }
}
