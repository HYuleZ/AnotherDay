using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));    
    }

    public void ChangeCar(){
        transform.Rotate(new Vector3(0,180,0));
        //Debug.Log("Rodando");
    }

    void OnCollisionEnter(Collision col){
        if(col.gameObject.name == "targetLeft" || col.gameObject.name == "targetRight"){
            ChangeCar();
        }
        //Debug.Log("Ta na hora de trocar");
    }


}
