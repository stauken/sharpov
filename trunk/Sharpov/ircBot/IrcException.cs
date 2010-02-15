
using System;

namespace Sharpov
{
	public class IrcException : Exception
	{
		
		public IrcException(string e) : base (e)
		{
		}
	}
}
