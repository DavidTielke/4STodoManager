﻿using System;
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
    }
}