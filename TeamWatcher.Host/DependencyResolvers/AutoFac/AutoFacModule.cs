using Autofac;
using TeamWatcher.Application.Interfaces.Operations;
using TeamWatcher.Application.Interfaces.Repositories;
using TeamWatcher.Persistence.Implementations.Operations;
using TeamWatcher.Persistence.Implementations.Repositories;

namespace TeamWatcher.Host.DependencyResolvers.AutoFac
{
    public class AutoFacModule:Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            #region Player
            builder.RegisterType<PlayerOperation>().As<IPlayerOperation>().SingleInstance();
            builder.RegisterType<PlayerRepository>().As<IPlayerRepository>().SingleInstance();
            #endregion
            #region Team
            builder.RegisterType<TeamOperation>().As<ITeamOperation>().SingleInstance();
            builder.RegisterType<TeamRepository>().As<ITeamRepository>().SingleInstance();
            #endregion

        }
    }

}

