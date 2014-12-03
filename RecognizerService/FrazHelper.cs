using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizerService
{
    public static class FrazHelper
    {
        public static string GetFraz(string input)
        {
            string result = input;
            switch (input)
            {
                case "sqrt":
                    break;
                case "_plus":
                    result = "+";
                    break;
                case "_equal":
                    result = "=";
                    break;
                case "lparen":
                    result = "{";
                    break;
                case "rparen":
                    result = "{";
                    break;
                default:
                    break;
            }


            return result;
        }
    }


    public static class MathHelper
    {
        public static bool IsOperator(string input)
        {

            bool result = false;
            switch (input)
            {
                case "sqrt":
                case "_plus":
                case "equal":
                case "lpain":
                    result = true;
                    break;
                default:

                    break;
            }

            return result;
        }

        public static string GetOperator(string input)
        {
            string result = input;
            switch (input)
            {
                case "sqrt":
                    break;
                case "_plus":
                    result = "+";
                    break;
                case "_equal":
                    result = "=";
                    break;
                case "lparen":
                    result = "{";
                    break;
                case "rparen":
                    result = "{";
                    break;
                default:
                    break;
            }


            return result;
        }
    }
}
