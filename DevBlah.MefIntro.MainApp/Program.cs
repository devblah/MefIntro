using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Threading;
using DevBlah.MefIntro.Contracts;

namespace DevBlah.MefIntro.HostApp
{
    class Program
    {
        /// <summary>
        /// here we hold our found Encoders later
        /// </summary>
        private static List<IEncoder> _encoders;

        /// <summary>
        /// starting point of our host application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // hey guys
            Console.WriteLine("Welcome to the devblah.com MEF Example Application");

            // loop our programm
            bool running = true;
            while (running)
            {
                Console.WriteLine("What do you want to do?");
                Console.WriteLine(" 1. Encode");
                Console.WriteLine(" 2. Decode");
                Console.WriteLine(" 9. Search Encoders");
                Console.WriteLine(" 0. Exit Program");

                Console.Write("Your input: ");
                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        Encode();
                        break;
                    case ConsoleKey.D2:
                        Decode();
                        break;
                    case ConsoleKey.D9:
                        SearchEncoders();
                        break;
                    case ConsoleKey.Escape:
                    case ConsoleKey.D0:
                        Console.WriteLine("Bye");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("This action is not supported");
                        break;
                }
            }

            Thread.Sleep(500);
        }

        /// <summary>
        /// search for encoders and add them to our property
        /// </summary>
        private static void SearchEncoders()
        {
            Console.WriteLine("searching for encoders ...");
            _encoders = GetEncoders();
            Console.WriteLine("You have {0} encoders registered", _encoders.Count);
        }

        /// <summary>
        /// encode our given string
        /// </summary>
        private static void Encode()
        {
            IEncoder encoder = SelectEncoder();
            if (encoder == null)
            {
                return;
            }

            try
            {
                Console.WriteLine("Encoding with {0}", encoder.Name);
                Console.Write("Enter the original string: ");
                string input = Console.ReadLine();
                Console.WriteLine("Your encoded string:");
                Console.WriteLine(encoder.Encode(input));
            }
            catch (Exception)
            {
                Console.WriteLine("Your input couldn't be encoded with the selected encoder type.");
            }
        }

        /// <summary>
        /// decode our given string
        /// </summary>
        private static void Decode()
        {
            IEncoder encoder = SelectEncoder();
            if (encoder == null)
            {
                return;
            }

            try
            {
                Console.WriteLine("Decoding with {0}", encoder.Name);
                Console.Write("Enter the encoded string: ");
                string input = Console.ReadLine();
                Console.WriteLine("Your decoded string:");
                Console.WriteLine(encoder.Decode(input));
            }
            catch (Exception)
            {
                Console.WriteLine("Your input couldn't be decoded with the selected encoder type.");
            }
        }

        /// <summary>
        /// select which encoder shoul be used to do our stuff
        /// </summary>
        /// <returns></returns>
        private static IEncoder SelectEncoder()
        {
            if (_encoders == null || _encoders.Count == 0)
            {
                Console.WriteLine("No encoders could be found.");
                return null;
            }

            if (_encoders.Count == 1)
            {
                return _encoders.First();
            }

            char[] keys = "1234567890abcdefghijklmnopqrstuvwxyz".ToCharArray();

            Console.WriteLine("Select Encoder:");
            for (int i = 0; i < _encoders.Count; i++)
            {
                Console.WriteLine("{0}. {1}", keys[i], _encoders[i].Name);
            }

            Console.Write("Your input: ");
            ConsoleKeyInfo info = Console.ReadKey();
            Console.WriteLine();

            int position = Array.IndexOf(keys, info.KeyChar);
            if (position == -1)
            {
                Console.WriteLine("This action is not supported");
                return null;
            }

            return _encoders[position];
        }

        /// <summary>
        /// the actual MEF stuff
        /// </summary>
        /// <returns></returns>
        private static List<IEncoder> GetEncoders()
        {
            var catalog = new DirectoryCatalog("./", "DevBlah.MefIntro.Encoder.*.dll");
            var container = new CompositionContainer(catalog);
            return container.GetExportedValues<IEncoder>().ToList();
        }
    }
}
