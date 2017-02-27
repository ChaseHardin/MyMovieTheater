using System.Linq;
using System.Reflection;
using LinqKit;

namespace MyMovieTheater.Business.Services
{
    public class BaseService
    {
        public BaseService()
        {
            AutoMapperConfiguration();
        }

        public void AutoMapperConfiguration()
        {
            Assembly.GetExecutingAssembly()
                 .GetTypes()
                 .Where(x => x.IsClass && x.Namespace == "MyMovieTheater.Business.ViewModels")
                 .ForEach(x => System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(x.TypeHandle));

            AutoMapper.Mapper.CreateMap<bool, short?>().ConvertUsing(x => x ? (short)1 : (short)0);
            AutoMapper.Mapper.CreateMap<short, bool>().ConvertUsing(x => x == 1);

            AutoMapper.Mapper.CreateMap<bool, int?>().ConvertUsing(x => x ? 1 : 0);
            AutoMapper.Mapper.CreateMap<int?, bool>().ConvertUsing(x => x.HasValue && x.Value == 1);

            AutoMapper.Mapper.CreateMap<short, int>().ConvertUsing(x => (int)x);
            AutoMapper.Mapper.CreateMap<int, int?>().ConvertUsing(x => x);
        }
    }
}