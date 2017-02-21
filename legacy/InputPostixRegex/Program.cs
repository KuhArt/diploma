using System;
using System.Collections.Generic;
//
//using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
//
using System.Drawing;
using System.Windows.Forms;

namespace InputPostfixRegex
{
   public struct ExprNameArray
    {
        public string shortName;
        public string name;
        public string[] arrPolish;
        public ExprNameArray(string shortName, string name, params  string[] arrPolish)
        {
            this.shortName = shortName;
            this.name = name;
            this.arrPolish = arrPolish;

        }

    }

    class Program
    {


        static void Main(string[] args)
        {
            if (!(args.GetLength(0) > 0))
            {
                Console.WriteLine("Error 1: No file name as argument!\n");
                Console.WriteLine("Press the Key ENTER to continue:\n");
                Console.ReadKey();
                return;
            }
            string fileNamePolish = args[0];
            int len = fileNamePolish.Length;
            if (fileNamePolish.Substring(len - 4) != ".txt")
            {
                Console.WriteLine("Error 2: No text file  as argument!\n");
                Console.WriteLine("Press the Key ENTER to continue:\n");
                Console.ReadKey();
                return;
            }
            string currDir = Directory.GetCurrentDirectory();
            Console.WriteLine("Current Directory:{0}\n", currDir);
            if (!File.Exists(fileNamePolish))
            {
                Console.WriteLine("Error 3: Not exists text file {0} as argument in current directory!\n", fileNamePolish);
                Console.WriteLine("Press the Key ENTER to continue:\n");
                Console.ReadKey();
                return;

            }
            StreamReader sr = new StreamReader(fileNamePolish);
            string shortName = sr.ReadLine();
            string name = sr.ReadLine();
            List<string> listPolish = new List<string>();
            while (!sr.EndOfStream)
            {
                string currLine=sr.ReadLine();
                listPolish.Add(currLine);
            }F
            Console.WriteLine("Polish Count :{0}\n", listPolish.Count);
            string[] arrPolish = new string[listPolish.Count];
            for (int i = 0; i < listPolish.Count; i++)
                arrPolish[i] = listPolish[i];

            //1) Demo
            //ExprNameArray namedPostfixExpID = new ExprNameArray("Id", " Identifier", "a", "a", "9", "Join", "Star", "Concat");
           // Draw_Diagram(namedPostfixExpID, bDrawPolish);

            ExprNameArray namedPostfixExpression = new ExprNameArray(shortName, name, arrPolish);
            Draw_Diagram(namedPostfixExpression, true);//true is for "Studing"

            Console.WriteLine("Press the Key ENTER to continue:\n");
            Console.ReadKey();
            return;

            ////////
            ///////////////////////////


            bool bDrawPolish = true; //Studing
            //foreach(string opt in args)
            //    Console.WriteLine(opt);
            //Console.WriteLine();

            if(args.GetLength(0)>0)
                if(args[0] == "test")
                    bDrawPolish = false;//Testing
                else
                    bDrawPolish = true;
            else
                bDrawPolish = true;

            if(bDrawPolish)
                Console.WriteLine("Studing\n");
            else
                Console.WriteLine("Testing\n");
            Console.WriteLine("Press the Key ENTER to continue:\n");
            Console.ReadKey();

            //1) Demo
             ExprNameArray namedPostfixExpID = new ExprNameArray("Id", " Identifier","a", "a", "9", "Join", "Star", "Concat"   );

            Draw_Diagram(namedPostfixExpID, bDrawPolish);

            //2) Demo
            ExprNameArray namedPostfixExpOCT = new ExprNameArray("Oct", " Octodecimal", "0", "0", "7", "Join", "Star", "Concat", "", "ul", "Join", "lu", "Join", "u", "Join", "l", "Join", "Concat");

            // //3) Input --> Resulting Diagram
            Draw_Diagram(namedPostfixExpOCT, bDrawPolish);

            ExprNameArray namedPostfixExp = new ExprNameArray();

            //Console.WriteLine("shortName:{0}", (namedPostfixExp.shortName == null));
            //Console.WriteLine("name:{0}", (namedPostfixExp.name == null));
            //Console.WriteLine("arrPolish:{0}", (namedPostfixExp.arrPolish == null));

            Console.WriteLine("Prompt 1: Enter the 'shortName' for Polish Expression : ");
            namedPostfixExp.shortName=Console.ReadLine();
            if (namedPostfixExp.shortName.Length > 7 || namedPostfixExp.shortName.Length == 0)
                namedPostfixExp.shortName = "Student";


            Console.WriteLine("\nPrompt 2: Enter the 'name' for Polish Expression : ");
            namedPostfixExp.name  = Console.ReadLine();
            if (namedPostfixExp.name.Length <= 7 )
                namedPostfixExp.name = "Student's Diagram";


            List<string> listPostfix = new List<string>();
            string tokens="";
            Console.WriteLine("\nEnter a list of  tokens delimited Space for Polish Expression.");
            Console.WriteLine("For example, Polish Expression for the identifiers is: ");
            Console.WriteLine("\na a 9 \"\" Join Join Star Concat");
            Console.WriteLine("\n\"\" is the token of the empty string. ");
            Console.WriteLine("a is the token of the class [_a-zA-Z]");
            Console.WriteLine("9 is the token of the class [0-9]");
            Console.WriteLine("Join, Concat, Star are tokens for |, *, either {} or ^");
            Console.WriteLine("\nSo Regular Expression for the identifiers can be like as:");
            Console.WriteLine("\na*{a|9|\"\"}\nor\na*{9|a}");

            Console.WriteLine("\nPrompt 3: Enter a list of  tokens delimited Space for Polish Expression:");

            tokens = Console.ReadLine();
            namedPostfixExp.arrPolish = tokens.Split(' ');

            //Output
            Console.WriteLine("\nPolish Expression:\n");
            Console.WriteLine("1)shortname:{0}", namedPostfixExp.shortName);
            Console.WriteLine("2)name:{0}", namedPostfixExp.name);
            Console.WriteLine("3)arrayPolish:");
            foreach (string s in namedPostfixExp.arrPolish)
                Console.WriteLine(s);

            Console.WriteLine("============");

            string infixExpression =PolishToInfix(new List<string> (namedPostfixExp.arrPolish));
            Console.WriteLine("\nInfix Expression:\n");
            Console.WriteLine("Length:({0})",infixExpression.Length );
            Console.WriteLine( infixExpression);
            Console.WriteLine("============");
            if (infixExpression == "")
            {
                Console.WriteLine("Invalid POLISH EXPRESSION!");
                Console.WriteLine("You get NO DIAGRAM, try again!");
                Console.ReadKey();
                return;

            }

                Console.WriteLine("THIS is a POLISH EXPRESSION!");
                Console.WriteLine("You  get its DIAGRAM !");
                Console.ReadKey();
            //===========Draw Diagramm for  ExprNameArray namedPostfixExp

                Draw_Diagram(namedPostfixExp,true );//bDrawPolish

            //======================================================
            Console.WriteLine("Press the Key Enter to Exit:");
            Console.ReadKey();
            return;


        }

        static void Draw_Diagram(ExprNameArray expression,bool bDrawPolish)
        {
            FormDiagramm formDiagramm = new FormDiagramm();
            formDiagramm.TopMost = true;

                string[] arrPolish = expression.arrPolish;

                string RegExpName = expression.name;

                List<string> polish = new List<string>(arrPolish);

            //=======Change "" for EMPTY STRING========
                for (int i = 0; i < polish.Count; i++)
                    if (polish[i] != "\"\"")
                        continue;
                    else
                        polish[i] = "";
             //========================================

#if NoTest
                Console.WriteLine("Source Polish Expression.");
                Console.WriteLine("PolishToTree: Do reduction for strings and Do join 1-strings-symbols!!!");

                foreach (string s in polish)
                    Console.WriteLine(s);
                Console.WriteLine();

                //Console.WriteLine("Press the Key Enter to Exit:");
                //Console.ReadKey();
                //return;

                //now USING
                Console.WriteLine("\n Now USING\nTree Chart attributed with size-RECTANGLES");

                //1) List<string> polish --(PolishToTree)--> TreeChart treeChart
                Console.WriteLine("Getting Reduced Polish Expression with Simple Operands untill Joined");
                Console.WriteLine("and With no Size");
#endif

                TreeChart treeChart = TreeChart.PolishToTree(polish);//No Size, No Numbers,ENUMERATION IMPOSSIBLE
                List<string> postfix = treeChart.GetPostfixForm();
#if NoTest
                Console.WriteLine("\nPostfix = treeChart.GetPostfixForm();");
                foreach (string sp in postfix)
                    Console.WriteLine(sp);
                Console.WriteLine();
                Console.ReadKey();

                //treeChart.PrintTree(); //With No Size No Numbers//Working
#endif

                //2) TreeChart treeChart --(AssignSizeToTree)--> TreeChart treeChart

                treeChart = treeChart.AssignSizeToTree(); //Get Size, But No Changing Numbers, ENUMERATION IMPOSSIBLE
#if NoTest

                //TODO:treeChart.AssignNumbersToTree();
                Console.WriteLine("\nTree Chart With Size");

                treeChart.PrintTree(); //With Size ~ //PrintTree(treeChart); No Numbers;ENUMERATION POSSIBLE
#endif

                //2) TreeChart treeChart --> string treeChart.RegExp

                //Back to Expression from TreeChart treeChart.

                treeChart.RegExp = treeChart.GetExpression().RegExp;// ~ PrintExpression(treeChart);

            if(bDrawPolish)
                treeChart.namedPostfixExp = expression;

                //TreeChart.PrintExpression(treeChart); //Working
#if NoTest

                Console.WriteLine("\ntreeChart.element:{0}", treeChart.element);
                Console.WriteLine("treeChart.RegExp:={0}", treeChart.RegExp);
#endif



                treeChart.DiagramName = "Diagram of Regular Expression:" + RegExpName;
#if NoTest


                Console.WriteLine("TreeChart.DiagramName:={0}", treeChart.DiagramName);

#endif


                // FormDiagramm formDiagramm = new FormDiagramm();

                //a) Add <<new TabPage>> for new <<expression>> in <<arrExpr>>

                TabPage currTabPage = new TabPage(expression.shortName);
                formDiagramm.tabControlDiagram.TabPages.Add(currTabPage);
                //currTabPage.Tab

                //b) Add <<treeChart>> of <<expression>> to <<listTreeChart>>
                formDiagramm.listTreeChart.Add(treeChart);


                formDiagramm.Width = treeChart.rectSize.Width + 20 + (int)(4 * TreeChart.emSizeChar);
                if (treeChart.DiagramName.Length * 14 > formDiagramm.Width)

                    formDiagramm.Width = treeChart.DiagramName.Length * 14;

                //formDiagramm.TopMost = true;

                //c)Set <<currTabPage>>
                //int indexCurrTabPage = formDiagramm.tabControlDiagram.TabPages.Count - 1;

                formDiagramm.tabControlDiagram.SelectedTab = currTabPage;

                currTabPage.Paint += new PaintEventHandler(formDiagramm.currTabPage_Paint);

            formDiagramm.ShowDialog();
            //Console.WriteLine("Press the Key Enter to Exit:");
            //Console.ReadKey();

        }

        static string PolishToInfix(List<string> polish) //Replaces GetTreeChart: any POLISH --> ready TREE, NO SIZE
        {
            //USING
            //"PolishToInfix:
            //ENUMERATION of VERTEXES is IMPOSSIBLE in VIEW of OPTIMIZATION 2,3
            //1)do <<reduction of strings for "Concat">> and
            //2)do <<join> 1-strings-symbols for "Join"!!!");
            //3)do <<star>> <<star>> as <<star>>
            string resExpression = null;

            string arg1 = null;
            string arg2 = null;

            Stack<string> stack = new Stack<string>();
            List<string> listSimpleConcat = null;

            //string joinS1S2 = "";

            foreach (string s in polish)
            {
                if (s.Length == 1)
                {
                    if (s == "\"")
                    {
                        resExpression = "";
                        break;

                    }

                    resExpression =  s;
                    stack.Push(resExpression);
                }
                else if (s.Length == 0)
                {
                    //resExpression = new string("\"", Size.Empty, null);
                    resExpression = "";
                    stack.Push(resExpression);
                }
                else switch (s)
                    {////s.Length > 1

                        case "Star": //(startPpoint,endPoint) in the middle
                            //TODO OPTIMIZATION arg=stack.Pop() if arg.element =="Star" then  arg STAR --> arg
                            if (stack.Count == 0)
                            {
                                resExpression = "";
                                break;
                            }

                            arg1 = stack.Pop();

                            resExpression =  "{" + arg1 + "}" ;//NO OPTIMIZATION
                            //if (arg1.element == "Star")

                            //    resExpression = arg1;

                            //else
                            //    resExpression = new string("Star", Size.Empty, new string[] { arg1 });//stack.Pop()

                            stack.Push(resExpression);

                            break;
                        case "Concat"://(startPpoint,endPoint) in the middle
                            if (stack.Count == 0)
                            {
                                resExpression = "";
                                break;
                            }
                            arg2 = stack.Pop();
                            if (stack.Count == 0)
                            {
                                resExpression = "";
                                break;
                            }
                            arg1 = stack.Pop();

                            //NEVER OPTIMIZE
                            resExpression =   arg1 + "*" + arg2  ;
                            //resExpression = new string("Concat", Size.Empty, new string[] { arg1, arg2 });//childs
                            stack.Push(resExpression);

                            break;
                        case "Join"://(startPpoint,endPoint) in the middle
                            if (stack.Count == 0)
                            {
                                resExpression = "";
                                break;
                            }
                            arg2 = stack.Pop();
                            if (stack.Count == 0)
                            {
                                resExpression = "";
                                break;
                            }
                            arg1 = stack.Pop();

                            resExpression =  arg1 + "|" + arg2 ; //NO OPTIMIZATION
                            stack.Push(resExpression);
                            ////DONE: JoinSimpleOperand-OPTIMIZATION
                            ////if( arg1 is <JoinSimpleOperand> && arg2 is <JoinSimpleOperand>) then arg1|arg2 is OPERAND, so
                            ////arg1,arg2 --> arg1|arg2
                            //if (!IsJoinSimpleOperand(arg1) || !IsJoinSimpleOperand(arg2))
                            //{


                            //    resExpression = new string("Join", Size.Empty, new string[] { arg1, arg2 });//childs
                            //    stack.Push(resExpression);
                            //}
                            //else//(IsJoinSimpleOperand(arg1) && IsJoinSimpleOperand(arg2)) ; JoinSimpleOperand-OPTIMIZATION DONE
                            //{//Boath Values <<rg1.element>> and <<arg2.element>> are strings in a form of
                            //    // "a" or "a0|a1|...|an" where ai is SYMBOL (ai.Length < 1)and ai != aj if i != j
                            //    joinS1S2 = JoinSimpleOperand(arg1.element, arg2.element);
                            //    resExpression = new string(joinS1S2, Size.Empty, null);//resExpression replaces  two trees arg1 and arg2
                            //    stack.Push(resExpression);
                            //}


                            break;
                        default:
                            //s.Length > 1, s is an string-concatenation
                            //TODO: REDUCE
                            if (s == "\"\"")
                            {
                                resExpression = "\"\"";
                                stack.Push(resExpression);
                                break;
                            }

                            listSimpleConcat = StringToConcat(s);
                            foreach (string symbol in listSimpleConcat)
                            {
                                if (symbol.Length == 1)
                                {
                                    resExpression =   symbol  ;
                                    //resExpression = new string(symbol, Size.Empty, null);
                                    stack.Push(resExpression);
                                }
                                else if (symbol.Length == 0)
                                {
                                    resExpression =   "\"\""  ;
                                    //resExpression = new string("\"", Size.Empty, null);
                                    stack.Push(resExpression);
                                }
                                else //symbol == "Concat"
                                {if (stack.Count == 0)
                            {
                                resExpression = "";
                                break;
                            }
                                    arg2 = stack.Pop();
                                    arg1 = stack.Pop();
                                    resExpression =   arg1 + "*" + arg2  ;
                                    //resExpression = new string("Concat", Size.Empty, new string[] { arg1, arg2 });//childs
                                    stack.Push(resExpression);

                                }
                            }
                            break;
                    }
            }
            if (stack.Count == 1)
                return resExpression;
            else
                return "";
        }

        static List <string> StringToConcat(string expr)
        {
            List<string> res = new List<string>();
            if (expr.Length > 1)
            {

                res.Add(expr.Substring(0, 1));
                for (int i = 1; i < expr.Length; i++)
                {
                    res.Add(expr.Substring(i, 1));
                    res.Add("Concat");
                }
                return res;
            };
            return null;
        }

    }
}
