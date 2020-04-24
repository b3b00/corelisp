using System;
using System.Collections.Generic;
using System.IO;
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

        public char[] Separators { get; set; } =  { ' ', '(' };

        public string[] GetSuggestions(string text, int index)
        {
            List<string> commands = new List<string>() {"load", "context", "quit"};

            if (text.StartsWith("load "))
            {
                var path = text.Substring(5);
                
                if (Directory.Exists(path))
                {
                    var props = Directory.GetDirectories(path).ToList();
                    props.AddRange(Directory.GetFiles(path).ToList());
                    return props.ToArray();    
                }
                else
                {
                    int ipath = Math.Max(path.LastIndexOf("\\"), path.LastIndexOf("/"));
                    if (ipath > 0)
                    {
                        string parent = path.Substring(0, ipath+1);
                        if (Directory.Exists(parent))
                        {
                            string namestart = path.Substring(ipath + 1);
                            var props = Directory.GetDirectories(parent).ToList();
                            props.AddRange(Directory.GetFiles(parent).ToList());
                            props = props.Where(x =>
                            {
                                string name = x.Substring(parent.Length);
                                ;
                                return name.StartsWith(namestart,StringComparison.OrdinalIgnoreCase);
                            }).ToList();
                            return props.ToArray();
                        }
                    }
                }

                return null;
            }

            int i = text.LastIndexOf("(");
            string start = text;
            if (i >= 0)
            {
                commands.AddRange(CoreLisp.Context.Scope.Keys.ToList());
                start = text.Substring(i + 1);
                start = start.Trim();
            }
            
            var t = commands.Where(x => x.StartsWith(start.Trim()));
            List<string> suggestions = new List<string>();
            foreach (var maybe in t)
            {
                string prop = maybe;
                // if (leadingSpaces == 0)
                // {
                //     string mm = text.Substring(0, Math.Max(0, i + 1 + leadingSpaces));
                //     prop = mm + maybe;
                // }

                suggestions.Add(prop);
                ;
            }
            // var suggestions = t.Select(maybe =>
            // {
            //     string mm = text.Substring(0, Math.Max(0,i+1));
            //     string prop = lead + maybe;
            //     return prop;
            // }).ToArray();

            return suggestions.ToArray();
        }
    }
}