using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Slider healthUI;
    public Text playerName;
    public Text livesText;

    public GameObject enemyUI;
    public Slider enemySlider;
    public Text enemyName;
    public float enemyUITime = 4f;

    private float enemyTimer;
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        healthUI.maxValue = player.maxLife;    
        healthUI.value = healthUI.maxValue;
        playerName.text = player.playerName;
        UpdateLifes();
    }

    void Update()
    {
        enemyTimer += Time.deltaTime;
        if(enemyTimer >= enemyUITime){
            enemyUI.SetActive(false);
            enemyTimer = 0;
        }
    }

    public void UpdateHealth(int qtd){
        healthUI.value = qtd;
    }

    public void UpdateEnemyUI(int maxHealth, int currentHealth, string name){
        enemySlider.maxValue = maxHealth;
        enemySlider.value = currentHealth;
        enemyName.text = name;
        enemyTimer = 0;
        enemyUI.SetActive(true);
    }

    public void UpdateLifes(){
        livesText.text = "x " + FindObjectOfType<GameManager>().lives.ToString();
    }
}
