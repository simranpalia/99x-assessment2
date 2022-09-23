using System.Data.Entity.Migrations;
using _99xAssessment2.Repository.ORM;

namespace _99xAssessment2.Repository
{
    public class DAL : Entities
    {
        public static Entities GetContext()
        {
            return new Entities();
        }

        public static void AddOrUpdateUser(User user)
        {
            var ctx = GetContext();
            if (user != null)
            {
                ctx.Users.AddOrUpdate(user);
                ctx.SaveChanges();
            }
        }
    }
}