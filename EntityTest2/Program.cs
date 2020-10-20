using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace EntityTest2
{
    // test reverse engineering
    // Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer

    class Program
    {
        static void Main(string[] args)
        {
            using(entitytestContext db = new entitytestContext())
            {
                User user1 = new User { Name = "Tom", Age = 25 };

                db.Users.Add(user1);
                db.SaveChanges();
            }

            using (entitytestContext db = new entitytestContext())
            {
                var users = db.Users.ToList();
                Console.WriteLine("Список объектов");
                foreach (var u in users)
                    Console.WriteLine($"{u.Id}. {u.Name} - {u.Age}");
            }

            using(entitytestContext db = new entitytestContext())
            {
                // get first obj
                User user = db.Users.FirstOrDefault();
                if (user != null)
                {
                    user.Name = "Bob";
                    user.Age = 44;
                    // update obj
                    db.Users.Update(user);
                    db.SaveChanges();
                }
                Console.WriteLine("\nДанные после редактирования");
                var users = db.Users.ToList();
                foreach (var u in users)
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
            }

            // удаление данных
            using(entitytestContext db = new entitytestContext())
            {
                // получаем первый объект
                User user = db.Users.FirstOrDefault();
                if (user != null)
                {
                    //удаляем объект
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
                // выводим данные после обновления
                Console.WriteLine("\nДанные после удаления:");
                var users = db.Users.ToList();
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }
            Console.ReadKey();
        }
    }
}
