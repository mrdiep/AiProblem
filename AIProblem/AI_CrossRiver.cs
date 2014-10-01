using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai
{
    public class AI_CrossRiver
    {
        static readonly int WEST = 1;
        static readonly int EAST = 0;

        public AI_CrossRiver()
        {            
            BFS(new int[] { 3, 3, 0, 0, WEST });
        }
        
        static void BFS(int[] state)
        {
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

                        string value ;
                        if (!step.TryGetValue(currentState, out value)) {
                            value = string.Empty;
                        }

                        step.Add(data, value + move_description.ElementAt(i) + ">>");
                    }
                }
            }
            Debug.WriteLine("DONE");
        }

        private static string GetString(int[] s)
        {
            return string.Empty + s[0] + s[1] + s[2] + s[3] + s[4];
        }


        static bool CheckResult(int[] state)
        {       
            //neu tat ca qua xong thi ok
            if (state[2] == 3 && state[3] == 3 ) 
                return true;
            

            return false;
        }

        static bool IsValid(int[] state)
        {
            //nha truyen giao it hon 3 con quy
            if (state[0] < 0) return false;
            if (state[1] < 0) return false;
            if (state[2] < 0) return false;
            if (state[3] < 0) return false;

            if (state[0] > 3) return false;
            if (state[1] > 3) return false;
            if (state[2] > 3) return false;
            if (state[3] > 3) return false;

            if (state[0] + state[2] > 3) return false;
            if (state[1] + state[3] > 3) return false;

            if (state[0] !=0 && state[0] < state[1]) return false;
            if (state[2] != 0 && state[2] < state[3]) return false;

            return true;
        }

        static List<int[]> GetListMoveAvailable(int[] state, out List<string> move_description)
        {
            List<int[]> list = new List<int[]>();
            move_description = new List<string>();

            int[] move_c;
            if (CanC( state, out move_c))
            {
                list.Add(move_c);
                move_description.Add("MOVE C");
            }

            int[] move_m;
            if (CanM( state, out move_m))
            {
                list.Add(move_m);
                move_description.Add("MOVE M");
            }
            int[] move_cc;
            if (CanCC( state, out move_cc))
            {
                list.Add(move_cc);
                move_description.Add("MOVE C + C");
            }

            int[] move_mm;
            if (CanMM( state, out move_mm))
            {
                list.Add(move_mm);
                move_description.Add("MOVE M+M");
            }

            int[] move_mc;
            if (CanMC( state, out move_mc))
            {
                list.Add(move_mc);
                move_description.Add("MOVE M+C");
            }

            return list;
        }

        #region Check Can Move
        //kiem tra nguoi vs quy qua duoc khong
        static bool CanMC(int[] state, out  int[] newResult)
        {
            if (state[4] == WEST)
            {
                int[] newState = new int[] { state[0] - 1, state[1] - 1, state[2] + 1, state[3] + 1, EAST };
                newResult = newState;
                return IsValid(newState);
            }
            else
            {
                int[] newState = new int[] { state[0] + 1, state[1] + 1, state[2] - 1, state[3] - 1, WEST };
                newResult = newState;
                return IsValid(newState);
            }
        }

        //kiem tra 2 nguoi qua duoc khong
        static bool CanMM(int[] state, out  int[] newResult)
        {
            if (state[4] == WEST)
            {
                int[] newState = new int[] { state[0] - 2, state[1], state[2] + 2, state[3], EAST };
                newResult = newState;
                return IsValid(newState);
            }
            else
            {
                int[] newState = new int[] { state[0] + 2, state[1], state[2] - 2, state[3], WEST };
                newResult = newState;
                return IsValid(newState);
            }
        }

        //kiem tra 2 quy qua duoc khong
        static bool CanCC(int[] state, out  int[] newResult)
        {
            if (state[4] == WEST)
            {
                int[] newState = new int[] { state[0], state[1] - 2, state[2], state[3] + 2, EAST };
                newResult = newState;
                return IsValid(newState);
            }
            else
            {
                int[] newState = new int[] { state[0], state[1] + 2, state[2], state[3] - 2, WEST };
                newResult = newState;
                return IsValid(newState);
            }
        }

        //kiem tra 1 quy qua duoc khong
        static bool CanC(int[] state, out  int[] newResult)
        {
            if (state[4] == WEST)
            {
                int[] newState = new int[] { state[0], state[1] - 1, state[2], state[3] + 1, EAST };
                newResult = newState;
                return IsValid(newState);
            }
            else
            {
                int[] newState = new int[] { state[0], state[1] + 1, state[2], state[3] - 1, WEST };
                newResult = newState;
                return IsValid(newState);
            }
        }

        //kiem tra 1 nguoi qua duoc khong
        static bool CanM(int[] state, out  int[] newResult)
        {
            if (state[4] == WEST)
            {
                int[] newState = new int[] { state[0] - 1, state[1], state[2] + 1, state[3], EAST };
                newResult = newState;
                return IsValid(newState);
            }
            else
            {
                int[] newState = new int[] { state[0] + 1, state[1], state[2] - 1, state[3], WEST };
                newResult = newState;
                return IsValid(newState);
            }
        } 
        #endregion

    }
}
