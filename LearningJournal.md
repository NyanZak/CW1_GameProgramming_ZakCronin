# Learning Journal

## 5/10/2021

**Movement**

During the scripting process for the movement I often had to tweak the values after playtesting them in the scene, this was because at multiple stages my player was either moving too quickly or not jumping high enough. This also happened when I later added the speed boost element to my movement system as when I initially added it my player would zoom to the other side of the level.

## 12/10/2021

**Score Manager Tutorial Used**

<a href="http://www.youtube.com/watch?feature=player_embedded&v=0zrZZN-QaDk
" target="_blank"><img src="http://img.youtube.com/vi/0zrZZN-QaDk/0.jpg" 
alt="TUTORIAL USED" width="240" height="180" border="10" /></a>

From this tutorial I learnt how to create and use playerpreferences to tie values to a player, this was something that I had struggled to replicate in my previous 2D game project. While the tutorial helped me create the majority of what I needed I had to make a couple of changes to it so that it could in work in my example game since the tutorial was a 2D endless runner meanwhile my example scene was a 2D platformer. Some of these changes included removing the start button at the beginning of the scene as well as making so the player trigger an object rather than jumping through an invisible box collider.

## 19/10/2021

**Camera Switch**

When setting up camera switching my cameras were transitioning too quickly to each other when toggling between the two. To fix this I had to go to the Main Camera and change the Ease In Out value until I was happy with the result.

## 26/10/221
**Player Switch Tutorial Used**

<a href="http://www.youtube.com/watch?feature=player_embedded&v=8oYUSep1qXo
" target="_blank"><img src="http://img.youtube.com/vi/8oYUSep1qXo/0.jpg" 
alt="TUTORIAL USED" width="240" height="180" border="10" /></a>

I did not have any problems following this tutorial however I had to change how the player switched the camera as I did not want the camera to be changed using OnMouseDown, instead I used a new Input in the Input Manager that I made which was binded to Q.

## 2/11/2021

**Game Controller**

After I made the script I forgot to attatch the game over screen compotent to the player controller which resulted in an error, to fix this I had to attatch the panel to gameobject slot on the game controller script. 
When writing down notes of how I created the Game Controller I was comparing the default values of objects such as Cameras, one of the objects I imported to see its base values was a panel, however I forgot to remove this once I was done (they were both white so it was hard to tell) so when I played the game and reached the game over score I could not press the buttons. I checked the script to see if it was timescale related, however after double clicking the screen it took me to the empty panel, so I simply deleted it and it started working again.
