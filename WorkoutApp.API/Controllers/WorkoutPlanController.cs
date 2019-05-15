using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutApp.API.Data;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class WorkoutPlanController : ControllerBase
    {
        private readonly IWorkoutRepository repo;
        private readonly IMapper mapper;


        public WorkoutPlanController(IWorkoutRepository repo, IMapper mapper)
        {
            this.repo = repo; 
            this.mapper = mapper;   
        }
    }
}