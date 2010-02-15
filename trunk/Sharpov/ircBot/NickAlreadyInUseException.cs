
using System;

namespace Sharpov
{
	
	
	public class NickAlreadyInUseException : Exception
	{
		
		public NickAlreadyInUseException(string e) : base (e)
		{
		}
	}
}
