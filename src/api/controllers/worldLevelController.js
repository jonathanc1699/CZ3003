const firebase = require("../db");
const Level = require("../models/level");
const firestore = firebase.firestore();

// Following section is for USER created worlds and levels //
const createUserWorld = async (req, res, next) => {
    try {
        if (req.body.name == undefined || req.body.description == undefined) {
            throw Error("worldId, name, description fields are required");
        }

        // check if user already has a world - only one world per user
        const userId = req.params.userId;
        const userDocument = await firestore.collection("users").doc(userId);
        let userData = await userDocument.get();
        userData = userData.data();

        if (userData.worldId) {
            throw Error(
                "This user already has a world. Only one world can be created per user."
            );
        }

        // autogenerate worldId
        // get number of world documents then + 1
        const worldCollection = await firestore.collection("worlds");
        const worldDocuments = await worldCollection.get();

        const worldId = worldDocuments.size + 1;

        // check if worldId exists
        docExists = await firestore
            .collection("worlds")
            .doc(worldId.toString())
            .get();
        if (docExists.exists) {
            throw Error(
                "worldId already exists, please check auto-generation code of worldId"
            );
        }

        // add to worlds collection
        const worldData = {
            name: req.body.name,
            description: req.body.description,
        };

        await firestore
            .collection("worlds")
            .doc(worldId.toString())
            .set(worldData);

        // add to user's' worldId field - parseInt
        const newUserData = {
            ...userData,
            worldId: worldId,
        };

        await userDocument.update(newUserData);
        res.send("User World created successfully");
    } catch (e) {
        console.log(e);
        res.status(400).send(e.message);
    }
};

const createUserLevel = async (req, res, next) => {
    try {
        if (
            req.body.ans1 == undefined ||
            req.body.ans2 == undefined ||
            req.body.ans3 == undefined ||
            req.body.ans4 == undefined ||
            req.body.correctAnswer == undefined ||
            req.body.points == undefined ||
            req.body.question == undefined ||
            req.body.questionNumber == undefined
        ) {
            throw Error(
                "ans1, ans2, ans3, ans4, correctAnswer, points, question, questionNumber fields are required"
            );
        }

        // check if user has created a world
        const userId = req.params.userId;
        const userDocument = await firestore.collection("users").doc(userId);
        let userData = await userDocument.get();
        userData = userData.data();

        if (!userData.worldId) {
            throw Error(
                "No worldId found under this user. Please create a world first."
            );
        }

        let userWorldId = userData.worldId;

        // check if user world already has 10 levels
        let userLevelsCollection = await firestore
            .collection("userlevels")
            .where("worldId", "==", userWorldId);
        let userLevelsDocuments = await userLevelsCollection.get();

        // console.log(userLevelsDocuments.size);
        if (userLevelsDocuments.size >= 10) {
            throw Error(
                "This world already has 10 levels. No more levels can be created."
            );
        }

        // check qn number doesnt exist for the worldid
        const questionNumberExists = await firestore
            .collection("userlevels")
            .where("worldId", "==", userWorldId)
            .where("questionNumber", "==", req.body.questionNumber)
            .get();

        if (questionNumberExists.size != 0) {
            throw Error(
                "Create a level with a unique question number for this world, this question number already exists"
            );
        }

        // autogenerate levelId based on how many userlevels there are
        userLevelsCollection = await firestore.collection("userlevels");
        userLevelsDocuments = await userLevelsCollection.get();

        // console.log(userLevelsDocuments.size);

        const levelId = userLevelsDocuments.size + 1;

        // add to userlevels collection under with user worldId field
        const newLevelData = {
            ans1: req.body.ans1,
            ans2: req.body.ans2,
            ans3: req.body.ans3,
            ans4: req.body.ans4,
            correctAnswer: parseInt(req.body.correctAnswer),
            question: req.body.question,
            points: parseInt(req.body.points),
            worldId: userWorldId,
            questionNumber: parseInt(req.body.questionNumber)
        };

        await firestore
            .collection("userlevels")
            .doc(levelId.toString())
            .set(newLevelData);

        res.send("User Level created successfully");
    } catch (e) {
        console.log(e);
        res.status(400).send(e.message);
    }
};

const getUserLevels = async (req, res, next) => {
    try {
        // check if user even has a world
        const userId = req.params.userId;
        const userDocument = await firestore.collection("users").doc(userId);
        let userData = await userDocument.get();
        userData = userData.data();

        if (!userData.worldId) {
            throw Error(
                "No worldId found under this user. Please create a world first."
            );
        }

        let userWorldId = userData.worldId;

        // get levels
        let userLevelsCollection = await firestore
            .collection("userlevels")
            .where("worldId", "==", userWorldId);
        let userLevelsDocuments = await userLevelsCollection.get();
        const levelsArray = [];

        userLevelsDocuments.forEach((doc) => {
            const level = new Level(
                parseInt(doc.id),
                doc.data().ans1,
                doc.data().ans2,
                doc.data().ans3,
                doc.data().ans4,
                doc.data().correctAnswer,
                doc.data().points,
                doc.data().question,
                doc.data().worldId
            );
            levelsArray.push(level);
        });

        res.send(levelsArray);
    } catch (e) {
        console.log(e);
        res.status(400).send(e.message);
    }
};

const getUserLevel = async (req, res, next) => {
    try {
        const userId = req.params.userId;
        const questionNumber = parseInt(req.params.questionNumber);
    
        // get worldId from userdata
        const userDocument = await firestore.collection("users").doc(userId);
        let userData = await userDocument.get();
        userData = userData.data();
    
        if (!userData.worldId) {
            throw Error(
                "There is no world for this user. Please create a world first."
            );
        }
    
        let userWorldId = userData.worldId;

        // check if this worldId and questionNumber exists in userlevels collection
        const levelDocument = await firestore
            .collection("userlevels")
            .where("worldId", "==", userWorldId)
            .where("questionNumber", "==", questionNumber)
            .get();

        if (levelDocument.size == 0) {
            throw Error(
                "This question number does not exist for this world. Please create one."
            );
        }

        const levelData = levelDocument.docs[0].data();
        const levelId = levelDocument.docs[0].id;

        let level = new Level(
            parseInt(levelId),
            levelData.ans1,
            levelData.ans2,
            levelData.ans3,
            levelData.ans4,
            levelData.correctAnswer,
            levelData.points,
            levelData.question,
            levelData.worldId,
            levelData.questionNumber
        )

        res.send(level);

    } catch (e) {
        console.log(e);
        res.status(400).send(e.message);
    }
}

const updateUserLevel = async (req, res, next) => {
    try {
        // check required fields
        if (
            req.body.ans1 == undefined ||
            req.body.ans2 == undefined ||
            req.body.ans3 == undefined ||
            req.body.ans4 == undefined ||
            req.body.correctAnswer == undefined ||
            req.body.points == undefined ||
            req.body.question == undefined
        ) {
            throw Error(
                "ans 1, ans2, ans3, ans4, correctAnswer, points, question fields are required"
            );
        }

        // get worldId from userdata
        const userId = req.params.userId;
        const userDocument = await firestore.collection("users").doc(userId);
        let userData = await userDocument.get();
        userData = userData.data();

        if (!userData.worldId) {
            throw Error(
                "There is no world for this user. Please create a world first."
            );
        }

        let userWorldId = userData.worldId;
        let questionNumber = parseInt(req.params.questionNumber)

        // check if this worldId and questionNumber exists in userlevels collection
        const levelDocument = await firestore
            .collection("userlevels")
            .where("worldId", "==", userWorldId)
            .where("questionNumber", "==", questionNumber)
            .get();

        if (levelDocument.size == 0) {
            throw Error(
                "This question number does not exist for this world. Please create one."
            );
        }

        // const levelData = levelDocument.docs[0].data();
        const levelId = levelDocument.docs[0].id;

        // update
        const newLevelData = {
            ans1: req.body.ans1,
            ans2: req.body.ans2,
            ans3: req.body.ans3,
            ans4: req.body.ans4,
            correctAnswer: parseInt(req.body.correctAnswer),
            question: req.body.question,
            points: parseInt(req.body.points),
            worldId: userWorldId,
            questionNumber: questionNumber
        };

        await firestore
            .collection("userlevels")
            .doc(levelId)
            .update(newLevelData);
        res.send("User Level updated successfully");
    } catch (e) {
        console.log(e);
        res.status(400).send(e.message);
    }
};

// the following is for internal admin use, normal gameplay levels and worlds //
const createGameplayLevel = async (req, res, next) => {
    try {
        if (!req.body.levelId) {
            throw Error("levelId is required");
        }
        if (
            req.body.ans1 == undefined ||
            req.body.ans2 == undefined ||
            req.body.ans3 == undefined ||
            req.body.ans4 == undefined ||
            req.body.correctAnswer == undefined ||
            req.body.points == undefined ||
            req.body.question == undefined ||
            req.body.worldId == undefined ||
            req.body.questionNumber == undefined
        ) {
            throw Error(
                "ans 1, ans2, ans3, ans4, correctAnswer, points, question, worldId, questionNumber fields are required"
            );
        }

        docExists = await firestore
            .collection("levels")
            .doc(req.body.levelId.toString())
            .get();
        if (docExists.exists) {
            throw Error(
                "Create a level with a unique levelId, this levelId already exists"
            );
        }

        // check qn number doesnt exist for the worldid
        const questionNumberExists = await firestore
            .collection("levels")
            .where("worldId", "==", req.body.worldId)
            .where("questionNumber", "==", req.body.questionNumber)
            .get();

        if (questionNumberExists.size != 0) {
            throw Error(
                "Create a level with a unique question number for this world, this question number already exists"
            );
        }

        const data = {
            ans1: req.body.ans1,
            ans2: req.body.ans2,
            ans3: req.body.ans3,
            ans4: req.body.ans4,
            correctAnswer: parseInt(req.body.correctAnswer),
            question: req.body.question,
            points: parseInt(req.body.points),
            worldId: parseInt(req.body.worldId),
            questionNumber: parseInt(req.body.questionNumber),
        };

        await firestore
            .collection("levels")
            .doc(req.body.levelId.toString())
            .set(data);
        res.send("Level created successfully");
    } catch (e) {
        console.log(e);
        res.status(400).send(e.message);
    }
};

const getGameplayLevels = async (req, res, next) => {
    try {
        const worldId = parseInt(req.params.worldId);
        const levels = await firestore
            .collection("levels")
            .where("worldId", "==", worldId);
        const data = await levels.get();
        const levelsArray = [];

        data.forEach((doc) => {
            const level = new Level(
                parseInt(doc.id),
                doc.data().ans1,
                doc.data().ans2,
                doc.data().ans3,
                doc.data().ans4,
                doc.data().correctAnswer,
                doc.data().points,
                doc.data().question,
                doc.data().worldId,
                doc.data().questionNumber
            );
            levelsArray.push(level);
        });

        res.send(levelsArray);
    } catch (e) {
        console.log(e);
        res.status(400).send(e.message);
    }
};

// Get gameplay level by worldid and question number
const getGameplayLevel = async (req, res, next) => {
    try {
        const worldId = parseInt(req.params.worldId);
        const questionNumber = parseInt(req.params.questionNumber);

        const levels = await firestore
            .collection("levels")
            .where("worldId", "==", worldId)
            .where("questionNumber", "==", questionNumber);

        const data = await levels.get();

        let level;
        data.forEach((doc) => {
            level = new Level(
                parseInt(doc.id),
                doc.data().ans1,
                doc.data().ans2,
                doc.data().ans3,
                doc.data().ans4,
                doc.data().correctAnswer,
                doc.data().points,
                doc.data().question,
                doc.data().worldId,
                doc.data().questionNumber
            );
        });

        res.send(level);
    } catch (e) {
        console.log(e);
        res.status(400).send(e.message);
    }
};

// assume cant change worldId or questionNumber of the level
// pass in worldId and questionNumber in the route path
const updateGameplayLevel = async (req, res, next) => {
    try {
        // check required fields
        if (
            req.body.ans1 == undefined ||
            req.body.ans2 == undefined ||
            req.body.ans3 == undefined ||
            req.body.ans4 == undefined ||
            req.body.correctAnswer == undefined ||
            req.body.points == undefined ||
            req.body.question == undefined
        ) {
            throw Error(
                "ans 1, ans2, ans3, ans4, correctAnswer, points, question fields are required"
            );
        }

        const worldId = parseInt(req.params.worldId);
        const questionNumber = parseInt(req.params.questionNumber);

        const levels = await firestore
            .collection("levels")
            .where("worldId", "==", worldId)
            .where("questionNumber", "==", questionNumber);

        const data = await levels.get();

        if (data.size == 0) {
            throw Error(
                "This worldId and questionNumber does not exist yet. Please create one."
            );
        }

        const levelData = data.docs[0].data();
        const levelId = data.docs[0].id;

        // update
        const newLevelData = {
            ans1: req.body.ans1,
            ans2: req.body.ans2,
            ans3: req.body.ans3,
            ans4: req.body.ans4,
            correctAnswer: parseInt(req.body.correctAnswer),
            question: req.body.question,
            points: parseInt(req.body.points),
            worldId: levelData.worldId,
            questionNumber: levelData.questionNumber,
        };

        await firestore.collection("levels").doc(levelId).update(newLevelData);
        res.send("Level updated successfully");
    } catch (e) {
        console.log(e);
        res.status(400).send(e.message);
    }
};

module.exports = {
    getGameplayLevels,
    createGameplayLevel,
    createUserWorld,
    createUserLevel,
    getUserLevels,
    updateGameplayLevel,
    updateUserLevel,
    getGameplayLevel,
    getUserLevel
};
