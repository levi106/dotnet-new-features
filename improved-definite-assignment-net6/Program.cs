C c = new();
if (c != null && c.M(out object obj1))
{
    obj1.ToString();
}

if ((c != null && c.M(out object obj2)) == true)
{
    obj2.ToString(); // Use of unassigned local variable 'obj2'
}

if ((c != null && c.M(out object obj3)) is true)
{
    obj3.ToString(); // Use of unassigned local variable 'obj3'
}


public class C
{
    public bool M(out object obj)
    {
        obj = new object();
        return true;
    }
}