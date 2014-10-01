using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai
{
    public class Ai_8Puzzle
    {

        static int[] puzzleGoal = new int[] { 1,2,3,4,5,6,7,8,0 };


        static void BFS(int[] state,out string result)
        {
            result = "CAN NOT FIND THE WAY...";
            Debug.WriteLine("START");
            Queue<int[]> queue = new Queue<int[]>();
            queue.Enqueue(state);

            Dictionary<int[], string> step = new Dictionary<int[], string>();

            HashSet<string> discovered = new HashSet<string>();

            while (queue.Count > 0)
            {
                int[] currentState = queue.Dequeue();

                bool checkResult = CheckResult(currentState);
                if (checkResult)
                {
                    Debug.WriteLine("TIM XONG DUONG DI");

                    string value;
                    step.TryGetValue(currentState, out value);
                    result = value;
                    Debug.WriteLine(value);
                    break;
                }
                else
                {
                    string currentSta = GetString(currentState);
                    if (discovered.Contains(currentSta))
                    {
                        continue;
                    }

                    discovered.Add(currentSta);

                    List<string> move_description;

                    var listMove = GetListMoveAvailable(currentState, out move_description);
                    for (int i = 0; i < listMove.Count; i++)
                    {
                        int[] data = listMove.ElementAt(i);
                        queue.Enqueue(data);

                        string value;
                        if (!step.TryGetValue(currentState, out value))
                        {
                            value = string.Empty;
                        }

                        step.Add(data, value + move_description.ElementAt(i) + "->");
                    }
                }
            }
            Debug.WriteLine("FINISH BFS");
        }

        public static List<int[]> GetListMoveAvailable(int[] currentState, out List<string> move_description)
        {
            List<int[]> listNextState = new List<int[]>();
            move_description = new List<string>();
            int[] movedown;
            if (TryMoveDown(currentState, out movedown))
            {
                listNextState.Add(movedown);
                move_description.Add("DOWN");
            }


            int[] moveup;
            if (TryMoveUp(currentState, out moveup))
            {
                listNextState.Add(moveup);
                move_description.Add("UP");
            }

            int[] moveleft;
            if (TryMoveLeft(currentState, out moveleft))
            {
                listNextState.Add(moveleft);
                move_description.Add("LEFT");
            }

            int[] moveright;
            if (TryMoveRight(currentState, out moveright))
            {
                listNextState.Add(moveright);
                move_description.Add("RIGHT");
            }

            return listNextState;
        }

        static bool CheckResult(int[] state)
        {

            for (int i = 0; i < puzzleGoal.Length; i++)
            {
                if (state[i] != puzzleGoal[i]) return false;
            }

            return true;
        }

        private static string GetString(int[] s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                sb.Append(s[i]);
            }
            
            return sb.ToString();   
         
        }

        public static void GetPosition(int[] state,out int index,out int x, out int y) {
            x = 0;
            y = 0;
            index = 0;
            for (int i = 0; i < state.Length; i++)
            {
                if (state[i] == 0)
                {
                    index = i;
                    x = i % 3;
                    y = i / 3;
                    return;
                }
            }
        }

        public static bool TryMoveDown(int[] state, out  int[] newResult)
        {
            newResult = null;
            int index, x, y;
            GetPosition(state, out index, out x, out y);
            if (y == 0)
            {
                return false;
            }

            int invertIndex = GetIndexFromXY(x, y - 1);

            newResult = SwapArray(state, index, invertIndex);

            return true;
        }

        public static bool TryMoveUp(int[] state, out  int[] newResult)
        {
            newResult = null;
            int index, x, y;
            GetPosition(state, out index, out x, out y);
            if (y >=2)
            {
                return false;
            }

            int invertIndex = GetIndexFromXY(x, y + 1);

            newResult = SwapArray(state, index, invertIndex);

            return true;
        }
     
        public static bool TryMoveRight(int[] state, out  int[] newResult)
        {
            newResult = null;
            int index, x, y;
            
            GetPosition(state, out index, out x, out y);           
            if (x <=0)
            {
                return false;
            }

            int invertIndex = GetIndexFromXY(x-1, y );

            newResult = SwapArray(state, index, invertIndex);

            return true;
        }

        public static bool TryMoveLeft(int[] state, out  int[] newResult)
        {
            newResult = null;
            int index, x, y;

            GetPosition(state, out index, out x, out y);            
            if (x >=2)
            {
                return false;
            }

            int invertIndex = GetIndexFromXY(x + 1, y);

            newResult = SwapArray(state, index, invertIndex);

            return true;
        }


        public static int GetIndexFromXY(int x, int y) {
          
            return y* 3 + x;
        }

        private static int[] SwapArray(int[] state, int index, int wrap)
        {
            int[] newResult;
            newResult = new int[state.Length];
            for (int i = 0; i < state.Length; i++)
            {
                if (i == index)
                {
                    newResult[i] = state[wrap];
                    continue;
                }
                if (i == wrap)
                {
                    newResult[i] = state[index];
                    continue;
                }

                newResult[i] = state[i];
            }
            return newResult;
        }


        public static string Search(int[] p)
        {
            string v;
            BFS(p, out v);
            return v;
        }
    }
}
