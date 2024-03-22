using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DeathState : BaseState
{
    public override void LogicUpdate()
    {
        
    }

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.anim.SetTrigger("Death");
    }

    public override void OnExit()
    {
        currentEnemy.DestroyEnemy();
    }

    public override void PhysicUpdate()
    {
        
    }
}
