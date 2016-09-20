using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebClient.Models;

namespace WebClient.Services
{
    public interface ITodoManager
    {
        IQueryable<TodoItem> GetAllUndone();
        IQueryable<TodoItem> GetAllDone();
        TodoItem GetById(int id);
        IQueryable<TodoItem> GetTodayTasks();
        IQueryable<TodoItem> GetTomorrowTasks();
    }

    public class TodoManager : ITodoManager
    {
        private readonly ITodoRepository _repository;

        public TodoManager(ITodoRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<TodoItem> GetAllUndone()
        {
            return _repository.Load().Where(i => !i.IsDone).AsQueryable();
        }

        public IQueryable<TodoItem> GetAllDone()
        {
            return _repository.Load().Where(i => i.IsDone).AsQueryable();
        }

        public TodoItem GetById(int id)
        {
            return _repository.Load().Single(i => i.Id == id);
        }

        public IQueryable<TodoItem> GetTodayTasks()
        {
            return _repository.Load().Where(t => t.DueDate.Date == DateTime.Now.Date).AsQueryable();
        }

        public IQueryable<TodoItem> GetTomorrowTasks()
        {
            return _repository.Load().Where(t => t.DueDate.Date == DateTime.Now.Date.AddDays(1)).AsQueryable();

        }
    }
}