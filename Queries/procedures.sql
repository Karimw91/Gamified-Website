

-- procedure number 1
﻿USE milestonee2;
 
GO
CREATE PROCEDURE FindModules 
(
    @CourseID AS INT
)
AS
BEGIN
    SELECT *
    From Modules m
    WHERE m.CourseID = @CourseID
END

drop procedure Courseregister
go
CREATE PROCEDURE Courseregister (
    @LearnerID INT,
    @CourseID INT )
AS
BEGIN
    DECLARE @PrereqCount INT;

    SELECT @PrereqCount = COUNT(*)
    FROM CoursePrerequisite CP
    INNER JOIN Course_enrollment CE ON CE.CourseID = CP.Prereq
    WHERE CE.LearnerID = @LearnerID
      AND CP.CourseID = @CourseID;

    IF @PrereqCount > 0
    BEGIN
        INSERT INTO Course_enrollment (CourseID, LearnerID, enrollment_date, status)
        VALUES (@CourseID, @LearnerID, GETDATE(), 'In Progress');

        PRINT 'You have successfully registered for the course.';
    END
    ELSE
    BEGIN
        PRINT 'You cannot register for this course as the prerequisites have not been completed.';
    END
END;
GO

EXEC Courseregister @LearnerID = 1, @CourseID = 1;


-- procedure number 1
go
CREATE PROCEDURE ViewInfo --DONE
    @LearnerID INT
AS
BEGIN 
    SELECT * 
    FROM Learner
    WHERE LearnerID = @LearnerID;
END;
EXEC ViewInfo @LearnerID = 1;

GO
-- procedure number 2
CREATE PROCEDURE LearnerInfo --DONE
    @LearnerID INT
AS
BEGIN
    SELECT *
    FROM PersonalizationProfiles
    WHERE LearnerID = @LearnerID;
END;
EXEC  LearnerInfo @LearnerID = 1;

GO


-- procedure number 3
CREATE PROCEDURE EmotionalState --DONE
    @LearnerID INT,
    @emotional_state VARCHAR(50) OUTPUT
AS
BEGIN
   
    SELECT TOP 1 @emotional_state = emotional_state
    FROM PersonalizationProfiles
    WHERE LearnerID = @LearnerID
    ORDER BY ProfileID DESC;

END;
    DECLARE @emotional_state VARCHAR(50);
EXEC EmotionalState @LearnerID = 1, @emotional_state = @emotional_state OUTPUT;
PRINT @emotional_state;

GO



-- procedure number 4
CREATE PROCEDURE LogDetails --DONE
    @LearnerID INT
   AS
    BEGIN
SELECT LogID,Duration, Timestamp, action_type    FROM Interaction_log
    WHERE LearnerID = @LearnerID
    ORDER BY TimeStamp DESC; 
 END;
 EXEC LogDetails @LearnerID = 1;
 GO
 -- procedure number 5
 CREATE PROCEDURE InstructorReview --DONE
 @InstructorID int
 AS
 BEGIN
 SELECT *
 FROM Emotionalfeedback_review
 WHERE InstructorID = @InstructorID;
 END;
  EXEC InstructorReview @InstructorID = 4;

 GO
 --procedure number 6
 CREATE PROCEDURE CourseRemove --DONE
    @courseID INT
AS
BEGIN
   
    IF NOT EXISTS (SELECT 1 FROM Course WHERE CourseID = @courseID)
    BEGIN
        PRINT 'CourseID does not exist.';
        RETURN; 
    END

    
    DELETE FROM CoursePrerequisite
    WHERE CourseID = @courseID OR Prereq = @courseID;

   
    DELETE FROM Course
    WHERE CourseID = @courseID;

   
    PRINT 'Course removed successfully.';
END;
SELECT * FROM Course;
EXEC CourseRemove @courseID = 2;
EXEC CourseRemove @courseID = 1;



GO


 -- procedure number 7
 
 CREATE PROCEDURE Highestgrade --DONE
 AS
 BEGIN
  SELECT CourseID, MAX(total_marks) AS Course_highest_grade
  FROM Assessments
  GROUP BY CourseID
END;
EXEC Highestgrade;
GO

-- procedure number 8
CREATE PROCEDURE InstructorCount -- DONE
AS
BEGIN
    SELECT 
        C.CourseID AS [Course ID],
        C.Title AS [Course Title],
        COUNT(T.InstructorID) AS [Number of Instructors]
    FROM Course C
    JOIN Teaches T ON C.CourseID = T.CourseID
    GROUP BY C.CourseID, C.Title
    HAVING COUNT(T.InstructorID) > 1;
END;
EXEC InstructorCount;

GO


--procedure number 9
CREATE PROCEDURE ViewNot -- DONE
@LearnerID int 
AS
BEGIN
SELECT *
FROM ReceivedNotification
WHERE LearnerID = @LearnerID;
END;
EXEC ViewNot @LearnerID = 1;
GO

--procedure number 10
CREATE PROCEDURE CreateDiscussion --DONE
    @ModuleID INT,
    @CourseID INT,
    @Title VARCHAR(50),
    @Description VARCHAR(50)
AS
BEGIN
    IF EXISTS (SELECT 1 
               FROM Modules 
               WHERE ModuleID = @ModuleID AND CourseID = @CourseID)
    BEGIN
        INSERT INTO Discussion_forum (ModuleID, CourseID, title, timestamp, last_active, description)
        VALUES (@ModuleID, @CourseID, @Title, GETDATE(), GETDATE(), @Description);

        PRINT 'Discussion forum created successfully.';
    END
    ELSE
    BEGIN
        PRINT 'Error: The specified ModuleID and CourseID do not exist.';
    END
END;
EXEC CreateDiscussion 
    @ModuleID = 101, 
    @CourseID = 1, 
    @Title = 'Programming general Discussion', 
    @Description = 'a place to discuss anything programming related';
    GO

    --procedure number 11
    CREATE PROCEDURE RemoveBadge--DONE
    @BadgeID INT
AS
BEGIN
    
    IF EXISTS (SELECT * FROM Badge WHERE BadgeID = @BadgeID)
    BEGIN
        DELETE FROM Badge
        WHERE BadgeID = @BadgeID;
          DELETE FROM Achievement
          WHERE BadgeID = @BadgeID;
       
        PRINT 'Badge removed successfully';
    END
    ELSE
    BEGIN
       
        PRINT 'Badge not found';
    END
END;
EXEC RemoveBadge @BadgeID = 2;
GO


-- procedure number 12
CREATE PROCEDURE CriteriaDelete --DONE
    @criteria VARCHAR(50)
AS
BEGIN
    DELETE FROM Quest
    WHERE criteria = @criteria;
END;
select * from Quest;
EXEC CriteriaDelete @criteria = 'Complete Python Quiz';

GO
    --procedure number 13
    CREATE PROCEDURE NotificationUpdate -- DONE
    @LearnerID INT,          
    @NotificationID INT,    
    @ReadStatus BIT          
AS
BEGIN
    
    IF EXISTS (
        SELECT 1
        FROM ReceivedNotification RN
        WHERE RN.NotificationID = @NotificationID AND RN.LearnerID = @LearnerID
    )
    BEGIN
      
        UPDATE Notification
        SET ReadStatus = @ReadStatus
        WHERE ID = @NotificationID;

        PRINT 'Notification updated successfully.';
    END
    ELSE
    BEGIN
        PRINT 'Notification not found for the specified learner.';
    END
END;
EXEC NotificationUpdate @LearnerID = 1, @NotificationID = 1, @ReadStatus = 1;
EXEC NotificationUpdate @LearnerID = 1, @NotificationID = 1, @ReadStatus = 0;
SELECT * 
FROM Notification
WHERE ID = 1;
GO
 --procedure number 14
CREATE PROCEDURE EmotionalTrendAnalysis -- DONE
    @CourseID INT,
    @ModuleID INT,
    @TimePeriod DATETIME
AS
BEGIN
    SELECT 
        EF.LearnerID AS [Learner ID],
        EF.timestamp AS [Feedback Timestamp],
        EF.emotional_state AS [Emotional State]
    FROM 
        Emotional_feedback EF
    INNER JOIN 
        Learning_activities LA ON EF.activity_ID = LA.ActivityID
    WHERE 
        LA.CourseID = @CourseID 
        AND LA.ModuleID = @ModuleID
        AND EF.timestamp >= @TimePeriod
    ORDER BY 
        EF.LearnerID, EF.timestamp;
END;
SELECT * FROM Emotional_feedback;
EXEC EmotionalTrendAnalysis @CourseID = 1, @ModuleID = 101, @TimePeriod = '2024-01-01';
GO


--procedure number 15
CREATE PROCEDURE ProfileUpdate --DONE
@LearnerID int,
@ProfileID int,
@Preferred_content_type varchar(50),
@emotional_state varchar(50),
@personality_type varchar(50)
AS
BEGIN
 UPDATE PersonalizationProfiles
 SET Preferred_content_type = @Preferred_content_type,
	 emotional_state = @emotional_state,
	 personality_type = @personality_type
 WHERE LearnerID = @LearnerID AND ProfileID = @ProfileID;
 END;
 SELECT * FROM PersonalizationProfiles;
EXEC ProfileUpdate 
    @LearnerID = 1, 
    @ProfileID = 101, 
    @Preferred_content_type = 'Text', 
    @emotional_state = 'Focused', 
    @personality_type = 'Analytical'; 
    GO

    --procedure number 16
CREATE PROCEDURE TotalPoints --DONE
    @LearnerID INT,
    @RewardType VARCHAR(50)
AS
BEGIN
    SELECT 
        SUM(R.value) AS TotalPoints
    FROM Reward R
    JOIN QuestReward QR ON R.RewardID = QR.RewardID
    WHERE QR.LearnerID = @LearnerID
      AND R.type = @RewardType;
END;

EXEC TotalPoints @LearnerID = 1, @RewardType = 'Completion';
GO
--procedure number 17
CREATE PROCEDURE EnrolledCourses --DONE
    @LearnerID INT
AS
BEGIN
    SELECT
        CE.CourseID,
        C.Title,
        C.learning_objective,
        C.credit_points,
        C.difficulty_level,
        C.pre_requisites,
        C.description
    FROM 
        Course_enrollment CE
        INNER JOIN Course C ON CE.CourseID = C.CourseID
    WHERE 
        CE.LearnerID = @LearnerID
END;

EXEC EnrolledCourses @LearnerID = 1;
GO

--procedure number 18
CREATE PROCEDURE Prerequisites(
    @LearnerID INT,
    @CourseID INT,
    @PrerequisiteStatus VARCHAR(50) OUTPUT
)
AS
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM CoursePrerequisite CP
        LEFT JOIN Course_enrollment CE ON CP.Prereq = CE.CourseID AND CE.LearnerID = @LearnerID
        WHERE CP.CourseID = @CourseID AND CE.Completion_date IS NULL
    )
    BEGIN
        SET @PrerequisiteStatus = 'Completed';
    END
    ELSE
    BEGIN
        SET @PrerequisiteStatus = 'Not Completed';
    END;
END;

DECLARE @PrerequisiteStatus VARCHAR(50);
EXEC Prerequisites @LearnerID = 202, @CourseID = 101, @PrerequisiteStatus = @PrerequisiteStatus OUTPUT;
SELECT @PrerequisiteStatus AS PrerequisiteStatus;
GO

 --procedure number 19
CREATE PROCEDURE Moduletraits( -- DONE
    @TargetTrait VARCHAR(50),
    @CourseID INT
)
AS
BEGIN
    SELECT M.ModuleID, M.Title, M.difficulty, M.ContentURL
    FROM Modules M
    JOIN Target_traits TT ON M.ModuleID = TT.ModuleID AND M.CourseID = TT.CourseID
    WHERE TT.Trait = @TargetTrait AND M.CourseID = @CourseID;
END;

EXEC Moduletraits @TargetTrait = 'Creativity', @CourseID = 6;
GO

 --procedure number 20
CREATE PROCEDURE LeaderboardRank(-- DONE
    @LeaderboardID INT
)
AS
BEGIN
    SELECT R.LearnerID, L.first_name, L.last_name, R.rank, R.total_points
    FROM Ranking R
    JOIN Learner L ON R.LearnerID = L.LearnerID
    WHERE R.BoardID = @LeaderboardID
    ORDER BY R.rank ASC;
END;

EXEC LeaderboardRank @LeaderboardID = 1;
GO

 --procedure number 21
CREATE PROCEDURE ActivityEmotionalFeedback --DONE
    @ActivityID INT,
    @LearnerID INT,
    @timestamp DATETIME,
    @emotionalstate VARCHAR(50)
AS
BEGIN
    INSERT INTO Emotional_feedback (FeedbackID, LearnerID, activity_ID, timestamp, emotional_state)
    VALUES ((SELECT ISNULL(MAX(FeedbackID), 0) + 1 FROM Emotional_feedback),@LearnerID,@ActivityID,@timestamp,@emotionalstate);
END;
EXEC ActivityEmotionalFeedback @ActivityID = 1 , @LearnerID = 1, @timestamp = '2024-11-20 15:30:00', @emotionalstate = 'Motivated';
GO
 --procedure number 22
CREATE PROCEDURE JoinQuest --DONE
    @LearnerID INT,
    @QuestID INT
AS
BEGIN
    DECLARE @MaxParticipants INT, @CurrentParticipants INT;

    SELECT @MaxParticipants = max_num_participants
    FROM Collaborative
    WHERE QuestID = @QuestID;

    SELECT @CurrentParticipants = COUNT(*) 
    FROM LearnersCollaboration
    WHERE QuestID = @QuestID;

    IF EXISTS (SELECT 1 FROM LearnersCollaboration WHERE LearnerID = @LearnerID AND QuestID = @QuestID)
    BEGIN
        PRINT 'You are already a member of this quest. Your participation request is rejected.';
        RETURN;
    END

    IF @CurrentParticipants >= @MaxParticipants
    BEGIN
        PRINT 'The quest is full. Your participation request is rejected.';
        RETURN;
    END

    INSERT INTO LearnersCollaboration (LearnerID, QuestID, completion_status)
    VALUES (@LearnerID, @QuestID, 'In Progress');

    PRINT 'You have successfully joined the quest. Welcome!';
END;
EXEC JoinQuest @LearnerID = 1, @QuestID = 3;
GO

-- procedure number 23
CREATE PROCEDURE SkillsProfeciency(--DONE
    @LearnerID INT
)
AS
BEGIN
    SELECT skill_name, proficiency_level, timestamp
    FROM SkillProgression
    WHERE LearnerID = @LearnerID;
END;

EXEC SkillsProfeciency @LearnerID = 2;
GO

 --procedure number 24
CREATE PROCEDURE Viewscore --DONE
    @LearnerID INT,
    @AssessmentID INT,
    @score INT OUTPUT
AS
BEGIN
    SELECT @score = scoredPoint
    FROM TakenAssessment
    WHERE LearnerID = @LearnerID AND AssessmentID = @AssessmentID;
END;
DECLARE @score INT;
EXEC Viewscore @LearnerID = 1, @AssessmentID = 1, @score = @score OUTPUT;
PRINT @score;
GO
-- procedure number 25
CREATE PROCEDURE AssessmentsList(-- DONE
    @CourseID INT,
    @ModuleID INT,
    @LearnerID INT
)
AS
BEGIN
    SELECT 
        A.title AS AssessmentTitle,  
        TA.scoredPoint AS ScoredPoints,  
        A.total_marks AS TotalMarks, 
        A.passing_marks AS PassingMarks, 
        CASE 
            WHEN TA.scoredPoint >= A.passing_marks THEN 'Passed'
            ELSE 'Failed'
        END AS Grade  
    FROM 
        Assessments A
    JOIN 
        Takenassessment TA 
    ON 
        A.ID = TA.AssessmentID
    WHERE 
        A.CourseID = @CourseID 
        AND A.ModuleID = @ModuleID 
        AND TA.LearnerID = @LearnerID; 
END;
EXEC AssessmentsList @CourseID = 1, @ModuleID = 101, @LearnerID = 1; 
GO

-- procedure number 26
GO
CREATE PROCEDURE Courseregister(@LearnerID INT, @CourseID INT)
AS
BEGIN
    DECLARE @preq INT
    DECLARE @preqmet INT

    SELECT @preq = COUNT(*)
    FROM CoursePrerequisite p
    WHERE p.CourseID = @CourseID

    SELECT @preqmet = COUNT(*)
    FROM Course_enrollment ce 
    INNER JOIN CoursePrerequisite p ON ce.CourseID = p.Prereq
    WHERE p.CourseID = @CourseID 
      AND ce.LearnerID = @LearnerID 
      AND ce.status = 'Completed'

    IF @preq = 0
    BEGIN
        INSERT INTO Course_enrollment (CourseID, LearnerID, enrollment_date, status)
        VALUES (@CourseID, @LearnerID, GETDATE(), 'Enrolled')
        SELECT 'Registration approved' AS Message
    END
    ELSE IF @preqmet = @preq
    BEGIN
        INSERT INTO Course_enrollment (CourseID, LearnerID, enrollment_date, status)
        VALUES (@CourseID, @LearnerID, GETDATE(), 'Enrolled')
        SELECT 'Registration approved' AS Message
    END
    ELSE
    BEGIN
        SELECT 'Registration Failed, You did not complete all prerequisites of this course' AS Message
    END
END


drop procedure Courseregister
exec Courseregister 5, 1
-- procedure number 27
go
CREATE PROCEDURE Post --DONE
    @LearnerID INT,              
    @DiscussionID INT,         
    @Post VARCHAR(MAX)         
AS
BEGIN
  
    IF EXISTS (SELECT 1 FROM Discussion_forum WHERE forumID = @DiscussionID)
    BEGIN
       
        INSERT INTO LearnerDiscussion (ForumID, LearnerID, Post, time)
        VALUES (@DiscussionID, @LearnerID, @Post, GETDATE()); 

        PRINT 'Post added successfully.';
    END
    ELSE
    BEGIN
        PRINT 'Discussion forum not found.';
    END
END;
SELECT * FROM LearnerDiscussion;
EXEC Post @LearnerID = 1, @DiscussionID = 1, @Post = 'This2 is a test post.';
GO

 -- procedure number 28
CREATE PROCEDURE AddGoal --DONE
    @LearnerID INT,       
    @GoalID INT          
AS
BEGIN
  
    IF EXISTS (SELECT 1 FROM Learner WHERE LearnerID = @LearnerID)
    AND EXISTS (SELECT 1 FROM Learning_goal WHERE ID = @GoalID)
    BEGIN
        INSERT INTO LearnersGoals (GoalID, LearnerID)
        VALUES (@GoalID, @LearnerID);
        END
     ELSE
    BEGIN
        PRINT 'either LearnerID or GoalID do not exist';
    END
END;
SELECT * FROM LearnersGoals
EXEC AddGoal @LearnerID = 1, @GoalID = 3;
EXEC AddGoal @LearnerID = 99, @GoalID = 1;
EXEC AddGoal @LearnerID = 1, @GoalID = 99;
GO
 -- procedure number 29
CREATE PROCEDURE CurrentPath(-- DONE
    @LearnerID INT
)
AS
BEGIN
    SELECT LP.pathID, LP.completion_status, LP.custom_content, LP.adaptive_rules
    FROM Learning_path LP
    WHERE LP.LearnerID = @LearnerID;
END;

EXEC CurrentPath @LearnerID = 2;
GO
 -- procedure number 30
CREATE PROCEDURE QuestMembers -- DONE
    @LearnerID INT
AS
BEGIN
    SELECT C.QuestID, LC.LearnerID AS ParticipantLearnerID
    FROM Collaborative C
    INNER JOIN LearnersCollaboration LC ON C.QuestID = LC.QuestID
    WHERE LC.QuestID IN (
        SELECT QuestID
        FROM LearnersCollaboration
        WHERE LearnerID = @LearnerID
    )
    AND C.deadline > GETDATE()
    ORDER BY C.QuestID, ParticipantLearnerID;
END;
EXEC QuestMembers @LearnerID = 7;
SELECT * FROM LearnersCollaboration
GO

 -- procedure number 31
CREATE PROCEDURE QuestProgress --DONE
    @LearnerID INT
AS
BEGIN
   
    SELECT 
        Q.QuestID,
        Q.title AS QuestTitle,
        LC.completion_status AS QuestCompletionStatus
    FROM 
        LearnersCollaboration LC
    INNER JOIN 
        Quest Q ON LC.QuestID = Q.QuestID
    WHERE 
        LC.LearnerID = @LearnerID
        AND LC.completion_status = 'In Progress';  

    SELECT 
        B.BadgeID,
        B.title AS BadgeTitle,
        A.date_earned AS DateEarned
    FROM 
        Achievement A
    INNER JOIN 
        Badge B ON A.BadgeID = B.BadgeID
    WHERE 
        A.LearnerID = @LearnerID;
END;
EXEC QuestProgress @LearnerID = 1;
GO
    -- procedure number 32
CREATE PROCEDURE GoalReminder --DONE
    @LearnerID INT,
    @GoalID INT
AS
BEGIN
    DECLARE @GoalStatus VARCHAR(50);
    DECLARE @Deadline DATE;
    DECLARE @Message VARCHAR(255);

    SELECT @GoalStatus = LG.status, @Deadline = LG.deadline
    FROM Learning_goal LG
    JOIN LearnersGoals LGs ON LG.ID = LGs.GoalID
    WHERE LGs.GoalID = @GoalID AND LGs.LearnerID = @LearnerID;

    IF @GoalStatus != 'Completed' AND @Deadline <= GETDATE() + 7
    BEGIN
        IF @Deadline < GETDATE()
        BEGIN
            SET @Message = 'Your learning goal is overdue. Please review and prioritize completing it.';
        END
        ELSE
        BEGIN
            SET @Message = 'Your learning goal deadline is approaching in less than a week. Stay on track!';
        END

        PRINT @Message;
    END
    ELSE
    BEGIN
        PRINT 'Your learning goal is completed.';
    END
END;
EXEC GoalReminder @LearnerID = 1, @GoalID = 1;
EXEC GoalReminder @LearnerID = 3, @GoalID = 3;

GO

 -- procedure number 33
CREATE PROCEDURE SkillProgressHistory --DONE
    @LearnerID INT,
    @Skill VARCHAR(50)
AS
BEGIN
    SELECT 
        timestamp AS [Date],
        proficiency_level AS [Proficiency Level]
    FROM SkillProgression
    WHERE LearnerID = @LearnerID AND skill_name = @Skill
    ORDER BY timestamp ASC;
END;
EXEC SkillProgressHistory @LearnerID = 1, @Skill = 'Programming';

GO

 -- procedure number 34
CREATE PROCEDURE AssessmentAnalysis -- DONE
    @LearnerID INT

AS
BEGIN
    SELECT 
        A.title AS [Assessment Title],
        A.type AS [Assessment Type],
        A.total_marks AS [Total Marks],
        A.passing_marks AS [Passing Marks],
        T.scoredPoint AS [Learner Score],
        (CAST(T.scoredPoint AS FLOAT) / A.total_marks) * 100 AS [Percentage],
        CASE 
            WHEN T.scoredPoint < A.passing_marks THEN 'Weakness: Did not meet the passing criteria.'
            ELSE 'Strength: Passed the assessment.'
        END AS [Performance Analysis]
    FROM Takenassessment T INNER JOIN Assessments A on T.AssessmentID = A.ID  
   
    WHERE T.LearnerID = @LearnerID

END;
EXEC AssessmentAnalysis @LearnerID = 4

GO
  -- procedure number 35
CREATE PROCEDURE LeaderboardFilter(--DONE
    @LearnerID INT
)
AS
BEGIN
    SELECT R.BoardID, R.rank, R.total_points, L.season
    FROM Ranking R
    JOIN Leaderboard L ON R.BoardID = L.BoardID
    WHERE R.LearnerID = @LearnerID
    ORDER BY R.rank DESC;
END;

EXEC LeaderboardFilter @LearnerID = 2;
GO

 -- procedure number 36
CREATE PROCEDURE SkillLearners -- DONE
    @Skillname VARCHAR(50)
AS
BEGIN
    SELECT 
        S.skill AS [Skill Name],
        L.LearnerID AS [Learner ID],
        CONCAT(L.first_name, ' ', L.last_name) AS [Learner Name],
        L.country AS [Country],
        L.cultural_background AS [Cultural Background]
    FROM Skills S
    JOIN Learner L ON S.LearnerID = L.LearnerID
    WHERE S.skill = @Skillname;
END;
EXEC SkillLearners @Skillname = 'Programming';
GO

  -- procedure number 37
CREATE PROCEDURE NewActivity -- DONE
    @CourseID INT,                  
    @ModuleID INT,                  
    @activitytype VARCHAR(50),      
    @instructiondetails VARCHAR(MAX), 
    @maxpoints INT                  
AS
BEGIN
  
    INSERT INTO Learning_activities (ModuleID, CourseID, activity_type, instruction_details, Max_points)
    VALUES (@ModuleID, @CourseID, @activitytype, @instructiondetails, @maxpoints);
END;
SELECT * FROM Learning_activities;
EXEC NewActivity @CourseID = 1, @ModuleID = 101, @activitytype = 'Assignment', @instructiondetails = 'Answer all questions', @maxpoints = 10;

GO

 -- procedure number 38
CREATE PROCEDURE NewAchievement --DONE
    @LearnerID INT,
    @BadgeID INT,
    @description VARCHAR(MAX),
    @date_earned DATE,
    @type VARCHAR(50)
AS
BEGIN
    DECLARE @NewID INT;
    SELECT @NewID = ISNULL(MAX(AchievementID), 0) + 1 FROM Achievement;
    INSERT INTO Achievement (AchievementID, LearnerID, BadgeID, description, date_earned, type)
    VALUES (@NewID, @LearnerID, @BadgeID, @description, @date_earned, @type);
END;
EXEC NewAchievement @LearnerID = 3, @BadgeID = 5, @description = 'Completed the Advanced Coding Challenge', @date_earned = '2024-11-21', @type = 'Skill';
SELECT * FROM Achievement
GO
  -- procedure number 39
CREATE PROCEDURE LearnerBadge--DONE
    @BadgeID INT
AS
BEGIN
    SELECT LearnerID
    FROM Achievement
    WHERE BadgeID = @BadgeID;
END;
EXEC LearnerBadge @BadgeID = 5;
GO

 -- procedure number 40
CREATE PROCEDURE NewPath -- DONE
    @LearnerID INT,
    @ProfileID INT,
    @completion_status VARCHAR(50),
    @custom_content VARCHAR(MAX),
    @adaptiverules VARCHAR(MAX)
AS
BEGIN
    INSERT INTO Learning_path (LearnerID, ProfileID, completion_status, custom_content, adaptive_rules)
    VALUES (@LearnerID, @ProfileID, @completion_status, @custom_content, @adaptiverules);
END;
SELECT * FROM Learning_path;
EXEC NewPath 
    @LearnerID = 1, 
    @ProfileID = 101, 
    @completion_status = 'In Progress', 
    @custom_content = 'Custom content for this learner', 
    @adaptiverules = 'Rule 1, Rule 2';

GO

 -- procedure number 41
CREATE PROCEDURE TakenCourses --DONE
    @LearnerID INT
AS
BEGIN
    SELECT 
        C.CourseID,
        C.Title AS CourseTitle,
        C.learning_objective,
        C.credit_points,
        C.difficulty_level,
        C.pre_requisites,
        C.description
    FROM Course_enrollment CE
    INNER JOIN Course C ON CE.CourseID = C.CourseID
    WHERE CE.LearnerID = @LearnerID
    
END;
EXEC TakenCourses @LearnerID = 5;
GO

 -- procedure number 42
CREATE PROCEDURE CollaborativeQuest -- DONE
    @difficulty_level VARCHAR(50),
    @criteria VARCHAR(50),
    @description VARCHAR(50),
    @title VARCHAR(50),
    @Maxnumparticipants INT,
    @deadline DATETIME
AS
BEGIN
    INSERT INTO Quest (difficulty_level, criteria, description, title)
    VALUES (@difficulty_level, @criteria, @description, @title);
    
    DECLARE @QuestID INT;
    SET @QuestID = SCOPE_IDENTITY();

    INSERT INTO Collaborative (QuestID, deadline, max_num_participants)
    VALUES (@QuestID, @deadline, @Maxnumparticipants);
    
    PRINT 'Collaborative quest has been successfully added.';
END;
EXEC CollaborativeQuest @difficulty_level = 'Hard', @criteria = 'Complete Python Quiz', @description = 'A challenging quest for Python enthusiasts', @title = 'Python Challenge', @Maxnumparticipants = 5, @deadline = '2024-12-31 23:59:59';
GO

 -- Procedure number 43
CREATE PROCEDURE DeadlineUpdate -- DONE
    @QuestID INT,
    @deadline DATETIME
AS
BEGIN
    UPDATE Collaborative
    SET deadline = @deadline
    WHERE QuestID = @QuestID;
END;
EXEC DeadlineUpdate @QuestID = 2, @deadline = '2024-05-01 13:00:00';
GO
 -- Procedure number 44
CREATE PROCEDURE GradeUpdate--DONE
    @LearnerID INT,
    @AssessmentID INT,
    @points INT
AS
BEGIN
    UPDATE Takenassessment
    SET scoredPoint = @points
    WHERE LearnerID = @LearnerID AND AssessmentID = @AssessmentID;
    PRINT 'Grade updated successfully.';
END;
EXEC GradeUpdate @LearnerID = 3, @AssessmentID = 3, @points = 82;
GO
 -- Procedure number 45
CREATE PROCEDURE AssessmentNot --DONE
    @NotificationID INT, 
    @timestamp DATETIME, 
    @message VARCHAR(MAX), 
    @urgencylevel VARCHAR(50), 
    @LearnerID INT
AS
BEGIN
    INSERT INTO Notification (ID, timestamp, message, urgency_level)
    VALUES (@NotificationID, @timestamp, @message, @urgencylevel);
    
    INSERT INTO ReceivedNotification (NotificationID, LearnerID)
    VALUES (@NotificationID, @LearnerID);

    PRINT 'Notification sent successfully.';
END;
EXEC AssessmentNot @NotificationID = 9, @timestamp = '2024-11-20 15:30:00', @message = 'You have a new assessment to complete.', @urgencylevel = 'High', @LearnerID = 1;
GO

  -- Procedure number 46
CREATE PROCEDURE NewGoal --DONE
    @GoalID INT,
    @status VARCHAR(MAX),
    @deadline DATETIME,
    @description VARCHAR(MAX)
AS
BEGIN
    INSERT INTO Learning_goal (ID, status, deadline, description)
    VALUES (@GoalID, @status, @deadline, @description);
    
END;
SELECT * FROM Learning_goal;
EXEC NewGoal @GoalID = 8, @status = 'In Progress', @deadline = '2024-12-31', @description = 'Complete the intermediate coding challenge';

GO
  -- Procedure number 47
CREATE PROCEDURE LearnersCoutrses--DONE
    @CourseID INT,
    @InstructorID INT
AS
BEGIN
    SELECT L.LearnerID,C.Title
    FROM Learner L INNER JOIN Course_enrollment CE ON L.LearnerID = CE.LearnerID INNER JOIN Course C ON CE.CourseID = C.CourseID
    INNER JOIN Teaches T ON C.CourseID = T.CourseID
    WHERE T.InstructorID = @InstructorID AND C.CourseID = @CourseID;
END;
EXEC LearnersCoutrses  @CourseID = 3, @InstructorID = 3;

GO

-- Procedure number 48
CREATE PROCEDURE LastActive --DONE
    @ForumID INT, 
    @lastactive DATETIME OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Discussion_forum WHERE forumID = @ForumID)
    BEGIN
        SELECT @lastactive = last_active
        FROM Discussion_forum
        WHERE forumID = @ForumID;
    END
    ELSE
    BEGIN
        SET @lastactive = NULL;
        PRINT 'ForumID does not exist.';
    END
END;
DECLARE @lastactive DATETIME;
EXEC LastActive @ForumID = 1, @lastactive = @lastactive OUTPUT;
PRINT @lastactive;
GO

 -- Procedure number 49
CREATE PROCEDURE CommonEmotiobnalState--DONE
    @state VARCHAR(50) OUTPUT
AS
BEGIN
    SELECT TOP 1 @state = emotional_state
    FROM Emotional_feedback
    GROUP BY emotional_state
    ORDER BY COUNT(*) DESC;
END;
DECLARE @state VARCHAR(50);
EXEC CommonEmotiobnalState @state = @state OUTPUT;
PRINT @state;
GO

 -- Procedure number 50
 CREATE PROCEDURE ModuleDifficulty(--done 
    @CourseID INT
)
AS
BEGIN
    SELECT ModuleID, Title, difficulty, ContentURL
    FROM Modules
    WHERE CourseID = @CourseID
    ORDER BY 
        CASE 
            WHEN difficulty = 'Easy' THEN 1
            WHEN difficulty = 'Medium' THEN 2
            WHEN difficulty = 'Hard' THEN 3
            ELSE 4 -- For any undefined difficulty levels
        END ASC;
END;
EXEC ModuleDifficulty @CourseID = 2;
GO

  -- Procedure number 51
CREATE PROCEDURE ProfeciencyLevel--DONE
    @LearnerID INT,
    @Skill VARCHAR(50) OUTPUT
AS
BEGIN
    SELECT TOP 1 @Skill = skill_name
    FROM SkillProgression
    WHERE LearnerID = @LearnerID
    ORDER BY 
        CASE 
            WHEN proficiency_level = 'Expert' THEN 1
            WHEN proficiency_level = 'Advanced' THEN 2
            WHEN proficiency_level = 'Intermediate' THEN 3
            WHEN proficiency_level = 'Beginner' THEN 4
            ELSE 5
        END;
END;
DECLARE @Skill VARCHAR(50);
EXEC ProfeciencyLevel @LearnerID = 1, @Skill = @Skill OUTPUT;
PRINT @Skill;
GO

  -- Procedure number 52
CREATE PROCEDURE ProfeciencyUpdate--DONE
    @Skill VARCHAR(50),
    @LearnerID INT,
    @Level VARCHAR(50)
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM SkillProgression
        WHERE LearnerID = @LearnerID AND skill_name = @Skill
    )
    BEGIN
        UPDATE SkillProgression
        SET proficiency_level = @Level,
            timestamp = GETDATE() 
        WHERE LearnerID = @LearnerID AND skill_name = @Skill;
    END
END;
EXEC ProfeciencyUpdate @Skill = 'Programming', @LearnerID = 1, @Level = 'Advanced';
GO
  -- Procedure number 53
CREATE PROCEDURE LeastBadge--DONE
    @LearnerID INT OUTPUT
AS
BEGIN
    SELECT TOP 1 @LearnerID = LearnerID
    FROM Achievement
    GROUP BY LearnerID
    ORDER BY COUNT(BadgeID) ASC;
END;
DECLARE @LeastLearnerID INT;
EXEC LeastBadge @LearnerID = @LeastLearnerID OUTPUT;
SELECT @LeastLearnerID AS LearnerWithLeastBadges;
GO


-- Procedure number 54
CREATE PROCEDURE PreferedType--DONE
    @type VARCHAR(50) OUTPUT
AS
BEGIN
    DECLARE @MostPreferedType VARCHAR(50);
    SELECT 
        TOP 1 
        Preferred_content_type
    FROM 
        PersonalizationProfiles
    GROUP BY 
        Preferred_content_type
    SET @type = @MostPreferedType;
END;
DECLARE @OutputType VARCHAR(50);
EXEC PreferedType @type = @OutputType;
GO
  -- Procedure number 55
CREATE PROCEDURE AssessmentAnalytics--done
    @CourseID INT,
    @ModuleID INT
AS
BEGIN
    SELECT 
        AVG(TA.scoredPoint) AS AverageScore,
        COUNT(TA.LearnerID) AS TotalParticipants
    FROM Takenassessment TA
    INNER JOIN Assessments A ON TA.AssessmentID = A.ID
    WHERE A.CourseID = @CourseID AND A.ModuleID = @ModuleID
    GROUP BY A.CourseID, A.ModuleID;
END;
EXEC AssessmentAnalytics @CourseID = 2 , @ModuleID = 102;
GO

  -- Procedure number 56
CREATE PROCEDURE EmotionalTrendAnalysisIns -- DONE
    @CourseID INT,
    @ModuleID INT,
    @TimePeriod DATETIME
AS
BEGIN
    SELECT 
        EF.LearnerID,
        EF.timestamp AS FeedbackTimestamp,
        EF.emotional_state AS EmotionalState
    FROM 
        Emotional_feedback EF
    INNER JOIN 
        Learning_activities LA ON EF.activity_ID = LA.ActivityID
    WHERE 
        LA.CourseID = @CourseID
        AND LA.ModuleID = @ModuleID
        AND EF.timestamp >= @TimePeriod
    ORDER BY 
        EF.LearnerID, EF.timestamp;
END;
EXEC EmotionalTrendAnalysisIns @CourseID = 1, @ModuleID = 101, @TimePeriod = '2024-01-01';
GO












