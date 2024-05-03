using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public float checkRadius; //¼ì²â°ë¾¶
    public LayerMask layer;  //¼ì²âµÄ²ã¼¶
    [Header("Check Ground")]
    public Vector2 checkPointOffset_Ground; //Æ«ÒÆ
    public bool isGround; //ÊÇ·ñÔÚµØÃæ
    [Header("Check Air")]
    public Vector2 checkPointOffset_Air;
    public bool isAir; // ÊÇ·ñÔÚ¿ÕÖÐ
    [Header("Check Left Wall")]
    public Vector2 checkPointOffset_LeftWall; //Æ«ÒÆ
    public bool isLeftWall; //ÊÇ·ñ¿¿½ü×ó±ßÇ½Ìå
    [Header("Check Right Wall")]
    public Vector2 checkPointOffset_RightWall; //Æ«ÒÆ
    public bool isRightWall; // ÊÇ·ñ¿¿½üÓÒ±ßÇ½Ìå

    private void Awake()
    {
        
    }
    void Update()
    {
        Check();
    }

   
    /// <summary>
    /// ¼ì²â
    /// </summary>
    void Check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + checkPointOffset_Ground * transform.localScale.x, checkRadius, layer);
        isRightWall = Physics2D.OverlapCircle((Vector2)transform.position + checkPointOffset_RightWall, checkRadius, layer);
        isLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + checkPointOffset_LeftWall , checkRadius, layer);
        isAir = !Physics2D.OverlapCircle((Vector2)transform.position + checkPointOffset_Air * transform.localScale.x, checkRadius, layer);
    }
    /// <summary>
    /// »­³ö¼ì²â·¶Î§
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + checkPointOffset_Ground * transform.localScale, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + checkPointOffset_LeftWall, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + checkPointOffset_RightWall, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + checkPointOffset_Air * transform.localScale, checkRadius);
    }
}
