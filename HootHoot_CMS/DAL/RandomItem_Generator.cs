using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HootHoot_CMS.DAL
{
    public class RandomItem_Generator<T> where T : class
    {
        private Random randNum_Generator = null;
        private IList<T> itemList = null;
        private int[] randIndex = null;
        private int resultSize = -1;

        public RandomItem_Generator(IList<T> itemList, int resultSize)
        {
            this.randNum_Generator = new Random();
            this.itemList = itemList;
            this.resultSize = resultSize;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<T> getRandomItem()
        {
            IList<T> randomItems_List = new List<T>();

            for (byte count = 0; count < resultSize; count++)
            {
                randomItems_List.Add(itemList[ randIndex[count] ]);
            }

            return randomItems_List;
        }

        /// <summary>
        /// 
        /// </summary>
        public void preparesRandomIndex()
        {
            randIndex = new int[resultSize];
            int randNum = -1;
            byte randFault = 0;
            byte i = 0;

            initializeArrayDefaults();

            while (i < resultSize)
            {
                randNum = randNum_Generator.Next(itemList.Count);
                if (!(randIndex.Contains(randNum)))
                {
                    randIndex[i] = randNum;
                    i++;
                    continue; // Proceed straight to the next iteration (ignore execution outside this branch)
                }

                randFault++;

                if(randFault > 2)
                {
                    getNewSeed();
                    randFault = 0;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void initializeArrayDefaults()
        {
            for (byte initCount = 0; initCount < resultSize; initCount++)
            {
                randIndex[initCount] = -1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void getNewSeed()
        {
            Thread.Sleep(500);
            randNum_Generator = null;
            randNum_Generator = new Random();
        }

    }
}
