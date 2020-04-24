using System;
using System.Collections.Generic;
using System.Linq;
using coreLisp;

namespace corelisprepl
{
    class LispAutoCompletionHandler : IAutoCompleteHandler
    {
        private CoreLisp CoreLisp { get; set; }
        
        public LispAutoCompletionHandler(CoreLisp coreLisp)
        {
            CoreLisp = coreLisp;
        }

        // characters to start completion from
        public char[] Separators { get; set; } = new char[] { ' ', '.', '/' };

        // text - The current text entered in the console
        // index - The index of the terminal cursor within {text}
        public string[] GetSuggestions(string text, int index)
        {
            List<string > commands = new List<string>() {"load", "context", "quit"};
            commands.AddRange(CoreLisp.Context.Scope.Keys.ToList());
            
            int i = text.LastIndexOf("(");
            string start = text;
            if (i >= 0)
            {
                start = text.Substring(i+1);
            }
            
            var t =  commands.Where(x => x.StartsWith(start));
            foreach (var maybe in t)
            {
                string mm = text.Substring(0, Math.Max(0,i+1));
                string prop = mm + maybe;
                ;
            }
                return t.Select(x => text.Substring(0,Math.Max(0,i+1)) + x).ToArray();

        }
    }
}