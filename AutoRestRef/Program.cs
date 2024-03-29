﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRestRef.DataAccess;
using HtmlAgilityPack;
using System.Net;

namespace AutoRestRef
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("The input parameters must only contains input file path and output file path.");
                return 1;
            }
            //if has only 2 params
            var inputFilePath = args[0];
            var outputFilePath = args[1];
            return ARRAgent.Run(inputFilePath, outputFilePath) ? 0 : 1;
        } 
    }
}
