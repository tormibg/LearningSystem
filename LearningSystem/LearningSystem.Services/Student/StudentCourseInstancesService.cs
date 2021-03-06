﻿namespace LearningSystem.Services.Student
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Data;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Repository.Contracts;

    public class StudentCourseInstancesService : IStudentCourseInstancesService
    {
        private readonly IRepository<LearningSystemContext, CourseInstance> repository;
        private readonly IMapper mapper;

        public StudentCourseInstancesService(IRepository<LearningSystemContext, CourseInstance> repository, 
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<TModel> GetCourseInstancesAsync<TModel>(int courseId)
        {
            var courseInstance = await this.repository
                .Details()
                .Include(c => c.Lectures)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            var model = this.mapper.Map<TModel>(courseInstance);

            return model;
        }

        public async Task<IEnumerable<TModel>> GetCourseInstancesAsync<TModel>(string searchText)
        {
            searchText = searchText ?? string.Empty;

            var courseInstance =  await this.repository
                .Details()
                .OrderByDescending(c => c.Id)
                .Where(c => c.Name.ToLower().Contains(searchText.ToLower()))
                .ToListAsync();

            var model = this.mapper.Map<IEnumerable<TModel>>(courseInstance);

            return model;
        }

        public IEnumerable<SelectListItem> GetCoursesForDropdownList()
        {
            return this.repository
                .Get()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .ToList();
        }
    }
}