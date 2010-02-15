
using System;
using System.IO;

namespace Sharpov
{
	
	
	public class DccFileTransfer
	{
		//receiving files
		public DccFileTransfer(ircBot bot, DccManager manager, string nick, string login, string hostname, string type, string filename, long address, int port, long size)
		{
			throw new NotImplementedException();
		}
		
		//sending files
		public DccFileTransfer(ircBot bot, DccManager manager, FileStream file, string nick, int timeout)
		{
			throw new NotImplementedException();
		}
		
		public void receive (FileStream file, bool resume) 
		{
			throw new NotImplementedException();
		}
		
		public void doReceive (FileStream file, bool resume)
		{
			throw new NotImplementedException();
		}
		
		public void doSend (bool allowResume)
		{
			throw new NotImplementedException();
		}
		
		void setProgress (long progress)
		{
			throw new NotImplementedException();
		}
		
		private void delay () {}
		
		public string Nick 
		{
			get { throw new NotImplementedException(); }
		}
		
		public string Login
		{
			get { throw new NotImplementedException(); }
		}
				
		public string HostName
		{
			get { throw new NotImplementedException(); }
		}
		
		public FileStream File
		{
			get { throw new NotImplementedException(); }
		}
		
		public int Port
		{
			get { throw new NotImplementedException(); }
		}
		
		public bool isIncoming
		{
			get { throw new NotImplementedException(); }
		}
		
		public bool isOutgoing
		{
			get { throw new NotImplementedException(); }
		}
		
		public long PacketDelay
		{
			set { throw new NotImplementedException(); }
			get { throw new NotImplementedException(); }
		}
		
		public long Size
		{
			get { throw new NotImplementedException(); }
		}
		
		public long Progress
		{
			get { throw new NotImplementedException(); }
		}
		
		public double ProgressPercentage
		{
			get { return 100.0 * (Progress / (double)Size); }
		}
		
		public void close()
		{
			throw new NotImplementedException();
		}
		
		public long TransferRate
		{
			get { throw new NotImplementedException(); }
		}
		
		public long NumericalAddress
		{
			get { throw new NotImplementedException(); }
		}
	}
}
