using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemperatureConverter.Math;
using TemperatureConverter.UI.TemperatureConversion;
using TemperatureConverter.UI.UserNotifications;

namespace TemperatureConverter.UI
{
	public class ContainerFactory
	{
		public static IContainer CreateContainer()
		{
			var container = new Container();

			container.Register<ITemperatureConverter, Math.TemperatureConverter>(Reuse.Singleton);
			container.Register<IUserNotificationManager, UserNotificationManager>(Reuse.Singleton);
			container.Register<ITemperatureConverterViewModel, TemperatureConverterViewModel>();
			container.Register<IMainWindowViewModel, MainWindowViewModel>();
			container.Register<MainWindow>();

			return container;
		}
	}
}
