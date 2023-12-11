import java.util.ArrayList;
import java.util.Scanner;

public class Part1 {
    private static long dist(Point a, Point b) {
        // Manhattan norm.
        return Math.abs(a.x - b.x) + Math.abs(a.y - b.y);
    }
    public static void main(String[] args) {
        var lines = new ArrayList<String>();
        var emptyCols = new ArrayList<Long>();
        var points = new ArrayList<Point>();
        var scan = new Scanner(System.in);

        while (scan.hasNext()) {
            String line = scan.nextLine().strip();
            if (line.isBlank())
                continue;
            lines.add(line);
        }
        scan.close();

        for (long x = 0; x < lines.get((int)0).length(); ++x) {
            boolean found = false;
            for (long y = 0; y < lines.size(); ++y) {
                if (lines.get((int)y).charAt((int)x) == '#') {
                    found = true;
                    break;
                }
            }

            if (!found)
                emptyCols.add(x);
        }

        System.out.println(emptyCols);
        long yOff = 0;
        for (long y = 0; y < lines.size(); ++y) {
            long xOff = 0;
            String line = lines.get((int)y);
            boolean found = false;
            for (long x = 0; x < line.length(); ++x) {
                if (emptyCols.contains(x))
                    xOff += 1_000_000 - 1;

                if (line.charAt((int)x) == '#') {
                    points.add(new Point(x + xOff, y + yOff));
                    found = true;
                }
            }

            if (!found)
                yOff += 1_000_000 - 1;

        }

        long sum = 0;
        // System.out.println("emptyCols: = x: " + String.join(", ", emptyCols));
        for (Point p : points) {
            System.out.println("p = x: " + p.x + " y: " + p.y);
            for (Point q : points) {
                sum += dist(p, q);
            }
        }

        System.out.println("sum: " + sum + " res: " + sum/2);

    }
}

class Point {
    public long x;
    public long y;

    public Point(long x, long y) {
        this.x = x;
        this.y = y;
    }
}
