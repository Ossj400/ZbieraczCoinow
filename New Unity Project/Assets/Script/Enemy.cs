using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum State
    {
        LOOKFOR,
        GOTO,
        ATTACK
    }
    public State curState;
    public float speed = .5f;
    public float goToDistance = 13;
    public float attackDistance = 1;
    public Transform target;
    public string PlayerTag = "Player";
    public float attackTimer = 2;
    private float curTime;
    private Player playerScript;


    // Start is called before the first frame update
    IEnumerator  Start()
    {
        target = GameObject.FindGameObjectWithTag(PlayerTag).transform;
        curTime = attackTimer;

        if (target != null)
        {
            playerScript = target.GetComponent<Player>();
        }

        while (true)
        {
            switch(curState)
            {
                case State.LOOKFOR:
                    LookFor();
                    break;
                case State.GOTO:
                    GoTo();
                    break;
                case State.ATTACK:
                    Attack();
                    break;
            }
            yield return 0;
        }
    }

    void LookFor()
    {

        print("Hi we are in lookfor");

        if (Vector3.Distance(target.position, transform.position)< goToDistance)
        {
            curState = State.GOTO;
        }

    }


    void GoTo()
    {
        print("Hi we are in goto");
        transform.LookAt(target);
        if (Vector3.Distance(target.position, transform.position) > attackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            curState = State.ATTACK;
        }

    }


    void Attack()
    {

        transform.LookAt(target);
        curTime = curTime - Time.deltaTime;
            if(curTime < 0)
        {
            playerScript.health--; 
            curTime = attackTimer;
        }

        if (Vector3.Distance(target.position, transform.position) > attackDistance)
        {
            curState = State.GOTO; 
        }
      


    }



}
