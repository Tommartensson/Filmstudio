using AutoMapper;
using Filmstudion.Entities;
using Filmstudion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Data
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            this.CreateMap<Movie, MovieModel>().ReverseMap();
            this.CreateMap<MovieCo, MovieCoModel>().ReverseMap();
            this.CreateMap<Admin, AdminModel>().ReverseMap();
     
        }
    }
}
