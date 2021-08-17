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

       
            var C = 1;
            W[X, 1] = C;
            C = C + 1;
         
            var R = X;
            var S = 1;
            goto _270;
            _210:
            if (R != H) goto _250;
          
            if (S != V) goto _240;

         
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
          
            if (W[R - 1, S] != 0) goto _600;
           
            if (S - 1 == 0) goto _430;
           
            if (W[R, S - 1] != 0) goto _430;
         
            if (R == H) goto _350;
           
            if (W[R + 1, S] != 0) goto _350;
           
            X = (int) Random.RND(3);

           
            switch (X)
            {
                case 1: goto _940;
                case 2: goto _980;
                case 3: goto _1020;
            }

            _350:
            if (S != V) goto _380;
         
            if (Z == 1) goto _410;
         
            Q = 1;
            goto _390;
            _380:
            if (W[R, S + 1] != 0) goto _410;
            _390:
            X = (int) Random.RND(3);
        
            switch (X)
            {
                case 1: goto _940;
                case 2: goto _980;
                case 3: goto _1090;
            }

            _410:
            X = (int) Random.RND(2);
       
            switch (X)
            {
                case 1: goto _940;
                case 2: goto _980;
            }

            _430:
            if (R == H) goto _530;
           
            if (W[R + 1, S] != 0) goto _530;
           
            if (S != V) goto _480;
           
            if (Z == 1) goto _510;
         
            Q = 1;
            goto _490;

            _480:
            if (W[R, S + 1] != 0) goto _510;
            _490:
            X = (int) Random.RND(3);
           
            switch (X)
            {
                case 1: goto _940;
                case 2: goto _1020;
                case 3: goto _1090;
            }

            _510:
            X = (int) Random.RND(2);
            
            switch (X)
            {
                case 1: goto _940;
                case 2: goto _1020;
            }

            _530:
            if (S != V) goto _560;
        
            if (Z == 1) goto _590;
        
            Q = 1;
            goto _570;
            _560:
            if (W[R, S + 1] != 0) goto _590;
            _570:
            X = (int) (Random.RND(0) * 2 + 1);
         
            switch (X)
            {
                case 1: goto _940;
                case 2: goto _1090;
            }

            _590:
            goto _940;
            _600:
            if (S - 1 == 0) goto _790;
           
            if (W[R, S - 1] != 0) goto _790;
          
            if (R == H) goto _720;
         
            if (W[R + 1, S] != 0) goto _720;
           
            if (S != V) goto _670;
           
            if (Z == 1) goto _700;
          
            Q = 1;
            goto _680;
            _670:
            if (W[R, S + 1] != 0) goto _700;
            _680:
            X = (int) Random.RND(3);
           
            switch (X)
            {
                case 1: goto _980;
                case 2: goto _1020;
                case 3: goto _1090;
            }

            _700:
            X = (int) Random.RND(2);
       
            switch (X)
            {
                case 1: goto _980;
                case 2: goto _1020;
            }

            _720:
            if (S != V) goto _750;
           
            if (Z == 1) goto _780;
           
            Q = 1;
            goto _760;
            _750:
            if (W[R, S + 1] != 0) goto _780;
            _760:
            X = (int) Random.RND(2);
          
            switch (X)
            {
                case 1: goto _980;
                case 2: goto _1090;
            }

            _780:
            goto _980;

            _790:
            if (R == H) goto _880;
         
            if (W[R + 1, S] != 0) goto _880;
          
            if (S != V) goto _840;
           
            if (Z == 1) goto _870;
           
            Q = 1;
            goto _990;
            _840:
            if (W[R, S + 1] != 0) goto _870;
         
            X = (int) Random.RND(2);
         
            switch (X)
            {
                case 1: goto _1020;
                case 2: goto _1090;
            }

            _870:
            goto _1020;
            _880:
            if (S != V) goto _910;
           
            if (Z == 1) goto _930;
          
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
           
            C = C + 1;
            D[R - 1, S] = 2;
            R = R - 1;
          
            if (C == H * V + 1) goto _1200;
           
            Q = 0;
            goto _270;
            _980:
            W[R, S - 1] = C;
            _990:
            C = C + 1;
           
            D[R, S - 1] = 1;
            S = S - 1;
            if (C == H * V + 1) goto _1200;
           
            Q = 0;
            goto _270;

            _1020:
            W[R + 1, S] = C;
            C = C + 1;
            if (D[R, S] == 0) goto _1050;
            D[R, S] = 3;
            goto _1060;

            _1050:
            D[R, S] = 2;

            _1060:
            R = R + 1;
     
            if (C == H * V + 1) goto _1200;
     
            goto _600;
            _1090:
            if (Q == 1) goto _1150;
     
            W[R, S + 1] = C;
            C = C + 1;
            if (D[R, S] == 0) goto _1120;
      
            D[R, S] = 3;
            goto _1130;
            _1120:
            D[R, S] = 1;
            _1130:
            S = S + 1;
            if (C == V * H + 1) goto _1200;
       
            goto _270;
            _1150:
            Z = 1;
       
            if (D[R, S] == 0) goto _1180;
       
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
    }
}
