using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NeumeierJob.Models
{
    public class FSContext : DbContext
    {
        public FSContext(DbContextOptions<FSContext> options)
            : base(options)
        {
            
        }

        public DbSet<FSItem> FSItems { get; set; }

        
        //public override int SaveChanges()
        //{
            
        //    //using (var fs = new FileStream(@"_SaveTest.txt", FileMode.OpenOrCreate))
        //    //{
        //    //    using (var wr = new StreamWriter(fs))
        //    //    {
        //    //        foreach(var item in FSItems)
        //    //        {
        //    //            wr.WriteLine(item.Name + " - " + item.IsComplete.ToString());
        //    //        }
        //    //    }
        //    //}
        //    return base.SaveChanges();
        //}
    }
}
