using System;
using System.Reflection;

namespace Titanis.Cli
{
	/// <summary>
	/// Describes a parameter group.
	/// </summary>
	/// <seealso cref="ParameterGroupAttribute"/>
	public class ParameterGroupInfo
	{
		internal ParameterGroupInfo(Type groupType, ParameterGroupOptions options)
		{
			this.GroupType = groupType;
			this.Options = options;
		}

		internal ParameterGroupInfo(
			Type groupType,
			ParameterGroupInfo? nestingGroup,
			PropertyInfo? groupProperty,
			ConstructorInfo? constructor,
			string? groupCategory,
			ParameterGroupOptions options)
			: this(groupType, options)
		{
			this.NestingGroup = nestingGroup;
			this.GroupProperty = groupProperty;
			this.Constructor = constructor;
			this.GroupCategory = groupCategory;
		}

		internal object GetGroupObject(object command)
			=> this.GetGroupObject(command, true)!;
		internal object? GetGroupObject(object command, bool create)
		{
			if (this.NestingGroup != null)
				command = this.NestingGroup.GetGroupObject(command);

			if (this.GroupProperty == null)
			{
				return command;
			}
			else
			{
				var propValue = this.GroupProperty.GetValue(command);
				if (propValue == null && create)
				{
					propValue = this.Constructor.Invoke(null);
					this.GroupProperty.SetValue(command, propValue);
				}
				return propValue;
			}
		}

		/// <summary>
		/// Gets the type implementing this group.
		/// </summary>
		public Type GroupType { get; }
		/// <summary>
		/// <see cref="ParameterGroupInfo"/> of the group containing this group.
		/// </summary>
		public ParameterGroupInfo? NestingGroup { get; }
		/// <summary>
		/// Gets the <see cref="PropertyInfo"/> referencing this group.
		/// </summary>
		public PropertyInfo? GroupProperty { get; }
		/// <summary>
		/// Gets the constructor to create a new instance of this group.
		/// </summary>
		public ConstructorInfo? Constructor { get; }
		/// <summary>
		/// Gets the name of the category for parameters in this group, if any.
		/// </summary>
		public string? GroupCategory { get; }
		/// <summary>
		/// Gets a <see cref="ParameterGroupOptions"/> specifying options for this group.
		/// </summary>
		public ParameterGroupOptions Options { get; }

		public bool IsRoot => (this.GroupProperty == null);
	}
}
