using Lab3App;

List<Watches> watches = new();
Random random = new();

for (int i = 0; i < Constants.NumberOfWatches; i++)
{
    var model = Constants.Models[random.Next(Constants.Models.Length)];
    var type = Constants.Types[random.Next(Constants.Types.Length)];
    watches.Add(Watches.Create(i + 1, model, $"SN{i + 1}", type));
}

// Initialize file processor
FileProcessor processor = new();

// Execute Task 1: Write watches to files using threads
Console.WriteLine("Task 1:");
processor.Task1(watches);

// Execute Task 2: Merge files into a single file
Console.WriteLine("Task 2:");
processor.Task2();

// Execute Task 3.1: Read and print watches sequentially
Console.WriteLine("Task 3.1:");
processor.Task3_1();

// Execute Task 3.2: Read and print watches using two threads
Console.WriteLine("Task 3.2:");
processor.Task3_2();

// Execute Task 3.3: Read and print watches using semaphore-controlled threads
Console.WriteLine("Task 3.3:");
processor.Task3_3();

// Wait for user input before exiting
Console.ReadLine();
