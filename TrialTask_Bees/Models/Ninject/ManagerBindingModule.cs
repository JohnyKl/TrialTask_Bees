using Ninject.Modules;
using TrialTask_Bees.DataSaving;
using TrialTask_Bees.Interfaces;

namespace TrialTask_Bees.Models.Ninject
{
    public class ManagerBindingModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGame>().To<BeesGame>();
            Bind<IGameService>().To<BeesGameService>();
            //Bind<IDataSaverController>().To<XmlFileDataSaverController>();
            Bind<IDataSaverController>().To<MySqlBeesGameDataSaverController>();
        }
    }
}