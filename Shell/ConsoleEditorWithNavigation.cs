using System;

namespace Shell
{
	public class ConsoleEditorWithNavigation : ConsoleEditorWithRemove
	{
		protected override void ProcessKey(ConsoleKeyInfo consoleKey, InputState inputState)
		{
			switch(consoleKey.Key)
			{
				case ConsoleKey.End:
					if(inputState.CarrageIndex < inputState.Line.Length)
					{
						inputState.GotoEnd();
					}
					break;
				case ConsoleKey.Home:
					if(inputState.CarrageIndex > 0)
					{
						inputState.GotoBegin();
					}
					break;
				case ConsoleKey.LeftArrow:
					if(inputState.CarrageIndex > 0)
					{
						inputState.CarrageIndex--;
						Console.Write('\b');
					}
					break;
				case ConsoleKey.RightArrow:
					if(inputState.CarrageIndex < inputState.Line.Length)
					{
						Console.Write(inputState.Line.ToString().Substring(inputState.CarrageIndex, 1));
						inputState.CarrageIndex++;
					}
					break;
				default:
					base.ProcessKey(consoleKey, inputState);
					return;
			}
		}
	}
}