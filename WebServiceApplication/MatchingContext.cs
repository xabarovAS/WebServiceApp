using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebServiceApplication.Models;

namespace WebServiceApplication
{
    public class MatchingContext: DbContext
    {
        public MatchingContext() : base("WebSerContext")
        { }
        public DbSet<Employee> Employeеs { get; set; }
        public DbSet<MeteringDevice> MeteringDevices { get; set; }
        public DbSet<StatusTasksEmployee> StatusTasksEmployees { get; set; }
        public DbSet<TasksEmployee> TasksEmployees { get; set; }

    }
}