using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : Singleton<GameController> {


	public GameObject player;
	public GameObject otherPlayer;
	public GameObject road;
	public GameObject[] building;
	public Material[] buildingMat;
	public int area = 0;

	List<GameObject> ListRoad = new List<GameObject>();
	List<GameObject> ListLeftBuilding = new List<GameObject>();
	List<GameObject> ListRightBuilding = new List<GameObject>();

	// Use this for initialization
	override protected void Awake () 
	{
		base.Awake();
		for (int i=0;i<80;i++)
        {
			Vector3 pos = new Vector3(0, 0, i * 8);
			GameObject goRoad = Instantiate(road, pos, Quaternion.identity);
			goRoad.transform.SetParent(GameObject.Find("Env/Road").transform);
			ListRoad.Add(goRoad);
        }
		for(int i=0;i<160;i++)
        {
			Vector3 pos = new Vector3(3, 0, i * 4);
			GameObject goRightBD = Instantiate(building[Random.Range(0, building.Length)], pos, Quaternion.identity);
			goRightBD.GetComponent<MeshRenderer>().material = buildingMat[Random.Range(0, buildingMat.Length)];
			goRightBD.transform.SetParent(GameObject.Find("Env/RightBuilding").transform);
			ListRightBuilding.Add(goRightBD);
        }
		for (int i = 0; i < 160; i++)
		{
			Vector3 pos = new Vector3(-3, 0, i * 4 - 4);
			GameObject goLeftBD = Instantiate(building[Random.Range(0, building.Length)], pos, Quaternion.Euler(new Vector3(0, 180, 0)));
			goLeftBD.GetComponent<MeshRenderer>().material = buildingMat[Random.Range(0, buildingMat.Length)];
			goLeftBD.transform.SetParent(GameObject.Find("Env/LeftBuilding").transform);
			ListLeftBuilding.Add(goLeftBD);
		}
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(player.transform.position.z > ListRoad[ListRoad.Count / 2].transform.position.z)
        {
			List<GameObject> newListRoad = new List<GameObject>();
			newListRoad.AddRange(ListRoad.Skip(ListRoad.Count / 2));
			for(int i=0;i<ListRoad.Count/2;i++)
            {
				ListRoad[i].transform.position += Vector3.forward * ListRoad.Count * 8;
				newListRoad.Add(ListRoad[i]);
            }
			ListRoad = newListRoad;

			List<GameObject> newListRightBuilding = new List<GameObject>();
			List<GameObject> newListLeftBuilding = new List<GameObject>();
			newListRightBuilding.AddRange(ListRightBuilding.Skip(ListRightBuilding.Count / 2));
			newListLeftBuilding.AddRange(ListLeftBuilding.Skip(ListLeftBuilding.Count / 2));
			for (int i = 0; i < ListRightBuilding.Count / 2; i++)
			{
				ListRightBuilding[i].transform.position += Vector3.forward * ListRightBuilding.Count * 4;
				ListLeftBuilding[i].transform.position += Vector3.forward * ListLeftBuilding.Count * 4;
				newListRightBuilding.Add(ListRightBuilding[i]);
				newListLeftBuilding.Add(ListLeftBuilding[i]);
			}
			ListRightBuilding = newListRightBuilding;
			ListLeftBuilding = newListLeftBuilding;
			area++;
		}
	}

	public Dictionary<int,PlayerControl> InitPlayer(int[] playerIDs)
    {
		Dictionary<int, PlayerControl> dicIDCon = new Dictionary<int, PlayerControl>();
		foreach (int id in playerIDs)
		{
			PlayerControl con;
			if (id.ToString() == PlayerCahe.Instance.ID)
            {
				player.transform.position = new Vector3(0, 0, 0);
				con = player.GetComponent<PlayerControl>();
			}
			else
            {
				con = Instantiate(otherPlayer, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<PlayerControl>();
			}				
			if (con != null)
				dicIDCon.Add(id, con);
		}
		return dicIDCon;

	}
}
