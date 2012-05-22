using System;
using System.Linq;

namespace Shell
{
	internal class EntryPoint
	{
		public static void Main(string[] args)
		{
			Console.Clear();
			Console.WriteLine("Welcome, {0}. This is something like shell written in c#.\n", Environment.UserName);

			var editor = new ConsoleEditorWithHistory { Prompt = Environment.UserName + "> " };
			editor.CommandReceived += command => ProcessCommand(command, editor);
			editor.Start();
		}

		private static void ProcessCommand(string command, ConsoleEditorWithHistory editor)
		{
			string[] parsedCommand = command.TrimStart().Split(new[] { ' ' }, 2);
			switch(parsedCommand.FirstOrDefault()) {
			case "quit":
			case "exit":
				editor.Stop("good bye!");
				break;
			case "help":
				Console.WriteLine("Try 'echo' or 'quit'.\nThis is just a sample, so you should write commands by yourself.");
				break;
			case "echo":
				Console.WriteLine(parsedCommand.Length == 2 ? parsedCommand[1] : null);
				break;
			}
		}
	}
}