# Tutorial 1: Movement

## 1. Create a new scene
Start by creating a new scene, you can choose any name but it is best to name it something relevant

Add a square sprite by rightclicking in the hierarchy and going to 2D Object > Sprites > Square and give it the Player tag in the Inspector and rename it to Player in the hierarchy 
As we will be wanting to move this sprite we need to give it a Rigidbody2D, in the Rigidbody settings we will be turning the Collision Detection from Discrete to Continuous
We will also assign a Box Collider 2D to the square sprite to help with future scripts

Create another squad sprite and call it Ground in the hierarchy, move it underneath the player cube and add a box collider 2d to it so that the player will not fall into the void. I would recommend to scale this sprite on the x axsis so that the player has room to run around



## 2. Creating the movement script
With the player square sprite selected go to add component in the inspector and type in movementScript and create a new script and then open it up

To start we will be using serialized fields, this will allow us to edit the speed and jump number inside of the inspector, which will save us a lot of time and allow us to easily tweak the numbers incase the player jumps too high/low or moves too fast/slow.
We will also reference the rigidbody component that we added to the sprite here too, I have simply called it rb so that it's quicker to type out, again saving us a lot of time.

```
[SerializeField] private float speed;
[SerializeField] private float jumpForce;
Rigidbody2D rb;
```

Currently the value of rb is not assigned to anything so we must assign it to the rigidbody of our player so that we can actually move with it, by putting it in the start void it will also be assigned when the scene starts

```
Void Start()
{
rb = GetComponent<Rigidbody2D>();
}
```

In order to move left and right we will be getting the detection of the left/right and/or A/D keys, we will create a new float called'movX' which will hold the value of our X axis movement direction. Because we called referenced the rigidbody under the name rb we can simply type in rb.velocity instead of rigidbod2d.velocity, using the float we just created and our serialized field we can tell the player to move in a certain direction with a certain speed.



```
void Update()
    {
        float movX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movX * speed, rb.velocity.y);
        }
  ```      
  
To jump we will want to use an if statement. While still in the void update create a new line underneath the rb.velocity line and type in 'if' and press tab afterwards which will put brackets in for you. In the top brackets we will reference the key we want to jump with, for us we will be using the space key but any other key will work too we will also have an Add logic in this bracket too this simple usage of maths makes sure that we can only jump when we are grounded, otherwise the player could just jump all the time. In order to execute the jump we will push the player upwards using the jumpforce float we created at the beginning.
```
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Approximately(rb.velocity.y,0))
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }
```

Save the script and then head back to the scene, make sure the script has been attatched to the Player square. These are the values I have used for my character but depending on your scene length you may want to increase/decrease them

```
Speed: 4
Jump Force: 375
```


----------------



# Tutorial 2: Player Switch



# Tutorial 3: Camera Switch




# Tutorial 4: Game Controller




# Tutorial 5: Score Manager 


