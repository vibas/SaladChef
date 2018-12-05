# SaladChef Unity Project 

Salad Chef is a cooking simulation, arcade style game local 2 player game made for PC, where each player has to control his/her respective chef and 
take order from customer and serve them proper salad in time. At the end of both player's timer game displays the winner. 

# Gameplay

Both the players have their respective timer running. In the mean time, customers will appear at counter and order a random salad. Customer will have also a waitig timer which is dependent on number of ingrediets. So players checks the order and moves to particular vegetable box to collect vegetable. Player can hold 2 vegetable max. If he/she is holding 2 vegetable, interaction with these vegetable boxes will be disabled. Now player has to carry required vegetable and reach at the chopping board and press the interaction key displayed on screen. Then Player starts chopping the vegetable. Player can not move, if he/she is chopping the vegetable. Once chopping is done, Pick Salad button will be displayed. If player has already all the required vegetables to the salad, and both of his/her hads are free, he can pick the salad in a plate and carry towards the proper customer counter. At customer counter, player has to press deliver key displayed on screen to deliver the salad to customer. Now Customer checks, if the delivered salad matchs with what he/she had ordered, then he/she becomes satisfied and pays the price of salad to the player. If delivery happens before 80% of timer is over, then customer gets impressed. And gives the player a powerup pick up which can be only collected by that player. Those powerups are 

* Time Booster - Increases player' timer
* Score Booster - Increases player's score
* Speed Booster - Increases player's movement speed

If customer's ordered salad and player's delivered salads are not matched, then customer gets angry. Customer's timer runs faster. Within that time if player can not deliver proper salad, then some score is deducted from that player. 


# Controls

Two players are there in game. Left is player 1 and right is player 2. Both have different control keys to move and interact with kitchen stuff and customer as well. For both the player there are scriptable objects called player configuration file inside Asset/GameConfiguration/Players. You can tweek them to change player behaviour and properties. 

Player 1 can move using A,S,D,W keys and interact with objects with E & Q keys
Player 2 can move using Right, Left, Up, Down keys and interact with objects with M & N keys

When ever any player reaches at any interactible object, inteaction key will be displayed. 
