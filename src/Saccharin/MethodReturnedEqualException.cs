using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Saccharin
{
	///<summary>
	///  Thrown when method invokation yields a result to be guarded against.
	///</summary>
	[Serializable]
	public class MethodReturnedEqualException : MethodReturnException
	{
		private static readonly Func<object, string> Format =
			o => string.Format(CultureInfo.InvariantCulture, "Invokation returned result equal to {0}.", o);

		/// <summary>
		///   Initializes a new instance of the <see cref = "MethodReturnedEqualException" /> class.
		/// </summary>
		/// <param name = "equalTo">The result was equal to this.</param>
		public MethodReturnedEqualException(object equalTo) : this(Format(equalTo)) {}

		/// <summary>
		///   Initializes a new instance of the <see cref = "MethodReturnedEqualException" /> class.
		/// </summary>
		public MethodReturnedEqualException() {}

		/// <summary>
		///   Initializes a new instance of the <see cref = "MethodReturnedEqualException" /> class.
		/// </summary>
		/// <param name = "message">The message.</param>
		public MethodReturnedEqualException(string message) : base(message) {}

		/// <summary>
		///   Initializes a new instance of the <see cref = "MethodReturnedEqualException" /> class.
		/// </summary>
		/// <param name = "message">The message.</param>
		/// <param name = "innerException">The inner exception.</param>
		public MethodReturnedEqualException(string message, Exception innerException) : base(message, innerException) {}

		/// <summary>
		///   Initializes a new instance of the <see cref = "MethodReturnedEqualException" /> class.
		/// </summary>
		/// <param name = "info">The <see cref = "T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name = "context">The <see cref = "T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		/// <exception cref = "T:System.ArgumentNullException">
		///   The <paramref name = "info" /> parameter is null.
		/// </exception>
		/// <exception cref = "T:System.Runtime.Serialization.SerializationException">
		///   The class name is null or <see cref = "P:System.Exception.HResult" /> is zero (0).
		/// </exception>
		protected MethodReturnedEqualException(SerializationInfo info, StreamingContext context) : base(info, context) {}
	}
}