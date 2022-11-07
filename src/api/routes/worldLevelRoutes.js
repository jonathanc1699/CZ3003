const express = require("express");
const {
    createGameplayLevel,
    getGameplayLevels,
    createUserWorld,
    createUserLevel,
    getUserLevels,
    updateGameplayLevel,
    updateUserLevel,
    getGameplayLevel,
    getUserLevel
} = require("../controllers/worldLevelController");

const router = express.Router();

router.post("/levels", createGameplayLevel);
router.get("/levels/:worldId", getGameplayLevels);
router.get("/level/:worldId/:questionNumber", getGameplayLevel);
router.put("/level/:worldId/:questionNumber", updateGameplayLevel);

// for user to create world and levels
router.post("/:userId/world", createUserWorld);
router.post("/:userId/level", createUserLevel);
router.get("/:userId/levels", getUserLevels);
router.get("/:userId/level/:questionNumber", getUserLevel);
router.put("/:userId/level/:questionNumber", updateUserLevel);

module.exports = {
    routes: router,
};
