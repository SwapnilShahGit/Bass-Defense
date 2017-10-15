using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGen {
    int seed;
    System.Random gen;

    public RandomGen(int _seed) {
        seed = _seed;
        gen = new System.Random(seed);
    }

    public Queue<T> RandomShuffleArray<T>(T[] array) {
        for(int i = 0; i < array.Length - 1; i++) {
            int randomIndex = gen.Next(i, array.Length);
            T tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }
        return new Queue<T>(array);
    }

    public int GetRandomNumber(int begin, int end) {
        return gen.Next(begin, end);
    }
}
