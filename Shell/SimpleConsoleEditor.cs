using System;

namespace Shell
{
	public class SimpleConsoleEditor : ConsoleEditorBase
	{
		protected override void ProcessKey(ConsoleKeyInfo consoleKey, InputState inputState)
		{
			if(consoleKey.Key == ConsoleKey.Enter)
				inputState.Done = true;
			else if(consoleKey.KeyChar != '\0')
			{
				inputState.Line.Insert(inputState.CarrageIndex, consoleKey.KeyChar);
				Console.Write(consoleKey.KeyChar);
				inputState.CarrageIndex++;
				inputState.RefreshTail();
			}
		}
	}
}