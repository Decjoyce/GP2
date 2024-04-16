using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    #region singleton
    public static EnemyController instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of EnemyController found");
            return;
        }
        instance = this;

        navAgent = GetComponent<NavMeshAgent>();
    }
    #endregion singleton

    public NavMeshAgent navAgent { get; private set; }

    EnemyState_Base currentState;
    public EnemyState_Idle state_idle = new EnemyState_Idle();
    public EnemyState_Patrol state_patrol = new EnemyState_Patrol();
    public EnemyState_Seek state_seek = new EnemyState_Seek();
    public EnemyState_Investigate state_investigate = new EnemyState_Investigate();
    public EnemyState_Kill state_kill = new EnemyState_Kill();
    public EnemyStates enemyState;

    public VisionTrig visTrig;
    public Vector3 target;
    public Transform currentPlayerTarget;
    public Transform[] waypoints;

    //[SerializeField] float hearRadius;
    [SerializeField] float hearThreshold;

    // Start is called before the first frame update
    void Start()
    {

        currentState = state_patrol;
        currentState.EnterState(this);
    }

    private void OnEnable()
    {
        GameManager.instance.OnLoadGameData_Gameplay += LoadEnemy;
        GameManager.instance.OnSaveGameData_Gameplay += SaveEnemy;
    }

    private void OnDisable()
    {
        GameManager.instance.OnLoadGameData_Gameplay -= LoadEnemy;
        GameManager.instance.OnSaveGameData_Gameplay -= SaveEnemy;
    }

    public void LoadEnemy()
    {
        if(GameManager.instance.gd_gameplay.beenInit)
            transform.position = GameManager.instance.gd_gameplay.pos_Monster;
    }

    public void SaveEnemy()
    {
         GameManager.instance.gd_gameplay.pos_Monster = transform.position;
    }

    private void Update()
    {
        currentState.FrameUpdate(this);
    }

    public void SwitchState(string newState)
    {
        currentState.ExitState(this);

        switch (newState)
        {
            case "IDLE":
                currentState = state_idle;
                enemyState = EnemyStates.idle;
                break;
            case "PATROL":
                currentState = state_patrol;
                enemyState = EnemyStates.patrol;
                break;
            case "SEEK":
                currentState = state_seek;
                enemyState = EnemyStates.seek;
                break;
            case "INVESTIGATE":
                currentState = state_investigate;
                enemyState = EnemyStates.investigate;
                break;
            case "KILL":
                currentState = state_kill;
                enemyState = EnemyStates.kill;
                break;
            default:
                Debug.LogError("ERROR: State - " + newState + " - does not exist");
                return;
        }

        currentState.EnterState(this);
    }

    public void PlayerClose(Transform player)
    {
        currentState.OnPlayerClose(this, player);
        currentPlayerTarget = player;
    }

    public void SawPlayer(Transform player)
    {
        currentState.OnPlayerSaw(this, player);
        currentPlayerTarget = player;
    }

    public void SoundHeard(Vector3 pos, float volume)
    {
        float dis = Vector3.SqrMagnitude(transform.position - pos);

        float intensity = (volume / (4 * Mathf.PI * (dis * dis))) * 10000000;

        Debug.Log(intensity);

        if(intensity >= hearThreshold)
        {
            currentState.OnSoundHeard(this, pos);
        }
    }

    public void StartDelay()
    {
        StartCoroutine(currentState.Delay(this));
    }

}

public enum EnemyStates
{
    idle, 
    patrol,
    seek,
    investigate,
    kill,
}