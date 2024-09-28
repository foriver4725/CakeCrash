namespace General
{
    internal struct LoopedInt
    {
        private int value;
        private readonly int max;

        /// <summary>
        /// <para>value, max : [1, 100]</para>
        /// <para>max is exclusive</para>
        /// </summary>
        internal LoopedInt(int max, int value = 0)
        {
            this.max = (max is >= 0 and <= 100) ? max : 0;
            this.value = (value >= 0 && value < this.max) ? value : 0;
        }

        internal int Value
        {
            get => this.value;
            set
            {
                int val = value;
                while (val >= max) val -= max;
                while (val < 0) val += max;
                this.@value = val;
            }
        }
    }
}