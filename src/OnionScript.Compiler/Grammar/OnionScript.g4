grammar OnionScript;

@lexer::header {
using System.Collections.Generic;
}

@lexer::members {
private Stack<bool> skipWhitespace = new Stack<bool>();
}

// Parser Rules

script : import_statement* statement* EOF;

statement : methodcall
    | function_definition
    | function_return
    | variable_definition
    | anonymous_function
    ;

methodcall : STATEMENT_BEGIN method_operation STATEMENT_END;
method_operation : identifier (WHITESPACE argument_any)* # OperatorCallMethod
    | operator_set WHITESPACE identifier WHITESPACE argument_any #OperatorSet
    | operator_add WHITESPACE argument_number WHITESPACE argument_number # OperatorAdd
    | operator_subtract WHITESPACE argument_number WHITESPACE argument_number # OperatorSubtract
    | operator_multiply WHITESPACE argument_number WHITESPACE argument_number # OperatorMultiply
    | operator_divide WHITESPACE argument_number WHITESPACE argument_number # OperatorDivide
    | operator_modulus WHITESPACE argument_number WHITESPACE argument_number # OperatorModulus
    | operator_not WHITESPACE argument_boolean # OperatorNot
    | operator_and WHITESPACE argument_boolean WHITESPACE argument_boolean # OperatorAnd
    | operator_or WHITESPACE argument_boolean WHITESPACE argument_boolean # OperatorOr
    | operator_nor WHITESPACE argument_boolean WHITESPACE argument_boolean # OperatorNor
    | operator_nand WHITESPACE argument_boolean WHITESPACE argument_boolean # OperatorNand
    | operator_equal WHITESPACE argument_any WHITESPACE argument_any #OperatorEquals
    | operator_notequal WHITESPACE argument_any WHITESPACE argument_any # OperatorNotEquals
    | operator_greaterthan WHITESPACE argument_number WHITESPACE argument_number #OperatorGreaterThan
    | operator_greaterthanequal WHITESPACE argument_number WHITESPACE argument_number #OperatorGreaterThanEqual
    | operator_lessthan WHITESPACE argument_number WHITESPACE argument_number #OperatorLessThan
    | operator_lessthanequal WHITESPACE argument_number WHITESPACE argument_number #OperatorLessThanEqual
    | operator_for WHITESPACE identifier WHITESPACE argument_number WHITESPACE argument_boolean WHITESPACE anonymous_function WHITESPACE (anonymous_function | methodcall) # OperatorFor
    | operator_while WHITESPACE argument_boolean WHITESPACE (anonymous_function | methodcall) # OperatorWhile
    | operator_dowhile WHITESPACE argument_boolean WHITESPACE (anonymous_function | methodcall) # OperatorDoWhile
    ;

operator_import : 'import';
operator_function : 'func';
operator_let : 'let';
operator_return : 'return';

operator_for : 'for';
operator_while : 'while';
operator_dowhile : 'dowhile';

operator_set : '=';
operator_add : '+';
operator_subtract : '-';
operator_multiply : '*';
operator_divide : '/';
operator_modulus : '%';
operator_not : 'not';
operator_and : 'and';
operator_or : 'or';
operator_nor : 'nor';
operator_nand : 'nand';
operator_equal : '==';
operator_notequal : '!=';
operator_greaterthan : '>';
operator_greaterthanequal : '>=';
operator_lessthan : '<';
operator_lessthanequal : '<=';

import_statement : STATEMENT_BEGIN operator_import WHITESPACE text STATEMENT_END;

anonymous_function : STATEMENT_BEGIN_IGNORE_WHITESPACE statement* STATEMENT_END_IGNORE_WHITESPACE;

function_definition : STATEMENT_BEGIN
    operator_function WHITESPACE identifier (WHITESPACE identifier)* WHITESPACE (anonymous_function)
    STATEMENT_END
    ;

function_return :STATEMENT_BEGIN
    operator_return WHITESPACE argument_any
    STATEMENT_END
    ;


variable_definition : STATEMENT_BEGIN
    operator_let WHITESPACE identifier WHITESPACE argument_any
    STATEMENT_END
    ;


text : TEXT;
integer : INTEGER;
float : FLOAT;
boolean : BOOLEAN;
identifier : IDENTIFIER;

argument_text : text | identifier | methodcall | anonymous_function;
argument_integer : integer | identifier | methodcall | anonymous_function;
argument_float : float | identifier | methodcall | anonymous_function;
argument_number : argument_integer | argument_float;
argument_boolean : boolean | identifier | methodcall | anonymous_function;
argument_statement : statement | identifier;
argument_any : argument_text
    | argument_number
    | argument_boolean
    ;



// Lexer rules

fragment UPPERCASE : [A-Z];
fragment LOWERCASE : [a-z];
fragment DIGIT : [0-9];
fragment HEXDIGIT : ([0-9] | [a-f] | [A-F]);

fragment ALPHACHAR : (UPPERCASE | LOWERCASE | '_');
fragment ALPHANUMCHAR : (UPPERCASE | LOWERCASE | '_' | DIGIT);
fragment NEWLINE : ('\r\n' | '\n');

WHITESPACE : (NEWLINE | ' ' | '\t')+ { if (skipWhitespace.Count == 0 || skipWhitespace.Peek()) Skip(); };

STATEMENT_BEGIN : '(' { skipWhitespace.Push(false); };
STATEMENT_END : ')' { try { skipWhitespace.Pop(); } catch (InvalidOperationException) { } };
STATEMENT_BEGIN_IGNORE_WHITESPACE : '(' WHITESPACE* { skipWhitespace.Push(true); };
STATEMENT_END_IGNORE_WHITESPACE : WHITESPACE* ')' { try { skipWhitespace.Pop(); } catch (InvalidOperationException) { } };

TEXT : '"' ('\\"' | .)*? '"';
BOOLEAN : 'true' | 'false';
FLOAT : ('-')? (DIGIT+)? '.' (DIGIT+)?;
INTEGER : ('-')? (DIGIT+|'0' ('x'|'X') HEXDIGIT+);
IDENTIFIER : ALPHACHAR (ALPHANUMCHAR+)?;
COMMENT : '#' .*? NEWLINE+ -> skip;

ENDL : NEWLINE+;