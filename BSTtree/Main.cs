namespace DefaultNamespace
{
    public class Main
    {
        
        private static readonly int size = 8
        
        public static void Main(string[] args)
        {
            Random rand = new Random();
            int nums[8];
            foreach (int num in nums)
            {
                num = rand.next(0, size * 3);
                
            }

            BST root = new BST(nums[0]);
            
            for (int i = 1; i < size; i++)
            {
                root.add(new BST(nums[i]));
            }

            root.printStats();
        }
    }
}