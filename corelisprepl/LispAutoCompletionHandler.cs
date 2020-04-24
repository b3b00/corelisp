using System;
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
            
            return CoreLisp.Context.Scope.Keys.ToArray();
            
        }
    }
}