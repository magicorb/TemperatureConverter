using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureConverter.UI.UserNotifications
{
	public interface IUserNotificationManager
	{
		void Warning(string text);
	}
}
