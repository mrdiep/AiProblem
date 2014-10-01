using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ai;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathGo = Ai_8Puzzle.Search(new int[] 
                    {                                          
                        1,2,3,
                        7,0,8,
                        4,5,6                       
                       
                     }
            );

            //List<string> v;
            //var c  = Ai_8Puzzle.GetListMoveAvailable(new int[] {
            //    0,1,3,
            //    4,2,5,
            //    6,7,8

            //}, out v);

            //foreach (string e in v)
            //{
            //    Debug.WriteLine(e);
            //}
           Debug.WriteLine(pathGo);
        }
    }
}
