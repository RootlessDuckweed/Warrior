using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackState : BaseState
{
    public override void LogicUpdate()
    {
        //Bocchi:������ҵĶ������߼�
        Collider2D facingCollider = Physics2D.OverlapCircle((Vector2)currentEnemy.transform.position + currentEnemy.chaseRadiusOffset,currentEnemy.stoppingDistance,currentEnemy.playerLayerMask);
        if (facingCollider != null)
        {   
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
                currentEnemy.transform.localScale = new Vector3(currentEnemy.currentFace, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
                currentEnemy.anim.SetTrigger("Attack");
                currentEnemy.canAttack = false;
                currentEnemy.moveable = false;
            }
        }
        //Debug.Log(currentEnemy.canAttack);
    }

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        //currentEnemy.anim.SetTrigger("Attack");
    }

    public override void OnExit()
    {
        //currentEnemy.anim.ResetTrigger("Attack");
    }

    public override void PhysicUpdate()
    {
        currentEnemy.rb.velocity = Vector2.zero;
        if (!currentEnemy.isDead && !currentEnemy.isHurt && currentEnemy.FoundPlayer() 
            && !currentEnemy.InAttackRange())
        {
            currentEnemy.SwitchState(State.CHASE);
        }
    }
}