using System.Collections;
using System.Collections.Generic;
using FSMTest;
using UnityEngine;

public class PlayerIdleState : FSMTest.AState
{
    public override void Start(IStateRunner runner)
    {
        base.Start(runner);
    }

    public override void Update(IStateRunner runner)
    {
        //rigidBody.velocity = Vector3.zero;
        float vertTrans = Input.GetAxis("Vertical");
        float horTrans = Input.GetAxis("Horizontal");

        if (vertTrans != 0 || horTrans != 0)
        {
           // playerMoveState = PlayerMoveStates.Walking;
            //animator.SetTrigger("walkTrigger");
            return;
        }
    }

    public override void FixedUpdate(IStateRunner runner)
    {
        base.FixedUpdate(runner);
    }


}
