using System;
using System.Collections;
using System.Linq;
using core.lisp.lexer;
using core.lisp.model;
using sly.lexer;
using sly.lexer.fsm;

namespace lispparser.core.lisp.lexer
{
    public class LispCustomLexer : ILexer<LispLexer>
    {
        public void AddDefinition(TokenDefinition<LispLexer> tokenDefinition)
        {
        }

        public LexerResult<LispLexer> Tokenize(string source)
        {
            var memorySource = new ReadOnlyMemory<char>(source.ToCharArray());
            return Tokenize(memorySource);
        }

        public LexerResult<LispLexer> Tokenize(ReadOnlyMemory<char> source)
        {
            
            
            LexerPosition position = new LexerPosition();
            while (position.Index <= source.Length)
            {
                
            }
            
            return null;
        }



        public (bool IsEnd, LexerPosition position) GoToNextToken(ReadOnlyMemory<char> source, LexerPosition position)
        {
            if (position.Index >= source.Length - 1)
            {
                return (true, position);
            }


            var sourceSpan = source.Span;

            int line = position.Line;
            int column = position.Column;
            int index = position.Index;

            ReadOnlySpan<char> space = " ".AsSpan();
            ReadOnlySpan<char> tab = "\t".AsSpan();

            int spacePos = boundedPos(sourceSpan.IndexOf(space),source.Length);
            int tabPos = boundedPos(sourceSpan.IndexOf(tab),source.Length);
            int nextPos = Math.Min(spacePos, tabPos);
            char nextChar = sourceSpan[nextPos + 1];
            if (nextPos < 0)
            {
                return (index >= source.Length, new LexerPosition(index, line, column));
            }


            while (nextChar == ' ' || nextChar == '\t')
            {
                spacePos = sourceSpan.IndexOf(space);
                tabPos = sourceSpan.IndexOf(tab);
                nextPos = Math.Min(spacePos, tabPos);


                nextChar = sourceSpan[nextPos + 1];
                var eol = EOLManager.IsEndOfLine(source, nextPos + 1);

                switch (eol)
                {
                    case EOLType.Windows:
                    {
                        index = nextPos + 2;
                        break;
                    }
                    case EOLType.Mac:
                    case EOLType.Nix:
                    {
                        index = nextPos + 1;
                        column = 0;
                        line++;
                        break;
                    }
                    default:
                    {
                        index = nextPos + 1;
                        break;
                    }
                }
            }

            return (index >= source.Length, new LexerPosition(index, line, column));



        }

        int boundedPos(int p,int max)
        {
            if (p >= 0)
            {
                return p;
            }

            return max;
        }


        public (bool IsEnded, LexerPosition position) GoToNextSpace(ReadOnlyMemory<char> source, LexerPosition position)
        {
            if (position.Index >= source.Length - 1)
            {
                return (true, position);
            }


            var sourceSpan = source.Span;

            int line = position.Line;
            int column = position.Column;
            int index = position.Index;

            ReadOnlySpan<char> space = " ".AsSpan();
            ReadOnlySpan<char> tab = "\t".AsSpan();

            int spacePos = boundedPos(sourceSpan.IndexOf(space),source.Length);
            int tabPos = boundedPos(sourceSpan.IndexOf(tab),source.Length);
            index = Math.Min(spacePos, tabPos);
            char nextChar = sourceSpan[index];
            if (index < 0)
            {
                return (index >= source.Length, new LexerPosition(index, line, column));
            }

            
            while (nextChar != ' ' && nextChar != '\t'
                   && EOLManager.IsEndOfLine(source,index) == EOLType.No
                   && index < source.Length)
            {
                spacePos = sourceSpan.IndexOf(space);
                tabPos = sourceSpan.IndexOf(tab);
                index = Math.Min(spacePos, tabPos);


                nextChar = sourceSpan[index];
                column++;
            }

            return (index >= source.Length, new LexerPosition(index,line,column)); 

        }


        public Token<LispLexer> GetCurrentToken(ReadOnlyMemory<char> source, LexerPosition position)
        {
            string value = "";
            var sourceSpan = source.Span;

            int index = position.Index;
            
            if (position.Index >= source.Length - 1)
            {
                return new Token<LispLexer>(LispLexer.EOS, ReadOnlyMemory<char>.Empty, position);
            }

            char currentChar = sourceSpan[position.Index];

            var end = GoToNextSpace(source, position);

            var val = source.Slice(position.Index, end.position.Index-position.Index);
            var spanValue = sourceSpan.Slice(position.Index, end.position.Index);
            Token<LispLexer> token = new Token<LispLexer>(LispLexer.SYMBOL, val, position, false, CommentType.No);

            return token;



        }




    }
}