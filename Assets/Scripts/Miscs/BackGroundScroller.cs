using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
    [SerializeField] private Vector2 scrollVelocity;
    private Material material; //材质组件

    private void Awake()
    {
        material = GetComponent<Renderer>().material; //Renderer渲染器是使对象显示在屏幕上的工具。使用该类可以访问任何对象、网格或粒子系统的渲染器
    }

    private void Update()
    {
        material.mainTextureOffset += scrollVelocity * Time.deltaTime;//偏移
       // material.mainTextureScale += scrollVelocity * Time.deltaTime;//平铺
    }
}