Segment s = new (new (10, 0),  new (20, 30));
Console.WriteLine($"{IsAnyEndOnXAxis(s)}");

//bool IsAnyEndOnXAxis(Segment segment) =>
//    segment is { Start: { Y: 0 } } or { End: { Y: 0 } };

bool IsAnyEndOnXAxis(Segment segment) =>
    segment is { Start.Y: 0 } or { End.Y: 0 };

public record Point(int X, int Y);
public record Segment(Point Start, Point End);

