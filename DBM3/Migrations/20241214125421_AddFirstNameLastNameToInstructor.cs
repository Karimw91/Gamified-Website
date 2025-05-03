using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBM3.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstNameLastNameToInstructor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Admin__719FE4E8E0AD72B3", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "Badge",
                columns: table => new
                {
                    BadgeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    criteria = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    points = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Badge__1918237C2E46D221", x => x.BadgeID);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    learning_objective = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    credit_points = table.Column<int>(type: "int", nullable: false),
                    difficulty_level = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    pre_requisites = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Course__C92D7187C07F22FA", x => x.CourseID);
                });

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    InstructorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    latest_qualification = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    expertise_area = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Instruct__9D010B7BABDFCD53", x => x.InstructorID);
                });

            migrationBuilder.CreateTable(
                name: "Leaderboard",
                columns: table => new
                {
                    BoardID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    season = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Leaderbo__F9646BD2B26833C9", x => x.BoardID);
                });

            migrationBuilder.CreateTable(
                name: "Learner",
                columns: table => new
                {
                    LearnerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    last_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    gender = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    country = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    cultural_background = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learner__67ABFCFA91637788", x => x.LearnerID);
                });

            migrationBuilder.CreateTable(
                name: "Learning_goal",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    deadline = table.Column<DateOnly>(type: "date", nullable: true),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learning__3214EC27930D285D", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: true),
                    message = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    urgency_level = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ReadStatus = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__3214EC277FB820B2", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Quest",
                columns: table => new
                {
                    QuestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    difficulty_level = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    criteria = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Quest__B6619ACB489A81A2", x => x.QuestID);
                });

            migrationBuilder.CreateTable(
                name: "Reward",
                columns: table => new
                {
                    RewardID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reward__825015998302920B", x => x.RewardID);
                });

            migrationBuilder.CreateTable(
                name: "Survey",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Survey__3214EC2702790C98", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CoursePrerequisite",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    Prereq = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CoursePr__F8693C2CB5F87924", x => new { x.CourseID, x.Prereq });
                    table.ForeignKey(
                        name: "FK__CoursePre__Cours__04C58C4B",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID");
                    table.ForeignKey(
                        name: "FK__CoursePre__Prere__05B9B084",
                        column: x => x.Prereq,
                        principalTable: "Course",
                        principalColumn: "CourseID");
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    ModuleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    difficulty = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ContentURL = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Modules__47E6A09FD4E3F70E", x => new { x.ModuleID, x.CourseID });
                    table.ForeignKey(
                        name: "FK__Modules__CourseI__08961D2F",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teaches",
                columns: table => new
                {
                    InstructorID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Teaches__F193DC63303A0B43", x => new { x.InstructorID, x.CourseID });
                    table.ForeignKey(
                        name: "FK__Teaches__CourseI__365CE7DF",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Teaches__Instruc__3568C3A6",
                        column: x => x.InstructorID,
                        principalTable: "Instructor",
                        principalColumn: "InstructorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Achievement",
                columns: table => new
                {
                    AchievementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LearnerID = table.Column<int>(type: "int", nullable: true),
                    BadgeID = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    date_earned = table.Column<DateOnly>(type: "date", nullable: true),
                    type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Achievem__276330E0101EA2C5", x => x.AchievementID);
                    table.ForeignKey(
                        name: "FK__Achieveme__Badge__5A9A4855",
                        column: x => x.BadgeID,
                        principalTable: "Badge",
                        principalColumn: "BadgeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Achieveme__Learn__59A6241C",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Course_enrollment",
                columns: table => new
                {
                    EnrollmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    LearnerID = table.Column<int>(type: "int", nullable: true),
                    completion_date = table.Column<DateOnly>(type: "date", nullable: true),
                    enrollment_date = table.Column<DateOnly>(type: "date", nullable: true),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Course_e__7F6877FBB4305544", x => x.EnrollmentID);
                    table.ForeignKey(
                        name: "FK__Course_en__Cours__319832C2",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Course_en__Learn__328C56FB",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearningPreference",
                columns: table => new
                {
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    preference = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learning__6032E1582F004F46", x => new { x.LearnerID, x.preference });
                    table.ForeignKey(
                        name: "FK__LearningP__Learn__7A47FDD8",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalizationProfiles",
                columns: table => new
                {
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    ProfileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Preferred_content_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    emotional_state = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    personality_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Personal__353B3472B382B545", x => new { x.LearnerID, x.ProfileID });
                    table.ForeignKey(
                        name: "FK__Personali__Learn__7D246A83",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ranking",
                columns: table => new
                {
                    BoardID = table.Column<int>(type: "int", nullable: false),
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    rank = table.Column<int>(type: "int", nullable: true),
                    total_points = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ranking__4F1ED41D06E958F2", x => new { x.BoardID, x.LearnerID });
                    table.ForeignKey(
                        name: "FK__Ranking__BoardID__3B219CFC",
                        column: x => x.BoardID,
                        principalTable: "Leaderboard",
                        principalColumn: "BoardID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Ranking__CourseI__3D09E56E",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Ranking__Learner__3C15C135",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    skill = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Skills__C45BDEA5C7381CD4", x => new { x.LearnerID, x.skill });
                    table.ForeignKey(
                        name: "FK__Skills__LearnerI__776B912D",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearnersGoals",
                columns: table => new
                {
                    GoalID = table.Column<int>(type: "int", nullable: false),
                    LearnerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learners__3C3540FE0360641F", x => new { x.GoalID, x.LearnerID });
                    table.ForeignKey(
                        name: "FK__LearnersG__GoalI__41CE9A8B",
                        column: x => x.GoalID,
                        principalTable: "Learning_goal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__LearnersG__Learn__42C2BEC4",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceivedNotification",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "int", nullable: false),
                    LearnerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Received__96B591FD2ADFE8DF", x => new { x.NotificationID, x.LearnerID });
                    table.ForeignKey(
                        name: "FK__ReceivedN__Learn__52050254",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__ReceivedN__Notif__5110DE1B",
                        column: x => x.NotificationID,
                        principalTable: "Notification",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Collaborative",
                columns: table => new
                {
                    QuestID = table.Column<int>(type: "int", nullable: false),
                    deadline = table.Column<DateTime>(type: "datetime", nullable: true),
                    max_num_participants = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Collabor__B6619ACBB2F416E0", x => x.QuestID);
                    table.ForeignKey(
                        name: "FK__Collabora__Quest__6423B28F",
                        column: x => x.QuestID,
                        principalTable: "Quest",
                        principalColumn: "QuestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearnersMastery",
                columns: table => new
                {
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    QuestID = table.Column<int>(type: "int", nullable: false),
                    completion_status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learners__CCCDE556564EA51E", x => new { x.LearnerID, x.QuestID });
                    table.ForeignKey(
                        name: "FK__LearnersM__Learn__6AD0B01E",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__LearnersM__Quest__6BC4D457",
                        column: x => x.QuestID,
                        principalTable: "Quest",
                        principalColumn: "QuestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skill_Mastery",
                columns: table => new
                {
                    QuestID = table.Column<int>(type: "int", nullable: false),
                    skill = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Skill_Ma__1591B894BEB4AA4B", x => new { x.QuestID, x.skill });
                    table.ForeignKey(
                        name: "FK__Skill_Mas__Quest__614745E4",
                        column: x => x.QuestID,
                        principalTable: "Quest",
                        principalColumn: "QuestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestReward",
                columns: table => new
                {
                    RewardID = table.Column<int>(type: "int", nullable: false),
                    QuestID = table.Column<int>(type: "int", nullable: false),
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    Time_earned = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QuestRew__D251A7C963CB588A", x => new { x.RewardID, x.QuestID, x.LearnerID });
                    table.ForeignKey(
                        name: "FK__QuestRewa__Learn__77368703",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__QuestRewa__Quest__764262CA",
                        column: x => x.QuestID,
                        principalTable: "Quest",
                        principalColumn: "QuestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__QuestRewa__Rewar__754E3E91",
                        column: x => x.RewardID,
                        principalTable: "Reward",
                        principalColumn: "RewardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestions",
                columns: table => new
                {
                    SurveyID = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SurveyQu__23FB983B56ADD8BA", x => new { x.SurveyID, x.Question });
                    table.ForeignKey(
                        name: "FK__SurveyQue__Surve__478773E1",
                        column: x => x.SurveyID,
                        principalTable: "Survey",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleID = table.Column<int>(type: "int", nullable: true),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    total_marks = table.Column<int>(type: "int", nullable: true),
                    passing_marks = table.Column<int>(type: "int", nullable: true),
                    criteria = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    weightage = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Assessme__3214EC2720651FEC", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Assessments__1407CFDB",
                        columns: x => new { x.ModuleID, x.CourseID },
                        principalTable: "Modules",
                        principalColumns: new[] { "ModuleID", "CourseID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentLibrary",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleID = table.Column<int>(type: "int", nullable: true),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    metadata = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    content_URL = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ContentL__3214EC277EA15424", x => x.ID);
                    table.ForeignKey(
                        name: "FK__ContentLibrary__112B6330",
                        columns: x => new { x.ModuleID, x.CourseID },
                        principalTable: "Modules",
                        principalColumns: new[] { "ModuleID", "CourseID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discussion_forum",
                columns: table => new
                {
                    forumID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleID = table.Column<int>(type: "int", nullable: true),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    last_active = table.Column<DateTime>(type: "datetime", nullable: true),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: true),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Discussi__BBA7A4400A1A8FEF", x => x.forumID);
                    table.ForeignKey(
                        name: "FK__Discussion_forum__6EA14102",
                        columns: x => new { x.ModuleID, x.CourseID },
                        principalTable: "Modules",
                        principalColumns: new[] { "ModuleID", "CourseID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Learning_activities",
                columns: table => new
                {
                    ActivityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleID = table.Column<int>(type: "int", nullable: true),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    activity_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    instruction_details = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Max_points = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learning__45F4A7F187B0FCB4", x => x.ActivityID);
                    table.ForeignKey(
                        name: "FK__Learning_activit__1AB4CD6A",
                        columns: x => new { x.ModuleID, x.CourseID },
                        principalTable: "Modules",
                        principalColumns: new[] { "ModuleID", "CourseID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModuleContent",
                columns: table => new
                {
                    ModuleID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    Content_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ModuleCo__C6FAFA2B9412E89E", x => new { x.ModuleID, x.CourseID, x.Content_type });
                    table.ForeignKey(
                        name: "FK__ModuleContent__0E4EF685",
                        columns: x => new { x.ModuleID, x.CourseID },
                        principalTable: "Modules",
                        principalColumns: new[] { "ModuleID", "CourseID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Target_traits",
                columns: table => new
                {
                    ModuleID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    Trait = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Target_t__4E005E4CFB70F715", x => new { x.ModuleID, x.CourseID, x.Trait });
                    table.ForeignKey(
                        name: "FK__Target_traits__0B7289DA",
                        columns: x => new { x.ModuleID, x.CourseID },
                        principalTable: "Modules",
                        principalColumns: new[] { "ModuleID", "CourseID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthCondition",
                columns: table => new
                {
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    ProfileID = table.Column<int>(type: "int", nullable: false),
                    condition = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HealthCo__930320B0CD9E9CE2", x => new { x.LearnerID, x.ProfileID, x.condition });
                    table.ForeignKey(
                        name: "FK__HealthCondition__0000D72E",
                        columns: x => new { x.LearnerID, x.ProfileID },
                        principalTable: "PersonalizationProfiles",
                        principalColumns: new[] { "LearnerID", "ProfileID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Learning_path",
                columns: table => new
                {
                    pathID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LearnerID = table.Column<int>(type: "int", nullable: true),
                    ProfileID = table.Column<int>(type: "int", nullable: true),
                    completion_status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    custom_content = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    adaptive_rules = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learning__BFB8200A23F55F4A", x => x.pathID);
                    table.ForeignKey(
                        name: "FK__Learning_path__25325BDD",
                        columns: x => new { x.LearnerID, x.ProfileID },
                        principalTable: "PersonalizationProfiles",
                        principalColumns: new[] { "LearnerID", "ProfileID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkillProgression",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    proficiency_level = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LearnerID = table.Column<int>(type: "int", nullable: true),
                    skill_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SkillPro__3214EC2754E05E9C", x => x.ID);
                    table.ForeignKey(
                        name: "FK__SkillProgression__56C9B771",
                        columns: x => new { x.LearnerID, x.skill_name },
                        principalTable: "Skills",
                        principalColumns: new[] { "LearnerID", "skill" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearnersCollaboration",
                columns: table => new
                {
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    QuestID = table.Column<int>(type: "int", nullable: false),
                    completion_status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learners__CCCDE556AF22C22D", x => new { x.LearnerID, x.QuestID });
                    table.ForeignKey(
                        name: "FK__LearnersC__Learn__67001F3A",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__LearnersC__Quest__67F44373",
                        column: x => x.QuestID,
                        principalTable: "Collaborative",
                        principalColumn: "QuestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilledSurvey",
                columns: table => new
                {
                    SurveyID = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FilledSu__D89C33C712C200A3", x => new { x.SurveyID, x.Question, x.LearnerID });
                    table.ForeignKey(
                        name: "FK__FilledSur__Learn__4B5804C5",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__FilledSurvey__4A63E08C",
                        columns: x => new { x.SurveyID, x.Question },
                        principalTable: "SurveyQuestions",
                        principalColumns: new[] { "SurveyID", "Question" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Takenassessment",
                columns: table => new
                {
                    AssessmentID = table.Column<int>(type: "int", nullable: false),
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    scoredPoint = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Takenass__8B5147F157A9FCEB", x => new { x.AssessmentID, x.LearnerID });
                    table.ForeignKey(
                        name: "FK__Takenasse__Asses__16E43C86",
                        column: x => x.AssessmentID,
                        principalTable: "Assessments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Takenasse__Learn__17D860BF",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearnerDiscussion",
                columns: table => new
                {
                    ForumID = table.Column<int>(type: "int", nullable: false),
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    Post = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LearnerD__FBCECC4A87697A78", x => new { x.ForumID, x.LearnerID, x.Post });
                    table.ForeignKey(
                        name: "FK__LearnerDi__Forum__717DADAD",
                        column: x => x.ForumID,
                        principalTable: "Discussion_forum",
                        principalColumn: "forumID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__LearnerDi__Learn__7271D1E6",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emotional_feedback",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LearnerID = table.Column<int>(type: "int", nullable: true),
                    activity_ID = table.Column<int>(type: "int", nullable: true),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: true),
                    emotional_state = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Emotiona__6A4BEDF655A6F925", x => x.FeedbackID);
                    table.ForeignKey(
                        name: "FK__Emotional__Learn__2161CAF9",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Emotional__activ__2255EF32",
                        column: x => x.activity_ID,
                        principalTable: "Learning_activities",
                        principalColumn: "ActivityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interaction_log",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    activity_ID = table.Column<int>(type: "int", nullable: true),
                    LearnerID = table.Column<int>(type: "int", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime", nullable: true),
                    action_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Interact__5E5499A831449E9E", x => x.LogID);
                    table.ForeignKey(
                        name: "FK__Interacti__Learn__1E855E4E",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Interacti__activ__1D913A15",
                        column: x => x.activity_ID,
                        principalTable: "Learning_activities",
                        principalColumn: "ActivityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pathreview",
                columns: table => new
                {
                    InstructorID = table.Column<int>(type: "int", nullable: false),
                    PathID = table.Column<int>(type: "int", nullable: false),
                    feedback = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pathrevi__11D776B874C682FA", x => new { x.InstructorID, x.PathID });
                    table.ForeignKey(
                        name: "FK__Pathrevie__Instr__29F710FA",
                        column: x => x.InstructorID,
                        principalTable: "Instructor",
                        principalColumn: "InstructorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Pathrevie__PathI__2AEB3533",
                        column: x => x.PathID,
                        principalTable: "Learning_path",
                        principalColumn: "pathID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emotionalfeedback_review",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(type: "int", nullable: false),
                    InstructorID = table.Column<int>(type: "int", nullable: false),
                    feedback = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Emotiona__C39BFD41679364F3", x => new { x.FeedbackID, x.InstructorID });
                    table.ForeignKey(
                        name: "FK__Emotional__Feedb__2DC7A1DE",
                        column: x => x.FeedbackID,
                        principalTable: "Emotional_feedback",
                        principalColumn: "FeedbackID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Emotional__Instr__2EBBC617",
                        column: x => x.InstructorID,
                        principalTable: "Instructor",
                        principalColumn: "InstructorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievement_BadgeID",
                table: "Achievement",
                column: "BadgeID");

            migrationBuilder.CreateIndex(
                name: "IX_Achievement_LearnerID",
                table: "Achievement",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_ModuleID_CourseID",
                table: "Assessments",
                columns: new[] { "ModuleID", "CourseID" });

            migrationBuilder.CreateIndex(
                name: "IX_ContentLibrary_ModuleID_CourseID",
                table: "ContentLibrary",
                columns: new[] { "ModuleID", "CourseID" });

            migrationBuilder.CreateIndex(
                name: "IX_Course_enrollment_CourseID",
                table: "Course_enrollment",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_enrollment_LearnerID",
                table: "Course_enrollment",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_CoursePrerequisite_Prereq",
                table: "CoursePrerequisite",
                column: "Prereq");

            migrationBuilder.CreateIndex(
                name: "IX_Discussion_forum_ModuleID_CourseID",
                table: "Discussion_forum",
                columns: new[] { "ModuleID", "CourseID" });

            migrationBuilder.CreateIndex(
                name: "IX_Emotional_feedback_activity_ID",
                table: "Emotional_feedback",
                column: "activity_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Emotional_feedback_LearnerID",
                table: "Emotional_feedback",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Emotionalfeedback_review_InstructorID",
                table: "Emotionalfeedback_review",
                column: "InstructorID");

            migrationBuilder.CreateIndex(
                name: "IX_FilledSurvey_LearnerID",
                table: "FilledSurvey",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Interaction_log_activity_ID",
                table: "Interaction_log",
                column: "activity_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Interaction_log_LearnerID",
                table: "Interaction_log",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_LearnerDiscussion_LearnerID",
                table: "LearnerDiscussion",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_LearnersCollaboration_QuestID",
                table: "LearnersCollaboration",
                column: "QuestID");

            migrationBuilder.CreateIndex(
                name: "IX_LearnersGoals_LearnerID",
                table: "LearnersGoals",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_LearnersMastery_QuestID",
                table: "LearnersMastery",
                column: "QuestID");

            migrationBuilder.CreateIndex(
                name: "IX_Learning_activities_ModuleID_CourseID",
                table: "Learning_activities",
                columns: new[] { "ModuleID", "CourseID" });

            migrationBuilder.CreateIndex(
                name: "IX_Learning_path_LearnerID_ProfileID",
                table: "Learning_path",
                columns: new[] { "LearnerID", "ProfileID" });

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CourseID",
                table: "Modules",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Pathreview_PathID",
                table: "Pathreview",
                column: "PathID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestReward_LearnerID",
                table: "QuestReward",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestReward_QuestID",
                table: "QuestReward",
                column: "QuestID");

            migrationBuilder.CreateIndex(
                name: "IX_Ranking_CourseID",
                table: "Ranking",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Ranking_LearnerID",
                table: "Ranking",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivedNotification_LearnerID",
                table: "ReceivedNotification",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_SkillProgression_LearnerID_skill_name",
                table: "SkillProgression",
                columns: new[] { "LearnerID", "skill_name" });

            migrationBuilder.CreateIndex(
                name: "IX_Takenassessment_LearnerID",
                table: "Takenassessment",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Teaches_CourseID",
                table: "Teaches",
                column: "CourseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Achievement");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "ContentLibrary");

            migrationBuilder.DropTable(
                name: "Course_enrollment");

            migrationBuilder.DropTable(
                name: "CoursePrerequisite");

            migrationBuilder.DropTable(
                name: "Emotionalfeedback_review");

            migrationBuilder.DropTable(
                name: "FilledSurvey");

            migrationBuilder.DropTable(
                name: "HealthCondition");

            migrationBuilder.DropTable(
                name: "Interaction_log");

            migrationBuilder.DropTable(
                name: "LearnerDiscussion");

            migrationBuilder.DropTable(
                name: "LearnersCollaboration");

            migrationBuilder.DropTable(
                name: "LearnersGoals");

            migrationBuilder.DropTable(
                name: "LearnersMastery");

            migrationBuilder.DropTable(
                name: "LearningPreference");

            migrationBuilder.DropTable(
                name: "ModuleContent");

            migrationBuilder.DropTable(
                name: "Pathreview");

            migrationBuilder.DropTable(
                name: "QuestReward");

            migrationBuilder.DropTable(
                name: "Ranking");

            migrationBuilder.DropTable(
                name: "ReceivedNotification");

            migrationBuilder.DropTable(
                name: "Skill_Mastery");

            migrationBuilder.DropTable(
                name: "SkillProgression");

            migrationBuilder.DropTable(
                name: "Takenassessment");

            migrationBuilder.DropTable(
                name: "Target_traits");

            migrationBuilder.DropTable(
                name: "Teaches");

            migrationBuilder.DropTable(
                name: "Badge");

            migrationBuilder.DropTable(
                name: "Emotional_feedback");

            migrationBuilder.DropTable(
                name: "SurveyQuestions");

            migrationBuilder.DropTable(
                name: "Discussion_forum");

            migrationBuilder.DropTable(
                name: "Collaborative");

            migrationBuilder.DropTable(
                name: "Learning_goal");

            migrationBuilder.DropTable(
                name: "Learning_path");

            migrationBuilder.DropTable(
                name: "Reward");

            migrationBuilder.DropTable(
                name: "Leaderboard");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropTable(
                name: "Instructor");

            migrationBuilder.DropTable(
                name: "Learning_activities");

            migrationBuilder.DropTable(
                name: "Survey");

            migrationBuilder.DropTable(
                name: "Quest");

            migrationBuilder.DropTable(
                name: "PersonalizationProfiles");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Learner");

            migrationBuilder.DropTable(
                name: "Course");
        }
    }
}
