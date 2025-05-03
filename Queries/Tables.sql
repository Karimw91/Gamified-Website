CREATE Database milestonee2;
USE milestonee2;

-- Disable foreign key constraints to allow dropping tables
EXEC sp_MSforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT ALL";

-- Drop all tables
EXEC sp_MSforeachtable "DROP TABLE ?";

-- Re-enable foreign key constraints
EXEC sp_MSforeachtable "ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL";


CREATE TABLE Admin (
    AdminID INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(100),
    email VARCHAR(100),
    password VARCHAR(100)
);

CREATE TABLE Learner (
    LearnerID INT IDENTITY(1,1) PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    gender VARCHAR(10),
    birth_date DATE,
    country VARCHAR(50),
    cultural_background VARCHAR(50),
    email VARCHAR(100),
    password VARCHAR(100)
);

CREATE TABLE Skills (
    LearnerID INT,
    skill VARCHAR(50),
    PRIMARY KEY (LearnerID, skill),
    FOREIGN KEY (LearnerID) REFERENCES Learner(LearnerID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE LearningPreference (
    LearnerID INT,
    preference VARCHAR(50),
    PRIMARY KEY (LearnerID, preference),
    FOREIGN KEY (LearnerID) REFERENCES Learner(LearnerID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE PersonalizationProfiles (
    LearnerID INT,
    ProfileID INT IDENTITY(101,1),
    Preferred_content_type VARCHAR(50),
    emotional_state VARCHAR(50),
    personality_type VARCHAR(50),
    PRIMARY KEY (LearnerID, ProfileID),  -- Composite Primary Key
    FOREIGN KEY (LearnerID) REFERENCES Learner(LearnerID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE HealthCondition (
    LearnerID INT,
    ProfileID INT,
    condition VARCHAR(255),
    PRIMARY KEY (LearnerID, ProfileID, condition),
    FOREIGN KEY (LearnerID, ProfileID) REFERENCES PersonalizationProfiles(LearnerID, ProfileID) ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE TABLE Course (
    CourseID INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(100) NOT NULL,
    learning_objective VARCHAR(100) NOT NULL,
    credit_points INT NOT NULL,
    difficulty_level VARCHAR(50) NOT NULL,
    pre_requisites VARCHAR(100),
    description VARCHAR(100),
    Status VARCHAR(50)
);

CREATE TABLE CoursePrerequisite (
    CourseID INT,
    Prereq INT,
    PRIMARY KEY (CourseID, Prereq),
    FOREIGN KEY (CourseID) REFERENCES Course(CourseID) ON DELETE NO ACTION ON UPDATE NO ACTION,
    FOREIGN KEY (Prereq) REFERENCES Course(CourseID) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- Ensure the 'Modules' table has a composite key on (ModuleID, CourseID)
CREATE TABLE Modules (
    ModuleID INT IDENTITY(101,1),
    CourseID INT NOT NULL,
    Title VARCHAR(100) NOT NULL,
    difficulty VARCHAR(50) NOT NULL,
    ContentURL VARCHAR(255),
    PRIMARY KEY (ModuleID, CourseID),  -- Composite key on (ModuleID, CourseID)
    FOREIGN KEY (CourseID) REFERENCES Course(CourseID) ON DELETE CASCADE ON UPDATE CASCADE
);

-- 'Target_traits' table with foreign key referencing (ModuleID, CourseID) in 'Modules'
CREATE TABLE Target_traits (
    ModuleID INT,
    CourseID INT,
    Trait VARCHAR(50),
    PRIMARY KEY (ModuleID, CourseID, Trait),
    FOREIGN KEY (ModuleID, CourseID) REFERENCES Modules(ModuleID, CourseID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE ModuleContent (
    ModuleID INT,
    CourseID INT,
    Content_type VARCHAR(50),
    PRIMARY KEY (ModuleID, CourseID, Content_type),
    FOREIGN KEY (ModuleID, CourseID) REFERENCES Modules(ModuleID, CourseID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE ContentLibrary (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    ModuleID INT,
    CourseID INT,
    Title VARCHAR(100),
    description VARCHAR(100),
    metadata VARCHAR(100),
    type VARCHAR(50),
    content_URL VARCHAR(255),
    FOREIGN KEY (ModuleID, CourseID) REFERENCES Modules(ModuleID, CourseID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Assessments (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    ModuleID INT,
    CourseID INT,
    type VARCHAR(50),
    total_marks INT,
    passing_marks INT,
    criteria VARCHAR(100),
    weightage DECIMAL(5, 2),
    description VARCHAR(100),
    title VARCHAR(100),
    FOREIGN KEY (ModuleID, CourseID) REFERENCES Modules(ModuleID, CourseID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Takenassessment (
    AssessmentID INT,
    LearnerID INT,
    scoredPoint INT,
    PRIMARY KEY (AssessmentID, LearnerID),
    FOREIGN KEY (AssessmentID) REFERENCES Assessments(ID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (LearnerID) REFERENCES Learner(LearnerID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Learning_activities (
    ActivityID INT IDENTITY PRIMARY KEY,
    ModuleID INT,
    CourseID INT,
    activity_type VARCHAR(50),
    instruction_details VARCHAR(100),
    Max_points INT,
    FOREIGN KEY (ModuleID, CourseID) REFERENCES Modules(ModuleID, CourseID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Interaction_log (
    LogID INT IDENTITY PRIMARY KEY,
    activity_ID INT,
    LearnerID INT,
    Duration INT,
    Timestamp DATETIME,
    action_type VARCHAR(50),
    FOREIGN KEY (activity_ID) REFERENCES Learning_activities(ActivityID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (LearnerID) REFERENCES Learner(LearnerID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Emotional_feedback (
    FeedbackID INT IDENTITY PRIMARY KEY,
    LearnerID INT,
    activity_ID INT,
    timestamp DATETIME,
    emotional_state VARCHAR(50),
    FOREIGN KEY (LearnerID) REFERENCES Learner(LearnerID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (activity_ID) REFERENCES Learning_activities(ActivityID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Learning_path (
    pathID INT IDENTITY PRIMARY KEY,
    LearnerID INT,
    ProfileID INT,
    completion_status VARCHAR(50),
    custom_content VARCHAR(100),
    adaptive_rules VARCHAR(100),
    FOREIGN KEY (LearnerID, ProfileID) REFERENCES PersonalizationProfiles(LearnerID, ProfileID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Instructor (
    InstructorID INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(100),
     first_name VARCHAR(100),
     last_name VARCHAR(100),
    latest_qualification VARCHAR(100),
    expertise_area VARCHAR(100),
    email VARCHAR(100),
    password VARCHAR(100)
);

CREATE TABLE Pathreview (
    InstructorID INT,
    PathID INT,
    feedback VARCHAR(100),
    PRIMARY KEY (InstructorID, PathID),
    FOREIGN KEY (InstructorID) REFERENCES Instructor(InstructorID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (PathID) REFERENCES Learning_path(PathID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Emotionalfeedback_review (
    FeedbackID INT,
    InstructorID INT,
    feedback VARCHAR(100),
    PRIMARY KEY (FeedbackID, InstructorID),
    FOREIGN KEY (FeedbackID) REFERENCES Emotional_feedback(FeedbackID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (InstructorID) REFERENCES Instructor(InstructorID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Course_enrollment (
    EnrollmentID INT IDENTITY(1,1) PRIMARY KEY,
    CourseID INT,
    LearnerID INT,
    completion_date DATE,
    enrollment_date DATE,
    status VARCHAR(50),
    FOREIGN KEY (CourseID) REFERENCES Course(CourseID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (LearnerID) REFERENCES Learner(LearnerID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Teaches (
    InstructorID INT,
    CourseID INT,
    PRIMARY KEY (InstructorID, CourseID),
    FOREIGN KEY (InstructorID) REFERENCES Instructor(InstructorID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (CourseID) REFERENCES Course(CourseID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Leaderboard (
    BoardID INT IDENTITY(1,1) PRIMARY KEY,
    season VARCHAR(50)
);

CREATE TABLE Ranking (
    BoardID INT,
    LearnerID INT,
    CourseID INT,
    rank INT,
    total_points INT,
    PRIMARY KEY (BoardID, LearnerID),
    FOREIGN KEY (BoardID) REFERENCES Leaderboard(BoardID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (LearnerID) REFERENCES Learner(LearnerID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (CourseID) REFERENCES Course(CourseID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Learning_goal (
    ID INT IDENTITY PRIMARY KEY,
    status VARCHAR(50),
    deadline DATE,
    description VARCHAR(100)
);

CREATE TABLE LearnersGoals (
    GoalID INT,
    LearnerID INT,
    PRIMARY KEY (GoalID, LearnerID),
    FOREIGN KEY (GoalID) REFERENCES Learning_goal(ID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (LearnerID) REFERENCES Learner(LearnerID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Survey (
    ID INT IDENTITY PRIMARY KEY,
    Title VARCHAR(100)
);

CREATE TABLE SurveyQuestions (
    SurveyID INT,
    Question VARCHAR(255),
    PRIMARY KEY (SurveyID, Question),
    FOREIGN KEY (SurveyID) REFERENCES Survey(ID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE FilledSurvey (
    SurveyID INT,
    Question VARCHAR(255),
    LearnerID INT,
    Answer VARCHAR(100),
    PRIMARY KEY (SurveyID, Question, LearnerID),
    FOREIGN KEY (SurveyID, Question) REFERENCES SurveyQuestions(SurveyID, Question) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (LearnerID) REFERENCES Learner(LearnerID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Notification (
    ID INT IDENTITY PRIMARY KEY,
    timestamp DATETIME,
    message VARCHAR(100),
    urgency_level VARCHAR(50),
    ReadStatus BIT DEFAULT 0
);

CREATE TABLE ReceivedNotification (
    NotificationID INT,
    LearnerID INT,
    PRIMARY KEY (NotificationID, LearnerID),
    FOREIGN KEY (NotificationID) REFERENCES Notification(ID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (LearnerID) REFERENCES Learner(LearnerID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Badge (
    BadgeID INT IDENTITY PRIMARY KEY,
    title VARCHAR(100),
    description VARCHAR(100),
    criteria VARCHAR(100),
    points INT
);

CREATE TABLE SkillProgression (
    ID INT IDENTITY PRIMARY KEY,
    proficiency_level VARCHAR(50),
    LearnerID INT,
    skill_name VARCHAR(50),
    timestamp DATETIME,
    FOREIGN KEY (LearnerID, skill_name) REFERENCES Skills(LearnerID, skill) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Achievement (
    AchievementID INT IDENTITY PRIMARY KEY,
    LearnerID INT,
    BadgeID INT,
    description VARCHAR(100),
    date_earned DATE,
    type VARCHAR(50),
    FOREIGN KEY (LearnerID) REFERENCES Learner(LearnerID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (BadgeID) REFERENCES Badge(BadgeID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Reward (
    RewardID INT IDENTITY PRIMARY KEY,
    value DECIMAL(10, 2),
    description VARCHAR(100),
    type VARCHAR(50)
);

CREATE TABLE Quest (
    QuestID INT IDENTITY PRIMARY KEY,
    difficulty_level VARCHAR(50),
    criteria VARCHAR(100),
    description VARCHAR(100),
    title VARCHAR(100)
);

CREATE TABLE Skill_Mastery (
    QuestID INT,
    skill VARCHAR(50),
    PRIMARY KEY (QuestID, skill),
    FOREIGN KEY (QuestID) REFERENCES Quest(QuestID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Collaborative (
    QuestID INT,
    deadline DATETIME,
    max_num_participants INT,
    PRIMARY KEY (QuestID),
    FOREIGN KEY (QuestID) REFERENCES Quest(QuestID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE LearnersCollaboration (
    LearnerID INT,
    QuestID INT,
    completion_status VARCHAR(50),
    PRIMARY KEY (LearnerID, QuestID),
    FOREIGN KEY (LearnerID) REFERENCES Learner(LearnerID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (QuestID) REFERENCES Collaborative(QuestID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE LearnersMastery (
    LearnerID INT,
    QuestID INT,
    completion_status VARCHAR(50),
    PRIMARY KEY (LearnerID, QuestID),
    FOREIGN KEY (LearnerID) REFERENCES Learner(LearnerID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (QuestID) REFERENCES Quest(QuestID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Discussion_forum (
    forumID INT IDENTITY PRIMARY KEY,
    ModuleID INT,
    CourseID INT,
    title VARCHAR(100),
    last_active DATETIME,
    timestamp DATETIME,
    description VARCHAR(100),
    FOREIGN KEY (ModuleID, CourseID) REFERENCES Modules(ModuleID, CourseID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE LearnerDiscussion (
    ForumID INT,
    LearnerID INT,
    Post VARCHAR(100),
    time DATETIME,
    PRIMARY KEY (ForumID, LearnerID, Post),
    FOREIGN KEY (ForumID) REFERENCES Discussion_forum(forumID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (LearnerID) REFERENCES Learner(LearnerID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE QuestReward (
    RewardID INT,
    QuestID INT,
    LearnerID INT,
    Time_earned DATETIME,
    PRIMARY KEY (RewardID, QuestID, LearnerID),
    FOREIGN KEY (RewardID) REFERENCES Reward(RewardID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (QuestID) REFERENCES Quest(QuestID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (LearnerID) REFERENCES Learner(LearnerID) ON DELETE CASCADE ON UPDATE CASCADE
);
