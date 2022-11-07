const firebase = require("firebase");
const {
    userData,
    worldData,
    userLevelData,
    gameplayLevelData,
} = require("./testData");

// to run, get config values and run "node main.js"

// config values are hidden for security
const firebaseConfig = {
    apiKey: "XXXXXXXXXXXXXXXXXXXXXXX",
    authDomain: "XXXXXXXXXXXXXXXXXXXXXXX",
    projectId: "XXXXXXXXXXXXXXXXXXXXXXX",
    storageBucket: "XXXXXXXXXXXXXXXXXXXXXXX",
    messagingSenderId: "XXXXXXXXXXXXXXXXXXXXXXX",
    appId: "XXXXXXXXXXXXXXXXXXXXXXX",
};

// Initialize Firebase
const app = firebase.initializeApp(firebaseConfig);
const firestore = app.firestore();

// insert data
const insertUserData = async (data) => {
    try {
        for (const user of data) {
            await firestore.collection("users").doc(user.id).set(user.data);
        }
    } catch (e) {
        console.log(e);
    }
};

const insertWorldData = async (data) => {
    try {
        for (const world of data) {
            await firestore.collection("worlds").doc(world.id).set(world.data);
        }
    } catch (e) {
        console.log(e);
    }
};

const insertUserLevelData = async (data) => {
    try {
        for (const userLevel of data) {
            await firestore
                .collection("userlevels")
                .doc(userLevel.id)
                .set(userLevel.data);
        }
    } catch (e) {
        console.log(e);
    }
};

const insertGameplayLevelData = async (data) => {
    try {
        for (const gameplayLevel of data) {
            await firestore
                .collection("levels")
                .doc(gameplayLevel.id)
                .set(gameplayLevel.data);
        }
    } catch (e) {
        console.log(e);
    }
};

insertUserData(userData);
insertWorldData(worldData);
insertUserLevelData(userLevelData);
insertUserLevelData(gameplayLevelData);
