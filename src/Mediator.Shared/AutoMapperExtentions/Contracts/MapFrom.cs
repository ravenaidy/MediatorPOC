using AutoMapper;

namespace Mediator.Shared.AutoMapperExtentions.Contracts
{
    public interface IMapFrom<T>
    {
        void CreateMap(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
