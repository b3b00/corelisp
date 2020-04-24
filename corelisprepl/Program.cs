using System;
using System.IO;
using System.Linq;
using core.lisp;

namespace core.lisp.repl
{
    class Program
    {
        
        private static CoreLisp coreLisp = new CoreLisp();
        
        static void Main(string[] args)
        {
            
            ReadLine.AutoCompletionHandler = new LispAutoCompletionHandler(coreLisp);
            ReadLine.HistoryEnabled = true;
            string input = ReadLine.Read("> ");
            while (input != "quit")
            {
                if (input.StartsWith("load"))
                {
                    string[] files = input.Split(new char[] {' '});
                    foreach (var file in files.Skip(1))
                    {
                        if (File.Exists(file))
                        {
                            string source = File.ReadAllText(file);
                            coreLisp.Run(source);
                        }
                        else
                        {
                            Console.WriteLine("file not found");
                        }
                    }
                }
                else if (input.StartsWith("context"))
                {
                    foreach (var key in coreLisp.Context.Scope.Keys)
                    {
                        Console.WriteLine(key);
                    }
                }
                else if (input.StartsWith("clear"))
                {
                    coreLisp = new CoreLisp();
                }
                else
                {
                    var t = coreLisp.Run(input);
                    Console.WriteLine(t);
                }

                input = ReadLine.Read(">");
                
            }
        }
    }
}