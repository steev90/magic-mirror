﻿using AutoMapper;
using MagicMirror.Business.Models;
using MagicMirror.DataAccess.Entities.Entities;

namespace MagicMirror.Business.Configuration
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<WeatherEntity, WeatherModel>()
                    .ForMember(dest => dest.SunRiseMilliseconds, source => source.MapFrom(src => src.Sys.Sunrise))
                    .ForMember(dest => dest.SunSetMilliSeconds, source => source.MapFrom(src => src.Sys.Sunset))
                    .ForMember(dest => dest.WeatherType, source => source.MapFrom(src => src.Weather[0].Main))
                    .ForMember(dest => dest.Icon, source => source.MapFrom(src => src.Weather[0].Icon))
                    .ForMember(dest => dest.Description, source => source.MapFrom(src => src.Weather[0].Description))
                    .ForMember(dest => dest.TemperatureKelvin, source => source.MapFrom(src => src.Main.Temp))
                .ReverseMap();

            CreateMap<TrafficEntity, TrafficModel>()
                .ForMember(dest => dest.Distance, source => source.MapFrom(src => src.Routes[0].Legs[0].Distance.Text))
                .ForMember(dest => dest.Minutes, source => source.MapFrom(src => src.Routes[0].Legs[0].Duration.Value))
                .ForMember(dest => dest.MinutesText, source => source.MapFrom(src => src.Routes[0].Legs[0].Duration.Text))
                .ReverseMap();

            CreateMap<AddressEntity, AddressModel>()
                .ForMember(dest => dest.HouseNumber, source => source.MapFrom(src => src.results[0].address_components[0].long_name))
                .ForMember(dest => dest.Street, source => source.MapFrom(src => src.results[0].address_components[1].long_name))
                .ForMember(dest => dest.City, source => source.MapFrom(src => src.results[0].address_components[2].long_name))
                .ForMember(dest => dest.Country, source => source.MapFrom(src => src.results[0].address_components[5].long_name))
                .ForMember(dest => dest.PostalCode, source => source.MapFrom(src => src.results[0].address_components[6].long_name))
                .ReverseMap();
        }
    }
}