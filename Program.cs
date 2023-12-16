using custom_compiler_tutorial.BindingStage;
using custom_compiler_tutorial.LexerStage;
using custom_compiler_tutorial.ParserStage;
using custom_compiler_tutorial.SyntaxTreeStage;

namespace custom_compiler_tutorial
{
    public static class Program
    {
        private static void Main()
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
                Binder binder = new();
                BoundExpression boundExpression = binder.BindExpression(syntaxTree.Root);

                IReadOnlyList<string> diagnostics = syntaxTree.Diagnostics.Concat(binder.Diagnostics).ToArray();

                if (showTree)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ResetColor();
                }

                if (diagnostics.Any())
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    foreach (string diagnostic in diagnostics) Console.WriteLine(diagnostic);
                    Console.ResetColor();
                }
                else
                {
                    object result = new Evaluator(boundExpression).Evaluate();
                    Console.WriteLine(result);
                }
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
            if (node is SyntaxToken t && t.Value != null)
            {
                Console.Write(" ");
                Console.Write(t.Value);
            }
            Console.WriteLine();

            SyntaxNode? lastChild = node.GetChildren().LastOrDefault();
            foreach (SyntaxNode child in node.GetChildren()) PrettyPrint(child, indent + (isLast ? "    " : "│   "), child == lastChild);
        }
    }
}