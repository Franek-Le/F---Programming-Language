namespace FMM.Helper
{
    class PrintTitle
    {
        public PrintTitle() {
            string text = "    ███████╗\n" +
                      "    ██╔════╝\n" +
                      "    █████╗█████╗█████╗\n" +
                      "    ██╔══╝╚════╝╚════╝\n" +
                      "    ██║               \n" +
                      "    ╚═╝               \n" +
                      "                   ";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(text);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("    ██╗   ██╗     ██████╗    ██╗                                        ");
            Console.WriteLine("    ██║   ██║    ██╔═████╗  ███║                                        ");
            Console.WriteLine("    ██║   ██║    ██║██╔██║  ╚██║                                        ");
            Console.WriteLine("    ╚██╗ ██╔╝    ████╔╝██║   ██║                                        ");
            Console.WriteLine("     ╚████╔╝     ╚██████╔╝██╗██║                                        ");
            Console.WriteLine("      ╚═══╝       ╚═════╝ ╚═╝╚═╝                                        ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nF-- Programming Language (Type exit to exit)\n");
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}
