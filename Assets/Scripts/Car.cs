using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Vehicle
{
    public AudioClip hornAClip;

    private AudioSource hornASource;

    new void Awake() {
        speed = 5.0f;
        base.Awake();
    }

    new void Start() {
        base.Start();

        hornASource = gameObject.GetComponent<AudioSource>();
        hornASource.clip = hornAClip;
        StartCoroutine( Honk( 5.0f ) );
    }

    IEnumerator Honk( float waitTime ) {
        while ( gameObject.activeSelf ) {
            yield return new WaitForSeconds( waitTime );
            hornASource.Play();
        }
    }
}
