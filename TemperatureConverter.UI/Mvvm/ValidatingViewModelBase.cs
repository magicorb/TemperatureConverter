using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureConverter.UI.Mvvm
{
	public class ValidatingViewModelBase : ViewModelBase, INotifyDataErrorInfo
	{
		private readonly List<(string PropertyName, string ErrorText)> _errors = new List<(string, string)>();

		public bool HasErrors => _errors.Any();

		public IEnumerable GetErrors(string propertyName)
			=> _errors
			.Where(e => e.PropertyName == propertyName)
			.Select(e => e.ErrorText);

		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

		protected void AddError(string propertyName, string errorText)
		{
			_errors.Add((propertyName, errorText));
			ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
		}

		protected void ClearErrors()
		{
			if (!HasErrors)
				return;
			
			_errors.Clear();
			ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(null));
		}

		protected string GetErrorsText(string propertyName)
		{
			var errors = GetErrors(propertyName).Cast<string>();

			if (!errors.Any())
				return null;

			return string.Join("\r\n", errors);
		}
	}
}
