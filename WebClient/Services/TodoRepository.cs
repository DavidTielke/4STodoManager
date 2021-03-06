﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;
using WebClient.Models;

namespace WebClient.Services
{
    public interface IRepository<TEntity>
    {
        void Insert(TEntity item);
        void Update(TEntity item);
        void Delete(int id);
        IQueryable<TEntity> Load();
        void Store(IEnumerable<TEntity> items);
    }

    public interface ITodoRepository : IRepository<TodoItem>
    {
    }

    public class TodoRepository : ITodoRepository
    {
        public static string PATH;

        public TodoRepository()
        {
            if (!File.Exists(PATH))
            {
                Store(new List<TodoItem> { new TodoItem()});
            }
        }

        public void Insert(TodoItem item)
        {
            var items = Load().ToList();
            item.Id = items.Max(i => i.Id) + 1;
            items.Add(item);
            Store(items);
        }

        public void Update(TodoItem item)
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

        public IQueryable<TodoItem> Load()
        {
            using (var strm = File.OpenRead(PATH))
            {
                var deserializer = new XmlSerializer(typeof(TodoItem[]));
                var items = (TodoItem[])deserializer.Deserialize(strm);
                return items.AsQueryable();
            }
        }

        public void Store(IEnumerable<TodoItem> items)
        {
            using (var strm = File.Create(PATH))
            {
                var serializer = new XmlSerializer(typeof(TodoItem[]));
                serializer.Serialize(strm, items.ToArray());
            }
        }
    }
}