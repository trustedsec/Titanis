using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Provides an implementation of <see cref="INotifyPropertyChanged"/>.
	/// </summary>
	/// <remarks>
	/// Implementors call <see cref="OnPropertyChanged(string)"/> to send notification when a property has changed.
	/// </remarks>
	public class NotifierObject : INotifyPropertyChanged
	{
		/// <inheritdoc/>
		public virtual event PropertyChangedEventHandler? PropertyChanged;
		/// <summary>
		/// Called when a property has changed.
		/// </summary>
		/// <param name="propertyName">Name of the property</param>
		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
			=> this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		protected void OnPropertyChanged(PropertyChangedEventArgs e)
			=> this.PropertyChanged?.Invoke(this, e);
		/// <summary>
		/// Updates a field and raises <see cref="PropertyChanged"/> if the value has changed.
		/// </summary>
		/// <typeparam name="T">Type of the value</typeparam>
		/// <param name="field">Field to update</param>
		/// <param name="value">Value to update it to</param>
		/// <param name="comparer">Equality comparer</param>
		/// <param name="propertyName">Name of property</param>
		/// <returns><see langword="true"/> if the field has changed; otherwise, <see langword="false"/>.</returns>
		protected bool NotifyIfChanged<T>(ref T field, T value, IEqualityComparer<T>? comparer = null, [CallerMemberName] string? propertyName = null)
		{
			if (!(comparer ?? EqualityComparer<T>.Default).Equals(field, value))
			{
				field = value;
				this.OnPropertyChanged(propertyName);
				return true;
			}
			return false;
		}
	}
}
