class User {
    constructor(id, username, email, role, totalPoints) {
        this.id = id;
        this.username = username;
        this.email = email;
        this.role = role;
        this.totalPoints = totalPoints;
    }
}

module.exports = User;
