namespace Hermany.AoC._2017._08
{
    public class Instruction
    {
        public string Name { get; set; }
        public int Inc { get; set; }
        public int Amount { get; set; }
        public string Lhs { get; set; }
        public string Operator { get; set; }
        public int Rhs { get; set; }

        public static Instruction Parse(string line)
        {
            var tokens = line.Split(' ');

            return new Instruction
            {
                Name = tokens[0],
                Inc = tokens[1] == "inc" ? 1 : -1,
                Amount = int.Parse(tokens[2]),
                Lhs = tokens[4],
                Operator = tokens[5],
                Rhs = int.Parse(tokens[6])
            };
        }
    }
}
