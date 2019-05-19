using AutoMapper;
using DataService.DBEntity;
using DataService.Model.RequestModel;
using DataService.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.MappingProfileModel
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ///<summary>
            /// Mapping Object <=> View Model
            this.CreateMap<Actor, ActorViewModel>();
            this.CreateMap<ActorViewModel, Actor>();

            this.CreateMap<Application, ApplicationViewModel>();
            this.CreateMap<ApplicationViewModel, Application>();

            this.CreateMap<Ecf, EcfViewModel>();
            this.CreateMap<EcfViewModel, Ecf>();

            this.CreateMap<Entity, EntityViewModel>();
            this.CreateMap<EntityViewModel, Entity>();

            this.CreateMap<ApplicationCharacteristic, ApplicationCharacteristicViewModel>();
            this.CreateMap<ApplicationCharacteristicViewModel, ApplicationCharacteristic>();

            this.CreateMap<Tcf, TcfViewModel>();
            this.CreateMap<TcfViewModel, Tcf>();


            this.CreateMap<UseCase, UseCaseViewModel>();
            this.CreateMap<UseCaseViewModel, UseCase>();

            this.CreateMap<UseCaseUpdateModel, UseCaseViewModel>();
            /// </summary>
            //For Missing Type
            this.AllowNullCollections = true;
            this.AllowNullDestinationValues = true;
            this.CreateMissingTypeMaps = true;
        }
    }
}
