using System.Collections.Generic;
using UnityEngine;

public class LearnLab : MonoBehaviour
{
    void Start()
    {
        // ① List：按序存取（默认选择）
        var list = new List<int> { 1, 2, 3 };
        list.Add(4);                 // 尾部加
        list.RemoveAt(0);            // 按下标删第 0 个
        Debug.Log("List: " + string.Join(",", list));   // 2,3,4

        // ② Dictionary：按键查表
        var dict = new Dictionary<string, int> { ["hp"] = 100 };
        if (dict.TryGetValue("atk", out int atk))       // 找不到不报错，走 else
            Debug.Log("atk=" + atk);
        else
            Debug.Log("dict 里没有 atk 这个键（TryGetValue 没抛异常，安全）");

        // ③ HashSet：只管"在不在"
        var set = new HashSet<string> { "a", "b" };
        Debug.Log("set.Contains a = " + set.Contains("a"));   // True

        // ④ Queue：先进先出（买奶茶排队）
        var q = new Queue<int>();
        q.Enqueue(1); q.Enqueue(2); q.Enqueue(3);
        Debug.Log("Queue 出队顺序 = " + q.Dequeue() + "," + q.Dequeue() + "," + q.Dequeue()); // 1,2,3

        // ⑤ Stack：后进先出（叠盘子）
        var s = new Stack<int>();
        s.Push(1); s.Push(2); s.Push(3);
        Debug.Log("Stack 出栈顺序 = " + s.Pop() + "," + s.Pop() + "," + s.Pop()); // 3,2,1

        // ★ 到作品预热：用 Queue 写个"波次出生计划表"（只演示 FIFO 思想，W7 才真做）
        var wavePlan = new Queue<string>();
        wavePlan.Enqueue("3 只小怪");
        wavePlan.Enqueue("2 只小怪");
        wavePlan.Enqueue("1 只精英");
        Debug.Log("下一波 = " + wavePlan.Peek());        // Peek：只看队头，不拿走
    }
}
