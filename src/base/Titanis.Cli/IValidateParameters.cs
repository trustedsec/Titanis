using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
	public interface IValidateParameters
	{
		void ValidateParameters(ParameterValidationContext context, ParameterGroupOptions options);
	}
}
