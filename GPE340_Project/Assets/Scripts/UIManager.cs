using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager UIMInstance { get; private set; }
    [SerializeField]
    private HealthBar playerHealthBar;
    [SerializeField]
    private HealthBar enemyHealthBarPrefab;
    [SerializeField]
    private Transform enemyHealthBarContainer;
    [SerializeField]
    private Image weaponDisplay;
    [SerializeField]
    private Text lifeDisplay;
    [SerializeField]
    private Sprite emptyGunSprite;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject loseMenu;
    [SerializeField]
    private GameObject winMenu;


    void Awake ()
    {
        UIMInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.thePlayer)
        {
            if(GameManager.Instance.thePlayer.equippedWeapon)
            {
                weaponDisplay.overrideSprite = GameManager.Instance.thePlayer.equippedWeapon.gunIcon;
            }
            else
            {
                weaponDisplay.overrideSprite = emptyGunSprite;
            }

            lifeDisplay.text = string.Format("Lives: " + GameManager.Instance.playerLives);
        }
    }

    
    public void RegisterEnemy(Enemy enemy)
    {
        HealthBar healthBar = Instantiate (enemyHealthBarPrefab) as HealthBar;
        healthBar.transform.SetParent (enemyHealthBarContainer, false);
        healthBar.SetTarget (enemy.healthEnemy);
        healthBar.isTracking = true;
    }
    

    public void RegisterPlayer (Player player)
    {
        playerHealthBar.SetTarget (player.playerHealth);
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void ShowGameOver()
    {
        loseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    public void DeleteBars()
    {
        //delete healths
    }

    public void ShowWinMenu()
    {
        winMenu.SetActive(true);
    }
}