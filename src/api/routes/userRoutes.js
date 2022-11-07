const express = require("express");
const {
    getAllUsers,
    getUser,
    updateUserPoints,
    deleteUser,
} = require("../controllers/userController");

const router = express.Router();

router.get("/users", getAllUsers);
router.get("/user/:userId", getUser);
router.put("/user/points/:userId", updateUserPoints);
router.delete("/user/:id", deleteUser);

module.exports = {
    routes: router,
};
