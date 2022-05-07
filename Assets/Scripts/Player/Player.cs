using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float moveSpeed = 10f; //速度
    [SerializeField] private float paddingY = 0.4f; //paddingX paddingY为机身原点开始的边距x y左右上下各超出的部分
    [SerializeField] private float paddingX = 1f;
    [SerializeField] private float accelerateTime = 0.2f; //加速度/按按键开始加速,越小加速度越快
    [SerializeField] private float slowDownTime = 3f; //减速度/放开按键开始减速,越小减速度越快
    private Coroutine moveCoroutine;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _input = ScriptableObject.CreateInstance<PlayerInput>(); //拖入PlayerInput组件
    }

    private void Start()
    {
        _rigidbody2D.gravityScale = 0f; //重力缩放为0
        _input.EnableGameplayInput(); //游戏一启动的动作表就激活
    }


    private void OnEnable()
    {
        _input.onMove += Move;
        _input.onStopMove += StopMove;
    }

    private void OnDisable()
    {
        _input.onMove -= Move;
        _input.onStopMove -= StopMove;
    }

    private void StopMove()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        //_rigidbody2D.velocity = Vector2.zero;
        StopCoroutine(MovePositionLimitCoroutine());
        moveCoroutine = StartCoroutine(MoveCoroutine(slowDownTime, Vector2.zero));
    }

    private void Move(Vector2 moveinput)
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        //_rigidbody2D.velocity = moveinput * moveSpeed;
        StartCoroutine(MovePositionLimitCoroutine());
        moveCoroutine = StartCoroutine(MoveCoroutine(accelerateTime, moveinput.normalized * moveSpeed));
    }

//移动限制协程
    IEnumerator MovePositionLimitCoroutine()
    {
        while (true)
        {
            transform.position = Viewport.Instance.PlayerMoveablePosition(transform.position, paddingX, paddingY);
            yield return null;
        }
    }

//物体运动加速
    IEnumerator MoveCoroutine(float time, Vector2 moveVelocity)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.fixedDeltaTime/time;//Time.fixedDeltaTime是一个固定值
            _rigidbody2D.velocity = Vector2.Lerp(_rigidbody2D.velocity, moveVelocity, t); //参数一1,想要改变的值,参数二:想要达到的值,参数三:参数1和2之间按参数3进行线性插值
            yield return null;
        }
    }
}