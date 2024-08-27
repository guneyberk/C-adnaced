//Inspect contents of assembly (types, methods, properties)
//inspect metadata, (class names, base classes, interfaces, fields)
//Methods, constructors, and properties can be dynamically invoked (private protected)
//Inspect custom attributes applied to classes, methods, or properties useful for implementing custom behaviours
//Not directly referenced
//Bypass accses modifiers

//USE CASES FOR REFLECTIONS 
//  Frameworks and Libraries Like ASP.NET for model binding, dependency injection, ORM tools like entitiy Framework
//  Dynamic loading Load assemblies andd types at runtime without knowing them at compile time enable plugin architecture
//  Code generation tools that generate code based on metadata such as serializers and proxies
//  Testing and tools used in testing frameworks (NUnit, MSTest) to discover run and test methods
//PERFORMANCE CONSIDERATIONS
//  Slower than direct method calls or property access (looking up metadata and possibly bypassing certain compiler optimizations)
//SECURITY CONSIDERATIONS
//  Accessing private members (Can bypass access modifiers)
using System;
using System.Reflection; 
public class Person{
    public string FirstName { get; set; }
    public string LastName { get; set;}

    public void Hello(){
        Console.WriteLine($"Hello, my name is {FirstName} {LastName}");
    }
}

class Program{
    static void Main(string[] args) {

        Type personType = typeof(Person);

        Console.WriteLine("Class Name: " +personType.FullName);

        Console.WriteLine("Properties: ");
        PropertyInfo[] propertyInfos= personType.GetProperties();
        foreach(var prop in propertyInfos){
            Console.WriteLine($"{prop.Name} ({prop.PropertyType.Name})");
        }

        Console.WriteLine("Methods");
        MethodInfo[] methodsInfos= personType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
        foreach(var method in methodsInfos){
            Console.WriteLine($"{method.Name}");
        }

        object personInstance = Activator.CreateInstance(personType);

        PropertyInfo firstNameProp = personType.GetProperty("FirstName");
        firstNameProp.SetValue(personInstance, "John");

        PropertyInfo lastNameProp = personType.GetProperty("LastName");
        lastNameProp.SetValue(personInstance,"Doe");

        MethodInfo helloMethod = personType.GetMethod("Hello");
        helloMethod.Invoke(personInstance,null);

        Type sampleType = typeof(SampleClass);

        object[] attriutes = sampleType.GetCustomAttributes(false);


        foreach(var attr in attriutes){
            if(attr is DevInfoAttribute devInfo){
                Console.WriteLine($"Developer: {devInfo.Developer}");
                Console.WriteLine($"Last Modified: {devInfo.LastModified}");
            }
        }
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class DevInfoAttribute : Attribute{
    public string Developer{ get; set;}
    public string LastModified{ get; set;}

    public DevInfoAttribute(string developer, string lastModified){

        Developer = developer;
        LastModified = lastModified;
        
    }

}

[DeveloperInfo("John Doe" ,"2024-08-28")]
public class SampleClass {
    public void Display => Console.WriteLine("Sample Class Display Method");
}

