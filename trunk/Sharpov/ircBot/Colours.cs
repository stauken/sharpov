
using System;

namespace Sharpov
{
	public class Colours
	{
		
		public static String NORMAL = "\u000f";
		public static String BOLD = "\u0002";
		public static String UNDERLINE = "\u001f";
		public static String REVERSE = "\u0016";
		public static String WHITE = "\u000300";
		public static String BLACK = "\u000301";
		public static String DARK_BLUE = "\u000302";
		public static String DARK_GREEN = "\u000303";
		public static String RED = "\u000304";
		public static String BROWN = "\u000305";
		public static String PURPLE = "\u000306";
		public static String OLIVE = "\u000307";
		public static String YELLOW = "\u000308";
		public static String GREEN = "\u000309";
		public static String TEAL = "\u000310";
		public static String CYAN = "\u000311";
		public static String BLUE = "\u000312";
		public static String MAGENTA = "\u000313";
		public static String DARK_GRAY = "\u000314";
		public static String LIGHT_GRAY = "\u000315";

		public Colours()
		{
		}
    
		public static String removeColors(String line) {
	        int length = line.Length;
	        //StringBuffer buffer = new StringBuffer();
			String buffer = "";
	        int i = 0;
	        while (i < length) {
	            char ch = line[i];
	            if (ch == '\u0003') {
	                i++;
	                // Skip "x" or "xy" (foreground color).
	                if (i < length) {
	                    ch = line[i];
	                    if (Char.IsDigit(ch)) {
	                        i++;
	                        if (i < length) {
	                            ch = line[i];
	                            if (Char.IsDigit(ch)) {
	                                i++;
	                            }
	                        }
	                        // Now skip ",x" or ",xy" (background color).
	                        if (i < length) {
	                            ch = line[i];
	                            if (ch == ',') {
	                                i++;
	                                if (i < length) {
	                                    ch = line[i];
	                                    if (Char.IsDigit(ch)) {
	                                        i++;
	                                        if (i < length) {
	                                            ch = line[i];
	                                            if (Char.IsDigit(ch)) {
	                                                i++;
	                                            }
	                                        }
	                                    }
	                                    else {
	                                        // Keep the comma.
	                                        i--;
	                                    }
	                                }
	                                else {
	                                    // Keep the comma.
	                                    i--;
	                                }
	                            }
	                        }
	                    }
	                }
	            }
	            else if (ch == '\u000f') {
	                i++;
	            }
	            else {
					buffer += ch;
	                i++;
	            }
	        }
	        return buffer;
	    }

	

		public static String removeFormatting(String line) {
			int length = line.Length;
			String buffer = "";
			for (int i = 0; i < length; i++) {
				char ch = line[i];
			    if (ch == '\u000f' || ch == '\u0002' || ch == '\u001f' || ch == '\u0016') { }
			    else buffer = buffer + ch;
			}
			return buffer;
		}
	    
	    
	    public static String removeFormattingAndColors(String line) {
			return removeFormatting(removeColors(line));
		}
	}
	
}
