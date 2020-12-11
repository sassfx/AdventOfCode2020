using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class InstructionExecutor
    {
        private List<Instruction> instructions = new List<Instruction>();

        public void LoadInstructions(string[] instructionStrings)
        {
            foreach (var instructionString in instructionStrings)
            {
                var parts = instructionString.Split(" ");

                var instruction = new Instruction(parts[0], int.Parse(parts[1]));
                instructions.Add(instruction);
            }
        }

        public int CalculateUntilFirstRepeat()
        {
            return CalculateUntilFirstRepeatOrEnd(instructions.ToArray()).Accumulator;
        }

        private (int Accumulator, bool WasTerminated) CalculateUntilFirstRepeatOrEnd(Instruction[] instructionArray)
        {
            var accumulator = 0;
            var index = 0;
            while (index < instructionArray.Length)
            {
                var instruction = instructionArray[index];

                if (instruction.TimesAccessed > 0)
                {
                    return (accumulator, true);
                }

                accumulator = instruction.GetNextValue(accumulator);
                index = instruction.GetNextIndex(index);
                instruction.Increment();
            }

            return (accumulator, false);
        }

        public int ModifyProgramAndRun()
        {
            var timesToRun = instructions.Count(x => x.IsJumpOrNope());

            for (var currentInstructionToSwitch = 1; currentInstructionToSwitch <= timesToRun; currentInstructionToSwitch ++)
            {
                var newInstructions = new List<Instruction>();
                var currentNopeOrJumpInstruction = 0;

                foreach (var instruction in instructions)
                {
                    var newInstruction = new Instruction(instruction.Type, instruction.Value);

                    if (newInstruction.IsJumpOrNope())
                    {
                        currentNopeOrJumpInstruction += 1;
                        if (currentNopeOrJumpInstruction == currentInstructionToSwitch)
                        {
                            newInstruction = newInstruction.Switch();
                        }
                    }

                    newInstructions.Add(newInstruction);
                }

                var result = CalculateUntilFirstRepeatOrEnd(newInstructions.ToArray());

                if (!result.WasTerminated)
                {
                    return result.Accumulator;
                }
            }


            return -1;
        }
    }

    internal class Instruction
    {
        public InstructionType Type { get; set; }

        public int Value { get; }

        public int TimesAccessed { get; private set; }

        public Instruction(string type, int value)
        {
            Type = Enum.Parse<InstructionType>(type);
            Value = value;
            TimesAccessed = 0;
        }

        public Instruction(InstructionType type, int value)
        {
            Type = type;
            Value = value;
            TimesAccessed = 0;
        }

        public void Increment()
        {
            TimesAccessed += 1;
        }

        public int GetNextValue(int current)
        {
            if (Type == InstructionType.acc)
            {
                return current + Value;
            }

            return current;
        }

        public int GetNextIndex(int current)
        {
            if (Type == InstructionType.jmp)
            {
                return current + Value;
            }

            return current + 1;
        }

        public Instruction Switch()
        {
            var newType = Type;
            if (newType == InstructionType.nop)
            {
                newType = InstructionType.jmp;
            }
            else if (newType == InstructionType.jmp)
            {
                newType = InstructionType.nop;
            }

            return new Instruction(newType, Value);
        }

        public bool IsJumpOrNope()
        {
            return Type == InstructionType.nop || Type == InstructionType.jmp;
        }
    }

    internal enum InstructionType
    {
        acc,
        jmp,
        nop,
    }
}