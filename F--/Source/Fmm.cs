using FMM.Frontend.Tokens;
using FMM.Frontend.Lexer;
using FMM.Frontend.Parser;
using FMM.Runtime;
using FMM.Imports;
using FMM.Helper;

namespace FMM
{
    class FMM
    {
        public static void Main()
        {
            PrintTitle title = new PrintTitle();

            Parser parser = new Parser();
            Interpreter interpreter = new Interpreter();

            Enviorment env = new Enviorment(null);
            env.DeclareVar("true", new BoolVal(true), true);
            env.DeclareVar("false", new BoolVal(false), true);
            env.DeclareVar("null", new NullVal(), true);

            while (true) {
                Console.Write(">>> ");
                String input = Console.ReadLine();

                if (input == "exit") {
                    Environment.Exit(0);
                }

                var program = parser.produceAST(input);

                RuntimeVal result = interpreter.Evaluate(program, env);
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
                Console.WriteLine(json);
            }
        }
    }
}