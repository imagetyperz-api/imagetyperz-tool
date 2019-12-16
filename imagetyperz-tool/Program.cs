using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace imagetyperz_tool
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                new CommandLineController(args).run();      // run command-line controller
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error occured: {0}", ex.Message));
            }
            finally
            {
                // disabled for command-line mode
                //Console.WriteLine("FINISHED ! Press ENTER to close window ...");
                //Console.ReadLine();
            }
        }
    }
}
