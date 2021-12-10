# Tutorial 1: Movement

## 1. Create a new scene
Start by creating a new scene, I have called this scene `ScoreManager` as that is our component however you can choose any name but it is best to name it something relevant

Add a square sprite by rightclicking in the hierarchy and going to 2D Object > Sprites > Square and give it the `Player` tag in the Inspector and rename it to Player in the hierarchy 
As we will be wanting to move this sprite we need to give it a `Rigidbody2D`, in the Rigidbody settings we will be turning the Collision Detection from Discrete to Continuous
We will also assign a `BoxCollider2D` to the square sprite to help with future scripts

Create another squad sprite and call it Ground in the hierarchy, move it underneath the player cube and add a `BoxCollider2D` to it so that the player will not fall into the void. I would recommend to scale this sprite on the x axis so that the player has room to run around

Create a circle sprite and make it yellow, give it a `RigidBody2D` and a circle collider, make sure Is Trigger is ticked. Create a new tag and call it `Coin`. Place a couple of these around your level, these will be our coins.

Create a Isometric Diamond sprite and give it a `BoxCollider2D`, making sure Is Trigger is ticked. Create a new tag and call it `Enemey`. Place a couple of these on the floor of your level, these will be our spikes. 

## 2. Creating the movement script
With the player square sprite selected go to add component in the inspector and type in `movementScript` and create a new script and then open it up.
>We will be using both Start and Update voids so do not delete them.

To start we will be using serialized fields, this will allow us to edit the speed and jump number inside of the inspector, which will save us a lot of time and allow us to easily tweak the numbers incase the player jumps too high/low or moves too fast/slow.
I am also adding a speedboost that the player recieves once they pickup a coin, we will control the time they are boosted for and how much their speed is increased. To determine whether the playing is being boosted or not we will make a isBoostActivated bool, being false as default as we will want to enable it later.
We will also reference the rigidbody component that we added to the sprite here too, I have simply called it rb so that it's quicker to type out, again saving us a lot of time.

```
[SerializeField] private float speed;
[SerializeField] private float jumpForce;
[SerializeField] private float boostTime;
[SerializeField] private float boostIncrease;
private bool isBoostActivated = false;
Rigidbody2D rb;
```
>We will be using both Start and Update voids so do not delete them.

Currently the value of rb is not assigned to anything so we must assign it to the rigidbody of our player so that we can actually move with it, by putting it in the start void it will also be assigned when the scene starts.

```
Void Start()
{
rb = GetComponent<Rigidbody2D>();
}
```

In order to move left and right we will be getting the detection of the left/right and/or A/D keys, we will create a new float called `movX` which will hold the value of our X axis movement direction. Because we called referenced the rigidbody under the name rb we can simply type in rb.velocity instead of rigidbod2d.velocity, using the float we just created and our serialized field we can tell the player to move in a certain direction with a certain speed.



```
void Update()
    {
        float movX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movX * speed, rb.velocity.y);
        }
  ```      
  
To jump we will want to use an if statement. While still in the void update create a new line underneath the rb.velocity line and type in `if` and press tab afterwards which will put brackets in for you. In the top brackets we will reference the key we want to jump with, for us we will be using the space key but any other key will work too we will also have an Add logic in this bracket too this simple usage of maths makes sure that we can only jump when we are grounded, otherwise the player could just jump all the time. In order to execute the jump we will push the player upwards using the jumpforce float we created at the beginning.
```
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Approximately(rb.velocity.y,0))
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }
```

Create a void OnTriggerEnter2D with the tags we made we can determine what the player has collided with for now if we collided with a coin we will have a trigger debuglog and destroy the coin gameobject, inside we have another if statement which will enable the boost. Staying in the same void we will creater another check to see if the player has collided with an enemey and if they have the console will put out a enemy debug log

```
private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
        {
            Debug.Log("Trigger");
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
        }
    }
```
Finally we just need to create another void so that the boost gets disabled after the boosttime

```
   private void EndBoost()
    {
        isBoostActivated = false;
    }
  ```  

Save the script and then head back to the scene, make sure the script has been attatched to the Player square. These are the values I have used for my character but depending on your scene length you may want to increase/decrease them.

Name | Value |
--- | --- |
Speed | 4 |
Jump Force | 375 |
Boost Time | 2 |
Boost Increase | 2 |

---


# Tutorial 2: Player Switch

## 1. Scene Setup

For us to be able to switch between two players we will of course need another player, simple duplicate the current player square and move it next to the other one, you can rename them to Player1 and Player2 to make things easier. I would also recommend changing the sprite colours so you can easily tell them apart, I have chosen Blue for Player1 and Red for Player2.

Now create an empty gameobject, I will be calling this `PlayerController`. Because we have two players we don't want to have every script appear twice which is why we will be putting on this new empty gameobject.


## 2. Creating the Player Switching script

Create a new script on the `PlayerController` in this scenario I have just called it playerController.

First of all we need to have access to our player gameobjects since we are not using the script on the players themselves. First of all we will create a list where we can add more or remove a gameobject from the list, we will also create a field that will tell us who the active player is, which in this case we will be using Player1 as the default current player.

```
public GameObject[] Players;
[SerializeField] public GameObject CurrentPlayer;
```
>We will be using both Start and Update voids so do not delete them.

First of all we create a new void and call it ChangePlayer, this will assign the currentplayer field to the player who is currently active. We will be disabling the players movement by looking at the current player and turning its movement script off.

```
public void ChangePlayer(GameObject player)
        {
            CurrentPlayer.GetComponent<movementScript>().enabled = false;
            CurrentPlayer = player;
        }
```

In our start void we wil create a loop, it is only a short loop since we will only be having two players, normally the int i value would be 0 but because we want to start with our first player we will put 1 which also means that the movement script for player 1 will not be disabled when we play the scene

```
 void Start()
    {
        for (int i = 1; i < Players.Length; i++)
        {
            Players[i].GetComponent<movementScript>().enabled = false;
        }
        CurrentPlayer = Players[0];
    }
```

To switch between our two players we will simply use the input of a button, we will be using Unitys `Input Manager` to do so. Save the current script you have, there will be errors if you try to play the scene which we will fix shortly. Go back to the scene view and go to Edit > Project Settings, from there head to the `Input Manager`, click on the arrows by Axes and it will show you every current Input including Horizontal which we used in our movement script, increase the size by 1 and go to the bottom you will see that it has created a duplicate Debug Horizontal, click the arrow next to the duplicated one and rename it to Switch, from there we will clear the negative button as we will not be needing it, and for the positive button you will set this to the key you want to use in order to switch players, for this I have used Q, finally at the bottom make sure the Type is set to Key or Mouse Button instead of Joystick otherwise Unity will not read the input.

Go back to the script and put Switch in the brackets with speech marks, Unity will then look to see how many gameobjects we have in the list and sets the the player that isn't the current player as the other player. Finally we will make it so that the current player will have its movement script enabled.

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
```
Save the script and go back to the scene view, if you were to play the scene you'd still have erros that's because we need to assign the players on the player controller, click on the playercontroller gameobject and you will see `Players` with an arrow and number as well as `Current Player` we do not need to drag anything into the current player as Unity will automatically asign it due to our script, however we will need to change some stuff with the `Players` first of all change the number on the right to 2, as this is the number of players we have, next click the arrw and you will have two fields, drag Player1 from the hierarchy into Element 0 and Player2 into Element 1, if you then click play you should be able to move around, and then pressing Q will allow you to switch control from Player 1 to Player 2 and vice versa. 


# Tutorial 3: Camera Switch

## 1. Scene Setup

In order to switch our camera positions we are going to be using Unity's built in Cinemachine package, this can be accessed and installed by going to Window > PackageManager going to Unity Registry and finding Cinemachine on the left hand side or typing in the search bar at the top right and then clicking install. If installed correctly you will notice a Cinemachine tab at the top of Unity.

Go to the Cinemachine tab and you will want to create a Virtual Camera, once this is done you'll notice that your Main Camera will now have a CinemachineBrain component, the only setting we will change is at the default blend changing the value to 0.5 seconds, this value determines how smoothly the camera will transition, and this value may have to be adjusted depending on how far your cameras are from each other.
Now go to your CM vcam 1 and look at the CinemachineVirtualCamera component attatched to it, you will want to change the body to Framing Transposer and assign Player1 to the Follow and Look At values, scroll down to the Aim and set it to Do nothing as all the settings we will change ourselves. In the Damping section under Body change the X and the Y Damping to 0.75 this changes how smoothly the camera will transition across those axis, we also will want to increase the screen Y so that we can see more above us, for my character I have set the Screen Y to 0.77. This camera will be for our Player 1 and will be centered the player, however we need another camera for our second Player. Duplicate the CM vcam and then drag Player2 into the Follow and Look At Values, I would rename the duplicate camera to CM vCam2, I'd also recommend to position the cameras slightly above the players so that you cannot see below the ground, you can edit the tracked object offset if you do not want the camera to be centered however I will be keeping mine centered.


## 2. Creating the Camera Switching script
Click back onto the `Player Controller` game object and create a new script on it, call it cameraSwitch
>We will be using both Start and Update voids so do not delete them.

Similarly to what we did with the players we will be creating a list to drag our cameras into as well as defining what our current active camera is.

```
public GameObject[] cameraList;
private int currentCamera;
```
In our start void function we will also be creating a loop similar to the player script however this time instead of disabling a movementscript we will be setting the camera as being active.


```
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
In our void update we will be making use of the Switch Input that we created for the player, when we switch from Player 1 to Player 2 we will simply be disabling the Player 1 Camera and enabling the Player 2 Camera, this is just one of many ways you could do this, you could also program to set the priorty of the a cinemachine camera to be higher than the other which would also create a similar effect.

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

Save the script and then as we did for the Players make sure the Camera List is set to 2 and add the Player1 Camera to Element 0 and Player 2 Camera to Element 1. In order to test this properly I would recommend spreading out the two players and their cameras so that when you load the scene you cannot immediately see the other player.


# Tutorial 4: Score Manager 
 
## 1. Scene Setup
With the `Score Manager` we will be using UI, because of this we are going to install another Unity package called TextMeshPro, this can be installed the same way we installed Cinemachine or by creating a Text UI element and picking the TMPRO option which will ask as to install it, we will only need the TMP Essentials. Create TMPRO Text element and that will auto create a Canvas, click on the Canvas and change the Canvas Scaler from Constant Pixel Size to Scale With Screen Size, I will be using 1920x1080 however you can choose a suitable resolution for you. Position the Text we just created to the top left of the screen, rename it from Text (TMP) to ScoreLabel and change the Text Input to SCORE: ,I would also recommend making the font bold and increasing the font size to 48, duplicate the ScoreLabel and move it after the first SCORE: colon, rename it from ScoreLabel(1) to ScoreValue and change the text input to 0. Duplicate both of these texts and move them to the right hand side, change ScoreValue(1) to HighScoreValue and then for the new ScoreLabel(1) remame it to HighScoreLabel and in the text input change the text to HIGHSCORE: , you will have to increase the width of the Rect Transform so that HIGHSCORE: appears on one line.


## 2. Creating the Score Manager.
Go to your `PlayerController` and create a new script on it called scoreManager and open it up.
We wont need the Start and Update voids for this script so you can simply delete them.
Because we are only using the text that comes from the TMPro package we don't actually need to reference the UnityEngine.UI, however we do need to reference the TMPRO package.

```
using TMPro;
```
We will instance the ScoreManager so that other scripts can use it as we have many global variables.
We will then create ints for our score and highscore so that we can increase/decrease the number and then we will create a reference to the score and highscore value texts we just created.

```
public static ScoreManager instance;
public static int score, highScore;
public TMP_Text scoreText, highScoreText;
```

Create a new void called Awake, every time we load the scene Unity will check to see if the player has played before and retrieve its highscore and display it instead of the 0 on our highscore value which we will set later
    
```
private void Awake()
    {
       instance = this;

        if (PlayerPrefs.HasKey("HighScore"))
      {
           highScore = PlayerPrefs.GetInt("HighScore");
           highScoreText.text = highScore.ToString();
       }
    }
```

In order for us to change the current scores value we will create two publc voids, one for adding score and one for taking away the score, these will both add/minus 1

```
public void AddScore()
    {
        score++;
        UpdateHighScore();
        scoreText.text = score.ToString();
    }

    
public void MinusScore()
    {
        score --;
        UpdateHighScore();
        scoreText.text = score.ToString();
    }
```

Next we will make it so that when our score is higher than the highscore it will become the new highscore

```
public void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = highScore.ToString();
           PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
```

This void will be useful for the next tutorial if you want to play the game again while still in the game, otherwise when you enter the game view for the first time the score will always be reset.
    
```
public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }
```

Another option for us is to clear the highscore, this is useful after playtesting to make sure everything is working correctly. 

```
public void ClearHighScore()
    {
       PlayerPrefs.DeleteKey("HighScore");

        highScore = 0;
        highScoreText.text = highScore.ToString();
    }
}
```
Save the script and then go to the Score Manager on the Player Controller object, drag the ScoreValue onto the Score Text and the HighScoreValue onto the High Score Text.

Since we now have our score system set up we can make our coins and spikes do something now, head back to the movement script in the OnTriggerEnter2D void, after where we deploy the trigger debug we can now instance the ScoreManager and refer to the AddScore void we made, simillary after the enemy debug we can now instance the MinusScore void.

```
private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
        {
            Debug.Log("Trigger");
           // ScoreManager.instance.AddScore();
            Destroy(other.gameObject);
        }

        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy");
           // ScoreManager.instance.MinusScore();
        }
    }
```

# Tutorial 5: Game Controller
## 1. Scene Setup
Right click the Hierarchy and create a UI Panel, I've decided to make the color less transparent than the default values to 241, but it is up to personal preference. On this panel create a TMPRO text place it in the top center and in the text input put GAME OVER. Then on the panel create 2 TMPro Buttons and place them in the center underneath the game over text, rename one of the buttons to ClearHighScoreButton and change its text input to CLEAR HIGHSCORE and similarly rename the other button to PlayAgain and change its text input to PLAY AGAIN



## 2. Creating the Game Controller

In order for us to "reset" the level we will just be reloading the scene, because of this we will need to put that we are using Unity's scenemanagement at the top

```
using UnityEngine.SceneManagement;
```

In order for the panel to appear we need to have a reference to it so it will appear when we need it, we want it when the player reaches a score below 0 so we will need to find the players score, we will do this by creating a static int

```
    public GameObject gameOverScreen;
    public static int playerScore;
```
When we start the scene we want to guarantee that it isn't frozen since we will be messing with time, so we will be setting the timescale to 1. We also dont want to see the panel until we say that the game is over so we disable the panel
```
private void Awake()
    {
        Time.timeScale = 1;
        gameOverScreen.SetActive(false);
    }
```
As before we can reference our scoremanager in other scripts we will use this to make the player score equal to the current score, and then when the players score is below 0 it will active the panel and freeze time

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
    
``` 
To restart our scene we will simply load the currentscene, since we put that we are using unitys scenemanager we can simple just load the scene again, make sure to replace the name in the brackets with the scenes name correctly. If the player is to restart then we will also need to reset the score which we can do with the ResetScore void we made in our `ScoreManager` earlier. Because our panel on awake is disabled we do not need to do anything about it
```
public void Restart()
    {
        SceneManager.LoadScene("ScoreManager");
        ScoreManager.instance.ResetScore();
    }
```    
Lastly  we will create a void to clear our highscore, which we will just reference our `ScoreManager` with the same void name

```    
public void ClearHighScore()
    {
        GetComponent<ScoreManager>().ClearHighScore();
    }
```
Save the script and head back to the scene, before we press play we must assign the Game Over Screen to our Game Controller script on the `PlayerController`, simple just drag the pane we made into the value. Now if you press play everything should be working correctly.

While not being a part of the tutorial I would highly recommend to clean up the Hierarchy
For example I have multiple empty gameobjects named which I use to indivudally store Cameras, Level Assets, Canvases and Coins/Spikes/Players. It would also be worth prefabbing the GameOverPanel, Players and PlayerControllerGameObject to easily use in other scenes.
