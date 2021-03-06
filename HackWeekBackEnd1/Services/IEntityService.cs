﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HackWeekBackEnd1.Models;

namespace HackWeekBackEnd1.Services
{
    public interface IEntityService<T> where T : IMongoEntity
    {
        T Create(T entity);
        void Delete(string id);
        T GetById(string id);
        T Update(T entity);
    }
}