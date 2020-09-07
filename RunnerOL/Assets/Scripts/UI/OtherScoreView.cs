using RunnerCommon.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherScoreView : MonoBehaviour {

	[SerializeField]
	GameObject scorePanelPrefabs;
	Dictionary<int, ScoreItem> dicScoreItem;

	void Awake()
    {
		EventCenter.Instance.AddListen<AccoutDto[]>(EventType.InitOtherScore, Init);
		EventCenter.Instance.AddListen<int,int>(EventType.UpdateOtherScore, UpdateScore);
		EventCenter.Instance.AddListen<int,int>(EventType.UpdateOtherLife, UpdateLife);
		EventCenter.Instance.AddListen<int>(EventType.RemoveScore, RemoveScore);
	}

	void Init(AccoutDto[] dtos)
    {
		dicScoreItem = new Dictionary<int, ScoreItem>();
		foreach (var dto in dtos)
		{
			if (dto.ID == int.Parse(PlayerCahe.Instance.ID))
				continue;
			GameObject goPanel = Instantiate(scorePanelPrefabs, gameObject.transform.FindChild("Grid").transform);
			ScoreItem item = goPanel.GetComponent<ScoreItem>();
			if (item == null)
				continue;
			item.Init(dto.name);
			dicScoreItem.Add(dto.ID, item);
        }
    }

	void UpdateScore(int id,int score)
    {
		if (!dicScoreItem.ContainsKey(id))
			return;
		dicScoreItem[id].UpdateScore(score);
    }

	void UpdateLife(int id,int life)
    {
		if (!dicScoreItem.ContainsKey(id))
			return;
		dicScoreItem[id].UpdateLife(life);
    }

	void RemoveScore(int id)
    {
		if (!dicScoreItem.ContainsKey(id))
			return;
		Destroy(dicScoreItem[id].gameObject);
	}

    private void OnDestroy()
    {
		EventCenter.Instance.RemoveListen<AccoutDto[]>(EventType.InitOtherScore, Init);
		EventCenter.Instance.RemoveListen<int, int>(EventType.UpdateOtherScore, UpdateScore);
		EventCenter.Instance.RemoveListen<int, int>(EventType.UpdateOtherLife, UpdateLife);
		EventCenter.Instance.RemoveListen<int>(EventType.RemoveScore, RemoveScore);
	}

}
