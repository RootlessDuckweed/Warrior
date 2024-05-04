using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public float checkRadius; //���뾶
    public LayerMask layer;  //���Ĳ㼶
    [Header("Check Ground")]
    public Vector2 checkPointOffset_Ground; //ƫ��
    public bool isGround; //�Ƿ��ڵ���
    [Header("Check Air")]
    public Vector2 checkPointOffset_Air;
    public bool isAir; // �Ƿ��ڿ���
    [Header("Check Left Wall")]
    public Vector2 checkPointOffset_LeftWall; //ƫ��
    public bool isLeftWall; //�Ƿ񿿽����ǽ��
    [Header("Check Right Wall")]
    public Vector2 checkPointOffset_RightWall; //ƫ��
    public bool isRightWall; // �Ƿ񿿽��ұ�ǽ��
    [Header("CheckPlayerDeadLayer")]
    public LayerMask playerDeadLayer;  //���Ĳ㼶
    public bool isPlayerDead; // �Ƿ�վ�����������������
    private void Awake()
    {
        
    }
    void Update()
    {
        Check();
    }

   
    /// <summary>
    /// ���
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
    /// ������ⷶΧ
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + checkPointOffset_Ground * transform.localScale, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + checkPointOffset_LeftWall, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + checkPointOffset_RightWall, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + checkPointOffset_Air * transform.localScale, checkRadius);
    }
}
