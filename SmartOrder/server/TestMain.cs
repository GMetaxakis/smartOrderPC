﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartOrder
{
    public class TestMain
    {
        public static int testMain(String[] args)
        {
            HttpServer httpServer;
            if (args.GetLength(0) > 0)
            {
                httpServer = new MyHttpServer(Convert.ToInt16(args[0]));
            }
            else
            {
                httpServer = new MyHttpServer(8080);
            }
            Thread thread = new Thread(new ThreadStart(httpServer.listen));
            thread.Start();
            return 0;
        }

    }
}
