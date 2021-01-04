using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosInterpolation : MonoBehaviour
{

    public enum InterpolationTypes
    {
        
        Lerp, Slerp
        
    }
    
    public InterpolationTypes interpolationMode = InterpolationTypes.Lerp;


    public bool interpolating = false;
    private float startTime;
    private float endTime;
    private Vector3 initialPos;
    private Vector3 newPos;
    private Transform tf;
    //TODO faire les positions avec une Queue pour passer d'une position à l'autre

    private void Awake()
    {
     
        tf = transform;
        newPos = tf.position;

    }
    private void Update()
    {

        if (interpolating)
        {

            if (Time.time < endTime)
            {

                float fraction = (Time.time - startTime) / Time.fixedDeltaTime;
                //Debug.Log($"FIXEDDELTATIME IS {Time.fixedDeltaTime}");
                
                switch (interpolationMode)
                {
                
                    case InterpolationTypes.Lerp:

                        tf.position = Vector3.Lerp(initialPos,newPos, fraction);
                    
                        break;
                    case InterpolationTypes.Slerp:

                        tf.position = Vector3.Slerp(initialPos, newPos, fraction);
                    
                        break;
                
                }
                
                

            }
            else
            {

                tf.position = newPos;
                interpolating = false;

            }

        }
        
    }

    public void StartInterpolating(Vector3 newPos)
    {

        //set des variables pour starter l'interpolation dans Update
        if (newPos != tf.position)
        {
            
            startTime = Time.time;
            endTime = startTime + Time.fixedDeltaTime;
            
            initialPos = tf.position = this.newPos;
            this.newPos = newPos;
            interpolating = true;   
            
        }

    }

}
