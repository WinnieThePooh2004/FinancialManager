using System.Net;

namespace FinancialManager.Shared.Exceptions.DomainExceptions
{
	public class InvalidParameterValueExeption : DomainException
	{
		public InvalidParameterValueExeption(object @object, string parameterName, string message)
			:base(HttpStatusCode.BadRequest, $"{message}\nparameter '{parameterName}' has invalid value", @object)
		{
		}
	}
}
