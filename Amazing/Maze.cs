using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
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
            var positionHistory = new int[width  * height];
            var maze = new int[width + 1, height + 1];
            var blocks = CalculateTotalBlocks(width, height);

            var Q = 0;
            var Z = 0;
            var entrance = (int)Random.RND(width);
            var column = entrance;
            var row = 1;

            AnimationChangeOutput?.DrawFrame(maze, column, row);

            foreach (var block in Enumerable.Range(1, width)) //130 FOR I=1 TO H
                maze[block, 0] = block == entrance ? 3 : 2;

            AnimationChangeOutput?.DrawFrame(maze, column, row);

            var blocksVisited = 1;
            SetVisited(entrance, 1);
            blocksVisited += 1;

            goto _270;
            CheckPosition:
            if (IsNotEndOfRow()) goto AdvanceColumn;

            if (IsNotLastRow()) goto NextRow;


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
            if (CurrentBlockIsNotVisited()) goto CheckPosition;
            _270:
            if (IsStartOfRow() || BlockToLeftIsVisited()) goto _600;
            if (IsFirstRow() || BlockAboveIsVisited()) goto _430;
            if (IsEndOfRow() || BlockToRightIsVisited()) goto _350;
            
            var direction = (int) Random.RND(3);


            switch (direction)
            {
                case 1: goto _940;
                case 2: goto _980;
                case 3: goto _1020;
            }

            _350:
            if (IsNotLastRow()) goto _380;

            if (Z == 1) goto _410;

            Q = 1;
            goto _390;
            _380:
            if (BlockBelowIsVisited()) goto _410;
            _390:
            direction = (int) Random.RND(3);

            switch (direction)
            {
                case 1: goto _940;
                case 2: goto _980;
                case 3: goto _1090;
            }

            _410:
            direction = (int) Random.RND(2);

            switch (direction)
            {
                case 1: goto _940;
                case 2: goto _980;
            }

            _430:
            if (column == width) goto _530;

            if (BlockToRightIsVisited()) goto _530;

            if (row != height) goto _480;

            if (Z == 1) goto _510;

            Q = 1;
            goto _490;

            _480:
            if (BlockBelowIsVisited()) goto _510;
            _490:
            direction = (int) Random.RND(3);

            switch (direction)
            {
                case 1: goto _940;
                case 2: goto _1020;
                case 3: goto _1090;
            }

            _510:
            direction = (int) Random.RND(2);

            switch (direction)
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
            if (BlockBelowIsVisited()) goto _590;
            _570:
            direction = (int) (Random.RND(0) * 2 + 1);

            switch (direction)
            {
                case 1: goto _940;
                case 2: goto _1090;
            }

            _590:
            goto _940;
            _600:
            if (IsFirstRow()) goto _790;

            if (BlockAboveIsVisited()) goto _790;

            if (IsEndOfRow()) goto _720;

            if (BlockToRightIsVisited()) goto _720;

            if (IsNotLastRow()) goto _670;

            if (Z == 1) goto _700;

            Q = 1;
            goto _680;
            _670:
            if (BlockBelowIsVisited()) goto _700;
            _680:
            direction = (int) Random.RND(3);

            switch (direction)
            {
                case 1: goto _980;
                case 2: goto _1020;
                case 3: goto _1090;
            }

            _700:
            direction = (int) Random.RND(2);

            switch (direction)
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
            if (BlockBelowIsVisited()) goto _780;
            _760:
            direction = (int) Random.RND(2);

            switch (direction)
            {
                case 1: goto _980;
                case 2: goto _1090;
            }

            _780:
            goto _980;

            _790:
            if (IsEndOfRow()) goto _880;

            if (BlockToRightIsVisited()) goto _880;

            if (IsNotLastRow()) goto _840;

            if (Z == 1) goto _870;

            Q = 1;
           // SetCurrentBlockVisited();
            goto _980;
            _840:
            if (BlockBelowIsVisited()) goto _870;

            direction = (int) Random.RND(2);

            switch (direction)
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
            if (BlockBelowIsVisited()) goto _930;
            _920:
            goto _1090;
            _930:
            goto CheckPosition;
            _940:
            SetBlockToLeftVisited();

   
            maze[column - 1, row] = 2;

            MoveLeft();
            if (AllBlocksVisited()) return maze;

            Q = 0;
            goto _270;
            _980:
            SetBlockBelowVisited();
         
            SetBlockAboveOpenTop();

            MoveUp();
            if (AllBlocksVisited()) return maze;

            Q = 0;
            goto _270;

            _1020:
            SetBlockToRightVisited();
            
   
            if (maze[column, row] == 0) goto _1050;
            maze[column, row] = 3;

            goto _1060;

            _1050:
            maze[column, row] = 2;

            _1060:
            MoveDown();
            if (AllBlocksVisited()) return maze;

            goto _600;
            _1090:
            if (Q == 1) goto Set_Z;

            SetBlockAboveVisited();
            
            if (CurrentPositionBothClosed()) goto _1120;

            SetBlockOpenBoth();

            goto _1130;
            _1120:
            SetBlockOpenTop();

        _1130:
            MoveRight();
            if (AllBlocksVisited()) return maze;

            goto _270;
            Set_Z:
            Z = 1;

            if (CurrentPositionBothClosed()) goto Set_Block_Open_Top_Reset_To_Start;

            SetBlockOpenBoth();

            Q = 0;
            goto CheckPosition;
            Set_Block_Open_Top_Reset_To_Start:
            SetBlockOpenTop();

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

            bool CurrentBlockIsNotVisited()
            {
                return !GetPositionHistory(column, row) ;
            }

            bool BlockToLeftIsVisited()
            {
                return GetPositionHistory(column - 1, row);
            }

            bool BlockAboveIsVisited()
            {
                return GetPositionHistory(column, row - 1);
            }

            bool BlockToRightIsVisited()
            {
                return GetPositionHistory(column + 1, row);
            }

            bool BlockBelowIsVisited()
            {
                return GetPositionHistory(column, row + 1);
            }

            bool GetPositionHistory(int x, int y)
            {
                return positionHistory[CalculatePositionForHistory(x, y)] != 0;
            }

            void SetBlockToLeftVisited()
            {
                SetVisited(column - 1, row);
                blocksVisited = blocksVisited + 1;
            }

            void SetBlockBelowVisited()
            {
                SetVisited(column, row - 1);
                blocksVisited = blocksVisited + 1;
            }

            bool AllBlocksVisited()
            {
                return blocksVisited == blocks;
                //return positionHistory.All(x => x > 0);
            }

            void SetBlockToRightVisited()
            {
                SetVisited(column + 1, row);
                blocksVisited = blocksVisited + 1;
            }

            void SetBlockAboveVisited()
            {
                SetVisited(column, row + 1);
                blocksVisited = blocksVisited + 1;
            }

            bool CurrentPositionBothClosed()
            {
                return maze[column, row] == 0;
            }

            void SetBlockOpenTop()
            {
                maze[column, row] = 1;
            }

            void SetBlockOpenBoth()
            {
                maze[column, row] = 3;
            }

            void SetBlockAboveOpenTop()
            {
                maze[column, row - 1] = 1;
            }

            bool IsFirstRow()
            {
                return row - 1 == 0;
            }

            bool IsEndOfRow()
            {
                return column == width;
            }

            bool IsNotLastRow()
            {
                return row != height;
            }

            void SetVisited(int x, int y)
            {
                positionHistory[CalculatePositionForHistory(x, y)] = blocksVisited;
            }

            int CalculatePositionForHistory(int x, int y)
            {
                return x-1 + (y -1) * height;
            }

            bool IsStartOfRow()
            {
                return column - 1 == 0;
            }

            bool IsNotEndOfRow()
            {
                return column != width;
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
