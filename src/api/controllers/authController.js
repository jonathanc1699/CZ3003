const firebase = require("../db");
const User = require("../models/User");
const firestore = firebase.firestore();

const initialPoints = 0;

// email, password, username, role
const signUp = async (req, res, next) => {
    try {
        if (req.body.role !== "admin" && req.body.role !== "student") {
            throw Error("Role must be either student or admin (in lowercase)");
        }
        const authResponse = await firebase
            .auth()
            .createUserWithEmailAndPassword(req.body.email, req.body.password);

        const data = {
            username: req.body.username,
            email: req.body.email,
            role: req.body.role,
            totalPoints: initialPoints,
        };

        await firestore
            .collection("users")
            .doc(authResponse.user.uid)
            .set(data);

        const user = new User(
            authResponse.user.uid,
            req.body.username,
            req.body.email,
            req.body.role,
            initialPoints
        );

        res.json(user);
    } catch (e) {
        res.status(400).send(e.message);
    }
};

const login = async (req, res, next) => {
    try {
        const authResponse = await firebase
            .auth()
            .signInWithEmailAndPassword(req.body.email, req.body.password);

        const uid = authResponse.user.uid;

        const userDoc = await firestore.collection("users").doc(uid);
        const userData = await userDoc.get();

        const user = new User(
            uid,
            userData.data().username,
            userData.data().email,
            userData.data().role,
            userData.data().totalPoints
        );
        res.json(user);
    } catch (e) {
        res.status(400).send(e.message);
    }
};

module.exports = {
    signUp,
    login,
};
