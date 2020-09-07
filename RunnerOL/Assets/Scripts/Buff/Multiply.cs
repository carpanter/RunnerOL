using RunnerCommon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiply : BuffObstacle
{

	new void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player" && !col.isTrigger)
		{
			if (SoundsManager.Instance.SoundsOn)
				SoundsManager.Instance.PlayAudioByName("getitem");
			PhotonManager.Instance.Request(OpCode.GameCode, GameCode.GetBuff, 2);
		}
		base.OnTriggerEnter(col);
	}
}
