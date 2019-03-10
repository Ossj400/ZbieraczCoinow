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
    public float speed = 3.5f;
    public float goToDistance = 55;
    public float attackDistance = 0;
    public Transform target;
    public string PlayerTag = "Player";
    public float attackTimer = 0.1f;
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
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit Buddy;

        if(Physics.Raycast(transform.position, fwd,out Buddy))
        {
          if (  Buddy.transform.tag != PlayerTag)
            {
                curState = State.LOOKFOR;
                return;
            }
        }


        if (Vector3.Distance(target.position, transform.position) > (attackDistance))
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            curTime = curTime - Time.deltaTime;
        
            if (curTime < 0 && Vector3.Distance(target.position, transform.position) < (attackDistance+1))
            {
                if (playerScript.health > 60)
                {
                    playerScript.health = playerScript.health - 5;
                }
                if (playerScript.health < 61 && playerScript.health > 27)
                {
                    playerScript.health = playerScript.health - 3;
                }
                if (playerScript.health < 28)
                {
                    playerScript.health--;
                }

                curTime = attackTimer;
                print("udany atak w czaSIE");
            }

        }
        else
        {
           
            curState = State.ATTACK;
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        }

    }


    void Attack()  //niepotrzebne
    {
        print("atakuje");
        
        transform.LookAt(target);
        curTime = curTime - Time.deltaTime;
            if(curTime < 0)
        {
            playerScript.health--; 
            curTime = attackTimer;
            print("udany atak w czaSIE");
        }

        if (Vector3.Distance(target.position, transform.position) > attackDistance)
        {
            curState = State.GOTO; 
        }
      


    }



}
