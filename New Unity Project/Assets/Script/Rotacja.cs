﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacja : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.up * (Random.Range(1,10)*1.0f));
    }
}
