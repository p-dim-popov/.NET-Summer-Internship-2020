using System;
using System.Linq;

namespace Codenavirus
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var world = new[]
            {
                new [] { '#', '#', '#'},
                new [] { '#', '#', '#'},
                new [] { '#', '#', '#'}
            };

            var firstInfected = new[] { 1, 1 };

            Console.WriteLine($"[ {string.Join(", ", Codenavirus(world, firstInfected))} ]");

            //TODO: remove additional functions
        }

        static int[] Codenavirus(char[][] world, int[] firstInfected)
        {
            // Not using the new "Tuple type" because not sure of required C# version

            // Projection of the world with people and their health
            // Item1 - person's health state, Item2 - infection day of the person
            var people = new Tuple<char, int>[world.Length][];
            for (int row = 0; row < world.Length; row++)
            {
                people[row] = new Tuple<char, int>[world[row].Length];
                for (int col = 0; col < world[row].Length; col++)
                {
                    var state = world[row][col] == '#' ? 'H' : '.';
                    people[row][col] = Tuple.Create(state, 0);
                }
            }

            // infect the first person
            people[firstInfected[0]][firstInfected[1]] = Tuple.Create('I', 0);

            people.PrintTupleArray();
            Console.WriteLine();

            int daysPassed = 1;
            int infectedPeople = 1;
            int recoveredPeople = 0;
            int notInfectedPeople = world.Length * world[0].Length - infectedPeople;
            bool isSomeoneInfectedToday = true;

            while (isSomeoneInfectedToday)
            {
                isSomeoneInfectedToday = false;

                for (int row = 0; row < people.Length; row++)
                {
                    for (int col = 0; col < people[row].Length; col++)
                    {
                        var person = people[row][col];

                        if (person.Item1 != 'I') continue;

                        if (daysPassed - person.Item2 >= 3)
                        {
                            people[row][col] = Tuple.Create('R', person.Item2);
                            recoveredPeople++;
                            infectedPeople--;
                            continue;
                        }

                        // if person is infected the same day 
                        if (daysPassed - person.Item2 == 0) continue;

                        var possibleTargets = new[]
                        {
                            new { IsFeasible = col + 1 < people.Length && people[row][col + 1].Item1 == 'H', Row = row, Col = col + 1 }, // on the right
                            new { IsFeasible = row - 1 >= 0 && people[row - 1][col].Item1 == 'H', Row = row - 1,Col = col }, // on top
                            new { IsFeasible = col - 1 >= 0 && people[row][col - 1].Item1 == 'H', Row = row, Col = col - 1 }, // on the left
                            new { IsFeasible = row + 1 < people[row].Length && people[row + 1][col].Item1 == 'H', Row = row + 1, Col = col } // underneath
                        };

                        foreach (var possibleTarget in possibleTargets)
                        {
                            if (possibleTarget.IsFeasible) // is feasible target => infect
                            {
                                people[possibleTarget.Row][possibleTarget.Col] = Tuple.Create('I', daysPassed);
                                infectedPeople++;
                                notInfectedPeople--;
                                isSomeoneInfectedToday = true;
                                break; // only one person infected per day
                            }
                        }
                    }
                }

                people.PrintTupleArray();
                Console.WriteLine();

                daysPassed++;
            }

            return new int[] { daysPassed, infectedPeople, recoveredPeople, notInfectedPeople };
        }

        static void PrintTupleArray(this Tuple<char, int>[][] people)
        {
            people
                .Select(pa => string.Join(", ", pa.Select(p => p.Item1)))
                .ToList()
                .ForEach(Console.WriteLine);
        }
    }
}
