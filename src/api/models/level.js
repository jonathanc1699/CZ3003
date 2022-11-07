class Level {
    constructor(
        levelId,
        ans1,
        ans2,
        ans3,
        ans4,
        correctAnswer,
        points,
        question,
        worldId,
        questionNumber
    ) {
        this.levelId = levelId;
        this.ans1 = ans1;
        this.ans2 = ans2;
        this.ans3 = ans3;
        this.ans4 = ans4;
        this.correctAnswer = correctAnswer;
        this.points = points;
        this.question = question;
        this.worldId = worldId;
        this.questionNumber = questionNumber;
    }
}

module.exports = Level;
