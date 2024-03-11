using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class EnemyState_Base
{
    public abstract void EnterState(EnemyController manager);

    public abstract void ExitState(EnemyController manager);

    public abstract void FrameUpdate(EnemyController manager);

    public abstract void PhysicsUpdate(EnemyController manager);

    public virtual void OnTriggerEnter(EnemyController manager, Collider other) { }

    public virtual void OnTriggerExit(EnemyController manager, Collider other) { }

    public virtual void OnPlayerSaw(EnemyController manager, Transform player) { }

    public virtual void OnSoundHeard(EnemyController manager, Vector3 pos) { }

    public virtual void OnPlayerClose(EnemyController manager, Transform player) { }

    public virtual IEnumerator Delay(EnemyController manager) { yield return null; }
}





public class EnemyState_Idle : EnemyState_Base
{
    public override void EnterState(EnemyController manager)
    {

    }

    public override void ExitState(EnemyController manager)
    {

    }

    public override void FrameUpdate(EnemyController manager)
    {

    }

    public override void PhysicsUpdate(EnemyController manager)
    {

    }

    public override void OnTriggerEnter(EnemyController manager, Collider other)
    {

    }

    public override void OnTriggerExit(EnemyController manager, Collider other)
    {
    }

    public override void OnPlayerSaw(EnemyController manager, Transform player)
    {
        manager.SwitchState("SEEK");
        manager.target = player.position;
    }

    public override void OnSoundHeard(EnemyController manager, Vector3 pos)
    {
        manager.SwitchState("INVESTIGATE");
        manager.target = pos;
    }

    public override void OnPlayerClose(EnemyController manager, Transform player)
    {
        manager.SwitchState("SEEK");
        manager.target = player.position;
    }
}





public class EnemyState_Patrol : EnemyState_Base
{
    Transform[] waypoints;
    Vector3 destination;
    int nextIndex = 0;
    bool doPatrol = true;

    public override void EnterState(EnemyController manager)
    {
        Debug.Log("do");
        manager.navAgent.speed = 3.5f;
        waypoints = manager.waypoints;
        nextIndex = Random.Range(0, waypoints.Length);
        destination = waypoints[nextIndex].position;
        manager.navAgent.destination = destination;
        doPatrol = true;
    }

    public override void ExitState(EnemyController manager)
    {

    }

    public override void FrameUpdate(EnemyController manager)
    {
        if (doPatrol)
        {
            Vector3 dis = manager.transform.position - destination;
            if (Vector3.SqrMagnitude(dis) < 2.5f)
            {
                destination = NextWaypoint(destination);
                manager.navAgent.destination = destination;
                Debug.Log("ddado");
            }
        }
    }

    public override void PhysicsUpdate(EnemyController manager)
    {

    }

    public override void OnTriggerEnter(EnemyController manager, Collider other)
    {

    }

    public override void OnTriggerExit(EnemyController manager, Collider other)
    {

    }

    public override void OnPlayerSaw(EnemyController manager, Transform player)
    {
        doPatrol = false;
        manager.SwitchState("SEEK");
        manager.target = player.position;
    }

    public override void OnSoundHeard(EnemyController manager, Vector3 pos)
    {
        doPatrol = false;
        manager.SwitchState("INVESTIGATE");
        manager.target = pos;
    }

    public override void OnPlayerClose(EnemyController manager, Transform player)
    {
        doPatrol = false;
        manager.SwitchState("SEEK");
        manager.target = player.position;
    }

    public Vector3 NextWaypoint(Vector3 currentPosition)
    {
        // Find array index of given waypoint
        for (int i = 0; i < waypoints.Length; i++)
        {
            // Once found calculate next one
            if (currentPosition == waypoints[i].transform.position)
            {
                // Modulus operator helps to avoid to go out of bounds
                // And resets to 0 the index count once we reach the end of the array
                nextIndex = (i + 1) % waypoints.Length;
            }
        }
        return waypoints[nextIndex].transform.position;
    }
}





public class EnemyState_Investigate : EnemyState_Base
{
    int numOfInvestigates;

    public override void EnterState(EnemyController manager)
    {
        manager.navAgent.destination = manager.target;
        manager.navAgent.speed = 5;
        manager.navAgent.autoBraking = true;
        numOfInvestigates = 0;
    }

    public override void ExitState(EnemyController manager)
    {

    }

    public override void FrameUpdate(EnemyController manager)
    {
        manager.navAgent.destination = manager.target;
        Vector3 dis = manager.transform.position - manager.target;
        if (Vector3.SqrMagnitude(dis) < 2.5f)
        {
            manager.target += (Vector3.right * Random.Range(-5f, 5f)) + (Vector3.forward * Random.Range(-5f, 5f));
            numOfInvestigates++;
            if (numOfInvestigates == 3)
                manager.SwitchState("PATROL");
        }
    }

    public override void PhysicsUpdate(EnemyController manager)
    {

    }

    public override void OnTriggerEnter(EnemyController manager, Collider other)
    {

    }

    public override void OnTriggerExit(EnemyController manager, Collider other)
    {

    }

    public override void OnPlayerSaw(EnemyController manager, Transform player)
    {
        manager.SwitchState("SEEK");
        manager.target = player.position;
    }

    public override void OnSoundHeard(EnemyController manager, Vector3 pos)
    {

    }

    public override void OnPlayerClose(EnemyController manager, Transform player)
    {
        manager.SwitchState("SEEK");
        manager.target = player.position;
    }
}





public class EnemyState_Seek : EnemyState_Base
{
    public override void EnterState(EnemyController manager)
    {
        manager.navAgent.autoBraking = false;
        manager.navAgent.speed = 8f;
    }

    public override void ExitState(EnemyController manager)
    {

    }

    public override void FrameUpdate(EnemyController manager)
    {
        manager.navAgent.destination = manager.currentPlayerTarget.position;
    }

    public override void PhysicsUpdate(EnemyController manager)
    {

    }

    public override void OnTriggerEnter(EnemyController manager, Collider other)
    {

    }

    public override void OnTriggerExit(EnemyController manager, Collider other)
    {

    }

    public override void OnPlayerSaw(EnemyController manager, Transform player)
    {

    }

    public override void OnSoundHeard(EnemyController manager, Vector3 pos)
    {

    }

    public override void OnPlayerClose(EnemyController manager, Transform player)
    {

    }
}




public class EnemyState_Kill : EnemyState_Base
{
    Coroutine delay;

    public override void EnterState(EnemyController manager)
    {
        manager.navAgent.isStopped = true;
        manager.StartDelay();
    }

    public override void ExitState(EnemyController manager)
    {

    }

    public override void FrameUpdate(EnemyController manager)
    {

    }

    public override void PhysicsUpdate(EnemyController manager)
    {

    }

    public override IEnumerator Delay(EnemyController manager)
    {
        yield return new WaitForSecondsRealtime(5f);
        manager.navAgent.isStopped = false;
        manager.SwitchState("PATROL");
    }
}