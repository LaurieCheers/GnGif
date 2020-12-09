using System;
using System.Drawing;
using System.Drawing.Imaging;
using Gif.Components;

namespace Example
{
	class ExampleMain
	{
		static int charToHex(char c)
		{
			if (c >= '0' && c <= '9')
				return c - '0';
			else if (c >= 'a' && c <= 'f')
				return (c - 'a') + 10;
			else
				return 0;
		}

		static byte charsToHexByte(char a, char b)
		{
			return (byte)(charToHex(a) * 16 + charToHex(b));
		}

		[STAThread]
		static void Main(string[] args)
		{
			/* create Gif */
			//you should replace filepath
			String [] imageFilePaths = new String[]{ "C:\\Users\\lauri\\Documents\\GitHub\\FaerieForeman\\testframe.png"}; 
			String outputFilePath = "test.gif";

			string paletteText = System.IO.File.ReadAllText("C:\\Users\\lauri\\Documents\\GitHub\\FaerieForeman\\Assets\\GifPalette.txt");
			byte[] paletteBytes = new byte[256 * 3];
			int index = 0;
			foreach (string s in paletteText.Split('\n'))
			{
				paletteBytes[index] = charsToHexByte(s[1], s[2]);
				index++;
				paletteBytes[index] = charsToHexByte(s[3], s[4]);
				index++;
				paletteBytes[index] = charsToHexByte(s[5], s[6]);
				index++;
			}

			AnimatedGifEncoder e = new AnimatedGifEncoder();
			e.SetFixedPalette(paletteBytes);
			e.Start( outputFilePath );
			e.SetDelay(500);
			//-1:no repeat,0:always repeat
			e.SetRepeat(0);
			for (int i = 0, count = imageFilePaths.Length; i < count; i++ ) 
			{
				e.AddFrame( Image.FromFile( imageFilePaths[i] ) );
			}
			e.Finish();
			/* extract Gif */
			/*string outputPath = "c:\\";
			GifDecoder gifDecoder = new GifDecoder();
			gifDecoder.Read( "c:\\test.gif" );
			for ( int i = 0, count = gifDecoder.GetFrameCount(); i < count; i++ ) 
			{
				Image frame = gifDecoder.GetFrame( i );  // frame i
				frame.Save( outputPath + Guid.NewGuid().ToString() + ".png", ImageFormat.Png );
			}*/
		}
	}
}
