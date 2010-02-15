using System;
using System.Collections.Generic;

namespace Sharpov
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			String temp2 = "Hurro World";
			
			for (int i = 0; i < temp2.Length; i++) {
				Console.WriteLine(temp2[i]);
			}
			
			String temp = "";
				
			temp += 'a';
			
			Console.WriteLine(temp);
			
			//Queue<String> werd = new Queue<string> ();
		}
	}
}