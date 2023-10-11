using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.DAL
{
    public class GymManagementDbContext:IdentityDbContext<IdentityUser>
    {
        public GymManagementDbContext(DbContextOptions<GymManagementDbContext> options):base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Appointment>()
                .HasOne<Member>()
                .WithMany(m => m.Appointments)
                .HasForeignKey(m => m.MemberId);

            builder.Entity<Appointment>()
                .HasOne<Member>()
                .WithMany(m => m.Appointments)
                .HasForeignKey(m => m.PersonelId);

            builder.Entity<AccountRecoveryEmailToken>()
                .HasOne<Member>()
                .WithMany()
                .HasForeignKey(m=>m.MemberId);

            builder.Entity<TeacherNotesToStudent>()
                .HasOne<Member>()
                .WithMany(m => m.TeacherNotesToStudents)
                .HasForeignKey(m => m.MemberId);

            builder.Entity<TeacherNotesToStudent>()
                .HasOne<Member>()
                .WithMany(m => m.TeacherNotesToStudents)
                .HasForeignKey(m => m.PersonelId);

            
            builder.Entity<MemberMembership>()
                .HasKey(cc=> cc.Id);

            builder.Entity<MemberMembership>().Property(cc => cc.Id).UseIdentityColumn();

            builder.Entity<MemberMembership>()
                .HasOne(cc=>cc.Member)
                .WithMany(cc=>cc.MemberMemberships)
                .HasForeignKey(cc=>cc.MemberId);

            builder.Entity<MemberMembership>()
                .HasOne(cc => cc.Membership)
                .WithMany(cc => cc.MemberMemberships)
                .HasForeignKey(cc => cc.MembershipId);

            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });


            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<PersonelFunctions> PersonelFunctions { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<AccountRecoveryEmailToken> AccountRecoveryEmailTokens { get; set; }
        public DbSet<TeacherNotesToStudent> NotesToStudents { get; set; }
        public DbSet<MemberMembership> MemberMemberships { get; set; }
    }
}
