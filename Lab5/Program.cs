using Lab5App;

var processor = new FileTaskProcessor();

Console.WriteLine("Task 1:");
await processor.Task1();
Console.WriteLine("Files written.");
Console.WriteLine();

Console.WriteLine("Task 2:");
await processor.Task2();
Console.WriteLine("File3 written.");
Console.WriteLine();

Console.WriteLine("Task 3:");
await processor.Task3();
Console.WriteLine("Task 3 completed.");
Console.WriteLine();
