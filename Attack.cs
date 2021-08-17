using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col){
        Enemy enemy = col.GetComponent<Enemy>();
        Player player = col.GetComponent<Player>();
        if(enemy != null){
            enemy.Damage(damage);
        }

        if(player != null){
            player.Damage(damage);
        }
    }
}
