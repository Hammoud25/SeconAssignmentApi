﻿using SecondAssignmentApi.Extenions;
using SecondAssignmentApi.IModels;
using SecondAssignmentApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecondAssignmentApi.Data
{
    public interface IAppartmentRepository
    {
        Task<Appartment> Create(Appartment appartment);
        Task<bool> AppartmentExists(string title);
        Task<Appartment> GetAppartment(int id);
        Task<bool> SaveAll();
        Task<PagedList<Appartment>> GetAppartments(UserParams userParams);
        void Delete<T>(T entity) where T : class;
    }
}
