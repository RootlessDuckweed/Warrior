using UnityEngine;

public class PatrolState : BaseState
{
    public PatrolState()
    {
    }

    public override void LogicUpdate()
    {

    }

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.anim.SetBool("isWalk",true);
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("isWalk",false);
    }

    public override void PhysicUpdate()
    {
        if(currentEnemy.currentFace>0f)
        {
            if (currentEnemy.check.isRightWall)
            {
                currentEnemy.currentFace = -currentEnemy.currentFace;
            }
        }
        else
        {
           if (currentEnemy.check.isLeftWall)
           {
               currentEnemy.currentFace = -currentEnemy.currentFace;
           }
        }
        
    }
}