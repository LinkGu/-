using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "player input")]
public class PlayerInput : ScriptableObject, PlayerInputActions.IGameplayActions
{
    public event UnityAction<Vector2> onMove = delegate { };
    public event UnityAction onStopMove = delegate { };

    PlayerInputActions playerInputActions;

    private void OnEnable()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Gameplay.SetCallbacks(this); //注册本类
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    //关闭所有输入
    public void DisableAllInput()
    {
        playerInputActions.Gameplay.Disable();
    }

//开启输入
    public void EnableGameplayInput()
    {
        playerInputActions.Gameplay.Enable(); //操作玩家时候
        Cursor.visible = false; //隐藏鼠标游标
        Cursor.lockState = CursorLockMode.Locked; //鼠标的锁定状态为锁定
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) //输入动作的阶段已经执行的时候相当于input.GetKey()函数
        {
            onMove.Invoke(context.ReadValue<Vector2>()); //事件调用函数传入 输入动作所读取到的二维向量的值
        }
        
        if (context.phase == InputActionPhase.Canceled) //当键盘松开的时候
        {
            onStopMove.Invoke();
        }
    }
}