﻿namespace LearningSystem.Web.Areas.Lecturer.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using LearningSystem.Models;
    using Utilities.Infrastructure.Contracts;

    public class CreateLectureBindingModel : IMapFrom<Lecture>, IHaveCustomMapping
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        [Display(Name = "Lecture Hall")]
        public string LectureHall { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        public int CourseId { get; set; }

        public void ConfigureMapping(Profile mapper) 
            => mapper.CreateMap<CreateLectureBindingModel, Lecture>();
    }
}