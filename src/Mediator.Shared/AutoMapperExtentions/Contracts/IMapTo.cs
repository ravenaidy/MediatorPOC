using AutoMapper;

namespace Mediator.Shared.AutoMapperExtentions.Contracts
{
    public interface IMapTo<T>
    {
        void CreateMap(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }
}
