using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TemperatureConverter.UI.UserNotifications
{
	public class UserNotificationManager : IUserNotificationManager
	{
		public void Warning(string text)
			=> MessageBox.Show(text, Properties.Resources.WarningWindowTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
	}
}
