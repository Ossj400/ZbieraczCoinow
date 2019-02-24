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
    public float attackDistance = 4;
    public Transform target;
    public string PlayerTag = "Player";


    // Start is called before the first frame update
    IEnumerator  Start()
    {
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
    }


    void Attack()
    {

    }


}
