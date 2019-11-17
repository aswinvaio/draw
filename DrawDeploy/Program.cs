using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawDeploy
{
    class Program
    {
        static void Main(string[] args)
        {
            AppTypes type = AppTypes.Unknown;
            Operation operation = Operation.Deploy;

            if (args.Length <= 0) return;

            try
            {
                switch (args[0].ToLower())
                {
                    case "all":
                        type = AppTypes.All;
                        break;
                    case "ng":
                    case "angular":
                        type = AppTypes.Ng;
                        break;
                    case "java":
                        type = AppTypes.Java;
                        break;
                }

                switch (args[1].ToLower())
                {
                    case "deploy":
                    case "destroy":
                        operation = Operation.Deploy;
                        break;
                    case "rollback":
                    case "fix":
                        operation = Operation.Rollback;
                        break;
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Invalid options");
                return;
            }

            if(type == AppTypes.All || type == AppTypes.Ng)
            {
                if (operation == Operation.Deploy)
                    Angular.Do();
                else
                    Angular.Undo();
            }
            if (type == AppTypes.All || type == AppTypes.Java)
            {
                if (operation == Operation.Deploy)
                    Java.Do();
                else
                    Java.Undo();
            }
            Console.WriteLine("done");
        }
    }
}
