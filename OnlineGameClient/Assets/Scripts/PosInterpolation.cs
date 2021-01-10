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

    
    public int nbPos;
    private float startTime;
    private bool firstPos = true;
    private Vector3 PreviousPos;
    private Queue<Vector3> Movements;
    private Transform tf;

    private void Awake()
    {
     
        tf = transform;
        startTime = 0.1f;
        Movements = new Queue<Vector3>();
        PreviousPos = tf.position;

    }
    private void Update()
    {

        if (Movements.Count > 0)
        {

            float fraction = (Time.time - startTime) / Time.fixedDeltaTime;
            
            //Debug.Log($"TRYING TO MOVE OTHER PLAYER, FRACTION IS {fraction}, {Movements.Count} POS INSIDE");

            if (fraction <= 1f && !firstPos)
            {
                
                switch (interpolationMode)
                {

                    case InterpolationTypes.Lerp:

                        tf.position = Vector3.Lerp(PreviousPos,Movements.Peek(), fraction);
    
                    break;
                    case InterpolationTypes.Slerp:

                        tf.position = Vector3.Slerp(PreviousPos, Movements.Peek(), fraction);
    
                    break;
                    
                }
                
            }
            else
            {

                if (firstPos) firstPos = false;
                PreviousPos = Movements.Dequeue();
                NewMovement();

            }

        }

    }

    //Set time-related variables when changing movement coordinates
    public void NewMovement()
    {
        
        startTime = Time.fixedTime;
        
    }

    public void AddMovement(Vector3 movement)
    {
        
        Movements.Enqueue(movement);
        //Debug.Log($"WE HAVE {Movements.Count} MOVEMENTS IN STOCK!!!"); //TODO Enable this debug to check if the queue risks overloading sometime, seems good for now

    }

}
