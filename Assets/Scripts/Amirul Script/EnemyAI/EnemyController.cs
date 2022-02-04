using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Movement state controller for enemy
/// Consist of 5 state for now: 
/// patrol, chase, returnposition, attack, dead
/// </summary>

namespace FYP.NPC.EnemyAI
{
    public class EnemyController : MonoBehaviour
    {
        public enum ENEMY_STATE
        {
            IDLE,
            PATROL, //Not sure whether idle or patrol
            CHASE,
            RETURNPOSITION,
            ATTACK,
            DEAD
        }

        public ENEMY_STATE CurrentState
        {
            get { return currentState; }

            set
            {
                //keep track of previous state
                //if (currentState != ENEMY_STATE.INTERRUPT)
                //    PreviousState = currentstate;

                //Update current state
                currentState = value;

                //Stop all running coroutines
                StopAllCoroutines();

                switch (currentState)
                {
                    case ENEMY_STATE.IDLE:
                        StartCoroutine(EnemyIdle());
                        break;

                    case ENEMY_STATE.PATROL:
                        StartCoroutine(EnemyPatrol());
                        break;

                    case ENEMY_STATE.CHASE:
                        StartCoroutine(EnemyChasing());
                        break;

                    case ENEMY_STATE.RETURNPOSITION:
                        StartCoroutine(EnemyReturnPosition());
                        break;

                    case ENEMY_STATE.ATTACK:
                        StartCoroutine(EnemyAttacking());
                        break;

                    case ENEMY_STATE.DEAD:
                        StartCoroutine(EnemyDead());
                        break;
                }

            }
        }

        public void ChangeEnemyState(ENEMY_STATE state)
        {
            CurrentState = state;
        }

        #region Variable

        public ENEMY_STATE currentState = ENEMY_STATE.PATROL;

        NavMeshAgent agent;

        [SerializeField] Material enemyColor;
        [SerializeField] List<Transform> destpoints;
        [SerializeField] int numDestpoints = 0;
        [SerializeField] bool isEntering;
        [SerializeField] Collider playerCollider = null;

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            ChangeEnemyState(ENEMY_STATE.PATROL);
            enemyColor.color = Color.green;

        }

        // Update is called once per frame
        void Update()
        {

        }

        public IEnumerator EnemyIdle()
        {
            enemyColor.color = Color.grey;

            while(currentState == ENEMY_STATE.IDLE)
            {

                NextDest();

                yield return new WaitForSeconds(2.0f);
                ChangeEnemyState(ENEMY_STATE.PATROL);

                yield return null;
            }

            yield return null;
        }

        public IEnumerator EnemyPatrol()
        {
            enemyColor.color = Color.grey;
            MoveSpeed(2.0f);

            while (currentState == ENEMY_STATE.PATROL)
            {

                
                agent.SetDestination(destpoints[numDestpoints].position);

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    ChangeEnemyState(ENEMY_STATE.IDLE);
                    
                }

                if (isEntering)
                {
                    //enemyColor.material.SetColor("_Color", Color.yellow);

                    enemyColor.color = Color.yellow;
                    MoveSpeed(1.0f);

                    yield return new WaitForSeconds(1.0f);
                    ChangeEnemyState(ENEMY_STATE.CHASE);
                }

                yield return null;
            }
            yield return null;
        }

        public IEnumerator EnemyChasing()
        {

            enemyColor.color = Color.red;
            MoveSpeed(3.0f);

            while (currentState == ENEMY_STATE.CHASE)
            {
                //enemyColor.material.SetColor("_Color", Color.red);

                if (isEntering)
                {
                    agent.SetDestination(playerCollider.transform.position);
                }
                else
                {
                    ChangeEnemyState(ENEMY_STATE.RETURNPOSITION);
                }

                yield return null;
            }

            yield return null;
        }

        public IEnumerator EnemyReturnPosition()
        {
            enemyColor.color = Color.grey;
            MoveSpeed(2.5f);

            while( currentState == ENEMY_STATE.RETURNPOSITION)
            {

                agent.SetDestination(destpoints[numDestpoints].position);

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    ChangeEnemyState(ENEMY_STATE.PATROL);

                }

                if (isEntering)
                {
                    ChangeEnemyState(ENEMY_STATE.CHASE);
                }

                yield return null;
            }

            yield return null;
        }

        public IEnumerator EnemyAttacking()
        {
            yield return null;
        }

        public IEnumerator EnemyDead()
        {
            enemyColor.color = Color.black;     //change color to black
            agent.isStopped = true;             //ai is stopped when dead

            while( currentState == ENEMY_STATE.DEAD)
            {
                //Wait a bit then destroy object
                yield return new WaitForSeconds(5.0f); 
                Destroy(this.gameObject);

                yield return null;
            }

            yield return null;
        }

        #region Functions

        //detect when player enter range
        public void PlayerEnter(Collider value)
        {
            if (value.CompareTag("Player"))
            {
                //assign the collider to new variable then ai refer to the collider
                //enable ai chasing player
                playerCollider = value;
                isEntering = true;
                Debug.Log("True");
            }           

        }

        //detect when player exit range
        public void PlayerExit(Collider value)
        {
            if (value.CompareTag("Player"))
            {
                //disable ai chasing player
                isEntering = false;
                Debug.Log("False");
            }
        }

        //for changing to next waypoint
        public void NextDest()
        {
            if(numDestpoints == destpoints.Count - 1)
                numDestpoints = 0;
            else
                numDestpoints++;
        }

        //control speed of ai moving
        public void MoveSpeed(float speed)
        {
            agent.speed = speed;
        }

        //private void OnDrawGizmosSelected()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(transform.position, lookRadius);
        //}
        #endregion
    }
}
