using System.Reflection;

while (true)
{
    Console.Write("Enter class name: ");
    string className = Console.ReadLine();
    if (string.IsNullOrEmpty(className)) break;
    Console.Write("Enter method name: ");
    string methodName = Console.ReadLine();
    Console.Write("Enter arguments (comma separated): ");
    string argsInput = Console.ReadLine();
    string[] argStrings = argsInput.Length > 0 ? argsInput.Split(',') : [] ;
    try
    {
        Assembly assembly = Assembly.LoadFrom("..\\..\\..\\..\\Lab1Library\\bin\\Debug\\net9.0\\Lab1Library.dll");
        Type type = assembly.GetType(className);
        if (type == null) throw new Exception("Class not found");
        MethodInfo method = type.GetMethod(methodName);
        if (method == null) throw new Exception("Method not found");
        ParameterInfo[] parameters = method.GetParameters();
        if (parameters.Length != argStrings.Length) throw new Exception("Argument count mismatch");
        object[] arguments = new object[parameters.Length];
        for (int i = 0; i < parameters.Length; i++)
        {
            string argStr = argStrings[i].Trim();
            Type paramType = parameters[i].ParameterType;
            if (paramType.IsEnum)
            {
                arguments[i] = Enum.Parse(paramType, argStr);
            }
            else
            {
                arguments[i] = Convert.ChangeType(argStr, paramType);
            }
        }
        object instance = method.IsStatic ? null : Activator.CreateInstance(type);
        object result = method.Invoke(instance, arguments);
        Console.WriteLine("Result: " + result);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}
