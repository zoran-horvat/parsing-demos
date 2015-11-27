using LexicalAnalyzerDemo.DemoRunners;
using System;

namespace LexicalAnalyzerDemo
{
    class RunnerSelector
    {
        private DemoRunnerBase[] AvailableRunners =>
            new DemoRunnerBase[]
            {
                new ArithmeticExpressionWithRestarts(),
                new ArithmeticExpressionWithUnexpectedChar()
            };

        public DemoRunnerBase SelectRunner()
        {

            for (int i = 0; i < AvailableRunners.Length; i++)
                Console.WriteLine("{0}. {1}", i + 1, this.AvailableRunners[i].Description);

            int index = SelectRunnerIndex();

            return DuplicateRunnerAt(index);

        }

        private DemoRunnerBase DuplicateRunnerAt(int index)
        {
            return NewMethod(AvailableRunners[index]);
        }

        private DemoRunnerBase NewMethod(DemoRunnerBase runner)
        {
            return DuplicateRunner(runner.GetType());
        }

        private DemoRunnerBase DuplicateRunner(Type type)
        {
            return (DemoRunnerBase)Activator.CreateInstance(type);
        }

        private int SelectRunnerIndex()
        {
            while (true)
            {

                int index = SelectAnyRunnerIndex();

                if (index >= 1 && index <= this.AvailableRunners.Length)
                    return index - 1;

            }
        }

        private int SelectAnyRunnerIndex()
        {

            Console.WriteLine();
            Console.Write("Select runner to demonstrate (1..{0}): ", this.AvailableRunners.Length);

            int index = 0;

            if (!int.TryParse(Console.ReadLine(), out index))
                return -1;

            return index;

        }

    }
}
