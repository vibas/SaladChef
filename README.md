# SaladChef Unity Project 

This project is made with Unity 2018.2.11f1. 

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


# Project Details
There in onle one scene "GamePlay" scene where whole game works. In Game Scene "Kitchen" GameObject holds whole kitchen, interactible objects like - Veg Box, Chopping Board, Extra Plate, Trash Bin, Customer Counter etc. 
GameManager GameObject inside scene contains GameManager class which controls all the game flow throuch various sub controller classes. 
For Example - PlayerSpawnManager manages spawing players and holdng them in a list and adding and removing from list etc... Now player manager's instance is there in GameManager class. And GameManager is the singleton class through which we can get all other controller classes referece and then call their respective methods. 

I have followed another design pattern that is - State Machine Pattern. This is used to create and maintain more number of states for a GameObject. For Example - Customer has various states like - Waiting, Satisfied, Dissatisfied, Angry, Impressed etc. So for each state we create concrete class derived from State class and overridden Enter, Execute and Exit Method. And all these states are controlled from a state machine class. This pattern is pretty much scalable. Any time, we can add a new state or modify a current state without effecting the whole system. 

I have created some Configuration files using scriptable object - GameConfig, PlayerConfig & PowerUpConfig. These classes are derived from ScriptableObject. And using CreateMenu , we can create new config file and add values to it. This helps programmer, designer and artist alwell to tweek values according to their need. 


# Conclusion 

I have created this project in 20-25 hours with in a week period of time. I have not tested the whole game from all perspective. So there might be some bugs. Please feel free to download this project and check this out. Please let me know if you find any bugs or if you think some modification in game design or code. Your valuable feedbacks are always welcome. 
