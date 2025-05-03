-- Delete existing data from all tables
EXEC sp_MSforeachtable "DELETE FROM ?";

-- Reset all identity columns in all tables to start from 1
EXEC sp_MSforeachtable @command1="DBCC CHECKIDENT ('?', RESEED, 0);"
-- Reset the identity seed for ModuleID to 101
DBCC CHECKIDENT ('Modules', RESEED, 100);
DBCC CHECKIDENT ('PersonalizationProfiles', RESEED, 100);

-- Insert data into Admin table
INSERT INTO Admin (name, email, password) VALUES
('Admin User', 'admin@example.com', 'adminPassword');

-- Insert data into Learner table (LearnerID is auto-generated)
INSERT INTO Learner (first_name, last_name, gender, birth_date, country, cultural_background, email, password) VALUES
('Elsa', 'Werner', 'Female', '2000-05-15', 'USA', 'Western', 'elsa.werner@example.com', 'password123'),
('Ahmed', 'Ali', 'Male', '1999-03-25', 'Egypt', 'Arab', 'ahmed.ali@example.com', 'password123'),
('Layla', 'Hassan', 'Female', '2001-07-20', 'Jordan', 'Arab', 'layla.hassan@example.com', 'password123'),
('Tobi', 'Brown', 'Male', '2002-01-10', 'UK', 'Western', 'tobi.brown@example.com', 'password123'),
('Mariam', 'Zayed', 'Female', '1998-09-05', 'Lebanon', 'Arab', 'mariam.zayed@example.com', 'password123'),
('John', 'Marvin', 'Male', '2001-02-14', 'USA', 'Western', 'john.marvin@example.com', 'password123'),
('Sara', 'Kamel', 'Female', '2000-11-30', 'Egypt', 'Arab', 'sara.kamel@example.com', 'password123');
SELECT * FROM Learner;
-- Insert data into Skills table
INSERT INTO Skills (LearnerID, skill) VALUES
(1, 'Programming'),
(2, 'Algorithms'),
(3, 'Critical Thinking'),
(4, 'Problem Solving'),
(5, 'Leadership'),
(6, 'Machine Learning'),
(7, 'Teamwork');

-- Insert data into LearningPreference table
INSERT INTO LearningPreference (LearnerID, preference) VALUES
(1, 'Visual'),
(2, 'Hands-On'),
(3, 'Reading'),
(4, 'Audio'),
(5, 'Interactive'),
(6, 'Text'),
(7, 'Video');

-- Insert data into PersonalizationProfiles table (ProfileID is auto-generated)
INSERT INTO PersonalizationProfiles (LearnerID, Preferred_content_type, emotional_state, personality_type) VALUES
(1, 'Video', 'Motivated', 'Introvert'),
(2, 'Text', 'Curious', 'Extrovert'),
(3, 'Audio', 'Relaxed', 'Ambivert'),
(4, 'Interactive', 'Motivated', 'Introvert'),
(5, 'Video', 'Motivated', 'Extrovert'),
(6, 'Audio', 'Confident', 'Analytical'),
(7, 'Text', 'Determined', 'Practical');
SELECT * FROM PersonalizationProfiles;
-- Insert data into HealthCondition table
INSERT INTO HealthCondition (LearnerID, ProfileID, condition) VALUES
(1, 101, 'ADHD'),
(2, 102, 'Anxiety'),
(3, 103, 'Stress'),
(4, 104, 'None'),
(5, 105, 'Insomnia'),
(6, 106, 'Migraines'),
(7, 107, 'None');

-- Insert data into Course table (CourseID is auto-generated)
INSERT INTO Course (Title, learning_objective, credit_points, difficulty_level, pre_requisites, description) VALUES
('Introduction to Programming', 'Learn basic programming concepts', 3, 'Beginner', 'None', 'Foundational programming course.'),
('Advanced Algorithms', 'Master algorithmic techniques', 4, 'Advanced', 'Introduction to Programming', 'Focus on algorithm design and optimization.'),
('Data Science', 'Understand data analysis techniques', 4, 'Intermediate', 'Programming Basics', 'Covers data analysis and visualization.'),
('Machine Learning', 'Apply machine learning algorithms', 5, 'Advanced', 'Data Science', 'Focuses on practical ML techniques.'),
('Cybersecurity Basics', 'Learn basic security concepts', 3, 'Beginner', 'None', 'Foundation-level cybersecurity course.'),
('Web Development', 'Build modern web applications', 4, 'Intermediate', 'Programming Basics', 'Focus on front-end and back-end development.'),
('Software Engineering', 'Understand software development lifecycle', 4, 'Intermediate', 'None', 'Covers principles of software engineering.');
INSERT INTO Course (Title, learning_objective, credit_points, difficulty_level, pre_requisites, description) VALUES
('Introduction testttt Programming', 'Learn basic programming concepts', 3, 'Beginner', 'None', 'Foundational programming course.')
-- Insert data into CoursePrerequisite table
INSERT INTO CoursePrerequisite (CourseID, Prereq) VALUES
(2, 1),
(3, 1),
(4, 3),
(6, 1),
(7, 6);

-- Insert data into Modules table (ModuleID is auto-generated starting from 101)
INSERT INTO Modules (CourseID, Title, difficulty, ContentURL) VALUES
(1, 'Programming Basics', 'Beginner', 'http://example.com/programming_basics'),
(2, 'Algorithm Design', 'Advanced', 'http://example.com/algorithm_design'),
(3, 'Data Analysis', 'Intermediate', 'http://example.com/data_analysis'),
(4, 'Machine Learning Applications', 'Advanced', 'http://example.com/ml_applications'),
(5, 'Security Fundamentals', 'Beginner', 'http://example.com/security_fundamentals'),
(6, 'Front-End Development', 'Intermediate', 'http://example.com/frontend_dev'),
(7, 'Software Development Models', 'Intermediate', 'http://example.com/software_models');

-- Insert data into Target_traits table
INSERT INTO Target_traits (ModuleID, CourseID, Trait) VALUES
(101, 1, 'Problem Solving'),
(102, 2, 'Critical Thinking'),
(103, 3, 'Data Analysis'),
(104, 4, 'Machine Learning'),
(105, 5, 'Security Awareness'),
(106, 6, 'Creativity'),
(107, 7, 'Team Collaboration');
SELECT * FROM Modules-- Insert data into ModuleContent table
INSERT INTO ModuleContent (ModuleID, CourseID, Content_type) VALUES
(101, 1, 'Video'),
(102, 2, 'Quiz'),
(103, 3, 'Video'),
(104, 4, 'Text'),
(105, 5, 'Video'),
(106, 6, 'Quiz'),
(107, 7, 'Text');

-- Insert data into ContentLibrary table (ID is auto-generated)
INSERT INTO ContentLibrary (ModuleID, CourseID, Title, description, metadata, type, content_URL) VALUES
(101, 1, 'Intro Video', 'Basic Programming Intro', 'Length: 10min', 'Video', 'http://example.com/intro'),
(102, 2, 'Algorithm Quiz', 'Test Algorithm Understanding', '10 Questions', 'Quiz', 'http://example.com/quiz'),
(103, 3, 'Data Analysis Tutorial', 'Learn Data Visualization', 'Length: 15min', 'Video', 'http://example.com/data_analysis'),
(104, 4, 'ML Guide', 'Understand ML Applications', 'Length: 20min', 'Text', 'http://example.com/ml_guide'),
(105, 5, 'Security Basics', 'Understand Basic Security Concepts', 'Length: 12min', 'Video', 'http://example.com/security_basics'),
(106, 6, 'Web Development Crash Course', 'Front-End Fundamentals', 'Length: 18min', 'Video', 'http://example.com/frontend'),
(107, 7, 'Software Models Overview', 'Learn SDLC Models', 'Length: 15min', 'Text', 'http://example.com/software_models');

-- Insert data into Assessments table (ID is auto-generated)
INSERT INTO Assessments (ModuleID, CourseID, type, total_marks, passing_marks, criteria, weightage, description, title) VALUES
(101, 1, 'Quiz', 100, 50, 'Pass basics quiz', 0.2, 'Programming Quiz', 'Programming Basics Quiz'),
(102, 2, 'Assignment', 150, 70, 'Submit algorithm project', 0.4, 'Algorithm Assignment', 'Algorithm Design Assignment'),
(103, 3, 'Quiz', 120, 60, 'Complete data analysis quiz', 0.3, 'Data Analysis Quiz', 'Data Analysis Basics'),
(104, 4, 'Exam', 200, 100, 'Submit ML project', 0.5, 'Machine Learning Exam', 'ML Applications Exam'),
(105, 5, 'Quiz', 100, 50, 'Pass security basics quiz', 0.2, 'Cybersecurity Quiz', 'Cybersecurity Basics Quiz'),
(106, 6, 'Assignment', 180, 80, 'Submit frontend development assignment', 0.3, 'Web Development Assignment', 'Frontend Basics Assignment'),
(107, 7, 'Exam', 150, 75, 'Pass software engineering test', 0.4, 'Software Engineering Exam', 'Software Engineering Models Exam');

-- Insert data into Takenassessment table
INSERT INTO Takenassessment (AssessmentID, LearnerID, scoredPoint) VALUES
(1, 1, 80),
(2, 2, 70),
(3, 3, 85),
(4, 4, 90),
(5, 5, 75),
(6, 6, 88),
(7, 7, 95);

-- Insert data into Learning_activities table
INSERT INTO Learning_activities (ModuleID, CourseID, activity_type, instruction_details, Max_points) VALUES
(101, 1, 'Quiz', 'Answer All Questions', 100),
(102, 2, 'Project', 'Complete Algorithm Design', 200),
(103, 3, 'Exam', 'Pass Exam with Full Marks', 150),
(104, 4, 'Project', 'Submit Complete ML Model', 250),
(105, 5, 'Quiz', 'Finish Cybersecurity Basics', 100),
(106, 6, 'Assignment', 'Submit Full Assignment', 80),
(107, 7, 'Exam', 'Pass Software Engineering Test', 120);

-- Insert data into Interaction_log table (LogID is auto-generated)
INSERT INTO Interaction_log (activity_ID, LearnerID, Duration, Timestamp, action_type) VALUES
(1, 1, 30, '2024-01-01 10:00:00', 'Viewed'),
(2, 2, 45, '2024-01-02 15:00:00', 'Attempted'),
(3, 3, 20, '2024-01-03 11:00:00', 'Completed'),
(4, 4, 50, '2024-01-04 13:30:00', 'Reviewed'),
(5, 5, 15, '2024-01-05 09:15:00', 'Skipped'),
(6, 6, 40, '2024-01-06 16:45:00', 'Attempted'),
(7, 7, 25, '2024-01-07 14:20:00', 'Viewed');

-- Insert data into Emotional_feedback table (FeedbackID is auto-generated)
INSERT INTO Emotional_feedback (LearnerID, activity_ID, timestamp, emotional_state) VALUES
(1, 1, '2024-01-05 12:00:00', 'Motivated'),
(2, 2, '2024-01-10 14:30:00', 'Stressed'),
(3, 3, '2024-01-15 10:15:00', 'Confident'),
(4, 4, '2024-01-20 16:45:00', 'Motivated'),
(5, 5, '2024-01-25 09:30:00', 'Motivated'),
(6, 6, '2024-01-30 11:50:00', 'Engaged'),
(7, 7, '2024-02-05 13:25:00', 'Determined');

-- Insert data into Learning_path table (pathID is auto-generated)
INSERT INTO Learning_path (LearnerID, ProfileID, completion_status, custom_content, adaptive_rules) VALUES
(1, 101, 'In Progress', 'Custom Content 1', 'Rule 1'),
(2, 102, 'Completed', 'Custom Content 2', 'Rule 2'),
(3, 103, 'In Progress', 'Custom Content 3', 'Rule 3'),
(4, 104, 'Completed', 'Custom Content 4', 'Rule 4'),
(5, 105, 'In Progress', 'Custom Content 5', 'Rule 5'),
(6, 106, 'Completed', 'Custom Content 6', 'Rule 6'),
(7, 107, 'In Progress', 'Custom Content 7', 'Rule 7');

-- Insert data into Instructor table (InstructorID is auto-generated)
INSERT INTO Instructor (name,first_name,last_name, latest_qualification, expertise_area, email, password) VALUES
('Dr. John Smith','John','Smith', 'PhD', 'Computer Science', 'john.smith@example.com', 'password123'),
('Dr. Ahmed Youssef','Ahmed','Youssef', 'PhD', 'Machine Learning', 'ahmed.youssef@example.com', 'password123'),
('Dr. Sarah Johnson','Sarah','Johnson', 'PhD', 'Cybersecurity', 'sarah.johnson@example.com', 'password123'),
('Dr. Charlie Brown','Charlie','Brown', 'PhD', 'Software Engineering', 'charlie.brown@example.com', 'password123'),
('Dr. Layla Hassan','Layla','Hassan', 'PhD', 'Data Science', 'layla.hassan@example.com', 'password123'),
('Dr. Mariam Zayed','Mariam','Zayed', 'PhD', 'Algorithms', 'mariam.zayed@example.com', 'password123'),
('Dr. Alice Doe','Alice','Doe', 'PhD', 'Programming', 'alice.doe@example.com', 'password123');

-- Insert data into Pathreview table
INSERT INTO Pathreview (InstructorID, PathID, feedback) VALUES
(1, 1, 'Great Progress'),
(2, 2, 'Excellent Completion'),
(3, 3, 'Needs Improvement'),
(4, 4, 'Well Done'),
(5, 5, 'Keep Going'),
(6, 6, 'Outstanding Work'),
(7, 7, 'Encouraging Progress');

-- Insert data into Emotionalfeedback_review table
INSERT INTO Emotionalfeedback_review (FeedbackID, InstructorID, feedback) VALUES
(1, 1, 'Well-Handled Stress'),
(2, 2, 'Needs Relaxation'),
(3, 3, 'Good Confidence'),
(4, 4, 'Well-Controlled Emotions'),
(5, 5, 'Motivated Learner'),
(6, 6, 'Highly Engaged'),
(7, 7, 'Great Determination');

-- Insert data into Course_enrollment table (EnrollmentID is auto-generated)
INSERT INTO Course_enrollment (CourseID, LearnerID, completion_date, enrollment_date, status) VALUES
(1, 1, '2024-01-15', '2024-01-01', 'Completed'),
(2, 2, '2024-02-15', '2024-01-15', 'In Progress'),
(3, 3, NULL, '2024-02-01', 'In Progress'),
(4, 4, '2024-03-15', '2024-02-15', 'Completed'),
(5, 5, NULL, '2024-03-01', 'In Progress'),
(6, 6, '2024-04-15', '2024-03-15', 'Completed'),
(7, 7, NULL, '2024-04-01', 'In Progress');
INSERT INTO Course_enrollment (CourseID, LearnerID, completion_date, enrollment_date, status) VALUES
(1, 5, '2024-01-15', '2024-01-01', 'Completed')

-- Insert data into Teaches table
INSERT INTO Teaches (InstructorID, CourseID) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(1, 2),
(5, 4),
(7, 7);

-- Insert data into Leaderboard table (BoardID is auto-generated)
INSERT INTO Leaderboard (season) VALUES
('Winter 2024'),
('Spring 2024'),
('Summer 2024'),
('Fall 2024'),
('Winter 2025'),
('Spring 2025'),
('Summer 2025');

-- Insert data into Ranking table
INSERT INTO Ranking (BoardID, LearnerID, CourseID, rank, total_points) VALUES
(1, 1, 1, 1, 100),
(2, 2, 2, 2, 200),
(3, 3, 3, 3, 150),
(4, 4, 4, 4, 250),
(5, 5, 5, 5, 100),
(6, 6, 6, 6, 180),
(7, 7, 7, 7, 220);

-- Insert data into Learning_goal table (ID is auto-generated)
INSERT INTO Learning_goal (status, deadline, description) VALUES
('Not Started', '2024-03-01', 'Learn Python Basics'),
('In Progress', '2024-04-01', 'Complete ML Foundations'),
('Completed', '2024-05-01', 'Master Data Analysis'),
('Not Started', '2024-06-01', 'Develop Web App'),
('In Progress', '2024-07-01', 'Understand Algorithms'),
('Completed', '2024-08-01', 'Finish Software Engineering Project'),
('In Progress', '2024-09-01', 'Build a Portfolio Website');

-- Insert data into LearnersGoals table
INSERT INTO LearnersGoals (GoalID, LearnerID) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7);

-- Insert data into Survey table (ID is auto-generated)
INSERT INTO Survey (Title) VALUES
('Programming Basics Survey'),
('Algorithm Design Feedback'),
('Data Analysis Insights'),
('ML Applications Review'),
('Cybersecurity Quiz Feedback'),
('Web Development Preferences'),
('Software Engineering Evaluation');

-- Insert data into SurveyQuestions table
INSERT INTO SurveyQuestions (SurveyID, Question) VALUES
(1, 'How clear was the programming content?'),
(2, 'Was the algorithm design project manageable?'),
(3, 'Did you find data analysis techniques useful?'),
(4, 'How practical were ML applications?'),
(5, 'Was the cybersecurity quiz challenging?'),
(6, 'Did you enjoy web development tasks?'),
(7, 'How would you rate the software models module?');

-- Insert data into FilledSurvey table
INSERT INTO FilledSurvey (SurveyID, Question, LearnerID, Answer) VALUES
(1, 'How clear was the programming content?', 1, 'Very Clear'),
(2, 'Was the algorithm design project manageable?', 2, 'Somewhat'),
(3, 'Did you find data analysis techniques useful?', 3, 'Very Useful'),
(4, 'How practical were ML applications?', 4, 'Extremely Practical'),
(5, 'Was the cybersecurity quiz challenging?', 5, 'Moderate'),
(6, 'Did you enjoy web development tasks?', 6, 'Yes, Loved It'),
(7, 'How would you rate the software models module?', 7, 'Excellent');

-- Insert data into Notification table (ID is auto-generated)
INSERT INTO Notification (timestamp, message, urgency_level, ReadStatus) VALUES
('2024-03-01 10:00:00', 'New course available: Python Basics', 'High', 0),
('2024-03-02 14:00:00', 'Assignment deadline approaching', 'Medium', 0),
('2024-03-03 09:00:00', 'Survey invitation: Programming Feedback', 'Low', 1),
('2024-03-04 16:00:00', 'Exam results released', 'High', 0),
('2024-03-05 11:30:00', 'Upcoming seminar on ML', 'Medium', 1),
('2024-03-06 13:45:00', 'New module added: Frontend Basics', 'Low', 0),
('2024-03-07 08:20:00', 'Final project submission deadline', 'High', 1);

-- Insert data into ReceivedNotification table
INSERT INTO ReceivedNotification (NotificationID, LearnerID) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7);

-- Insert data into Badge table (BadgeID is auto-generated)
INSERT INTO Badge (title, description, criteria, points) VALUES
('Python Pro', 'Completed Python Basics', 'Pass Python Quiz', 50),
('Algorithm Ace', 'Mastered Algorithms', 'Submit Algorithm Project', 100),
('Data Analyst', 'Completed Data Analysis Module', 'Finish Data Exam', 75),
('ML Specialist', 'Completed ML Project', 'Submit ML Project', 150),
('Cybersecurity Hero', 'Finished Cybersecurity Quiz', 'Pass Security Quiz', 60),
('Web Developer', 'Completed Frontend Development', 'Finish Frontend Task', 80),
('Software Engineer', 'Passed Software Models Exam', 'Complete SDLC Exam', 100);

-- Insert data into SkillProgression table (ID is auto-generated)
INSERT INTO SkillProgression (proficiency_level, LearnerID, skill_name, timestamp) VALUES
('Beginner', 1, 'Programming', '2024-01-01 10:00:00'),
('Intermediate', 2, 'Algorithms', '2024-01-02 15:00:00'),
('Intermediate', 3, 'Critical Thinking', '2024-01-03 11:00:00'),
('Advanced', 4, 'Problem Solving', '2024-01-04 13:30:00'),
('Advanced', 5, 'Leadership', '2024-01-05 09:15:00'),
('Expert', 6, 'Machine Learning', '2024-01-06 16:45:00'),
('Intermediate', 7, 'Teamwork', '2024-01-07 14:20:00');

-- Insert data into Achievement table (AchievementID is auto-generated)
INSERT INTO Achievement (LearnerID, BadgeID, description, date_earned, type) VALUES
(1, 1, 'Completed Python Basics', '2024-03-10', 'Course Completion'),
(2, 2, 'Mastered Algorithms', '2024-04-15', 'Project Completion'),
(3, 3, 'Completed Data Analysis', '2024-05-20', 'Exam Completion'),
(4, 4, 'Submitted ML Project', '2024-06-25', 'Project Completion'),
(5, 5, 'Finished Security Quiz', '2024-07-30', 'Quiz Completion'),
(6, 6, 'Developed Frontend Project', '2024-08-15', 'Task Completion'),
(7, 7, 'Completed SDLC Exam', '2024-09-10', 'Exam Completion');

-- Insert data into Reward table (RewardID is auto-generated)
INSERT INTO Reward (value, description, type) VALUES
(10.00, 'Python Basics Completion Reward', 'Completion'),
(20.00, 'Algorithm Project Reward', 'Completion'),
(15.00, 'Data Analysis Exam Reward', 'Exam'),
(25.00, 'ML Project Reward', 'Project'),
(12.00, 'Cybersecurity Quiz Reward', 'Quiz'),
(18.00, 'Frontend Development Reward', 'Task'),
(20.00, 'Software Engineering Exam Reward', 'Exam');

-- Insert data into Quest table (QuestID is auto-generated)
INSERT INTO Quest (difficulty_level, criteria, description, title) VALUES
('Beginner', 'Complete Python Quiz', 'Python Basics Completion', 'Python Quest'),
('Advanced', 'Submit Algorithm Project', 'Algorithm Mastery', 'Algorithm Quest'),
('Intermediate', 'Pass Data Analysis Exam', 'Data Proficiency', 'Data Quest'),
('Advanced', 'Submit ML Project', 'ML Expertise', 'ML Quest'),
('Beginner', 'Pass Security Quiz', 'Cybersecurity Hero', 'Security Quest'),
('Intermediate', 'Complete Frontend Task', 'Frontend Developer', 'Web Quest'),
('Intermediate', 'Complete SDLC Exam', 'Software Engineer', 'Engineering Quest');

-- Insert data into Skill_Mastery table
INSERT INTO Skill_Mastery (QuestID, skill) VALUES
(1, 'Programming'),
(2, 'Algorithms'),
(3, 'Data Analysis'),
(4, 'Machine Learning'),
(5, 'Cybersecurity'),
(6, 'Web Development'),
(7, 'Software Engineering');

-- Insert data into Collaborative table
INSERT INTO Collaborative (QuestID, deadline, max_num_participants) VALUES
(1, '2024-04-01 12:00:00', 5),
(2, '2024-05-01 12:00:00', 4),
(3, '2024-06-01 12:00:00', 6),
(4, '2024-07-01 12:00:00', 8),
(5, '2024-08-01 12:00:00', 7),
(6, '2024-09-01 12:00:00', 5),
(7, '2025-10-01 12:00:00', 6);

-- Insert data into LearnersCollaboration table
INSERT INTO LearnersCollaboration (LearnerID, QuestID, completion_status) VALUES
(1, 1, 'Completed'),
(2, 2, 'In Progress'),
(3, 3, 'Completed'),
(4, 4, 'In Progress'),
(5, 5, 'Completed'),
(6, 6, 'In Progress'),
(7, 7, 'Completed');

-- Insert data into LearnersMastery table
INSERT INTO LearnersMastery (LearnerID, QuestID, completion_status) VALUES
(1, 1, 'Completed'),
(2, 2, 'In Progress'),
(3, 3, 'Completed'),
(4, 4, 'In Progress'),
(5, 5, 'Completed'),
(6, 6, 'In Progress'),
(7, 7, 'Completed');

-- Insert data into Discussion_forum table (forumID is auto-generated)
INSERT INTO Discussion_forum (ModuleID, CourseID, title, last_active, timestamp, description) VALUES
(101, 1, 'Programming Basics Discussion', '2024-01-15 10:00:00', '2024-01-10 08:30:00', 'Discussion on basic programming concepts'),
(102, 2, 'Advanced Algorithms Discussion', '2024-02-15 12:00:00', '2024-02-10 10:00:00', 'Discussion on algorithm design and optimization'),
(103, 3, 'Data Analysis Techniques', '2024-03-15 14:30:00', '2024-03-10 12:00:00', 'Discussion on various data analysis techniques'),
(104, 4, 'ML Model Applications', '2024-04-15 16:00:00', '2024-04-10 13:00:00', 'Discussion on machine learning model applications'),
(105, 5, 'Cybersecurity Basics Discussion', '2024-05-15 18:00:00', '2024-05-10 14:30:00', 'Discussion on basic cybersecurity concepts'),
(106, 6, 'Web Development Practices', '2024-06-15 17:30:00', '2024-06-10 09:30:00', 'Discussion on front-end and back-end development'),
(107, 7, 'Software Engineering Practices', '2024-07-15 19:00:00', '2024-07-10 10:45:00', 'Discussion on software development lifecycle');

-- Insert data into LearnerDiscussion table
INSERT INTO LearnerDiscussion (ForumID, LearnerID, Post, time) VALUES
(1, 1, 'Can anyone explain the concept of loops in programming?', '2024-01-11 09:00:00'),
(2, 2, 'What are some efficient algorithms for sorting large datasets?', '2024-02-12 11:30:00'),
(3, 3, 'How do you interpret data analysis results for business decisions?', '2024-03-13 15:30:00'),
(4, 4, 'What machine learning models are most suitable for classification?', '2024-04-14 17:00:00'),
(5, 5, 'What are the best practices for securing an e-commerce site?', '2024-05-16 09:15:00'),
(6, 6, 'What tools can be used for both front-end and back-end development?', '2024-06-17 14:45:00'),
(7, 7, 'Can anyone explain the concept of agile development?', '2024-07-18 10:30:00');

-- Insert data into QuestReward table
INSERT INTO QuestReward (RewardID, QuestID, LearnerID, Time_earned) VALUES
(1, 1, 1, '2024-03-01 12:00:00'),
(2, 2, 2, '2024-04-01 14:00:00'),
(3, 3, 3, '2024-05-01 16:00:00'),
(4, 4, 4, '2024-06-01 18:00:00'),
(5, 5, 5, '2024-07-01 10:00:00'),
(6, 6, 6, '2024-08-01 12:00:00'),
(7, 7, 7, '2024-09-01 14:00:00');
