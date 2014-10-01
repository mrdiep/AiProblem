using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai
{
     
    /// <summary>
    /// 
    /// Bạn hãy giúp bố, mẹ, 2 con trai, 2 con gái, cảnh sát và tên cướp qua sông. 
    /// Nếu không có cảnh sát, tên cướp sẽ đánh mọi người. 
    /// Nếu không bố, con trai sẽ quấy mẹ. 
    /// Nếu không có mẹ, con gái sẽ quấy bố. 
    ///Thuyền chỉ chở tối đa 2 người. 
    ///Chỉ có bố, mẹ và cảnh sát mới lái được thuyền. Nhấp chuột vào thuyền để qua sông.
    /// </summary>
    public class AI_8Person
    {
        static readonly int WEST = 1;
        static readonly int EAST = 0;

        static readonly int WIFE_EAST = 1;
        static readonly int HUSBAND_EAST = 2;
        static readonly int GIRL_EAST = 3;
        static readonly int BOY_EAST = 4;
        static readonly int ROBBER_EAST = 5;
        static readonly int POLICE_EAST = 6;

        static readonly int WIFE_WEST = 7;
        static readonly int HUSBAND_WEST = 8;
        static readonly int GIRL_WEST = 9;
        static readonly int BOY_WEST = 10;
        static readonly int ROBBER_WEST = 11;
        static readonly int POLICE_WEST = 12;


        public AI_8Person()
        {
            //thu tu: vo,chong, 2 congai,2 con trai,  canh sat,cuop
            BFS(new int[] { EAST, 1,1,2,2,1,1, 0,0,0,0,0,0 });
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

        private static List<int[]> GetListMoveAvailable(int[] state, out List<string> move_description)
        {
            List<int[]> list = new List<int[]>();
            move_description = new List<string>();

            int[] move_hw;
            if (CanHW(state, out move_hw))
            {
                list.Add(move_hw);
                move_description.Add("MOVE HUSBAND + WIFE");
            }

            int[] move_h;
            if (CanH(state, out move_h))
            {
                list.Add(move_h);
                move_description.Add("MOVE HUSBAND");
            }


            int[] move_hb;
            if (CanHB(state, out move_hb))
            {
                list.Add(move_hb);
                move_description.Add("MOVE HUSBAND + 1 BOY");
            }
            int[] move_hg;
            if (CanHG(state, out move_hg))
            {
                list.Add(move_hg);
                move_description.Add("MOVE HUSBAND + 1 GIRL");
            }

            int[] move_w;
            if (CanW(state, out move_w))
            {
                list.Add(move_w);
                move_description.Add("MOVE WIFE");
            }

            int[] move_wb;
            if (CanWB(state, out move_wb))
            {
                list.Add(move_wb);
                move_description.Add("MOVE WIFE + 1 BOY");
            }
            int[] move_wg;
            if (CanWG(state, out move_wg))
            {
                list.Add(move_wg);
                move_description.Add("MOVE WIFE + 1 GIRL");
            }


            int[] move_p;
            if (CanP(state, out move_p))
            {
                list.Add(move_p);
                move_description.Add("MOVE POLICE");
            }

            int[] move_pr;
            if (CanPR(state, out move_pr))
            {
                list.Add(move_pr);
                move_description.Add("MOVE POLICE + ROBBER");
            }

            int[] move_ph;
            if (CanPH(state, out move_ph))
            {
                list.Add(move_ph);
                move_description.Add("MOVE POLICE + HUSBAND");
            }

            int[] move_pw;
            if (CanPW(state, out move_pw))
            {
                list.Add(move_pw);
                move_description.Add("MOVE POLICE + WIFE");
            }

            int[] move_pb;
            if (CanPB(state, out move_pb))
            {
                list.Add(move_pb);
                move_description.Add("MOVE POLICE + 1 BOY");
            }

            int[] move_pg;
            if (CanPG(state, out move_pg))
            {
                list.Add(move_pg);
                move_description.Add("MOVE POLICE + 1 GIRL");
            }

            return list;
        }

        private static string GetString(int[] s)
        {
            return string.Empty + s[0] + s[1] + s[2] + s[3] + s[4] + s[5] + s[6] + s[7] + s[8] + s[9] + s[10] + s[11] + s[12];
        }


        static readonly int[] winState = new int[] { 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 1, 1 };
       static bool CheckResult(int[] state)
       {

           for (int i = 1; i <= 12; i++)
           {
               if (state[i] != winState[i])
               {
                   return false;
               }
           }

           return true;
       }

        static bool IsValid(int[] state)
        {
            for (int i = 1; i <= 12; i++) {
                if (state[i] < 0) return false;
            }

            #region Cac ben vuot qua so nguoi quy dinh
            if (state[1] > 1) return false;
            if (state[2] > 1) return false;
            if (state[3] > 2) return false;
            if (state[4] > 2) return false;
            if (state[5] > 1) return false;
            if (state[6] > 1) return false;

            if (state[7] > 1) return false;
            if (state[8] > 1) return false;
            if (state[9] > 2) return false;
            if (state[10] > 2) return false;
            if (state[11] > 1) return false;
            if (state[12] > 1) return false; 
            #endregion

            #region Dieu kien cuop
            if (state[ROBBER_EAST] == 1)
            {
                //co cuop ben nay
                if (state[HUSBAND_EAST] ==1 || state[WIFE_EAST] ==1 || state[BOY_EAST] > 0 || state[GIRL_EAST] > 0)
                {
                    if (state[POLICE_EAST] == 0)
                    {
                        //neu co cuop + khong co canh sat+ co nguoi
                        return false;
                    }
                }
            }
            else if (state[ROBBER_WEST] == 1)
            {
                //co cuop ben nay
                if (state[HUSBAND_WEST] == 1 || state[WIFE_WEST]==1 || state[BOY_WEST] > 0 || state[GIRL_WEST] > 0)
                {
                    if (state[POLICE_WEST] == 0)
                    {
                        //neu co cuop + khong co canh sat+ co nguoi
                        return false;
                    }
                }
            } 
            #endregion


            if (state[WIFE_EAST] == 1 && state[HUSBAND_EAST] == 0 && state[BOY_EAST] > 0) return false;
            if (state[WIFE_EAST] == 0 && state[HUSBAND_EAST] == 1 && state[GIRL_EAST] > 0) return false;
            
            if (state[WIFE_WEST] == 1 && state[HUSBAND_WEST] == 0 && state[BOY_WEST] > 0) return false;            
            if (state[WIFE_WEST] == 0 && state[HUSBAND_WEST] == 1 && state[GIRL_WEST] > 0) return false;



            return true;
        }

        //di chuyen vo + chong
        static bool CanHW(int[] s, out  int[] newResult)
        {
            if (s[0] == WEST)
            {

                int[] newState = new int[] { EAST, s[WIFE_EAST]+1,s[HUSBAND_EAST]+1, s[GIRL_EAST] ,s[BOY_EAST],s[ROBBER_EAST],s[POLICE_EAST],
                                                   s[WIFE_WEST]-1,s[HUSBAND_WEST]-1, s[GIRL_WEST] ,s[BOY_WEST],s[ROBBER_WEST],s[POLICE_WEST]
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
            else
            {

                int[] newState = new int[] { WEST,  s[WIFE_EAST]-1,s[HUSBAND_EAST]-1, s[GIRL_EAST] ,s[BOY_EAST],s[ROBBER_EAST],s[POLICE_EAST],
                                                    s[WIFE_WEST]+1,s[HUSBAND_WEST]+1, s[GIRL_WEST] ,s[BOY_WEST],s[ROBBER_WEST],s[POLICE_WEST]
                                                    };
                newResult = newState;
                return IsValid(newState);

            }
        }

        //di chuyen 1 boy + chong
        static bool CanHB(int[] s, out  int[] newResult)
        {
            if (s[0] == WEST)
            {
                int[] newState = new int[] { EAST, s[WIFE_EAST] , s[HUSBAND_EAST]+1, s[GIRL_EAST] ,s[BOY_EAST]+1,s[ROBBER_EAST],s[POLICE_EAST],
                                                    s[WIFE_WEST], s[HUSBAND_WEST]-1, s[GIRL_WEST] ,s[BOY_WEST]-1,s[ROBBER_WEST],s[POLICE_WEST]
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
            else
            {
                int[] newState = new int[] { WEST, s[WIFE_EAST] ,s[HUSBAND_EAST]-1, s[GIRL_EAST] ,s[BOY_EAST]-1,s[ROBBER_EAST],s[POLICE_EAST],
                                                    s[WIFE_WEST] ,s[HUSBAND_WEST]+1, s[GIRL_WEST] ,s[BOY_WEST]+1,s[ROBBER_WEST],s[POLICE_WEST]
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
        }
        //di chuyen 1 girl + chong
        static bool CanHG(int[] s, out  int[] newResult)
        {
            if (s[0] == WEST)
            {
                int[] newState = new int[] { EAST, s[WIFE_EAST] ,s[HUSBAND_EAST]+1, s[GIRL_EAST]+1 ,s[BOY_EAST],s[ROBBER_EAST],s[POLICE_EAST],
                                                    s[WIFE_WEST],s[HUSBAND_WEST]-1, s[GIRL_WEST]-1 ,s[BOY_WEST],s[ROBBER_WEST],s[POLICE_WEST]
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
            else
            {
                int[] newState = new int[] { WEST, s[WIFE_EAST] , s[HUSBAND_EAST]-1, s[GIRL_EAST]-1 ,s[BOY_EAST],s[ROBBER_EAST],s[POLICE_EAST],
                                                    s[WIFE_WEST] ,s[HUSBAND_WEST]+1, s[GIRL_WEST]+1 ,s[BOY_WEST],s[ROBBER_WEST],s[POLICE_WEST]
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
        }

        //di chuyen 1 girl + vo
        static bool CanWG(int[] s, out  int[] newResult)
        {
            if (s[0] == WEST)
            {
                int[] newState = new int[] { EAST, s[WIFE_EAST]+1, s[HUSBAND_EAST], s[GIRL_EAST]+1 ,s[BOY_EAST],s[ROBBER_EAST],s[POLICE_EAST],
                                                   s[WIFE_WEST]-1, s[HUSBAND_WEST], s[GIRL_WEST]-1 ,s[BOY_WEST],s[ROBBER_WEST],s[POLICE_WEST]
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
            else
            {
                int[] newState = new int[] { WEST, s[WIFE_EAST]-1 , s[HUSBAND_EAST], s[GIRL_EAST]-1 ,s[BOY_EAST],s[ROBBER_EAST],s[POLICE_EAST],
                                                    s[WIFE_WEST]+1, s[HUSBAND_WEST], s[GIRL_WEST]+1 ,s[BOY_WEST],s[ROBBER_WEST],s[POLICE_WEST]
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
        }

        //di chuyen 1 boy + vo
        static bool CanWB(int[] s, out  int[] newResult)
        {
            if (s[0] == WEST)
            {
                int[] newState = new int[] { EAST,  s[WIFE_EAST]+1, s[HUSBAND_EAST], s[GIRL_EAST] ,s[BOY_EAST]+1,s[ROBBER_EAST],s[POLICE_EAST],
                                                     s[WIFE_WEST]-1, s[HUSBAND_WEST], s[GIRL_WEST] ,s[BOY_WEST]-1,s[ROBBER_WEST],s[POLICE_WEST]
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
            else
            {
                int[] newState = new int[] { WEST,  s[WIFE_EAST]-1 , s[HUSBAND_EAST], s[GIRL_EAST] ,s[BOY_EAST]-1,s[ROBBER_EAST],s[POLICE_EAST],
                                                     s[WIFE_WEST]+1, s[HUSBAND_WEST], s[GIRL_WEST] ,s[BOY_WEST]+1,s[ROBBER_WEST],s[POLICE_WEST]
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
        }

        #region Di chuyen canh sat
        //di chuyen 1 boy + canh sat
        static bool CanPB(int[] s, out  int[] newResult)
        {
            if (s[0] == WEST)
            {
                int[] newState = new int[] { EAST, s[WIFE_EAST] , s[HUSBAND_EAST], s[GIRL_EAST] ,s[BOY_EAST]+1,s[ROBBER_EAST],s[POLICE_EAST]+1,
                                                    s[WIFE_WEST], s[HUSBAND_WEST], s[GIRL_WEST] ,s[BOY_WEST]-1,s[ROBBER_WEST],s[POLICE_WEST]-1
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
            else
            {
                int[] newState = new int[] { WEST, s[WIFE_EAST] , s[HUSBAND_EAST], s[GIRL_EAST] ,s[BOY_EAST]-1,s[ROBBER_EAST],s[POLICE_EAST]-1,
                                                    s[WIFE_WEST] , s[HUSBAND_WEST], s[GIRL_WEST] ,s[BOY_WEST]+1,s[ROBBER_WEST],s[POLICE_WEST]+1
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
        }

        //di chuyen 1 girl + canh sat
        static bool CanPG(int[] s, out  int[] newResult)
        {
            if (s[0] == WEST)
            {
                int[] newState = new int[] { EAST, s[WIFE_EAST] , s[HUSBAND_EAST], s[GIRL_EAST]+1 ,s[BOY_EAST],s[ROBBER_EAST],s[POLICE_EAST]+1,
                                                    s[WIFE_WEST], s[HUSBAND_WEST], s[GIRL_WEST]-1 ,s[BOY_WEST],s[ROBBER_WEST],s[POLICE_WEST]-1
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
            else
            {
                int[] newState = new int[] { WEST, s[WIFE_EAST] , s[HUSBAND_EAST], s[GIRL_EAST]-1 ,s[BOY_EAST],s[ROBBER_EAST],s[POLICE_EAST]-1,
                                                    s[WIFE_WEST] , s[HUSBAND_WEST], s[GIRL_WEST]+1 ,s[BOY_WEST],s[ROBBER_WEST],s[POLICE_WEST]+1
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
        }

        //di chuyen chong + canh sat
        static bool CanPH(int[] s, out  int[] newResult)
        {
            if (s[0] == WEST)
            {
                int[] newState = new int[] { EAST, s[WIFE_EAST] , s[HUSBAND_EAST]+1, s[GIRL_EAST],s[BOY_EAST],s[ROBBER_EAST],s[POLICE_EAST]+1,
                                                    s[WIFE_WEST], s[HUSBAND_WEST]-1, s[GIRL_WEST] ,s[BOY_WEST],s[ROBBER_WEST],s[POLICE_WEST]-1
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
            else
            {
                int[] newState = new int[] { WEST, s[WIFE_EAST] ,s[HUSBAND_EAST]-1, s[GIRL_EAST] ,s[BOY_EAST],s[ROBBER_EAST],s[POLICE_EAST]-1,
                                                    s[WIFE_WEST] ,s[HUSBAND_WEST]+1, s[GIRL_WEST] ,s[BOY_WEST],s[ROBBER_WEST],s[POLICE_WEST]+1
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
        }

        //di chuyen vo + canh sat
        static bool CanPW(int[] s, out  int[] newResult)
        {
            if (s[0] == WEST)
            {
                int[] newState = new int[] { EAST,s[WIFE_EAST] +1 , s[HUSBAND_EAST], s[GIRL_EAST],s[BOY_EAST],s[ROBBER_EAST],s[POLICE_EAST]+1,
                                                   s[WIFE_WEST] -1, s[HUSBAND_WEST], s[GIRL_WEST] ,s[BOY_WEST],s[ROBBER_WEST],s[POLICE_WEST]-1
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
            else
            {
                int[] newState = new int[] { WEST, s[WIFE_EAST] -1, s[HUSBAND_EAST], s[GIRL_EAST] ,s[BOY_EAST],s[ROBBER_EAST],s[POLICE_EAST]-1,
                                                   s[WIFE_WEST] +1, s[HUSBAND_WEST], s[GIRL_WEST] ,s[BOY_WEST],s[ROBBER_WEST],s[POLICE_WEST]+1
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
        }

        //di chuyen cuop + canh sat
        static bool CanPR(int[] s, out  int[] newResult)
        {
            if (s[0] == WEST)
            {
                int[] newState = new int[] { EAST, s[WIFE_EAST] , s[HUSBAND_EAST], s[GIRL_EAST] ,s[BOY_EAST],s[ROBBER_EAST]+1,s[POLICE_EAST]+1,
                                                    s[WIFE_WEST], s[HUSBAND_WEST], s[GIRL_WEST] ,s[BOY_WEST],s[ROBBER_WEST]-1,s[POLICE_WEST]-1
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
            else
            {
                int[] newState = new int[] { WEST, s[WIFE_EAST] , s[HUSBAND_EAST], s[GIRL_EAST] ,s[BOY_EAST],s[ROBBER_EAST]-1,s[POLICE_EAST]-1,
                                                    s[WIFE_WEST] , s[HUSBAND_WEST], s[GIRL_WEST] ,s[BOY_WEST],s[ROBBER_WEST]+1,s[POLICE_WEST]+1
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
        }
        #endregion

        //di chuyen  canh sat
        static bool CanP(int[] s, out  int[] newResult)
        {
            if (s[0] == WEST)
            {
                int[] newState = new int[] { EAST, s[WIFE_EAST] , s[HUSBAND_EAST], s[GIRL_EAST],s[BOY_EAST],s[ROBBER_EAST],s[POLICE_EAST]+1,
                                                   s[WIFE_WEST], s[HUSBAND_WEST], s[GIRL_WEST] ,s[BOY_WEST],s[ROBBER_WEST],s[POLICE_WEST]-1
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
            else
            {
                int[] newState = new int[] { WEST, s[WIFE_EAST] , s[HUSBAND_EAST], s[GIRL_EAST] ,s[BOY_EAST],s[ROBBER_EAST],s[POLICE_EAST]-1,
                                                   s[WIFE_WEST] , s[HUSBAND_WEST], s[GIRL_WEST] ,s[BOY_WEST],s[ROBBER_WEST],s[POLICE_WEST]+1
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
        }

        //di chuyen  vo
        static bool CanW(int[] s, out  int[] newResult)
        {
            if (s[0] == WEST)
            {
                int[] newState = new int[] { EAST, s[WIFE_EAST]+1, s[HUSBAND_EAST], s[GIRL_EAST],s[BOY_EAST],s[ROBBER_EAST],s[POLICE_EAST],
                                                   s[WIFE_WEST] -1, s[HUSBAND_WEST], s[GIRL_WEST] ,s[BOY_WEST],s[ROBBER_WEST],s[POLICE_WEST]
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
            else
            {
                int[] newState = new int[] { WEST,s[WIFE_EAST]-1, s[HUSBAND_EAST], s[GIRL_EAST] ,s[BOY_EAST],s[ROBBER_EAST],s[POLICE_EAST],
                                                   s[WIFE_WEST]+1, s[HUSBAND_WEST], s[GIRL_WEST] ,s[BOY_WEST],s[ROBBER_WEST],s[POLICE_WEST]
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
        }

        //di chuyen  chong
        static bool CanH(int[] s, out  int[] newResult)
        {
            if (s[0] == WEST)
            {
                int[] newState = new int[] { EAST, s[WIFE_EAST] ,s[HUSBAND_EAST]+1, s[GIRL_EAST],s[BOY_EAST],s[ROBBER_EAST],s[POLICE_EAST],
                                                    s[WIFE_WEST],  s[HUSBAND_WEST]-1, s[GIRL_WEST] ,s[BOY_WEST],s[ROBBER_WEST],s[POLICE_WEST]
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
            else
            {
                int[] newState = new int[] { WEST, s[WIFE_EAST] ,s[HUSBAND_EAST]-1, s[GIRL_EAST] ,s[BOY_EAST],s[ROBBER_EAST],s[POLICE_EAST],
                                                   s[WIFE_WEST], s[HUSBAND_WEST]+1, s[GIRL_WEST] ,s[BOY_WEST],s[ROBBER_WEST],s[POLICE_WEST]
                                                    };
                newResult = newState;
                return IsValid(newState);
            }
        }
    }
}
