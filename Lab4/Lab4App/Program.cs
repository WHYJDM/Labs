using Lab4App;

var processor = new FileTaskProcessor();

Console.WriteLine("Task 1:");
processor.Task1();
Console.WriteLine("Files written.");

Console.WriteLine("Task 2:");
processor.Task2();
Console.WriteLine("File3 written.");

Console.WriteLine("Task 3:");
await processor.Task3();
Console.WriteLine("Task 3 completed.");
