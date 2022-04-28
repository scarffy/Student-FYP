using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEnemy : MonoBehaviour
{
    [SerializeField] bool isAttacking;    

    public void Awake()
    {
            
    }

    private void Update()
    {

        if (isAttacking)
        {

            if (Input.GetMouseButtonDown(0))
            {

                Debug.Log("attack");

            }
        }


    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            isAttacking = true;
            Debug.Log("on range player attack");

        }        
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            isAttacking = false;
            Debug.Log("did not found monster");

        }
    }
}
