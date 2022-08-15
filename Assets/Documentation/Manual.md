Score Manager System Guide
==================
This documentation describes how to use the `Score Manager` component in your project.

Behaviours
----------
-   \[`CameraSwitch`\]
-   \[`GameController`\]
-   \[`movementScript`\]
-   \[`playerController`\]

CameraSwitch
------------------------
This behaviour allows the user to switch the active camera.

### Properties
-   `cameraList` List of all the cameras in the scene.
-   `currentCamera` Allows the switching between cameras based on the current active camera.
    
### Script    
We create a reference to an empty gameobject in the scene that groups all the cameras. When the game starts we make sure that the default camera is the first camera in our gameobject's list.
    
```    
{
    public GameObject[] cameraList;
    private int currentCamera;
    void Start()
    {
        currentCamera = 0;
        for (int i = 0; i < cameraList.Length; i++)
        {
        }

        if (cameraList.Length > 0)
        {
            cameraList[0].gameObject.SetActive(true);
        }
    }
```
We then create a custom input so that the user can customise what button will be used for the switch. When the button is pressed the code checks to see whether it  has reached the end of the list, which when you begin will not have happened so you will switch to the second camera in the gameobject list.

```  
    void Update()
    {
        if (Input.GetButtonDown("Switch"))
        {
            currentCamera++;
            if (currentCamera < cameraList.Length)
            {
                cameraList[currentCamera - 1].gameObject.SetActive(false);
                cameraList[currentCamera].gameObject.SetActive(true);
            }
            else
            {
                currentCamera = 0;
                cameraList[currentCamera].gameObject.SetActive(true);
            }
        }
    }
}
```  

GameController
-------
This behaviour allows the user to manage the games current state.

### Properties
-   `gameOverScreen` Reference to our panel.
-   `playerScoree` - References the players current score.

### Script  
We reference the disabled panel so that once we reach the gameoverstate we can simply enable it. To make sure that the game runs properly after the user resets the game we make sure to set the timescale to 1 so that the game is not permanently paused.

```
    public GameObject gameOverScreen;
    public static int playerScore;

    private void Awake()
    {
        Time.timeScale = 1;
        gameOverScreen.SetActive(false);
    }
    
```
We tie the players score to the scoremanager, therefore we can then tell the game that once the player gets below zero to end the game, which then enables the gameover panel and pauses the game.

From this two functions were made for buttons on the gameoverpanel in order to either restart the scene and the players current score as well as a button to clear the players high score.

```
    private void Update()
    {
        playerScore = ScoreManager.score;
        if (ScoreManager.score == -1)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("ScoreManager");
        //gameOverScreen.SetActive(false);
        ScoreManager.instance.ResetScore();
    }

    public void ClearHighScore()
    {
        GetComponent<ScoreManager>().ClearHighScore();
    }
}
```

Movement Script
----------------------------------
These scripts allow the player to be able to move and have its movement effected by outside elements.

### Properties
-   `speed` - The regular walking speed of the player.
-   `jumpForce` - How quickly the player can jump.
-   `boostTime` - How long the player is boosted for.
-   `boostIncrease` - How much the players speed is increased by.

### Script 
For our player values we use floats as later we can multiply them in order to create the effect that we want.
We also start the game with the player not being boosted so that they are not incredibly fast all the time, we also reference the rigidbody that our player is using in order to change its velocity.
 
``` 
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float boostTime;
    [SerializeField] private float boostIncrease;
    private bool isBoostActivated = false;
    Rigidbody2D rb;

    void Start()
    {
            rb = GetComponent<Rigidbody2D>();
    }
```

In order to allow the player to move horizontally we simply reference the raw axis and for the vertical movement we ge tthe current horizontal movement and times it by the speed float that we have setup. This speed is then multiplied if the player happens to be boosted. For the jumping of the player we make sure that the player cannot spam the jump button otherwise it would ruin the game.

```
    void Update()
    {
        float movX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movX * speed, rb.velocity.y);
        if (isBoostActivated)
        {
            rb.velocity = new Vector2(movX * (speed + boostIncrease), rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Approximately(rb.velocity.y,0))
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }
```

In order to trigger the boosting of the player we reference trigger areas that we have put onto the coins so that when the player steps into them not only will it add to the score and delete the coin so that the player cannot farm points but that it'll trigger the players boost for the amount that we set it for. Another If statement is also setup so that if the player was to collide with an enemy instead of a coin that it would minus the players score while not deleting the enemy.

```
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
        {
            Debug.Log("Trigger");
            ScoreManager.instance.AddScore();
            Destroy(other.gameObject);
            if (!isBoostActivated)
            {
                Debug.Log("BOOST");
                isBoostActivated = true;
                Invoke("EndBoost", boostTime);
            }
        }

        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy");
            ScoreManager.instance.MinusScore();
        }
    }
    private void EndBoost()
    {
        isBoostActivated = false;
    }
}
```

Player Controller
-------------

This behaviour allows the user to switch between multiple playable objects.

### Properties
-   `Players` Reference to the list of objects the player can switch to.
-   `CurrentPlayer` References the object that the player is currently controlling.

### Script 

First of all we use a list similar to the one used for the cameras, instead using it to group all of the objects/sprites the player can switch between. We set the current player to the beginning of the list which is 0 and we also disable the movementscript for all the objects which will allow us to only be able to move one object at a time.
```
   public GameObject[] Players;
    [SerializeField] public GameObject CurrentPlayer;

    void Start()
    {
        for (int i = 1; i < Players.Length; i++)
        {
            Players[i].GetComponent<movementScript>().enabled = false;
        }
        CurrentPlayer = Players[0];
    }
```
If our custom input is pressed we then search for the other object in the list and set that to the player which ends up as the new currentplayer, enabling its movement script so that we dont just have two sprites that we cannot move while disabling the movement script on the old currentplayer.

```
    void Update()
    {
        if (Input.GetButtonDown("Switch"))
        {
            GameObject otherPlayer = null;
            foreach (GameObject player in Players)
            {
                if (player != CurrentPlayer)
                {
                    otherPlayer = player;
                    break;
                }
            }
            ChangePlayer(otherPlayer);
            CurrentPlayer.GetComponent<movementScript>().enabled = true;
        }
    }

        public void ChangePlayer(GameObject player)
        {
            CurrentPlayer.GetComponent<movementScript>().enabled = false;
            CurrentPlayer = player;
        }
 }
```
