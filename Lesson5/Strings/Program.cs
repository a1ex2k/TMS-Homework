using Strings;

var mainMenu = new ConsoleMenu.Menu("Select task");
mainMenu.SetItem('1',"Basic tasks", BasicTaskRunner.Run);
mainMenu.SetItem('2',"Task from presentation", MainTaskRunner.Run);
mainMenu.Run();