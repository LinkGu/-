using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewport : Singleton<Viewport>
{
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private void Start()
    {
        Camera mainCamera = Camera.main;
        Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f)); //视口坐标转换为世界坐标的0,0
        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1f, 1f)); //视口坐标转换为世界坐标的0,0

        minX = bottomLeft.x;
        minY = bottomLeft.y;

        maxX = topRight.x;
        maxY = topRight.y;
    }

    //玩家可移动范围
    public Vector3 PlayerMoveablePosition(Vector3 playerposition,float paddingX,float paddingY)//paddingX paddingY为机身边距x y左右上下各超出的部分
    {
        Vector3 position = Vector3.zero;//初始化
        position.x = Mathf.Clamp(playerposition.x, minX+paddingX, maxX-paddingX);//限定移动范围 value的值在min和max之间
        position.y = Mathf.Clamp(playerposition.y, minY+paddingY, maxY-paddingY);
        return position;
    }
}