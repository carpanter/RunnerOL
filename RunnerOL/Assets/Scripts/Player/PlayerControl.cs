using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using RunnerCommon;
using System.Xml.Serialization;

public class PlayerControl : MonoBehaviour
{
    public enum PlayerState
    {
        Runing,
        TurnRight,
        TurnLeft,
        Rolling,
        Jump,
        DoubleJump,
        JumpDown,
        InFlaot, //在空中
        Dead,
    }

    public float turnSpeed = 8.0f;
    public float forwardSpeed = 10.0f;
    public bool doubleJump = false;
    public float jumpFroce = 300.0f;
    public bool sprint = false;
    public bool multiplyCoin = false;
    public PlayerState playerState = new PlayerState();
     
    Rigidbody rig;
    PlayerBuff pb;
    SkinnedMeshRenderer render;

    int turnDir = 0; //转左就是正的，转右就是负的
    Vector3 prePosition = Vector3.zero; //记录上次结束移动时的位置
    bool hasDoubleJump = false;
    bool isInTurning = false;
    bool isInDamage = false;
    float speedTimer;
    float shakeTimer;

    // Start is called before the first frame update
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody>();
        pb = gameObject.GetComponent<PlayerBuff>();
        render = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        prePosition = transform.position;
        turnDir = Mathf.Clamp(turnDir, -2, 2);
        playerState = PlayerState.Runing;
        speedTimer = 0;
        shakeTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState == PlayerState.Dead || ProgressSetting.Instance.gameOver)
            return;
        if(!ProgressSetting.Instance.gameStart)
        {
            rig.isKinematic = true;
            return;
        }
        else
        {
            rig.isKinematic = false;
        }
            
        speedTimer += Time.deltaTime;
        if (sprint)
            forwardSpeed = 20.0f;
        else if(!isInDamage)
            forwardSpeed = (speedTimer / 120) + 5;
        if (!sprint && forwardSpeed > 12)
            forwardSpeed = 12;

        if (turnDir < 2 && turnDir > -1)
            InputManager.Instance.TrunRightSend = true;
        else
            InputManager.Instance.TrunRightSend = false;
        if(turnDir > -2 && turnDir < 1)
            InputManager.Instance.TrunLeftSend = true;
        else
            InputManager.Instance.TrunLeftSend = false;
        if (playerState != PlayerState.Rolling)
            InputManager.Instance.RollSend = true;
        else
            InputManager.Instance.RollSend = false;
        if (playerState != PlayerState.Jump && playerState != PlayerState.InFlaot || doubleJump && (playerState == PlayerState.Jump || playerState == PlayerState.InFlaot) && hasDoubleJump == false)
            InputManager.Instance.JumpSend = true;
        else
            InputManager.Instance.JumpSend = false;

        if (playerState == PlayerState.Runing && rig.velocity.y < -0.5f && transform.position.y > 1)
        {
            playerState = PlayerState.JumpDown;
        }

        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime); //向前走
        Turn(turnDir, prePosition.x);
        if (isInDamage)
            Damage();
    }
    
    public void OnTrunRight()
    {
        turnDir++;
    }

    public void OnTrunLeft()
    {
        turnDir--;
    }
    
    public void OnRoll(bool PostEvent)
    {
        if(PostEvent)
            EventCenter.Instance.PostEvent(EventType.RollingStart);
        playerState = PlayerState.Rolling;
    }

    public void OnJump()
    {
        if (playerState != PlayerState.Jump && playerState != PlayerState.InFlaot)
        {
            playerState = PlayerState.Jump;
            rig.AddForce(Vector3.up * jumpFroce);
        }
        else if (doubleJump && (playerState == PlayerState.Jump || playerState == PlayerState.InFlaot) && hasDoubleJump == false)
        {
            playerState = PlayerState.DoubleJump;
            hasDoubleJump = true;
            rig.AddForce(Vector3.up * jumpFroce);
        }
    }

    public void OnGetBuff(int code)
    {
        switch(code)
        {
            case 0:
                sprint = true;
                pb.GetStar();
                break;
            case 1:
                doubleJump = true;
                pb.GetShoe();
                break;
            case 2:
                multiplyCoin = true;
                pb.GetMultiply();
                break;
        }
    }

    public void OnDamage()
    {
        isInDamage = true;
        forwardSpeed -= 0.5f;
        Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Obstacle"), true);
    }

    public void OnDead()
    {
        playerState = PlayerState.Dead;
        //if(gameObject.tag == "Player")
          //  EventCenter.Instance.PostEvent(EventType.CameraChange);
    }

    void Turn(int dir, float StartX)
    {
        if(dir > 0)
        {
            if (transform.position.x < 1.5f && Math.Abs(transform.position.x - StartX) < 1.5f)
            {
                if (!isInTurning)
                {
                    if (SoundsManager.Instance.SoundsOn)
                        SoundsManager.Instance.PlayAudioByName("slide");
                    playerState = PlayerState.TurnRight;
                    isInTurning = true;
                }
                transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.right * 1.5f, turnSpeed * Time.deltaTime);
            }
            else
            {
                isInTurning = false;
                prePosition = transform.position;
                turnDir--;
            }
                
        }
        else if(dir < 0)
        {
            if (transform.position.x > -1.5f && Math.Abs(transform.position.x - StartX) < 1.5f)
            {
                if (!isInTurning)
                {
                    if (SoundsManager.Instance.SoundsOn)
                        SoundsManager.Instance.PlayAudioByName("slide");
                    playerState = PlayerState.TurnLeft;
                    isInTurning = true;
                }
                transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.left * 1.5f, turnSpeed * Time.deltaTime);
            }
            else
            {
                isInTurning = false;
                prePosition = transform.position;
                turnDir++;
            }
        }
    }

    void Damage()
    {
        shakeTimer += Time.deltaTime;
        if (shakeTimer % 0.2 > 0.1)
            render.enabled = false;
        else
            render.enabled = true;

        if(shakeTimer > 1.5f)
        {
            render.enabled = true;
            isInDamage = false;
            shakeTimer = 0;
            Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Obstacle"), false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (playerState == PlayerState.Dead)
            return;
        if ((collision.gameObject.tag == "Road" || collision.gameObject.tag == "Env" || collision.gameObject.tag == "BigEnv" || collision.gameObject.tag == "Vehicle") && 
            (playerState == PlayerState.Jump || playerState == PlayerState.DoubleJump || playerState == PlayerState.JumpDown) || playerState == PlayerState.InFlaot)
        {
            playerState = PlayerState.Runing;
            hasDoubleJump = false;
        }
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "BigObstacle")
        {
            if (gameObject.tag == "Player")
            {
                EventCenter.Instance.PostEvent<float, float>(EventType.CameraShake, 0.7f, 0.15f);
                
                if (SoundsManager.Instance.SoundsOn)
                    SoundsManager.Instance.PlayAudioByName("hit");

                if (collision.gameObject.tag == "BigObstacle")
                    PhotonManager.Instance.Request(OpCode.GameCode, GameCode.GetDamage, 3);
                else
                    PhotonManager.Instance.Request(OpCode.GameCode, GameCode.GetDamage, 1);
            }   
        }
        if(collision.gameObject.tag == "Vehicle")
        {
            if (collision.impulse.y < 0.2f && collision.impulse.y > -0.2f && collision.contacts[0].normal.y < 0.2f)
            {
                if (gameObject.tag == "Player")
                {
                    EventCenter.Instance.PostEvent<float, float>(EventType.CameraShake, 0.7f, 0.15f);

                    if (SoundsManager.Instance.SoundsOn)
                        SoundsManager.Instance.PlayAudioByName("hit");

                    if (collision.gameObject.tag == "BigObstacle")
                        PhotonManager.Instance.Request(OpCode.GameCode, GameCode.GetDamage, 3);
                    else
                        PhotonManager.Instance.Request(OpCode.GameCode, GameCode.GetDamage, 1);
                }
            }
        }
        
    }

    public void OnDestroy()
    {
        Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Obstacle"), false);
        Destroy(this.gameObject);
    }

}
