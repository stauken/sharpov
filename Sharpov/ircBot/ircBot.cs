
using System;
using System.Collections.Generic;
using System.IO;

namespace Sharpov
{
	public abstract class ircBot : ReplyConstants
	{
		public static string VERSION = "0";
		
		const int OP_ADD = 1;
		const int OP_REMOVE = 2;
		const int VOICE_ADD = 3;
		const int VOICE_REMOVE = 4;
		
	    private InputThread _inputThread;
    	private OutputThread _outputThread;
    	private string _charset;
	    private InetAddress _inetAddress;

		private string _server;// = null;
		private int _port = -1;
		private string _password;// = null;
		
		private Queue<string> _outQueue = new Queue<string> ();
		private long _messageDelay = 1000;
		
		private Dictionary<Object, Object> _channels = new Dictionary<Object, Object>();
		private Dictionary<Object, Object> _topic = new Dictionary<Object, Object>();
		
		private DccManager _dccManager;// = new DccManager(this);
		private int[] _dccPorts;
		private InetAddress _dccInetAddress;
		
		private bool _autoNickChange = false;
		private bool _verbose = false;
		private string _name = "ircBot";
		private string _nick = "ircBot";
		private string _login = "";
		private string _version = "";
		private string _finger = "";
		
		private string _channelPrefixes = "#&+!";
		
		public ircBot() {}
		
		public void connect (string hostname)
		{
			connect(hostname, 6667, null);
		}
		
		public void connect (string hostname, int port)
		{
			connect (hostname, port, null);
		}
		
		//TODO: Synchronize
		public void connect (string hostname, int port, string password) 
		{
			_server = hostname;
			_port = port;
			_password = password;
		}
		
		public void reconnect ()
		{
			throw new NotImplementedException();
		}
		
		public void disconnect ()
		{
			throw new NotImplementedException();
		}
		
		public bool AutoNickChange
		{
			set { _autoNickChange = value; }
		}
		
		public void startIdentServer () 
		{
			throw new NotImplementedException();
		}
		
		public void joinChannel (string channel) 
		{
			throw new NotImplementedException();
		}
		
		public void partChannel (string channel)
		{
			throw new NotImplementedException();
		}
		
		public void partChannel (string channel, string reason)
		{
			throw new NotImplementedException();
		}
		
		public void quitServer ()
		{
			quitServer("");
		}
		
		public void quitServer (string reason) 
		{
			throw new NotImplementedException();
		}
		
		public void sendRawLine (string line) 
		{
			throw new NotImplementedException();
		}
		
		public void sendRawLineViaQueue (string line)
		{
			throw new NotImplementedException();
		}
		
		public void sendMessage (string target, string message)
		{
			_outQueue.Enqueue("PRIVMSG " + target + " :" + message);
		}
		
		public void sendAction (string target, string action)
		{
			throw new NotImplementedException();
		}
		
		public void sendNotice (string target, string notice)
		{
			_outQueue.Enqueue("NOTICE " + target + " :" + notice);
		}
		
		public void sendCTCPCommand (string target, string command)
		{
			_outQueue.Enqueue("PRIVMSG " + target + " :\u0001" + command + "\u0001");
		}
		
		public void nickChange (string newNick)
		{
			sendRawLine("NICK " + newNick);
		}
		
		public void identify (string pass)
		{
			sendRawLine("NICKSERV IDENTIFY " + pass);
		}
		
		public void setMode (string channel, string mode)
		{
			sendRawLine ("MODE " + channel + " " + mode);
		}
		
		public void sendInvite (string nick, string channel)
		{
			sendRawLine ("INVITE " + nick + " :" + channel);
		}
		
		public void ban (string channel, string hostmask) 
		{
			setMode (channel, "+b " + hostmask);
		}
		
		public void unban (string channel, string hostmask)
		{
			setMode (channel, "-b " + hostmask);		}
		
		public void op (string channel, string nick)
		{
			setMode (channel, "+o " + nick);
		}
		
		public void deop (string channel, string nick)
		{
			setMode (channel, "-o " + nick);
		}

		public void voice (string channel, string nick)
		{
			setMode (channel, "+v " + nick);
		}

		public void devoice (string channel, string nick)
		{
			setMode (channel, "-v " + nick);
		}
		
		public void setTopic (string channel, string topic)
		{
			sendRawLine ("TOPIC " + channel + " :" + topic);
		}
		
		public void kick (string channel, string nick) 
		{
			kick (channel, nick, "");
		}
		
		public void kick (string channel, string nick, string reason) 
		{
			sendRawLine ("KICK " + channel + " " + nick + " :" + reason);
		}
		
		public void listChannels () 
		{
			
		}
		
		public void listChannels (string param)
		{
			if (param == "") sendRawLine("LIST");
			else sendRawLine ("LIST " + param);
		}
		
		public DccFileTransfer dccSendFile (FileStream file, string nick, int timeout)
		{
			DccFileTransfer transfer = new DccFileTransfer (this, _dccManager, file, nick, timeout);
			transfer.doSend(true);
			return transfer;
		}
		
		public DccChat dccSendChatRequest (string nick, int timeout)
		{
			throw new NotImplementedException();
		}
		
		//TODO: Add system time to verbose logging.
		public void log (string line)
		{
			if (_verbose)
				Console.WriteLine(line);
		}
		
		protected void handleLine (string line) 
		{
			log(line);
			
			if (line.StartsWith("PING "))
			{
				//onServerPing(line.substring(5));
				return;
			}
		}
		
		protected void onConnect() {}
		
		protected void onDisconnect() {}
		
		private void processServerResponse(int code, string response)
		{
			throw new NotImplementedException();
		}
		
		protected void onServerResponse(int code, string response) {}
		
		protected void onUserList(string channel, User[] users) {}
		
		protected void onMessage(string channel, string sender, string login, string hostname, string message) {}
		
		protected void onPrivateMessage(string sender, string login, string hostname, string message) {}
		
		protected void onAction(string sender, string login, string hostname, string target, string action) {}
		
		protected void onNotice(string sourceNick, string sourceLogin, string sourceHostname, string target, string notice) {}
		
		protected void onJoin(string channel, string sender, string login, string hostname) {}
		
		protected void onPart(string channel, string sender, string login, string hostname) {}
		
		protected void onNickChange(string oldNick, string login, string hostname, string newNick) {}
		
		protected void onKick(string channel, string kickerNick, string kickerLogin, string kickerHostname, string recipientNick, string reason) {}
		
		protected void onQuit(string sourceNick, string sourceLogin, string sourceHostname, string reason) {}
		
		protected void onTopic(string channel, string topic) {}
		
		protected void onTopic(string channel, string topic, string setBy, long date, bool changed) {}
		
		protected void onChannelInfo(string channel, int userCount, string topic) {}
		
		private void processMode(string target, string sourceNick, string sourceLogin, string sourceHostname, string mode) 
		{
			throw new NotImplementedException();
		}
		
		protected void onMode(string channel, string sourceNick, string sourceLogin, string sourceHostname, string mode) {}
		
		protected void onUserMode(string targetNick, string sourceNick, string sourceLogin, string sourceHostname, string mode) {}
		
		protected void onOp(string channel, string sourceNick, string sourceLogin, string sourceHostname, string recipient) {}
		
		protected void onDeop(string channel, string sourceNick, string sourceLogin, string sourceHostname, string recipient) {}
		
		protected void onVoice(string channel, string sourceNick, string sourceLogin, string sourceHostname, string recipient) {}
		
		protected void onDeVoice(string channel, string sourceNick, string sourceLogin, string sourceHostname, string recipient) {}
		
		protected void onSetChannelKey(string channel, string sourceNick, string sourceLogin, string sourceHostname, string key) {}
		
		protected void onRemoveChannelKey(string channel, string sourceNick, string sourceLogin, string sourceHostname, string key) {}
		
		protected void onSetChannelLimit(string channel, string sourceNick, string sourceLogin, string sourceHostname, int limit) {}
		
		protected void onRemoveChannelLimit(string channel, string sourceNick, string sourceLogin, string sourceHostname) {}
		
		protected void onSetChannelBan(string channel, string sourceNick, string sourceLogin, string sourceHostname, string hostmask) {}
		
		protected void onRemoveChannelBan(string channel, string sourceNick, string sourceLogin, string sourceHostname, string hostmask) {}
		
		protected void onSetTopicProtection(string channel, string sourceNick, string sourceLogin, string sourceHostname) {}
		
		protected void onRemoveTopicProtection(string channel, string sourceNick, string sourceLogin, string sourceHostname) {}
		
		protected void onSetNoExternalMessages(string channel, string sourceNick, string sourceLogin, string sourceHostname) {}
		
		protected void onRemoveNoExternalMessages(string channel, string sourceNick, string sourceLogin, string sourceHostname) {}
		
		protected void onSetInviteOnly(string channel, string sourceNick, string sourceLogin, string sourceHostname) {}
		
		protected void onRemoveInviteOnly(string channel, string sourceNick, string sourceLogin, string sourceHostname) {}
		
		protected void onSetModerated(string channel, string sourceNick, string sourceLogin, string sourceHostname) {}
		
		protected void onRemoveModerated(string channel, string sourceNick, string sourceLogin, string sourceHostname) {}
		
		protected void onSetPrivate(string channel, string sourceNick, string sourceLogin, string sourceHostname) {}
		
		protected void onRemovePrivate(string channel, string sourceNick, string sourceLogin, string sourceHostname) {}
		
		protected void onSetSecret(string channel, string sourceNick, string sourceLogin, string sourceHostname) {}
		
		protected void onRemoveSecret(string channel, string sourceNick, string sourceLogin, string sourceHostname) {}
		
		protected void onInvite(string targetNick, string sourceNick, string sourceLogin, string sourceHostname, string channel)  {}
		
		protected void onDccSendRequest(string sourceNick, string sourceLogin, string sourceHostname, string filename, long address, int port, int size) {}
		
		protected void onDccChatRequest(string sourceNick, string sourceLogin, string sourceHostname, long address, int port) {}
		
		protected void onIncomingFileTransfer(DccFileTransfer transfer) {}
		
		protected void onFileTransferFinished(DccFileTransfer transfer, Exception e) {}
		
		protected void onIncomingChatRequest(DccChat chat) {}
		
		protected void onVersion (string sourceNick, string sourceLogin, string sourceHostname, string target)
		{
			sendRawLine("NOTICE " + sourceNick + ":\u0001VERSION " + _version + "\u0001");
		}
		
		protected void onPing (string sourceNick, string soureLogin, string sourceHostname, string target, string pingValue)
		{
			sendRawLine("NOTICE " + sourceNick + " :\u0001PING " + pingValue + "\u0001");
		}
		
		protected void onServerPing (string response)
		{
			sendRawLine("PONG " + response);
		}
		
		//TODO: Add System Time
		protected void onTime (string sourceNick, string sourceLogin, string sourceHostname, string target)
		{
			sendRawLine ("NOTICE " + sourceNick + " :\u0001TIME " /*+ TIME HERE*/ + "\u0001");
		}
		
		protected void onFinger (string sourceNick, string sourceLogin, string sourceHostname, string target)
		{
			sendRawLine("NOTICE " + sourceNick + " :\u0001FINGER " + _finger + "\u0001");
		}
		
		protected void onUnknown(String line) 
		{
			// And then there were none :)
		}
		
		public bool Verbose
		{
			set { _verbose = value; }
		}
		
		public string Name
		{
			set { _name = value; }
			get { return _name; }
		}
		
		public string Nick
		{
			set { _nick = value; }
			get { return _nick; }
		}
		
		public string Login
		{
			set { _login = value; }
			get { return _login; }
		}
		
		public string Version
		{
			set { _version = value; }
			get { return _version; }
		}
		
		public string Finger
		{
			set { _finger = value; }
			get { return _finger; }
		}
		
		//TODO: Synchronize
		public bool isConnected() 
		{
			throw new NotImplementedException();
		}
		
		public long MessageDelay
		{
			set 
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("Cannot have negative time.");
				_messageDelay = value;
			}
			get { return _messageDelay; }
		}
		
		public int MaxLineLength
		{
			get { throw new NotImplementedException(); }
		}
		
		public int OutgoingQueueSize
		{
			get { return _outQueue.Count; }
		}
		
		public string Server
		{
			get { return _server; }
		}
		
		public int Port
		{
			get { return _port; }
		}
		
		public string Password
		{
			get { return _password; }
		}
		
		public int[] longToIp (long address)
		{
			int[] ip = new int [4];
			for (int i = 3; i >= 0; i--)
			{
				ip[i] = (int) (address % 256);
				address /= 256;
			}
			return ip;
		}
		
		public long ipToLong(byte[] address)
		{
			if (address.Length != 4)
				throw new ArgumentException("byte array must be of length 4");
			long ipNum = 0;
			long multiplier = 1;
			for (int i = 3; i >= 0; i--) 
			{
				int byteVal = (address[i] + 256) % 256;
				ipNum += byteVal * multiplier;
				multiplier *= 256;
			}
			return ipNum;
		}
		
		//TODO: Figure out character encoding for C#...
		public string Encoding
		{
			get { return _charset; }
			set 
			{ 
				//add encoding support check
				_charset = value; 
			}
		}
		
		public InetAddress InetAddress 
		{
			get { return _inetAddress; }
		}
		
		public InetAddress DccInetAddress
		{
			set { _dccInetAddress = value; }
			get { return _dccInetAddress; }
		}
		
		public int[] DccPorts
		{
			get 
			{
				if (_dccPorts == null || _dccPorts.Length == 0)
					return null;
				return (int[])_dccPorts.Clone();
			}
			set
			{
				if (value == null || value.Length == 0)
					_dccPorts = null;
				_dccPorts = value;
			}
		}
		
		//TODO: check if we need an equality operator, seems pointless
		
		//TODO: check if we need to do base.hashCode(), looks like java only stuff
		
		public string toString () 
		{
			return "Version{" + _version + "}" +
                " Connected{" + isConnected() + "}" +
                " Server{" + _server + "}" +
                " Port{" + _port + "}" +
                " Password{" + _password + "}";
		}
		
		public User[] getUsers (string channel)
		{
			throw new NotImplementedException();
		}
		
		//TODO: figure out what's going on in this function...
		public string[] getChannels () 
		{
			throw new NotImplementedException();
		}
		
		public void dispose ()
		{
			throw new NotImplementedException();
		}
		
		private void addUser (string channel, User user)
		{
			throw new NotImplementedException();
		}
		
		private User removeUser (string channel, User user)
		{
			throw new NotImplementedException();
		}
		
		private void removeUser (string nick)
		{
			throw new NotImplementedException();
		}
		
		private void renameUser (string oldNick, string newNick)
		{
			throw new NotImplementedException();
		}
		
		private void removeChannel (string channel)
		{
			throw new NotImplementedException();
		}
		
		private void removeAllChannels ()
		{
			throw new NotImplementedException();
		}
		
		private void updateUser (string channel, int userMode, string nick)
		{
			throw new NotImplementedException();
		}
	}
}
