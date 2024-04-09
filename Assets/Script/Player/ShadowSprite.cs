using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowSprite : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer playerSpriteRender;
    private SpriteRenderer thisSpriteRenderer;

    private Color color;

    [Header("ʱ�����")]
    public float activeTime;  //��ʾʱ��
    public float activeStart; //��ʼ��ʾʱ��

    private float alpha; //��������ʱ��仯
    [Header("��͸���ȿ���")]
    public float alphaSet; //��͸���ȵĳ�ʼֵ
    public float alphaMutiplier; //��ʲô����˥��

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
        playerSpriteRender = player.GetComponent<SpriteRenderer>();
        alpha = alphaSet;
        thisSpriteRenderer.sprite = playerSpriteRender.sprite;
        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;

        activeStart = Time.time;
    }

    private void FixedUpdate()
    {
        alpha*=alphaMutiplier;

        color = new Color(0.3f,0.3f,0.3f,alpha);
        thisSpriteRenderer.color = color;
        if (Time.time >= activeStart + activeTime)
        {
            //Return pool
            ShadowPool.Instance.Release(gameObject);
        }
        
    }
}
