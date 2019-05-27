using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using WorkoutApp.API.Data;
using WorkoutApp.API.Dtos;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutApp.API.Helpers.QueryParams;
using WorkoutApp.API.Helpers.Specifications;

namespace WorkoutApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ScheduledWorkoutsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWorkoutRepository repo;

        public ScheduledWorkoutsController(IMapper mapper, IWorkoutRepository repo)
        {
            this.mapper = mapper;
            this.repo = repo;
        }
    }
}