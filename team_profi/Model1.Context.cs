﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace team_profi
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TeamProfiBDEntities : DbContext
    {
        public TeamProfiBDEntities()
            : base("name=TeamProfiBDEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<Assignments> Assignments { get; set; }
        public virtual DbSet<Grades> Grades { get; set; }
        public virtual DbSet<MediaFiles> MediaFiles { get; set; }
        public virtual DbSet<StudentMessages> StudentMessages { get; set; }
        public virtual DbSet<StudentRatings> StudentRatings { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
