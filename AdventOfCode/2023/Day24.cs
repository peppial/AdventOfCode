using System.Numerics;
using Advent2023.Structures;

namespace Advent2023;

public class Day24(string[] lines) : IDay
{
	private (Coordinate2D positions, Coordinate2D velocities)[] input;

	public long GetTotalPartA()
	{
		Init();
		return SolvePart1();
	}
	
	private void Init()
	{
		List<(Coordinate2D positionsA1, Coordinate2D positionsA2)> list = [];
		foreach (string line in lines)
		{
			var parts = line.SplitDefault("@");
			long[] coords = parts[0].GetNumbersLong();
			long[] offsets = parts[1].GetNumbersLong();
			Coordinate2D positionA1 = new (coords[0], coords[1]);
			Coordinate2D positionA2 = new (coords[0]+offsets[0], coords[1]+offsets[1]);

			list.Add((positionA1,positionA2));

		}

		input = list.ToArray();
	}
	long SolvePart1()
	{
		const long from = 200000000000000L;
		const long to = 400000000000000L;
		return input.Combinations(2).Count(list => Intersects(list[0], list[1]));

		bool Intersects((Coordinate2D positionsA1, Coordinate2D positionsA2) a, (Coordinate2D positionsB1, Coordinate2D positionsB2) b)
		{
			
			//y=mx+c
			double m1 =  (a.positionsA2.Y - (double)a.positionsA1.Y)/(a.positionsA2.X - (double)a.positionsA1.X);
			double c1 = a.positionsA1.Y - m1 * a.positionsA1.X; // c = y-mx
			
			double m2 =  (b.positionsB2.Y - (double)b.positionsB1.Y)/(b.positionsB2.X - (double)b.positionsB1.X);
			double c2 = b.positionsB1.Y - (m2 * b.positionsB1.X);
			
			if(Math.Abs(m1 - m2) < 0.0001) return false;// lines are parallel

			double diff = (m1 - m2);
			double intersectX = (c2 - c1)/diff; // x = (c2-c1)/(m1-m2)
			double intersectY = m1 * intersectX + c1;

			//check in the past
			if ( (a.positionsA1.X - a.positionsA2.X) * (intersectX - a.positionsA1.X)>0) return false;
			if ( (a.positionsA1.Y - a.positionsA2.Y) * (intersectY - a.positionsA1.Y)>0) return false;
			if ( (b.positionsB1.X - b.positionsB2.X) * (intersectX - b.positionsB1.X)>0) return false;
			if ( (b.positionsB1.Y - b.positionsB2.Y) * (intersectY - b.positionsB1.Y)>0) return false;
		
			return intersectX >= from && intersectX <= to && intersectY >= from && intersectY <= to;
		}
		
	}
	
	
	public long GetTotalPartB()
	{
		throw new NotImplementedException();
	}
}