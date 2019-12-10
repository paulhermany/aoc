using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermany.AoC.Common
{
    public class Intcode
    {
        public Queue<long> Input { get; set; }

        public List<long> Output { get; set; }

        public bool IsHalted { get; private set; }

        public long Noun
        {
            get => GetMemory(1);
            set => SetMemory(1, value);
        }

        public long Verb
        {
            get => GetMemory(2);
            set => SetMemory(2, value);
        }

        public Intcode(long[] program, params long[] inputs)
        {
            _memory = new Dictionary<long, long>();
            _position = 0;
            _relativeBase = 0;

            for (var i = 0; i < program.LongLength; i++)
                _memory.Add(i, program[i]);

            IsHalted = false;

            Input = new Queue<long>(inputs);
            Output = new List<long>();
        }

        public void AddInput(params long[] inputs)
        {
            foreach (var input in inputs)
                Input.Enqueue(input);
        }

        public long? NextOutput(params long[] inputs)
        {
            if (IsHalted) return null;

            AddInput(inputs);

            var outputCount = Output.Count;
            while (!IsHalted && Output.Count == outputCount)
                Step();

            return Output.LastOrDefault();
        }

        public long? Run(params long[] inputs)
        {
            if (IsHalted) return null;

            AddInput(inputs);

            while (!IsHalted)
                Step();

            return Output.LastOrDefault();
        }

        public void Step()
        {
            var currentValue = GetMemory(_position);
            _opcode = currentValue % 100;
            _modes = (currentValue / 100).ToString().PadLeft(3, '0').Reverse().ToArray();

            switch (_opcode)
            {
                case 99:
                    IsHalted = true;
                    break;
                case 1:
                    SetParameterValue(3, GetParameterValue(1) + GetParameterValue(2));
                    _position += 4;
                    break;
                case 2:
                    SetParameterValue(3, GetParameterValue(1) * GetParameterValue(2));
                    _position += 4;
                    break;
                case 3:
                    SetParameterValue(1, Input.Dequeue());
                    _position += 2;
                    break;
                case 4:
                    Output.Add(GetParameterValue(1));
                    _position += 2;
                    break;
                case 5:
                    _position = GetParameterValue(1) != 0 ? GetParameterValue(2) : _position + 3;
                    break;
                case 6:
                    _position = GetParameterValue(1) == 0 ? GetParameterValue(2) : _position + 3;
                    break;
                case 7:
                    SetParameterValue(3, GetParameterValue(1) < GetParameterValue(2) ? 1 : 0);
                    _position += 4;
                    break;
                case 8:
                    SetParameterValue(3, GetParameterValue(1) == GetParameterValue(2) ? 1 : 0);
                    _position += 4;
                    break;
                case 9:
                    _relativeBase += GetParameterValue(1);
                    _position += 2;
                    break;
            }

        }

        private long GetMemory(long position)
        {
            if (!_memory.ContainsKey(position))
                _memory.Add(position, 0);
            return _memory[position];
        }

        private long GetParameterValue(short offset)
        {
            if (offset < 1) throw new ArgumentException(nameof(offset));
            var mode = _modes[offset - 1];

            switch (mode)
            {
                case '0':
                    return GetMemory(GetMemory(_position + offset));
                case '1':
                    return GetMemory(_position + offset);
                case '2':
                    return GetMemory(GetMemory(_position + offset) + _relativeBase);
            }

            throw new InvalidOperationException();
        }

        private void SetMemory(long position, long value)
        {
            if (!_memory.ContainsKey(position))
                _memory.Add(position, 0);
            _memory[position] = value;
        }

        private void SetParameterValue(short offset, long value)
        {
            if (offset < 1) throw new ArgumentException(nameof(offset));
            var mode = _modes[offset - 1];

            switch (mode)
            {
                case '0':
                    SetMemory(GetMemory(_position + offset), value);
                    return;
                case '1':
                    break;
                case '2':
                    SetMemory(GetMemory(_position + offset) + _relativeBase, value);
                    return;
            }

            throw new InvalidOperationException();
        }

        private Dictionary<long, long> _memory;

        private long _position;
        private long _opcode;
        private char[] _modes;

        private long _relativeBase;
    }

}
