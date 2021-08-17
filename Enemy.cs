using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxSpeed;
    public float minHeight, maxHeight;
    public float damageTime = 0.5f;
    public int maxLife;
    public float attackRate = 1;
    public float nextAttack;
    public string enemyName;
    public AudioClip damageSound, deathSound;
    public bool isBoss = false;
    public static bool bossSpeech;

    private int currentLife;
    private float currentSpeed;
    private Rigidbody rb;
    private Animator anim;
    private bool faceRight = false;
    private Transform playerTarget;
    private bool isDead = false;
    private float zForce;
    private float walkTimer;
    private bool damaged = false;
    private float damageTimer;
    private AudioSource audioS;
    private MusicController musicC;
    private Player prayer;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerTarget = FindObjectOfType<Player>().transform;
        currentLife = maxLife;
        prayer = FindObjectOfType<Player>();
        audioS = GetComponent<AudioSource>();
        musicC = FindObjectOfType<MusicController>();
        if(isBoss){
            musicC.PlaySong(musicC.bossSong);
        }
    }

    void Update()
    {
        anim.SetBool("Dead", isDead);

        faceRight = (playerTarget.position.x < transform.position.x) ? false : true;
        if(faceRight){
            transform.eulerAngles = new Vector3(0,180,0);
        }else{
            transform.eulerAngles = new Vector3(0,0,0);
        }

        if(damaged && !isDead){
            damageTimer += Time.deltaTime;
            if(damageTimer >= damageTime){
                damaged = false;
                damageTimer = 0;
            }
        }

        if(Input.GetKeyDown(KeyCode.F)){
            anim.SetTrigger("Attack");
        }

        walkTimer += Time.deltaTime;
    }

    void FixedUpdate(){
        if(!isDead){
            Vector3 targetDistance = playerTarget.position - transform.position;
            float hForce = targetDistance.x / Mathf.Abs(targetDistance.x);

            if(walkTimer >= Random.Range(1f, 2f)){
                zForce = Random.Range(-1, 2);
                walkTimer = 0;
            }

            if(Mathf.Abs(targetDistance.x) < 1.5f){
                hForce = 0;
            }

            if(!damaged)
            rb.velocity = new Vector3(hForce * currentSpeed, 0, zForce * currentSpeed);
            
            anim.SetFloat("Speed", Mathf.Abs(currentSpeed));

            if(Mathf.Abs(targetDistance.x) < 2 && Mathf.Abs(targetDistance.z) < 2 && Time.time > nextAttack){
                anim.SetTrigger("Attack");
                currentSpeed = 0;
                nextAttack = Time.time + attackRate;
            }
        }

        rb.position = new Vector3(rb.position.x, rb.position.y, Mathf.Clamp(rb.position.z, minHeight, maxHeight));
    }

    public void Damage(int damage){
        if(!isDead){
            damaged = true;
            currentLife -= damage;
            anim.SetTrigger("HitDamage");
            PlaySong(damageSound);
            FindObjectOfType<UiManager>().UpdateEnemyUI(maxLife, currentLife, enemyName);
            if(currentLife <= 0){
                isDead = true;
                rb.AddRelativeForce(new Vector3(3, 5, 0), ForceMode.Impulse);
                PlaySong(deathSound);
                if(isBoss){
                    bossSpeech = true;
                    musicC.PlaySong(musicC.levelClearSong);
                    prayer.enabled = false;
                    DestroyAllObjects();
                }
            }
        }
    }

    public void DestroyEnemy(){
        gameObject.SetActive(false);
        Destroy(this.gameObject, 2f);
    }

    public void DestroyAllObjects(){
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag ("Enemy");
        for(var i = 0 ; i < gameObjects.Length ; i ++){
            Destroy(gameObjects[i], 1f);
        }
}

    void ResetSpeed(){
        currentSpeed = maxSpeed;
    }

    public void PlaySong( AudioClip clip){
        audioS.clip = clip;
        audioS.Play();
    }
    
}
