const firebase = require("../db");
const User = require("../models/User");
const firestore = firebase.firestore();

const getAllUsers = async (req, res, next) => {
    try {
        const users = await firestore.collection("users");
        const data = await users.get();
        const usersArray = [];
        if (data.empty) {
            res.status(404).send("No users found, please add one");
        } else {
            data.forEach((doc) => {
                const user = new User(
                    doc.id,
                    doc.data().username,
                    doc.data().email,
                    doc.data().role,
                    doc.data().totalPoints
                );
                usersArray.push(user);
            });
            res.send(usersArray);
        }
    } catch (e) {
        res.status(400).send(e.message);
    }
};

const getUser = async (req, res, next) => {
    try {
        const id = req.params.userId;
        const user = await firestore.collection("users").doc(id);
        const data = await user.get();
        if (!data.exists) {
            res.status(404).send("User with the given ID not found");
        } else {
            res.send(data.data());
        }
    } catch (e) {
        res.status(400).send(e.message);
    }
};

const updateUserPoints = async (req, res, next) => {
    try {
        if (
            req.body.totalPoints == undefined ||
            !Number.isInteger(req.body.totalPoints)
        ) {
            throw Error("totalPoints field is required and must be an integer");
        }
        const id = req.params.userId;
        const newPoints = req.body.totalPoints;
        const user = await firestore.collection("users").doc(id);
        const oldData = await user.get();
        const data = {
            ...oldData.data(),
            totalPoints: newPoints,
        };
        console.log(data);
        await user.update(data);

        let updatedUser = await firestore.collection("users").doc(id);
        updatedUser = await updatedUser.get();
        res.send(updatedUser.data());
    } catch (e) {
        res.status(400).send(e.message);
    }
};

const deleteUser = async (req, res, next) => {
    try {
        const id = req.params.id;
        const user = await firestore.collection("users").doc(id).delete();
        res.send("User deleted successfully");
    } catch (e) {
        res.status(400).send(e.message);
    }
};

module.exports = {
    getAllUsers,
    getUser,
    updateUserPoints,
    deleteUser,
};
