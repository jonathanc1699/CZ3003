## Setup

-   Get .env file from me first

Im using node v14.13.1

Run the following commands:

```
$ npm install

$ npm start
```

To test the routes with postman, I have included the postman collection here. It is the file called "CZ3003 APIS.postman_collection.json". Install postman and import it to your workspace, the routes will already be there for you and some sample json request I have created so yall don't need to create from scratch. If udk how to use ask me to zoom or smth i can show u.

## Routes Created

### User Routes

-   login
-   getAllUsers
-   getUser
-   updateUserPoints
-   deleteUser
-   createUserWorld
-   createUserLevel
-   getUserLevels
-   updateUserLevels

### Admin Routes

-   signUp (to create a user on our side so yall dont have to go into firebase to create)
-   deleteUser
-   createGameplayLevel
-   updateGameplayLevel (pass in worldId and questionNumber)
-   getGameplayLevels
-   getGameplayLevel (pass in worldId and questionNumber)

** Some stuff like deleting user world (if its already created, since one user can only have one world), its easier if you just go into the firebase database to delete yourself. If yall need APIs for that (or for the application) let me know and I'll make. **

** Gameplay worlds are already created in the database, with worldId 1,2,3,4 (since there's going to be 4 gameplay worlds) **

-   I will add more details about the routes when Im free or can just ask me
