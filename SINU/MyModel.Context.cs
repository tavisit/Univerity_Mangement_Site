﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SINU
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Project_WebsiteEntities : DbContext
    {
        public Project_WebsiteEntities()
            : base("name=Project_WebsiteEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Building_info> Building_info { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Info_department> Info_department { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<Status_Job> Status_Job { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Subjects_list> Subjects_list { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Teaching_Types> Teaching_Types { get; set; }
        public virtual DbSet<Univ_info> Univ_info { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Working_staff> Working_staff { get; set; }
    }
}
