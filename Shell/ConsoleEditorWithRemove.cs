using System;

namespace Shell
{
	public class ConsoleEditorWithRemove : SimpleConsoleEditor
	{
		protected override void ProcessKey(ConsoleKeyInfo consoleKey, InputState inputState)
		{
			switch(consoleKey.Key)
			{
				case ConsoleKey.Backspace:
					if(inputState.CarrageIndex > 0)
					{
						inputState.Line.Remove(inputState.CarrageIndex - 1, 1);
						inputState.CarrageIndex--;
						Console.Write("\b");
						inputState.RefreshTail(1);
					}
					break;
				case ConsoleKey.Delete:
					if(inputState.CarrageIndex < inputState.Line.Length)
					{
						inputState.Line.Remove(inputState.CarrageIndex, 1);
						inputState.RefreshTail(1);
					}
					break;
				default:
					base.ProcessKey(consoleKey, inputState);
					break;
			}
		}
	}
}