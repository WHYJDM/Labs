using Lab3App;

List<Watches> watches = new();
Random random = new();
string[] models = { "Rolex", "Casio", "Seiko", "Omega", "Tag Heuer" };
WatchesType[] types = { WatchesType.Electronic, WatchesType.Mechanic, WatchesType.Tower };

for (int i = 0; i < 20; i++)
{
    var model = models[random.Next(models.Length)];
    var type = types[random.Next(types.Length)];
    watches.Add(Watches.Create(i + 1, model, $"SN{i + 1}", type));
}

FileProcessor processor = new();

Console.WriteLine("Task 1:");
processor.Task1(watches);

Console.WriteLine("Task 2:");
processor.Task2();

Console.WriteLine("Task 3.1:");
processor.Task3_1();

Console.WriteLine("Task 3.2:");
processor.Task3_2();

Console.WriteLine("Task 3.3:");
processor.Task3_3();

Console.ReadLine();
