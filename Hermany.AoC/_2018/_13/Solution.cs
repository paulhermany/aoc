using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._13
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var tracks = new Dictionary<(int, int), Track>();

            var carts = new List<Cart>();

            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[0].Length; x++)
                {
                    var c = input[y][x];
                    if (c != ' ')
                    {
                        var track = new Track(c, x, y);

                        switch (c)
                        {
                            case '+':
                                track.IsIntersection = true;
                                track.ConnectUp(tracks);
                                track.ConnectLeft(tracks);
                                break;
                            case '/':
                                track.IsTurn = true;
                                if (x > 0 && (input[y][x - 1] == '-' || input[y][x - 1] == '+' || input[y][x - 1] == '<' || input[y][x - 1] == '>'))
                                {
                                    track.ConnectUp(tracks);
                                    track.ConnectLeft(tracks);
                                }
                                break;
                            case '\\':
                                track.IsTurn = true;
                                if (x > 0 && (input[y][x - 1] == '-' || input[y][x - 1] == '+' || input[y][x - 1] == '<' || input[y][x - 1] == '>'))
                                {
                                    track.ConnectLeft(tracks);
                                }
                                if (y > 0 && (input[y - 1][x] == '|' || input[y - 1][x] == '+' || input[y - 1][x] == '^' || input[y - 1][x] == 'v'))
                                {
                                    track.ConnectUp(tracks);
                                }
                                break;
                            case '-':
                                track.ConnectLeft(tracks);
                                break;
                            case '|':
                                track.ConnectUp(tracks);
                                break;
                            case '>':
                                track.ConnectLeft(tracks);
                                carts.Add(new Cart(track, Direction.Right));                                
                                break;
                            case '^':
                                track.ConnectUp(tracks);
                                carts.Add(new Cart(track, Direction.Up));
                                break;
                            case '<':
                                track.ConnectLeft(tracks);
                                carts.Add(new Cart(track, Direction.Left));
                                break;
                            case 'v':
                                track.ConnectUp(tracks);
                                carts.Add(new Cart(track, Direction.Down));
                                break;
                        }
                        tracks.Add((x, y), track);
                    }
                }
            }

            var collision = false;
            Track collisionTrack = null;

            while (!collision)
            {
                foreach (var cart in carts)
                {
                    if (null != cart.Move())
                    {
                        collision = true;
                        collisionTrack = cart.Track;
                        break;
                    }
                }
            }

            return $"{collisionTrack.X},{collisionTrack.Y}";
        }

        //private void Print(Dictionary<(int, int), Track> tracks)
        //{
        //    for (var y = 0; y < 20; y++)
        //    {
        //        for (var x = 0; x < 20; x++)
        //        {
        //            if (tracks.ContainsKey((x, y)))
        //            {
        //                var track = tracks[(x, y)];
        //                Console.Write(null != track.Cart ? track.Cart.Print() : track.C);
        //            }
        //            else
        //            {
        //                Console.Write(' ');
        //            }
        //        }

        //        Console.WriteLine();
        //    }
        //}

        public string Part2(params string[] input)
        {
            var tracks = new Dictionary<(int, int), Track>();

            var carts = new List<Cart>();

            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[0].Length; x++)
                {
                    var c = input[y][x];
                    if (c != ' ')
                    {
                        var track = new Track(c, x, y);

                        switch (c)
                        {
                            case '+':
                                track.IsIntersection = true;
                                track.ConnectUp(tracks);
                                track.ConnectLeft(tracks);
                                break;
                            case '/':
                                track.IsTurn = true;
                                if (x > 0 && (input[y][x - 1] == '-' || input[y][x - 1] == '+' || input[y][x - 1] == '<' || input[y][x - 1] == '>'))
                                {
                                    track.ConnectUp(tracks);
                                    track.ConnectLeft(tracks);
                                }
                                break;
                            case '\\':
                                track.IsTurn = true;
                                if (x > 0 && (input[y][x - 1] == '-' || input[y][x - 1] == '+' || input[y][x - 1] == '<' || input[y][x - 1] == '>'))
                                {
                                    track.ConnectLeft(tracks);
                                }
                                if (y > 0 && (input[y - 1][x] == '|' || input[y - 1][x] == '+' || input[y - 1][x] == '^' || input[y - 1][x] == 'v'))
                                {
                                    track.ConnectUp(tracks);
                                }
                                break;
                            case '-':
                                track.ConnectLeft(tracks);
                                break;
                            case '|':
                                track.ConnectUp(tracks);
                                break;
                            case '>':
                                track.ConnectLeft(tracks);
                                carts.Add(new Cart(track, Direction.Right));
                                break;
                            case '^':
                                track.ConnectUp(tracks);
                                carts.Add(new Cart(track, Direction.Up));
                                break;
                            case '<':
                                track.ConnectLeft(tracks);
                                carts.Add(new Cart(track, Direction.Left));
                                break;
                            case 'v':
                                track.ConnectUp(tracks);
                                carts.Add(new Cart(track, Direction.Down));
                                break;
                        }
                        tracks.Add((x, y), track);
                    }
                }
            }
            
            while (carts.Count(_ => !_.Disabled) > 1)
            {
                foreach (var cart in carts)
                {
                    if (!cart.Disabled)
                    {
                        var hit = cart.Move();
                        if (null != hit)
                        {
                            hit.Disable();
                            cart.Disable();
                        }
                    }
                }
            }

            var lastCart = carts.Single(_ => !_.Disabled);

            return $"{lastCart.Track.X},{lastCart.Track.Y}";
        }
    }

    public enum Direction
    {
        Right = 0,
        Up = 1,
        Left = 2,
        Down = 3
    }

    public class Cart
    {
        public Track Track { get; set; }
        public Direction Facing { get; set; }

        public int NextTurn { get; set; }

        public bool Disabled { get; set; }

        public Cart(Track track, Direction facing)
        {
            this.Track = track;
            track.Cart = this;
            this.Facing = facing;
            this.NextTurn = 0;
            this.Disabled = false;
        }

        public Cart Move()
        {
            var currentTrack = Track;

            Track.Cart = null;

            switch (Facing)
            {
                case Direction.Right:
                    Track = Track.Right;
                    if (Track.IsIntersection)
                        Facing = NextTurn != 1 ? NextTurn == 0 ? Direction.Up : Direction.Down: Facing;
                    else if (Track.IsTurn)
                        Facing = null != Track.Up ? Direction.Up : Direction.Down;
                    break;

                case Direction.Up:
                    Track = Track.Up;
                    if (Track.IsIntersection)
                        Facing = NextTurn != 1 ? NextTurn == 0 ? Direction.Left : Direction.Right : Facing;
                    else if (Track.IsTurn)
                        Facing = null != Track.Left ? Direction.Left : Direction.Right;
                    break;

                case Direction.Left:
                    Track = Track.Left;
                    if (Track.IsIntersection)
                        Facing = NextTurn != 1 ? NextTurn == 0 ? Direction.Down : Direction.Up : Facing;
                    else if (Track.IsTurn)
                        Facing = null != Track.Up ? Direction.Up : Direction.Down;
                    break;

                case Direction.Down:
                    Track = Track.Down;
                    if (Track.IsIntersection)
                        Facing = NextTurn != 1 ? NextTurn == 0 ? Direction.Right : Direction.Left : Facing;
                    else if (Track.IsTurn)
                        Facing = null != Track.Left ? Direction.Left : Direction.Right;
                    break;
            }

            if(Track.IsIntersection)
                NextTurn = (NextTurn + 1) % 3;

            if (null != Track.Cart)
                return Track.Cart;

            Track.Cart = this;
            return null;
        }

        public void Disable()
        {
            if (null != this.Track)
                this.Track.Cart = null;
            this.Disabled = true;
        }

        public char Print()
        {
            switch (Facing)
            {
                case Direction.Right: return '>';
                case Direction.Up: return '^';
                case Direction.Left: return '<';
                case Direction.Down: return 'v';
            }
            return ' ';
        }
    }

    public class Track
    {
        public char C { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Track Right { get; set; }
        public Track Up { get; set; }
        public Track Left { get; set; }
        public Track Down { get; set; }
        public Cart Cart { get; set; }
        public bool IsIntersection { get; set; }
        public bool IsTurn { get; set; }

        public Track(char c, int x, int y)
        {
            this.C = c;
            this.X = x;
            this.Y = y;
            this.Right = null;
            this.Up = null;
            this.Left = null;
            this.Down = null;
            this.Cart = null;
            this.IsIntersection = false;
            this.IsTurn = false;
        }

        public void ConnectUp(Dictionary<(int, int), Track> tracks)
        {
            Up = tracks[(X, Y - 1)];
            tracks[(X, Y - 1)].Down = this;
        }

        public void ConnectLeft(Dictionary<(int, int), Track> tracks)
        {
            Left = tracks[(X - 1, Y)];
            tracks[(X - 1, Y)].Right = this;
        }
    }
}
