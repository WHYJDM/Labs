using System.Reflection;

try
{
    Assembly assembly = Assembly.LoadFrom("..\\..\\..\\..\\Lab1Library\\bin\\Debug\\net9.0\\Lab1Library.dll");
    Type[] types = assembly.GetTypes();
    foreach (Type type in types)
    {
        Console.WriteLine(type.Name);
        MemberInfo[] members = type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        foreach (MemberInfo member in members)
        {
            Console.WriteLine("  " + member.MemberType + ": " + member.Name);
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}
