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
            var blocks = CalculateTotalBlocks();

            var exitComplete = false;
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

      
        while (!AllBlocksVisited())
        {
            var nextAction = GetNextAction();

            switch (nextAction)
            {
                case NextAction.MoveUp:
                    KnockThroughAndMoveUp();
                    break;
                case NextAction.MoveRight:
                    if (IsLastRow())
                    {
                        SetExitComplete();
                        SetBlockOpenTop();
                        FindJunction();
                    }
                    else
                    {
                        SetBlockAboveVisited();
                        SetBlockOpenTop();
                        MoveRight();
                    }
                    break;
                case NextAction.MoveDown:
                    SetBlockToRightVisited();
                    SetCurrentBlockOpenRight();
                    MoveDown();
                    break;
                case NextAction.MoveLeft:
                    KnockThroughAndMoveLeft();
                    break;
                default:
                    FindJunction();
                    break;
                }
        }

        return maze;
            
            /////////////////////////////////////////
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
                return !GetPositionHistoryVisited(column, row) ;
            }

            bool BlockToLeftIsVisited()
            {
                return GetPositionHistoryVisited(column - 1, row);
            }

            bool BlockAboveIsVisited()
            {
                return GetPositionHistoryVisited(column, row - 1);
            }

            bool BlockToRightIsVisited()
            {
                return GetPositionHistoryVisited(column + 1, row);
            }

            bool BlockBelowIsVisited()
            {
                return GetPositionHistoryVisited(column, row + 1);
            }

            bool GetPositionHistoryVisited(int x, int y)
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

            void SetBlockOpenTop()
            {
                maze[column, row] = maze[column, row] | 1;
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

            void SetToBeginning()
            {
                column = 1;
                row = 1;
            }

            void SetExitComplete()
            {
                exitComplete = true;
            }

            bool IsLastRow()
            {
                return !IsNotLastRow();
            }

            void SetBlockToLeftOpenRight()
            {
                maze[column - 1, row] = 2;
            }

            void KnockThroughAndMoveLeft()
            {
                SetBlockToLeftVisited();
                SetBlockToLeftOpenRight();
                MoveLeft();
            }

            void KnockThroughAndMoveUp()
            {
                SetBlockBelowVisited();
                SetBlockAboveOpenTop();
                MoveUp();
            }

            void SetCurrentBlockOpenRight()
            {
                maze[column, row] = maze[column, row] | 2;
            }

            void SetToBeginningOfNextRow()
            {
                column = 1;
                row += 1;
                AnimationChangeOutput?.DrawFrame(maze, column, row);
            }

            int CalculateTotalBlocks()
            {
                return width * height + 1;
            }

            void FindJunction()
            {
                do
                {
                    if (IsNotEndOfRow())
                        MoveDown();
                    else if (IsNotLastRow())
                        SetToBeginningOfNextRow();
                    else
                        SetToBeginning();
                } while (CurrentBlockIsNotVisited());
            }

            NextAction GetNextAction()
            {
                if (IsStartOfRow() || BlockToLeftIsVisited())
                {
                    if (IsFirstRow() || BlockAboveIsVisited())
                    {
                        if (IsEndOfRow() || BlockToRightIsVisited())
                        {
                            if (IsNotLastRow() && BlockBelowIsVisited() || !IsNotLastRow() && exitComplete)
                                return NextAction.FindJunction; // goto FindJunction;
                            return NextAction.MoveRight; // goto MoveRight;
                        }

                        if (IsNotLastRow())
                        {
                            if (BlockBelowIsVisited()) return NextAction.MoveDown; // goto MoveDown;


                            switch ((int) Random.RND(2))
                            {
                                case 1: return NextAction.MoveDown; // goto MoveDown;
                                default:
                                    return NextAction.MoveRight;
                            }
                        }

                        if (exitComplete) return NextAction.MoveDown; // goto MoveDown;

                        return NextAction.MoveUp; // goto MoveUp;
                    }

                    if (IsEndOfRow() || BlockToRightIsVisited())
                    {
                        if (IsNotLastRow() && BlockBelowIsVisited() || !IsNotLastRow() && exitComplete) 
                            return NextAction.MoveUp; // goto MoveUp;

                        switch ((int) Random.RND(2))
                        {
                            case 1: return NextAction.MoveUp; // goto MoveUp;
                            default:  return NextAction.MoveRight;
                        }
                    }

                    if (IsNotLastRow() && BlockBelowIsVisited() || !IsNotLastRow() && exitComplete)
                    {
                        switch ((int) Random.RND(2))
                        {
                            case 1: return NextAction.MoveUp; // goto MoveUp;
                            default:
                                return NextAction.MoveDown;
                        }
                    }

                    switch ((int) Random.RND(3))
                    {
                        case 1: return NextAction.MoveUp; // goto MoveUp;
                        case 2: return NextAction.MoveDown; // goto MoveDown;
                        default:
                            return NextAction.MoveRight;
                    }
                }

                if (IsFirstRow() || BlockAboveIsVisited())
                {
                    if (IsEndOfRow() || BlockToRightIsVisited())
                    {
                        if (IsNotLastRow() && BlockBelowIsVisited() || !IsNotLastRow() && exitComplete) 
                            return NextAction.MoveLeft; // goto MoveLeft;

                        switch ((int) (Random.RND(0) * 2 + 1))
                        {
                            case 1: return NextAction.MoveLeft; // goto MoveLeft;
                            default:
                                return NextAction.MoveRight;
                        }
                    }

                    if (IsNotLastRow() && BlockBelowIsVisited() || !IsNotLastRow() && exitComplete)
                    {
                        switch ((int) Random.RND(2))
                        {
                            case 1: return NextAction.MoveLeft; // goto MoveLeft;
                            default:
                                return NextAction.MoveDown;
                        }
                    }

                    switch ((int) Random.RND(3))
                    {
                        case 1: return NextAction.MoveLeft; // goto MoveLeft;
                        case 2: return NextAction.MoveDown; // goto MoveDown;
                        default:
                            return NextAction.MoveRight;
                    }
                }

                if (IsEndOfRow() || BlockToRightIsVisited())
                {
                    if (IsNotLastRow() && BlockBelowIsVisited() || !IsNotLastRow() && exitComplete)
                    {
                        switch ((int) Random.RND(2))
                        {
                            case 1: return NextAction.MoveLeft; // goto MoveLeft;
                            default: return NextAction.MoveUp;
                        }
                    }

                    switch ((int) Random.RND(3))
                    {
                        case 1: return NextAction.MoveLeft; // goto MoveLeft;
                        case 2: return NextAction.MoveUp; // goto MoveUp;
                        default:
                            return NextAction.MoveRight;
                    }
                }

                var direction = (int) Random.RND(3);


                switch (direction)
                {
                    case 1: return NextAction.MoveLeft; // goto MoveLeft;
                    case 2: return NextAction.MoveUp; // goto MoveUp;
                    default:
                        return NextAction.MoveDown;
                }
            }
        }

    }
}
