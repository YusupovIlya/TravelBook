using Autofac;
using TravelBook.Core.Interfaces;
using TravelBook.Core.Services;

namespace TravelBook.Core
{
    public class DefaultCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ToDoItemSearchService>()
                .As<IToDoItemSearchService>().InstancePerLifetimeScope();
        }
    }
}