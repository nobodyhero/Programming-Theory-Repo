using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speedH = 2;
    public float speedV = 2;

    private float yaw = 0;
    private float pitch = 0;
    private float zoomAmount = 0;
    private float maxToClamp = 10;
    private float rotSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetMouseButton( 1 ) ) {
            HundleAngle();
        }

        HundleZoom();
    }

    private void HundleAngle() {
        yaw += speedH * Input.GetAxis( "Mouse X" );
        pitch -= speedV * Input.GetAxis( "Mouse Y" );

        transform.eulerAngles = new Vector3( pitch, yaw, 0.0f );
    }

    private void HundleZoom() {
        zoomAmount += Input.GetAxis( "Mouse ScrollWheel" );
        zoomAmount = Mathf.Clamp( zoomAmount, -maxToClamp, maxToClamp );
        var translate = Mathf.Min( Mathf.Abs( Input.GetAxis( "Mouse ScrollWheel" ) ), maxToClamp - Mathf.Abs( zoomAmount ) );
        transform.Translate( 0, 0, translate * rotSpeed * Mathf.Sign( Input.GetAxis( "Mouse ScrollWheel" ) ) );
    }
}
