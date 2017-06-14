using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace EncryptStrings
{
    class Program
    {
        static void Main(string[] args)
        {

            string assembly = "";

            var definition = AssemblyDefinition.ReadAssembly(assembly);

            foreach (var t in definition.MainModule.Types)
            {
                foreach (var m in t.Methods)
                {
                    Console.WriteLine(m.Name);
                    if (m.Name == "AddTwoNumbers")
                    {
                        var processor = m.Body.GetILProcessor();
                        var instructions = processor.Body.Instructions.Skip(1).ToList();

                        foreach (var i in instructions)
                        {
                            processor.Remove(i);
                        }

                        processor.Append(processor.Create(OpCodes.Ldarg_0));

                        processor.Append(processor.Create(OpCodes.Ret));
                    }
                }
            }

        }
    }
}
