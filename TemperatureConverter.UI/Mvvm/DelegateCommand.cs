using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TemperatureConverter.UI.Mvvm
{
	public class DelegateCommand<T> : ICommand
	{
		private readonly Action<T> _execute;
		private readonly Func<T, bool> _canExecute;

		public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public DelegateCommand(Action<T> execute)
			: this(execute, _ => true)
		{
		}

		public void Execute(object parameter)
			=> _execute((T)parameter);

		public bool CanExecute(object parameter)
			=> _canExecute((T)parameter);

		public event EventHandler CanExecuteChanged;

		public void RaiseCanExecuteChanged()
			=> CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}

	public class DelegateCommand : ICommand
	{
		private readonly Action _execute;
		private readonly Func<bool> _canExecute;

		public DelegateCommand(Action execute, Func<bool> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public DelegateCommand(Action execute)
			: this(execute, () => true)
		{
		}

		public void Execute(object parameter)
			=> _execute();

		public bool CanExecute(object parameter)
			=> _canExecute();

		public event EventHandler CanExecuteChanged;

		public void RaiseCanExecuteChanged()
			=> CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}
}
