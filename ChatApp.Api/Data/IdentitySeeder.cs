using ChatApp.Api.Data.Identity;
using ChatApp.Api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ChatApp.Api.Data
{
    public static class IdentitySeeder
    {
        public async static Task seedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var Secondrole = new IdentityRole()
                {
                    Name = "Reader"
                };
                var firstRole = new IdentityRole()
                {
                    Name = "Writer"
                };


                var result = await roleManager.CreateAsync(firstRole);
                if (result.Succeeded)
                {
                    await roleManager.CreateAsync(Secondrole);
                }

            }

        }
        public async static Task seedUser(UserManager<CustomUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var admin = new CustomUser()
                {
                    Email = "Admin@chat-App.com",
                    EmailConfirmed = true,
                    UserName = "Admin",
                    PhoneNumber = "+963936769321",


                };
                var result = await userManager.CreateAsync(admin, "Suarez.123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Reader");
                    await userManager.AddToRoleAsync(admin, "Writer");
                }
                var user = new CustomUser()
                {
                    Email = "user@Chat-App.com",
                    EmailConfirmed = true,
                    UserName = "user",
                    PhoneNumber = "0934480248",


                };
                var resultofCreatingUser = await userManager.CreateAsync(user, "Suarez.1234");
                if (resultofCreatingUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Reader");

                }



            }


        }
        public static async Task SeedApplicationUsers(UserManager<CustomUser> userManager,ApplicationDbContext context)
        {
            var users = await userManager.Users.ToListAsync();
            if (users.Count == 2)
            {
                List<CustomUser> newUsers = new List<CustomUser>()
                {

                    new CustomUser
                    {
                           Email = "EmilyWilson@Chat-App.com",
                    EmailConfirmed = true,
                    UserName = "EmilyWilson",
                    PhoneNumber = "09343949421",
                        DateBirth = new DateTime(1992, 5, 12),
                        Created = DateTime.Now,
                        LastActive = DateTime.Now.AddDays(-3),
                        KnownAs = "Emily Wilson",
                        Gender = "Female",
                        LookingFor = "Friendship",
                        Interests = "Hiking, Reading, Traveling",
                        Introduction = "I'm a software engineer living in New York City. I love trying out new restaurants and going on hikes on the weekends.",
                        City = "New York City",
                        Country = "USA",
                        Photos=new List<Photo>(){new Photo() {IsMain = true, Url= "https://randomuser.me/api/portraits/women/59.jpg" } }
                       
                    },
                    new CustomUser
                    {
                           Email = "MichaelDavis@Chat-App.com",
                    EmailConfirmed = true,
                    UserName = "MichaelDavis",
                    PhoneNumber = "0935910133",
                        DateBirth = new DateTime(1985, 2, 20),
                        Created = DateTime.Now.AddDays(-10),
                        LastActive = DateTime.Now.AddDays(-1),
                        KnownAs = "Michael Davis",
                        Gender = "Male",
                        LookingFor = "Dating",
                        Interests = "Playing guitar, Watching movies, Playing sports",
                        Introduction = "I'm a musician living in Los Angeles. I'm looking for someone who shares my passion for music and life.",
                        City = "Los Angeles",
                        Country = "USA",
                        Photos=new List<Photo>(){new Photo() {IsMain = true, Url= "https://randomuser.me/api/portraits/men/32.jpg" } }
                    },
                    new CustomUser
                    {
                           Email = "SophiaLee@Chat-App.com",
                    EmailConfirmed = true,
                    UserName = "SophiaLee",
                    PhoneNumber = "0934449233",
                        DateBirth = new DateTime(1995, 8, 25),
                        Created = DateTime.Now.AddDays(-5),
                        LastActive = DateTime.Now,
                        KnownAs = "Sophia Lee",
                        Gender = "Female",
                        LookingFor = "Friendship",
                        Interests = "Cooking, Painting, Yoga",
                        Introduction = "I'm a student living in Seoul. I love trying out new recipes and practicing yoga in my free time.",
                        City = "Seoul",
                        Country = "South Korea",
                        Photos=new List<Photo>(){new Photo() {IsMain=true ,Url= "https://randomuser.me/api/portraits/women/50.jpg" } }

                    },
                    new CustomUser
                    {
                           Email = "DavidKim@Chat-App.com",
                    EmailConfirmed = true,
                    UserName = "DavidKim",
                    PhoneNumber = "0934220212",
                        DateBirth = new DateTime(1980, 11, 15),
                        Created = DateTime.Now.AddDays(-15),
                        LastActive = DateTime.Now.AddDays(-7),
                        KnownAs = "David Kim",
                        Gender = "Male",
                        LookingFor = "Dating",
                        Interests = "Reading, Traveling, Photography",
                        Introduction = "I'm a writer living in Paris. I'm looking for someone who shares my love for literature and adventure.",
                        City = "Paris",
                        Country = "France",
                        Photos=new List<Photo>(){new Photo() {IsMain=true, Url= "https://randomuser.me/api/portraits/men/14.jpg" } }

                    },
                    new CustomUser
                    {
                           Email = "OliviaMartin@Chat-App.com",
                    EmailConfirmed = true,
                    UserName = "OliviaMartin",
                    PhoneNumber = "09341110245",
                        DateBirth = new DateTime(1990, 3, 28),
                        Created = DateTime.Now.AddDays(-8),
                        LastActive = DateTime.Now.AddDays(-2),
                        KnownAs = "Olivia Martin",
                        Gender = "Female",
                        LookingFor = "Friendship",
                        Interests = "Dancing, Singing, Hiking",
                        Introduction = "I'm a dancer living in London. I love trying out new dance styles and going on hikes in the countryside.",
                        City = "London",
                        Country = "UK",
                        Photos=new List<Photo>(){new Photo() {IsMain=true, Url= "https://randomuser.me/api/portraits/women/35.jpg" } }
                    },
                    new CustomUser
                    {
                           Email = "AlexanderBrown@Chat-App.com",
                    EmailConfirmed = true,
                    UserName = "AlexendarBrown",
                    PhoneNumber = "0931303491",
                        DateBirth = new DateTime(1982, 6, 1),
                        Created = DateTime.Now.AddDays(-12),
                        LastActive = DateTime.Now.AddDays(-4),
                        KnownAs = "Alexander Brown",
                        Gender = "Male",
                        LookingFor = "Dating",
                        Interests = "Playing sports, Watching movies, Playing video games",
                        Introduction = "I'm a software engineer living in Berlin. I'm looking for someone who shares my passion for technology and gaming.",
                        City = "Berlin",
                        Country = "Germany",
                        Photos=new List < Photo >() { new Photo() {IsMain = true, Url = "https://randomuser.me/api/portraits/men/94.jpg" } }
                    },
                    new CustomUser
                    {
                           Email = "IsabellaGracia@Chat-App.com",
                    EmailConfirmed = true,
                    UserName = "IsabellaGracia",
                    PhoneNumber = "094890183",
                        DateBirth = new DateTime(1997, 10, 18),
                        Created = DateTime.Now.AddDays(-3),
                        LastActive = DateTime.Now,
                        KnownAs = "Isabella Garcia",
                        Gender = "Female",
                        LookingFor = "Friendship",
                        Interests = "Reading, Writing, Traveling",
                        Introduction = "I'm a student living in Madrid. I love trying out new books and traveling to new places.",
                        City = "Madrid",
                        Country = "Spain",
                        Photos=new List < Photo >(){new Photo() {IsMain = true, Url= "https://randomuser.me/api/portraits/women/64.jpg" } }
                        
                    }
                    

                };
               
               foreach(var user in newUsers)
                {
                   var resultofCreating= await userManager.CreateAsync(user, "Suarez.123");
                    if (resultofCreating.Succeeded)
                    {
                        await  userManager.AddToRoleAsync(user, "Reader");
                    }
                    foreach(var photo in user.Photos)
                    {
                        photo.Id = Guid.NewGuid();
                        photo.CustomUserId = user.Id;
                      await   context.photos.AddAsync(photo);
                        await context.SaveChangesAsync();
                      
                    }
                }
            }
        }
    }
}
