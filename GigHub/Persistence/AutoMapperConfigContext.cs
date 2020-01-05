using System;
using AutoMapper;
using GigHub.App_Start;

namespace GigHub.Persistence
{
    public class AutoMapperConfigContext
    {
        public IConfigurationProvider AutoMapperConfig
        {
            get { return  _config;}
        }

        private readonly IConfigurationProvider _config;
        public AutoMapperConfigContext()
        {
             _config = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            });
        }  
    }
}