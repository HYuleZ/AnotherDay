using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float maxSpeed = 4;
    public float jumpForce = 400;
    public float minHeight, maxHeight;
    public int maxLife = 10;
    public string playerName;
    public AudioClip damageSound, jumpSound;

    private int currentLife;
    private float currentSpeed;
    private Rigidbody rb;
    private Animator anim;
    private Transform groundCheck;
    private bool onGround;
    private bool isDead = false;
    private bool faceRight = true;
    private bool jump = false;
    private AudioSource audioS;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        currentSpeed = maxSpeed;
        currentLife = maxLife;
        audioS = GetComponent<AudioSource>();
    }

    void Update()
    {
        onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        anim.SetBool("OnGround", onGround);
        anim.SetBool("Dead", isDead);

        if(Input.GetButtonDown("Jump") && onGround){
            jump = true;
        }

        if(Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.K)){
            anim.SetTrigger("Attack");
        }
    }

    private void FixedUpdate(){
        if(!isDead){
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if(!onGround)
                v = 0;

            rb.velocity = new Vector3(h * currentSpeed, rb.velocity.y, v * currentSpeed);

            if(onGround){
                anim.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));
            }

            if(h > 0 && !faceRight){
                Flip();
            }else if(h < 0 && faceRight){
                Flip();
            }

            if(jump){
                jump = false;
                rb.AddForce(Vector3.up * jumpForce);
                PlaySong(jumpSound);
            }
            float minWidth = Camera.main.ScreenToWorldPoint(new Vector3(0,0,10)).x;
            float maxWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,10)).x;
            rb.position = new Vector3(Mathf.Clamp(rb.position.x, minWidth + 1, maxWidth - 1), rb.position.y, Mathf.Clamp(rb.position.z, minHeight, maxHeight));
        }
    }

    void Flip(){
        faceRight = !faceRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void ZeroSpeed(){
        currentSpeed = 0;
    }

    void ResetSpeed(){
        currentSpeed = maxSpeed;
    }

    public void Damage(int damage){
        if(!isDead){
            currentLife -= damage;
            anim.SetTrigger("HitDamage");
            FindObjectOfType<UiManager>().UpdateHealth(currentLife);
            PlaySong(damageSound);
            if(currentLife <= 0){
                isDead = true;
                FindObjectOfType<GameManager>().lives --;
                anim.SetTrigger("HitDamage");
                if(faceRight){
                    rb.AddForce(new Vector3(-3, 5, 0), ForceMode.Impulse);
                    PlayerRespawn();
                }else{
                    rb.AddForce(new Vector3(3, 5, 0), ForceMode.Impulse);
                    PlayerRespawn();
                }
            }
        }
    }
    public void PlaySong( AudioClip clip){
        audioS.clip = clip;
        audioS.Play();
    }

    void PlayerRespawn(){
        if(FindObjectOfType<GameManager>().lives > -1){
            isDead = false;
            FindObjectOfType<UiManager>().UpdateLifes();
            currentLife = maxLife;
            FindObjectOfType<UiManager>().UpdateHealth(currentLife);
            anim.Rebind();
            float minWidth = Camera.main.ScreenToWorldPoint(new Vector3(0,0,10)).x;
            transform.position = new Vector3(minWidth, 10, -4);
        }else{
            SceneManager.LoadScene("GameOver");
        }
    }
}
