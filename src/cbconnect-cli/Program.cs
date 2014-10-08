using System;
using System.Text;

namespace cbconnectcli
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("Expected a command. Received {0} args.", args.Length);
			}
			else
			{
				string command = args[0].ToLower();
				switch(command)
				{
				case "extract-images":
					RunCommand(new ExtractSpeakerImageUrlCommand());
					break;
				case "create-baseline-database":
					RunCommand (new CreateBaselineDatabaseCommand ());
					break;
				default:
					Console.WriteLine ("Unknown command.");
					break;
				}
			}
		}

		static void RunCommand<T> (T command) where T : ICommand
		{
			command.Run ();
			Console.WriteLine ("Press Enter to close.");
			Console.ReadLine();
			Console.WriteLine ("Exiting [{0}]...", command.ToString ());
		}
	}

}
