using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string[] speechTxt;
    public string actorName;
    public LayerMask playerLayer;
    private DialogueControl dc;
    bool ready;
    void Start()
    {
        dc = FindObjectOfType<DialogueControl>();
        ready = true;
    }

    void Update()
    {
        if(ready){
            dc.Speech(speechTxt, actorName);
            //Debug.Log("HA");
            ready = false;
        }
    }
}
