using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public float checkRadius; //检测半径
    public LayerMask layer;  //检测的层级
    [Header("Check Ground")]
    public Vector2 checkPointOffset_Ground; //偏移
    public bool isGround; //是否在地面
    [Header("Check Air")]
    public Vector2 checkPointOffset_Air;
    public bool isAir; // 是否在空中
    [Header("Check Left Wall")]
    public Vector2 checkPointOffset_LeftWall; //偏移
    public bool isLeftWall; //是否靠近左边墙体
    [Header("Check Right Wall")]
    public Vector2 checkPointOffset_RightWall; //偏移
    public bool isRightWall; // 是否靠近右边墙体
    [Header("CheckPlayerDeadLayer")]
    public LayerMask playerDeadLayer;  //检测的层级
    public bool isPlayerDead; // 是否站在玩家死亡身体上面
    private void Awake()
    {
        
    }
    void Update()
    {
        Check();
    }

   
    /// <summary>
    /// 检测
    /// </summary>
    void Check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + checkPointOffset_Ground * transform.localScale.x, checkRadius, layer);
        isRightWall = Physics2D.OverlapCircle((Vector2)transform.position + checkPointOffset_RightWall, checkRadius, layer);
        isLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + checkPointOffset_LeftWall , checkRadius, layer);
        isAir = !Physics2D.OverlapCircle((Vector2)transform.position + checkPointOffset_Air * transform.localScale.x, checkRadius, layer);
        isPlayerDead = Physics2D.OverlapCircle((Vector2)transform.position + checkPointOffset_Ground * transform.localScale.x, checkRadius, playerDeadLayer);
    }
    /// <summary>
    /// 画出检测范围
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + checkPointOffset_Ground * transform.localScale, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + checkPointOffset_LeftWall, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + checkPointOffset_RightWall, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + checkPointOffset_Air * transform.localScale, checkRadius);
    }
}
