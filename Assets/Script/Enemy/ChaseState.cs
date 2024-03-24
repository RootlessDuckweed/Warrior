using Cainos.PixelArtPlatformer_VillageProps;
using System.Transactions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ChaseState:BaseState
{
    public ChaseState() 
    { }

    public override void LogicUpdate()
    {

    }

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.anim.SetBool("isChase",true);
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("isChase", false);
    }
    public override void PhysicUpdate()
    {
        Collider2D facingCollider = Physics2D.OverlapCircle((Vector2)currentEnemy.transform.position + currentEnemy.chaseRadiusOffset, 
                                    currentEnemy.chaseRadius, currentEnemy.playerLayerMask);
        if (facingCollider != null)
        {
            Attack();
            Chase(facingCollider);
        }
        else
        {
            currentEnemy.SwitchState(State.PATROL);
        }
    }
    //Bocchi:׷�������߼�
    protected void Chase(Collider2D facingCollider)
    {
        if(currentEnemy.moveable)
        {
            //Bocchi:ͨ����������ײ�����ı䳯��
            //Collider2D facingCollider = Physics2D.OverlapCircle((Vector2)currentEnemy.transform.position + currentEnemy.chaseRadiusOffset, currentEnemy.chaseRadius, currentEnemy.playerLayerMask);
            if (facingCollider != null)
            {
                if (facingCollider.gameObject.transform.position.x - currentEnemy.transform.position.x > 0f)
                {
                    currentEnemy.currentFace = 1;
                }
                else
                {
                    currentEnemy.currentFace = -1;
                }

            }
            currentEnemy.transform.localScale = new Vector3(currentEnemy.currentFace, currentEnemy.transform.localScale.y, 
                                                            currentEnemy.transform.localScale.z);
            currentEnemy.rb.velocity = new Vector2(currentEnemy.currentFace * Time.deltaTime * currentEnemy.chaseSpeed, 0);
        }
    }

    protected void Attack()
    {
        //Bocchi:������ҵĶ������߼�
        Collider2D facingCollider = Physics2D.OverlapCircle((Vector2)currentEnemy.transform.position + currentEnemy.chaseRadiusOffset,
                                                            currentEnemy.stoppingDistance, currentEnemy.playerLayerMask);
        if (facingCollider != null)
        {
            currentEnemy.moveable = false;
            currentEnemy.anim.SetBool("isChase", false);
            currentEnemy.rb.velocity = Vector2.zero;
            //Bocchi:����ʱת��
            if (facingCollider.gameObject.transform.position.x - currentEnemy.transform.position.x > 0f)
            {
                currentEnemy.currentFace = 1;
            }
            else
            {
                currentEnemy.currentFace = -1;
            }

            if (currentEnemy.canAttack)
            {
                //Bocchi:����ʱת��
                currentEnemy.transform.localScale = new Vector3(currentEnemy.currentFace, currentEnemy.transform.localScale.y,
                                                                currentEnemy.transform.localScale.z);
                currentEnemy.anim.SetTrigger("Attack");
                currentEnemy.canAttack = false;

            }
        }
        else
        {
            currentEnemy.anim.SetBool("isChase", true);
            currentEnemy.moveable = true;
        }
    }
}