using System;
using System.Runtime.Serialization;

namespace Saccharin
{
	///<summary>
	///  Thrown when method invokation yielded null.
	///</summary>
	[Serializable]
	public class MethodReturnedNullException : MethodReturnException
	{
		/// <summary>
		///   Initializes a new instance of the <see cref = "MethodReturnedNullException" /> class.
		/// </summary>
		public MethodReturnedNullException() : base("Method invokation yielded null.") {}

		/// <summary>
		///   Initializes a new instance of the <see cref = "MethodReturnedNullException" /> class.
		/// </summary>
		/// <param name = "message">The message.</param>
		public MethodReturnedNullException(string message) : base(message) {}

		/// <summary>
		///   Initializes a new instance of the <see cref = "MethodReturnedNullException" /> class.
		/// </summary>
		/// <param name = "message">The message.</param>
		/// <param name = "innerException">The inner exception.</param>
		public MethodReturnedNullException(string message, Exception innerException) : base(message, innerException) {}

		/// <summary>
		///   Initializes a new instance of the <see cref = "MethodReturnedNullException" /> class.
		/// </summary>
		/// <param name = "info">The <see cref = "T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name = "context">The <see cref = "T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		/// <exception cref = "T:System.ArgumentNullException">
		///   The <paramref name = "info" /> parameter is null.
		/// </exception>
		/// <exception cref = "T:System.Runtime.Serialization.SerializationException">
		///   The class name is null or <see cref = "P:System.Exception.HResult" /> is zero (0).
		/// </exception>
		protected MethodReturnedNullException(SerializationInfo info, StreamingContext context) : base(info, context) {}
	}
}