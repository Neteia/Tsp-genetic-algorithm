using System;
using System.IO;

namespace tsp
{
	class Program
	{
		public static int[,] ReWriteArray(String filePath)
		{
			String[] lines = File.ReadAllLines(filePath);
			int size = int.Parse(lines[0]);
			int[,] distancesArray = new int[size, size];
			int currentLine = 0;

			foreach (string line in lines)
			{
				string[] row = line.Trim().Split(' ');

				if (line != lines[0])
				{
					for (int i = 0; i < row.Length; i++)
					{
						int value = int.Parse(row[i]);
						distancesArray[currentLine, i] = value;
						distancesArray[i, currentLine] = value;
					}
					currentLine++;
				}
			}
			return distancesArray;
		}
		public static void WriteArray(int[,] arr)
		{
			for (int i = 0; i < arr.GetLength(0); i++)
			{
				for (int j = 0; j < arr.GetLength(1); j++)
				{
					Console.Write(arr[i, j] + " ");
				}
				Console.WriteLine();
			}
		}

		public static int[] DrawIndividual(int[,] arr)
		{
			int[] individual = new int[arr.GetLength(0) + 1];
			int sumOfDistances = 0;
			Random rand = new Random();
			int firstDistance = rand.Next(1, arr.GetLength(0));
			int drawn = 0;
			int y = -1;
			for (int i = 0; i < arr.GetLength(0) + 2; i++)
			{
				for (int j = 0; j < arr.GetLength(1); j++)
				{
					y++;
					if (firstDistance != -1)
					{
						individual[0] = firstDistance;
						sumOfDistances += arr[i, firstDistance];
						i = firstDistance - 1;
						firstDistance = -1;
						break;
					}
					if (drawn != 0)
					{
						j = drawn;
						individual[y - 1] = j;
						sumOfDistances += arr[i - 1, j - 1];
						i = drawn - 1;
						break;
					}
					if (drawn == 0)
					{
						j = 0;
						individual[y] = arr[i, j];
						sumOfDistances += arr[i, j];
						i = arr.GetLength(0) + 2;
						break;
					}
				}
				drawn = rand.Next(1, arr.GetLength(0));
				for (int x = 0; x < arr.GetLength(0); x++)
				{
					if (individual[x] == drawn)
					{
						drawn = rand.Next(1, arr.GetLength(0));
						x = 0;
					}
					if (individual[0] == drawn)
					{
						drawn = rand.Next(1, arr.GetLength(0));
					}
				}
				if (y == individual.Length - 2)
				{
					drawn = 0;
				}
			}
			individual[individual.Length - 1] = sumOfDistances;
			return individual;
		}
		static void Main(string[] args)
		{
			String filePath = @"C:\Users\bbrzy\OneDrive\Pulpit\Berlin52.txt";
			int[,] distancesArray = null;
			distancesArray = Program.ReWriteArray(filePath);
			WriteArray(distancesArray);
			int[] individual = new int[distancesArray.GetLength(0) + 1];
			int[,] population = new int[40, individual.Length];
			int[] tempArr = new int[distancesArray.GetLength(0) + 1];
			int[] tempArr2 = new int[distancesArray.GetLength(0)];
			int p = 0;
			#region CreatePopulation
			for (int i = 0; i < population.GetLength(0); i++)
			{
				individual = Program.DrawIndividual(distancesArray);
				for (int j = 0; j < population.GetLength(1); j++)
				{
					if (individual[1] == individual[j])
					{
						p += 1;

					}
					if (p == 2 && j == distancesArray.GetLength(0) && i != 0)
					{
						i -= 1;
					}
					if (p == 2 && j == distancesArray.GetLength(0) && i == 0)
					{
						i = 0;
					}
					population[i, j] = individual[j];
					Console.Write(population[i, j] + " ");
				}
				Console.WriteLine();
			}
			#endregion
			int counter = 0;
			int[,] tournamentArr = null;
			while (counter < 300000)
			{
				#region TournamentSelection
				Random rand = new Random();
				int drawnX = 0;
				int drawnY = 0;
				int drawnZ = 0;
				int drawnT = 0;
				int drawnR = 0;
				int drawnK = 0;
				int drawnL = 0;
				int x = 0;
				int y = 0;
				int z = 0;
				int t = 0;
				int r = 0;
				int k = 0;
				int l = 0;
				int minimum = 0;
				tournamentArr = new int[40, individual.Length];
				for (int i = 0; i < population.GetLength(0); i++)
				{
					drawnX = rand.Next(0, population.GetLength(0));
					x = population[drawnX, individual.Length - 1];
					drawnY = rand.Next(0, population.GetLength(0));
					y = population[drawnY, individual.Length - 1];
					drawnZ = rand.Next(0, population.GetLength(0));
					z = population[drawnZ, individual.Length - 1];
					drawnT = rand.Next(0, population.GetLength(0));
					t = population[drawnT, individual.Length - 1];
					drawnR = rand.Next(0, population.GetLength(0));
					r = population[drawnR, individual.Length - 1];
					drawnK = rand.Next(0, population.GetLength(0));
					k = population[drawnK, individual.Length - 1];
					drawnL = rand.Next(0, population.GetLength(0));
					l = population[drawnL, individual.Length - 1];
					minimum = Math.Min(Math.Min(l, Math.Min(x, y)), Math.Min(Math.Min(z, t), Math.Min(r, k)));
					if (x == minimum)
					{
						for (int j = 0; j < population.GetLength(1); j++)
						{
							tournamentArr[i, j] = population[drawnX, j];
						}
					}
					if (y == minimum)
					{
						for (int j = 0; j < population.GetLength(1); j++)
						{
							tournamentArr[i, j] = population[drawnY, j];
						}
					}
					if (z == minimum)
					{
						for (int j = 0; j < population.GetLength(1); j++)
						{
							tournamentArr[i, j] = population[drawnZ, j];
						}
					}
					if (t == minimum)
					{
						for (int j = 0; j < population.GetLength(1); j++)
						{
							tournamentArr[i, j] = population[drawnT, j];
						}
					}
					if (r == minimum)
					{
						for (int j = 0; j < population.GetLength(1); j++)
						{
							tournamentArr[i, j] = population[drawnR, j];
						}
					}
					if (k == minimum)
					{
						for (int j = 0; j < population.GetLength(1); j++)
						{
							tournamentArr[i, j] = population[drawnK, j];
						}
					}
					if (l == minimum)
					{
						for (int j = 0; j < population.GetLength(1); j++)
						{
							tournamentArr[i, j] = population[drawnL, j];
						}
					}
				}
				#endregion
				#region CrossOver
				for (int i = 0; i < population.GetLength(0); i++)
				{
					for (int j = 0; j < distancesArray.GetLength(1) + 1; j++)
					{
						population[i, j] = -1;
					}
				}
				for (int i = 0; i < population.GetLength(0); i++)
				{
					if (i % 2 == 0)
					{
						for (int j = 0; j < distancesArray.GetLength(1); j++)
						{
							int f = rand.Next(25);

							if (f == 0)
							{
								tempArr[j] = 0;
							}
							else
							{
								tempArr[j] = 1;
							}

						}
					}
					for (int j = 0; j < distancesArray.GetLength(1) + 1; j++)
					{
						if (tempArr[j] == 1)
						{
							population[i, j] = tournamentArr[i, j];
						}
					}
					for (int j = 0; j < distancesArray.GetLength(1); j++)
					{
						tempArr2[j] = population[i, j];
					}
					int searching = -1;
					bool isEverything = false;
					while (isEverything == false)
					{
						for (int j = 0; j < distancesArray.GetLength(1); j++)
						{
							if (population[i, j] == -1)
							{

								searching = j;
								j = distancesArray.GetLength(0) + 1;
								isEverything = false;
							}
							else
							{
								isEverything = true;
							}

						}
						if (searching != -1)
						{

							if (i % 2 == 0)
							{

								for (int j = 0; j < distancesArray.GetLength(1); j++)
								{
									int isUnique = 0;
									int p1 = tournamentArr[i + 1, j];
									for (int o = 0; o < distancesArray.GetLength(1); o++)
									{
										if (tempArr2[o] != p1)
										{

											isUnique++;
										}
										if (distancesArray.GetLength(0) == isUnique)
										{
											population[i, searching] = p1;
											tempArr2[searching] = p1;
											j = distancesArray.GetLength(0) + 1;
											o = distancesArray.GetLength(0) + 1;
										}
									}
								}
							}
							if (i % 2 == 1)
							{
								for (int j = 0; j < distancesArray.GetLength(0); j++)
								{
									int isUnique = 0;
									int p1 = tournamentArr[i - 1, j];
									for (int o = 0; o < distancesArray.GetLength(0); o++)
									{
										if (tempArr2[o] != p1)
										{

											isUnique++;
										}

										if (distancesArray.GetLength(0) == isUnique)
										{
											population[i, searching] = p1;
											tempArr2[searching] = p1;
											j = distancesArray.GetLength(0) + 1;
											o = distancesArray.GetLength(0) + 1;
										}
									}
								}
							}
						}
					}
				}
				for (int i = 0; i < population.GetLength(0); i++)
				{
					for (int j = 0; j < population.GetLength(1); j++)
					{
						tournamentArr[i, j] = population[i, j];
					}
				}
				#endregion CrossOver
				#region Mutation
				int firstRand = 0;
				int secondRand = 0;
				int temp = 0;
				int temp2 = 0;
				int chanceForMutation = 0;
				bool isMutating = false;
				for (int i = 0; i < tournamentArr.GetLength(0); i++)
				{
					chanceForMutation = rand.Next(100);
					if (chanceForMutation < 2)
					{
						firstRand = rand.Next(0, population.GetLength(1));
						secondRand = rand.Next(0, population.GetLength(1));
						while (firstRand == secondRand)
						{
							firstRand = rand.Next(0, population.GetLength(1));
							secondRand = rand.Next(0, population.GetLength(1));
						}
						if (secondRand < firstRand)
						{
							temp = secondRand;
							secondRand = firstRand;
							firstRand = temp;
						}
						isMutating = true;
					}
					else
					{
						isMutating = false;
					}

					for (int j = 0; j < tournamentArr.GetLength(1); j++)
					{
						if (isMutating == true)
						{
							if (j > firstRand && j < secondRand)
							{
								if (firstRand != secondRand)
								{
									temp2 = tournamentArr[i, firstRand + 1];
									tournamentArr[i, firstRand + 1] = tournamentArr[i, secondRand - 1];
									tournamentArr[i, secondRand - 1] = temp2;
									firstRand++;
									secondRand--;
								}
							}
							if (j == secondRand)
							{
								break;
							}
						}
						else
						{
							break;
						}
					}
				}
				int sumAfterMutation = 0;
				for (int i = 0; i < population.GetLength(0); i++)
				{
					sumAfterMutation = 0;
					int h = tournamentArr[i, distancesArray.GetLength(0) - 1];
					for (int j = 0; j < distancesArray.GetLength(0); j++)
					{
						sumAfterMutation += distancesArray[h, j];
						if (j == 0)
						{
							h = tournamentArr[i, 0];
						}
						else
						{
							h = tournamentArr[i, j - 1];
						}
					}
					tournamentArr[i, distancesArray.GetLength(0)] = sumAfterMutation;
				}
				for (int i = 0; i < population.GetLength(0); i++)
				{
					for (int j = 0; j < population.GetLength(1); j++)
					{
						population[i, j] = tournamentArr[i, j];
					}
				}
				#endregion
				counter++;
				if (counter % 5000 == 0)
				{
					for (int i = 0; i < tournamentArr.GetLength(0); i++)
					{
						for (int j = 0; j < tournamentArr.GetLength(1); j++)
						{
							Console.Write(tournamentArr[i, j] + " ");
						}
						Console.WriteLine();
					}
				}
			}
			Console.WriteLine("Wyniki");
			int smallest = 500000;
			int[] bestIndvidual = new int [distancesArray.GetLength(0) + 1];
			for (int i = 0; i < 40; i++)
			{
				for (int j = 0; j < distancesArray.GetLength(1) + 1; j++)
				{
					if (smallest > tournamentArr[i, distancesArray.GetLength(0)])
					{
						smallest = tournamentArr[i, distancesArray.GetLength(0)];
						for (int k = 0; k < distancesArray.GetLength(0) + 1; k++)
						{
							bestIndvidual[k] = tournamentArr[i, k];
						}
					}
				}
			}
			for (int i = 0; i < bestIndvidual.Length; i++)
			{
				Console.Write(bestIndvidual[i] + " ");
			}
		}
	}
}

