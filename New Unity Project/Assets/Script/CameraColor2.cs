using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using com.cortastudios.DynamicColorCorrection;
using UnityEngine.Audio;

public class CameraColor2 : MonoBehaviour
{




    // Start is called before the first frame update
    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<ColorCurvesManager>().Factor = (Player.camCol2);
        GetComponent<ColorCurvesManager>().SaturationA = (2 + (-Player.camCol2));
        GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().volume= (0.3f + (Player.camCol2));
        GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().pitch = Mathf.Clamp((0.86f+(0.1f*Player.camCol2)),0.7f,1.2f);

        print(Player.heatlth2);

        //print(ColorCurvesManager);


    }
}
