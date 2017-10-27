create table Department(
	Id int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(255)
);
insert into Department(Name) values (N'Computer Science and Engineering')
insert into Department(Name) values (N'Information Science and Engineering')

create table Quota(
	Id int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(255)
);
insert into Quota(Name) values (N'CET');
insert into Quota(Name) values (N'COMEDK');

create table Gender(
	Id int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(255)
);
insert into Gender(Name) values (N'Male');
insert into Gender(Name) values (N'Female');
insert into Gender(Name) values (N'Other');

create table Student(
	Id UNIQUEIDENTIFIER DEFAULT newsequentialid() primary key,
	Usn nvarchar(10),
	AadharNo numeric(12,0),
	Name nvarchar(225),
	Gender int foreign key references Gender(Id),
	Dob date,
	AdmissionQuota int FOREIGN KEY REFERENCES Quota(Id),
	Department int FOREIGN KEY REFERENCES department(Id),
	EmailId nvarchar(20),
	PhoneNumber numeric(10,0)
);

create table ParentType (
	Id int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(255),
);
insert into ParentType(Name) values(N'Father');
insert into ParentType(Name) values(N'Mother');
insert into ParentType(Name) values(N'Primary Guardian');
insert into ParentType(Name) values(N'Secondary Guardian');

create table Parent(
	Id UNIQUEIDENTIFIER DEFAULT newsequentialid() primary key,
	StudentId uniqueidentifier FOREIGN KEY REFERENCES Student(Id),
	PType int foreign key references ParentType(Id),
	Name nvarchar(225),
	PhoneNo numeric(10,0),
	EmailId nvarchar(20),
	Qualification nvarchar(10),
	Occupation nvarchar(10),
	AnnualSalary numeric(10,2)
);

create table AddressType (
	Id int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(255)
);
insert into AddressType(Name) values (N'Permanent');
insert into AddressType(Name) values (N'Present');

create table Address(
	Id UNIQUEIDENTIFIER DEFAULT newsequentialid() primary key,
	StudentId uniqueidentifier FOREIGN KEY REFERENCES Student(Id),
	AType int foreign key references AddressType(Id),
	Addr nvarchar(250)
);

create table BoardType(
	Id int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(255)
);
insert into BoardType(Name) values (N'ICSE');
insert into BoardType(Name) values (N'CBSE');
insert into BoardType(Name) values (N'State');
insert into BoardType(Name) values (N'IG');

create table School(
	Id UNIQUEIDENTIFIER DEFAULT newsequentialid() primary key,
	StudentId uniqueidentifier FOREIGN KEY REFERENCES Student(Id),
	Grade numeric(2, 0),
	Name nvarchar(255),
	Board int references BoardType(Id),
	MediumInstruction nvarchar(255),
	IsGPA bit,
	PercentageMarks numeric(3,2)
);

create table AreaInterest(
	Id UNIQUEIDENTIFIER DEFAULT newsequentialid() primary key,
	StudentId uniqueidentifier FOREIGN KEY REFERENCES Student(Id),
	Area nvarchar(10)
);

create table EventType(
	Id int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(255)
);
insert into EventType(Name) values (N'Co curricular');
insert into EventType(Name) values (N'Sports');

create table EventLevel (
	id int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(255)
);
insert into EventLevel(Name) values (N'Inter school');
insert into EventLevel(Name) values (N'Intra school');
insert into EventLevel(Name) values (N'Taluk');
insert into EventLevel(Name) values (N'District');
insert into EventLevel(Name) values (N'State');
insert into EventLevel(Name) values (N'National');
insert into EventLevel(Name) values (N'International');

create table Events(
	Id UNIQUEIDENTIFIER DEFAULT newsequentialid() primary key,
	StudentId uniqueidentifier FOREIGN KEY REFERENCES Student(Id),
	Category int FOREIGN KEY REFERENCES EventType(Id),
	Name nvarchar(255),
	ELevel int foreign key references EventLevel(Id),
	Position numeric(6,0),
	Certified bit,
	CertificateId varchar(255)
);

create table SkillsCategory(
	Id int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(255)
);
insert into SkillsCategory(Name) values (N'Programming');
insert into SkillsCategory(Name) values (N'Literary');

create table Skills(
	Id UNIQUEIDENTIFIER DEFAULT newsequentialid() primary key,
	StudentId uniqueidentifier FOREIGN KEY REFERENCES Student(Id),
	Category int foreign key references SkillsCategory(Id),
);

create table SubSkills(
	Id UNIQUEIDENTIFIER DEFAULT newsequentialid() primary key,
	SkillId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Skills(Id),
	Name nvarchar(255),
	Certified bit,
	CertificateId varchar(255)
);

create table JobProjectsCategory(
	Id int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(255)
);
insert into JobProjectsCategory(Name) values (N'Full Time Job');
insert into JobProjectsCategory(Name) values (N'Internship');
insert into JobProjectsCategory(Name) values (N'Project');

create table JobProjects(
	Id UNIQUEIDENTIFIER DEFAULT newsequentialid() primary key,
	StudentId uniqueidentifier FOREIGN KEY REFERENCES Student(Id),
	Category int foreign key references JobProjectsCategory(Id),
	Name nvarchar(255),
	Descri text,
	Duration numeric(3, 0),
	PaidUnpaid bit,
	Salary numeric(10, 2)
);