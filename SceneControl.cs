using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public Animator anim;
    public GameObject transitionPhoda;

    private MusicController musicc;
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        musicc = FindObjectOfType<MusicController>();
        if(SceneManager.GetActiveScene().buildIndex > 1){
            ShowDay();
        }
    }

    void Update()
    {
        
    }
    public void TransitionScene(){
        transitionPhoda.SetActive(true);
        anim.SetTrigger("Go");
        Invoke("NextScene", 1f);
    }

    public void ShowDay(){
        transitionPhoda.SetActive(true);
        anim.SetTrigger("Show");
        StartCoroutine(HideSecond(5f));
    }

    public void NextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame(){
        Application.Quit();
    }
    public void BackMenu(){
        SceneManager.LoadScene("Menu");
        FindObjectOfType<GameManager>().lives = 3;
    }

    IEnumerator HideSecond(float sec){
        yield return new WaitForSeconds(sec);
        transitionPhoda.SetActive(false);
    }

    public void OnTriggerEnter(Collider col){
        if(col.gameObject.name == "Player"){
            musicc.PlaySong(musicc.levelClearSong);
            player.ZeroSpeed();
            Invoke("TransitionScene", 8f);
        }
    }
}
