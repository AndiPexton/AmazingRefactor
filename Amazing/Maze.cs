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
    public class Maze
    {
        private static IRandom Random => Shelf.RetrieveInstance<IRandom>();
        private static IAnimationChangeOutput AnimationChangeOutput => Shelf.RetrieveInstance<IAnimationChangeOutput>();

        public static int[,] BuildMaze(int width, int height)
        {
            var clear = true;
            var positionHistory = new int[width + 1, height + 1];
            var maze = new int[width + 1, height + 1];
            var blocks = CalculateTotalBlocks(width, height);

            var Q = 0;
            var Z = 0;
            var opening = (int) Random.RND(width);
            var column = opening;
            var row = 1;

            AnimationChangeOutput?.DrawFrame(maze, column, row);

            foreach (var block in Enumerable.Range(1, width)) //130 FOR I=1 TO H
                maze[block, 0] = block == opening ? 3 : 2;

            AnimationChangeOutput?.DrawFrame(maze, column, row);

            var blocksVisited = 1;
            positionHistory[opening, 1] = blocksVisited;
            blocksVisited += 1;

            goto _270;
            CheckPosition:
            if (column != width) goto AdvanceColumn;

            if (row != height) goto NextRow;


            column = 1;
            row = 1;
            goto CheckRoute;
            NextRow:
            (column, row) = StartOfNextRow(row);
            goto CheckRoute;
            AdvanceColumn:
            column = column + 1;
            AnimationChangeOutput?.DrawFrame(maze, column, row);
            CheckRoute:
            if (positionHistory[column, row] == 0) goto CheckPosition;
            _270:
            if (column - 1 == 0) goto _600;

            if (positionHistory[column - 1, row] != 0) goto _600;

            if (row - 1 == 0) goto _430;

            if (positionHistory[column, row - 1] != 0) goto _430;

            if (column == width) goto _350;

            if (positionHistory[column + 1, row] != 0) goto _350;

            opening = (int) Random.RND(3);


            switch (opening)
            {
                case 1: goto _940;
                case 2: goto _980;
                case 3: goto _1020;
            }

            _350:
            if (row != height) goto _380;

            if (Z == 1) goto _410;

            Q = 1;
            goto _390;
            _380:
            if (positionHistory[column, row + 1] != 0) goto _410;
            _390:
            opening = (int) Random.RND(3);

            switch (opening)
            {
                case 1: goto _940;
                case 2: goto _980;
                case 3: goto _1090;
            }

            _410:
            opening = (int) Random.RND(2);

            switch (opening)
            {
                case 1: goto _940;
                case 2: goto _980;
            }

            _430:
            if (column == width) goto _530;

            if (positionHistory[column + 1, row] != 0) goto _530;

            if (row != height) goto _480;

            if (Z == 1) goto _510;

            Q = 1;
            goto _490;

            _480:
            if (positionHistory[column, row + 1] != 0) goto _510;
            _490:
            opening = (int) Random.RND(3);

            switch (opening)
            {
                case 1: goto _940;
                case 2: goto _1020;
                case 3: goto _1090;
            }

            _510:
            opening = (int) Random.RND(2);

            switch (opening)
            {
                case 1: goto _940;
                case 2: goto _1020;
            }

            _530:
            if (row != height) goto _560;

            if (Z == 1) goto _590;

            Q = 1;
            goto _570;
            _560:
            if (positionHistory[column, row + 1] != 0) goto _590;
            _570:
            opening = (int) (Random.RND(0) * 2 + 1);

            switch (opening)
            {
                case 1: goto _940;
                case 2: goto _1090;
            }

            _590:
            goto _940;
            _600:
            if (row - 1 == 0) goto _790;

            if (positionHistory[column, row - 1] != 0) goto _790;

            if (column == width) goto _720;

            if (positionHistory[column + 1, row] != 0) goto _720;

            if (row != height) goto _670;

            if (Z == 1) goto _700;

            Q = 1;
            goto _680;
            _670:
            if (positionHistory[column, row + 1] != 0) goto _700;
            _680:
            opening = (int) Random.RND(3);

            switch (opening)
            {
                case 1: goto _980;
                case 2: goto _1020;
                case 3: goto _1090;
            }

            _700:
            opening = (int) Random.RND(2);

            switch (opening)
            {
                case 1: goto _980;
                case 2: goto _1020;
            }

            _720:
            if (row != height) goto _750;

            if (Z == 1) goto _780;

            Q = 1;
            goto _760;
            _750:
            if (positionHistory[column, row + 1] != 0) goto _780;
            _760:
            opening = (int) Random.RND(2);

            switch (opening)
            {
                case 1: goto _980;
                case 2: goto _1090;
            }

            _780:
            goto _980;

            _790:
            if (column == width) goto _880;

            if (positionHistory[column + 1, row] != 0) goto _880;

            if (row != height) goto _840;

            if (Z == 1) goto _870;

            Q = 1;
            goto _990;
            _840:
            if (positionHistory[column, row + 1] != 0) goto _870;

            opening = (int) Random.RND(2);

            switch (opening)
            {
                case 1: goto _1020;
                case 2: goto _1090;
            }

            _870:
            goto _1020;
            _880:
            if (row != height) goto _910;

            if (Z == 1) goto _930;

            Q = 1;
            goto _920;

            _910:
            if (positionHistory[column, row + 1] != 0) goto _930;
            _920:
            goto _1090;
            _930:
            goto CheckPosition;
            _940:
            positionHistory[column - 1, row] = blocksVisited;

            blocksVisited = blocksVisited + 1;
            maze[column - 1, row] = 2;

            MoveLeft();
            if (blocksVisited == blocks) return maze;

            Q = 0;
            goto _270;
            _980:
            positionHistory[column, row - 1] = blocksVisited;
            _990:
            blocksVisited = blocksVisited + 1;

            maze[column, row - 1] = 1;

            MoveUp();
            if (blocksVisited == width * height + 1) return maze;

            Q = 0;
            goto _270;

            _1020:
            positionHistory[column + 1, row] = blocksVisited;
            blocksVisited = blocksVisited + 1;
            if (maze[column, row] == 0) goto _1050;
            maze[column, row] = 3;

            goto _1060;

            _1050:
            maze[column, row] = 2;

            _1060:
            MoveDown();
            if (blocksVisited == blocks) return maze;

            goto _600;
            _1090:
            if (Q == 1) goto Set_Z;

            positionHistory[column, row + 1] = blocksVisited;
            blocksVisited = blocksVisited + 1;
            if (maze[column, row] == 0) goto _1120;

            maze[column, row] = 3;

            goto _1130;
            _1120:
            maze[column, row] = 1;

            _1130:
            MoveRight();
            if (blocksVisited == blocks) return maze;

            goto _270;
            Set_Z:
            Z = 1;

            if (maze[column, row] == 0) goto Set_Block_Open_Top_Reset_To_Start;

            maze[column, row] = 3;

            Q = 0;
            goto CheckPosition;
            Set_Block_Open_Top_Reset_To_Start:
            maze[column, row] = 1;

            Q = 0;
            column = 1;
            row = 1;
            goto CheckRoute;


           

            void MoveRight()
            {
                row = row + 1;
                AnimationChangeOutput?.DrawFrame(maze, column, row);
            }

            void MoveDown()
            {
                column = column + 1;
                AnimationChangeOutput?.DrawFrame(maze, column, row);
            }

            void MoveUp()
            {
                row = row - 1;
                AnimationChangeOutput?.DrawFrame(maze, column, row);
            }

            void MoveLeft()
            {
                column = column - 1;
                AnimationChangeOutput?.DrawFrame(maze, column, row);
            }
        }

        private static int CalculateTotalBlocks(int width, int height)
        {
            return width * height + 1;
        }


        private static (int, int) StartOfNextRow(int row)
        {
            return (1, row + 1);
        }
    }
}
