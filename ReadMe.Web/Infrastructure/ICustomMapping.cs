using AutoMapper;

namespace ReadMe.Web.Infrastructure
{
    public interface ICustomMapping
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
