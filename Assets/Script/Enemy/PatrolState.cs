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
        //Bocchi:改变敌人的朝向
        if(currentEnemy.moveable)
        {
            if (currentEnemy.check.isRightWall)
            {
                currentEnemy.currentFace = -currentEnemy.currentFace;
            }
            else
            {
               if (currentEnemy.check.isLeftWall)
               {
                   currentEnemy.currentFace = -currentEnemy.currentFace;
               }
            }
            if (currentEnemy.FoundPlayer()
                && currentEnemy.InAttackRange()
                && currentEnemy.attackerTransform != null && !currentEnemy.attackerTransform.GetComponent<PlayerController>().isDead)
            {
                currentEnemy.SwitchState(State.CHASE);
            }
            Move();
        }
    }
    //Bocchi:敌人移动
    public void Move()
    {
        currentEnemy.transform.localScale = new Vector3(currentEnemy.currentFace, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
        currentEnemy.rb.velocity = new Vector2(currentEnemy.currentFace * Time.deltaTime * currentEnemy.normalSpeed, 0);
    }
}