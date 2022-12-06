using System;
using System.Collections.Generic;
using System.Linq;

namespace test03
{

    /*    --- Day 3: Rucksack Reorganization ---
    One Elf has the important job of loading all of the rucksacks with supplies for the jungle journey. Unfortunately, that Elf didn't quite follow the packing instructions, and so a few items now need to be rearranged.

    Each rucksack has two large compartments. All items of a given type are meant to go into exactly one of the two compartments. The Elf that did the packing failed to follow this rule for exactly one item type per rucksack.

    The Elves have made a list of all of the items currently in each rucksack (your puzzle input), but they need your help finding the errors. Every item type is identified by a single lowercase or uppercase letter (that is, a and A refer to different types of items).

    The list of items for each rucksack is given as characters all on a single line. A given rucksack always has the same number of items in each of its two compartments, so the first half of the characters represent items in the first compartment, while the second half of the characters represent items in the second compartment.

    For example, suppose you have the following list of contents from six rucksacks:

    vJrwpWtwJgWrhcsFMMfFFhFp
    jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
    PmmdzqPrVvPwwTWBwg
    wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
    ttgJtRGJQctTZtZT
    CrZsJsPPZsGzwwsLwLmpwMDw

    The first rucksack contains the items vJrwpWtwJgWrhcsFMMfFFhFp, which means its first compartment contains the items vJrwpWtwJgWr, while the second compartment contains the items hcsFMMfFFhFp. The only item type that appears in both compartments is lowercase p.
    The second rucksack's compartments contain jqHRNqRjqzjGDLGL and rsFMfFZSrLrFZsSL. The only item type that appears in both compartments is uppercase L.
    The third rucksack's compartments contain PmmdzqPrV and vPwwTWBwg; the only common item type is uppercase P.
    The fourth rucksack's compartments only share item type v.
    The fifth rucksack's compartments only share item type t.
    The sixth rucksack's compartments only share item type s.
    To help prioritize item rearrangement, every item type can be converted to a priority:

    Lowercase item types a through z have priorities 1 through 26.
    Uppercase item types A through Z have priorities 27 through 52.
    In the above example, the priority of the item type that appears in both compartments of each rucksack is 16 (p), 38 (L), 42 (P), 22 (v), 20 (t), and 19 (s); the sum of these is 157.

    Find the item type that appears in both compartments of each rucksack. What is the sum of the priorities of those item types?

    Your puzzle answer was 7716.

    The first half of this puzzle is complete! It provides one gold star: *

    --- Part Two ---
    As you finish identifying the misplaced items, the Elves come to you with another issue.

    For safety, the Elves are divided into groups of three. Every Elf carries a badge that identifies their group. For efficiency, within each group of three Elves, the badge is the only item type carried by all three Elves. That is, if a group's badge is item type B, then all three Elves will have item type B somewhere in their rucksack, and at most two of the Elves will be carrying any other item type.

    The problem is that someone forgot to put this year's updated authenticity sticker on the badges. All of the badges need to be pulled out of the rucksacks so the new authenticity stickers can be attached.

    Additionally, nobody wrote down which item type corresponds to each group's badges. The only way to tell which item type is the right one is by finding the one item type that is common between all three Elves in each group.

    Every set of three lines in your list corresponds to a single group, but each group can have a different badge item type. So, in the above example, the first group's rucksacks are the first three lines:

    vJrwpWtwJgWrhcsFMMfFFhFp
    jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
    PmmdzqPrVvPwwTWBwg

    And the second group's rucksacks are the next three lines:

    wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
    ttgJtRGJQctTZtZT
    CrZsJsPPZsGzwwsLwLmpwMDw

    In the first group, the only item type that appears in all three rucksacks is lowercase r; this must be their badges. In the second group, their badge item type must be Z.

    Priorities for these items must still be found to organize the sticker attachment efforts: here, they are 18 (r) for the first group and 52 (Z) for the second group. The sum of these is 70.

    Find the item type that corresponds to the badges of each three-Elf group. What is the sum of the priorities of those item types?
    */
    class Program
    {
        static void part1Calculator(string[] contents)
        {
            char[] allchars;
            List<char> first = new List<char>();
            List<char> second = new List<char>();
            int half;
            int total=0;
            List<char> checkedList = new List<char>();

            for (int i = 0; i < contents.Length; i++)
            {
                checkedList.Clear();
                first.Clear();
                second.Clear();
                allchars = contents[i].ToCharArray();
                half = allchars.Length / 2;

                for (int k = 0; k < half; k++)
                {
                    first.Add(allchars[k]);
                    second.Add(allchars[half + k]);
                }
                for (int j = 0; j < half; j++)
                {
                    for (int t = 0; t < half; t++)
                    {
                        if (first[j]==second[t])
                        {
                            if (char.IsUpper(first[j]) && !checkedList.Contains(first[j]))
                            {
                                total += (int)first[j] - 38;
                            }
                            if (char.IsLower(first[j]) && !checkedList.Contains(first[j]))
                            {
                                total += (int)first[j] - 96;
                            }
                            checkedList.Add(first[j]);
                        }
                    }
                }

            }
            Console.WriteLine(total);
        }

        static void part2Calculator(string[] contents)
        {
            List<string> groupOfElves = new List<string>();
            int total=0;
            char groupID='a';
            
            for (int i = 0; i < contents.Length; i=i+3)
            {
                groupOfElves.Add(contents[i]);
                groupOfElves.Add(contents[i+1]);
                groupOfElves.Add(contents[i+2]);
                groupOfElves.Sort((a,b) => a.Length.CompareTo(b.Length));
                groupID = sameChar(groupOfElves[0], groupOfElves[1], groupOfElves[2]);
                groupOfElves.Clear();
                total += charNumber(groupID);
            }
            Console.WriteLine(total);
        }
        static int charNumber(char a)
        {
            if (char.IsUpper(a))
            {
                return (int)a - 38;
            }
            else
            {
                return (int)a - 96;
            }
        }
        static char sameChar(string a, string b, string c)
        {
            List<char> commonChars = new List<char>();
            List<char> first = new List<char>();
            List<char> second = new List<char>();
            List<char> third = new List<char>();
            

            first = a.ToList<char>();
            second = b.ToList<char>();
            third = c.ToList<char>();
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    if (first[i] == second[j]&&!commonChars.Contains(first[i]))
                    {
                        commonChars.Add(first[i]);
                    }
                }
            }

            for (int i = 0; i < third.Count; i++)
            {
                for (int j = 0; j < commonChars.Count; j++)
                {
                    if (third[i] == commonChars[j])
                    {
                        return third[i];
                    }
                }
            }
            return 'a';

        }
        static void Main(string[] args)
        {

            string[] lines = System.IO.File.ReadAllLines(@"D:\0omer_ilhan\codeTestNewYear\test03\input.txt");

            string[] testData = { "vJrwpWtwJgWrhcsFMMfFFhFp" , "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL" , "PmmdzqPrVvPwwTWBwg" , "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn" , "ttgJtRGJQctTZtZT" , "CrZsJsPPZsGzwwsLwLmpwMDw" };




            part1Calculator(lines);
            part2Calculator(lines);
            
            Console.ReadLine();
        }
    }
}
