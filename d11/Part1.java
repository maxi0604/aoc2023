import java.util.ArrayList;
import java.util.Scanner;

public class Part1 {
    private static int dist(Point a, Point b) {
        // Manhattan norm.
        return Math.abs(a.x - b.x) + Math.abs(a.y - b.y);
    }
    public static void main(String[] args) {
        var lines = new ArrayList<String>();
        var emptyCols = new ArrayList<Integer>();
        var points = new ArrayList<Point>();
        var scan = new Scanner(System.in);

        while (scan.hasNext()) {
            String line = scan.nextLine().strip();
            if (line.isBlank())
                continue;
            lines.add(line);
        }
        scan.close();

        for (int x = 0; x < lines.get(0).length(); ++x) {
            boolean found = false;
            for (int y = 0; y < lines.size(); ++y) {
                if (lines.get(y).charAt(x) == '#') {
                    found = true;
                    break;
                }
            }

            if (!found)
                emptyCols.add(x);
        }

        System.out.println(emptyCols);
        int yOff = 0;
        for (int y = 0; y < lines.size(); ++y) {
            int xOff = 0;
            String line = lines.get(y);
            boolean found = false;
            for (int x = 0; x < line.length(); ++x) {
                if (emptyCols.contains(x))
                    xOff += 1;

                if (line.charAt(x) == '#') {
                    points.add(new Point(x + xOff, y + yOff));
                    found = true;
                }
            }

            if (!found)
                yOff += 1;

        }

        int sum = 0;
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
    public int x;
    public int y;

    public Point(int x, int y) {
        this.x = x;
        this.y = y;
    }
}
