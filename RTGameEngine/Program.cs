using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTGameEngine
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			AllocConsole();
			Console.WriteLine("STart");
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new GameWindow());
		}

		// Allows the command line to be seen during normal execution
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAsAttribute(UnmanagedType.Bool)]
		static extern bool AllocConsole();
	}
}
