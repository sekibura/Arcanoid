using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SekiburaGames.Arkanoid.Gameplay
{
    namespace SekiburaGames.Arkanoid.Gameplay
    {
        [CreateAssetMenu(fileName = "LevelsAsset", menuName = "LevelsAsset")]
        public class LevelsSO : ScriptableObject
        {
            [TextArea(3, 10)]
            public string[] Levels;


            public int[,] GetLevel(int index)
            {

                return ConvertStringToMatrix(Levels[index]);
            }

            public int[,] ConvertStringToMatrix(string input)
            {
                // Разделение строки на строки
                string[] rows = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                // Определение количества строк и столбцов
                int rowCount = rows.Length;
                int colCount = rows[0].Split(' ').Length;

                // Инициализация матрицы
                int[,] matrix = new int[rowCount, colCount];

                // Заполнение матрицы
                for (int i = 0; i < rowCount; i++)
                {
                    string[] cols = rows[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < colCount; j++)
                    {
                        matrix[i, j] = int.Parse(cols[j]);
                    }
                }

                return matrix;
            }
        }
    }
}
