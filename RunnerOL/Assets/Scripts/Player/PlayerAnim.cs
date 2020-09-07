using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animation anim;
    PlayerControl pc;
    AnimationState curState;
    float AnimTimer = 0;
    float moveAnimTime = 0.4f;
    float rollAnimTime = 0.37f;
    float jumpAnimTime = 0.4f;
    float jumpDownAnimTime = 0.44f;
    bool inRolling = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        pc = gameObject.GetComponent<PlayerControl>();
        
    }

    // Update is called once per frame
    void Update()
    {
        curState = GetCurState();
        if (!ProgressSetting.Instance.gameStart)
        {
            if (curState != null)
                curState.speed = 0;
            return;
        }
        else
        {
            if (curState != null && curState.speed <0.1f)
                curState.speed = 1.0f;

        }
        if(inRolling && pc.playerState != PlayerControl.PlayerState.Rolling)
        {
            inRolling = false;
            EventCenter.Instance.PostEvent(EventType.RollingEnd);
        }
        AnimControl();
    }

    void PlayerAnimByName(string animName)
    {
        AnimTimer = 0;
        anim.CrossFade(animName);
    }

    bool IsPlaying(string animName)
    {
        return anim.IsPlaying(animName);
    }

    AnimationState GetCurState()
    {
        foreach(AnimationState state in anim)
        {
            if (IsPlaying(state.name))
                return state;
        }
        return null;
    }

    void AnimControl()
    {
        switch (pc.playerState)
        {
            case PlayerControl.PlayerState.Runing:
                if (!IsPlaying("Run"))
                    PlayerAnimByName("Run");
                break;
            case PlayerControl.PlayerState.TurnRight:
                if (!IsPlaying("TurnRight"))
                    PlayerAnimByName("TurnRight");
                AnimTimerControl(moveAnimTime);
                break;
            case PlayerControl.PlayerState.TurnLeft:
                if (!IsPlaying("TurnLeft"))
                    PlayerAnimByName("TurnLeft");
                AnimTimerControl(moveAnimTime);
                break;
            case PlayerControl.PlayerState.Rolling:
                if (!IsPlaying("Roll"))
                {
                    inRolling = true;
                    PlayerAnimByName("Roll");
                }
                AnimTimerControl(rollAnimTime);
                break;
            case PlayerControl.PlayerState.Jump:
                if (!IsPlaying("JumpUp"))
                    PlayerAnimByName("JumpUp");
                AnimTimerControl(jumpAnimTime);
                break;
            case PlayerControl.PlayerState.DoubleJump:
                PlayerAnimByName("JumpUp");
                AnimTimerControl(jumpAnimTime);
                break;
            case PlayerControl.PlayerState.JumpDown:
                if (!IsPlaying("JumpDown"))
                    PlayerAnimByName("JumpDown");
                AnimTimerControl(jumpDownAnimTime);
                break;
            case PlayerControl.PlayerState.InFlaot:
                if (!IsPlaying("JumpLoop"))
                    PlayerAnimByName("JumpLoop");
                break;
            case PlayerControl.PlayerState.Dead:
                if (!IsPlaying("Dead"))
                    PlayerAnimByName("Dead");
                break;
        }
    }

    void AnimTimerControl(float animTime)
    {
        AnimTimer += Time.deltaTime;
        if (AnimTimer > animTime)
        {
            if (pc.playerState == PlayerControl.PlayerState.Jump || pc.playerState == PlayerControl.PlayerState.DoubleJump || pc.playerState == PlayerControl.PlayerState.JumpDown)
            {
                pc.playerState = PlayerControl.PlayerState.InFlaot;
            }
            else
            {
                pc.playerState = PlayerControl.PlayerState.Runing;
            }

            AnimTimer = 0;
        }
    }

}
