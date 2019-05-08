using AutoMapper;
using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.Mapping
{
    public class LibraryManagerMapperConfiguration
    {
        public static void Initialize(IMapperConfigurationExpression config = null)
        {
            if(config == null)
            {
                Mapper.Initialize(Initialize);
                return;
            }

            config.CreateMap<Entity, EntityViewModel>();
            config.CreateMap<EntityViewModel, Entity>()
                  .ForMember(x => x.Id, o => o.Ignore())
                  .ForMember(x => x.CreatedAt, o => o.Ignore())
                  .ForMember(x => x.ChangedAt, o => o.Ignore())
                  .ForMember(x => x.RemovedAt, o => o.Ignore());
            config.CreateMap<Book, BookViewModel>().IncludeBase<Entity, EntityViewModel>();
            config.CreateMap<BookViewModel, Book>().IncludeBase<EntityViewModel, Entity>();
        }
    }
}
