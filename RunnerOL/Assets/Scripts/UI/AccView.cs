using UnityEngine;
using UnityEngine.UI;
using RunnerCommon.Dto;
using RunnerCommon;
using LitJsonEx;

public class AccView : MonoBehaviour {

	[SerializeField]
	private InputField accField;
	[SerializeField]
	private InputField pswField;
	[SerializeField]
	private GameObject tips;
	[SerializeField]
	private Button btnSigin;
	[SerializeField]
	private Button btnRegister;
	[SerializeField]
	private GameObject createView;
	[SerializeField]
	private GameObject matchView;
	// Use this for initialization
	void Start () {
		EventCenter.Instance.AddListen<string>(EventType.RegisterAccount, ShowTips);
		EventCenter.Instance.AddListen<string>(EventType.SiginFalse, ShowTips);
		EventCenter.Instance.AddListen(EventType.SiginSucessWithName, SiginWithName);
		EventCenter.Instance.AddListen(EventType.SiginSucessWithouName, SiginWithoutName);
	}
	

	public void OnSiginBtnClick()
    {
		if (SoundsManager.Instance.SoundsOn)
			SoundsManager.Instance.PlayAudioByName("button");
		if (string.IsNullOrEmpty(accField.text) || string.IsNullOrEmpty(pswField.text))
			return;
		AccoutDto accDto = new AccoutDto()
		{
			acc = accField.text,
			psw = pswField.text
		};
		PhotonManager.Instance.Request(OpCode.AccCode, AccoutCode.Sigin, JsonMapper.ToJson(accDto));
		btnSigin.interactable = false;
	}

	public void OnRegisterBtnClick()
    {
		if (SoundsManager.Instance.SoundsOn)
			SoundsManager.Instance.PlayAudioByName("button");
		if (string.IsNullOrEmpty(accField.text) || string.IsNullOrEmpty(pswField.text))
			return;
		AccoutDto accDto = new AccoutDto()
		{
			acc = accField.text,
			psw = pswField.text
		};
		PhotonManager.Instance.Request(OpCode.AccCode, AccoutCode.Register, JsonMapper.ToJson(accDto));
		btnRegister.interactable = false;
	}

	void ShowTips(string msg)
    {
		tips.SetActive(true);
		tips.GetComponentInChildren<Text>().text = msg;
		accField.text = string.Empty;
		pswField.text = string.Empty;
		btnSigin.interactable = true;
		btnRegister.interactable = true;
    }

	void SiginWithoutName()
    {
		gameObject.SetActive(false);
		createView.SetActive(true);
    }

	void SiginWithName()
    {
		gameObject.SetActive(false);
		matchView.SetActive(true);
    }

    private void OnDestroy()
    {
		EventCenter.Instance.RemoveListen<string>(EventType.RegisterAccount, ShowTips);
		EventCenter.Instance.RemoveListen<string>(EventType.SiginFalse, ShowTips);
		EventCenter.Instance.RemoveListen(EventType.SiginSucessWithName, SiginWithName);
		EventCenter.Instance.RemoveListen(EventType.SiginSucessWithouName, SiginWithoutName);
	}
}
