using System;
using System.Collections;
using System.Text;

namespace Shell
{
	public abstract class ConsoleEditorBase
	{
		private bool running;
		public string Prompt { get; set; }
		public event Action<string> CommandReceived = delegate { };

		public void Stop(string goodbyeMessage)
		{
			Console.WriteLine(goodbyeMessage);
			running = false;
		}

		public void Start()
		{
			running = true;
			while(running)
			{
				string line = GetLine();
				CommandReceived(line);
			}
		}

		private string GetLine()
		{
			Console.Write(Prompt);
			var accumulator = new InputState();

			do
			{
				ProcessKey(Console.ReadKey(true), accumulator);
			}
			while(!accumulator.Done);

			Console.WriteLine();
			return accumulator.Line.ToString();
		}

		protected abstract void ProcessKey(ConsoleKeyInfo consoleKey, InputState inputState);

		protected class InputState
		{
			public InputState()
			{
				Line = new StringBuilder();
			}

			public StringBuilder Line { get; private set; }
			public int CarrageIndex { get; set; }
			public bool Done { get; set; }
			public Hashtable Data = new Hashtable();

			public void GotoEnd()
			{
				PrintTailInternal("");
				CarrageIndex = Line.Length;
			}

			public void GotoBegin()
			{
				Console.Write(new string('\b', CarrageIndex));
				CarrageIndex = 0;
			}

			public void RefreshTail(int symbolsToCleanAfterTail = 0)
			{
				var correctCursor = new { Console.CursorLeft, Console.CursorTop };
				PrintTailInternal(new string(' ', symbolsToCleanAfterTail));
				Console.SetCursorPosition(correctCursor.CursorLeft, correctCursor.CursorTop);
			}

			private void PrintTailInternal(string suffix)
			{
				Console.Write(Line.ToString().Substring(CarrageIndex) + suffix);
			}
		}
	}
}