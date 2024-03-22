using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : BaseState
{
    public override void LogicUpdate()
    {
        
    }

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.moveable = false;
        currentEnemy.anim.SetTrigger("Hurt");
        currentEnemy.rb.AddForce(new Vector2(-currentEnemy.currentFace * currentEnemy.hurtForce, 0));
    }

    public override void OnExit()
    {
        currentEnemy.moveable = true;
        currentEnemy.isHurt = false;
        currentEnemy.rb.velocity=Vector2.zero;
    }

    public override void PhysicUpdate()
    {

    }
}
