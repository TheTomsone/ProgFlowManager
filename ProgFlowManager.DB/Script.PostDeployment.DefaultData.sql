/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT INTO [dbo].[C_CATEGORY] ([c_id],[c_label]) VALUES
(10000,'System'),
    (11000,'Operating System'),
(20000,'Application'),
    (21000,'Graphical Software'),
        (21100,'Video Game'),
            (21101,'Action'),
            (21102,'Adventure'),
            (21103,'RPG'),
            (21104,'Strategy'),
            (21105,'Simulation'),
            (21106,'Sport'),
            (21107,'Racing'),
            (21108,'FPS'),
            (21109,'TPS'),
            (21110,'Shooter'),
            (21111,'Management'),
            (21112,'MMO'),
            (21113,'Coop'),
            (21114,'Solo'),
        (21200,'Game Engine'),
    (22000,'Database Management'),
        (22100, 'Relational Database'),
        (22200, 'NoSQL Database'),
        (22300, 'Object Relational Mapping'),
        (22400, 'Database Administration'),
    (23000,'Web'),
        (23100,'One-Page'),
        (23200,'Multi-Page'),
        (23300,'E-Commerce'),
    (24000,'Word Processors'),
    (25000,'Development Tools'),
    (26000,'Communication Platforms'),
        (26100,'Social Network'),
        (26200,'Crypted Network'),
    (27000,'Cyber-Security'),
        (27100,'Anti-Malware'),
        (27200,'Virtual Private Network'),
        (27300,'Password Manager'),
    (28000,'Media Editors'),
        (28100,'Image Editor'),
        (28200,'Video Editor');

INSERT INTO [dbo].[L_LANGUAGE] ([l_id], [l_label]) VALUES
(10,'C++'),
(20,'C'),
(30,'Assemble'),
(40,'C#'),
    (41, 'Dapper'),
(50,'.NET Core'),
    (51,'Console'),
    (52,'ASP.NET Core'),
    (53,'Entity Framework'),
    (54,'UWP'),
    (55,'Windows Form'),
    (56,'WPF'),
(60,'Typescript'),
    (61,'Angular'),
(70,'Javascript'),
    (71,'Node.js'),
(80,'HTML'),
(90,'CSS'),
(100,'SQL'),
(110,'NoSQL');

INSERT INTO [dbo].[S_STAGE] ([s_id], [s_label]) VALUES
(200,'Developement'),
    (210,'Alpha'),
(300,'Testing'),
    (310,'Beta'),
(400,'Released'),
    (410,'Release Candidate'),
    (420,'Release'),
    (430,'Post-release fixes');


INSERT INTO [dbo].[D_DATA] ([d_name], [d_resume]) VALUES
('ProgFlowManager', 'PFM is a tool for creating roadmap, manage project and team, generate code with ERD/UML diagram,...'),
    ('Database','Implementation of the database for the program API'),
        ('Programs Tables', 'For every programs data, like name, description, date, version, content,...'),
        ('Users Tables', 'For every users data, like name, email, team, role,...'),
    ('Data Access Layer','Implementation of the DAL and services for accessing the database'),
        ('Model','Adding model with same propreties from their tables'),
        ('Services','Implementation of generic base services for accessing data'),
('LazyDataAccess', 'LDA is a framework for easily access any database with reflection'),
    ('Generic Access Layer', 'Implemenation of the generic base class for accessing database'),
        ('Base class','Implementation of base class to access data tables'),
        ('Generic Services', 'Adding generic services for basic CRUD database action'),
        ('Query Provider', 'Implementation of the query provider for accessing database more easily');

INSERT INTO [dbo].[S_SOFTWARE] ([s_id]) VALUES
(1),
(8);

INSERT INTO [dbo].[VN_VERSION_NB] ([vn_id],[vn_major],[vn_minor],[vn_patch],[vn_software_id]) VALUES
(2, 0,0,1, 1),
(5, 0,0,2, 1),
(9, 0,0,1, 8);

INSERT INTO [dbo].[C_CONTENT] ([c_id],[c_version_nb_id]) VALUES
(3,2),
(4,2),
(6,5),
(7,5),
(10,9),
(11,9),
(12,9);

INSERT INTO [dbo].[SC_SOFTWARE_CATEGORY] ([sc_software_id],[sc_category_id]) VALUES
(1,25000),
(1,20000),
(8,22400),
(8,22300),
(8,22200),
(8,22100),
(8,22000),
(8,25000),
(8,20000);

