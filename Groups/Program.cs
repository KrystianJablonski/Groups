using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groups
{
    public class Group<T> : IComparable<Group<int>>
    {
        public Group(IEnumerable<T> elements)
        {
            Elements = elements.OrderBy(n => n).ToArray();
        }
        public T[] Elements { get; }

        public bool ContainsElements(T[] otherElements)
        {
            foreach (var e1 in Elements)
            {
                foreach (var e2 in otherElements)
                {
                    if (e1.Equals(e2))
                        return true;
                }
            }

            return false;
        }

        public bool Equals(Group<T> other)
        {
            if (object.ReferenceEquals(this, other))
                return true;
            if (Elements.Length != other.Elements.Length)
                return false;
            for (int i = 0; i < Elements.Length; i++)
                if (!Elements[i].Equals(other.Elements[i]))
                    return false;
            return true;
        }


        public int CompareTo(Group<int> other)
        {
            if (other.Elements.Length != Elements.Length)
            {
                return other.Elements.Length.CompareTo(Elements.Length);
            }
            int[] elements = Elements as int[];
            for (int i = 0; i < Elements.Length; i++)
            {
                if (elements[i] < other.Elements[i])
                    return -1;
                else if (elements[i] > other.Elements[i])
                    return 1;
            }

            return 0;
        }

        public override string ToString()
        {
            string result = "{";
            for (int i = 0; i < Elements.Length; i++)
                result += Elements[i] + (i == Elements.Length - 1 ? "" : ", ");
            result += "}";
            return result;
        }
    }
    class Program
    {
        public static bool DecreaseForVariable(int[] variables, int iterator, int maxValue)
        {
            variables[iterator]++;
            int reversedIterator = variables.Length - 1 - iterator;
            if (variables[iterator] > maxValue - reversedIterator)
            {
                if (iterator == 0)
                    return false;
                DecreaseForVariable(variables, iterator - 1, maxValue);
                variables[iterator] = variables[iterator - 1] + 1;
            }

            return true;
        }
        static void Main(string[] args)
        {
            Stopwatch timer = Stopwatch.StartNew();
            int[] allElements = {1, 2, 3, 4, 5, 6, 7, 8, 9};
            List<Group<int>> singleGroupCombinations = new List<Group<int>>();
            for (int i = 0; i < allElements.Length; i++)
            {
                for (int j = i + 1; j < allElements.Length; j++)
                {
                    for (int k = j + 1; k < allElements.Length; k++)
                    {
                        Group<int> newGroup = new Group<int>(new[]
                        {
                            allElements[i], allElements[j], allElements[k]
                        });
                        singleGroupCombinations.Add(newGroup);
                    }
                }
            }
            
            
            
            List<Group<Group<int>>> groupsCombinations = new List<Group<Group<int>>>();
            for (int i = 0; i < singleGroupCombinations.Count; i++)
            {
                List<Group<int>> singleGroupCombinations2 = singleGroupCombinations
                    .Except(singleGroupCombinations.Where(g => g.ContainsElements(singleGroupCombinations[i].Elements)))
                    .ToList();
                for (int j = 0; j < singleGroupCombinations2.Count; j++)
                {
                    List<Group<int>> singleGroupCombinations3 = singleGroupCombinations2
                        .Except(singleGroupCombinations2.Where(g => g.ContainsElements(singleGroupCombinations2[j].Elements)))
                        .ToList();
                    for (int k = 0; k < singleGroupCombinations3.Count; k++)
                    {
                        Group<Group<int>> newGroup = new Group<Group<int>>(new[]
                        {
                            singleGroupCombinations[i], singleGroupCombinations2[j], singleGroupCombinations3[k]
                        });
                        groupsCombinations.Add(newGroup);
                        
                    }
                }
            }
            Console.WriteLine(timer.ElapsedMilliseconds);
//            int[] places = new int[allElements.Length];
//
//            for (int i = 0; i < places.Length; i++)
//            {
//                places[i] = i;
//            }
//
//            while (true)
//            {
//                Group<int>[] newGroups = new Group<int>[3];
//                for (int i = 0; i < newGroups.Length; i++)
//                {
//                    int index1 = i * 3;
//                    int index2 = index1 + 1;
//                    int index3 = index2 + 1;
//                newGroups[i] = new Group<int>(new []
//                    {
//                        allElements[places[index1]], allElements[places[index2]] , allElements[places[index3]]
//                    });
//                }
//                groupsCombinations.Add(new Group<Group<int>>(newGroups));
//                if(!DecreaseForVariable(places, places.Length - 1, allElements.Length - 1))
//                    break;
//            }

            Console.ReadKey();
        }
    }
}
