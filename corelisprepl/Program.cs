using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using core.lisp;

namespace core.lisp.repl
{
    class Program
    {
        
        private static CoreLisp coreLisp = new CoreLisp();

        public delegate  void Command(string input);


        public static void Load(string input)
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

        public static void Clear(string input)
        {
            coreLisp = new CoreLisp();
        }

        public static void Context(string input)
        {
            foreach (var key in coreLisp.Context.Scope.Keys)
            {
                Console.WriteLine(key);
            }
        }

        public static Dictionary<string, Command> Commands = new Dictionary<string, Command>()
        {
            { "load", Load },
            { "clear", Clear },
            { "context", Context }
        };
    
        
        static void Main(string[] args)
        {
            
            
            ReadLine.AutoCompletionHandler = new LispAutoCompletionHandler(coreLisp);
            ReadLine.HistoryEnabled = true;
            string input = ReadLine.Read("> ");

            while (input != "quit")
            {

                bool isCommand = false;
                foreach (var command in Commands)
                {
                    if (input.StartsWith(command.Key))
                    {
                        command.Value(input);
                        isCommand = true;
                        break;
                    }
                }
                
                
                if (!isCommand)
                {
                    var t = coreLisp.Run(input);
                    Console.WriteLine(t);
                }

                input = ReadLine.Read(">");
                
            }
        }
    }
}