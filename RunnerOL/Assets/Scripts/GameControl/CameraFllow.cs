using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFllow : MonoBehaviour
{
    //public float cameraMoveSpeed = 20.0f;

    Transform target;
    Vector3 offset;
    bool cameraShake = false;
    float shakeTime;
    float shakeQuake;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target != null)
        {
            offset = gameObject.transform.position - target.position;
        }
        transform.LookAt(target);
        EventCenter.Instance.AddListen<float, float>(EventType.CameraShake, CameraShake);
        EventCenter.Instance.AddListen(EventType.CameraChange, CameraChange);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
            gameObject.transform.position = target.position + offset;

        if (cameraShake)
        {
            transform.localPosition += Random.insideUnitSphere * shakeQuake;
        }
            
    }

    void CameraShake(float time ,float quake)
    {
        cameraShake = true;
        shakeTime = time;
        shakeQuake = quake;
        StartCoroutine("ShakeTimmer", shakeTime);
    }

    IEnumerator ShakeTimmer(float time)
    {
        yield return new WaitForSeconds(time);
        transform.localPosition = Vector3.zero;
        cameraShake = false;
        shakeTime = 0;
        shakeQuake = 0;
    }

    void CameraChange()
    {
        StartCoroutine("ChangeCamera");
    }

    IEnumerator ChangeCamera()
    {
        yield return new WaitForSeconds(3.5f);
        if(!ProgressSetting.Instance.gameOver)
        {
            Transform newTarget = GameObject.FindGameObjectWithTag("OtherPlayer").transform;
            if (newTarget != null)
                target = newTarget;
        }
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveListen<float, float>(EventType.CameraShake, CameraShake);
        EventCenter.Instance.RemoveListen(EventType.CameraChange, CameraChange);
    }
}
