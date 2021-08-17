using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float xMargin = 1f;
    public float xSmooth = 8f;
    public float ySmooth = 8f;
    public Vector2 maxXandY;
    public Vector2 minXandY;

    private Transform thePlayer;

    private void Awake()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private bool CheckXMargin(){
        return (transform.position.x - thePlayer.position.x) < xMargin;
    }

    private void Update(){
        TrackPlayer();
    }

    private void TrackPlayer(){
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if(CheckXMargin()){
            targetX = Mathf.Lerp(transform.position.x, thePlayer.position.x, xSmooth * Time.deltaTime);
        }

        targetX = Mathf.Clamp(targetX, minXandY.x, maxXandY.x);

        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }
}
