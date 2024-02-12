using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState_Base
{
    public virtual void EnterState(GameManager manager)
    {

    }

    public virtual void ExitState(GameManager manager)
    {

    }

    public virtual void FrameUpdateState(GameManager manager)
    {

    }

    public virtual void PhysicsUpdateState(GameManager manager)
    {

    }

}
