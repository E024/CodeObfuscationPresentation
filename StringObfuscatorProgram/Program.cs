using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using Mono.Cecil.Cil;
using static Mono.Cecil.Cil.OpCodes;
using OpCode = System.Reflection.Emit.OpCode;

namespace StringObfuscatorProgram
{
    class Program
    {


        static void Main(string[] args)
        {

            string path = @"C:\projects\CodeObfuscation\Dotfuscated\";
            //string path = @"C:\projects\CodeObfuscation\AntiTampering\bin\Release\";

            string name = "StringObfuscation.exe";

            string assembly = $"{path}{name}";

            var resolver = new DefaultAssemblyResolver();
            resolver.AddSearchDirectory(path);
            resolver.Resolve("System");

            var definition = AssemblyDefinition.ReadAssembly(assembly,
                new ReaderParameters {AssemblyResolver = resolver});

            var obfuscateTypeDefinition = CreateObfuscator(definition);

            //definition.MainModule.Types.Add(obfuscateTypeDefinition);

            SearchString(definition);

            definition.Write($"{path}Obfuscated.exe");
        }

        static void SearchString(AssemblyDefinition definition)
        {

            var oType = definition.MainModule.Types.First(x => x.Name.Equals("Program"));
            var unEncMethod = oType.Methods.First(x => x.Name.Equals("unencrypt", StringComparison.OrdinalIgnoreCase));


            var type = definition.MainModule.Types.First(x => x.Name.Equals("Program"));
            {
                foreach (var method in type.Methods)
                {
                    var processor = method.Body.GetILProcessor();

                    foreach (var instruction in processor.Body.Instructions)
                    {
                        if (instruction.OpCode == Ldstr)
                        {
                            var text =  instruction.Operand.ToString();

                            var bytes = System.Text.Encoding.ASCII.GetBytes(text);

                            var base64Text = System.Convert.ToBase64String(bytes);

                            processor.InsertAfter(instruction, processor.Create(OpCodes.Call, unEncMethod));
                            processor.InsertAfter(instruction, processor.Create(OpCodes.Ldstr, base64Text));

                            processor.Remove(instruction);

                            break;
                        }
                    }

                }
            }

        }

        static TypeDefinition CreateObfuscator(AssemblyDefinition definition)
        {

            var obfuscator = new MethodDefinition("Unencrypt", MethodAttributes.Static,
                definition.MainModule.TypeSystem.String);

            obfuscator.Parameters.Add(new ParameterDefinition(definition.MainModule.TypeSystem.String));

            var methods = definition.MainModule.TypeSystem.String.Resolve().Methods;
            var getString = definition.MainModule.
                    ImportReference(
                    System.
                    Text.
                    Encoding.
                    ASCII.
                    GetType().
                    GetMethod("GetString", new Type[] {typeof(byte[])}));


            var getAscii = definition.MainModule.
                ImportReference(
                    typeof(System.
                        Text.
                        Encoding).GetMethod("get_ASCII"));


            var convertFromString =
                definition.MainModule.ImportReference(
                    typeof(System.Convert).GetMethod("FromBase64String", new Type[] {typeof(string)})
                );


            var con = definition.MainModule.ImportReference(typeof(System.Convert));


            var toLower =
                definition.MainModule.ImportReference(typeof(string).GetMethod("ToLower", new Type[] { }));

            var processor = obfuscator.Body.GetILProcessor();

            processor.Body.InitLocals = true;

            processor.Body.Variables.Add(new VariableDefinition(definition.MainModule.TypeSystem.String));

            processor.Append(processor.Create(OpCodes.Nop));
            processor.Append(processor.Create(OpCodes.Call, getAscii));
            processor.Append(processor.Create(Ldarg_0));
            processor.Append(processor.Create(OpCodes.Call, convertFromString));
            processor.Append(processor.Create(OpCodes.Callvirt, getString));
            processor.Append(processor.Create(OpCodes.Stloc_0));
            processor.Append(processor.Create(Ldloc_0));
            processor.Append(processor.Create(OpCodes.Ret));

            var t = definition.MainModule.Types.First(x => x.Name.Equals("Program"));
            

            t.Methods.Add(obfuscator);

            return t;
        }


    }
}
