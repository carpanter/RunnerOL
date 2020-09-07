using RunnerCommon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : BuffObstacle
{

	new void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player" && !col.isTrigger)
		{
			if (SoundsManager.Instance.SoundsOn)
				SoundsManager.Instance.PlayAudioByName("getitem");
			PhotonManager.Instance.Request(OpCode.GameCode, GameCode.GetBuff, 0);
		}
		base.OnTriggerEnter(col);
	}
}
