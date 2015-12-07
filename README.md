# parsing-demos

01-recursive-descent
    Demonstrates basic recursive descent technique on an example of a fully parenthesized arithmetic expression with four arithmetic operations.
    Comes with generator which simply generates text and with interpreter.

02-lexical-analyzers
    Demonstrates techniques to perform lexical analysis.
    
    RegexLexicalAnalyzer is a configurable lexer, but not that efficient. It lets the caller add patterns that are producing tokens.
        Each pattern is based on a regular expression which is then matched against current position in the input character stream.
	This solution exhibits two major problems. First, regular expressions are prefixed by ^, indicating beginning of the string.
	This leads to the requirement to remove recognized tokens from the input string, or otherwise Regex wouldn't see the string beginning
	and none of the regular expressions would ever match anything after the first token has been recognized. This makes input
	processing O(n^2). On the other hand, for every input position, all regular expressions are taken into account.
	Regex matching then comes with O(k*n) time complexity, where k is number of regular expressions and n is the input size.
	Total complexity of this lexical analysis method is O((k+n)*n), which is not acceptable. The worst acceptable complexity 
	would be O(k*n), which would then be linear in input size, since k is effectively a constant, coming out from the language description.
	There is one interesting idea in this solution. TokenPatten class, which describes a regular expression for one token type,
	is an abstract class and it is implemented in two variants - invisible token pattern and visible token pattern.
	This concept recognized that some parts of the input, such as white space, are legal but produce no token in the output stream.
	Therefore, lexical analyzer setup will contain description for white space, but no white space token will ever appear in the output.
	
03-text-input
    Demonstrates techniques to input text before lexical analysis.
    
    ITextInput interface is supposed to be implemented by classes that will be supplied to lexical analyzers. There are two basic operations:
        LookAhead and Advance. LookAhead is meant to return a sequence of characters that are following, starting from current position 
	in the input. This lets the lexical analyzer consume as many input characters as needed before making the decision which 
	token has been recognized. During that process, ITextInput object will not advance through input. Once the token has been
	recognized, lexical analyzer will call Advance method to effectively advance the input by specified number of characters.
	
    StringInput is the concrete implementation of ITextInput which receives a string through its constructor. Object then iterates through
        characters of the string. This is the general-purpose implementation and it doesn't have much practical use. It is meant to be used
	by more specific implementations, such as console input or file input.
	
    ConsoleInput is the abstract implementation based on console input/output. It wraps StringInput and feeds it with content read from the console.
        This is the abstract class and its ReadInput method is supposed to be implemented by derived classes. There are two concrete derived classes:
	ConsoleLineInput (which reads a single line) and ConsoleBlockInput (which reads multiline content).