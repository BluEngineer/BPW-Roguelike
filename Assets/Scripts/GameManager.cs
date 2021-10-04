using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float timeScale;
    public int money;

    public int dungeonLayer;
    //public TestGun gun;

    public GameObject playerObject;
    public bool playerDead;
    public int playerHealth = 100;


    private float timeElapsed;
    public float lerpDuration;

    public Animator quadAnimator;

    public AudioSource audioSource;
    public AudioClip gameOverSound;
    public AudioClip enterPortalSound;
    public AudioClip exitPortalSound;
    public AudioClip heartPickupSound;

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(gameObject);
        //SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {

        if(playerDead && Input.GetKeyDown(KeyCode.Space))
        {
            ResetGame();
        }

        if(playerHealth <= 0 && !playerDead)
        {
            EventManager.Invoke(EventType.GAME_OVER);
            StartCoroutine(PlayerDead());
        }
    }


    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //DungeonGeneration.DungeonGenerator.Instance.GenerateDungeon();
        //maak UI en link deze shit eraan
        //moneyText = GameObject.Find("Score").GetComponent<Text>();
        //deadText = GameObject.Find("restarttext").GetComponent<Text>();
        //damageText = GameObject.Find("DamageText").GetComponent<Text>();
        //staminaBar = GameObject.Find("StaminaBar").GetComponent<Slider>();
        //playerHealthbar = GameObject.Find("HealthBar").GetComponent<Slider>();

    }

    public IEnumerator PlayerDead()
    {
        playerDead = true;
        quadAnimator.SetTrigger("playerDead");
        audioSource.PlayOneShot(gameOverSound);

        yield return new WaitForSeconds(3f);
        
    }

    public void ResetGame()
    {
        DungeonGeneration.DungeonGenerator.Instance.ResetDungeon();
        dungeonLayer = 1;
        if (GunManager.Instance.currentGun)
        {
            GunManager.Instance.currentGun.DropWeapon();
            GunManager.Instance.currentGun = null;
        }

        playerDead = false;
        UImanager.Instance. playerHealthBar.maxValue = 100;
        money = 0;
        playerHealth = (int)UImanager.Instance.playerHealthBar.maxValue;
        quadAnimator.SetTrigger("resetAnim");
        SceneManager.LoadScene(1);

    }



}
