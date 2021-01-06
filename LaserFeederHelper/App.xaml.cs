using LaserFeederHelper.Controls;
using LaserFeederHelper.VM;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LaserFeederHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<YesNoDialog,YesNoDialogVM>();
            containerRegistry.RegisterDialog<SetSettings,SetSettingsVM>();

            // register other needed services here
        }

        protected override Window CreateShell()
        {
            var w = Container.Resolve<MainWindow>();
            return w;
        }
    }
}
