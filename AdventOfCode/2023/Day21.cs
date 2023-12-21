namespace Advent2023;

public class Day21(string[] lines):IDay 
{
    int len = lines[0].Length;
    int height = lines[0].Length;
    public long GetTotalPartA()
    {
        return Solve(false);

    }
    public long GetTotalPartB()
    {
        return Solve(true);
    }

    private long Solve(bool part2)
    {
        TransposeGridPoint start = null;
        foreach (int i in height-1)
        {
            int j = lines[i].IndexOf('S');
            if (j >= 0)
            {
                start = new TransposeGridPoint(i, j);
                break;
            }
        }

        Queue<(TransposeGridPoint gp, int steps)> queue = new();
        int steps = 64;
        if (part2) steps = 26501365;
        queue.Enqueue((start,steps));
        List<TransposeGridPoint> list = [];

        while (queue.Count > 0)
        {
            var item = queue.Dequeue();
            if (item.steps == 0)
            {
                list.Add(item.gp);
                continue;
            }

            var current = item.gp;
            if((current.row>0||part2) && IsToAdd(queue,item.steps-1,Transpose(current.row-1,current.column, current.transposeRow,current.transposeColumn)))
                queue.Enqueue( (Transpose(current.row-1,current.column, current.transposeRow,current.transposeColumn),item.steps-1 ));
            
            if((current.row < height-1||part2) && IsToAdd(queue,item.steps-1,Transpose(current.row+1,current.column, current.transposeRow,current.transposeColumn)))
                queue.Enqueue( (Transpose(current.row+1,current.column, current.transposeRow,current.transposeColumn),item.steps-1 ));
            if((current.column < len-1||part2) && IsToAdd(queue,item.steps-1,Transpose(current.row,current.column+1, current.transposeRow,current.transposeColumn)))
                queue.Enqueue( (Transpose(current.row,current.column+1, current.transposeRow,current.transposeColumn),item.steps-1 ));
            if((current.column >0||part2) && IsToAdd(queue,item.steps-1,Transpose(current.row,current.column-1, current.transposeRow,current.transposeColumn)))
                queue.Enqueue((Transpose(current.row,current.column-1, current.transposeRow,current.transposeColumn),item.steps-1 ));

            
        }
        return list.Count;
    }
    private TransposeGridPoint Transpose(int row, int  column, int transposeRow, int transposeColumn)
    {
        
        if (row < 0)
        {
            row = height + row;
            transposeRow--;

        }

        if (column < 0)
        {
            column = len + column;
            transposeColumn--;
        }

        if (row == height)
        {
            row = 0;
            transposeRow++;
        }

        if (column == len)
        {
            column = 0;
            transposeColumn++;
        }
        return new TransposeGridPoint(row, column, transposeRow, transposeColumn);
    }

    private bool IsToAdd(Queue<(TransposeGridPoint gp, int steps)> queue, int step, TransposeGridPoint gp)
    {
        char c = lines[gp.row][gp.column];
        if (c != '.' && c != 'S') return false;
        return !queue.Any(q => q.gp.column == gp.column && q.gp.row == gp.row && q.steps == step 
                               && q.gp.transposeColumn == gp.transposeColumn
                               && q.gp.transposeRow == gp.transposeRow);
    }
   
    
   
}