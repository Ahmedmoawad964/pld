
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;
namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF         =  0, // (EOF)
        SYMBOL_ERROR       =  1, // (Error)
        SYMBOL_WHITESPACE  =  2, // Whitespace
        SYMBOL_MINUS       =  3, // '-'
        SYMBOL_EXCLAMEQ    =  4, // '!='
        SYMBOL_PERCENT     =  5, // '%'
        SYMBOL_LPAREN      =  6, // '('
        SYMBOL_RPAREN      =  7, // ')'
        SYMBOL_TIMES       =  8, // '*'
        SYMBOL_COMMA       =  9, // ','
        SYMBOL_DIV         = 10, // '/'
        SYMBOL_CARET       = 11, // '^'
        SYMBOL_PLUS        = 12, // '+'
        SYMBOL_LT          = 13, // '<'
        SYMBOL_LTEQ        = 14, // '<='
        SYMBOL_EQ          = 15, // '='
        SYMBOL_EQEQ        = 16, // '=='
        SYMBOL_GT          = 17, // '>'
        SYMBOL_GTEQ        = 18, // '>='
        SYMBOL_AND         = 19, // and
        SYMBOL_CALL        = 20, // call
        SYMBOL_DIGIT       = 21, // digit
        SYMBOL_DO          = 22, // do
        SYMBOL_ELSE        = 23, // else
        SYMBOL_END         = 24, // end
        SYMBOL_FUNC        = 25, // func
        SYMBOL_ID          = 26, // id
        SYMBOL_LET         = 27, // let
        SYMBOL_OR          = 28, // or
        SYMBOL_REPEAT      = 29, // repeat
        SYMBOL_TIMES2      = 30, // times
        SYMBOL_WHEN        = 31, // when
        SYMBOL_ASS         = 32, // <ass>
        SYMBOL_COND        = 33, // <cond>
        SYMBOL_DIGIT2      = 34, // <digit>
        SYMBOL_EXP         = 35, // <exp>
        SYMBOL_FACTOR      = 36, // <factor>
        SYMBOL_FUNC2       = 37, // <func>
        SYMBOL_FUNCCALL    = 38, // <funccall>
        SYMBOL_ID2         = 39, // <id>
        SYMBOL_OP          = 40, // <op>
        SYMBOL_PARAMS      = 41, // <params>
        SYMBOL_POWER       = 42, // <power>
        SYMBOL_PROGRAM     = 43, // <program>
        SYMBOL_REPEATSTATE = 44, // <repeatstate>
        SYMBOL_STATE       = 45, // <state>
        SYMBOL_STATES      = 46, // <states>
        SYMBOL_TERM        = 47, // <term>
        SYMBOL_VALUE       = 48, // <value>
        SYMBOL_WHENSTATE   = 49  // <whenstate>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM                         =  0, // <program> ::= <states>
        RULE_STATES                          =  1, // <states> ::= <state>
        RULE_STATES2                         =  2, // <states> ::= <state> <states>
        RULE_STATE                           =  3, // <state> ::= <whenstate>
        RULE_STATE2                          =  4, // <state> ::= <ass>
        RULE_STATE3                          =  5, // <state> ::= <repeatstate>
        RULE_STATE4                          =  6, // <state> ::= <func>
        RULE_STATE5                          =  7, // <state> ::= <funccall>
        RULE_ASS_LET_EQ                      =  8, // <ass> ::= let <id> '=' <exp>
        RULE_EXP_PLUS                        =  9, // <exp> ::= <exp> '+' <term>
        RULE_EXP_MINUS                       = 10, // <exp> ::= <exp> '-' <term>
        RULE_EXP                             = 11, // <exp> ::= <term>
        RULE_TERM_TIMES                      = 12, // <term> ::= <term> '*' <factor>
        RULE_TERM_DIV                        = 13, // <term> ::= <term> '/' <factor>
        RULE_TERM_PERCENT                    = 14, // <term> ::= <term> '%' <factor>
        RULE_TERM                            = 15, // <term> ::= <factor>
        RULE_FACTOR                          = 16, // <factor> ::= <power>
        RULE_POWER_CARET                     = 17, // <power> ::= <value> '^' <power>
        RULE_POWER                           = 18, // <power> ::= <value>
        RULE_VALUE_LPAREN_RPAREN             = 19, // <value> ::= '(' <exp> ')'
        RULE_VALUE                           = 20, // <value> ::= <id>
        RULE_VALUE2                          = 21, // <value> ::= <digit>
        RULE_ID_ID                           = 22, // <id> ::= id
        RULE_DIGIT_DIGIT                     = 23, // <digit> ::= digit
        RULE_WHENSTATE_WHEN_DO_ELSE_END      = 24, // <whenstate> ::= when <cond> do <states> else <states> end
        RULE_COND                            = 25, // <cond> ::= <exp> <op> <exp>
        RULE_COND_AND                        = 26, // <cond> ::= <cond> and <cond>
        RULE_COND_OR                         = 27, // <cond> ::= <cond> or <cond>
        RULE_OP_GT                           = 28, // <op> ::= '>'
        RULE_OP_LT                           = 29, // <op> ::= '<'
        RULE_OP_EQEQ                         = 30, // <op> ::= '=='
        RULE_OP_EXCLAMEQ                     = 31, // <op> ::= '!='
        RULE_OP_GTEQ                         = 32, // <op> ::= '>='
        RULE_OP_LTEQ                         = 33, // <op> ::= '<='
        RULE_REPEATSTATE_REPEAT_TIMES_DO_END = 34, // <repeatstate> ::= repeat <exp> times do <states> end
        RULE_FUNC_FUNC_LPAREN_RPAREN_DO_END  = 35, // <func> ::= func <id> '(' <params> ')' do <states> end
        RULE_FUNCCALL_CALL_LPAREN_RPAREN     = 36, // <funccall> ::= call <id> '(' <params> ')'
        RULE_PARAMS                          = 37, // <params> ::= <exp>
        RULE_PARAMS_COMMA                    = 38, // <params> ::= <exp> ',' <params>
        RULE_PARAMS2                         = 39  // <params> ::= 
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox lst;
        ListBox ls;
        public MyParser(string filename,ListBox lst,ListBox ls)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read,  
                                               FileShare.Read);

            this.lst = lst;
            this.ls = ls;

            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);

            parser.OnTokenRead += new LALRParser.TokenReadHandler(Tokenreadevent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PERCENT :
                //'%'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CARET :
                //'^'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_AND :
                //and
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CALL :
                //call
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT :
                //digit
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DO :
                //do
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_END :
                //end
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FUNC :
                //func
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //id
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LET :
                //let
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OR :
                //or
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_REPEAT :
                //repeat
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES2 :
                //times
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHEN :
                //when
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASS :
                //<ass>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COND :
                //<cond>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT2 :
                //<digit>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXP :
                //<exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FUNC2 :
                //<func>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FUNCCALL :
                //<funccall>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID2 :
                //<id>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OP :
                //<op>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PARAMS :
                //<params>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_POWER :
                //<power>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_REPEATSTATE :
                //<repeatstate>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATE :
                //<state>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATES :
                //<states>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERM :
                //<term>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VALUE :
                //<value>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHENSTATE :
                //<whenstate>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM :
                //<program> ::= <states>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATES :
                //<states> ::= <state>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATES2 :
                //<states> ::= <state> <states>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATE :
                //<state> ::= <whenstate>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATE2 :
                //<state> ::= <ass>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATE3 :
                //<state> ::= <repeatstate>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATE4 :
                //<state> ::= <func>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATE5 :
                //<state> ::= <funccall>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASS_LET_EQ :
                //<ass> ::= let <id> '=' <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_PLUS :
                //<exp> ::= <exp> '+' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_MINUS :
                //<exp> ::= <exp> '-' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP :
                //<exp> ::= <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_TIMES :
                //<term> ::= <term> '*' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_DIV :
                //<term> ::= <term> '/' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_PERCENT :
                //<term> ::= <term> '%' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM :
                //<term> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR :
                //<factor> ::= <power>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_POWER_CARET :
                //<power> ::= <value> '^' <power>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_POWER :
                //<power> ::= <value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_LPAREN_RPAREN :
                //<value> ::= '(' <exp> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE :
                //<value> ::= <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE2 :
                //<value> ::= <digit>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ID_ID :
                //<id> ::= id
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIGIT_DIGIT :
                //<digit> ::= digit
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_WHENSTATE_WHEN_DO_ELSE_END :
                //<whenstate> ::= when <cond> do <states> else <states> end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COND :
                //<cond> ::= <exp> <op> <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COND_AND :
                //<cond> ::= <cond> and <cond>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COND_OR :
                //<cond> ::= <cond> or <cond>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GT :
                //<op> ::= '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LT :
                //<op> ::= '<'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EQEQ :
                //<op> ::= '=='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EXCLAMEQ :
                //<op> ::= '!='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GTEQ :
                //<op> ::= '>='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LTEQ :
                //<op> ::= '<='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_REPEATSTATE_REPEAT_TIMES_DO_END :
                //<repeatstate> ::= repeat <exp> times do <states> end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNC_FUNC_LPAREN_RPAREN_DO_END :
                //<func> ::= func <id> '(' <params> ')' do <states> end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCCALL_CALL_LPAREN_RPAREN :
                //<funccall> ::= call <id> '(' <params> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMS :
                //<params> ::= <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMS_COMMA :
                //<params> ::= <exp> ',' <params>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMS2 :
                //<params> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"'"+"in line"+args.UnexpectedToken.Location.LineNr;
            lst.Items.Add(message);
            string m2 = "ExpectedTokens: "+ args.ExpectedTokens.ToString();
            lst.Items.Add(m2);
            //todo: Report message to UI?
        }
        private void Tokenreadevent(LALRParser parser,TokenReadEventArgs args)
        {
            string info = args.Token.Text + "\t  \t" +(SymbolConstants) args.Token.Symbol.Id;
            ls.Items.Add(info); 
        }

    }
}
