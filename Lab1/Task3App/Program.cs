using System.Reflection;

try
{
    Assembly assembly = Assembly.LoadFrom("..\\..\\..\\..\\Lab1Library\\bin\\Debug\\net9.0\\Lab1Library.dll");
    Type watchesType = assembly.GetType("Lab1Library.Watches");
    Type enumType = assembly.GetType("Lab1Library.WatchesType");
    object enumValue = Enum.Parse(enumType, "Electronic");
    MethodInfo createMethod = watchesType.GetMethod("Create");
    object watchesInstance = createMethod.Invoke(null, new object[] { 1, "Rolex", "SN123", enumValue });
    MethodInfo printMethod = watchesType.GetMethod("PrintObject");
    printMethod.Invoke(watchesInstance, null);

    Type manufacturerType = assembly.GetType("Lab1Library.Manufacturer");
    createMethod = manufacturerType.GetMethod("Create");
    object manufacturerInstance = createMethod.Invoke(null, new object[] { "Apple", "USA", true });
    printMethod = manufacturerType.GetMethod("PrintObject");
    printMethod.Invoke(manufacturerInstance, null);
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}
