﻿    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityStandardAssets.ImageEffects;

public class Player : MonoBehaviour
{


    
    public float speed = 10;
    public float gravity = 10;
    public float maxVelocityChange = 10;
    public float jumpHeight = 2;
    public float points = 0;
    public static float count;
    public static float camCol=1;
    public static float camCol2;
    public static float hpCol=1;
    public static float heatlth2;
    

    public Text countText;
    public Text winText;
    public AudioSource coinBing;
    public AudioSource backgroundMusic;




    public static float z;
    static float x;
        static float y;



    private Rigidbody _rigidbody;
    private bool grounded;

 
    public float health = 100; // player health 
    private Transform PlayerTransform;

    // Start is called before the first frame update
    void Start()
    {
    
        PlayerTransform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.freezeRotation = true;
        count = 0;
        countText.text = "Skradzione koiny : " + count.ToString();
        winText.text = "";

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        health = Mathf.Clamp(health, 0, 100);
        hpCol = (health / 100 );
        hpCol = Mathf.Clamp(hpCol, 0.01f, 2);
      //  if (hpCol > 0.01f)
      //  {
      //      hpCol = Mathf.Sqrt(hpCol);

       // }

        camCol =  Mathf.Sqrt(hpCol) + (count / 55);
        camCol = Mathf.Clamp(camCol, 0.01f, 2);
        camCol2 = camCol - 0.01f;
        camCol2 = Mathf.Clamp(camCol2, 0.01f, 2);

        if (camCol < 50)
        {
            hpCol = health / 55;
            camCol = (hpCol + count / 55);
        }

        heatlth2 = health;
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            PlayerTransform.Rotate(0,Input.GetAxis("Horizontal")*3, 0);

            Vector3 targetVelocity = new Vector3(0, 0, Input.GetAxis("Vertical"));
            targetVelocity = PlayerTransform.TransformDirection(targetVelocity);
            targetVelocity = targetVelocity * speed;
            Vector3 velocity = _rigidbody.velocity;
            Vector3 velocityChange = targetVelocity - velocity;
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            x = velocityChange.x;
            y = velocityChange.y;
            z = velocityChange.z;

            _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);



            if (Input.GetButton("Jump") && grounded == true)
            // if (Input.touchCount == 1 && grounded == true)
            {
                _rigidbody.velocity = new Vector3(velocity.x, CalculateJump(), velocity.z);
            }
        }
        
             else
             {
            // Player movement in mobile devices
            // Building of force vector 
            Vector3 movement = new Vector3(Input.acceleration.x, 0.0f, Input.acceleration.y);
            // Adding force to rigidbody
            _rigidbody.AddForce(movement * speed * Time.deltaTime);

            if(Input.touchCount == 1 && grounded == true)
            {
                _rigidbody.velocity = new Vector3(x, CalculateJump(),z);
            }
        }

        _rigidbody.AddForce(new Vector3(0, -gravity * _rigidbody.mass, 0));
        grounded = false;



    }
                      /*
    void Update()

    {
        if(health < 1)
        {
            Application.LoadLevel("111");

        }

    }
                     */


    float CalculateJump()
    {
        float jump = Mathf.Sqrt(2 * jumpHeight * gravity);
        return jump;
    }


    void OnCollisionStay()
    {
        grounded = true;    


    }

    void OnTriggerEnter(Collider Tomek)
    {
        if (Tomek.tag == "Coin")
        {
            PlayCoinBing();
            count = count + 1;
            countText.text = "Skradzione koiny : " + count.ToString();

            points = points + 5;
            Destroy(Tomek.gameObject);

            if (count > 43  )
            {
                winText.text = "Wszystkie Koiny Twoje!";
            }


            camCol = (hpCol + count / 10);


        }
      
    }


    void Hit(Collider Przeciwnik)
    {
       
        if(Przeciwnik.gameObject.tag == "Enemy")

            {
            
            print(hpCol);
            }
        
       
    }


    public void PlayCoinBing()
    {
        coinBing.Play();
    }
    public void PlayBackgroundMusic()
    {
        backgroundMusic.Play();
    }

}
