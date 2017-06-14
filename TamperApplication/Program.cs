using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace TamperApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = @"C:\projects\CodeObfuscation\Dotfuscated\";
            //string path = @"C:\projects\CodeObfuscation\AntiTampering\bin\Release\";

            string name = "AntiTampering.exe";

            string assembly = $"{path}{name}";

            var resolver = new DefaultAssemblyResolver();
            resolver.AddSearchDirectory(path);

            var definition = AssemblyDefinition.ReadAssembly(assembly, 
                    new ReaderParameters {AssemblyResolver = resolver});

            foreach (var t in definition.MainModule.Types)
            {
                foreach (var m in t.Methods)
                {
                    Console.WriteLine(m.Name);
                    if (m.Name == "ShowPassphrase")
                    {
                        var processor = m.Body.GetILProcessor();
                        //var instructions = processor.Body.Instructions.Skip(1).ToList();

                        var instructions = processor.Body.Instructions.ToList();

                        foreach (var i in instructions)
                        {
                            processor.Remove(i);
                        }

                        processor.Append(processor.Create(OpCodes.Ldstr, "HIJACKED!"));

                        processor.Append(processor.Create(OpCodes.Ret));

                    }
                }
            }

            definition.Write($"{path}\\tampered\\Tampered.exe");

        }
    }
}
