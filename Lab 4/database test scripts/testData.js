const userData = [
    {
        id: "yqc2ON8axmR2kkOOqyMACug6yOo2",
        data: {
            email: "userone@test.com",
            role: "student",
            totalPoints: 0,
            username: "userone",
            worldId: 6,
        },
    },
    {
        id: "hNbMPFR8uehRp9tGRWFyCq34jRM2",
        data: {
            email: "usertwo@test.com",
            role: "student",
            totalPoints: 0,
            username: "usertwo",
            worldId: 5,
        },
    },
];

const worldData = [
    {
        id: "1",
        data: {
            name: "World 1",
            description: "Addition",
        },
    },
    {
        id: "2",
        data: {
            name: "World 2",
            description: "Subtraction",
        },
    },
    {
        id: "3",
        data: {
            name: "World 3",
            description: "Multiplication",
        },
    },
    {
        id: "4",
        data: {
            name: "World 4",
            description: "Division",
        },
    },
    {
        id: "5",
        data: {
            name: "Usertwo's World",
            description: "Usertwo's World",
        },
    },
    {
        id: "6",
        data: {
            name: "Userone's World",
            description: "Userone's World",
        },
    },
];

const userLevelData = [
    {
        id: "1",
        data: {
            ans1: 12,
            ans2: 24,
            ans3: 9,
            ans4: 0,
            correctAnswer: 3,
            points: 35,
            question: "What is 3*3?",
            questionNumber: 1,
            worldId: 5,
        },
    },
    {
        id: "2",
        data: {
            ans1: 12,
            ans2: 24,
            ans3: 9,
            ans4: 15,
            correctAnswer: 4,
            points: 40,
            question: "What is 5*3?",
            questionNumber: 2,
            worldId: 5,
        },
    },
];

const gameplayLevelData = [
    {
        id: "1",
        data: {
            ans1: 2,
            ans2: 3,
            ans3: 0,
            ans4: 4,
            correctAnswer: 1,
            points: 10,
            question: "What is 1+1?",
            questionNumber: 1,
            worldId: 1,
        },
    },
    {
        id: "2",
        data: {
            ans1: 3,
            ans2: 4,
            ans3: 0,
            ans4: 8,
            correctAnswer: 4,
            points: 20,
            question: "What is 4+4?",
            questionNumber: 2,
            worldId: 1,
        },
    },
    {
        id: "3",
        data: {
            ans1: 3,
            ans2: 4,
            ans3: 10,
            ans4: 8,
            correctAnswer: 3,
            points: 20,
            question: "What is 4+6?",
            questionNumber: 3,
            worldId: 1,
        },
    },
    {
        id: "4",
        data: {
            ans1: 3,
            ans2: 4,
            ans3: 0,
            ans4: 9,
            correctAnswer: 4,
            points: 20,
            question: "What is 4+5?",
            questionNumber: 4,
            worldId: 1,
        },
    },
    {
        id: "5",
        data: {
            ans1: 14,
            ans2: 12,
            ans3: 6,
            ans4: 8,
            correctAnswer: 2,
            points: 20,
            question: "What is 6+6?",
            questionNumber: 5,
            worldId: 1,
        },
    },
    {
        id: "6",
        data: {
            ans1: 14,
            ans2: 11,
            ans3: 6,
            ans4: 8,
            correctAnswer: 2,
            points: 20,
            question: "What is 6+5?",
            questionNumber: 6,
            worldId: 1,
        },
    },
    {
        id: "7",
        data: {
            ans1: 13,
            ans2: 12,
            ans3: 6,
            ans4: 8,
            correctAnswer: 1,
            points: 20,
            question: "What is 6+7?",
            questionNumber: 7,
            worldId: 1,
        },
    },
];

module.exports = {
    userData,
    worldData,
    userLevelData,
    gameplayLevelData,
};
