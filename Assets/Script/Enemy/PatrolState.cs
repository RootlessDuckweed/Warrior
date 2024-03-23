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
        //Bocchi:�ı���˵ĳ���
        if(currentEnemy.moveable)
        {
            if (currentEnemy.check.isRightWall)
            {
                currentEnemy.currentFace = -currentEnemy.currentFace;
            }
        }
    }
    //Bocchi:�����ƶ�
    public void Move()
    {
        currentEnemy.transform.localScale = new Vector3(currentEnemy.currentFace, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
        currentEnemy.rb.velocity = new Vector2(currentEnemy.currentFace * Time.deltaTime * currentEnemy.normalSpeed, 0);
    }
}