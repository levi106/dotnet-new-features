A[] aa = new A[3];
B[] bb = new B[3];
A[] aa2 = bb;
aa[0] = new A();
aa[1] = new B();
aa[2] = new C();
aa2[0] = new B();
aa2[1] = new C();
aa2[2] = new A();

public class A { }
public class B : A { }
public class C : B { }
