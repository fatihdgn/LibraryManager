using AutoMapper;
using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.UI.Models;
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
            if (config == null)
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
            config.CreateMap<DateTimeOffset?, string>().ConvertUsing(x => (x ?? DateTimeOffset.Now).ToString("dd.MM.yyyy"));
            config.CreateMap<string, DateTimeOffset?>().ConstructUsing((x, _) =>
            {
                if (string.IsNullOrEmpty(x)) return new DateTimeOffset?();
                DateTimeOffset returnsAt;
                DateTimeOffset.TryParseExact(x, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.AssumeLocal, out returnsAt);
                return new DateTimeOffset?(returnsAt);
            });

            config.CreateMap<Book, BookViewModel>().IncludeBase<Entity, EntityViewModel>();
            config.CreateMap<BookViewModel, Book>().IncludeBase<EntityViewModel, Entity>();

            config.CreateMap<Author, AuthorViewModel>().IncludeBase<Entity, EntityViewModel>();
            config.CreateMap<AuthorViewModel, Author>().IncludeBase<EntityViewModel, Entity>();

            config.CreateMap<Library, LibraryViewModel>().IncludeBase<Entity, EntityViewModel>();
            config.CreateMap<LibraryViewModel, Library>().IncludeBase<EntityViewModel, Entity>();

            config.CreateMap<User, UserViewModel>().IncludeBase<Entity, EntityViewModel>();
            config.CreateMap<UserViewModel, User>().IncludeBase<EntityViewModel, Entity>();

            config.CreateMap<Loan, LoanViewModel>().IncludeBase<Entity, EntityViewModel>();
            config.CreateMap<LoanViewModel, Loan>().IncludeBase<EntityViewModel, Entity>();
            config.CreateMap<Role, RoleViewModel>().ForMember(x => x.Scopes, o => o.Ignore()).IncludeBase<Entity, EntityViewModel>();
            config.CreateMap<RoleViewModel, Role>().ForMember(x => x.Scopes, o => o.Ignore()).IncludeBase<EntityViewModel, Entity>();
        }
    }
}
