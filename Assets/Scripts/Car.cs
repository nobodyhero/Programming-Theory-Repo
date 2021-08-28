using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [INHERITANCE]
// - This class inherits from the Vehicle class
public class Car : Vehicle
{
    public AudioClip hornAClip;

    private AudioSource hornASource;

    // [POLYMORPHISM]
    // - Overriding parent's Awake() and changing the default speed, then calling the parent's Awake()
    new void Awake() {
        speed = 5.0f;
        base.Awake();
    }

    // [POLYMORPHISM]
    // - Overriding parent's Start(), calling the parent's Start() and only the instance of this class honks in every 5 seconds
    new void Start() {
        base.Start();

        hornASource = gameObject.GetComponent<AudioSource>();
        hornASource.clip = hornAClip;
        StartCoroutine( Honk( 5.0f ) );
    }

    // [POLYMORPHISM]
    // - Update() is not overriden in this class so the parent's Update() is called for this class.


    IEnumerator Honk( float waitTime ) {
        while ( gameObject.activeSelf ) {
            yield return new WaitForSeconds( waitTime );
            hornASource.Play();
        }
    }
}
