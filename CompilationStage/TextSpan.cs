﻿namespace custom_compiler_tutorial.CompilationStage
{
    public struct TextSpan
    {
        public int Start { get; }
        public int Length { get; }
        public int End => Start + Length;

        public TextSpan(int start, int length)
        {
            Start = start;
            Length = length;
        }

        public static TextSpan FromBounds(int start, int end)
        {
            int length = end - start;
            return new(start, length);
        }
    }
}
