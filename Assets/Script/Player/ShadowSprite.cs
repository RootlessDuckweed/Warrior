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

    [Header("时间控制")]
    public float activeTime;  //显示时间
    public float activeStart; //开始显示时间

    private float alpha; //不断随着时间变化
    [Header("不透明度控制")]
    public float alphaSet; //不透明度的初始值
    public float alphaMutiplier; //以什么倍率衰减

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
