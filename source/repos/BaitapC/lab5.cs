namespace lab05
{
    public class lab5
    {
   
        public void PTbac1()
        {
            Console.WriteLine("CHUONG TRINH TINH PHUONG TRINH BAC 1");
            Console.WriteLine("ax + b = 0");
            Console.WriteLine("Nhập vào hệ số a: ");
            float a = float.Parse(Console.ReadLine());
            Console.WriteLine("Nhập vào hệ số b: ");
            float b = float.Parse(Console.ReadLine());
            Console.WriteLine("Gia tri nghiem x la :, {0}", -b / a);
            Console.ReadLine();
        }
    }
}
