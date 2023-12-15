using custom_compiler_tutorial.LexerStage;
using custom_compiler_tutorial.ParserStage;
using custom_compiler_tutorial.SyntaxTreeStage;

namespace custom_compiler_tutorial
{
    public class Program
    {
        static void Main(string[] args)
        {
            bool showTree = false;
            while (true)
            {
                Console.Write("> ");
                string? line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) return;

                if (line == "#showTree")
                {
                    showTree = !showTree;
                    Console.WriteLine(showTree ? "Showing parse trees" : "Not showing parse trees");
                    continue;
                }
                else if (line == "#cls")
                {
                    Console.Clear();
                    continue;
                }

                SyntaxTree syntaxTree = SyntaxTree.Parse(line);

                ConsoleColor color = Console.ForegroundColor;
                if (showTree)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ForegroundColor = color;
                }

                if (syntaxTree.Diagnostics.Any())
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    foreach (string diagnostic in syntaxTree.Diagnostics) Console.WriteLine(diagnostic);
                    Console.ForegroundColor = color;
                }
                else
                {
                    int result = new Evaluator(syntaxTree.Root).Evaluate();
                    Console.WriteLine(result);
                }

                // Shows what the lexer outputs
                /*Lexer lexer = new(line);
                while (true)
                {
                    SyntaxToken token = lexer.NextToken();
                    if (token.Kind == SyntaxKind.EndOfFileToken) break;
                    Console.Write($"{token.Kind}: '{token.text}'");
                    if (token.value != null) Console.Write($" {token.value}");
                    Console.WriteLine();
                }*/
            }
        }

        static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
        {
            // Copy and paste characters for the tree
            // ├──
            // │  
            // └──
            Console.Write(indent);
            Console.Write(isLast ? "└── " : "├── ");
            Console.Write(node.Kind);
            if (node is SyntaxToken t && t.value != null)
            {
                Console.Write(" ");
                Console.Write(t.value);
            }
            Console.WriteLine();

            SyntaxNode? lastChild = node.GetChildren().LastOrDefault();
            foreach (SyntaxNode child in node.GetChildren()) PrettyPrint(child, indent + (isLast ? "    " : "│   "), child == lastChild);
        }
    }
}