using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehavior : MonoBehaviour
{
    //transform of the gameobject you want to shake
    private Transform myTransform;
    //desired duration of shake effect
    private float shakeDuration = 0f;
    //measure of magnitude for the shake. tweak on preference
    private float shakeMagnitude = 0.7f;
    //measure how quickly the shake effect should evaporate
    private float dampingSpeed = 1.0f;
    //the initial position of the gameobject
    Vector3 initialPosition;

     void Awake()
    {
        if (myTransform == null)
        {
            myTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        initialPosition = myTransform.localPosition;   
    }
    
    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            myTransform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            myTransform.localPosition = initialPosition;
        }
    }

    public void TriggerShake()
    {
        shakeDuration = 2.0f;
    }
}
