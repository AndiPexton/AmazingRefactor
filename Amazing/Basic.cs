using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using Amazing.Gateway;
using Dependency;

namespace Amazing
{
    public class Basic
    {
        private static IRandom Random => Shelf.RetrieveInstance<IRandom>();

        public static void CLS(int width, int height)
        {
            Console.SetCursorPosition(0, 0);
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
            Console.Clear();
        }

        public static void PRINT(int pos, string text)
        {
            var y = pos / 64;
            var x = pos - (y * 64);

            Console.SetCursorPosition(x, y);

            Console.Write(text);
        }

        public static void LPRINT_(string text)
        {
            Console.Write(text);
        }


        public static void LPRINT(string text)
        {
                Console.WriteLine(text);
        }

        public static void PRINT(string text)
        {
            Console.Write(text);
        }

        public static void INPUT(string text, out int H, out int V)
        {
            Console.WriteLine(text);
            Console.Write("> ");
            var h = Console.ReadLine();
            Console.Write("> ");
            var v = Console.ReadLine();

            if (!int.TryParse(h, out H))
                H = 1;

            if (!int.TryParse(v, out V))
                V = 1;
        }

        public static (int, int) GetDimensions()
        {
            var width = 0;
            var height = 0;

            while (width <= 1 || height <= 1)
            {
                CLS(64, 16);
                INPUT("WHAT ARE YOUR WIDTH AND LENGTH", out width, out height);
                if (width > 1 && height > 1) return (width, height);
                PRINT("MEANINGLESS DIMENSIONS. TRY AGAIN");
                Thread.Sleep(2000);
            }

            return (width, height);
        }

        public static void DisplayWelcome()
        {
            CLS(64, 16);
            PRINT(412, "AMAZING");
            PRINT(538, "COPYRIGHT BY");
            PRINT(587, "CREATIVE COMPUTING  MORRISTOWN, NEW JERSEY");

            Thread.Sleep(5000);
        }

        public static int[,] BuildMaze(int H, int V)
        {
            var W = new int[H + 1, V + 1];
            var D = new int[H + 1, V + 1];

            var Q = 0;
            var Z = 0;
            var X = (int) Random.RND(H);


            foreach (var I in Enumerable.Range(1, H)) //130 FOR I=1 TO H
            {
                D[I, 0] = I == X ? 3 : 2;
            }

            _190:
            var C = 1;
            W[X, 1] = C;
            C = C + 1;
            _200:
            var R = X;
            var S = 1;
            goto _270;
            _210:
            if (R != H) goto _250;
            _220:
            if (S != V) goto _240;

            _230:
            R = 1;
            S = 1;
            goto _260;
            _240:
            R = 1;
            S = S + 1;
            goto _260;
            _250:
            R = R + 1;
            _260:
            if (W[R, S] == 0) goto _210;
            _270:
            if (R - 1 == 0) goto _600;
            _280:
            if (W[R - 1, S] != 0) goto _600;
            _290:
            if (S - 1 == 0) goto _430;
            _300:
            if (W[R, S - 1] != 0) goto _430;
            _310:
            if (R == H) goto _350;
            _320:
            if (W[R + 1, S] != 0) goto _350;
            _330:
            X = (int) Random.RND(3);

            _340:
            switch (X)
            {
                case 1: goto _940;
                case 2: goto _980;
                case 3: goto _1020;
            }

            _350:
            if (S != V) goto _380;
            _360:
            if (Z == 1) goto _410;
            _370:
            Q = 1;
            goto _390;
            _380:
            if (W[R, S + 1] != 0) goto _410;
            _390:
            X = (int) Random.RND(3);
            _400:
            switch (X)
            {
                case 1: goto _940;
                case 2: goto _980;
                case 3: goto _1090;
            }

            _410:
            X = (int) Random.RND(2);
            _420:
            switch (X)
            {
                case 1: goto _940;
                case 2: goto _980;
            }

            _430:
            if (R == H) goto _530;
            _440:
            if (W[R + 1, S] != 0) goto _530;
            _450:
            if (S != V) goto _480;
            _460:
            if (Z == 1) goto _510;
            _470:
            Q = 1;
            goto _490;

            _480:
            if (W[R, S + 1] != 0) goto _510;
            _490:
            X = (int) Random.RND(3);
            _500: //ON X GOTO 940,1020,1090
            switch (X)
            {
                case 1: goto _940;
                case 2: goto _1020;
                case 3: goto _1090;
            }

            _510:
            X = (int) Random.RND(2);
            _520: //ON X GOTO 940,1020
            switch (X)
            {
                case 1: goto _940;
                case 2: goto _1020;
            }

            _530:
            if (S != V) goto _560;
            _540:
            if (Z == 1) goto _590;
            _550:
            Q = 1;
            goto _570;
            _560:
            if (W[R, S + 1] != 0) goto _590;
            _570:
            X = (int) (Random.RND(0) * 2 + 1);
            _580: //1ON X GOTO 940,1090
            switch (X)
            {
                case 1: goto _940;
                case 2: goto _1090;
            }

            _590:
            goto _940;
            _600:
            if (S - 1 == 0) goto _790;
            _610:
            if (W[R, S - 1] != 0) goto _790;
            _620:
            if (R == H) goto _720;
            _630:
            if (W[R + 1, S] != 0) goto _720;
            _640:
            if (S != V) goto _670;
            _650:
            if (Z == 1) goto _700;
            _660:
            Q = 1;
            goto _680;
            _670:
            if (W[R, S + 1] != 0) goto _700;
            _680:
            X = (int) Random.RND(3);
            _690: //ON X GOTO 980,1020,1090
            switch (X)
            {
                case 1: goto _980;
                case 2: goto _1020;
                case 3: goto _1090;
            }

            _700:
            X = (int) Random.RND(2);
            _710: //ON X GOTO 980,1020
            switch (X)
            {
                case 1: goto _980;
                case 2: goto _1020;
            }

            _720:
            if (S != V) goto _750;
            _730:
            if (Z == 1) goto _780;
            _740:
            Q = 1;
            goto _760;
            _750:
            if (W[R, S + 1] != 0) goto _780;
            _760:
            X = (int) Random.RND(2);
            _770: //ON X GOTO 980,1090
            switch (X)
            {
                case 1: goto _980;
                case 2: goto _1090;
            }

            _780:
            goto _980;

            _790:
            if (R == H) goto _880;
            _800:
            if (W[R + 1, S] != 0) goto _880;
            _810:
            if (S != V) goto _840;
            _820:
            if (Z == 1) goto _870;
            _830:
            Q = 1;
            goto _990;
            _840:
            if (W[R, S + 1] != 0) goto _870;
            _850:
            X = (int) Random.RND(2);
            _860: //ON X GOTO 1020,1090
            switch (X)
            {
                case 1: goto _1020;
                case 2: goto _1090;
            }

            _870:
            goto _1020;
            _880:
            if (S != V) goto _910;
            _890:
            if (Z == 1) goto _930;
            _900:
            Q = 1;
            goto _920;

            _910:
            if (W[R, S + 1] != 0) goto _930;
            _920:
            goto _1090;
            _930:
            goto _1190;
            _940:
            W[R - 1, S] = C;
            _950:
            C = C + 1;
            D[R - 1, S] = 2;
            R = R - 1;
            _960:
            if (C == H * V + 1) goto _1200;
            _970:
            Q = 0;
            goto _270;
            _980:
            W[R, S - 1] = C;
            _990:
            C = C + 1;
            _1000:
            D[R, S - 1] = 1;
            S = S - 1;
            if (C == H * V + 1) goto _1200;
            _1010:
            Q = 0;
            goto _270;
            _1020:
            W[R + 1, S] = C;
            _1030:
            C = C + 1;
            if (D[R, S] == 0) goto _1050;
            _1040:
            D[R, S] = 3;
            goto _1060;
            _1050:
            D[R, S] = 2;
            _1060:
            R = R + 1;
            _1070:
            if (C == H * V + 1) goto _1200;
            _1080:
            goto _600;
            _1090:
            if (Q == 1) goto _1150;
            _1100:
            W[R, S + 1] = C;
            C = C + 1;
            if (D[R, S] == 0) goto _1120;
            _1110:
            D[R, S] = 3;
            goto _1130;
            _1120:
            D[R, S] = 1;
            _1130:
            S = S + 1;
            if (C == V * H + 1) goto _1200;
            _1140:
            goto _270;
            _1150:
            Z = 1;
            _1160:
            if (D[R, S] == 0) goto _1180;
            _1170:
            D[R, S] = 3;
            Q = 0;
            goto _1190;
            _1180:
            D[R, S] = 1;
            Q = 0;
            R = 1;
            S = 1;
            goto _260;
            _1190:
            goto _210;

            _1200:
            return D;
        }

        public static void DrawMaze(int[,] maze)
        {
            var width = maze.GetUpperBound(0);
            var height = maze.GetUpperBound(1);

            CLS(width * 3 + 2, height * 2 + 2);
            Console.WriteLine();

            foreach (var row in Enumerable.Range(0, height + 1))
            {
                if (row > 0)
                {
                    LPRINT_("I");
                    foreach (var column in Enumerable.Range(1, width))
                    {
                        LPRINT_(DrawWall(maze[column, row]));
                    }

                    LPRINT("");
                }

                foreach (var column in Enumerable.Range(1, width))
                {
                    LPRINT_(DrawBoxTop(maze[column, row]));
                }

                LPRINT(":");

                /*
                    2 = I
                    1 or 3 = --

                    Bin
                    00
                    "   "
                    ":  "

                    01
                    "   "
                    ":--
                    
                    10
                    "  I"
                    ":  "

                    11
                    "  I"
                    ":=="
                 */
            }
        }

        public static string DrawWall(int block) =>
            (block & 2) == 0 
                ? "  I" 
                : "   ";

        public static string DrawBoxTop(int block) =>
            (block & 1) == 0 
                ? ":--" 
                : ":  ";
    }
}
