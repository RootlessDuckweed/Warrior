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
        currentEnemy.anim.SetBool("isChase", false);
        currentEnemy.moveable = true;
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

            //RootlessDuckweed:修改在前方无路或者是悬崖的转向
            if (currentEnemy.check.isAir)
            {
                currentEnemy.currentFace = -currentEnemy.currentFace;
            }

            if (currentEnemy.FoundPlayer())
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
        //RootlessDuckweed:修改无法下坠bug
        currentEnemy.rb.velocity = new Vector2(currentEnemy.currentFace * Time.deltaTime * currentEnemy.normalSpeed, currentEnemy.transform.position.y);
    }
}