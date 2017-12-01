using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{

    public GameObject normalHands;
    public GameObject fastHands;
    public GameObject megaHands;
    public GameObject enemy;
    public GameObject blueHUD;
    public GameObject pinkHUD;
    public GameObject violetHUD;
    public GameObject gameOverButtons;

    public Text healthText;
    public Text pinkText;
    public Text blueText;
    public Text violetText;
    public Text keyText;
    public Text secretText;
    public Text updateText;
    public Text endText;
    public Text enemyCountdown;
    public Text vibeText;
    public Text energyText;
    public Text scoreText;

    public Image smiley;
    public Image smileyCheck;
    public Image alien;
    public Image alienCheck;
    public Image pizza;
    public Image pizzaCheck;

    public Image pinkHand;
    public Image blueHand;
    public Image violetHand;
    public FlashScreen flashScreen;
    public int blueAmmo;
    public int pinkAmmo;
    public int violetAmmo;
    public int score = 0;
    public bool alienKey = false;
    public bool pizzaKey = false;
    public bool smileyKey = false;
    public bool isDead = false;
    public bool ending = false;
    public bool gameWon = false;

    public GameObject[] enemySpawnPoints;

    StartOptions startOp;
    BGColorSwitch bgColor;
    LightColorSwitch lightSwitch;
    WeaponSwitch weaponSwitch;
    PlayMusic playMusic;
    Text actionText;
    PlayerMovement playerMove;

    GameObject[] remainingEnemies;

    float timer;
    bool newUpdate = false;
    bool gameOver = false;

    void Awake()
    {
        enemySpawnPoints = null;
        remainingEnemies = null;

        actionText = GameObject.Find("ActionInfo").GetComponent<Text>();
        actionText.text = "";

        blueAmmo = fastHands.GetComponent<NormalHands>().ammoCount;
        pinkAmmo = normalHands.GetComponent<NormalHands>().ammoCount;
        violetAmmo = megaHands.GetComponent<MegaHands>().rocketsAmount;
        weaponSwitch = FindObjectOfType<WeaponSwitch>();
        playMusic = FindObjectOfType<PlayMusic>();
        lightSwitch = FindObjectOfType<LightColorSwitch>();
        bgColor = FindObjectOfType<BGColorSwitch>();
        startOp = FindObjectOfType<StartOptions>();
        playerMove = FindObjectOfType<PlayerMovement>();

        endText.enabled = false;
        enemyCountdown.enabled = false;

        smileyCheck.enabled = false;
        pizzaCheck.enabled = false;
        alienCheck.enabled = false;

        gameOverButtons.SetActive(false);

    }

    void Start()
    {
        StartCoroutine(GameStart());
    }

    void Update()
    {
        timer += Time.deltaTime;

        enemySpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawner");
        remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        scoreText.text = "SCORE:\n" + score.ToString();

        if (newUpdate == false && timer >= 3)
        {
            updateText.text = "";
            actionText.text = updateText.text;
        }
        else if (newUpdate == true && timer < 3 && !gameWon)
        {
            actionText.text = updateText.text;
        }

        if (weaponSwitch.selectedWeapon == 0)
        {
            pinkHUD.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            blueHUD.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            violetHUD.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            pinkHand.enabled = true;
            blueHand.enabled = false;
            violetHand.enabled = false;
        }
        else if (weaponSwitch.selectedWeapon == 1)
        {
            pinkHUD.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            blueHUD.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            violetHUD.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            pinkHand.enabled = false;
            blueHand.enabled = true;
            violetHand.enabled = false;
        }
        else if (weaponSwitch.selectedWeapon == 2)
        {
            pinkHUD.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            blueHUD.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            violetHUD.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            pinkHand.enabled = false;
            blueHand.enabled = false;
            violetHand.enabled = true;
        }

        if (alienKey == true && ending == false && gameOver == false)
        {
            alienCheck.enabled = true;
        }

        if (pizzaKey == true && ending == false && gameOver == false)
        {
            pizzaCheck.enabled = true;
        }

        if (smileyKey == true && ending == false && gameOver == false)
        {
            smileyCheck.enabled = true;
        }

        if (alienKey == true && pizzaKey == true && smileyKey == true && ending == false)
        {
            playMusic.PlaySelectedMusic(2);
            lightSwitch.dimmed = true;
            bgColor.dimmed = true;
            StartCoroutine(EndGame());
            AddPoints(100);
            ending = true;
        }

        if (ending == true)
        {
            enemyCountdown.text = "BADDIES LEFT: " + remainingEnemies.Length.ToString();
        }

        if (isDead == true && gameWon == false && gameOver == false)
        {
            gameOver = true;
            StartCoroutine(GameOver());
        }

        if (ending == true && remainingEnemies.Length == 0 && gameWon == false)
        {
            gameWon = true;
            playMusic.FadeFXDown(5f);
            StartCoroutine(GameWon());
            timer = 0;
        }

        if (gameWon == true)
        {
            timer += Time.deltaTime;

            playerMove.playerWalkingSpeed = 10f;
            playerMove.playerRunningSpeed = 15f;
            playerMove.jumpStrength = 5f;

            if (timer < 3)
            {
                weaponSwitch.selectedWeapon = 0;
            }
            else if (timer >= 3 && timer < 6)
            {
                weaponSwitch.selectedWeapon = 1;
            }
            else if (timer >= 6 && timer < 9)
            {
                weaponSwitch.selectedWeapon = 2;
            }
            else if (timer > 9)
            {
                timer = 0;
            }
        }

    }

    public void SpawnEnemy()
    {
        float num = Random.value;

        if (enemySpawnPoints.Length != 0 && num > 0.3f)
        {
            GameObject spawnInstance = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];

            EnemySpawner enSp = spawnInstance.GetComponent<EnemySpawner>();

            enSp.spawned = true;
            enSp.lerpTimer = 0;

            Instantiate(enemy, spawnInstance.transform.position, Quaternion.identity);

        }
    }

    public void DestroySpawner()
    {
        if (enemySpawnPoints.Length != 0)
        {
            GameObject spawnInstance = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];

            Destroy(spawnInstance);
        }

    }

    public void AddPoints(int points)
    {
        if (!isDead && !gameWon)
        {
            score += points;
        }
    }

    public void SecretHUD()
    {
        StartCoroutine(SecretHUDText());
    }

    public void HealthHUD()
    {
        StartCoroutine(HealthHUDText());
    }

    public void BlueHUD()
    {
        StartCoroutine(BlueHUDText());
    }

    public void PinkHUD()
    {
        StartCoroutine(PinkHUDText());
    }

    public void VioletHUD()
    {
        StartCoroutine(VioletHUDText());
    }

    public void KeyHUD()
    {
        StartCoroutine(KeyHUDText());
    }

    IEnumerator HealthHUDText()
    {
        updateText.text = healthText.text + "\n" + updateText.text;

        newUpdate = true;

        timer = 0;

        yield return new WaitForSeconds(3f);

        newUpdate = false;

    }

    IEnumerator KeyHUDText()
    {
        updateText.text = keyText.text + "\n" + updateText.text;

        newUpdate = true;

        timer = 0;

        yield return new WaitForSeconds(3f);

        newUpdate = false;

    }

    IEnumerator BlueHUDText()
    {
        updateText.text = blueText.text + "\n" + updateText.text;

        newUpdate = true;

        timer = 0;

        blueAmmo += 10;

        yield return new WaitForSeconds(3f);

        newUpdate = false;

    }

    IEnumerator PinkHUDText()
    {
        updateText.text = pinkText.text + "\n" + updateText.text;

        newUpdate = true;

        timer = 0;

        pinkAmmo += 5;

        yield return new WaitForSeconds(3f);

        newUpdate = false;

    }

    IEnumerator VioletHUDText()
    {
        updateText.text = violetText.text + "\n" + updateText.text;

        newUpdate = true;

        timer = 0;

        violetAmmo += 2;

        yield return new WaitForSeconds(3f);

        newUpdate = false;

    }

    IEnumerator SecretHUDText()
    {
        updateText.text = secretText.text + "\n" + updateText.text;

        newUpdate = true;

        timer = 0;

        yield return new WaitForSeconds(3f);

        newUpdate = false;

    }

    void DisableKeys()
    {
        pizza.enabled = false;
        smiley.enabled = false;
        alien.enabled = false;
        pizzaCheck.enabled = false;
        smileyCheck.enabled = false;
        alienCheck.enabled = false;
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1f);

        endText.text = "FIND\n THE THREE KEYS!";

        endText.enabled = true;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = false;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = true;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = false;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = true;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = false;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = true;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = false;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = true;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = false;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = true;

        yield return new WaitForSeconds(2f);

        endText.enabled = false;
        endText.text = "CRUSH\n ALL BAD VIBES!";

        yield return new WaitForSeconds(1f);

        endText.enabled = true;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = false;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = true;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = false;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = true;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = false;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = true;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = false;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = true;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = false;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = true;

        yield return new WaitForSeconds(2f);

        endText.enabled = false;
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.1f);

        playMusic.PlaySelectedMusic(4);
        playMusic.FadeFXDown(3f);

        yield return new WaitForSeconds(1f);

        endText.text = "BUMMER!\n YOU WIPED OUT.";
        endText.enabled = true;

        yield return new WaitForSeconds(4f);

        DisableKeys();
        vibeText.enabled = false;
        energyText.enabled = false;
        scoreText.enabled = false;
        flashScreen.gameOverScreen = true;

        yield return new WaitForSeconds(8f);

        endText.color = new Color(0, 0, 0, 1);
        endText.text = "TRY AGAIN?";
        gameOverButtons.SetActive(true);

    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2f);

        endText.text = remainingEnemies.Length.ToString() + " BADDIES LEFT!";

        DisableKeys();

        endText.enabled = true;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = false;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = true;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = false;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = true;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = false;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = true;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = false;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = true;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = false;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = true;

        yield return new WaitForSeconds(3f);

        endText.enabled = false;

        enemyCountdown.enabled = true;
    }

    IEnumerator GameWon()
    {
        yield return new WaitForSeconds(3f);

        playMusic.PlaySelectedMusic(3);

        endText.text = "BAD VIBES CRUSHED.\n SLICK MOVES!";

        endText.enabled = true;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = false;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = true;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = false;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = true;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = false;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = true;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = false;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = true;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = false;

        enemyCountdown.enabled = false;

        yield return new WaitForSeconds(0.3f);

        endText.enabled = true;

        yield return new WaitForSeconds(8f);

        vibeText.enabled = false;
        energyText.enabled = false;
        enemyCountdown.enabled = false;
        scoreText.enabled = false;

        //flashScreen.gameOverScreen = true;

        endText.text = "SCORE:\n" + score.ToString();

        yield return new WaitForSeconds(30f);

        startOp.LoadMenu();
    }

}
