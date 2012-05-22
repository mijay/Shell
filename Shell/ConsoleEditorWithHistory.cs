using System;
using System.Collections.Generic;
using System.Linq;

namespace Shell
{
	public class ConsoleEditorWithHistory : ConsoleEditorWithNavigation
	{
		private const string dataKey = "histIndex";
		private readonly List<string> history = new List<string>();

		protected override void ProcessKey(ConsoleKeyInfo consoleKey, InputState inputState)
		{
			switch(consoleKey.Key)
			{
				case ConsoleKey.UpArrow:
					if(history.Any())
					{
						int historyIndex = inputState.Data.ContainsKey(dataKey) ? (int) inputState.Data[dataKey] : history.Count;
						if(historyIndex == 0)
							break;
						--historyIndex;
						inputState.Data[dataKey] = historyIndex;

						PutLineFromHistory(inputState, historyIndex);
					}
					break;
				case ConsoleKey.DownArrow:
					if(history.Any() && inputState.Data.ContainsKey(dataKey))
					{
						var historyIndex = (int) inputState.Data[dataKey];
						if(historyIndex == history.Count - 1)
							break;
						++historyIndex;
						inputState.Data[dataKey] = historyIndex;

						PutLineFromHistory(inputState, historyIndex);
					}
					break;
				default:
					base.ProcessKey(consoleKey, inputState);
					if(inputState.Done)
						history.Add(inputState.Line.ToString());
					break;
			}
		}

		private void PutLineFromHistory(InputState inputState, int historyIndex)
		{
			int oldLength = inputState.Line.Length;
			string newLine = history[historyIndex];

			inputState.GotoBegin();
			inputState.Line.Clear();
			inputState.Line.Append(newLine);
			inputState.GotoEnd();

			if(newLine.Length < oldLength)
				inputState.RefreshTail(oldLength - newLine.Length);
		}
	}
}