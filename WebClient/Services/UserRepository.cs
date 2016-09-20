using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebClient.Models;

namespace WebClient.Services
{


    public class UserRepository : IRepository<User>
    {
        public static string _PATH;

        public void Insert(User item)
        {
            var items = Load().ToList();
            items.Add(item);
            Store(items);
        }

        public void Update(User item)
        {
            var items = Load().ToList();
            var index = items.FindIndex(i => i.Id == item.Id);
            items[index] = item;
            Store(items);
        }

        public void Delete(int id)
        {
            var items = Load().ToList();
            var index = items.FindIndex(i => i.Id == id);
            items.RemoveAt(index);
            Store(items);
        }

        public IQueryable<User> Load()
        {
            var lines = File.ReadAllLines(_PATH);

            var users = new List<User>();
            foreach (var line in lines)
            {
                var parts = line.Split(new[] {","}, StringSplitOptions.None);
                var user = new User
                {
                    Id = int.Parse(parts[0]),
                    Username = parts[1],
                    Password = parts[2]
                };
                users.Add(user);
            }

            return users.AsQueryable();
        }

        public void Store(IEnumerable<User> items)
        {
            var lines = items.Select(i => $"{i.Id},{i.Username},{i.Password}");
            File.WriteAllLines(_PATH, lines);
        }
    }
}