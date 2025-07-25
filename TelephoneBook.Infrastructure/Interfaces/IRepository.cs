﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using TelephoneBook.Domain.Common;

namespace TelephoneBook.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : IBaseEntity
    {
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FilterAsync(FilterDefinition<T> filter);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(string id, T entity);
        Task DeleteAsync(string id);

        Task<T> FilterFirstAsync(FilterDefinition<T> filter);
    }
}
