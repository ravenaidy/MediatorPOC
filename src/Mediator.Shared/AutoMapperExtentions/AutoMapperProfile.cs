using AutoMapper;
using Mediator.Shared.AutoMapperExtentions.Contracts;
using System;
using System.Linq;

namespace Mediator.Shared.AutoMapperExtentions
{
    public class AutoMapperProfile : Profile
    {
        private const string METHOD_NAME = "CreateMap";

        public AutoMapperProfile()
        {
            var types = StartupExtentions.Assemblies
                .SelectMany(x => x.GetExportedTypes())
                .Where(x =>
                    x.IsClass &&
                    !x.IsAbstract &&
                    x.GetInterfaces()
                        .Any(i =>
                            i.IsGenericType &&
                            (
                                i.GetGenericTypeDefinition() == typeof(IMapFrom<>) ||
                                i.GetGenericTypeDefinition() == typeof(IMapTo<>)
                            )
                        )
                    )
                .ToArray();

            foreach (Type type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo =
                    type.GetMethod(METHOD_NAME) ??
                    type.GetInterface("IMapFrom`1")?.GetMethod(METHOD_NAME) ??
                    type.GetInterface("IMapTo`1")?.GetMethod(METHOD_NAME);

                methodInfo.Invoke(instance, new object[] { this });
            }
        }
    }
}
