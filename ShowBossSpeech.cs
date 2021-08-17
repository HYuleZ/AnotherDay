using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBossSpeech : MonoBehaviour
{
    public GameObject bossSpeech;
    public GameObject boss1;
    private Player player;
    void Start()
    {        
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if(boss1 != null){
            if(Enemy.bossSpeech){
                StartCoroutine(WaitOneSecond(5));
                Enemy.bossSpeech = false;
            }
        }
    }

    IEnumerator WaitOneSecond(float sec){
        yield return new WaitForSeconds(sec);
        boss1.SetActive(true);
        bossSpeech.SetActive(true);
        player.enabled = false;

    }
}
