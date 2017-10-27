namespace RoSAT.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RosatEntities : DbContext
    {
        public RosatEntities()
            : base("name=RosatEntities")
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AddressType> AddressTypes { get; set; }
        public virtual DbSet<AreaInterest> AreaInterests { get; set; }
        public virtual DbSet<BoardType> BoardTypes { get; set; }
        public virtual DbSet<Choice> Choices { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<EventLevel> EventLevels { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<JobProject> JobProjects { get; set; }
        public virtual DbSet<JobProjectsCategory> JobProjectsCategories { get; set; }
        public virtual DbSet<Mark> Marks { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<ParentType> ParentTypes { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Quiz> Quizs { get; set; }
        public virtual DbSet<Quota> Quotas { get; set; }
        public virtual DbSet<SalaryType> SalaryTypes { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<SchoolType> SchoolTypes { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<SkillsCategory> SkillsCategories { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentTakesQuiz> StudentTakesQuizs { get; set; }
        public virtual DbSet<SubjectType> SubjectTypes { get; set; }
        public virtual DbSet<SubSkill> SubSkills { get; set; }
        public virtual DbSet<SyllabusType> SyllabusTypes { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<MajorQuota> MajorQuotas { get; set; }
        public virtual  DbSet<MinorQuota> MinorQuotas { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressType>()
                .HasMany(e => e.Addresses)
                .WithRequired(e => e.AddressType)
                .HasForeignKey(e => e.AType);

            modelBuilder.Entity<BoardType>()
                .HasMany(e => e.Schools)
                .WithRequired(e => e.BoardType)
                .HasForeignKey(e => e.Board);

            modelBuilder.Entity<Choice>()
                .Property(e => e.Choice1)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Quizs)
                .WithRequired(e => e.Department1)
                .HasForeignKey(e => e.Department);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Students)
                .WithRequired(e => e.Department1)
                .HasForeignKey(e => e.Department);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.SubjectTypes)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.Dept);

            modelBuilder.Entity<EventLevel>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.EventLevel)
                .HasForeignKey(e => e.ELevel);

            modelBuilder.Entity<Event>()
                .Property(e => e.CertificateId)
                .IsUnicode(false);

            modelBuilder.Entity<EventType>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.EventType)
                .HasForeignKey(e => e.Category);

            modelBuilder.Entity<Gender>()
                .HasMany(e => e.Students)
                .WithRequired(e => e.Gender1)
                .HasForeignKey(e => e.Gender);

            modelBuilder.Entity<JobProject>()
                .Property(e => e.Descri)
                .IsUnicode(false);

            modelBuilder.Entity<JobProject>()
                .Property(e => e.Salary)
                .HasPrecision(10, 2);

            modelBuilder.Entity<JobProjectsCategory>()
                .HasMany(e => e.JobProjects)
                .WithRequired(e => e.JobProjectsCategory)
                .HasForeignKey(e => e.Category);

            modelBuilder.Entity<Parent>()
                .Property(e => e.PhoneNo)
                .HasPrecision(10, 0);

            modelBuilder.Entity<ParentType>()
                .HasMany(e => e.Parents)
                .WithRequired(e => e.ParentType)
                .HasForeignKey(e => e.PType);

            modelBuilder.Entity<Question>()
                .Property(e => e.Solution)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Choices)
                .WithRequired(e => e.Question1)
                .HasForeignKey(e => e.Question);

            modelBuilder.Entity<Quiz>()
                .Property(e => e.NoOfQues)
                .HasPrecision(3, 0);

            modelBuilder.Entity<Quiz>()
                .Property(e => e.Semester)
                .HasPrecision(1, 0);

            modelBuilder.Entity<Quiz>()
                .Property(e => e.Batch)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Quiz>()
                .Property(e => e.Section)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Quota>()
                .HasMany(e => e.Students)
                .WithRequired(e => e.Quota)
                .HasForeignKey(e => e.AdmissionQuota);

            modelBuilder.Entity<SalaryType>()
                .HasMany(e => e.Parents)
                .WithRequired(e => e.SalaryType1)
                .HasForeignKey(e => e.SalaryType);

            modelBuilder.Entity<School>()
                .Property(e => e.PercentageMarks)
                .HasPrecision(5, 2);

            modelBuilder.Entity<SkillsCategory>()
                .HasMany(e => e.Skills)
                .WithRequired(e => e.SkillsCategory)
                .HasForeignKey(e => e.Category);

            modelBuilder.Entity<Student>()
                .Property(e => e.AadharNo)
                .HasPrecision(12, 0);

            modelBuilder.Entity<Student>()
                .Property(e => e.PhoneNumber)
                .HasPrecision(10, 0);

            modelBuilder.Entity<StudentTakesQuiz>()
                .Property(e => e.Score)
                .HasPrecision(3, 0);

            modelBuilder.Entity<StudentTakesQuiz>()
                .Property(e => e.Attempts)
                .HasPrecision(3, 0);

            modelBuilder.Entity<SubjectType>()
                .Property(e => e.Semester)
                .HasPrecision(1, 0);

            modelBuilder.Entity<SubjectType>()
                .Property(e => e.MinMarks)
                .HasPrecision(3, 0);

            modelBuilder.Entity<SubjectType>()
                .Property(e => e.MinExternalMarks)
                .HasPrecision(3, 0);

            modelBuilder.Entity<SubjectType>()
                .Property(e => e.MaxCredits)
                .HasPrecision(2, 0);

            modelBuilder.Entity<SubjectType>()
                .HasMany(e => e.Marks)
                .WithRequired(e => e.SubjectType)
                .HasForeignKey(e => e.SubType);

            modelBuilder.Entity<SubSkill>()
                .Property(e => e.CertificateId)
                .IsUnicode(false);

            modelBuilder.Entity<SyllabusType>()
                .Property(e => e.Year)
                .HasPrecision(4, 0);

            modelBuilder.Entity<SyllabusType>()
                .HasMany(e => e.Marks)
                .WithRequired(e => e.SyllabusType)
                .HasForeignKey(e => e.SylType);

            modelBuilder.Entity<SyllabusType>()
                .HasMany(e => e.SubjectTypes)
                .WithRequired(e => e.SyllabusType)
                .HasForeignKey(e => e.SubId);
        }
    }
}
