
using System;

namespace Sharpov
{
	
	
	public class User : IComparable<User>
	{
		private string _prefix;
		private string _nick;
		private string _lowerNick;
		
		public User(string prefix, string nick)
		{
			_prefix = prefix;
			_nick = nick;
			_lowerNick = _nick.ToLower();
		}
		
		public string Prefix
		{
			get { return _prefix; }
		}
		
		public bool isOp
		{
			get { return _prefix.IndexOf('@') >= 0; }
		}
		
		public bool hasVoice
		{
			get { return _prefix.IndexOf('+') >= 0; }
		}
		
		public string Nick
		{
			get { return _nick; }
		}
		
		public string toString ()
		{
			return Prefix + Nick;
		}
		
		public int CommpareTo (User u) 
		{
			return u._lowerNick.CompareTo(_lowerNick);
		}
	}
}
