using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(EnemyController controller) : base(controller)
    {
        // Attack -> Follow
        Transition transitionAttackToFollow = new Transition(
            isValid : () => {
                float distance = Vector3.Distance(
                    controller.Player.position,
                    controller.transform.position
                );
                if (distance >= controller.DistanceToAttack)
                {
                    return true;
                }else
                {
                    return false;
                }
            },
            getNextState : () => {
                return new FollowState(controller);
            }
        );

        Transitions.Add(transitionAttackToFollow);
    }


    public override void OnStart()
    {
        Debug.Log("Estado Attack: Start");
        //controller.rb.velocity = Vector3.zero;
        controller.agent.isStopped = true;
    }

    public override void OnUpdate()
    {
    }
    public override void OnFinish()
    {
        Debug.Log("Estado Attack: Finish");
    }
}
