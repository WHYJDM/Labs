using Lab5App;

var processor = new MultithreadingProcessor();

Console.WriteLine("Write Files:");
await processor.WriteFilesAsync();
Console.WriteLine("Files written.");
Console.WriteLine();

Console.WriteLine("Read Files:");
processor.SetTotalRecords(50);
await processor.ReadFilesAsync();
Console.WriteLine("Read Files completed.");
Console.WriteLine();

Console.WriteLine("Start Sorting Process:");
await processor.StartSortingProcess();
Console.WriteLine("Sorting Process completed.");
Console.WriteLine();
