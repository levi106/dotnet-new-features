using System.Diagnostics;

A a = new();
Debug.Assert(a.IsValid(), $"Detail: {a.GetDetail()}");

internal class A
{
    public bool IsValid() => true;

    public string GetDetail()
    {
        return "detail";
    }
}
